using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainForm
{
    public class Step
    {
        public TimeSpan Interval { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string StartRef { get; set; }
        public string EndRef { get; set; }
        public double StartOffset { get; set; }
        public double EndOffset { get; set; }
        public string Phase { get; set; }
        public List<double> Av {  get; set; }
        public List<string> Tv { get; set; }
        public List<int> ISO { get; set; }
        public string Script { get; set; }
        public int AEB { get; set; }

    }
}
