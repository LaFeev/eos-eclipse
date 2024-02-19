using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using EOSDigital.API;
using EOSDigital.SDK;
using System.Threading;
using GMap.NET;
using GMap.NET.WindowsForms;
using EOSeclipse.Controls;
using System.Linq;
using GMap.NET.MapProviders;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices.ComTypes;
using System.IO;
using EDSDKLib.API.Helper;

namespace MainForm
{
    public partial class MainForm : Form
    {
        #region Variables

        CanonAPI APIHandler;
        Camera MainCamera;
        CameraValue[] AvList;
        CameraValue[] TvList;
        CameraValue[] ISOList;
        List<AEBValue> AEBList;
        List<SeIndex> SeList;
        List<Camera> CamList;
        bool IsInit = false;
        Bitmap Evf_Bmp;
        int LVBw, LVBh, w, h;
        float LVBratio, LVration;

        int ErrCount;
        object ErrLock = new object();
        object LvLock = new object();
        PointLatLng ShootingLocation;
        Double Elevation;
        GMapOverlay markersOverlay = new GMapOverlay("markers");

        List<StepControl> StepList = new List<StepControl>();
        private StepControl _editStep;

        string AppDataDir;
        public CalcRaw calcRaw = new CalcRaw();
        public CalcResult SeResult;

        #endregion

        public MainForm()
        {
            try
            {
                InitializeComponent();
                APIHandler = new CanonAPI();
                APIHandler.CameraAdded += APIHandler_CameraAdded;
                ErrorHandler.SevereErrorHappened += ErrorHandler_SevereErrorHappened;
                ErrorHandler.NonSevereErrorHappened += ErrorHandler_NonSevereErrorHappened;
                SavePathTextBox.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "RemotePhoto");
                SaveFolderBrowser.Description = "Save Images To...";
                LiveViewPicBox.Paint += LiveViewPicBox_Paint;
                LVBw = LiveViewPicBox.Width;
                LVBh = LiveViewPicBox.Height;
                RefreshCamera();
                IsInit = true;
                SaveSettingsButton.Enabled = false;
                LoadedCameraSettingsLabel.Visible = false;

                // Location tab
                gmap.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
                gmap.Dock = DockStyle.Fill;
                gmap.DragButton = MouseButtons.Middle;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
                gmap.ShowCenter = false;
                gmap.MinZoom = 1;
                gmap.MaxZoom = 20;
                gmap.Zoom = 3;

                // Eclipse tab
                SeList = SeIndex.SeIndices();
                SeIndexComboBox.Items.Clear();
                for (int i = 0; i < SeList.Count; i++) { SeIndexComboBox.Items.Add(SeList[i].StringValue); }
                WattsLinkLabel.Enabled = false;

                // Sequence Panel
                splitContainer1.Panel2.Controls.Add( gmap );
                splitContainer2.Panel2Collapsed = true;

                // TODO: load cached session (stage, sequence, location, etc) instead of clearing everything
                ResetStage();
                ClearSequence();
                ScriptTextBox.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Eclipse Scripts");
                LoadScriptBrowserOLD.Description = "Load Script...";

                // load html content
                string curDir = Directory.GetCurrentDirectory();
                SeWebBrowser.Url = new Uri(String.Format("file:///{0}/HTMLcontent/SeCalc.html", curDir));
                SeWebBrowser.ObjectForScripting = calcRaw;

                // create an app data directory if it doesn't already exist
                var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                AppDataDir = Path.Combine(directory, @"EOSeclipse");
                try
                {
                    // Determine whether the directory exists.
                    if (Directory.Exists(AppDataDir))
                    {
                        Console.WriteLine("EOSeclipse app data directory already exists.");
                        return;
                    }

                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(AppDataDir);
                    Console.WriteLine("App data directory was created successfully at {0}.", Directory.GetCreationTime(AppDataDir));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Directory creation process failed: {0}", e.ToString());
                }
            }
            catch (DllNotFoundException) { ReportError("Canon DLLs not found!", true); }
            catch (Exception ex) { ReportError(ex.Message, true); }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                IsInit = false;
                MainCamera?.Dispose();
                APIHandler?.Dispose();
            }
            catch (Exception ex) { ReportError(ex.Message, false); }

