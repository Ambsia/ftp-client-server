using System.ComponentModel;

namespace FTPLibrary
{
    public class FileProgress : INotifyPropertyChanged
    {
        public string FileName { get; set; }
        public bool IsUpload { get; set; }
        public bool IsDownload { get; set; }
        public string Size { get; set; }
        private string _completed = "Queued..";

        private long _progress;
        public FileProgress(string fileName, bool isUpload, bool isDownload, string size, long progress)
        {
            this.FileName = fileName;
            this.IsUpload = isUpload;
            this.IsDownload = isDownload;
            this.Size = size;
            this._progress = progress;
          
        }
        public long Progress
        {
            get { return _progress; }
            set
            {
                if (_progress == value) return;
                _progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public string Completed
        {
            get { return _completed; }
            set
            {
                if (_completed == value) return;
                _completed = value;
                OnPropertyChanged("Completed");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
