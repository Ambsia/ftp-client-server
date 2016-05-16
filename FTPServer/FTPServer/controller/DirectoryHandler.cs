using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTPServer.model;

namespace FTPServer.controller
{
    public class DirectoryHandler
    {
        public FileRepository FileRepository { get; }
        public DirectoryHandler(FileRepository fileRepository)
        {
            this.FileRepository = fileRepository;
        }

        public void CreateNewDirectory(FTPLibrary.Directory newDirectory)
        {
            
        }
        public void AddFileToDirectory()
        {
            
        }
        public void RemoveDirectory()
        {
            
        }
        public void RemoveFileFromDirectory()
        {
            
        }
    }
}
