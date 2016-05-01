namespace FTPServer.view
{
    partial class ServerControlPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerUpdateInformation = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnPushSettings = new System.Windows.Forms.Button();
            this.txtCapacity = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.lblChangePort = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.tpServerLog = new System.Windows.Forms.TabPage();
            this.txtActionLog = new System.Windows.Forms.TextBox();
            this.tpFileManagement = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tpUserManagement = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lsvAdmins = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvUsers = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLoggedIn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlServerAdmin = new System.Windows.Forms.Panel();
            this.lblServerAdmins = new System.Windows.Forms.Label();
            this.pnlUserManagementBackground = new System.Windows.Forms.Panel();
            this.lblUserManagementTitle = new System.Windows.Forms.Label();
            this.btnRemoveAdmin = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEditAdmin = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAddAdmin = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tpHome = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlInformationHolder = new System.Windows.Forms.Panel();
            this.lblBytesReceived = new System.Windows.Forms.Label();
            this.lblBytesSent = new System.Windows.Forms.Label();
            this.lblMemoryUsage = new System.Windows.Forms.Label();
            this.lblCPUUsage = new System.Windows.Forms.Label();
            this.lblUsers = new System.Windows.Forms.Label();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.lblUptime = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPortTitle = new System.Windows.Forms.Label();
            this.lblIPTitle = new System.Windows.Forms.Label();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.lblConcurrentTitle = new System.Windows.Forms.Label();
            this.lblCapacityTitle = new System.Windows.Forms.Label();
            this.lblBytesRecievedTitle = new System.Windows.Forms.Label();
            this.lblUptimeTitle = new System.Windows.Forms.Label();
            this.lblBytesSentTitle = new System.Windows.Forms.Label();
            this.lblCPUUsageTitle = new System.Windows.Forms.Label();
            this.lblMemUsageTitle = new System.Windows.Forms.Label();
            this.pnlInformationBackground = new System.Windows.Forms.Panel();
            this.lblInfoTitle = new System.Windows.Forms.Label();
            this.pnlManagementBackground = new System.Windows.Forms.Panel();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.txtServerActions = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbHolder = new System.Windows.Forms.TabControl();
            this.tpSettings.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.tpServerLog.SuspendLayout();
            this.tpFileManagement.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tpUserManagement.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlServerAdmin.SuspendLayout();
            this.pnlUserManagementBackground.SuspendLayout();
            this.tpHome.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlInformationHolder.SuspendLayout();
            this.pnlInformationBackground.SuspendLayout();
            this.pnlManagementBackground.SuspendLayout();
            this.tbHolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerUpdateInformation
            // 
            this.timerUpdateInformation.Enabled = true;
            this.timerUpdateInformation.Interval = 1000;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.WorkerReportsProgress = true;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.panel9);
            this.tpSettings.Location = new System.Drawing.Point(4, 22);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(812, 376);
            this.tpSettings.TabIndex = 3;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnPushSettings);
            this.panel9.Controls.Add(this.txtCapacity);
            this.panel9.Controls.Add(this.txtPort);
            this.panel9.Controls.Add(this.label28);
            this.panel9.Controls.Add(this.lblChangePort);
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Controls.Add(this.btnSaveSettings);
            this.panel9.Controls.Add(this.panel11);
            this.panel9.Location = new System.Drawing.Point(6, 6);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(801, 366);
            this.panel9.TabIndex = 12;
            // 
            // btnPushSettings
            // 
            this.btnPushSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPushSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPushSettings.Location = new System.Drawing.Point(543, 34);
            this.btnPushSettings.Name = "btnPushSettings";
            this.btnPushSettings.Size = new System.Drawing.Size(253, 42);
            this.btnPushSettings.TabIndex = 10;
            this.btnPushSettings.Text = "Push Settings";
            this.btnPushSettings.UseVisualStyleBackColor = true;
            // 
            // txtCapacity
            // 
            this.txtCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCapacity.Location = new System.Drawing.Point(81, 72);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Size = new System.Drawing.Size(271, 24);
            this.txtCapacity.TabIndex = 6;
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(81, 39);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(271, 24);
            this.txtPort.TabIndex = 6;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(6, 75);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(69, 18);
            this.label28.TabIndex = 7;
            this.label28.Text = "Capacity:";
            // 
            // lblChangePort
            // 
            this.lblChangePort.AutoSize = true;
            this.lblChangePort.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangePort.Location = new System.Drawing.Point(6, 42);
            this.lblChangePort.Name = "lblChangePort";
            this.lblChangePort.Size = new System.Drawing.Size(40, 18);
            this.lblChangePort.TabIndex = 7;
            this.lblChangePort.Text = "Port:";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel10.Controls.Add(this.label2);
            this.panel10.Location = new System.Drawing.Point(3, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(349, 25);
            this.panel10.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Server Settings";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSettings.Location = new System.Drawing.Point(377, 34);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(160, 42);
            this.btnSaveSettings.TabIndex = 10;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            // 
            // panel11
            // 
            this.panel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel11.Controls.Add(this.label4);
            this.panel11.Location = new System.Drawing.Point(377, 3);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(419, 25);
            this.panel11.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Control Panel";
            // 
            // tpServerLog
            // 
            this.tpServerLog.Controls.Add(this.txtActionLog);
            this.tpServerLog.Location = new System.Drawing.Point(4, 22);
            this.tpServerLog.Name = "tpServerLog";
            this.tpServerLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpServerLog.Size = new System.Drawing.Size(812, 376);
            this.tpServerLog.TabIndex = 1;
            this.tpServerLog.Text = "Activity Log";
            this.tpServerLog.UseVisualStyleBackColor = true;
            // 
            // txtActionLog
            // 
            this.txtActionLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtActionLog.Location = new System.Drawing.Point(6, 6);
            this.txtActionLog.Multiline = true;
            this.txtActionLog.Name = "txtActionLog";
            this.txtActionLog.ReadOnly = true;
            this.txtActionLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtActionLog.Size = new System.Drawing.Size(800, 365);
            this.txtActionLog.TabIndex = 3;
            // 
            // tpFileManagement
            // 
            this.tpFileManagement.Controls.Add(this.panel7);
            this.tpFileManagement.Location = new System.Drawing.Point(4, 22);
            this.tpFileManagement.Name = "tpFileManagement";
            this.tpFileManagement.Size = new System.Drawing.Size(812, 376);
            this.tpFileManagement.TabIndex = 4;
            this.tpFileManagement.Text = "File Manager";
            this.tpFileManagement.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.panel6);
            this.panel7.Controls.Add(this.treeView1);
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(806, 373);
            this.panel7.TabIndex = 11;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel6.Controls.Add(this.label25);
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(800, 25);
            this.panel6.TabIndex = 9;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(2, 3);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(114, 20);
            this.label25.TabIndex = 0;
            this.label25.Text = "File Repository";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(3, 33);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(800, 335);
            this.treeView1.TabIndex = 0;
            // 
            // tpUserManagement
            // 
            this.tpUserManagement.Controls.Add(this.panel2);
            this.tpUserManagement.Location = new System.Drawing.Point(4, 22);
            this.tpUserManagement.Name = "tpUserManagement";
            this.tpUserManagement.Size = new System.Drawing.Size(812, 376);
            this.tpUserManagement.TabIndex = 2;
            this.tpUserManagement.Text = "User Manager";
            this.tpUserManagement.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lsvAdmins);
            this.panel2.Controls.Add(this.lsvUsers);
            this.panel2.Controls.Add(this.pnlServerAdmin);
            this.panel2.Controls.Add(this.pnlUserManagementBackground);
            this.panel2.Controls.Add(this.btnRemoveAdmin);
            this.panel2.Controls.Add(this.btnRemove);
            this.panel2.Controls.Add(this.btnEditAdmin);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAddAdmin);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(803, 370);
            this.panel2.TabIndex = 1;
            // 
            // lsvAdmins
            // 
            this.lsvAdmins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvAdmins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lsvAdmins.Location = new System.Drawing.Point(377, 69);
            this.lsvAdmins.Name = "lsvAdmins";
            this.lsvAdmins.Size = new System.Drawing.Size(423, 296);
            this.lsvAdmins.TabIndex = 9;
            this.lsvAdmins.UseCompatibleStateImageBehavior = false;
            this.lsvAdmins.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Username";
            this.columnHeader1.Width = 155;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Status";
            this.columnHeader2.Width = 122;
            // 
            // lsvUsers
            // 
            this.lsvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lsvUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colLoggedIn});
            this.lsvUsers.Location = new System.Drawing.Point(7, 71);
            this.lsvUsers.Name = "lsvUsers";
            this.lsvUsers.Size = new System.Drawing.Size(351, 294);
            this.lsvUsers.TabIndex = 8;
            this.lsvUsers.UseCompatibleStateImageBehavior = false;
            this.lsvUsers.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Username";
            this.colName.Width = 197;
            // 
            // colLoggedIn
            // 
            this.colLoggedIn.Text = "Status";
            this.colLoggedIn.Width = 122;
            // 
            // pnlServerAdmin
            // 
            this.pnlServerAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlServerAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlServerAdmin.Controls.Add(this.lblServerAdmins);
            this.pnlServerAdmin.Location = new System.Drawing.Point(377, 3);
            this.pnlServerAdmin.Name = "pnlServerAdmin";
            this.pnlServerAdmin.Size = new System.Drawing.Size(423, 25);
            this.pnlServerAdmin.TabIndex = 6;
            // 
            // lblServerAdmins
            // 
            this.lblServerAdmins.AutoSize = true;
            this.lblServerAdmins.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerAdmins.Location = new System.Drawing.Point(2, 3);
            this.lblServerAdmins.Name = "lblServerAdmins";
            this.lblServerAdmins.Size = new System.Drawing.Size(161, 20);
            this.lblServerAdmins.TabIndex = 0;
            this.lblServerAdmins.Text = "Server Administrators";
            // 
            // pnlUserManagementBackground
            // 
            this.pnlUserManagementBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlUserManagementBackground.Controls.Add(this.lblUserManagementTitle);
            this.pnlUserManagementBackground.Location = new System.Drawing.Point(7, 3);
            this.pnlUserManagementBackground.Name = "pnlUserManagementBackground";
            this.pnlUserManagementBackground.Size = new System.Drawing.Size(352, 25);
            this.pnlUserManagementBackground.TabIndex = 5;
            // 
            // lblUserManagementTitle
            // 
            this.lblUserManagementTitle.AutoSize = true;
            this.lblUserManagementTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserManagementTitle.Location = new System.Drawing.Point(2, 3);
            this.lblUserManagementTitle.Name = "lblUserManagementTitle";
            this.lblUserManagementTitle.Size = new System.Drawing.Size(70, 20);
            this.lblUserManagementTitle.TabIndex = 0;
            this.lblUserManagementTitle.Text = "Clientele";
            // 
            // btnRemoveAdmin
            // 
            this.btnRemoveAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnRemoveAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAdmin.Location = new System.Drawing.Point(645, 34);
            this.btnRemoveAdmin.Name = "btnRemoveAdmin";
            this.btnRemoveAdmin.Size = new System.Drawing.Size(155, 30);
            this.btnRemoveAdmin.TabIndex = 1;
            this.btnRemoveAdmin.Text = "Remove";
            this.btnRemoveAdmin.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(249, 33);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(110, 30);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = false;
            // 
            // btnEditAdmin
            // 
            this.btnEditAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(158)))), ((int)(((byte)(101)))));
            this.btnEditAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditAdmin.Location = new System.Drawing.Point(510, 34);
            this.btnEditAdmin.Name = "btnEditAdmin";
            this.btnEditAdmin.Size = new System.Drawing.Size(129, 30);
            this.btnEditAdmin.TabIndex = 1;
            this.btnEditAdmin.Text = "Edit";
            this.btnEditAdmin.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(158)))), ((int)(((byte)(101)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(124, 33);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(119, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnAddAdmin
            // 
            this.btnAddAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(102)))));
            this.btnAddAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAdmin.Location = new System.Drawing.Point(377, 34);
            this.btnAddAdmin.Name = "btnAddAdmin";
            this.btnAddAdmin.Size = new System.Drawing.Size(127, 30);
            this.btnAddAdmin.TabIndex = 1;
            this.btnAddAdmin.Text = "Add";
            this.btnAddAdmin.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(102)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(8, 33);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 30);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // tpHome
            // 
            this.tpHome.Controls.Add(this.panel1);
            this.tpHome.Location = new System.Drawing.Point(4, 22);
            this.tpHome.Name = "tpHome";
            this.tpHome.Padding = new System.Windows.Forms.Padding(3);
            this.tpHome.Size = new System.Drawing.Size(812, 376);
            this.tpHome.TabIndex = 0;
            this.tpHome.Text = "Home";
            this.tpHome.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pnlInformationHolder);
            this.panel1.Controls.Add(this.pnlInformationBackground);
            this.panel1.Controls.Add(this.pnlManagementBackground);
            this.panel1.Controls.Add(this.txtServerActions);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnRestart);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 370);
            this.panel1.TabIndex = 0;
            // 
            // pnlInformationHolder
            // 
            this.pnlInformationHolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInformationHolder.Controls.Add(this.lblBytesReceived);
            this.pnlInformationHolder.Controls.Add(this.lblBytesSent);
            this.pnlInformationHolder.Controls.Add(this.lblMemoryUsage);
            this.pnlInformationHolder.Controls.Add(this.lblCPUUsage);
            this.pnlInformationHolder.Controls.Add(this.lblUsers);
            this.pnlInformationHolder.Controls.Add(this.lblCapacity);
            this.pnlInformationHolder.Controls.Add(this.lblUptime);
            this.pnlInformationHolder.Controls.Add(this.lblPort);
            this.pnlInformationHolder.Controls.Add(this.lblIP);
            this.pnlInformationHolder.Controls.Add(this.lblStatus);
            this.pnlInformationHolder.Controls.Add(this.lblPortTitle);
            this.pnlInformationHolder.Controls.Add(this.lblIPTitle);
            this.pnlInformationHolder.Controls.Add(this.lblStatusTitle);
            this.pnlInformationHolder.Controls.Add(this.lblConcurrentTitle);
            this.pnlInformationHolder.Controls.Add(this.lblCapacityTitle);
            this.pnlInformationHolder.Controls.Add(this.lblBytesRecievedTitle);
            this.pnlInformationHolder.Controls.Add(this.lblUptimeTitle);
            this.pnlInformationHolder.Controls.Add(this.lblBytesSentTitle);
            this.pnlInformationHolder.Controls.Add(this.lblCPUUsageTitle);
            this.pnlInformationHolder.Controls.Add(this.lblMemUsageTitle);
            this.pnlInformationHolder.Location = new System.Drawing.Point(377, 34);
            this.pnlInformationHolder.Name = "pnlInformationHolder";
            this.pnlInformationHolder.Size = new System.Drawing.Size(423, 333);
            this.pnlInformationHolder.TabIndex = 7;
            // 
            // lblBytesReceived
            // 
            this.lblBytesReceived.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBytesReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBytesReceived.Location = new System.Drawing.Point(261, 259);
            this.lblBytesReceived.Name = "lblBytesReceived";
            this.lblBytesReceived.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblBytesReceived.Size = new System.Drawing.Size(158, 17);
            this.lblBytesReceived.TabIndex = 5;
            this.lblBytesReceived.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBytesSent
            // 
            this.lblBytesSent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBytesSent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBytesSent.Location = new System.Drawing.Point(261, 231);
            this.lblBytesSent.Name = "lblBytesSent";
            this.lblBytesSent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblBytesSent.Size = new System.Drawing.Size(158, 17);
            this.lblBytesSent.TabIndex = 5;
            this.lblBytesSent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMemoryUsage
            // 
            this.lblMemoryUsage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMemoryUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemoryUsage.Location = new System.Drawing.Point(261, 203);
            this.lblMemoryUsage.Name = "lblMemoryUsage";
            this.lblMemoryUsage.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblMemoryUsage.Size = new System.Drawing.Size(158, 17);
            this.lblMemoryUsage.TabIndex = 5;
            this.lblMemoryUsage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPUUsage
            // 
            this.lblCPUUsage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCPUUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPUUsage.Location = new System.Drawing.Point(261, 176);
            this.lblCPUUsage.Name = "lblCPUUsage";
            this.lblCPUUsage.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCPUUsage.Size = new System.Drawing.Size(158, 17);
            this.lblCPUUsage.TabIndex = 5;
            this.lblCPUUsage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUsers
            // 
            this.lblUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsers.Location = new System.Drawing.Point(261, 148);
            this.lblUsers.Name = "lblUsers";
            this.lblUsers.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblUsers.Size = new System.Drawing.Size(158, 17);
            this.lblUsers.TabIndex = 5;
            this.lblUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCapacity
            // 
            this.lblCapacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCapacity.Location = new System.Drawing.Point(261, 119);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCapacity.Size = new System.Drawing.Size(158, 17);
            this.lblCapacity.TabIndex = 5;
            this.lblCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUptime
            // 
            this.lblUptime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUptime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUptime.Location = new System.Drawing.Point(261, 91);
            this.lblUptime.Name = "lblUptime";
            this.lblUptime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblUptime.Size = new System.Drawing.Size(158, 17);
            this.lblUptime.TabIndex = 5;
            this.lblUptime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPort
            // 
            this.lblPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.Location = new System.Drawing.Point(261, 62);
            this.lblPort.Name = "lblPort";
            this.lblPort.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPort.Size = new System.Drawing.Size(158, 17);
            this.lblPort.TabIndex = 5;
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIP
            // 
            this.lblIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.Location = new System.Drawing.Point(210, 35);
            this.lblIP.Name = "lblIP";
            this.lblIP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblIP.Size = new System.Drawing.Size(209, 16);
            this.lblIP.TabIndex = 5;
            this.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(261, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStatus.Size = new System.Drawing.Size(158, 16);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPortTitle
            // 
            this.lblPortTitle.AutoSize = true;
            this.lblPortTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortTitle.Location = new System.Drawing.Point(3, 63);
            this.lblPortTitle.Name = "lblPortTitle";
            this.lblPortTitle.Size = new System.Drawing.Size(126, 16);
            this.lblPortTitle.TabIndex = 4;
            this.lblPortTitle.Text = "Server Port Number";
            // 
            // lblIPTitle
            // 
            this.lblIPTitle.AutoSize = true;
            this.lblIPTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIPTitle.Location = new System.Drawing.Point(3, 35);
            this.lblIPTitle.Name = "lblIPTitle";
            this.lblIPTitle.Size = new System.Drawing.Size(117, 16);
            this.lblIPTitle.TabIndex = 4;
            this.lblIPTitle.Text = "Server IP Address";
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.AutoSize = true;
            this.lblStatusTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusTitle.Location = new System.Drawing.Point(3, 9);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(117, 16);
            this.lblStatusTitle.TabIndex = 4;
            this.lblStatusTitle.Text = "FTP Server Status";
            // 
            // lblConcurrentTitle
            // 
            this.lblConcurrentTitle.AutoSize = true;
            this.lblConcurrentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConcurrentTitle.Location = new System.Drawing.Point(3, 149);
            this.lblConcurrentTitle.Name = "lblConcurrentTitle";
            this.lblConcurrentTitle.Size = new System.Drawing.Size(111, 16);
            this.lblConcurrentTitle.TabIndex = 4;
            this.lblConcurrentTitle.Text = "Concurrent Users";
            // 
            // lblCapacityTitle
            // 
            this.lblCapacityTitle.AutoSize = true;
            this.lblCapacityTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCapacityTitle.Location = new System.Drawing.Point(3, 120);
            this.lblCapacityTitle.Name = "lblCapacityTitle";
            this.lblCapacityTitle.Size = new System.Drawing.Size(104, 16);
            this.lblCapacityTitle.TabIndex = 4;
            this.lblCapacityTitle.Text = "Server Capacity";
            // 
            // lblBytesRecievedTitle
            // 
            this.lblBytesRecievedTitle.AutoSize = true;
            this.lblBytesRecievedTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBytesRecievedTitle.Location = new System.Drawing.Point(3, 260);
            this.lblBytesRecievedTitle.Name = "lblBytesRecievedTitle";
            this.lblBytesRecievedTitle.Size = new System.Drawing.Size(104, 16);
            this.lblBytesRecievedTitle.TabIndex = 4;
            this.lblBytesRecievedTitle.Text = "Bytes Received";
            // 
            // lblUptimeTitle
            // 
            this.lblUptimeTitle.AutoSize = true;
            this.lblUptimeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUptimeTitle.Location = new System.Drawing.Point(3, 92);
            this.lblUptimeTitle.Name = "lblUptimeTitle";
            this.lblUptimeTitle.Size = new System.Drawing.Size(94, 16);
            this.lblUptimeTitle.TabIndex = 4;
            this.lblUptimeTitle.Text = "Server Uptime";
            // 
            // lblBytesSentTitle
            // 
            this.lblBytesSentTitle.AutoSize = true;
            this.lblBytesSentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBytesSentTitle.Location = new System.Drawing.Point(3, 232);
            this.lblBytesSentTitle.Name = "lblBytesSentTitle";
            this.lblBytesSentTitle.Size = new System.Drawing.Size(72, 16);
            this.lblBytesSentTitle.TabIndex = 4;
            this.lblBytesSentTitle.Text = "Bytes Sent";
            // 
            // lblCPUUsageTitle
            // 
            this.lblCPUUsageTitle.AutoSize = true;
            this.lblCPUUsageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPUUsageTitle.Location = new System.Drawing.Point(3, 177);
            this.lblCPUUsageTitle.Name = "lblCPUUsageTitle";
            this.lblCPUUsageTitle.Size = new System.Drawing.Size(80, 16);
            this.lblCPUUsageTitle.TabIndex = 4;
            this.lblCPUUsageTitle.Text = "CPU Usage";
            // 
            // lblMemUsageTitle
            // 
            this.lblMemUsageTitle.AutoSize = true;
            this.lblMemUsageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemUsageTitle.Location = new System.Drawing.Point(3, 204);
            this.lblMemUsageTitle.Name = "lblMemUsageTitle";
            this.lblMemUsageTitle.Size = new System.Drawing.Size(101, 16);
            this.lblMemUsageTitle.TabIndex = 4;
            this.lblMemUsageTitle.Text = "Memory Usage";
            // 
            // pnlInformationBackground
            // 
            this.pnlInformationBackground.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInformationBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlInformationBackground.Controls.Add(this.lblInfoTitle);
            this.pnlInformationBackground.Location = new System.Drawing.Point(377, 3);
            this.pnlInformationBackground.Name = "pnlInformationBackground";
            this.pnlInformationBackground.Size = new System.Drawing.Size(423, 25);
            this.pnlInformationBackground.TabIndex = 6;
            // 
            // lblInfoTitle
            // 
            this.lblInfoTitle.AutoSize = true;
            this.lblInfoTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblInfoTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoTitle.Location = new System.Drawing.Point(2, 3);
            this.lblInfoTitle.Name = "lblInfoTitle";
            this.lblInfoTitle.Size = new System.Drawing.Size(140, 20);
            this.lblInfoTitle.TabIndex = 0;
            this.lblInfoTitle.Text = "Server Information";
            // 
            // pnlManagementBackground
            // 
            this.pnlManagementBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlManagementBackground.Controls.Add(this.lblServerStatus);
            this.pnlManagementBackground.Location = new System.Drawing.Point(7, 3);
            this.pnlManagementBackground.Name = "pnlManagementBackground";
            this.pnlManagementBackground.Size = new System.Drawing.Size(352, 25);
            this.pnlManagementBackground.TabIndex = 5;
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.AutoSize = true;
            this.lblServerStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerStatus.Location = new System.Drawing.Point(2, 3);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(153, 20);
            this.lblServerStatus.TabIndex = 0;
            this.lblServerStatus.Text = "Server Management";
            // 
            // txtServerActions
            // 
            this.txtServerActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtServerActions.Location = new System.Drawing.Point(8, 70);
            this.txtServerActions.Multiline = true;
            this.txtServerActions.Name = "txtServerActions";
            this.txtServerActions.ReadOnly = true;
            this.txtServerActions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServerActions.Size = new System.Drawing.Size(351, 297);
            this.txtServerActions.TabIndex = 2;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(249, 34);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(110, 30);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop Server";
            this.btnStop.UseVisualStyleBackColor = false;
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(158)))), ((int)(((byte)(101)))));
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestart.Location = new System.Drawing.Point(124, 34);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(119, 30);
            this.btnRestart.TabIndex = 1;
            this.btnRestart.Text = "Restart Server";
            this.btnRestart.UseVisualStyleBackColor = false;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(102)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(8, 34);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(110, 30);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start Server";
            this.btnStart.UseVisualStyleBackColor = false;
            // 
            // tbHolder
            // 
            this.tbHolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHolder.Controls.Add(this.tpHome);
            this.tbHolder.Controls.Add(this.tpUserManagement);
            this.tbHolder.Controls.Add(this.tpFileManagement);
            this.tbHolder.Controls.Add(this.tpServerLog);
            this.tbHolder.Controls.Add(this.tpSettings);
            this.tbHolder.Location = new System.Drawing.Point(12, 12);
            this.tbHolder.Name = "tbHolder";
            this.tbHolder.SelectedIndex = 0;
            this.tbHolder.Size = new System.Drawing.Size(820, 402);
            this.tbHolder.TabIndex = 0;
            // 
            // ServerControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 426);
            this.Controls.Add(this.tbHolder);
            this.Name = "ServerControlPanel";
            this.Text = "Control Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmServerControlPanel_FormClosing);
            this.tpSettings.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.tpServerLog.ResumeLayout(false);
            this.tpServerLog.PerformLayout();
            this.tpFileManagement.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tpUserManagement.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlServerAdmin.ResumeLayout(false);
            this.pnlServerAdmin.PerformLayout();
            this.pnlUserManagementBackground.ResumeLayout(false);
            this.pnlUserManagementBackground.PerformLayout();
            this.tpHome.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlInformationHolder.ResumeLayout(false);
            this.pnlInformationHolder.PerformLayout();
            this.pnlInformationBackground.ResumeLayout(false);
            this.pnlInformationBackground.PerformLayout();
            this.pnlManagementBackground.ResumeLayout(false);
            this.pnlManagementBackground.PerformLayout();
            this.tbHolder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerUpdateInformation;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.TabPage tpServerLog;
        private System.Windows.Forms.TabPage tpFileManagement;
        private System.Windows.Forms.TabPage tpUserManagement;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lsvUsers;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colLoggedIn;
        private System.Windows.Forms.Panel pnlServerAdmin;
        private System.Windows.Forms.Label lblServerAdmins;
        private System.Windows.Forms.Panel pnlUserManagementBackground;
        private System.Windows.Forms.Label lblUserManagementTitle;
        private System.Windows.Forms.Button btnRemoveAdmin;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnEditAdmin;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAddAdmin;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TabPage tpHome;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlInformationHolder;
        private System.Windows.Forms.Label lblBytesReceived;
        private System.Windows.Forms.Label lblBytesSent;
        private System.Windows.Forms.Label lblMemoryUsage;
        private System.Windows.Forms.Label lblCPUUsage;
        private System.Windows.Forms.Label lblUsers;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.Label lblUptime;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPortTitle;
        private System.Windows.Forms.Label lblIPTitle;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.Label lblConcurrentTitle;
        private System.Windows.Forms.Label lblCapacityTitle;
        private System.Windows.Forms.Label lblBytesRecievedTitle;
        private System.Windows.Forms.Label lblUptimeTitle;
        private System.Windows.Forms.Label lblBytesSentTitle;
        private System.Windows.Forms.Label lblCPUUsageTitle;
        private System.Windows.Forms.Label lblMemUsageTitle;
        private System.Windows.Forms.Panel pnlInformationBackground;
        private System.Windows.Forms.Label lblInfoTitle;
        private System.Windows.Forms.Panel pnlManagementBackground;
        private System.Windows.Forms.Label lblServerStatus;
        private System.Windows.Forms.TextBox txtServerActions;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TabControl tbHolder;
        private System.Windows.Forms.TextBox txtActionLog;
        private System.Windows.Forms.ListView lsvAdmins;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label lblChangePort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnPushSettings;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCapacity;
        private System.Windows.Forms.Label label28;
    }
}