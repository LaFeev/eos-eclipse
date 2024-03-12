using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EOSDigital.API;

namespace EOSeclipse.Controls
{
    [Serializable]
    public partial class TaskControl : UserControl
    {
        public CameraValue Av { get; set; }
        public CameraValue Tv { get; set; }
        public CameraValue ISO { get; set; }
        public CameraValue AEBMinus { get; set; }
        public CameraValue AEBPlus { get; set; }
        public string Script { get; set; }
        public AEBValue AEB { get; set; }

        public TaskControl()
        {
            InitializeComponent();
        }

        private void TaskControl_Refresh()
        {
            if (Script != null)
            {
                ScriptVisible(true);
                ScriptLabel.Text = Script;
            }
            else
            {
                ScriptVisible(false);
                TvLabel.Text = Tv.StringValue;
                IsoLabel.Text = ISO.DoubleValue.ToString();
                AvLabel.Text = Av.StringValue;
                AEBMinusLabel.Text = AEBMinus.StringValue;
                AEBPlusLabel.Text = AEBPlus.StringValue;
                if (AEBMinus.StringValue == TvValues.Auto.StringValue)
                {
                    // signals that AEB is disabled
                    AEBMinusLabel.Visible = false;
                    AEBPlusLabel.Visible = false;
                }               
            }
            

        }

        private void ScriptVisible(bool toggle)
        {
            if (toggle)
            {
                TvLabel.Visible = false;
                IsoLabel.Visible = false;
                AvLabel.Visible = false;
                AEBMinusLabel.Visible = false;
                AEBPlusLabel.Visible = false;
                label1.Visible = false;   // the word "ISO"
                ScriptLabel.Visible = true;
            }
            else
            {
                TvLabel.Visible = true;
                IsoLabel.Visible = true;
                AvLabel.Visible = true;
                AEBMinusLabel.Visible = true;
                AEBPlusLabel.Visible = true;
                ScriptLabel.Visible = false;
            }
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            TaskControl_Refresh();
        }
    }
}
