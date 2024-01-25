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
            this.components = new System.ComponentModel.Container();
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
            this.SaveBrowseButton = new System.Windows.Forms.Button();
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
            this.EclipseTabPage = new System.Windows.Forms.TabPage();
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
            this.SeqSettingsPanel = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.SeqAvCoBox = new System.Windows.Forms.ComboBox();
            this.SeqIsoCoBox = new System.Windows.Forms.ComboBox();
            this.AEBGroupBox = new System.Windows.Forms.GroupBox();
            this.AEBRadioButton = new System.Windows.Forms.RadioButton();
            this.AEBDisabledRadioButton = new System.Windows.Forms.RadioButton();
            this.AEBUpDown = new System.Windows.Forms.DomainUpDown();
            this.ExposureGroupBox = new System.Windows.Forms.GroupBox();
            this.SeqTvListBox = new System.Windows.Forms.ListBox();
            this.CancelStageButton = new System.Windows.Forms.Button();
            this.AddStageButton = new System.Windows.Forms.Button();
            this.ScriptGroupBox = new System.Windows.Forms.GroupBox();
            this.ScriptBrowseButton = new System.Windows.Forms.Button();
            this.ScriptTextBox = new System.Windows.Forms.TextBox();
            this.IntervalGroupBox = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.IntervalSecUpDown = new System.Windows.Forms.NumericUpDown();
            this.IntervalMinUpDown = new System.Windows.Forms.NumericUpDown();
            this.IntervalRadioButton = new System.Windows.Forms.RadioButton();
            this.SingleRadioButton = new System.Windows.Forms.RadioButton();
            this.ContinuousRadioButton = new System.Windows.Forms.RadioButton();
            this.EndOffsetUpDown = new System.Windows.Forms.NumericUpDown();
            this.StartOffsetUpDown = new System.Windows.Forms.NumericUpDown();
            this.EndRefComboBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.StartRefComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.PhaseComboBox = new System.Windows.Forms.ComboBox();
            this.CaptureTabPage = new System.Windows.Forms.TabPage();
            this.TakeNPhotoButton = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ExpandButton = new System.Windows.Forms.Button();
            this.SequenceGroupBox = new System.Windows.Forms.GroupBox();
            this.ClearSeqButton = new System.Windows.Forms.Button();
            this.LoadSeqButton = new System.Windows.Forms.Button();
            this.SaveSeqButton = new System.Windows.Forms.Button();
            this.SeqFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SeqSizerPanel = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.LoadScriptBrowserOLD = new System.Windows.Forms.FolderBrowserDialog();
            this.ScriptFileBrowser = new System.Windows.Forms.OpenFileDialog();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.LoadSettingsButton = new System.Windows.Forms.Button();
            this.LoadedCameraSettingsLabel = new System.Windows.Forms.Label();
            this.SettingsFileBrowser = new System.Windows.Forms.OpenFileDialog();
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
            this.SeqGenTabPage.SuspendLayout();
            this.SeqSettingsPanel.SuspendLayout();
            this.AEBGroupBox.SuspendLayout();
            this.ExposureGroupBox.SuspendLayout();
            this.ScriptGroupBox.SuspendLayout();
            this.IntervalGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalSecUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalMinUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndOffsetUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartOffsetUpDown)).BeginInit();
            this.CaptureTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SequenceGroupBox.SuspendLayout();
            this.SeqFlowPanel.SuspendLayout();
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
            this.LiveViewGroupBox.Size = new System.Drawing.Size(541, 355);
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
            this.LiveViewPicBox.Size = new System.Drawing.Size(522, 293);
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
            this.SettingsTabPage.Controls.Add(this.SaveBrowseButton);
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
            // SaveBrowseButton
            // 
            this.SaveBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveBrowseButton.Enabled = false;
            this.SaveBrowseButton.Location = new System.Drawing.Point(293, 110);
            this.SaveBrowseButton.Name = "SaveBrowseButton";
            this.SaveBrowseButton.Size = new System.Drawing.Size(99, 23);
            this.SaveBrowseButton.TabIndex = 5;
            this.SaveBrowseButton.Text = "Browse";
            this.SaveBrowseButton.UseVisualStyleBackColor = true;
            this.SaveBrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
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
            // EclipseTabPage
            // 
            this.EclipseTabPage.Location = new System.Drawing.Point(4, 22);
            this.EclipseTabPage.Name = "EclipseTabPage";
            this.EclipseTabPage.Size = new System.Drawing.Size(398, 193);
            this.EclipseTabPage.TabIndex = 3;
            this.EclipseTabPage.Text = "Eclipse";
            this.EclipseTabPage.UseVisualStyleBackColor = true;
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
            this.SeqGenTabPage.AutoScroll = true;
            this.SeqGenTabPage.AutoScrollMargin = new System.Drawing.Size(0, 25);
            this.SeqGenTabPage.Controls.Add(this.LoadedCameraSettingsLabel);
            this.SeqGenTabPage.Controls.Add(this.LoadSettingsButton);
            this.SeqGenTabPage.Controls.Add(this.SaveSettingsButton);
            this.SeqGenTabPage.Controls.Add(this.SeqSettingsPanel);
            this.SeqGenTabPage.Controls.Add(this.CancelStageButton);
            this.SeqGenTabPage.Controls.Add(this.AddStageButton);
            this.SeqGenTabPage.Controls.Add(this.ScriptGroupBox);
            this.SeqGenTabPage.Controls.Add(this.IntervalGroupBox);
            this.SeqGenTabPage.Controls.Add(this.EndOffsetUpDown);
            this.SeqGenTabPage.Controls.Add(this.StartOffsetUpDown);
            this.SeqGenTabPage.Controls.Add(this.EndRefComboBox);
            this.SeqGenTabPage.Controls.Add(this.label12);
            this.SeqGenTabPage.Controls.Add(this.StartRefComboBox);
            this.SeqGenTabPage.Controls.Add(this.label11);
            this.SeqGenTabPage.Controls.Add(this.label10);
            this.SeqGenTabPage.Controls.Add(this.PhaseComboBox);
            this.SeqGenTabPage.Location = new System.Drawing.Point(4, 22);
            this.SeqGenTabPage.Name = "SeqGenTabPage";
            this.SeqGenTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 100);
            this.SeqGenTabPage.Size = new System.Drawing.Size(398, 193);
            this.SeqGenTabPage.TabIndex = 1;
            this.SeqGenTabPage.Text = "Sequence Gen";
            this.SeqGenTabPage.UseVisualStyleBackColor = true;
            // 
            // SeqSettingsPanel
            // 
            this.SeqSettingsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SeqSettingsPanel.Controls.Add(this.label16);
            this.SeqSettingsPanel.Controls.Add(this.label15);
            this.SeqSettingsPanel.Controls.Add(this.SeqAvCoBox);
            this.SeqSettingsPanel.Controls.Add(this.SeqIsoCoBox);
            this.SeqSettingsPanel.Controls.Add(this.AEBGroupBox);
            this.SeqSettingsPanel.Controls.Add(this.ExposureGroupBox);
            this.SeqSettingsPanel.Location = new System.Drawing.Point(0, 144);
            this.SeqSettingsPanel.Name = "SeqSettingsPanel";
            this.SeqSettingsPanel.Size = new System.Drawing.Size(309, 146);
            this.SeqSettingsPanel.TabIndex = 14;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(243, 111);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 13);
            this.label16.TabIndex = 13;
            this.label16.Text = "Av";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(243, 84);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "ISO";
            // 
            // SeqAvCoBox
            // 
            this.SeqAvCoBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SeqAvCoBox.FormattingEnabled = true;
            this.SeqAvCoBox.Location = new System.Drawing.Point(147, 106);
            this.SeqAvCoBox.Name = "SeqAvCoBox";
            this.SeqAvCoBox.Size = new System.Drawing.Size(90, 21);
            this.SeqAvCoBox.TabIndex = 12;
            // 
            // SeqIsoCoBox
            // 
            this.SeqIsoCoBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SeqIsoCoBox.FormattingEnabled = true;
            this.SeqIsoCoBox.Location = new System.Drawing.Point(147, 79);
            this.SeqIsoCoBox.Name = "SeqIsoCoBox";
            this.SeqIsoCoBox.Size = new System.Drawing.Size(90, 21);
            this.SeqIsoCoBox.TabIndex = 12;
            // 
            // AEBGroupBox
            // 
            this.AEBGroupBox.Controls.Add(this.AEBRadioButton);
            this.AEBGroupBox.Controls.Add(this.AEBDisabledRadioButton);
            this.AEBGroupBox.Controls.Add(this.AEBUpDown);
            this.AEBGroupBox.Location = new System.Drawing.Point(147, 3);
            this.AEBGroupBox.Name = "AEBGroupBox";
            this.AEBGroupBox.Size = new System.Drawing.Size(149, 72);
            this.AEBGroupBox.TabIndex = 8;
            this.AEBGroupBox.TabStop = false;
            this.AEBGroupBox.Text = "Bracketing (AEB)";
            // 
            // AEBRadioButton
            // 
            this.AEBRadioButton.AutoSize = true;
            this.AEBRadioButton.Location = new System.Drawing.Point(7, 43);
            this.AEBRadioButton.Name = "AEBRadioButton";
            this.AEBRadioButton.Size = new System.Drawing.Size(39, 17);
            this.AEBRadioButton.TabIndex = 1;
            this.AEBRadioButton.Text = "+/-";
            this.AEBRadioButton.UseVisualStyleBackColor = true;
            // 
            // AEBDisabledRadioButton
            // 
            this.AEBDisabledRadioButton.AutoSize = true;
            this.AEBDisabledRadioButton.Checked = true;
            this.AEBDisabledRadioButton.Location = new System.Drawing.Point(7, 20);
            this.AEBDisabledRadioButton.Name = "AEBDisabledRadioButton";
            this.AEBDisabledRadioButton.Size = new System.Drawing.Size(66, 17);
            this.AEBDisabledRadioButton.TabIndex = 1;
            this.AEBDisabledRadioButton.TabStop = true;
            this.AEBDisabledRadioButton.Text = "Disabled";
            this.AEBDisabledRadioButton.UseVisualStyleBackColor = true;
            this.AEBDisabledRadioButton.CheckedChanged += new System.EventHandler(this.AEBDisabledRadioButton_CheckedChanged);
            // 
            // AEBUpDown
            // 
            this.AEBUpDown.Location = new System.Drawing.Point(46, 43);
            this.AEBUpDown.Name = "AEBUpDown";
            this.AEBUpDown.Size = new System.Drawing.Size(90, 20);
            this.AEBUpDown.TabIndex = 0;
            this.AEBUpDown.Text = "1/3";
            // 
            // ExposureGroupBox
            // 
            this.ExposureGroupBox.Controls.Add(this.SeqTvListBox);
            this.ExposureGroupBox.Location = new System.Drawing.Point(6, 3);
            this.ExposureGroupBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.ExposureGroupBox.Name = "ExposureGroupBox";
            this.ExposureGroupBox.Size = new System.Drawing.Size(135, 137);
            this.ExposureGroupBox.TabIndex = 7;
            this.ExposureGroupBox.TabStop = false;
            this.ExposureGroupBox.Text = "Exposure Lengths";
            // 
            // SeqTvListBox
            // 
            this.SeqTvListBox.FormattingEnabled = true;
            this.SeqTvListBox.Location = new System.Drawing.Point(6, 19);
            this.SeqTvListBox.Name = "SeqTvListBox";
            this.SeqTvListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.SeqTvListBox.Size = new System.Drawing.Size(120, 108);
            this.SeqTvListBox.TabIndex = 6;
            // 
            // CancelStageButton
            // 
            this.CancelStageButton.Location = new System.Drawing.Point(217, 358);
            this.CancelStageButton.Name = "CancelStageButton";
            this.CancelStageButton.Size = new System.Drawing.Size(75, 23);
            this.CancelStageButton.TabIndex = 11;
            this.CancelStageButton.Text = "Reset";
            this.CancelStageButton.UseVisualStyleBackColor = true;
            this.CancelStageButton.Click += new System.EventHandler(this.CancelStageButton_Click);
            // 
            // AddStageButton
            // 
            this.AddStageButton.Location = new System.Drawing.Point(298, 358);
            this.AddStageButton.Name = "AddStageButton";
            this.AddStageButton.Size = new System.Drawing.Size(75, 23);
            this.AddStageButton.TabIndex = 11;
            this.AddStageButton.Text = "Add Stage";
            this.AddStageButton.UseVisualStyleBackColor = true;
            this.AddStageButton.Click += new System.EventHandler(this.AddStageButton_Click);
            // 
            // ScriptGroupBox
            // 
            this.ScriptGroupBox.Controls.Add(this.ScriptBrowseButton);
            this.ScriptGroupBox.Controls.Add(this.ScriptTextBox);
            this.ScriptGroupBox.Location = new System.Drawing.Point(6, 290);
            this.ScriptGroupBox.Name = "ScriptGroupBox";
            this.ScriptGroupBox.Size = new System.Drawing.Size(367, 53);
            this.ScriptGroupBox.TabIndex = 10;
            this.ScriptGroupBox.TabStop = false;
            this.ScriptGroupBox.Text = "Script";
            // 
            // ScriptBrowseButton
            // 
            this.ScriptBrowseButton.Location = new System.Drawing.Point(286, 18);
            this.ScriptBrowseButton.Name = "ScriptBrowseButton";
            this.ScriptBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.ScriptBrowseButton.TabIndex = 10;
            this.ScriptBrowseButton.Text = "Browse";
            this.ScriptBrowseButton.UseVisualStyleBackColor = true;
            this.ScriptBrowseButton.Click += new System.EventHandler(this.ScriptBrowseButton_Click);
            // 
            // ScriptTextBox
            // 
            this.ScriptTextBox.Location = new System.Drawing.Point(6, 19);
            this.ScriptTextBox.Name = "ScriptTextBox";
            this.ScriptTextBox.Size = new System.Drawing.Size(274, 20);
            this.ScriptTextBox.TabIndex = 9;
            // 
            // IntervalGroupBox
            // 
            this.IntervalGroupBox.Controls.Add(this.label14);
            this.IntervalGroupBox.Controls.Add(this.label13);
            this.IntervalGroupBox.Controls.Add(this.IntervalSecUpDown);
            this.IntervalGroupBox.Controls.Add(this.IntervalMinUpDown);
            this.IntervalGroupBox.Controls.Add(this.IntervalRadioButton);
            this.IntervalGroupBox.Controls.Add(this.SingleRadioButton);
            this.IntervalGroupBox.Controls.Add(this.ContinuousRadioButton);
            this.IntervalGroupBox.Location = new System.Drawing.Point(6, 88);
            this.IntervalGroupBox.Name = "IntervalGroupBox";
            this.IntervalGroupBox.Size = new System.Drawing.Size(367, 53);
            this.IntervalGroupBox.TabIndex = 5;
            this.IntervalGroupBox.TabStop = false;
            this.IntervalGroupBox.Text = "Stage Repeat";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(340, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "sec";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(267, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "min";
            // 
            // IntervalSecUpDown
            // 
            this.IntervalSecUpDown.Location = new System.Drawing.Point(294, 18);
            this.IntervalSecUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.IntervalSecUpDown.Name = "IntervalSecUpDown";
            this.IntervalSecUpDown.Size = new System.Drawing.Size(46, 20);
            this.IntervalSecUpDown.TabIndex = 5;
            // 
            // IntervalMinUpDown
            // 
            this.IntervalMinUpDown.Location = new System.Drawing.Point(221, 18);
            this.IntervalMinUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.IntervalMinUpDown.Name = "IntervalMinUpDown";
            this.IntervalMinUpDown.Size = new System.Drawing.Size(46, 20);
            this.IntervalMinUpDown.TabIndex = 5;
            // 
            // IntervalRadioButton
            // 
            this.IntervalRadioButton.AutoSize = true;
            this.IntervalRadioButton.Checked = true;
            this.IntervalRadioButton.Location = new System.Drawing.Point(150, 21);
            this.IntervalRadioButton.Name = "IntervalRadioButton";
            this.IntervalRadioButton.Size = new System.Drawing.Size(60, 17);
            this.IntervalRadioButton.TabIndex = 4;
            this.IntervalRadioButton.TabStop = true;
            this.IntervalRadioButton.Text = "Interval";
            this.IntervalRadioButton.UseVisualStyleBackColor = true;
            this.IntervalRadioButton.CheckedChanged += new System.EventHandler(this.IntervalRadioButton_CheckedChanged);
            // 
            // SingleRadioButton
            // 
            this.SingleRadioButton.AutoSize = true;
            this.SingleRadioButton.Location = new System.Drawing.Point(90, 21);
            this.SingleRadioButton.Name = "SingleRadioButton";
            this.SingleRadioButton.Size = new System.Drawing.Size(54, 17);
            this.SingleRadioButton.TabIndex = 4;
            this.SingleRadioButton.Text = "Single";
            this.SingleRadioButton.UseVisualStyleBackColor = true;
            this.SingleRadioButton.CheckedChanged += new System.EventHandler(this.SingleRadioButton_CheckedChanged);
            // 
            // ContinuousRadioButton
            // 
            this.ContinuousRadioButton.AutoSize = true;
            this.ContinuousRadioButton.Location = new System.Drawing.Point(6, 21);
            this.ContinuousRadioButton.Name = "ContinuousRadioButton";
            this.ContinuousRadioButton.Size = new System.Drawing.Size(78, 17);
            this.ContinuousRadioButton.TabIndex = 4;
            this.ContinuousRadioButton.Text = "Continuous";
            this.ContinuousRadioButton.UseVisualStyleBackColor = true;
            // 
            // EndOffsetUpDown
            // 
            this.EndOffsetUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EndOffsetUpDown.Location = new System.Drawing.Point(65, 62);
            this.EndOffsetUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.EndOffsetUpDown.Minimum = new decimal(new int[] {
            3600,
            0,
            0,
            -2147483648});
            this.EndOffsetUpDown.Name = "EndOffsetUpDown";
            this.EndOffsetUpDown.Size = new System.Drawing.Size(62, 20);
            this.EndOffsetUpDown.TabIndex = 3;
            // 
            // StartOffsetUpDown
            // 
            this.StartOffsetUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StartOffsetUpDown.Location = new System.Drawing.Point(65, 35);
            this.StartOffsetUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.StartOffsetUpDown.Minimum = new decimal(new int[] {
            3600,
            0,
            0,
            -2147483648});
            this.StartOffsetUpDown.Name = "StartOffsetUpDown";
            this.StartOffsetUpDown.Size = new System.Drawing.Size(62, 20);
            this.StartOffsetUpDown.TabIndex = 3;
            // 
            // EndRefComboBox
            // 
            this.EndRefComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EndRefComboBox.FormattingEnabled = true;
            this.EndRefComboBox.Items.AddRange(new object[] {
            "C1",
            "C2",
            "C3",
            "C4"});
            this.EndRefComboBox.Location = new System.Drawing.Point(6, 61);
            this.EndRefComboBox.Name = "EndRefComboBox";
            this.EndRefComboBox.Size = new System.Drawing.Size(52, 21);
            this.EndRefComboBox.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(133, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "End +/- offset (sec)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StartRefComboBox
            // 
            this.StartRefComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StartRefComboBox.FormattingEnabled = true;
            this.StartRefComboBox.Items.AddRange(new object[] {
            "C1",
            "C2",
            "C3",
            "C4"});
            this.StartRefComboBox.Location = new System.Drawing.Point(6, 34);
            this.StartRefComboBox.Name = "StartRefComboBox";
            this.StartRefComboBox.Size = new System.Drawing.Size(52, 21);
            this.StartRefComboBox.TabIndex = 2;
            this.StartRefComboBox.SelectedIndexChanged += new System.EventHandler(this.StartRefComboBox_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(133, 37);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Start +/- offset (sec)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(133, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Phase";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PhaseComboBox
            // 
            this.PhaseComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.PhaseComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.PhaseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PhaseComboBox.FormattingEnabled = true;
            this.PhaseComboBox.Location = new System.Drawing.Point(6, 7);
            this.PhaseComboBox.Name = "PhaseComboBox";
            this.PhaseComboBox.Size = new System.Drawing.Size(121, 21);
            this.PhaseComboBox.TabIndex = 0;
            this.PhaseComboBox.SelectedIndexChanged += new System.EventHandler(this.PhaseComboBox_SelectedIndexChanged);
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
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ExpandButton);
            this.splitContainer2.Panel1.Controls.Add(this.InitGroupBox);
            this.splitContainer2.Panel1.Controls.Add(this.SettingsTabControl);
            this.splitContainer2.Panel1.Controls.Add(this.LiveViewGroupBox);
            this.splitContainer2.Panel1MinSize = 560;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.AutoScrollMargin = new System.Drawing.Size(3, 3);
            this.splitContainer2.Panel2.Controls.Add(this.SequenceGroupBox);
            this.splitContainer2.Panel2MinSize = 305;
            this.splitContainer2.Size = new System.Drawing.Size(871, 607);
            this.splitContainer2.SplitterDistance = 560;
            this.splitContainer2.TabIndex = 14;
            // 
            // ExpandButton
            // 
            this.ExpandButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExpandButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExpandButton.Location = new System.Drawing.Point(543, 216);
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
            this.SequenceGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SequenceGroupBox.Controls.Add(this.ClearSeqButton);
            this.SequenceGroupBox.Controls.Add(this.LoadSeqButton);
            this.SequenceGroupBox.Controls.Add(this.SaveSeqButton);
            this.SequenceGroupBox.Controls.Add(this.SeqFlowPanel);
            this.SequenceGroupBox.Location = new System.Drawing.Point(3, 13);
            this.SequenceGroupBox.MinimumSize = new System.Drawing.Size(292, 581);
            this.SequenceGroupBox.Name = "SequenceGroupBox";
            this.SequenceGroupBox.Size = new System.Drawing.Size(298, 581);
            this.SequenceGroupBox.TabIndex = 0;
            this.SequenceGroupBox.TabStop = false;
            this.SequenceGroupBox.Text = "Sequence";
            // 
            // ClearSeqButton
            // 
            this.ClearSeqButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearSeqButton.Location = new System.Drawing.Point(16, 546);
            this.ClearSeqButton.Name = "ClearSeqButton";
            this.ClearSeqButton.Size = new System.Drawing.Size(75, 23);
            this.ClearSeqButton.TabIndex = 11;
            this.ClearSeqButton.Text = "Clear";
            this.ClearSeqButton.UseVisualStyleBackColor = true;
            this.ClearSeqButton.Click += new System.EventHandler(this.ClearSeqButton_Click);
            // 
            // LoadSeqButton
            // 
            this.LoadSeqButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadSeqButton.Location = new System.Drawing.Point(123, 546);
            this.LoadSeqButton.Name = "LoadSeqButton";
            this.LoadSeqButton.Size = new System.Drawing.Size(75, 23);
            this.LoadSeqButton.TabIndex = 11;
            this.LoadSeqButton.Text = "Load";
            this.LoadSeqButton.UseVisualStyleBackColor = true;
            // 
            // SaveSeqButton
            // 
            this.SaveSeqButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveSeqButton.Location = new System.Drawing.Point(204, 546);
            this.SaveSeqButton.Name = "SaveSeqButton";
            this.SaveSeqButton.Size = new System.Drawing.Size(75, 23);
            this.SaveSeqButton.TabIndex = 11;
            this.SaveSeqButton.Text = "Save";
            this.SaveSeqButton.UseVisualStyleBackColor = true;
            this.SaveSeqButton.Click += new System.EventHandler(this.SaveSeqButton_Click);
            // 
            // SeqFlowPanel
            // 
            this.SeqFlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SeqFlowPanel.AutoScroll = true;
            this.SeqFlowPanel.BackColor = System.Drawing.SystemColors.Window;
            this.SeqFlowPanel.Controls.Add(this.SeqSizerPanel);
            this.SeqFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.SeqFlowPanel.Location = new System.Drawing.Point(16, 66);
            this.SeqFlowPanel.MinimumSize = new System.Drawing.Size(256, 0);
            this.SeqFlowPanel.Name = "SeqFlowPanel";
            this.SeqFlowPanel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 10);
            this.SeqFlowPanel.Size = new System.Drawing.Size(263, 472);
            this.SeqFlowPanel.TabIndex = 0;
            this.SeqFlowPanel.WrapContents = false;
            this.SeqFlowPanel.Resize += new System.EventHandler(this.SeqFlowPanel_Resize);
            // 
            // SeqSizerPanel
            // 
            this.SeqSizerPanel.Location = new System.Drawing.Point(8, 3);
            this.SeqSizerPanel.Name = "SeqSizerPanel";
            this.SeqSizerPanel.Size = new System.Drawing.Size(128, 0);
            this.SeqSizerPanel.TabIndex = 0;
            // 
            // ScriptFileBrowser
            // 
            this.ScriptFileBrowser.FileName = "openFileDialog1";
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.AutoSize = true;
            this.SaveSettingsButton.Location = new System.Drawing.Point(251, 7);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(122, 23);
            this.SaveSettingsButton.TabIndex = 15;
            this.SaveSettingsButton.Text = "Save Camera Settings";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButton_Click);
            // 
            // LoadSettingsButton
            // 
            this.LoadSettingsButton.AutoSize = true;
            this.LoadSettingsButton.Location = new System.Drawing.Point(251, 34);
            this.LoadSettingsButton.Name = "LoadSettingsButton";
            this.LoadSettingsButton.Size = new System.Drawing.Size(122, 23);
            this.LoadSettingsButton.TabIndex = 15;
            this.LoadSettingsButton.Text = "Load Camera Settings";
            this.LoadSettingsButton.UseVisualStyleBackColor = true;
            this.LoadSettingsButton.Click += new System.EventHandler(this.LoadSettingsButton_Click);
            // 
            // LoadedCameraSettingsLabel
            // 
            this.LoadedCameraSettingsLabel.AutoEllipsis = true;
            this.LoadedCameraSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadedCameraSettingsLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.LoadedCameraSettingsLabel.Location = new System.Drawing.Point(251, 61);
            this.LoadedCameraSettingsLabel.Name = "LoadedCameraSettingsLabel";
            this.LoadedCameraSettingsLabel.Size = new System.Drawing.Size(122, 16);
            this.LoadedCameraSettingsLabel.TabIndex = 16;
            this.LoadedCameraSettingsLabel.Text = "camera settings loaded";
            // 
            // SettingsFileBrowser
            // 
            this.SettingsFileBrowser.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 607);
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
            this.SeqGenTabPage.ResumeLayout(false);
            this.SeqGenTabPage.PerformLayout();
            this.SeqSettingsPanel.ResumeLayout(false);
            this.SeqSettingsPanel.PerformLayout();
            this.AEBGroupBox.ResumeLayout(false);
            this.AEBGroupBox.PerformLayout();
            this.ExposureGroupBox.ResumeLayout(false);
            this.ScriptGroupBox.ResumeLayout(false);
            this.ScriptGroupBox.PerformLayout();
            this.IntervalGroupBox.ResumeLayout(false);
            this.IntervalGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalSecUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalMinUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndOffsetUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartOffsetUpDown)).EndInit();
            this.CaptureTabPage.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.SequenceGroupBox.ResumeLayout(false);
            this.SeqFlowPanel.ResumeLayout(false);
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
        private System.Windows.Forms.Button SaveBrowseButton;
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
        private System.Windows.Forms.ComboBox PhaseComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox StartRefComboBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown StartOffsetUpDown;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton IntervalRadioButton;
        private System.Windows.Forms.NumericUpDown EndOffsetUpDown;
        private System.Windows.Forms.ComboBox EndRefComboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox IntervalGroupBox;
        private System.Windows.Forms.RadioButton SingleRadioButton;
        private System.Windows.Forms.RadioButton ContinuousRadioButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown IntervalSecUpDown;
        private System.Windows.Forms.NumericUpDown IntervalMinUpDown;
        private System.Windows.Forms.ListBox SeqTvListBox;
        private System.Windows.Forms.GroupBox ExposureGroupBox;
        private System.Windows.Forms.GroupBox AEBGroupBox;
        private System.Windows.Forms.DomainUpDown AEBUpDown;
        private System.Windows.Forms.RadioButton AEBRadioButton;
        private System.Windows.Forms.RadioButton AEBDisabledRadioButton;
        private System.Windows.Forms.FolderBrowserDialog LoadScriptBrowserOLD;
        private System.Windows.Forms.GroupBox ScriptGroupBox;
        private System.Windows.Forms.TextBox ScriptTextBox;
        private System.Windows.Forms.Button ScriptBrowseButton;
        private System.Windows.Forms.Button SaveSeqButton;
        private System.Windows.Forms.Button CancelStageButton;
        private System.Windows.Forms.Button AddStageButton;
        private System.Windows.Forms.Button ClearSeqButton;
        private System.Windows.Forms.Button LoadSeqButton;
        private System.Windows.Forms.OpenFileDialog ScriptFileBrowser;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox SeqAvCoBox;
        private System.Windows.Forms.ComboBox SeqIsoCoBox;
        private System.Windows.Forms.Panel SeqSettingsPanel;
        private System.Windows.Forms.Panel SeqSizerPanel;
        private System.Windows.Forms.Button LoadSettingsButton;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.Label LoadedCameraSettingsLabel;
        private System.Windows.Forms.OpenFileDialog SettingsFileBrowser;
    }
}

