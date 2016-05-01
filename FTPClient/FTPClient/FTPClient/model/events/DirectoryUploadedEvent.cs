using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTPLibrary;

namespace FTPClient.model.events
{
    public class UploadedEvent
    {
        public class UploadedDirectoryArgs : EventArgs
        {
            public UploadedDirectoryArgs(Directory directory)
            {
                this.Directory = directory;
            }
            public Directory Directory { get; }
        }

        public delegate void DirectoryUploadedEventHandler(object sender, UploadedDirectoryArgs e);

        public class UploadedFileArgs : EventArgs
        {
            public UploadedFileArgs(FTPLibrary.File file)
            {
                this.File = file;
            }
            public File File { get; }
        }

        public delegate void FileUploadedEventHandler(object sender, UploadedFileArgs e);

        public class FileListProgressArgs : EventArgs
        {
            public FileListProgressArgs(List<FileProgress> files)
            {
                this.Files = files;
            }
            public List<FileProgress> Files { get; }
        }
        public delegate void ListOfProgressFileEventHandler(object sender, FileListProgressArgs e);


        public class FileProgressArgs : EventArgs
        {
            public FileProgressArgs(FileProgress f, int progress)
            {
                this.File = f;
                this.Progress = progress;
            }

            public FileProgress File { get; }
            public int Progress { get; }
        }


        public delegate void ProgressEventHandler(object sender, FileProgressArgs e);

    }


}
