using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FTPClient.model
{
    public class TreeNodeItem : TreeViewItem , INotifyPropertyChanged
    {
        public TreeNodeItem(string path)
        {
            Path = path;
            if (path.Contains("\\"))
            {
                this.Header = path.Substring(path.LastIndexOf("\\")) == "\\" ? path.Replace(@"\", "") : path.Substring(path.LastIndexOf("\\"));
                if (((string) Header).Contains(@"\"))
                {
                    this.Header = ((string) Header).Replace(@"\", "");
                }
            }
            else
            {
                Path = path;
            }
        }

        public string Path { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
