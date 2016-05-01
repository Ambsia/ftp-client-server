using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FTPLibrary
{
    //this class represents a virtual directory system
    [Serializable]
    public class Directory 
    {
        public string DirectoryName { get; }
        //the list will always have one user in, that being the owner.
        public List<Client> PermittedUsers { get; }
        public Client DirectoryOwner { get; }

        public Dictionary<string, File> Files;

        public Dictionary<string, Directory> SubDirectories; 
        public string FullPath { get; set; }
    
        public Directory(string directoryName, string fullPath, List<Client> permittedUsers, Client directoryOwner)
        {
            this.DirectoryName = directoryName;
            this.FullPath = fullPath;
            this.PermittedUsers = permittedUsers;
            this.DirectoryOwner = directoryOwner;
            this.Files = new Dictionary<string, File>();
            this.SubDirectories = new Dictionary<string, Directory>();
        }



        public void AddFile(File file)
        {
            file.Path = FullPath + @"\" + file.FileName;

            //if a new file is added in the same directory, we just overwrite it
            if (this.Files.ContainsKey(file.FileName))
                this.Files.Remove(file.FileName);
            
            this.Files.Add(file.FileName,file);
        }

        public void AddDirectory(Directory directoryToAdd)
        {
            directoryToAdd.FullPath = this.FullPath + @"\" + directoryToAdd.DirectoryName;

            if (this.SubDirectories.ContainsKey(directoryToAdd.DirectoryName))
                this.SubDirectories.Remove(directoryToAdd.DirectoryName);

            this.SubDirectories.Add(directoryToAdd.DirectoryName, directoryToAdd);
        }
    }
}
