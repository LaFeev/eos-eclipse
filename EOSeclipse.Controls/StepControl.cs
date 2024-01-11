using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EOSeclipse.Controls;

namespace EOSeclipse.Controls
{
    public partial class StepControl: UserControl//, INotifyPropertyChanged
    {
        private bool _active { get; set; }
        public TimeSpan Interval { get; set; }
        private DateTime _intervalStartTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string StartRef { get; set; }
        public string EndRef { get; set; }
        public TimeSpan StartOffset { get; set; }
        public TimeSpan EndOffset { get; set; }
        //public string Script { get; set; }      // should this be a property of Task?
        public string Phase { get; set; }
        private List<TaskControl> Tasks { get; set; }
        // the following properties should move to a different class for "Task"
        //public List<double> Av { get; set; }
        //public List<string> Tv { get; set; }
        //public List<int> ISO { get; set; }
        //public int AEB { get; set; }

        public StepControl()
        {
            InitializeComponent();
            this.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
        }

        private void StepControl_Load(object sender, EventArgs e)
        {
            StepControl_Refresh();
        }

        #region Refresh
        private void StepControl_Refresh()
        {
            // Must be called anytime a property is updated

            if (!CheckInStep() && StepTimer.Enabled)
            {
                // if the Step properties change while it is InStep, and the new properties force it out of step,
                // then this will force the timers to shutdown without having to wait for the next tick of StepTimer
                FinishStep();
            }
            else
            {
                // If start/end time or Interval are changed while InStep, both Start() (if _active) and RunStep() need to be
                // fired to ensure the IntervalTimer and StepTimer intervals are updated
                if (_active) Start();

                RunStep();
            }

            PartialRefresh_PhaseLabel();
            PartialRefresh_StartRefLabel();
            PartialRefresh_EndRefLabel();
            PartialRefresh_IntervalLabel();
            PartialRefresh_IntervalTimerLabel();
            PartialRefresh_ProgressBar();
            PartialRefresh_ProgressTimes();
            ToggleEnabled();
        }

        private void PartialRefresh_PhaseLabel()
        {
            PhaseLabel.Text = Phase;
        }

        private void PartialRefresh_StartRefLabel()
        {
            StepStartRefLabel.Text = BuildTimeRef(StartRef, StartOffset);
        }

        private void PartialRefresh_EndRefLabel()
        {
            StepEndRefLabel.Text = BuildTimeRef(EndRef, EndOffset);
        }

        private void PartialRefresh_IntervalLabel()
        {
            IntervalLabel.Text = GetIntervalText(Interval);
        }

        private void PartialRefresh_IntervalTimerLabel()
        {
            IntervalTimerLabel.Text = GetIntervalTimerText();
        }

        private void PartialRefresh_ProgressBar()
        {
            // called if any of the related properties of this Step instance are changed, which could put us in the middle of the Step
            StepProgressBar.Value = GetCurrentProgress();
        }

        private void PartialRefresh_ProgressTimes()
        {
            StepElapsedTimeLabel.Text = GetElapsedTimeText();
            StepRemainingTimeLabel.Text = GetRemainingTimeText();
        }

        #endregion

        #region Controls
        public void Start()
        {
            // only 'start' if we are within the step start/end times
            if (CheckInStep())
            {
                _active = true;
                ToggleEnabled();

                if (Interval > TimeSpan.Zero)
                {
                    IntervalTimer.Interval = (int)Interval.TotalMilliseconds;
                    IntervalTimer.Start();
                }

            }
            
        }

        public void Stop()
        {
            _active = false;
            IntervalTimer.Stop();
            PartialRefresh_IntervalTimerLabel();
            ToggleEnabled();
        }

        public void RunStep()
        {
            // the step "runs" whenever real time is within the start/end times of the step
            //  this does not mean the step is "active" though, which is user-triggered with Start()
            if (CheckInStep())
            {
                // the step timer fires off 100 times, evenly spaced, thru the duration of the Step - it updates the progress bar
                StepTimer.Interval = GetStepIncrement();
                StepTimer.Start();
                // the refresh timer fires off every 100ms and is used to update the interval, elapsed, and remaining timers
                RefreshTimer.Start();
                
                ToggleEnabled();    // should enable the ProgressPanel
            }
        }

        private void FinishStep()
        {
            StepTimer.Stop();
            RefreshTimer.Stop();
            Stop();
            PartialRefresh_ProgressBar();
            PartialRefresh_ProgressTimes();
            ToggleEnabled();
        }

        public void AddTask(TaskControl task)
        {
            if (Tasks == null)
            {
                Tasks = new List<TaskControl>();
            }
            Tasks.Add(task);
            StepFlowLayoutPanel.Controls.Add(task);

            if (StepFlowLayoutPanel.HorizontalScroll.Visible)
            {
                this.Height = 133;
            }
        }

