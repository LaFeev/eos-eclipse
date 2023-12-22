using System;

namespace WinFormsExample
{
    partial class MainForm
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
            this.LiveViewGroupBox = new System.Windows.Forms.GroupBox();
            this.FocusFar3Button = new System.Windows.Forms.Button();
            this.FocusFar2Button = new System.Windows.Forms.Button();
            this.FocusFar1Button = new System.Windows.Forms.Button();
            this.FocusNear1Button = new System.Windows.Forms.Button();
            this.FocusNear2Button = new System.Windows.Forms.Button();
            this.FocusNear3Button = new System.Windows.Forms.Button();
            this.LiveViewPicBox = new System.Windows.Forms.PictureBox();
            this.LiveViewButton = new System.Windows.Forms.Button();
            this.InitGroupBox = new System.Windows.Forms.GroupBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.CameraListBox = new System.Windows.Forms.ListBox();
            this.SessionLabel = new System.Windows.Forms.Label();
            this.SessionButton = new System.Windows.Forms.Button();
            this.SaveFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.SettingsTabControl = new System.Windows.Forms.TabControl();
            this.SettingsTabPage = new System.Windows.Forms.TabPage();
            this.AvCoBox = new System.Windows.Forms.ComboBox();
            this.MainProgressBar = new System.Windows.Forms.ProgressBar();
            this.TvCoBox = new System.Windows.Forms.ComboBox();
            this.SavePathTextBox = new System.Windows.Forms.TextBox();
            this.ISOCoBox = new System.Windows.Forms.ComboBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.BulbUpDo = new System.Windows.Forms.NumericUpDown();
            this.SaveToGroupBox = new System.Windows.Forms.GroupBox();
            this.STBothRdButton = new System.Windows.Forms.RadioButton();
            this.STComputerRdButton = new System.Windows.Forms.RadioButton();
            this.STCameraRdButton = new System.Windows.Forms.RadioButton();
            this.TakePhotoButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.RecordVideoButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LocTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LonLabel = new System.Windows.Forms.Label();
            this.LatLabel = new System.Windows.Forms.Label();
            this.GPSDateTimeTextBox = new System.Windows.Forms.Label();
            this.GPSStatusTextBox = new System.Windows.Forms.Label();
            this.SetLocButton = new System.Windows.Forms.Button();
            this.GetGPSButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.AltTextBox = new System.Windows.Forms.TextBox();
            this.LonTextBox = new System.Windows.Forms.TextBox();
            this.LatTextBox = new System.Windows.Forms.TextBox();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.SeqGenTabPage = new System.Windows.Forms.TabPage();
            this.TakeNPhotoButton = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ExpandButton = new System.Windows.Forms.Button();
            this.SequenceGroupBox = new System.Windows.Forms.GroupBox();
            this.SeqFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.EclipseTabPage = new System.Windows.Forms.TabPage();
            this.CaptureTabPage = new System.Windows.Forms.TabPage();
            this.LiveViewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LiveViewPicBox)).BeginInit();
            this.InitGroupBox.SuspendLayout();
            this.SettingsTabControl.SuspendLayout();
            this.SettingsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BulbUpDo)).BeginInit();
            this.SaveToGroupBox.SuspendLayout();
            this.LocTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SequenceGroupBox.SuspendLayout();
            this.CaptureTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // LiveViewGroupBox
            // 
            this.LiveViewGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LiveViewGroupBox.Controls.Add(this.FocusFar3Button);
            this.LiveViewGroupBox.Controls.Add(this.FocusFar2Button);
            this.LiveViewGroupBox.Controls.Add(this.FocusFar1Button);
            this.LiveViewGroupBox.Controls.Add(this.FocusNear1Button);
            this.LiveViewGroupBox.Controls.Add(this.FocusNear2Button);
            this.LiveViewGroupBox.Controls.Add(this.FocusNear3Button);
            this.LiveViewGroupBox.Controls.Add(this.LiveViewPicBox);
            this.LiveViewGroupBox.Controls.Add(this.LiveViewButton);
            this.LiveViewGroupBox.Enabled = false;
            this.LiveViewGroupBox.Location = new System.Drawing.Point(12, 238);
            this.LiveViewGroupBox.Name = "LiveViewGroupBox";
            this.LiveViewGroupBox.Size = new System.Drawing.Size(547, 355);
            this.LiveViewGroupBox.TabIndex = 13;
            this.LiveViewGroupBox.TabStop = false;
            this.LiveViewGroupBox.Text = "LiveView";
            // 
            // FocusFar3Button
            // 
            this.FocusFar3Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FocusFar3Button.Location = new System.Drawing.Point(345, 20);
            this.FocusFar3Button.Name = "FocusFar3Button";
            this.FocusFar3Button.Size = new System.Drawing.Size(28, 23);
            this.FocusFar3Button.TabIndex = 6;
            this.FocusFar3Button.Text = ">>>";
            this.FocusFar3Button.UseVisualStyleBackColor = true;
            this.FocusFar3Button.Click += new System.EventHandler(this.FocusFar3Button_Click);
            // 
            // FocusFar2Button
            // 
            this.FocusFar2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FocusFar2Button.Location = new System.Drawing.Point(311, 20);
            this.FocusFar2Button.Name = "FocusFar2Button";
            this.FocusFar2Button.Size = new System.Drawing.Size(28, 23);
            this.FocusFar2Button.TabIndex = 6;
            this.FocusFar2Button.Text = ">>";
            this.FocusFar2Button.UseVisualStyleBackColor = true;
            this.FocusFar2Button.Click += new System.EventHandler(this.FocusFar2Button_Click);
            // 
            // FocusFar1Button
            // 
            this.FocusFar1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FocusFar1Button.Location = new System.Drawing.Point(277, 20);
            this.FocusFar1Button.Name = "FocusFar1Button";
            this.FocusFar1Button.Size = new System.Drawing.Size(28, 23);
            this.FocusFar1Button.TabIndex = 6;
            this.FocusFar1Button.Text = ">";
            this.FocusFar1Button.UseVisualStyleBackColor = true;
            this.FocusFar1Button.Click += new System.EventHandler(this.FocusFar1Button_Click);
            // 
            // FocusNear1Button
            // 
            this.FocusNear1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FocusNear1Button.Location = new System.Drawing.Point(243, 20);
            this.FocusNear1Button.Name = "FocusNear1Button";
            this.FocusNear1Button.Size = new System.Drawing.Size(28, 23);
            this.FocusNear1Button.TabIndex = 6;
            this.FocusNear1Button.Text = "<";
            this.FocusNear1Button.UseVisualStyleBackColor = true;
            this.FocusNear1Button.Click += new System.EventHandler(this.FocusNear1Button_Click);
            // 
            // FocusNear2Button
            // 
            this.FocusNear2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FocusNear2Button.Location = new System.Drawing.Point(209, 20);
            this.FocusNear2Button.Name = "FocusNear2Button";
            this.FocusNear2Button.Size = new System.Drawing.Size(28, 23);
            this.FocusNear2Button.TabIndex = 6;
            this.FocusNear2Button.Text = "<<";
            this.FocusNear2Button.UseVisualStyleBackColor = true;
            this.FocusNear2Button.Click += new System.EventHandler(this.FocusNear2Button_Click);
            // 
            // FocusNear3Button
            // 
            this.FocusNear3Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FocusNear3Button.Location = new System.Drawing.Point(175, 20);
            this.FocusNear3Button.Name = "FocusNear3Button";
            this.FocusNear3Button.Size = new System.Drawing.Size(28, 23);
            this.FocusNear3Button.TabIndex = 6;
            this.FocusNear3Button.Text = "<<<";
            this.FocusNear3Button.UseVisualStyleBackColor = true;
            this.FocusNear3Button.Click += new System.EventHandler(this.FocusNear3Button_Click);
            // 
            // LiveViewPicBox
            // 
            this.LiveViewPicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LiveViewPicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LiveViewPicBox.Location = new System.Drawing.Point(9, 51);
            this.LiveViewPicBox.Name = "LiveViewPicBox";
            this.LiveViewPicBox.Size = new System.Drawing.Size(528, 293);
            this.LiveViewPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LiveViewPicBox.TabIndex = 1;
            this.LiveViewPicBox.TabStop = false;
            this.LiveViewPicBox.SizeChanged += new System.EventHandler(this.LiveViewPicBox_SizeChanged);
            // 
            // LiveViewButton
            // 
            this.LiveViewButton.Location = new System.Drawing.Point(8, 20);
            this.LiveViewButton.Name = "LiveViewButton";
            this.LiveViewButton.Size = new System.Drawing.Size(70, 22);
            this.LiveViewButton.TabIndex = 2;
            this.LiveViewButton.Text = "Start LV";
            this.LiveViewButton.UseVisualStyleBackColor = true;
            this.LiveViewButton.Click += new System.EventHandler(this.LiveViewButton_Click);
            // 
            // InitGroupBox
            // 
            this.InitGroupBox.Controls.Add(this.RefreshButton);
            this.InitGroupBox.Controls.Add(this.CameraListBox);
            this.InitGroupBox.Controls.Add(this.SessionLabel);
            this.InitGroupBox.Controls.Add(this.SessionButton);
            this.InitGroupBox.Location = new System.Drawing.Point(12, 13);
            this.InitGroupBox.Name = "InitGroupBox";
            this.InitGroupBox.Size = new System.Drawing.Size(135, 158);
            this.InitGroupBox.TabIndex = 12;
            this.InitGroupBox.TabStop = false;
            this.InitGroupBox.Text = "Connect Camera";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.Location = new System.Drawing.Point(96, 127);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(31, 23);
            this.RefreshButton.TabIndex = 9;
            this.RefreshButton.Text = "↻";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // CameraListBox
            // 
            this.CameraListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CameraListBox.FormattingEnabled = true;
            this.CameraListBox.Location = new System.Drawing.Point(8, 35);
            this.CameraListBox.Name = "CameraListBox";
            this.CameraListBox.Size = new System.Drawing.Size(121, 82);
            this.CameraListBox.TabIndex = 6;
            // 
            // SessionLabel
            // 
            this.SessionLabel.AutoSize = true;
            this.SessionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionLabel.Location = new System.Drawing.Point(6, 16);
            this.SessionLabel.Name = "SessionLabel";
            this.SessionLabel.Size = new System.Drawing.Size(109, 16);
            this.SessionLabel.TabIndex = 8;
            this.SessionLabel.Text = "No open session";
            // 
            // SessionButton
            // 
            this.SessionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SessionButton.Location = new System.Drawing.Point(6, 127);
            this.SessionButton.Name = "SessionButton";
            this.SessionButton.Size = new System.Drawing.Size(84, 23);
            this.SessionButton.TabIndex = 7;
            this.SessionButton.Text = "Open Session";
            this.SessionButton.UseVisualStyleBackColor = true;
            this.SessionButton.Click += new System.EventHandler(this.SessionButton_Click);
            // 
            // SaveFolderBrowser
            // 
            this.SaveFolderBrowser.Description = "Save Images To...";
            // 
            // SettingsTabControl
            // 
            this.SettingsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsTabControl.Controls.Add(this.SettingsTabPage);
            this.SettingsTabControl.Controls.Add(this.EclipseTabPage);
            this.SettingsTabControl.Controls.Add(this.LocTabPage);
            this.SettingsTabControl.Controls.Add(this.SeqGenTabPage);
            this.SettingsTabControl.Controls.Add(this.CaptureTabPage);
            this.SettingsTabControl.HotTrack = true;
            this.SettingsTabControl.Location = new System.Drawing.Point(153, 13);
            this.SettingsTabControl.MinimumSize = new System.Drawing.Size(406, 219);
            this.SettingsTabControl.Name = "SettingsTabControl";
            this.SettingsTabControl.SelectedIndex = 0;
            this.SettingsTabControl.Size = new System.Drawing.Size(406, 219);
            this.SettingsTabControl.TabIndex = 10;
            this.SettingsTabControl.Tag = "";
            // 
            // SettingsTabPage
            // 
            this.SettingsTabPage.Controls.Add(this.AvCoBox);
            this.SettingsTabPage.Controls.Add(this.MainProgressBar);
            this.SettingsTabPage.Controls.Add(this.TvCoBox);
            this.SettingsTabPage.Controls.Add(this.SavePathTextBox);
            this.SettingsTabPage.Controls.Add(this.ISOCoBox);
            this.SettingsTabPage.Controls.Add(this.BrowseButton);
            this.SettingsTabPage.Controls.Add(this.BulbUpDo);
            this.SettingsTabPage.Controls.Add(this.SaveToGroupBox);
            this.SettingsTabPage.Controls.Add(this.TakePhotoButton);
            this.SettingsTabPage.Controls.Add(this.label4);
            this.SettingsTabPage.Controls.Add(this.RecordVideoButton);
            this.SettingsTabPage.Controls.Add(this.label3);
            this.SettingsTabPage.Controls.Add(this.label1);
            this.SettingsTabPage.Controls.Add(this.label2);
            this.SettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.SettingsTabPage.Name = "SettingsTabPage";
            this.SettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTabPage.Size = new System.Drawing.Size(398, 193);
            this.SettingsTabPage.TabIndex = 0;
            this.SettingsTabPage.Text = "Settings";
            this.SettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // AvCoBox
            // 
            this.AvCoBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AvCoBox.FormattingEnabled = true;
            this.AvCoBox.Location = new System.Drawing.Point(6, 6);
            this.AvCoBox.Name = "AvCoBox";
            this.AvCoBox.Size = new System.Drawing.Size(94, 21);
            this.AvCoBox.TabIndex = 0;
            this.AvCoBox.SelectedIndexChanged += new System.EventHandler(this.AvCoBox_SelectedIndexChanged);
            // 
            // MainProgressBar
            // 
            this.MainProgressBar.Location = new System.Drawing.Point(6, 87);
            this.MainProgressBar.Name = "MainProgressBar";
            this.MainProgressBar.Size = new System.Drawing.Size(130, 20);
            this.MainProgressBar.TabIndex = 8;
            // 
            // TvCoBox
            // 
            this.TvCoBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TvCoBox.FormattingEnabled = true;
            this.TvCoBox.Location = new System.Drawing.Point(6, 33);
            this.TvCoBox.Name = "TvCoBox";
            this.TvCoBox.Size = new System.Drawing.Size(94, 21);
            this.TvCoBox.TabIndex = 0;
            this.TvCoBox.SelectedIndexChanged += new System.EventHandler(this.TvCoBox_SelectedIndexChanged);
            // 
            // SavePathTextBox
            // 
            this.SavePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePathTextBox.Enabled = false;
            this.SavePathTextBox.Location = new System.Drawing.Point(5, 112);
            this.SavePathTextBox.Name = "SavePathTextBox";
            this.SavePathTextBox.Size = new System.Drawing.Size(282, 20);
            this.SavePathTextBox.TabIndex = 6;
            // 
            // ISOCoBox
            // 
            this.ISOCoBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ISOCoBox.FormattingEnabled = true;
            this.ISOCoBox.Location = new System.Drawing.Point(6, 60);
            this.ISOCoBox.Name = "ISOCoBox";
            this.ISOCoBox.Size = new System.Drawing.Size(94, 21);
            this.ISOCoBox.TabIndex = 0;
            this.ISOCoBox.SelectedIndexChanged += new System.EventHandler(this.ISOCoBox_SelectedIndexChanged);
            // 
            // BrowseButton
            // 
            this.BrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseButton.Enabled = false;
            this.BrowseButton.Location = new System.Drawing.Point(293, 110);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(99, 23);
            this.BrowseButton.TabIndex = 5;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // BulbUpDo
            // 
            this.BulbUpDo.Location = new System.Drawing.Point(142, 33);
            this.BulbUpDo.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.BulbUpDo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BulbUpDo.Name = "BulbUpDo";
            this.BulbUpDo.Size = new System.Drawing.Size(94, 20);
            this.BulbUpDo.TabIndex = 1;
            this.BulbUpDo.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // SaveToGroupBox
            // 
            this.SaveToGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveToGroupBox.Controls.Add(this.STBothRdButton);
            this.SaveToGroupBox.Controls.Add(this.STComputerRdButton);
            this.SaveToGroupBox.Controls.Add(this.STCameraRdButton);
            this.SaveToGroupBox.Location = new System.Drawing.Point(296, 7);
            this.SaveToGroupBox.Name = "SaveToGroupBox";
            this.SaveToGroupBox.Size = new System.Drawing.Size(96, 100);
            this.SaveToGroupBox.TabIndex = 4;
            this.SaveToGroupBox.TabStop = false;
            this.SaveToGroupBox.Text = "Save To";
            // 
            // STBothRdButton
            // 
            this.STBothRdButton.AutoSize = true;
            this.STBothRdButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.STBothRdButton.Location = new System.Drawing.Point(6, 71);
            this.STBothRdButton.Name = "STBothRdButton";
            this.STBothRdButton.Size = new System.Drawing.Size(52, 20);
            this.STBothRdButton.TabIndex = 0;
            this.STBothRdButton.Text = "Both";
            this.STBothRdButton.UseVisualStyleBackColor = true;
            this.STBothRdButton.CheckedChanged += new System.EventHandler(this.SaveToRdButton_CheckedChanged);
            // 
            // STComputerRdButton
            // 
            this.STComputerRdButton.AutoSize = true;
            this.STComputerRdButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.STComputerRdButton.Location = new System.Drawing.Point(6, 45);
            this.STComputerRdButton.Name = "STComputerRdButton";
            this.STComputerRdButton.Size = new System.Drawing.Size(83, 20);
            this.STComputerRdButton.TabIndex = 0;
            this.STComputerRdButton.Text = "Computer";
            this.STComputerRdButton.UseVisualStyleBackColor = true;
            this.STComputerRdButton.CheckedChanged += new System.EventHandler(this.SaveToRdButton_CheckedChanged);
            // 
            // STCameraRdButton
            // 
            this.STCameraRdButton.AutoSize = true;
            this.STCameraRdButton.Checked = true;
            this.STCameraRdButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.STCameraRdButton.Location = new System.Drawing.Point(6, 19);
            this.STCameraRdButton.Name = "STCameraRdButton";
            this.STCameraRdButton.Size = new System.Drawing.Size(73, 20);
            this.STCameraRdButton.TabIndex = 0;
            this.STCameraRdButton.TabStop = true;
            this.STCameraRdButton.Text = "Camera";
            this.STCameraRdButton.UseVisualStyleBackColor = true;
            this.STCameraRdButton.CheckedChanged += new System.EventHandler(this.SaveToRdButton_CheckedChanged);
            // 
            // TakePhotoButton
            // 
            this.TakePhotoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TakePhotoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TakePhotoButton.Location = new System.Drawing.Point(244, 139);
            this.TakePhotoButton.Name = "TakePhotoButton";
            this.TakePhotoButton.Size = new System.Drawing.Size(71, 48);
            this.TakePhotoButton.TabIndex = 2;
            this.TakePhotoButton.Text = "Take Photo";
            this.TakePhotoButton.UseVisualStyleBackColor = true;
            this.TakePhotoButton.Click += new System.EventHandler(this.TakePhotoButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(242, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Bulb (s)";
            // 
            // RecordVideoButton
            // 
            this.RecordVideoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RecordVideoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordVideoButton.Location = new System.Drawing.Point(321, 139);
            this.RecordVideoButton.Name = "RecordVideoButton";
            this.RecordVideoButton.Size = new System.Drawing.Size(71, 48);
            this.RecordVideoButton.TabIndex = 2;
            this.RecordVideoButton.Text = "Record Video";
            this.RecordVideoButton.UseVisualStyleBackColor = true;
            this.RecordVideoButton.Click += new System.EventHandler(this.RecordVideoButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(106, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "ISO";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(106, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Av";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(106, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tv";
            // 
            // LocTabPage
            // 
            this.LocTabPage.Controls.Add(this.splitContainer1);
            this.LocTabPage.Location = new System.Drawing.Point(4, 22);
            this.LocTabPage.Name = "LocTabPage";
            this.LocTabPage.Size = new System.Drawing.Size(398, 193);
            this.LocTabPage.TabIndex = 2;
            this.LocTabPage.Text = "Location";
            this.LocTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LonLabel);
            this.splitContainer1.Panel1.Controls.Add(this.LatLabel);
            this.splitContainer1.Panel1.Controls.Add(this.GPSDateTimeTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.GPSStatusTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.SetLocButton);
            this.splitContainer1.Panel1.Controls.Add(this.GetGPSButton);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.AltTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.LonTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.LatTextBox);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel1MinSize = 133;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gmap);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel2MinSize = 200;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(398, 193);
            this.splitContainer1.SplitterDistance = 133;
            this.splitContainer1.TabIndex = 0;
            // 
            // LonLabel
            // 
            this.LonLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.LonLabel.Location = new System.Drawing.Point(14, 174);
            this.LonLabel.Name = "LonLabel";
            this.LonLabel.Size = new System.Drawing.Size(90, 16);
            this.LonLabel.TabIndex = 4;
            this.LonLabel.Text = "Shooting Lng";
            this.LonLabel.Visible = false;
            // 
            // LatLabel
            // 
            this.LatLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.LatLabel.Location = new System.Drawing.Point(14, 161);
            this.LatLabel.Name = "LatLabel";
            this.LatLabel.Size = new System.Drawing.Size(90, 13);
            this.LatLabel.TabIndex = 4;
            this.LatLabel.Text = "Shooting Lat";
            this.LatLabel.Visible = false;
            // 
            // GPSDateTimeTextBox
            // 
            this.GPSDateTimeTextBox.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.GPSDateTimeTextBox.Location = new System.Drawing.Point(14, 121);
            this.GPSDateTimeTextBox.Name = "GPSDateTimeTextBox";
            this.GPSDateTimeTextBox.Size = new System.Drawing.Size(100, 13);
            this.GPSDateTimeTextBox.TabIndex = 4;
            this.GPSDateTimeTextBox.Text = "GPS Date Time";
            this.GPSDateTimeTextBox.Visible = false;
            // 
            // GPSStatusTextBox
            // 
            this.GPSStatusTextBox.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.GPSStatusTextBox.Location = new System.Drawing.Point(14, 108);
            this.GPSStatusTextBox.Name = "GPSStatusTextBox";
            this.GPSStatusTextBox.Size = new System.Drawing.Size(100, 13);
            this.GPSStatusTextBox.TabIndex = 4;
            this.GPSStatusTextBox.Text = "No GPS data...";
            this.GPSStatusTextBox.Visible = false;
            // 
            // SetLocButton
            // 
            this.SetLocButton.Location = new System.Drawing.Point(8, 137);
            this.SetLocButton.Name = "SetLocButton";
            this.SetLocButton.Size = new System.Drawing.Size(96, 23);
            this.SetLocButton.TabIndex = 4;
            this.SetLocButton.Text = "Set Location";
            this.SetLocButton.UseVisualStyleBackColor = true;
            this.SetLocButton.Click += new System.EventHandler(this.SetLocButton_Click);
            // 
            // GetGPSButton
            // 
            this.GetGPSButton.Enabled = false;
            this.GetGPSButton.Location = new System.Drawing.Point(8, 86);
            this.GetGPSButton.Name = "GetGPSButton";
            this.GetGPSButton.Size = new System.Drawing.Size(96, 23);
            this.GetGPSButton.TabIndex = 3;
            this.GetGPSButton.Text = "Get Camera GPS";
            this.GetGPSButton.UseVisualStyleBackColor = true;
            this.GetGPSButton.Click += new System.EventHandler(this.GetGPSButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(90, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Elv (m)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label9.Location = new System.Drawing.Point(104, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Lng";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(104, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Lng";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label8.Location = new System.Drawing.Point(104, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Lat";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(104, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Lat";
            // 
            // AltTextBox
            // 
            this.AltTextBox.Location = new System.Drawing.Point(8, 60);
            this.AltTextBox.Name = "AltTextBox";
            this.AltTextBox.Size = new System.Drawing.Size(80, 20);
            this.AltTextBox.TabIndex = 2;
            // 
            // LonTextBox
            // 
            this.LonTextBox.Location = new System.Drawing.Point(8, 34);
            this.LonTextBox.Name = "LonTextBox";
            this.LonTextBox.Size = new System.Drawing.Size(96, 20);
            this.LonTextBox.TabIndex = 1;
            // 
            // LatTextBox
            // 
            this.LatTextBox.Location = new System.Drawing.Point(8, 8);
            this.LatTextBox.Name = "LatTextBox";
            this.LatTextBox.Size = new System.Drawing.Size(96, 20);
            this.LatTextBox.TabIndex = 0;
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemory = 5;
            this.gmap.Location = new System.Drawing.Point(3, 3);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 2;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(256, 187);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 0D;
            this.gmap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseClick);
            // 
            // SeqGenTabPage
            // 
            this.SeqGenTabPage.Location = new System.Drawing.Point(4, 22);
            this.SeqGenTabPage.Name = "SeqGenTabPage";
            this.SeqGenTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SeqGenTabPage.Size = new System.Drawing.Size(398, 193);
            this.SeqGenTabPage.TabIndex = 1;
            this.SeqGenTabPage.Text = "Sequence Gen";
            this.SeqGenTabPage.UseVisualStyleBackColor = true;
            // 
            // TakeNPhotoButton
            // 
            this.TakeNPhotoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TakeNPhotoButton.Location = new System.Drawing.Point(10, 13);
            this.TakeNPhotoButton.Name = "TakeNPhotoButton";
            this.TakeNPhotoButton.Size = new System.Drawing.Size(116, 23);
            this.TakeNPhotoButton.TabIndex = 9;
            this.TakeNPhotoButton.Text = "Take N Photos";
            this.TakeNPhotoButton.UseVisualStyleBackColor = true;
            this.TakeNPhotoButton.Click += new System.EventHandler(this.TakeNPhotoButton_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ExpandButton);
            this.splitContainer2.Panel1.Controls.Add(this.InitGroupBox);
            this.splitContainer2.Panel1.Controls.Add(this.SettingsTabControl);
            this.splitContainer2.Panel1.Controls.Add(this.LiveViewGroupBox);
            this.splitContainer2.Panel1MinSize = 566;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.SequenceGroupBox);
            this.splitContainer2.Size = new System.Drawing.Size(862, 607);
            this.splitContainer2.SplitterDistance = 566;
            this.splitContainer2.TabIndex = 14;
            // 
            // ExpandButton
            // 
            this.ExpandButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExpandButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExpandButton.Location = new System.Drawing.Point(549, 216);
            this.ExpandButton.Name = "ExpandButton";
            this.ExpandButton.Size = new System.Drawing.Size(17, 52);
            this.ExpandButton.TabIndex = 7;
            this.ExpandButton.TabStop = false;
            this.ExpandButton.Text = "<";
            this.ExpandButton.UseVisualStyleBackColor = true;
            this.ExpandButton.Click += new System.EventHandler(this.ExpandButton_Click);
            // 
            // SequenceGroupBox
            // 
            this.SequenceGroupBox.Controls.Add(this.SeqFlowPanel);
            this.SequenceGroupBox.Location = new System.Drawing.Point(3, 13);
            this.SequenceGroupBox.Name = "SequenceGroupBox";
            this.SequenceGroupBox.Size = new System.Drawing.Size(284, 581);
            this.SequenceGroupBox.TabIndex = 0;
            this.SequenceGroupBox.TabStop = false;
            this.SequenceGroupBox.Text = "Sequence";
            // 
            // SeqFlowPanel
            // 
            this.SeqFlowPanel.AutoScroll = true;
            this.SeqFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.SeqFlowPanel.Location = new System.Drawing.Point(16, 66);
            this.SeqFlowPanel.Name = "SeqFlowPanel";
            this.SeqFlowPanel.Size = new System.Drawing.Size(248, 503);
            this.SeqFlowPanel.TabIndex = 0;
            this.SeqFlowPanel.WrapContents = false;
            // 
            // EclipseTabPage
            // 
            this.EclipseTabPage.Location = new System.Drawing.Point(4, 22);
            this.EclipseTabPage.Name = "EclipseTabPage";
            this.EclipseTabPage.Size = new System.Drawing.Size(398, 193);
            this.EclipseTabPage.TabIndex = 3;
            this.EclipseTabPage.Text = "Eclipse";
            this.EclipseTabPage.UseVisualStyleBackColor = true;
            // 
            // CaptureTabPage
            // 
            this.CaptureTabPage.Controls.Add(this.TakeNPhotoButton);
            this.CaptureTabPage.Location = new System.Drawing.Point(4, 22);
            this.CaptureTabPage.Name = "CaptureTabPage";
            this.CaptureTabPage.Size = new System.Drawing.Size(398, 193);
            this.CaptureTabPage.TabIndex = 4;
            this.CaptureTabPage.Text = "Capture";
            this.CaptureTabPage.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 607);
            this.Controls.Add(this.splitContainer2);
            this.Name = "MainForm";
            this.Text = "EOS Eclipse";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.LiveViewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LiveViewPicBox)).EndInit();
            this.InitGroupBox.ResumeLayout(false);
            this.InitGroupBox.PerformLayout();
            this.SettingsTabControl.ResumeLayout(false);
            this.SettingsTabPage.ResumeLayout(false);
            this.SettingsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BulbUpDo)).EndInit();
            this.SaveToGroupBox.ResumeLayout(false);
            this.SaveToGroupBox.PerformLayout();
            this.LocTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.SequenceGroupBox.ResumeLayout(false);
            this.CaptureTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox LiveViewGroupBox;
        private System.Windows.Forms.Button FocusFar3Button;
        private System.Windows.Forms.Button FocusFar2Button;
        private System.Windows.Forms.Button FocusFar1Button;
        private System.Windows.Forms.Button FocusNear1Button;
        private System.Windows.Forms.Button FocusNear2Button;
        private System.Windows.Forms.Button FocusNear3Button;
        private System.Windows.Forms.PictureBox LiveViewPicBox;
        private System.Windows.Forms.Button LiveViewButton;
        private System.Windows.Forms.GroupBox InitGroupBox;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.ListBox CameraListBox;
        private System.Windows.Forms.Label SessionLabel;
        private System.Windows.Forms.Button SessionButton;
        private System.Windows.Forms.FolderBrowserDialog SaveFolderBrowser;
        private System.Windows.Forms.TabControl SettingsTabControl;
        private System.Windows.Forms.TabPage SettingsTabPage;
        private System.Windows.Forms.ComboBox AvCoBox;
        private System.Windows.Forms.ProgressBar MainProgressBar;
        private System.Windows.Forms.ComboBox TvCoBox;
        private System.Windows.Forms.TextBox SavePathTextBox;
        private System.Windows.Forms.ComboBox ISOCoBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.NumericUpDown BulbUpDo;
        private System.Windows.Forms.GroupBox SaveToGroupBox;
        private System.Windows.Forms.RadioButton STBothRdButton;
        private System.Windows.Forms.RadioButton STComputerRdButton;
        private System.Windows.Forms.RadioButton STCameraRdButton;
        private System.Windows.Forms.Button TakePhotoButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button RecordVideoButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage SeqGenTabPage;
        private System.Windows.Forms.Button TakeNPhotoButton;
        private System.Windows.Forms.TabPage LocTabPage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox LonTextBox;
        private System.Windows.Forms.TextBox LatTextBox;
        private System.Windows.Forms.Button GetGPSButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SetLocButton;
        private System.Windows.Forms.Label GPSStatusTextBox;
        private System.Windows.Forms.Label LatLabel;
        private System.Windows.Forms.Label GPSDateTimeTextBox;
        private System.Windows.Forms.Label LonLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox AltTextBox;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button ExpandButton;
        private System.Windows.Forms.GroupBox SequenceGroupBox;
        private System.Windows.Forms.FlowLayoutPanel SeqFlowPanel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage EclipseTabPage;
        private System.Windows.Forms.TabPage CaptureTabPage;
    }
}

