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
using System.Globalization;

namespace EOSeclipse.Controls
{
    [Serializable]
    public partial class StepControl: UserControl//, INotifyPropertyChanged
    {
        private bool _active { get; set; }
        private bool _finished { get; set; }
        public TimeSpan Interval { get; set; }
        private DateTime _intervalStartTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string StartRef { get; set; }
        public string EndRef { get; set; }
        public TimeSpan StartOffset { get; set; }
        public TimeSpan EndOffset { get; set; }
        public string Phase { get; set; }
        private List<TaskControl> Tasks { get; set; }
        private int _heightLow = 113;
        private int _heightHigh = 133;


        public StepControl()
        {
            InitializeComponent();
            this.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
        }

        private void StepControl_Load(object sender, EventArgs e)
        {
            StepControl_Refresh();

            // labels for debugging times
            startDtLabel.Text = StartDateTime.ToString();
            endDtLabel.Text = EndDateTime.ToString();
            startDtLabel.Visible = false;
            endDtLabel.Visible = false;
        }


        #region events

        public event EventHandler EditStage;

        public event EventHandler DeleteStage;

        public event EventHandler<TaskFiredEventArgs> TaskFired;

        #endregion

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
        public void Start(bool forceStart=false)
        {
            // only 'start' if we are within the step start/end times
            if (CheckInStep() || forceStart)
            {
                _active = true;
                _finished = false;

                // make sure step is running (will also ToggleEnabled)
                RunStep();

                if (Interval > TimeSpan.Zero)
                {
                    IntervalTimer.Interval = (int)Interval.TotalMilliseconds;
                    IntervalTimer.Start();
                }
                // Fire one task at the start (will be the only firing for single/continuous steps)
                IntervalTimer_Tick(this, new EventArgs());
            }
            
        }

        public void Stop()
        {
            _active = false;
            IntervalTimer.Stop();
            PartialRefresh_IntervalTimerLabel();
            ToggleEnabled();
        }

        public bool IsActive()
        {
            return _active;
        }
        public bool IsFinished()
        {
            return _finished;
        }

        public bool IsRunning()
        {
            return StepTimer.Enabled;
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
            _finished = true;
        }

        public void AddTask(TaskControl task)
        {
            if (Tasks == null)
            {
                Tasks = new List<TaskControl>();
            }
            Tasks.Add(task);
            StepFlowLayoutPanel.Controls.Add(task);

            AdjustStepControlHeight();
        }

        public void AdjustStepControlHeight()
        {
            int padding = 50;
            int containedWidth = 0;
            foreach (TaskControl task in Tasks)
            {
                containedWidth += task.Width;
            }
            if (containedWidth >= StepFlowLayoutPanel.Width - padding) 
            {
                this.Height = _heightHigh;
            }
            else
            {
                this.Height = _heightLow;
            }
        }

        public void ClearTasks() 
        {
            if (Tasks != null) 
            {
                foreach (TaskControl task in Tasks)
                {
                    StepFlowLayoutPanel.Controls.Remove(task);
                }
                Tasks.Clear();

                // reduce the height of the control since there are no tasks present (for now)
                this.Height = _heightLow;
            }
        }

        public List<TaskControl> GetTasks()
        {
            return Tasks;
        }

        public void EditRefresh()
        {
            StepControl_Refresh();
        }

        #endregion

        #region Subroutines
        private void ToggleEnabled()
        {
            if(InvokeRequired)
            {
                Invoke(new Action(() => 
                {
                    PhaseLabel.Enabled = _active;
                    StartEndPanel.Enabled = _active;
                    IntervalTimerLabel.Enabled = _active;
                    IntervalLabel.Enabled = _active;
                    ProgressPanel.Enabled = CheckInStep();
                }));
                
            }
            
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
                ts = Interval - (DateTime.Now - _intervalStartTime);
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
            //Console.WriteLine("Checking InStep {3}\nNow: {0}\nStart: {1}\nEnd: {2}", DateTime.Now.ToString(), StartDateTime.ToString(), EndDateTime.ToString(), Phase);
            if (DateTime.Now >= StartDateTime && DateTime.Now < EndDateTime) return true;
            return false;
        }

        private int GetCurrentProgress()
        {
            if (CheckInStep())
            {
                int inc = GetStepIncrement();
                double elapsed = (DateTime.Now - StartDateTime).TotalMilliseconds;
                return (int)(elapsed / inc) ;
            }
            else if (DateTime.Now < StartDateTime)
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
                ts = DateTime.Now - StartDateTime; 
            }
            else if (DateTime.Now < StartDateTime)
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
                ts = EndDateTime - DateTime.Now;
            }
            else if (DateTime.Now >= EndDateTime)
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
            _intervalStartTime = DateTime.Now;

            // Execute the Task series here
            // raise a TaskFired event with payload
            TaskFiredEventArgs args = new TaskFiredEventArgs();
            args.IntervalStartTime = _intervalStartTime;
            if (Interval < TimeSpan.Zero)
            {
                // single fire
                args.Repeat = false;
                args.IntervalEndTime = EndDateTime;
            }
            else if (Interval == TimeSpan.Zero)
            {
                // continuous
                args.Repeat = true;
                args.IntervalEndTime = EndDateTime;
            }
            else
            {
                // interval
                args.Repeat = false;
                args.IntervalEndTime = _intervalStartTime + Interval;
            }
            // get longest task length (should be the first task, since default Tv sorting puts longest exposure lengths first)
            if (Phase == "Script")
                args.LongestTask = TimeSpan.Zero;
            else
            {
                double millis = 1000 * (Tasks[0].Tv.DoubleValue + Tasks[0].AEBMinus.DoubleValue + Tasks[0].AEBPlus.DoubleValue);
                args.LongestTask = new TimeSpan(0,0,0,0,(int)millis);
            }

            EventHandler<TaskFiredEventArgs> handler = TaskFired;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void EditStageMenuItem_Click(object sender, EventArgs e)
        {
            // raise an EditStage event
            if (EditStage != null)
            {
                EditStage(this, new EventArgs());
            }
                
        }

        private void DeleteStageMenuItem_Click(object sender, EventArgs e)
        {
            // raise a DeleteStage event
            if (DeleteStage != null)
            {
                DeleteStage(this, new EventArgs());
            }
        }

        public TimeSpan GetStepTime()
        {
            return TimeSpan.ParseExact(StepElapsedTimeLabel.Text, "mm\\:ss\\.f", CultureInfo.InvariantCulture);
        }

        #endregion
    }

    public class TaskFiredEventArgs : EventArgs
    {
        public DateTime IntervalStartTime { get; set; }
        public DateTime IntervalEndTime { get; set; }
        public TimeSpan LongestTask { get; set; }
        public bool Repeat { get; set; }
    }
}