        #endregion

        #region Subroutines
        private void ToggleEnabled()
        {
            PhaseLabel.Enabled = _active;
            StartEndPanel.Enabled = _active;
            IntervalTimerLabel.Enabled = _active;
            IntervalLabel.Enabled = _active;
            ProgressPanel.Enabled = CheckInStep();
        }

        private string BuildTimeRef(string timeRef, TimeSpan timeOffset)
        {
            string offsetText;
            if (timeOffset > TimeSpan.Zero) { offsetText = "+" + timeOffset.TotalSeconds.ToString(); }
            else if (timeOffset == TimeSpan.Zero) { offsetText = "-" +  timeOffset.TotalSeconds.ToString(); }
            else {  offsetText = timeOffset.TotalSeconds.ToString(); }

            return $"{timeRef}{offsetText}";
        }

        private string GetIntervalText(TimeSpan interval)
        {
            string intervalText = string.Empty;
            string intervalMetric = string.Empty;

            if (interval < TimeSpan.Zero) intervalText = "Single";
            else if (interval == TimeSpan.Zero) intervalText = "Cont.";
            else if (interval.Days > 0 || interval.Hours > 0) { intervalText = interval.TotalHours.ToString(); intervalMetric = " hr"; }
            else if (interval.Minutes > 0) { intervalText = interval.TotalMinutes.ToString(); intervalMetric = " min"; }
            else if (interval.Seconds > 0) { intervalText = interval.TotalSeconds.ToString(); intervalMetric = " sec"; }
            else { intervalText = interval.TotalMilliseconds.ToString(); intervalMetric = " ms"; }

            return $"Intvl: {intervalText}{intervalMetric}";
        }

        private string GetIntervalTimerText()
        {
            // interval should be practically limited to something less than 60min, validated somewhere upstream
            //  therefore this method will ignore any hours/days in the TimeSpan
            // TODO: throw a warning if hours or days are encountered
            TimeSpan ts;
            
            if (Interval <= TimeSpan.Zero)
            {
                // single action or continuous (no interval)
                return "--:--.-";
            }
            else if (_active)
            {
                // interval timer is running
                ts = Interval - (DateTime.UtcNow - _intervalStartTime);
            }
            else
            {
                // interval timer is not running
                ts = Interval;
            }

            return ts.ToString("mm\\:ss\\.f");
        }

        private bool CheckInStep()
        {
            if (DateTime.UtcNow >= StartDateTime && DateTime.UtcNow < EndDateTime) return true;
            return false;
        }

        private int GetCurrentProgress()
        {
            if (CheckInStep())
            {
                int inc = GetStepIncrement();
                double elapsed = (DateTime.UtcNow - StartDateTime).TotalMilliseconds;
                return (int)(elapsed / inc) ;
            }
            else if (DateTime.UtcNow < StartDateTime)
            {
                return 0;
            }
            else
            {
                return 100;
            }
        }

        // returns the length of one step increment (1/100th) in whole milliseconds
        private int GetStepIncrement()
        {
            return (int)((EndDateTime - StartDateTime).TotalMilliseconds / 100);
        }

        private string GetElapsedTimeText()
        {
            TimeSpan ts;
            if (CheckInStep())
            {
                ts = DateTime.UtcNow - StartDateTime; 
            }
            else if (DateTime.UtcNow < StartDateTime)
            {
                ts = TimeSpan.Zero;
            }
            else
            {
                ts = EndDateTime - StartDateTime;
            }

            if (ts.Days > 0) { return ts.ToString("d\\-hh\\:mm"); }
            else if (ts.Hours > 0) { return ts.ToString("hh\\:mm\\:ss"); }
            else { return ts.ToString("mm\\:ss\\.f"); }
        }

        private string GetRemainingTimeText()
        {
            TimeSpan ts;
            if (CheckInStep())
            {
                ts = EndDateTime - DateTime.UtcNow;
            }
            else if (DateTime.UtcNow >= EndDateTime)
            {
                ts = TimeSpan.Zero;
            }
            else
            {
                ts = EndDateTime - StartDateTime;
            }

            if (ts.Days > 0) { return ts.ToString("d\\-hh\\:mm"); }
            else if (ts.Hours > 0) { return ts.ToString("hh\\:mm\\:ss"); }
            else { return ts.ToString("mm\\:ss\\.f"); }
        }

        private void StepTimer_Tick(object sender, EventArgs e)
        {
            StepProgressBar.PerformStep();
            if (!CheckInStep())
            {
                FinishStep();
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            PartialRefresh_IntervalTimerLabel();
            PartialRefresh_ProgressTimes();
        }

        private void IntervalTimer_Tick(object sender, EventArgs e)
        {
            _intervalStartTime = DateTime.UtcNow;

            // Execute the Task series here
            //
            // ...take pictures...
            //
        }

        #endregion
    }
}