            // TODO: add a dialog to confirm form closing a la:
            // https://stackoverflow.com/a/9231770/22854232
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            //
        }


        #region API Events

        private void APIHandler_CameraAdded(CanonAPI sender)
        {
            try { Invoke((Action)delegate { RefreshCamera(); }); }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void MainCamera_StateChanged(Camera sender, StateEventID eventID, int parameter)
        {
            try { if (eventID == StateEventID.Shutdown && IsInit) { Invoke((Action)delegate { CloseSession(); }); } }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }
        
        private void MainCamera_ProgressChanged(object sender, int progress)
        {
            try { Invoke((Action)delegate { MainProgressBar.Value = progress; }); }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void MainCamera_LiveViewUpdated(Camera sender, Stream img)
        {
            try
            {
                lock (LvLock)
                {
                    Evf_Bmp?.Dispose();
                    Evf_Bmp = new Bitmap(img);
                }
                LiveViewPicBox.Invalidate();
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void MainCamera_DownloadReady(Camera sender, DownloadInfo Info)
        {
            try
            {
                string dir = null;
                Invoke((Action)delegate { dir = SavePathTextBox.Text; });
                sender.DownloadFile(Info, dir);
                Invoke((Action)delegate { MainProgressBar.Value = 0; });
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void ErrorHandler_NonSevereErrorHappened(object sender, ErrorCode ex)
        {
            ReportError($"SDK Error code: {ex} ({((int)ex).ToString("X")})", false);
        }

        private void ErrorHandler_SevereErrorHappened(object sender, Exception ex)
        {
            ReportError(ex.Message, true);
        }

        #endregion

        #region Session

        private void SessionButton_Click(object sender, EventArgs e)
        {
            if (MainCamera?.SessionOpen == true) CloseSession();
            else OpenSession();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            try { RefreshCamera(); }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        #endregion

        #region Settings

        private void TakePhotoButton_Click(object sender, EventArgs e)
        {
            try
            {
                if ((string)TvCoBox.SelectedItem == "Bulb") MainCamera.TakePhotoBulbAsync((int)BulbUpDo.Value);
                else MainCamera.TakePhotoShutterAsync();
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private async void TakeNPhotoButton_Click(object sender, EventArgs e)
        {
            String s1 = "1/20 (1/3)";
            String s2 = "1/15";

            // cycle thru the 6 shot sequence 4 times to stress test the buffer
            Console.WriteLine("start sequence");
            var timer = System.Diagnostics.Stopwatch.StartNew();
            var lapTimer = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 8; i++)
            {
                // take a burst of 3 bracketed images, change the base shutter speed and take another 3 shot burst
                if (i > 0) Console.WriteLine("burst dT: {0}s", lapTimer.Elapsed.ToString("ss'.'fff"));
                Console.WriteLine("== burst {0} ==", i);
                lapTimer.Restart();
                Console.WriteLine("setting Tv: {0}", s1);
                MainCamera.SetSettingCont(PropertyID.Tv, TvValues.GetValue(s1).IntValue);
                MainCamera.TakePhotoShutterCont(500);
                Console.WriteLine("setting Tv: {0}", s2);
                MainCamera.SetSettingCont(PropertyID.Tv, TvValues.GetValue(s2).IntValue);
                MainCamera.TakePhotoShutterCont(500);
            }
            lapTimer.Stop();
            timer.Stop();

            Console.WriteLine("burst dT: {0}s", lapTimer.Elapsed.ToString("ss'.'fff"));
            Console.WriteLine("end sequence");
            Console.WriteLine("dT = {0}s", timer.Elapsed.ToString("ss'.'fff"));
        }

        private void RecordVideoButton_Click(object sender, EventArgs e)
        {
            try
            {
                Recording state = (Recording)MainCamera.GetInt32Setting(PropertyID.Record);
                if (state != Recording.On)
                {
                    MainCamera.StartFilming(true);
                    RecordVideoButton.Text = "Stop Video";
                }
                else
                {
                    bool save = STComputerRdButton.Checked || STBothRdButton.Checked;
                    MainCamera.StopFilming(save);
                    RecordVideoButton.Text = "Record Video";
                }
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(SavePathTextBox.Text)) SaveFolderBrowser.SelectedPath = SavePathTextBox.Text;
                if (SaveFolderBrowser.ShowDialog() == DialogResult.OK)
                {
                    SavePathTextBox.Text = SaveFolderBrowser.SelectedPath;
                }
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void AvCoBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (AvCoBox.SelectedIndex < 0) return;
                MainCamera.SetSetting(PropertyID.Av, AvValues.GetValue((string)AvCoBox.SelectedItem).IntValue);
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void TvCoBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (TvCoBox.SelectedIndex < 0) return;

                MainCamera.SetSetting(PropertyID.Tv, TvValues.GetValue((string)TvCoBox.SelectedItem).IntValue);
                BulbUpDo.Enabled = (string)TvCoBox.SelectedItem == "Bulb";
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void ISOCoBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ISOCoBox.SelectedIndex < 0) return;
                MainCamera.SetSetting(PropertyID.ISO, ISOValues.GetValue((string)ISOCoBox.SelectedItem).IntValue);
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void SaveToRdButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsInit)
                {
                    if (STCameraRdButton.Checked)
                    {
                        MainCamera.SetSetting(PropertyID.SaveTo, (int)SaveTo.Camera);
                        SaveBrowseButton.Enabled = false;
                        SavePathTextBox.Enabled = false;
                    }
                    else
                    {
                        if (STComputerRdButton.Checked) MainCamera.SetSetting(PropertyID.SaveTo, (int)SaveTo.Host);
                        else if (STBothRdButton.Checked) MainCamera.SetSetting(PropertyID.SaveTo, (int)SaveTo.Both);

                        MainCamera.SetCapacity(4096, int.MaxValue);
                        SaveBrowseButton.Enabled = true;
                        SavePathTextBox.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        #endregion

        #region Location
        private void SetLocButton_Click(object sender, EventArgs e)
        {
            // Pull the lat/lon from the text fields and add the marker
            // TODO: need to validate the input
            ShootingLocation = new PointLatLng(Convert.ToDouble(LatTextBox.Text), Convert.ToDouble(LonTextBox.Text));
            if (AltTextBox.Text != String.Empty) Elevation = Convert.ToDouble(AltTextBox.Text);
            else Elevation = 0;

            var marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(ShootingLocation,
              GMap.NET.WindowsForms.Markers.GMarkerGoogleType.purple_small);

            markersOverlay.Markers.Clear();
            markersOverlay.Markers.Add(marker);
            gmap.Overlays.Add(markersOverlay);
            gmap.Position = ShootingLocation;

            LatLabel.Text = ShootingLocation.Lat.ToString();
            LonLabel.Text = ShootingLocation.Lng.ToString();
            ElvLabel.Text = Elevation.ToString();
            LatLabel.Visible = true;
            LonLabel.Visible = true;
            ElvLabel.Visible = true;
        }

        private void gmap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var point = gmap.FromLocalToLatLng(e.X, e.Y);
                double lat = point.Lat;
                double lon = point.Lng;

                LatTextBox.Text = lat.ToString();
                LonTextBox.Text = lon.ToString();
            }
        }

        private void GetGPSButton_Click(object sender, EventArgs e)
        {
            String gpsLatRef = MainCamera.GetStringSetting(PropertyID.GPSLatitudeRef, 0, new List<String> {ErrorCode.PROPERTIES_UNAVAILABLE.ToString()});
            if (gpsLatRef == ErrorCode.PROPERTIES_UNAVAILABLE.ToString())
            {
                GPSStatusTextBox.Text = "GPS unavailable";
                GPSStatusTextBox.Visible = true;
            }
            else
            {
                String gpsLonRef = MainCamera.GetStringSetting(PropertyID.GPSLatitudeRef);
                String gpsDateStamp = MainCamera.GetStringSetting(PropertyID.GPSDateStamp);
                Rational[] gpsLat = MainCamera.GetRationalArrSetting(PropertyID.GPSLatitude);
                Rational[] gpsLon = MainCamera.GetRationalArrSetting(PropertyID.GPSLongitude);
                Rational[] gpsTimeStamp = MainCamera.GetRationalArrSetting(PropertyID.GPSTimeStamp);
                uint[] gpsAlt = MainCamera.GetUInt32ArrSetting(PropertyID.GPSAltitude);
                int gpsAltRef = MainCamera.GetUInt16Setting(PropertyID.GPSAltitudeRef);

                //TODO: actually parse the returned values and insert in text boxes (not a priority until Canon adds GPS support in SDK)
                GPSStatusTextBox.Text = "GPS found";
                GPSStatusTextBox.Visible = true;
            }
        }

        #endregion

        #region Sequence gen

        private void PhaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PhaseComboBox.SelectedItem != null)
            {
                IntervalGroupBox.Enabled = true;
                SeqSettingsPanel.Enabled = true;
                SingleRadioButton.Enabled = true;
                ScriptGroupBox.Enabled = false;

                StartRefComboBox.Items.Clear();
                StartOffsetUpDown.Value = 0;
                StartRefComboBox.SelectedIndex = -1;
                StartRefComboBox.Enabled = true;
                StartOffsetUpDown.Enabled = true;
                EndRefComboBox.Items.Clear();
                EndOffsetUpDown.Value = 0;
                EndRefComboBox.SelectedIndex = -1;
                EndRefComboBox.Enabled = false;
                EndOffsetUpDown.Enabled = false;

                switch (PhaseComboBox.SelectedItem.ToString())
                {
                    case "Partial":
                        if (SingleRadioButton.Checked)
                        {
                            IntervalRadioButton.Checked = true;
                        }
                        StartRefComboBox.Items.AddRange(new string[] { "C1", "C2" });
                        EndRefComboBox.Items.AddRange(new string[] { "C3", "C4" });

                        StartRefComboBox.SelectedItem = "C1";
                        EndRefComboBox.SelectedItem = "C4";

                        EndRefComboBox.Enabled = true;
                        EndOffsetUpDown.Enabled = true;
                        SingleRadioButton.Enabled = false;
                        break;
                    case "Baily's Beads":
                        if (SingleRadioButton.Checked)
                        {
                            IntervalRadioButton.Checked = true;
                        }
                        SingleRadioButton.Enabled = false;

                        StartRefComboBox.Items.AddRange(new string[] { "C2", "C3" });
                        EndRefComboBox.Items.AddRange(new string[] { "C2", "C3" });

                        EndRefComboBox.Enabled = false;
                        EndOffsetUpDown.Enabled = true;

                        // TODO: need to test this change.  Add both BB stages, and then try to edit one.
                        if (AddStageButton.Text == "Add Stage")
                        {
                            // if one of the two baily's beads stages has been configured, remove it from the start/end reference options
                            foreach (StepControl step in StepList)
                            {
                                if (step.Phase == "Baily's Beads")
                                {
                                    StartRefComboBox.Items.Remove(step.StartRef);
                                    EndRefComboBox.Items.Remove(step.EndRef);
                                }
                            }
                            StartRefComboBox.Enabled = true;
                            StartRefComboBox.SelectedIndex = 0;
                            EndRefComboBox.SelectedIndex = 0;
                        }
                        else
                        {
                            StartRefComboBox.Enabled = false;
                        }
                        break;
                    case "Totality":
                        if (SingleRadioButton.Checked)
                        {
                            IntervalRadioButton.Checked = true;
                        }
                        StartRefComboBox.Items.AddRange(new string[] { "C2" });
                        EndRefComboBox.Items.AddRange(new string[] { "C3" });

                        StartRefComboBox.SelectedItem = "C2";
                        EndRefComboBox.SelectedItem = "C3";
                        SingleRadioButton.Enabled = false;

                        // set default offsets and allow editing
                        StartRefComboBox.Enabled = false;
                        EndRefComboBox.Enabled = false;
                        EndOffsetUpDown.Enabled = true;
                        StartOffsetUpDown.Value = 10;
                        EndOffsetUpDown.Value = -10;

                        // if either baily's beads events are already configured, use their start/end offsets to define totality and disable edit
                        foreach (StepControl step in StepList)
                        {
                            if (step.Phase == "Baily's Beads" && step.StartRef == "C2")
                            {
                                StartOffsetUpDown.Enabled = false;
                                StartOffsetUpDown.Value = (decimal)step.EndOffset.TotalSeconds;
                            }
                            else if (step.Phase == "Baily's Beads" && step.StartRef == "C3")
                            {
                                EndOffsetUpDown.Enabled = false;
                                EndOffsetUpDown.Value = (decimal)step.StartOffset.TotalSeconds;
                            }
                        }
                        break;
                    case "Max Eclipse":
                        StartRefComboBox.Items.AddRange(new string[] { "Mx" });
                        EndRefComboBox.Items.AddRange(new string[] { "Mx" });

                        StartRefComboBox.SelectedItem = "Mx";
                        EndRefComboBox.SelectedItem = "Mx";

                        StartRefComboBox.Enabled = false;
                        EndOffsetUpDown.Enabled = true;

                        StartOffsetUpDown.Value = 10;
                        EndOffsetUpDown.Value = -10;
                        break;
                    case "C1":
                        StartRefComboBox.Items.AddRange(new string[] { "C1" });
                        EndRefComboBox.Items.AddRange(new string[] { "C1" });

                        StartRefComboBox.SelectedItem = "C1";
                        EndRefComboBox.SelectedItem = "C1";

                        StartRefComboBox.Enabled = false;
                        EndOffsetUpDown.Enabled = true;

                        StartOffsetUpDown.Value = 1;
                        EndOffsetUpDown.Value = -1;
                    break;
                    case "C2":
                        StartRefComboBox.Items.AddRange(new string[] { "C2" });
                        EndRefComboBox.Items.AddRange(new string[] { "C2" });

                        StartRefComboBox.SelectedItem = "C2";
                        EndRefComboBox.SelectedItem = "C2";

                        StartRefComboBox.Enabled = false;
                        EndOffsetUpDown.Enabled = true;

                        StartOffsetUpDown.Value = 1;
                        EndOffsetUpDown.Value = -1;
                    break;
                    case "C3":
                        StartRefComboBox.Items.AddRange(new string[] { "C3" });
                        EndRefComboBox.Items.AddRange(new string[] { "C3" });

                        StartRefComboBox.SelectedItem = "C3";
                        EndRefComboBox.SelectedItem = "C3";

                        StartRefComboBox.Enabled = false;
                        EndOffsetUpDown.Enabled = true;

                        StartOffsetUpDown.Value = 1;
                        EndOffsetUpDown.Value = -1;
                    break;
                    case "C4":
                        StartRefComboBox.Items.AddRange(new string[] { "C4" });
                        EndRefComboBox.Items.AddRange(new string[] { "C4" });

                        StartRefComboBox.SelectedItem = "C4";
                        EndRefComboBox.SelectedItem = "C4";

                        StartRefComboBox.Enabled = false;
                        EndOffsetUpDown.Enabled = true;

                        StartOffsetUpDown.Value = 1;
                        EndOffsetUpDown.Value = -1;
                    break;
                    case "Script":
                        StartRefComboBox.Items.AddRange(new string[] { "C1", "C2", "Mx", "C3", "C4" });
                        EndRefComboBox.Items.AddRange(new string[] { "C1", "C2", "Mx", "C3", "C4" });

                        StartRefComboBox.SelectedIndex = -1;
                        EndRefComboBox.SelectedIndex = -1;

                        if (SingleRadioButton.Checked)
                        {
                            EndRefComboBox.Enabled = false;
                        }
                        else { EndRefComboBox.Enabled = true; }
                        EndOffsetUpDown.Enabled = true;

                        StartOffsetUpDown.Value = 0;
                        EndOffsetUpDown.Value = 0;

                        SeqSettingsPanel.Enabled = false;
                        ScriptGroupBox.Enabled = true;
                    break;
                    default:
                        StartRefComboBox.Items.AddRange(new string[] { "C1", "C2", "Mx", "C3", "C4" });
                        EndRefComboBox.Items.AddRange(new string[] { "C1", "C2", "Mx", "C3", "C4" });

                        StartRefComboBox.SelectedIndex = -1;
                        EndRefComboBox.SelectedIndex = -1;

                        if (SingleRadioButton.Checked)
                        {
                            EndRefComboBox.Enabled = false;
                        }
                        else { EndRefComboBox.Enabled = true; }
                        EndOffsetUpDown.Enabled = true;

                        StartOffsetUpDown.Value = 0;
                        EndOffsetUpDown.Value = 0;
                    break;
                }
            }
            else
            {
                // should only get here via a call to StageReset(), which should clear everything etc
                // TODO: delete this else block?
            }
        }

        private void IntervalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (IntervalRadioButton.Checked) 
            {
                IntervalMinUpDown.Enabled = true;
                IntervalSecUpDown.Enabled = true;
            }
            else
            {
                IntervalMinUpDown.Enabled = false;
                IntervalSecUpDown.Enabled = false;
            }
        }

        private void ResetStage()
        {
            PhaseComboBox.Enabled = true;
            PhaseComboBox.SelectedIndex = -1;
            StartRefComboBox.SelectedIndex = -1;
            StartRefComboBox.Items.Clear();
            StartRefComboBox.Enabled = false;
            StartOffsetUpDown.Value = 0;
            StartOffsetUpDown.Enabled = false;
            EndRefComboBox.SelectedIndex = -1;
            EndRefComboBox.Items.Clear();
            EndRefComboBox.Enabled = false;
            EndOffsetUpDown.Value = 0;
            EndOffsetUpDown.Enabled = false;
            IntervalRadioButton.Checked = true;
            IntervalMinUpDown.Value = 0;
            IntervalSecUpDown.Value = 0;
            SeqTvListBox.SelectedIndex = -1;
            SeqAvCoBox.SelectedIndex = AvCoBox.SelectedIndex;
            SeqIsoCoBox.SelectedIndex = ISOCoBox.SelectedIndex;
            AEBDisabledRadioButton.Checked = true;
            AEBUpDown.SelectedIndex = AEBUpDown.Items.Count -1;
            AEBUpDown.Enabled = false;

            IntervalGroupBox.Enabled = false;
            SeqSettingsPanel.Enabled = false;
            ScriptGroupBox.Enabled = false;

            CancelStageButton.Text = "Reset";
            AddStageButton.Text = "Add Stage";
            ClearSeqButton.Enabled = true;
        }

        private void ClearSequence()
        {
            if (StepList.Count > 0)
            {
                foreach (StepControl step in StepList)
                {
                    SeqFlowPanel.Controls.Remove(step);
                }
                StepList.Clear();
                ResetPhaseList();
            }
        }

        private void RemoveStep(StepControl step) 
        {
            if (StepList.Count > 0)
            {
                SeqFlowPanel.Controls.Remove(step);
                StepList.Remove(step);
                ResetPhaseList();
            }
        }

        private void ResetPhaseList()
        {
            PhaseComboBox.Items.Clear();
            // reset the phase combo box selection
            PhaseComboBox.SelectedIndex = -1;
            PhaseComboBox.Items.AddRange(new object[] { "Partial", "Baily's Beads", "Totality", "Max Eclipse", "C1", "C2", "C3", "C4", "Script", "Other" });
            
            // now cycle through any existing StepControls and remove, if necessary, the phase from phase list
            bool otherBead = false;
            foreach (StepControl step in StepList)
            {
                if (step.Phase == "Baily's Beads")
                {
                    if (otherBead) { PhaseComboBox.Items.Remove(step.Phase); }
                    else { otherBead = true; }
                }
                else if ((step.Phase != "Script") & (step.Phase != "Other"))
                {
                    PhaseComboBox.Items.Remove(step.Phase);
                }
            }
        }

        private void ScriptBrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(ScriptTextBox.Text)) ScriptFileBrowser.InitialDirectory = ScriptTextBox.Text;
                if (ScriptFileBrowser.ShowDialog() == DialogResult.OK)
                {
                    ScriptTextBox.Text = ScriptFileBrowser.FileName;
                }
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void CancelStageButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DialogResult confirmResult;
            if (btn.Text == "Reset")
            {
                string msg = "Are you sure you want to clear the stage inputs above?";
                string caption = "Confirm reset!";
                confirmResult = MessageBox.Show(msg, caption, MessageBoxButtons.OKCancel);
            }
            else
            {
                string msg = "Are you sure you want to cancel the stage edit?";
                string caption = "Confirm cancel!";
                confirmResult = MessageBox.Show(msg, caption, MessageBoxButtons.OKCancel);
            }

            if (confirmResult == DialogResult.OK)
            {
                ResetStage();
                ResetPhaseList();
            }
            else
            {
                // don't reset...
            }
        }

        private void AEBDisabledRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AEBDisabledRadioButton.Checked)
            {
                AEBUpDown.Enabled = false;
            }
            else
            {
                AEBUpDown.Enabled = true;
            }
        }

        private void StartRefComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StartRefComboBox.SelectedItem != null)
            {
                switch (PhaseComboBox.SelectedItem.ToString())
                {
                    case "Partial":
                        if (StartRefComboBox.SelectedItem.ToString() == "C1") { EndRefComboBox.SelectedItem = "C4"; }
                        else { EndRefComboBox.SelectedItem = "C3"; }
                    break;
                    case "Baily's Beads":
                        if (StartRefComboBox.SelectedItem.ToString() == "C2") { EndRefComboBox.SelectedItem = "C2"; }
                        else { EndRefComboBox.SelectedItem = "C3"; }
                    break;
                    case "Script":
                        EndRefComboBox.SelectedItem = StartRefComboBox.SelectedItem.ToString();
                    break;
                    case "Other":
                        if (SingleRadioButton.Checked)
                        {
                            EndRefComboBox.SelectedItem = StartRefComboBox.SelectedItem.ToString();
                        }
                    break;
                }

            }
        }

        private void SingleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SingleRadioButton.Checked)
            {
                EndRefComboBox.Enabled = false;
                EndOffsetUpDown.Enabled = false;

                if (StartRefComboBox.SelectedItem != null) 
                {
                    EndRefComboBox.SelectedItem = StartRefComboBox.SelectedItem;
                    EndOffsetUpDown.Value = StartOffsetUpDown.Value;
                }
            }
            else
            {
                EndRefComboBox.Enabled = true;
                EndOffsetUpDown.Enabled = true;
            }
        }

        private void StartOffsetUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (EndOffsetUpDown.Enabled == false)
            {
                EndOffsetUpDown.Value = StartOffsetUpDown.Value;
            }
        }

        private void AddStageButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            StepControl step;
            if (btn.Text != "Add Stage")
            {
                // grab the reference to the step which triggered the edit event
                step = _editStep;
            }
            else
            {
                // create the step
                step = new StepControl();
            }
            
            step.Phase = PhaseComboBox.SelectedItem.ToString();
            step.StartRef = StartRefComboBox.SelectedItem.ToString();
            step.StartOffset = new TimeSpan(0,0, (int)StartOffsetUpDown.Value);
            step.EndRef = EndRefComboBox.SelectedItem.ToString();
            step.EndOffset = new TimeSpan(0,0, (int)EndOffsetUpDown.Value);
            
            if (IntervalRadioButton.Checked)
            {
                step.Interval = new TimeSpan(0, (int)IntervalMinUpDown.Value, (int)IntervalSecUpDown.Value);
            }
            else if (ContinuousRadioButton.Checked) { step.Interval = TimeSpan.Zero; }
            else { step.Interval = new TimeSpan(-99,-99, -99); }
            
            // clear tasks (relevant for edit mode)
            step.ClearTasks();

            int i = 0;
            // create tasks
            if (step.Phase == "Script")
            {
                TaskControl task = new TaskControl();
                task.Script = ScriptTextBox.Text;
                step.AddTask(task);
            }
            else
            {
                foreach (string Tv in SeqTvListBox.SelectedItems)
                {
                    TaskControl task = new TaskControl();
                    task.Tv = new CameraValue(Tv, PropertyID.Tv);
                    task.ISO = new CameraValue((double)SeqIsoCoBox.SelectedItem, PropertyID.ISO);
                    task.Av = new CameraValue(SeqAvCoBox.SelectedItem.ToString(), PropertyID.Av);
                    // TODO: assign AEBminus and AEBPlus values

                    if (AEBRadioButton.Checked)
                    {
                        task.AEB = new AEBValue(AEBUpDown.SelectedItem.ToString());
                        task.AEBMinus = GetAEB(task.Tv, new AEBValue(AEBUpDown.SelectedItem.ToString()), false);
                        task.AEBPlus = GetAEB(task.Tv, new AEBValue(AEBUpDown.SelectedItem.ToString()), true);
                    }
                    else { task.AEBMinus = task.AEBPlus = TvValues.Auto; }

                    // for debug only
                    Console.WriteLine("##### task {0} ######", i);
                    Console.WriteLine("Tv: {0}\nAv: {1}\nISO: {2}\nAEB+ {3}\nAEB- {4}",
                        task.Tv.StringValue, task.Av.StringValue, task.ISO.DoubleValue.ToString(),
                        task.AEBPlus.StringValue, task.AEBMinus.StringValue);

                    // add task to step
                    step.AddTask(task);
                    i++;
                }
            }

            // if -not- in edit mode
            if (btn.Text == "Add Stage")
            {
                // add the step to the step list
                StepList.Add(step);

                // add the step to the sequence panel
                SeqFlowPanel.Controls.Add(step);

                // add the event listeners
                step.EditStage += new EventHandler(EventHandler_EditStage);
                step.DeleteStage += new EventHandler(EventHandler_DeleteStage);
            }
            else
            {
                step.EditRefresh();
            }

            // remove the phase from the phase list (if necessary)
            ResetPhaseList();
            ResetStage();
        }

        #endregion

        #region Sequence Panel

        private void ClearSeqButton_Click(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show("Are you sure you want to clear the entire sequence? This cannot be undone.", 
                "Confirm reset!", 
                MessageBoxButtons.OKCancel);

            if (confirmResult == DialogResult.OK)
            {
                ClearSequence();
            }
            else
            {
                // don't reset...
            }
        }
        
        // TODO: UserControl is not serializable, not sure how to proceed
        private void SaveSeqButton_Click(object sender, EventArgs e)
        {
            try
            {
                SequenceFileBrowser.InitialDirectory = AppDataDir;
                SequenceFileBrowser.FileName = "eclipse.sequence";
                SequenceFileBrowser.Title = "Save sequence file";
                SequenceFileBrowser.CheckFileExists = false;
                if (SequenceFileBrowser.ShowDialog() == DialogResult.OK)
                {
                    string fpath = SequenceFileBrowser.FileName;
                    Console.WriteLine(fpath);
                    // serialize the current sequence and save to disk
                    IFormatter formatter = new BinaryFormatter();
                    // debug location
                    Stream streamDebug = new FileStream("../../test.sequence", FileMode.Create, System.IO.FileAccess.Write);
                    // production location
                    Stream stream = new FileStream(fpath, FileMode.Create, System.IO.FileAccess.Write);
                    // write to debug location
                    formatter.Serialize(streamDebug, StepList);
                    streamDebug.Close();
                    // also write to production location
                    formatter.Serialize(stream, StepList);
                    stream.Close();
                }
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
            
        }

        #endregion

        #region Eclipse Panel
        private void SeCalcButton_Click(object sender, EventArgs e)
        {
            // TODO: need to parse null results

            // variables
            String language = "en";
            String lat;
            String lon;
            String alt;
            SeIndex eclipse_index;
            // make the calculations in UTC
            String tzh = "0";
            String tzm = "0";
            String tzx = "1";
            String dst = "0";

            // gather inputs
            lat = ShootingLocation.Lat.ToString();
            lon = (ShootingLocation.Lng).ToString();
            alt = Elevation.ToString();
            ComputedLatLngLabel.Text = string.Format("{0} lat, {1} lng @ {2}m Elv", lat, lon, alt);
            // Xavier's code uses "west" longitude, inverse of convention
            lon = (ShootingLocation.Lng * -1).ToString();
            eclipse_index = SeIndex.GetValue(SeIndexComboBox.SelectedItem.ToString());
            Object[] arglist = { language, lat, lon, alt, tzh, tzm, tzx, dst, eclipse_index.IntValue };

            // make the call to the javascript calculator
            SeWebBrowser.Document.InvokeScript("calc", arglist);
            SeResult = new CalcResult(calcRaw);

            // display the results to the Eclipse tab
            TypeLabel.Text = SeResult.ecltype;
            DurationLabel.Text = SeResult.duration.ToString();
            DurationCorrLabel.Text = SeResult.duration_corr.ToString();
            DeltaTLabel.Text = SeResult.deltaT.ToString() + "s";
            C1DateTimeLabel.Text = SeResult.c1_datetime.ToString();
            C2DateTimeLabel.Text = SeResult.c2_datetime.ToString();
            MxDateTimeLabel.Text = SeResult.mid_datetime.ToString();
            C3DateTimeLabel.Text = SeResult.c3_datetime.ToString();
            C4DateTimeLabel.Text = SeResult.c4_datetime.ToString();
            C1AltLabel.Text = SeResult.c1_alt.ToString() + "\xB0";
            C2AltLabel.Text = SeResult.c2_alt.ToString() + "\xB0";
            MxAltLabel.Text = SeResult.mid_alt.ToString() + "\xB0";
            C3AltLabel.Text = SeResult.c3_alt.ToString() + "\xB0";
            C4AltLabel.Text = SeResult.c4_alt.ToString() + "\xB0";
            C1AziLabel.Text = SeResult.c1_azi.ToString() + "\xB0";
            C2AziLabel.Text = SeResult.c2_azi.ToString() + "\xB0";
            MxAziLabel.Text = SeResult.mid_azi.ToString() + "\xB0";
            C3AziLabel.Text = SeResult.c3_azi.ToString() + "\xB0";
            C4AziLabel.Text = SeResult.c4_azi.ToString() + "\xB0";
            C1PLabel.Text = SeResult.c1_p.ToString() + "\xB0";
            C2PLabel.Text = SeResult.c2_p.ToString() + "\xB0";
            MxPLabel.Text = SeResult.mid_p.ToString() + "\xB0";
            C3PLabel.Text = SeResult.c3_p.ToString() + "\xB0";
            C4PLabel.Text = SeResult.c4_p.ToString() + "\xB0";
            C1VLabel.Text = SeResult.c1_v.ToString();
            C2VLabel.Text = SeResult.c2_v.ToString();
            MxVLabel.Text = SeResult.mid_v.ToString();
            C3VLabel.Text = SeResult.c3_v.ToString();
            C4VLabel.Text = SeResult.c4_v.ToString();
            C2LcLabel.Text = SeResult.c2_lc.ToString() + "s";
            C3LcLabel.Text = SeResult.c3_lc.ToString() + "s";
            EclipseDepthLabel.Text = SeResult.eclipse_depth;
            DepthLabel.Text = SeResult.depth.ToString() + "%";
            CoverageLabel.Text = SeResult.coverage.ToString() + "%";
            MagnitudeLabel.Text = SeResult.mag.ToString();
            SunMoonRatioLabel.Text = SeResult.sunmoonratio.ToString();
            LibrationLLabel.Text = SeResult.libl.ToString() + "\xB0";
            LibrationBLabel.Text = SeResult.libb.ToString() + "\xB0";
            LibrationCLabel.Text = SeResult.pac.ToString() + "\xB0";
            WattsLinkLabel.Enabled = true;
        }

        private void WattsLinkLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(SeResult.watts_chart_link);
        }
        #endregion

        #region Live view

        private void LiveViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MainCamera.IsLiveViewOn) { MainCamera.StartLiveView(); LiveViewButton.Text = "Stop LV"; }
                else { MainCamera.StopLiveView(); LiveViewButton.Text = "Start LV"; }
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void LiveViewPicBox_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                LVBw = LiveViewPicBox.Width;
                LVBh = LiveViewPicBox.Height;
                LiveViewPicBox.Invalidate();
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void LiveViewPicBox_Paint(object sender, PaintEventArgs e)
        {
            if (MainCamera == null || !MainCamera.SessionOpen) return;

            if (!MainCamera.IsLiveViewOn) e.Graphics.Clear(BackColor);
            else
            {
                lock (LvLock)
                {
                    if (Evf_Bmp != null)
                    {
                        LVBratio = LVBw / (float)LVBh;
                        LVration = Evf_Bmp.Width / (float)Evf_Bmp.Height;
                        if (LVBratio < LVration)
                        {
                            w = LVBw;
                            h = (int)(LVBw / LVration);
                        }
                        else
                        {
                            w = (int)(LVBh * LVration);
                            h = LVBh;
                        }
                        e.Graphics.DrawImage(Evf_Bmp, 0, 0, w, h);
                    }
                }
            }
        }

        private void FocusNear3Button_Click(object sender, EventArgs e)
        {
            try { MainCamera.SendCommand(CameraCommand.DriveLensEvf, (int)DriveLens.Near3); }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void FocusNear2Button_Click(object sender, EventArgs e)
        {
            try { MainCamera.SendCommand(CameraCommand.DriveLensEvf, (int)DriveLens.Near2); }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void FocusNear1Button_Click(object sender, EventArgs e)
        {
            try { MainCamera.SendCommand(CameraCommand.DriveLensEvf, (int)DriveLens.Near1); }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void FocusFar1Button_Click(object sender, EventArgs e)
        {
            try { MainCamera.SendCommand(CameraCommand.DriveLensEvf, (int)DriveLens.Far1); }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void FocusFar2Button_Click(object sender, EventArgs e)
        {
            try { MainCamera.SendCommand(CameraCommand.DriveLensEvf, (int)DriveLens.Far2); }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        private void FocusFar3Button_Click(object sender, EventArgs e)
        {
            try { MainCamera.SendCommand(CameraCommand.DriveLensEvf, (int)DriveLens.Far3); }
            catch (Exception ex) { ReportError(ex.Message, false); }
        }

        #endregion

        #region Subroutines

        private void CloseSession()
        {
            MainCamera.CloseSession();
            AvCoBox.Items.Clear();
            TvCoBox.Items.Clear();
            ISOCoBox.Items.Clear();
            AEBUpDown.Items.Clear();
            PhaseComboBox.Items.Clear();
            SeqTvListBox.Items.Clear();
            SeqAvCoBox.Items.Clear();
            SeqIsoCoBox.Items.Clear();
            SettingsTabPage.Enabled = false;
            LiveViewGroupBox.Enabled = false;
            GetGPSButton.Enabled = false;
            SessionButton.Text = "Open Session";
            SessionLabel.Text = "No open session";
            LiveViewButton.Text = "Start LV";
            LoadSettingsButton.Enabled = true;
            SaveSettingsButton.Enabled = false;
        }

        private void ExpandButton_Click(object sender, EventArgs e)
        {
            if (splitContainer2.Panel2Collapsed == true)
            {
                splitContainer2.Panel2Collapsed = false;
                ExpandButton.Text = ">";
            }
            else
            {
                splitContainer2.Panel2Collapsed = true;
                ExpandButton.Text = "<";
            }
        }

        private void RefreshCamera()
        {
            CameraListBox.Items.Clear();
            CamList = APIHandler.GetCameraList();
            foreach (Camera cam in CamList) CameraListBox.Items.Add(cam.DeviceName);
            if (MainCamera?.SessionOpen == true) CameraListBox.SelectedIndex = CamList.FindIndex(t => t.ID == MainCamera.ID);
            else if (CamList.Count > 0) CameraListBox.SelectedIndex = 0;
        }

        private void OpenSession()
        {
            if (CameraListBox.SelectedIndex >= 0)
            {
                MainCamera = CamList[CameraListBox.SelectedIndex];
                MainCamera.OpenSession();
                MainCamera.LiveViewUpdated += MainCamera_LiveViewUpdated;
                MainCamera.ProgressChanged += MainCamera_ProgressChanged;
                MainCamera.StateChanged += MainCamera_StateChanged;
                MainCamera.DownloadReady += MainCamera_DownloadReady;

                SessionButton.Text = "Close Session";
                SessionLabel.Text = MainCamera.DeviceName;
                AvList = MainCamera.GetSettingsList(PropertyID.Av);
                TvList = MainCamera.GetSettingsList(PropertyID.Tv);
                ISOList = MainCamera.GetSettingsList(PropertyID.ISO);
                AEBList = AEBValue.AEBValues();
                SeqTvListBox.Items.Clear();
                SeqAvCoBox.Items.Clear();
                SeqIsoCoBox.Items.Clear();
                AEBUpDown.Items.Clear();
                foreach (var Av in AvList) { AvCoBox.Items.Add(Av.StringValue); SeqAvCoBox.Items.Add(Av.StringValue); }
                foreach (var Tv in TvList) { TvCoBox.Items.Add(Tv.StringValue); SeqTvListBox.Items.Add(Tv.StringValue); }
                foreach (var ISO in ISOList) { ISOCoBox.Items.Add(ISO.StringValue); SeqIsoCoBox.Items.Add(ISO.DoubleValue); }
                for (int i = AEBList.Count - 1; i >= 0; i--) { AEBUpDown.Items.Add(AEBList[i].StringValue); }
                ResetPhaseList();
                LoadSettingsButton.Enabled = false;
                SaveSettingsButton.Enabled = true;
                LoadedCameraSettingsLabel.Visible = false;
                AvCoBox.SelectedIndex = AvCoBox.Items.IndexOf(AvValues.GetValue(MainCamera.GetInt32Setting(PropertyID.Av)).StringValue);
                TvCoBox.SelectedIndex = TvCoBox.Items.IndexOf(TvValues.GetValue(MainCamera.GetInt32Setting(PropertyID.Tv)).StringValue);
                ISOCoBox.SelectedIndex = ISOCoBox.Items.IndexOf(ISOValues.GetValue(MainCamera.GetInt32Setting(PropertyID.ISO)).StringValue);
                SeqAvCoBox.SelectedIndex = AvCoBox.SelectedIndex;
                SeqIsoCoBox.SelectedIndex = ISOCoBox.SelectedIndex;
                SettingsTabPage.Enabled = true;
                LiveViewGroupBox.Enabled = true;
                GetGPSButton.Enabled = true;
            }
        }


        private void ReportError(string message, bool lockdown)
        {
            int errc;
            lock (ErrLock) { errc = ++ErrCount; }

            if (lockdown) EnableUI(false);

            if (errc < 4) MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (errc == 4) MessageBox.Show("Many errors happened!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            lock (ErrLock) { ErrCount--; }
        }

        private void EnableUI(bool enable)
        {
            if (InvokeRequired) Invoke((Action)delegate { EnableUI(enable); });
            else
            {
                SettingsTabPage.Enabled = enable;
                InitGroupBox.Enabled = enable;
                LiveViewGroupBox.Enabled = enable;
                GetGPSButton.Enabled = enable;
            }
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            // serialize the current Tv, Av, and ISO settings and save to file.
            string fname = MainCamera.DeviceName.Replace(" ", "_");
            fname += ".settings";
            Console.WriteLine(fname);
            IFormatter formatter = new BinaryFormatter();
            // debug location
            Stream streamDebug = new FileStream("../../test.settings", FileMode.Create, System.IO.FileAccess.Write);
            // production location
            Stream stream = new FileStream(Path.Combine(AppDataDir, fname), FileMode.Create, System.IO.FileAccess.Write);

            List<CameraValue> valueList = AvList.ToList<CameraValue>();
            Console.WriteLine(valueList.Count);
            valueList.AddRange(TvList);
            Console.WriteLine(valueList.Count);
            valueList.AddRange(ISOList);
            Console.WriteLine(valueList.Count);
            // write to debug location
            formatter.Serialize(streamDebug, valueList);
            streamDebug.Close();
            // also write to production location
            formatter.Serialize(stream, valueList);
            stream.Close();
        }

        // TODO: change default filename in designer
        private void LoadSettingsButton_Click(object sender, EventArgs e)
        {
            // get the settings file
            String SettingsPath;
            try
            {
                if (File.Exists(AppDataDir)) SettingsFileBrowser.InitialDirectory = AppDataDir;
                if (SettingsFileBrowser.ShowDialog() == DialogResult.OK)
                {
                    SettingsPath = SettingsFileBrowser.FileName;
                    // deserialize the settings and populate the lists
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(SettingsPath, FileMode.Open, System.IO.FileAccess.Read);
                    List<CameraValue> valueList = (List<CameraValue>)formatter.Deserialize(stream);
                    Console.WriteLine(valueList.Count);
                    foreach (CameraValue val in valueList)
                    {
                        if (val.ValueType == PropertyID.Av) { SeqAvCoBox.Items.Add(val.StringValue); }
                        else if (val.ValueType == PropertyID.ISO) { SeqIsoCoBox.Items.Add(val.DoubleValue); }
                        else if (val.ValueType == PropertyID.Tv) { SeqTvListBox.Items.Add(val.StringValue); }
                        else { Console.WriteLine("ValueType mismatch!"); }
                    }
                    ResetPhaseList();
                    String cameraname = Path.GetFileNameWithoutExtension(SettingsPath);
                    cameraname = cameraname.Replace("_", " ");
                    LoadedCameraSettingsLabel.Visible = true;
                    LoadedCameraSettingsLabel.Text = cameraname;
                    AEBList = AEBValue.AEBValues();
                    for (int i = AEBList.Count - 1; i >= 0; i--) { AEBUpDown.Items.Add(AEBList[i].StringValue); }
                }
            }
            catch (Exception ex) { ReportError(ex.Message, false); }
            
        }

        private void SeqFlowPanel_Resize(object sender, EventArgs e)
        {
            // the children within SeqFlowPanel will get automatically resized to the width of the widest member, but resizing
            // the first StepControl was not working.  The SeqSizer panel is a zero-height panel that is resized to follow the
            // SeqFlowPanel, and the subsequent StepControls will follow suit.
            int offset = 15;
            if (SeqFlowPanel.VerticalScroll.Visible) { offset = 32; }
                
            SeqSizerPanel.Width = SeqFlowPanel.Width - offset;
        }

        private CameraValue GetAEB(CameraValue baseTv, AEBValue AEB, bool plus)
        {
            double offset = (double)AEB.IntValue;

            // if "plus" is false, get the "minus" bracket
            if (!plus)
            {
                offset *= -1;
            }

            double target = baseTv.DoubleValue * Math.Pow(2, offset / 3);  // computes the precise shutter speed, but cameras list nominal values
            Console.WriteLine("offset: {0}\nbaseTv: {1}\ntarget: {2}", offset.ToString(), baseTv.DoubleValue.ToString(), target.ToString());

            // TODO: TvList is null if settings were loaded, change this reference
            for (int i = 0; i < TvList.Count(); i++)
            {
                double high = TvList[i].DoubleValue;
                
                // on first item, if target is higher, return first item
                if (i == 0 & target >= high) { return TvList[i]; }
                // on last item, no other choice, return last item
                if (i == TvList.Count() - 1) { return TvList[i]; }

                // only try this if not on last item
                double low = TvList[i + 1].DoubleValue;

                // assumes TvList is sorted longest to shortest
                if (target >= low)
                {
                    if (Math.Abs(target - high) < Math.Abs(target - low)) { return TvList[i]; }
                    else { return TvList[i+1]; }
                }
            }
            // shouldn't get here without returning
            return TvValues.Invalid;
        }

        private void EventHandler_EditStage(object sender, EventArgs e)
        {
            StepControl step = (StepControl)sender;
            Console.WriteLine("Edit Step: {0}", StepList.IndexOf(step));

            SettingsTabControl.SelectedTab = SeqGenTabPage;
            PhaseComboBox.Items.Clear();
            PhaseComboBox.Items.Add(step.Phase);
            PhaseComboBox.SelectedIndex = 0;
            PhaseComboBox.Enabled = false;
            ClearSeqButton.Enabled = false;
            CancelStageButton.Text = "Cancel";
            AddStageButton.Text = "Save Changes";

            StartRefComboBox.SelectedItem = step.StartRef;
            EndRefComboBox.SelectedItem = step.EndRef;
            StartOffsetUpDown.Value = (decimal)step.StartOffset.TotalSeconds;
            EndOffsetUpDown.Value = (decimal)(step.EndOffset.TotalSeconds);

            if (step.Interval < TimeSpan.Zero) { SingleRadioButton.Checked = true; }
            else if (step.Interval == TimeSpan.Zero) { ContinuousRadioButton.Checked = true; }
            else
            {
                IntervalRadioButton.Checked = true;
                IntervalMinUpDown.Value = step.Interval.Minutes;
                IntervalSecUpDown.Value = step.Interval.Seconds;
            }

            // now we need to populate the task data
            List<TaskControl> tasks = step.GetTasks();
            foreach (TaskControl task in tasks)
            {
                if (task.Script != null)
                {
                    ScriptTextBox.Text = task.Script;
                }
                else
                {
                    SeqTvListBox.SelectedItems.Add(task.Tv.StringValue);
                    SeqAvCoBox.SelectedItem = task.Av.StringValue;
                    SeqIsoCoBox.SelectedItem = task.ISO.DoubleValue;
                    if (task.AEBMinus == TvValues.Auto)
                    {
                        AEBDisabledRadioButton.Checked = true;
                    }
                    else
                    {
                        AEBRadioButton.Checked = true;
                        AEBUpDown.SelectedItem = task.AEB.StringValue;
                    }
                }
            }

            // TODO: handle the script field

            // save the index of this step so it can be written to later
            _editStep = step;
        }

        private void EventHandler_DeleteStage(object sender, EventArgs e)
        {
            StepControl step = (StepControl)sender;
            DialogResult confirmResult = MessageBox.Show("Are you sure you want to delete this stage? This cannot be undone.", 
                "Confirm delete!", 
                MessageBoxButtons.OKCancel);

            if (confirmResult == DialogResult.OK)
            {
                RemoveStep(step);
            }
            else
            {
                // don't reset...
            }
        }

        #endregion        
    }
}
