namespace EOSeclipse.Controls
{
    partial class TaskControl
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.IsoLabel = new System.Windows.Forms.Label();
            this.AvLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AEBMinusLabel = new System.Windows.Forms.Label();
            this.AEBPlusLabel = new System.Windows.Forms.Label();
            this.TvLabel = new System.Windows.Forms.Label();
            this.ScriptLabel = new System.Windows.Forms.Label();
            this.MainPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.AutoSize = true;
            this.MainPanel.BackColor = System.Drawing.Color.MediumOrchid;
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainPanel.Controls.Add(this.panel2);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(70, 46);
            this.MainPanel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.IsoLabel);
            this.panel2.Controls.Add(this.AvLabel);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.MinimumSize = new System.Drawing.Size(30, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(30, 44);
            this.panel2.TabIndex = 3;
            // 
            // IsoLabel
            // 
            this.IsoLabel.AutoSize = true;
            this.IsoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsoLabel.Location = new System.Drawing.Point(4, 13);
            this.IsoLabel.Name = "IsoLabel";
            this.IsoLabel.Size = new System.Drawing.Size(21, 9);
            this.IsoLabel.TabIndex = 0;
            this.IsoLabel.Text = "3200";
            this.IsoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AvLabel
            // 
            this.AvLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AvLabel.AutoSize = true;
            this.AvLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvLabel.Location = new System.Drawing.Point(4, 33);
            this.AvLabel.Name = "AvLabel";
            this.AvLabel.Size = new System.Drawing.Size(15, 9);
            this.AvLabel.TabIndex = 0;
            this.AvLabel.Text = "5.4";
            this.AvLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 9);
            this.label1.TabIndex = 0;
            this.label1.Text = "ISO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.ScriptLabel);
            this.panel1.Controls.Add(this.AEBMinusLabel);
            this.panel1.Controls.Add(this.AEBPlusLabel);
            this.panel1.Controls.Add(this.TvLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(30, 0);
            this.panel1.MinimumSize = new System.Drawing.Size(30, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(38, 44);
            this.panel1.TabIndex = 2;
            // 
            // AEBMinusLabel
            // 
            this.AEBMinusLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AEBMinusLabel.AutoSize = true;
            this.AEBMinusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AEBMinusLabel.Location = new System.Drawing.Point(9, 6);
            this.AEBMinusLabel.Name = "AEBMinusLabel";
            this.AEBMinusLabel.Size = new System.Drawing.Size(17, 9);
            this.AEBMinusLabel.TabIndex = 0;
            this.AEBMinusLabel.Text = "000";
            this.AEBMinusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AEBPlusLabel
            // 
            this.AEBPlusLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AEBPlusLabel.AutoSize = true;
            this.AEBPlusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AEBPlusLabel.Location = new System.Drawing.Point(9, 30);
            this.AEBPlusLabel.Name = "AEBPlusLabel";
            this.AEBPlusLabel.Size = new System.Drawing.Size(17, 9);
            this.AEBPlusLabel.TabIndex = 0;
            this.AEBPlusLabel.Text = "000";
            this.AEBPlusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TvLabel
            // 
            this.TvLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TvLabel.AutoSize = true;
            this.TvLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TvLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TvLabel.Location = new System.Drawing.Point(4, 15);
            this.TvLabel.Name = "TvLabel";
            this.TvLabel.Size = new System.Drawing.Size(28, 13);
            this.TvLabel.TabIndex = 0;
            this.TvLabel.Text = "000";
            this.TvLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ScriptLabel
            // 
            this.ScriptLabel.AutoSize = true;
            this.ScriptLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScriptLabel.Location = new System.Drawing.Point(-2, 15);
            this.ScriptLabel.Name = "ScriptLabel";
            this.ScriptLabel.Size = new System.Drawing.Size(37, 13);
            this.ScriptLabel.TabIndex = 1;
            this.ScriptLabel.Text = "script";
            this.ScriptLabel.Visible = false;
            // 
            // TaskControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.MainPanel);
            this.MinimumSize = new System.Drawing.Size(60, 46);
            this.Name = "TaskControl";
            this.Size = new System.Drawing.Size(70, 46);
            this.Load += new System.EventHandler(this.TaskControl_Load);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label TvLabel;
        private System.Windows.Forms.Label AEBMinusLabel;
        private System.Windows.Forms.Label AEBPlusLabel;
        private System.Windows.Forms.Label IsoLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ScriptLabel;
        private System.Windows.Forms.Label AvLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
