using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using FTPLibrary;
using FTPServer.util;
using Newtonsoft.Json;
using Directory = FTPLibrary.Directory;
using File = System.IO.File;

namespace FTPServer.model
{
    static class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        public static class FILE_ATTRIBUTE
        {
            public const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        }

        public static class SHGFI
        {
            public const uint SHGFI_TYPENAME = 0x000000400;
            public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        }

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
    }
    public class FileRepository
    {

        public event EventHandler RefreshTree;
        protected virtual void OnRefreshTree()
        {
            RefreshTree?.Invoke(this, EventArgs.Empty);
        }

        public const long MaxFileSize = 8589934592;

        uint dwFileAttributes = NativeMethods.FILE_ATTRIBUTE.FILE_ATTRIBUTE_NORMAL;
        uint uFlags = (uint)(NativeMethods.SHGFI.SHGFI_TYPENAME | NativeMethods.SHGFI.SHGFI_USEFILEATTRIBUTES);
        private NativeMethods.SHFILEINFO info;

        private Directory _rootDirectory;
        public Directory RootDirectory
        {
            get { return _rootDirectory; }
            set
            {
                _rootDirectory = value;
                FillDictionary();
                SaveDictionaryToFile();
            }
        }
        private string FileName { get; }

        public FileRepository(string fileName, Directory rootDirectory)
        {
            this.FileName = fileName;
            this.RootDirectory = rootDirectory;
            this.FillDictionary();
            this.SaveDictionaryToFile();
            //   this.LoadDictionaryFromFile();
            info = new NativeMethods.SHFILEINFO();

        }


        public void SaveDictionaryToFile()
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream fileStream = new FileStream(FileName, FileMode.Create, FileAccess.Write))
                {
                    binaryFormatter.Serialize(fileStream, JsonConvert.SerializeObject(RootDirectory));
                }
                ConsoleBuffer.AddToActionLog("Dictionary saved as list to file.");
            }
            catch (Exception e)
            {
                ConsoleBuffer.AddToActionLog("Exeception Occured, could not write to file." + e.ToString());
            }
        }

        public void LoadDictionaryFromFile()
        {
            try
            {
                using (FileStream fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    var jsonString = (string)binaryFormatter.Deserialize(fileStream);

                    this.RootDirectory = JsonConvert.DeserializeObject<Directory>(jsonString);
                }
              //  ConsoleBuffer.AddToActionLog("File loaded as dictionary." + "Dictionary count: " + this._directoryDictionary.Count + ".");
            }
            catch (Exception e)
            {
                ConsoleBuffer.AddToActionLog("Exeception Occured, could not load file." + e.Message);
            }
        }

        public Directory FindDirectory(Directory directoryToSearch, string path)
        {
            if (directoryToSearch.FullPath == path)
            {
                return directoryToSearch;;
            }

            if (directoryToSearch.SubDirectories.Count > 0)
            {
                foreach (Directory dir in directoryToSearch.SubDirectories.Values)
                {
                    if (dir.FullPath == path)
                    {
                        return dir;
                    }
                    if (dir.SubDirectories.Count > 0)
                    {
                        return FindDirectory(dir, path);
                    }
                }
            }
            else
            {
                return directoryToSearch.SubDirectories.Values.Select(directory => FindDirectory(directory, path)).FirstOrDefault();
            }

            return RootDirectory;
        }

        public void FillDictionary()
        {
            InitialiseAllDirectories(RootDirectory, 0);
            OnRefreshTree();
            //  SaveDictionaryToFile();
            //  LoadDictionaryFromFile();
        }

        //recusrively creates new directory/file objects to create a nested directory object, each 
        //directory consists of a dictionary of directorys and a dictionary of files,
        //a dictionary is brilliant for this, as files within the same directory cannot have the same
        //name, same with directories. dictionary keys provide this functionality.
        public void InitialiseAllDirectories(Directory currentDirectory, int i)
        {

            var dirsFound = System.IO.Directory.GetDirectories(currentDirectory.FullPath);
            if (dirsFound.Length > 0)
            {
                if (i < dirsFound.Length)
                {
                    if (currentDirectory.SubDirectories.ContainsKey(dirsFound[i])) return;
                    foreach (FileInfo fileInfo in System.IO.Directory.GetFiles(currentDirectory.FullPath).Select(file => new FileInfo(file)).Where(fileInfo => !currentDirectory.Files.ContainsKey(fileInfo.Name)))
                    {

                        NativeMethods.SHGetFileInfo(fileInfo.FullName, dwFileAttributes, ref info, (uint)Marshal.SizeOf(info), uFlags);
                        FTPLibrary.File file = new FTPLibrary.File(
                                         fileInfo.Name,
                                         info.szTypeName,
                                         fileInfo.FullName,
                                         fileInfo.LastWriteTime,
                                         fileInfo.Length,
                                         System.IO.File.GetAccessControl(fileInfo.FullName).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString());
                        currentDirectory.AddFile(file);
                    }
                    DirectoryInfo dirToAdd = new DirectoryInfo(dirsFound[i]);
                    Client client = new Client(System.IO.Directory.GetAccessControl(dirToAdd.FullName).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString());

                    Directory directory = new Directory(
                           dirToAdd.Name,
                           dirToAdd.FullName/*.Substring(dirToAdd.FullName.IndexOf(RootDirectory.DirectoryName, StringComparison.Ordinal))*/,
                           new List<Client>() { client },
                           client
                           );
                    currentDirectory.AddDirectory(directory);
                    InitialiseAllDirectories(currentDirectory, ++i);
                }
                else
                {
                    foreach (Directory directory in currentDirectory.SubDirectories.Values)
                    {
                        InitialiseAllDirectories(directory, 0);
                    }
                }
            }
            else if (System.IO.Directory.GetFiles(currentDirectory.FullPath).Length > 0)
            {
                foreach (string file in System.IO.Directory.GetFiles(currentDirectory.FullPath))
                {
                    NativeMethods.SHGetFileInfo(file, dwFileAttributes, ref info, (uint)Marshal.SizeOf(info), uFlags);
                    FileInfo fileInfo = new FileInfo(file);
                    if (!currentDirectory.Files.ContainsKey(fileInfo.Name))
                    {
                        FTPLibrary.File f = new FTPLibrary.File(
                                         fileInfo.Name,
                                         info.szTypeName,
                                         fileInfo.FullName/*Substring(fileInfo.FullName.IndexOf(RootDirectory.DirectoryName, StringComparison.Ordinal)*/,
                                         fileInfo.LastWriteTime,
                                         fileInfo.Length,
                                         System.IO.File.GetAccessControl(fileInfo.FullName).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString());
                        currentDirectory.AddFile(f);
                    }
                }
            }
        }

        public TreeNode CreateDirectoryTree(FTPLibrary.Directory directory)
        {
            var directoryNode = new TreeNode(directory.DirectoryName);
            foreach (FTPLibrary.Directory dir in directory.SubDirectories.Values)
                directoryNode.Nodes.Add(CreateDirectoryTree(dir));
            foreach (FTPLibrary.File file in directory.Files.Values)
                directoryNode.Nodes.Add(new TreeNode(file.FileName));
            return directoryNode;
        }


    
    }
}
