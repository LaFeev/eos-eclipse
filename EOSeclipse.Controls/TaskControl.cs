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
    public partial class TaskControl : UserControl
    {
        public double Av { get; set; }
        public string Tv { get; set; }
        public int ISO { get; set; }
        public int AEB { get; set; }
        public string Script { get; set; }

        public TaskControl()
        {
            InitializeComponent();
        }
    }
}
