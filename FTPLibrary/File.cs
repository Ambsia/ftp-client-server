using System;

namespace FTPLibrary
{
    [Serializable]
    public class File
    {
        public string FileName { get; }
        public string FileType { get; }
        public string Path { get; set; }
        public DateTime DateTimeModified { get; }
        public long FileSize { get; }
        public string FileAuthor { get; }
        public string FileSizeAsString => FileSize.LongToString();

        public File(string fileName, string fileType, string path, DateTime dateTimeModified, long fileSize, string fileAuthor)
        {
            FileName = fileName;
            FileType = fileType;
            Path = path;
            DateTimeModified = dateTimeModified;
            FileSize = fileSize;
            FileAuthor = fileAuthor;
        }
    }
}
