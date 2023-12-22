using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOSeclipse.Controls
{
    public partial class Form1 : Form
    {
        private List<StepControl> steps;

        public Form1()
        {
            InitializeComponent();
        }

        private void AddStep_Click(object sender, EventArgs e)
        {
            StepControl step = new StepControl();
            step.Phase = Phase.Text;
            step.StartRef = StartRef.Text;
            step.StartOffset = StartOffset.Value;
            step.EndRef = EndRef.Text;
            step.EndOffset = EndOffset.Value;
            step.StartDateTime = GetDateTime(step.StartRef, step.StartOffset);
            step.EndDateTime = GetDateTime(step.EndRef, step.EndOffset);

        }

        private DateTime GetDateTime(string startRef, TimeSpan startOffset)
        {
            DateTime refTime;
            TimeSpan offset = new TimeSpan(0)

            if (startRef == "C1") { refTime = C1dt.Value; }
            else if (startRef == "C2") { refTime = C2dt.Value; }
            else if (startRef == "C3") { refTime = C3dt.Value; }
            else if (startRef == "C4") { refTime = C4dt.Value; }

            return refTime; + TimeSpan(0, 0, startOffset);
        }
    }
}
