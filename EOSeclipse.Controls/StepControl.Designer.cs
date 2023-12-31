﻿namespace EOSeclipse.Controls
{
    partial class StepControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.StepFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.PhaseLabel = new System.Windows.Forms.Label();
            this.StepProgressBar = new System.Windows.Forms.ProgressBar();
            this.StepStartRefLabel = new System.Windows.Forms.Label();
            this.StepEndRefLabel = new System.Windows.Forms.Label();
            this.StepTimer = new System.Windows.Forms.Timer(this.components);
            this.StepElapsedTimeLabel = new System.Windows.Forms.Label();
            this.StepRemainingTimeLabel = new System.Windows.Forms.Label();
            this.ProgressPanel = new System.Windows.Forms.Panel();
            this.StartEndPanel = new System.Windows.Forms.Panel();
            this.StartEndSplitter = new System.Windows.Forms.Splitter();
            this.IntervalTimerLabel = new System.Windows.Forms.Label();
            this.IntervalLabel = new System.Windows.Forms.Label();
            this.IntervalTimer = new System.Windows.Forms.Timer(this.components);
            this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.ProgressPanel.SuspendLayout();
            this.StartEndPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StepFlowLayoutPanel
            // 
            this.StepFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StepFlowLayoutPanel.AutoScroll = true;
            this.StepFlowLayoutPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.StepFlowLayoutPanel.Location = new System.Drawing.Point(80, 32);
            this.StepFlowLayoutPanel.Name = "StepFlowLayoutPanel";
            this.StepFlowLayoutPanel.Size = new System.Drawing.Size(158, 43);
            this.StepFlowLayoutPanel.TabIndex = 1;
            this.StepFlowLayoutPanel.TabStop = true;
            // 
            // PhaseLabel
            // 
            this.PhaseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PhaseLabel.AutoEllipsis = true;
            this.PhaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhaseLabel.Location = new System.Drawing.Point(8, 7);
            this.PhaseLabel.MinimumSize = new System.Drawing.Size(98, 0);
            this.PhaseLabel.Name = "PhaseLabel";
            this.PhaseLabel.Size = new System.Drawing.Size(115, 17);
            this.PhaseLabel.TabIndex = 0;
            this.PhaseLabel.Text = "Phase Label";
            // 
            // StepProgressBar
            // 
            this.StepProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StepProgressBar.ForeColor = System.Drawing.Color.BlueViolet;
            this.StepProgressBar.Location = new System.Drawing.Point(44, 2);
            this.StepProgressBar.Name = "StepProgressBar";
            this.StepProgressBar.Size = new System.Drawing.Size(136, 17);
            this.StepProgressBar.Step = 1;
            this.StepProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.StepProgressBar.TabIndex = 6;
            // 
            // StepStartRefLabel
            // 
            this.StepStartRefLabel.AutoEllipsis = true;
            this.StepStartRefLabel.Location = new System.Drawing.Point(3, 3);
            this.StepStartRefLabel.Name = "StepStartRefLabel";
            this.StepStartRefLabel.Size = new System.Drawing.Size(50, 13);
            this.StepStartRefLabel.TabIndex = 2;
            this.StepStartRefLabel.Text = "C1 -100";
            this.StepStartRefLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StepEndRefLabel
            // 
            this.StepEndRefLabel.AutoEllipsis = true;
            this.StepEndRefLabel.Location = new System.Drawing.Point(59, 3);
            this.StepEndRefLabel.Name = "StepEndRefLabel";
            this.StepEndRefLabel.Size = new System.Drawing.Size(50, 13);
            this.StepEndRefLabel.TabIndex = 3;
            this.StepEndRefLabel.Text = "C1 +100";
            this.StepEndRefLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StepTimer
            // 
            this.StepTimer.Tick += new System.EventHandler(this.StepTimer_Tick);
            // 
            // StepElapsedTimeLabel
            // 
            this.StepElapsedTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StepElapsedTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepElapsedTimeLabel.Location = new System.Drawing.Point(3, 4);
            this.StepElapsedTimeLabel.Name = "StepElapsedTimeLabel";
            this.StepElapsedTimeLabel.Size = new System.Drawing.Size(43, 13);
            this.StepElapsedTimeLabel.TabIndex = 4;
            this.StepElapsedTimeLabel.Text = "00:00:00";
            // 
            // StepRemainingTimeLabel
            // 
            this.StepRemainingTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepRemainingTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepRemainingTimeLabel.Location = new System.Drawing.Point(181, 4);
            this.StepRemainingTimeLabel.Name = "StepRemainingTimeLabel";
            this.StepRemainingTimeLabel.Size = new System.Drawing.Size(43, 13);
            this.StepRemainingTimeLabel.TabIndex = 5;
            this.StepRemainingTimeLabel.Text = "00:00:00";
            // 
            // ProgressPanel
            // 
            this.ProgressPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ProgressPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProgressPanel.Controls.Add(this.StepProgressBar);
            this.ProgressPanel.Controls.Add(this.StepRemainingTimeLabel);
            this.ProgressPanel.Controls.Add(this.StepElapsedTimeLabel);
            this.ProgressPanel.Location = new System.Drawing.Point(11, 80);
            this.ProgressPanel.Name = "ProgressPanel";
            this.ProgressPanel.Size = new System.Drawing.Size(227, 23);
            this.ProgressPanel.TabIndex = 7;
            // 
            // StartEndPanel
            // 
            this.StartEndPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartEndPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StartEndPanel.Controls.Add(this.StepEndRefLabel);
            this.StartEndPanel.Controls.Add(this.StepStartRefLabel);
            this.StartEndPanel.Controls.Add(this.StartEndSplitter);
            this.StartEndPanel.Location = new System.Drawing.Point(126, 7);
            this.StartEndPanel.Name = "StartEndPanel";
            this.StartEndPanel.Size = new System.Drawing.Size(112, 20);
            this.StartEndPanel.TabIndex = 8;
            // 
            // StartEndSplitter
            // 
            this.StartEndSplitter.Location = new System.Drawing.Point(0, 0);
            this.StartEndSplitter.Name = "StartEndSplitter";
            this.StartEndSplitter.Size = new System.Drawing.Size(56, 18);
            this.StartEndSplitter.TabIndex = 0;
            this.StartEndSplitter.TabStop = false;
            // 
            // IntervalTimerLabel
            // 
            this.IntervalTimerLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.IntervalTimerLabel.AutoSize = true;
            this.IntervalTimerLabel.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IntervalTimerLabel.Location = new System.Drawing.Point(13, 36);
            this.IntervalTimerLabel.Name = "IntervalTimerLabel";
            this.IntervalTimerLabel.Size = new System.Drawing.Size(64, 17);
            this.IntervalTimerLabel.TabIndex = 9;
            this.IntervalTimerLabel.Text = "00:00.0";
            // 
            // IntervalLabel
            // 
            this.IntervalLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.IntervalLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.IntervalLabel.Location = new System.Drawing.Point(13, 53);
            this.IntervalLabel.Name = "IntervalLabel";
            this.IntervalLabel.Size = new System.Drawing.Size(65, 13);
            this.IntervalLabel.TabIndex = 10;
            this.IntervalLabel.Text = "Intvl: 30 min";
            // 
            // IntervalTimer
            // 
            this.IntervalTimer.Tick += new System.EventHandler(this.IntervalTimer_Tick);
            // 
            // RefreshTimer
            // 
            this.RefreshTimer.Interval = 10;
            this.RefreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
            // 
            // StepControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.IntervalLabel);
            this.Controls.Add(this.IntervalTimerLabel);
            this.Controls.Add(this.StartEndPanel);
            this.Controls.Add(this.ProgressPanel);
            this.Controls.Add(this.PhaseLabel);
            this.Controls.Add(this.StepFlowLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(250, 107);
            this.Name = "StepControl";
            this.Size = new System.Drawing.Size(248, 106);
            this.Load += new System.EventHandler(this.StepControl_Load);
            this.ProgressPanel.ResumeLayout(false);
            this.StartEndPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel StepFlowLayoutPanel;
        private System.Windows.Forms.Label PhaseLabel;
        private System.Windows.Forms.ProgressBar StepProgressBar;
        private System.Windows.Forms.Label StepStartRefLabel;
        private System.Windows.Forms.Label StepEndRefLabel;
        private System.Windows.Forms.Timer StepTimer;
        private System.Windows.Forms.Label StepElapsedTimeLabel;
        private System.Windows.Forms.Label StepRemainingTimeLabel;
        private System.Windows.Forms.Panel ProgressPanel;
        private System.Windows.Forms.Panel StartEndPanel;
        private System.Windows.Forms.Splitter StartEndSplitter;
        private System.Windows.Forms.Label IntervalTimerLabel;
        private System.Windows.Forms.Label IntervalLabel;
        private System.Windows.Forms.Timer IntervalTimer;
        private System.Windows.Forms.Timer RefreshTimer;
    }
}
