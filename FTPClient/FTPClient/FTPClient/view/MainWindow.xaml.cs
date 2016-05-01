using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using FTPClient.controller;
using FTPClient.model;
using FTPLibrary;
using Directory = FTPLibrary.Directory;
using File = FTPLibrary.File;
using Label = System.Windows.Controls.Label;

namespace FTPClient.view
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        private readonly Host _hostModel;
        private readonly ClientHandler _clientHandler;
        private readonly List<string> _log;
        private readonly System.Windows.Forms.Timer _logWorker;
        private NativeMethods.SHFILEINFO _info = new NativeMethods.SHFILEINFO();
        private const uint DwFileAttributes = NativeMethods.FILE_ATTRIBUTE.FILE_ATTRIBUTE_NORMAL;
        private const uint _uFlags = (uint)(NativeMethods.SHGFI.SHGFI_TYPENAME | NativeMethods.SHGFI.SHGFI_USEFILEATTRIBUTES);
        public MainWindow()
        {
            
            InitializeComponent();
            _hostModel = new Host("host.dat");
            DataContext = _hostModel;

            TextBoxLocalDirectory.KeyDown += TextBoxLocalDirectory_KeyDown
                ;
            FileLocalTreeView();
            _clientHandler = new ClientHandler();

            _clientHandler.ServerEvent += _clientHandler_ServerEvent;
            _clientHandler.ServerError += _clientHandler_ServerError;
            _clientHandler.Disconnected += _clientHandler_Disconnected;
            _clientHandler.LoginAuthorised += _clientHandler_LoginAuthorised;
            _clientHandler.ClientInfo += _clientHandler_ClientInfo;
            _clientHandler.DirectoryUploaded += _clientHandler_DirectoryUploaded;
            _clientHandler.FileUploaded += _clientHandler_FileUploaded;
            _clientHandler.ListOfFiles += _clientHandler_ListOfFiles;
            _log = new List<string>();
            _logWorker = new System.Windows.Forms.Timer();
            _logWorker.Tick += Worker_DoWork;
            _logWorker.Interval = 400;
            _logWorker.Start();


            

        }

        private void _clientHandler_ListOfFiles(object sender, model.events.UploadedEvent.FileListProgressArgs e)
        {
            this.Dispatcher.Invoke((Action) (() =>
            {
                foreach (FileProgress file in e.Files)
                {
                    ProgressListview.Items.Add(file);
                }
            }));
        }
        private void _clientHandler_FileUploaded(object sender, model.events.UploadedEvent.UploadedFileArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                TreeNodeItem root = ((TreeNodeItem)RemoteDirectoryTreeView.ItemContainerGenerator.ContainerFromIndex(0));

                File f = e.File;
                ((Directory)root.Tag).AddFile(f);
                root.MouseDoubleClick += RemoteDirectoryTreeView_MouseDoubleClick;

               // e.File.Path = root.Path + @"\" + e.File.FileName;

                TreeNodeItem item = new TreeNodeItem(f.Path) {Tag = f};

                foreach (TreeNodeItem node in root.Items)
                {
                    object tag = node.Tag;
                    if (!(tag is File)) continue;
                    if (((File)tag).FileName == ((File)item.Tag).FileName)
                    {
                        root.Items.Remove(node);
                        break;
                    }
                }

                root.Items.Add(item);
                RemoteDirectoryTreeView.Items.SortDescriptions.Clear();
                RemoteDirectoryTreeView.Items.SortDescriptions.Add(new SortDescription("Header", ListSortDirection.Ascending));
            }));
        }

        private void _clientHandler_DirectoryUploaded(object sender, model.events.UploadedEvent.UploadedDirectoryArgs e)
        {
            this.Dispatcher.Invoke((Action) (() =>
            {
                TreeNodeItem root = ((TreeNodeItem) RemoteDirectoryTreeView.ItemContainerGenerator.ContainerFromIndex(0));

                e.Directory.FullPath = root.Header + @"\" + e.Directory.DirectoryName;
                TreeNodeItem item = new TreeNodeItem(e.Directory.FullPath);

                foreach (FTPLibrary.File file in e.Directory.Files.Values)
                {
                    file.Path = e.Directory.FullPath + @"\" + file.FileName;
                    TreeNodeItem fileNode = new TreeNodeItem(file.Path) { Tag = file, Header = file.FileName };
                    item.Items.Add(fileNode);
                }
                item.MouseDoubleClick += RemoteDirectoryTreeView_MouseDoubleClick;
                item.Tag = e.Directory;

                foreach (TreeNodeItem node in root.Items) 
                {
                    object tag = node.Tag;
                    if (!(tag is Directory)) continue;
                    if (((Directory) tag).DirectoryName == ((Directory) item.Tag).DirectoryName)
                    {
                        root.Items.Remove(node);
                        break;
                    }
                }

                root.Items.Add(item);
                RemoteDirectoryTreeView.Items.SortDescriptions.Clear();
                RemoteDirectoryTreeView.Items.SortDescriptions.Add(new SortDescription("Header", ListSortDirection.Ascending));
                RemoteDirectoryTreeView.Items.Refresh();
            }));
        }

        private void Worker_DoWork(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke((Action) (() =>
            {
                var bc = new BrushConverter();
                clientLog.Text = String.Join(String.Empty, _log.ToArray().Reverse());
            }));
        }

        private void _clientHandler_ClientInfo(object sender, model.events.ClientInfoArgs e)
        {

            _log.Add("[" + DateTime.Now + "] " + e.Information + Environment.NewLine);
        }

        private void _clientHandler_LoginAuthorised(object sender, EventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke((Action) (() =>
                {
                    txtIP.IsEnabled = false;
                    txtPort.IsEnabled = false;
                    txtUsername.IsEnabled = false;
                    txtPassword.IsEnabled = false;
                    var bc = new BrushConverter();
                    btnConnection.Background = (System.Windows.Media.Brush) bc.ConvertFrom("#FFB23C3C");
                    btnConnection.Content = "DISCONNECT";
                    btnConnection_Copy.Background = (System.Windows.Media.Brush) bc.ConvertFrom("#FF32ABBF");
                    btnConnection_Copy.IsEnabled = true;
                    ServerLogBorder.BorderBrush = (System.Windows.Media.Brush)bc.ConvertFrom("#FF669966");
                    this.BorderBrush = (System.Windows.Media.Brush)bc.ConvertFrom("#FF669966");
                    this.ServerProgress.BorderBrush = (System.Windows.Media.Brush)bc.ConvertFrom("#FF669966");

                }));
            }
            catch (Exception)
            {
                
            }
        } 

        private void _clientHandler_Disconnected(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke((Action) (() =>
            {
                RemoteDirectoryTreeView.Items.Clear();
                RemoteDirectoryInfoListView.Items.Clear(); 
                txtIP.IsEnabled = true;
                txtPort.IsEnabled = true;
                txtUsername.IsEnabled = true;
                txtPassword.IsEnabled = true;
                var bc = new BrushConverter();
                btnConnection.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FF669966");
                btnConnection.Content = "CONNECT";
                TextBoxRemoteDirectory.Text = @"ftp:\";
                btnConnection_Copy.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFAAC9CF");
                btnConnection_Copy.IsEnabled = false;
                ServerLogBorder.BorderBrush = (System.Windows.Media.Brush)bc.ConvertFrom("#FFB23C3C");
                this.BorderBrush = (System.Windows.Media.Brush)bc.ConvertFrom("#FFB23C3C");
                this.ServerProgress.BorderBrush = (System.Windows.Media.Brush)bc.ConvertFrom("#FFB23C3C");

            }));
        }

        private void _clientHandler_ServerError(object sender, ServerErrorArgs e)
        {
            this.Dispatcher.Invoke((Action) (() =>
            {
                _log.Add("[" + DateTime.Now + "] " + e.ErrorMessage + Environment.NewLine);
            }));
        }

        private void _clientHandler_ServerEvent(object sender, ServerArgs e)
        {

            this.Dispatcher.Invoke((Action)(() =>
            {
                RemoteDirectoryTreeView.Items.Clear();
                RemoteDirectoryTreeView.Items.Add(CreateDirectoryTreeNodeItem(e.RootNode));
                TreeNodeItem root = ((TreeNodeItem)RemoteDirectoryTreeView.ItemContainerGenerator.ContainerFromIndex(0));
                root.IsExpanded = true;
                root.MouseDoubleClick += RemoteDirectoryTreeView_MouseDoubleClick;
            }));
        }

        public TreeNodeItem CreateDirectoryTreeNodeItem(FTPLibrary.Directory directory)
        {
            var directoryNode = new TreeNodeItem(directory.FullPath) { Tag = directory, Header = directory.DirectoryName };

            foreach (FTPLibrary.Directory dir in directory.SubDirectories.Values)
                directoryNode.Items.Add(CreateDirectoryTreeNodeItem(dir));

            foreach (FTPLibrary.File file in directory.Files.Values)
            {
                TreeNodeItem fileNode = new TreeNodeItem(file.Path) {Tag = file, Header = file.FileName};
           
                directoryNode.Items.Add(fileNode);
            }
               
            return directoryNode;
        }

        private void TextBoxLocalDirectory_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
                return;

            try
            {
                string rootPath = System.IO.Path.GetPathRoot(TextBoxLocalDirectory.Text);
                foreach (TreeNodeItem treeNodeItem in LocalDirectoryTreeView.Items)
                {
                    if (treeNodeItem.Path != rootPath) continue;
                    TreeNodeItem item = FindTreeNode(treeNodeItem, TextBoxLocalDirectory.Text);
                    if (item == null) continue;
                    item.Focus();
                    break;
                }
            }
            catch
            {
            }

        }


        //tree finding algorithm, skips searching base directories, finds the correct directory  
        private TreeNodeItem FindTreeNode(TreeNodeItem node, string itemToFind)
        {
            if (node.Path == itemToFind)
            {
                return node;
            }
            node.IsExpanded = true;
            foreach (TreeNodeItem child in node.Items)
            {
                //get the root path
                string root = System.IO.Path.GetPathRoot(itemToFind);
                //extract the path without the root
                string pathWithOutRoot = itemToFind.Substring(root.Length);

                //check if the current child path contains the root and the first directory, if so carry on, if not continue
                if (!child.Path.Contains(root + pathWithOutRoot.Split(System.IO.Path.DirectorySeparatorChar).First()))
                    continue;

                //extract the last directory in the child path and its index
                string lastele = child.Path.Split(System.IO.Path.DirectorySeparatorChar).ToList().FindAll(s => s != root.Replace(@"\","")).ToList().Last();
                int indexOfLast = child.Path.Split(System.IO.Path.DirectorySeparatorChar).ToList().FindAll(s => s != root.Replace(@"\", "")).FindIndex(s => s == lastele);
   
                //compare both the index and directory name we're currently in to the same index and directory name to the one we're looking for
                var continueFlag = lastele != pathWithOutRoot.Split(System.IO.Path.DirectorySeparatorChar)[indexOfLast]; 

                //if they're not the same, skip
                if (continueFlag) continue;

                //if the current child has the same path we're lookin for, we are here focus it and return it
                if (child.Path == itemToFind)
                {
                    child.Focus();
                    return child;
                }

                //if it isn't the path and has no children, then skip 
                if (!child.HasItems) continue;

                //recursively search the directory, it is clearly a part of the correct path, we must find the child 
                TreeNodeItem found = FindTreeNode(child, itemToFind);

               
                if (found == null) continue;

                //when we have found it
                found.Focus();
                return found;
            }
            return null;
        }

        private void FileLocalTreeView()
        {
            foreach (TreeNodeItem item in System.IO.Directory.GetLogicalDrives().Select(s => new TreeNodeItem(s) {Header = s, Tag = s, FontWeight = FontWeights.Normal}))
            {
                item.Items.Add("+");
                item.Expanded += Expand;
                LocalDirectoryTreeView.Items.Add(item);
            }
        }

        private void Expand(object sender, RoutedEventArgs e)
        {
            TreeNodeItem item = (TreeNodeItem) sender;
            TextBoxLocalDirectory.Text = item.Path;
            FillListViewWithCorrectFiles(item);
            if (item.Items.Count > 0)
            {
                item.Items.Clear();
                try
                {
                    foreach (TreeNodeItem subitem in System.IO.Directory.GetDirectories(item.Tag.ToString()).Select(s => new TreeNodeItem(s) {Header = s.Substring(s.LastIndexOf("\\", StringComparison.Ordinal) + 1), Tag = s, FontWeight = FontWeights.Normal}))
                    {
                        //lazy loading tree
                        subitem.Items.Add("+");
                        subitem.Expanded -= Expand;
                        subitem.Expanded += Expand;
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception)
                {
                }
            }
            //always handeled, prevents repetition for root nodes
            e.Handled = true;
        }



        private void FillListViewWithCorrectFiles(TreeNodeItem item)
        {    
            try
            {
                LocalDirectoryInfoListView.Items.Clear();
                foreach (string file in System.IO.Directory.GetFiles(item.Path))
                {
                    NativeMethods.SHGetFileInfo(file, DwFileAttributes, ref _info, (uint)Marshal.SizeOf(_info), _uFlags);
                    FileInfo fileInfo = new FileInfo(file);
                    FTPLibrary.File f = new FTPLibrary.File(fileInfo.Name, _info.szTypeName, fileInfo.FullName, fileInfo.LastWriteTime, fileInfo.Length, "");
                    LocalDirectoryInfoListView.Items.Add(f);
                }
            }
            catch (Exception)
            {
                
            }
        } 

 

        private void PortGotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPort.Text == "Port...")
                txtPort.Text = "";
        }

        private void PortLostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPort.Text.Length == 0)
                txtPort.Text = "Port...";
        }

        private void IpLostFocus(object sender, RoutedEventArgs e)
        {
            if (txtIP.Text.Length == 0)
                txtIP.Text = "IP Address...";
        }

        private void IpGotFocus(object sender, RoutedEventArgs e)
        {
            if (txtIP.Text == "IP Address...")
                txtIP.Text = "";
        }


        private void UsernameGotFocus(object sender, RoutedEventArgs e)
        {

            if (txtUsername.Text == "Username...")
            {
                txtUsername.Text = "";
            }
        }

        private void UsernameLostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text.Length == 0)
            {
                txtUsername.Text = "Username...";
            }
        }

        private void PasswordLostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password.Length == 0)
            {
                txtPassword.Password = "Password...";
            }
        }

        private void PasswordGotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == "Password...")
            {
                txtPassword.Password = "";
            }
        }


        private void Connect(object sender, RoutedEventArgs e)
        {
            string btnContect = (string) ((System.Windows.Controls.Button) sender).Content;
            if (btnContect == "DISCONNECT")
            {
                this._clientHandler.ConnectionToServer.CloseConnection();
                return;
            }
            switch (_hostModel.CheckDetails())
            {
                case Host.DetailsMatched:
                    _hostModel.UserPassword = txtPassword.Password;
                    _clientHandler.Connect(_hostModel);
                    break;
                // _clientHandler.Register(_clientModel); break;
                case Host.DetailsUnmatched:
                    _log.Add("[" + DateTime.Now + "] " + "All details fail to meet the requirements." + Environment.NewLine);
                    break;
                case Host.UsernameUnmatched:
                    _log.Add("[" + DateTime.Now + "] " + "Username doesn't meet necessary requirements." + Environment.NewLine);
                    break;
                case Host.IpUnmatched:
                    _log.Add("[" + DateTime.Now + "] " + "IP Address doesn't meet necessary requirements." + Environment.NewLine);
                    break;
                case Host.PortUnmatched:
                    _log.Add("[" + DateTime.Now + "] " + "Port doesn't meet necessary requirements." + Environment.NewLine);
                    break;
                case Host.DirectoryDoesNotExist:
                    _log.Add("[" + DateTime.Now + "] " + "Specified download path does not exist." + Environment.NewLine);
                    break;
            }

        }

        public void LabelErrorMessageAnimator(string errorMsg, Label lbl)
        {
            lbl.Content = errorMsg;
            Storyboard sBoard = new Storyboard();
            TimeSpan timeLineDuration = TimeSpan.FromMilliseconds(500); //

            DoubleAnimation fadeIn = new DoubleAnimation()
            {From = 0.0, To = 1.0, Duration = new Duration(timeLineDuration)};

            DoubleAnimation fadeOut = new DoubleAnimation()
            {From = 1.0, To = 0.0, Duration = new Duration(timeLineDuration)};
            fadeOut.BeginTime = TimeSpan.FromSeconds(3);
            Storyboard.SetTargetName(fadeIn, lbl.Name);
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath("Opacity", 1));
            sBoard.Children.Add(fadeIn);
            sBoard.Begin(lbl);

            Storyboard.SetTargetName(fadeOut, lbl.Name);
            Storyboard.SetTargetProperty(fadeOut, new PropertyPath("Opacity", 0));
            sBoard.Children.Add(fadeOut);
            sBoard.Begin(lbl);

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            TreeNodeItem itemCurrentlySelected = LocalDirectoryTreeView.SelectedItem as TreeNodeItem;
            if (itemCurrentlySelected == null) return;

            try
            {
                //simple, open the path in explorer
                System.Diagnostics.Process.Start("explorer.exe", itemCurrentlySelected.Path);
                e.Handled = true;
            }
            catch (Exception)
            {
                
            }
        }

        private void LocalDirectoryTreeView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            TreeNodeItem treeNodeItem = FindNodeWithMousePosition(e.OriginalSource as DependencyObject);

            if (treeNodeItem == null) return;

            treeNodeItem.IsSelected = true;
            e.Handled = true;
        }


        static TreeNodeItem FindNodeWithMousePosition(DependencyObject source)
        {
            while (source != null && !(source is TreeNodeItem))
                source = VisualTreeHelper.GetParent(source);

            return (TreeNodeItem) source;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_clientHandler.ClientThread != null)
            {
                if (_clientHandler.ConnectionToServer?.TcpClient != null)
                {
                    this._clientHandler.ConnectionToServer.CloseConnection();
                }
            }
        }

        private void Download(object sender, RoutedEventArgs e)
        {
            if (_hostModel.CheckDetails() == Host.DetailsMatched)
            {
                TreeNodeItem itemCurrentlySelected = RemoteDirectoryTreeView.SelectedItem as TreeNodeItem;

                if (itemCurrentlySelected == null) return;

                object itemsTag = itemCurrentlySelected.Tag;

                List<FTPLibrary.File> fileList = new List<File>();
                FTPLibrary.Directory directory = null;
                if (itemsTag is FTPLibrary.File)
                {
                    FTPLibrary.File selectedFile = (FTPLibrary.File) itemCurrentlySelected.Tag;
                    fileList.Add(selectedFile);
                }
                else
                {
                     directory = (FTPLibrary.Directory) itemCurrentlySelected.Tag;
                    directory.Files.Values.ToList().ForEach(file => fileList.Add(file));
                }


                _clientHandler.Download(fileList , directory);
            }
            else
            {
                _log.Add("[" + DateTime.Now + "] " + "Cannot download until the specified directory has been created." + Environment.NewLine);
            }
        }

        private void RequestUpdate(object sender, RoutedEventArgs e)
        {
            if (_hostModel.CheckDetails() == Host.DetailsMatched)
            {
                _clientHandler.RequestRootNode();
            }
        }

        private void UploadDirectory(object sender, RoutedEventArgs e)
        {

            TreeNodeItem itemCurrentlySelected =  (TreeNodeItem) LocalDirectoryTreeView.SelectedItem;

            if (itemCurrentlySelected == null) return;

            try
            {
                string[] files = System.IO.Directory.GetFiles(itemCurrentlySelected.Path);


      //          if (files.Length <= 0) return;

                Client owner = new Client(this._hostModel.UserName);
                FTPLibrary.Directory directoryToBeUploaded = new Directory((string) itemCurrentlySelected.Header, itemCurrentlySelected.Path, new List<Client>() {owner}, owner);

                for (int index = 0; index < System.IO.Directory.GetFiles(itemCurrentlySelected.Path).Length; index++)
                {
                    string fileFullPath = System.IO.Directory.GetFiles(itemCurrentlySelected.Path)[index];

                    FileInfo fileInfo = new FileInfo(fileFullPath);


                    if (fileInfo.IsReadOnly)
                    {
                        _log.Add("[" + DateTime.Now + "] " + "Cannot upload file: " + fileInfo.Name + " because the file is read only." + Environment.NewLine);
                        continue;
                    }

                    NativeMethods.SHGetFileInfo(fileInfo.FullName, DwFileAttributes, ref _info, (uint) Marshal.SizeOf(_info),  _uFlags);
                    File file = new File(
                        fileInfo.Name,
                        _info.szTypeName,
                        fileInfo.FullName,
                        fileInfo.LastWriteTime,
                        fileInfo.Length,
                        "");

                    directoryToBeUploaded.AddFile(file);
                }

                _clientHandler.UploadDirectory(directoryToBeUploaded);



            }
            catch (Exception exception)
            {
                _log.Add("[" + DateTime.Now + "] " + "Cannot upload directory: " + itemCurrentlySelected.Header + "; you may not have enough access." + Environment.NewLine);
            }
        }




        private void RemoteDirectoryTreeView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeNodeItem treeNodeItem = FindNodeWithMousePosition(e.OriginalSource as DependencyObject);

            if (treeNodeItem == null) return;

            treeNodeItem.IsSelected = true;
            e.Handled = true;
        }

        private void RemoteDirectoryTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeNodeItem treeNodeItem = FindNodeWithMousePosition(e.OriginalSource as DependencyObject);
            TreeNodeItem root = (TreeNodeItem)RemoteDirectoryTreeView.ItemContainerGenerator.ContainerFromIndex(0);
            if (treeNodeItem == null) return;
            
            TextBoxRemoteDirectory.Text = @"ftp:\" + treeNodeItem.Path.Substring(treeNodeItem.Path.IndexOf((string)root.Header, StringComparison.Ordinal)); // find out the path of the current node
            var tag = treeNodeItem.Tag as Directory;
            if (tag == null) return;
            try
            {
                RemoteDirectoryInfoListView.Items.Clear();
                foreach (FTPLibrary.File file in tag.Files.Values)
                {
                    RemoteDirectoryInfoListView.Items.Add(file);
                }
            }
            catch (Exception)
            {

            }
        }


        private void Save(object sender, RoutedEventArgs e)
        {
           _hostModel.SaveDictionaryToFile();
        }

        private void UploadFile(object sender, RoutedEventArgs e)
        {
            List<FTPLibrary.File> listOfFiles = LocalDirectoryInfoListView.SelectedItems.Cast<File>().ToList();

            if (listOfFiles.Count <= 0) return;

            _clientHandler.UploadFiles(listOfFiles);

        }
    }


}




