using EOSDigital.API;
using EOSDigital.SDK;
using EOSeclipse.Controls;
using GMap.NET.MapProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace EDSDKLib.API.Helper
{
    [Serializable]
    public class StepComposer
    {
        public string Version {  get; set; }
        public List<StepBuilder> StepList { get; set; }
        public SeIndex Eclipse { get; set; }
        public double ShootingLat { get; set; }
        public double ShootingLng { get; set; }
        public double ShootingElv { get; set; }
        public SeDateTime C1 { get; set; }
        public SeDateTime C2 { get; set; }
        public SeDateTime C3 { get; set; }
        public SeDateTime C4 { get; set; }
        public SeDateTime Mx { get; set; }
        public string SolarFilterIP { get; set; }
        public string FocuserIP { get; set; }

        [field: NonSerialized]
        public event EventHandler SequenceUpdated;
        [field: NonSerialized]
        public event EventHandler CircumstancesRequested;

        List<string> references = new List<string> { "C1", "C2", "Mx", "C3", "C4" };


        public StepComposer()
        {
            Version = "";
            StepList = new List<StepBuilder>();
            ShootingLat = 0;
            ShootingLng = 0;
            ShootingElv = -9999;
            Eclipse = null;
        }

        public void AddStep(string phase, string startRef, TimeSpan startOffset, string endRef, TimeSpan endOffset, TimeSpan interval, string script, List<CameraValue> tv, CameraValue av, CameraValue iso, AEBValue aeb, List<CameraValue> aebPlus, List<CameraValue> aebMinus)
        {
            // need to request for updated circumstances to be injected
            CircumstancesRequested?.Invoke(this, new EventArgs());

            StepBuilder stepBuilder = new StepBuilder();
            // create the taskbuilder list
            if (phase == "Script")
            {
                TaskBuilder task = new TaskBuilder();
                task.Script = script;
                stepBuilder.TaskList.Add(task);
            }
            else
            {
                //foreach (CameraValue tv in tvs)
                for (int i = 0; i < tv.Count(); i++)
                {
                    TaskBuilder task = new TaskBuilder();
                    task.Tv = tv[i];
                    task.ISO = iso;
                    task.Av = av;
                    task.AEB = aeb;
                    task.AEBMinus = aebMinus[i];
                    task.AEBPlus = aebPlus[i];
                    // add task to step
                    stepBuilder.TaskList.Add(task);
                }
            }

            // set the remainder of the StepBuilder properties
            stepBuilder.Phase = phase;
            stepBuilder.StartRef = startRef;
            stepBuilder.StartOffset = startOffset;
            stepBuilder.StartDateTime = GetDateTime(startRef, startOffset);
            stepBuilder.EndRef = endRef;
            stepBuilder.EndOffset = endOffset;
            stepBuilder.EndDateTime = GetDateTime(endRef, endOffset);
            stepBuilder.Interval = interval;

            // add the step to StepList
            if (StepList.Count() == 0)
            {
                StepList.Add(stepBuilder);
                UpdateSequence();
                return;
            }
            else
            {
                for (int i = 0; i < StepList.Count; i++)
                {
                    if (StepList[i].StartDateTime < stepBuilder.StartDateTime)
                    {
                        if (StepList[i].EndDateTime > stepBuilder.StartDateTime)
                        {
                            // overlapping times, drop the new sb in behind the overlapping step
                            StepList.Insert(i + 1, stepBuilder);
                            UpdateSequence();
                            return;
                        }
                    }
                    else
                    {
                        // the first step that is equal to or later than the new sb's start time
                        StepList.Insert(i, stepBuilder);
                        UpdateSequence();
                        return;
                    }
                }
                StepList.Add(stepBuilder);
                UpdateSequence();
            }
        }

        public void AddStep(string phase, string startRef, TimeSpan startOffset, string endRef, TimeSpan endOffset, TimeSpan interval, string script, List<CameraValue> tv, CameraValue av, CameraValue iso)
        {
            // build a default aeb list.  The TaskControl takes values of TvValues.Auto to mean AEB is disabled
            List<CameraValue> aeblist = new List<CameraValue>();
            for (int i = 0; i < tv.Count(); i++) aeblist.Add(TvValues.Auto);

            AddStep(phase, startRef, startOffset, endRef, endOffset, interval, script, tv, av, iso, null, aeblist, aeblist);
        }

        public void Refresh()
        {
            // called by the Refresh Button event handler in Main Form.
            // Need to get updated circumstances, location, then update the
            //  datetime properties in each StepBuilder herein, then notify
            //  Main Form that the sequence has been updated.

            // need to request for updated circumstances to be injected
            CircumstancesRequested?.Invoke(this, new EventArgs());

            // check for and remove any steps for phases that are no longer present
            PurgePhases();

            // update datetime properties
            UpdateDateTimes();

            // update (fix as needed) the sequence
            UpdateSequence();

            return;
        }

        public void ReloadSequence()
        {
            // assumes no changes were made internal or external to composer, just triggering
            // the chain of events to populate the sequence panel
            SequenceUpdated?.Invoke(this, new EventArgs());

            return;
        }

        private void UpdateSequence()
        {
            // scan thru the steplist and trim/split any overlapping steps. Higher tier phases get priority when trimming,
            //  the exception being any phases with repeat set to "single" (they cannot be shortened, by definition).
            // Tier 1: C1, C2, C3, C4, Max Eclipse
            // Tier 2: Baily's Beads, Other
            // Tier 3: Partial, Totality
            List<string> tier1 = new List<string> { "C1", "C2", "C3", "C4", "Max Eclipse" };
            List<string> tier2 = new List<string> { "Baily's Beads", "Other" };
            List<string> tier3 = new List<string> { "Partial", "Totality"};

            if (StepList.Count() <= 1)
            {
                // no overlapping possible with 0-1 steps, so skip the processing and just trigger the event
                SequenceUpdated?.Invoke(this, new EventArgs());
                return;
            }
            // first scan the list for any phases that don't belong based on the circumstances
            PurgePhases();

            // now handle any overlapping events
            for (int i = 0; i < StepList.Count - 1; i++)
            {
                if (StepList[i].EndDateTime > StepList[i + 1].StartDateTime)
                {
                    // overlap found
                    if (StepList[i].EndDateTime > StepList[i + 1].EndDateTime)
                    {
                        // split
                        StepBuilder split = StepList[i].Clone();
                        StepList[i].EndDateTime = StepList[i + 1].StartDateTime;
                        split.StartDateTime = StepList[i + 1].EndDateTime;
                        StepList.Insert(i + 2, split);
                        UpdateSequence();
                        return;
                    }
                    else
                    {
                        // trim
                        int first, second;
                        if (StepList[i].Interval == new TimeSpan(-99, -99, -99)) first = 0;
                        else if (tier1.Contains(StepList[i].Phase)) first = 1;
                        else if (tier2.Contains(StepList[i].Phase)) first = 2;
                        else first = 3;
                        if (StepList[i+1].Interval == new TimeSpan(-99, -99, -99)) second = 0;
                        else if (tier1.Contains(StepList[i+1].Phase)) second = 1;
                        else if (tier2.Contains(StepList[i+1].Phase)) second = 2;
                        else second = 3;

                        
                        if (first == 0 & second == 0)
                        {
                            // both steps are "single" repeat, they must have same start time, nudge one back by 0.5s
                            StepList[i + 1].StartDateTime += new TimeSpan(0, 0, 0, 0, 500);
                            StepList[i + 1].EndDateTime += new TimeSpan(0, 0, 0, 0, 500);
                        }
                        else if (first > second) 
                        {
                            // SB @ i+1 has priority, trim SB @ i
                            StepList[i].EndDateTime = StepList[i+1].StartDateTime;
                        }
                        else
                        {
                            // SB @ i has priority (wins in a tie), trim SB @ i+1
                            StepList[i + 1].StartDateTime = StepList[i].EndDateTime;
                        }
                        UpdateSequence();
                        return;
                    }
                }
            }
            // following the possible moves/splits we need to verify no 0-time events  (that aren't "single" repeat,
            // and recompute references and offsets
            for (int i = 0; i < StepList.Count(); i++)
            {
                if (StepList[i].StartDateTime == StepList[i].EndDateTime && 
                    StepList[i].Interval != new TimeSpan(-99, -99, -99)) 
                { 
                    this.DeleteStep(i); 
                }
                else
                {
                    StepList[i].StartRef = GetReference(StepList[i].StartDateTime);
                    StepList[i].EndRef = GetReference(StepList[i].EndDateTime);
                    StepList[i].StartOffset = GetOffset(StepList[i].StartRef, StepList[i].StartDateTime);
                    StepList[i].EndOffset = GetOffset(StepList[i].EndRef, StepList[i].EndDateTime);
                }
            }

            // finally, trigger the 'updated' event
            SequenceUpdated?.Invoke(this, new EventArgs());

        }

        private void UpdateDateTimes()
        {
            if (StepList.Count > 0)
            {
                foreach (StepBuilder step in StepList)
                {
                    step.StartDateTime = GetDateTime(step.StartRef, step.StartOffset);
                    step.EndDateTime = GetDateTime(step.EndRef, step.EndOffset);
                    //if (step.Interval == new TimeSpan(-99, -99, -99))
                    //{
                    //    Console.WriteLine("setting endtime for a Single-interval step");
                    //    step.EndDateTime += new TimeSpan(0, 0, 0, 0, 200);
                    //}
                }
            }
        }

        private DateTime GetDateTime(string startRef, TimeSpan startOffset)
        {
            SeDateTime refTime = (SeDateTime)typeof(StepComposer).GetProperty(startRef).GetValue(this);
            if (refTime.IsNull)
            {
                throw new Exception("attempting to compute a stage start/end time based on a null DateTime.  The StepComposer should have been updated prior to this point.");
            }
            return refTime.ComputeValue + startOffset;
        }

        public void DeleteStep(int stepIdx, bool skipUpdate=false)
        {
            if (StepList.Count() > stepIdx)
            {
                //// need to request for updated circumstances to be injected
                //CircumstancesRequested?.Invoke(this, new EventArgs());

                StepList.RemoveAt(stepIdx);
                
                StepBuilder left = StepList.ElementAtOrDefault(stepIdx - 1);
                StepBuilder right = StepList.ElementAtOrDefault(stepIdx);

                if (left != null && right != null)
                {
                    if (left == right)
                    {
                        // the steps on either side of the deleted step belong to the same phase
                        // (they had been previously split), let's merge them
                        left.EndDateTime = right.EndDateTime;
                        left.EndRef = right.EndRef;
                        left.EndOffset = right.EndOffset;
                        DeleteStep(stepIdx);
                        return;
                    }
                }

                if (!skipUpdate)
                {
                    UpdateSequence();
                    return;
                }
            }
            else
            {
                throw new Exception("index out of range for StepList");
            }
        }

        private string GetReference(DateTime eventTime)
        {
            List<TimeSpan> deltas = new List<TimeSpan>();
            foreach (string reference in references)
            {
                SeDateTime refTime = (SeDateTime)typeof(StepComposer).GetProperty(reference).GetValue(this);
                deltas.Add((eventTime - refTime.ComputeValue).Duration());
            }
            return references[deltas.IndexOf(deltas.Min())];
        }

        private TimeSpan GetOffset(string eventRef, DateTime eventTime)
        {
            TimeSpan offset;
            switch (eventRef)
            {
                case "C1":
                    offset = eventTime - C1.ComputeValue; break;
                case "C2":
                    offset = eventTime - C2.ComputeValue; break;
                case "C3":
                    offset = eventTime - C3.ComputeValue; break;
                case "C4":
                    offset = eventTime - C4.ComputeValue; break;
                case "Mx":
                    offset = eventTime - Mx.ComputeValue; break;
                default:
                    throw new Exception("event reference must be one of ['C1', 'C2', 'Mx', 'C3', 'C4']");
            }
            return offset;
        }

        public List<StepControl> GetStepControls()
        {
            List<StepControl> steps = new List<StepControl>();
            if (StepList == null) return steps;

            for (int i = 0; i < StepList.Count(); i++)
            {
                steps.Add(StepList[i].BuildControl());
            }

            return steps;
        }

        private List<string> GetPhases()
        {
            // evaluates the circumstances currently stored in the StepComposer and returns a list of valid phases
            // ask for updated circumstances
            CircumstancesRequested?.Invoke(this, new EventArgs());

            int circSum = Convert.ToInt32(!C1.IsNull) + 
                Convert.ToInt32(!C2.IsNull) + 
                Convert.ToInt32(!Mx.IsNull) +
                Convert.ToInt32(!C3.IsNull) +
                Convert.ToInt32(!C4.IsNull);
            List<string> phases;

            // TODO: need to add eclipse Type as a property to StepComposer, and then add phases for annular eclipses

            if (circSum == 1)
            {
                // no visual eclipse
                phases = new List<string>();
            }
            else if (circSum == 3) 
            {
                // partial eclipse
                phases = new List<string> { "Partial", "C1", "C4", "Script", "Other" };
            }
            else if (circSum == 5)
            {
                // total eclipse
                phases = new List<string> { "Partial", "Baily's Beads", "Totality", "Max Eclipse", "C1", "C2", "C3", "C4", "Script", "Other" };
            }
            else
            {
                // not valid
                throw new Exception();
            }
            return phases;
        }

        public List<string> GetRemainingPhases()
        {
            // evaluates all the StepBuilders in StepList and determines which phases remain as
            // unassigned (e.g. for populating the phase combobox on the sequence generator tab)
            List<string> remainingPhases = GetPhases();
            bool c2bb = false;
            bool c3bb = false;

            if (ShootingElv != -9999)
            {
                foreach (string ph in GetPhases())
                {
                    if (ph != "Script" && ph != "Other")
                    {
                        foreach (StepBuilder sb in StepList)
                        {
                            if (sb.Phase == ph)
                            {
                                if (sb.Phase == "Baily's Beads")
                                {
                                    if (sb.StartRef == "C2") c2bb = true;
                                    if (sb.StartRef == "C3") c3bb = true;

                                    if (c2bb && c3bb) remainingPhases.Remove(ph); break;
                                }
                                else
                                {
                                    remainingPhases.Remove(ph);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return remainingPhases;
        }

        private void PurgePhases()
        {
            // will purge steps with dead phases but also any steps that have start or end times that
            // are 'dead'.  This is heavy-handed, and maybe later can be scaled back with a more eloquent
            // solution to reassign start/end references to an in-bound ref...
            List<string> goodCircs = new List<string>();
            if (!C1.IsNull) goodCircs.Add("C1");
            if (!C2.IsNull) goodCircs.Add("C2");
            if (!C3.IsNull) goodCircs.Add("C3");
            if (!C4.IsNull) goodCircs.Add("C4");
            if (!Mx.IsNull) goodCircs.Add("Mx");
            List<string> goodPhases = GetPhases();
            List<int> idxs = new List<int>();
            for (int i = 0; i < StepList.Count(); i++)
            {
                if (!goodPhases.Contains(StepList[i].Phase) ||
                    !goodCircs.Contains(StepList[i].StartRef) ||
                    !goodCircs.Contains(StepList[i].EndRef))
                {
                    idxs.Add(i);
                }
            }
            while (idxs.Count > 0)
            {
                DeleteStep(idxs.Max(), true);
                idxs.Remove(idxs.Max());
            }
        }

        public int StepListCount()
        {
            return StepList.Count();
        }

        public bool IsEventHandlerRegistered(EventHandler handler)
        {
            if (this.SequenceUpdated != null)
            {
                Console.WriteLine("EventHandler not null");
                foreach (Delegate existingHandler in this.SequenceUpdated.GetInvocationList())
                {
                    if (Delegate.Equals(existingHandler,handler)) return true;
                }
            }
            return false;
        }
    }


    [Serializable]
    public class StepBuilder : IEquatable<StepBuilder>
    {
        public string Phase { get; set; }
        public string StartRef { get; set; }
        public TimeSpan StartOffset { get; set; }
        public string EndRef { get; set; }
        public TimeSpan EndOffset { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public TimeSpan Interval { get; set; }
        public List<TaskBuilder> TaskList { get; set; }

        public StepBuilder()
        {
            Phase = string.Empty;
            StartRef = string.Empty;
            EndRef = string.Empty;
            StartOffset = TimeSpan.Zero;
            EndOffset = TimeSpan.Zero;
            Interval = TimeSpan.Zero;
            StartDateTime = DateTime.MinValue;
            EndDateTime = DateTime.MaxValue;
            TaskList = new List<TaskBuilder>();
        }

        public StepBuilder Clone()
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            var jsonWriter = new JsonTextWriter(streamWriter);

            var serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, this);
            jsonWriter.Flush();
            streamWriter.Flush();
            stream.Seek(0, System.IO.SeekOrigin.Begin);

            var streamReader = new StreamReader(stream);
            var jsonReader = new JsonTextReader(streamReader);
            StepBuilder newSb = serializer.Deserialize<StepBuilder>(jsonReader);
            streamReader.Close();
            jsonReader.Close();
            stream.Dispose();
            return newSb;
        }

        internal StepControl BuildControl()
        {
            StepControl step = new StepControl();
            step.Phase = Phase;
            step.StartRef = StartRef;
            step.EndRef = EndRef;
            step.StartOffset = StartOffset;
            step.EndOffset = EndOffset;
            step.Interval = Interval;
            step.StartDateTime = StartDateTime;
            step.EndDateTime = EndDateTime;
            foreach (TaskBuilder task in TaskList)
            {
                step.AddTask(task.BuildControl());
            }
            return step;
        }

        #region Equality Override
        public override bool Equals(object obj) => this.Equals(obj as StepBuilder);

        public bool Equals(StepBuilder sb)
        {
            if (sb is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, sb))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != sb.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            if (TaskList.Count() == sb.TaskList.Count())
            {
                for (int i = 0; i < TaskList.Count(); i++)
                {
                    if (TaskList[i] != sb.TaskList[i])
                    {
                        return false;
                    }
                }
                return (Phase == sb.Phase) && (StartRef == sb.StartRef) && (Interval == sb.Interval);
            }
            else return false;
        }

        public override int GetHashCode() => (Phase, StartRef, Interval, TaskList).GetHashCode();

        public static bool operator ==(StepBuilder lhs, StepBuilder rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(StepBuilder lhs, StepBuilder rhs) => !(lhs == rhs);
        #endregion
    }

    [Serializable]
    public class TaskBuilder : IEquatable<TaskBuilder>
    {
        public string Script { get; set; }
        public CameraValue Tv {  get; set; }
        public CameraValue Av { get; set; }
        public CameraValue ISO { get; set;}
        public AEBValue AEB { get; set; }
        public CameraValue AEBPlus { get; set; }
        public CameraValue AEBMinus { get; set; }

        internal TaskControl BuildControl()
        {
            TaskControl task = new TaskControl();
            task.Script = Script;
            task.Tv = Tv;
            task.Av = Av;
            task.ISO = ISO;
            task.AEB = AEB;
            task.AEBPlus = AEBPlus;
            task.AEBMinus = AEBMinus;
            return task;
        }

        #region Equality override
        public override bool Equals(object obj) => this.Equals(obj as TaskBuilder);

        public bool Equals(TaskBuilder tb)
        {
            if (tb is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, tb))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != tb.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return (Script == tb.Script) && 
                    (Tv == tb.Tv) && (Av == tb.Av) &&
                    (ISO == tb.ISO) && (AEB == tb.AEB) &&
                    (AEBPlus == tb.AEBPlus) && (AEBMinus == tb.AEBMinus);
        }

        public override int GetHashCode() => (Script, Tv, Av, ISO, AEB, AEBPlus, AEBMinus).GetHashCode();

        public static bool operator ==(TaskBuilder lhs, TaskBuilder rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(TaskBuilder lhs, TaskBuilder rhs) => !(lhs == rhs);
        #endregion
    }
}
