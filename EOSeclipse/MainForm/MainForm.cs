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
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Data.Entity.Infrastructure;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
        Double Elevation = -9999;
        GMapOverlay markersOverlay = new GMapOverlay("markers");

        StepComposer Composer = new StepComposer();
        private StepControl _editStep;
        private TimeSpan _averageTaskTime = new TimeSpan(0, 0, 0, 0, 700);
        private int _burstLength = 500;     // TODO: offer a way to congifure this in settings. It should equal roughly (1000/[camera fps] * 3.5).  This gives the ~average between a 3 and 4 shot burst in milliseconds.

        string AppDataDir;
        public CalcRaw calcRaw = new CalcRaw();
        public CalcResult SeResult;
        public List<CalcResult> SimResults = new List<CalcResult>();
        public List<SeIndex> SimIndices = new List<SeIndex>();
        public bool SessionIsLive = false;

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
                SeCalcButton.Enabled = false;

                // Sequence Panel
                splitContainer1.Panel2.Controls.Add( gmap );
                splitContainer2.Panel2Collapsed = true;
                RefreshSequenceButton.Visible = false;

                // Control tab
                BuildSimulations();
                MasterTimer.Start();

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
                    }
                    else
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(AppDataDir);
                        Console.WriteLine("App data directory was created successfully at {0}.", Directory.GetCreationTime(AppDataDir));
                    }
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

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // load the auto-saved session if present
            LoadSession();
            
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

        #region Camera Control
        private async Task TakeBurst(int burstDuration, TaskControl task)
        {
            // make the setting changes
            MainCamera.SetSettingCont(PropertyID.Tv, task.Tv.IntValue);
            MainCamera.SetSettingCont(PropertyID.Av, task.Av.IntValue);
            MainCamera.SetSettingCont(PropertyID.ISO, task.ISO.IntValue);
            // fire the burst
            await MainCamera.TakePhotoShutterContAsync(burstDuration);
        }

        private async Task TakePhoto(TaskControl task)
        {
            // make the setting changes
            MainCamera.SetSettingCont(PropertyID.Tv, task.Tv.IntValue);
            MainCamera.SetSettingCont(PropertyID.Av, task.Av.IntValue);
            MainCamera.SetSettingCont(PropertyID.ISO, task.ISO.IntValue);
            // fire the shot
            MainCamera.TakePhotoShutterAsync();
        }

        private async Task FireTask(TaskControl task)
        {
            if (MainCamera != null)
            {
                // responsible for determining what methods to call depending on the content of the TaskControl
                if (task.Script != null)
                {
                    // script deploy
                    Console.WriteLine("TODO: deploy script in FireTask()");
                }
                else if (task.AEBMinus != TvValues.Auto)
                {
                    // AEB mode activated
                    // TODO: check if AEB is set on camera and flag the UI if it is not
                    await TakeBurst(_burstLength, task);
                }
                else
                {
                    // assume we're taking a single shot - if another option arises this will need to be updated
                    await TakePhoto(task);
                }
            }
            else
            {
                // no camera attached, fire debug messages
                if (task.Script != null)
                    Console.WriteLine("{0} FIRE - script: {1}", DateTime.Now.ToString(), task.Script);
                else if (task.AEBMinus != TvValues.Auto)
                {
                    Console.WriteLine("{0} FIRE - burst: {1} Tv; {2} Av; {3} ISO", DateTime.Now, task.Tv.StringValue, task.Av.StringValue, task.ISO.StringValue);
                    await Task.Delay(_averageTaskTime);
                }
                else
                {
                    Console.WriteLine("{0} FIRE - shoot: {1} Tv; {2} Av; {3} ISO", DateTime.Now, task.Tv.StringValue, task.Av.StringValue, task.ISO.StringValue);
                    await Task.Delay(_averageTaskTime);
                }
            }
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

            // Invalidate the sequence panel if these location settings differ from those stored in the Composer.
            //  The user will need to hit the sequence refresh button to enable the sequence (if they want
            //  to accept this new location)
            EnableSequence(VerifyComposer());
            SaveSession();
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
                        // TODO: this doesn't work, the button text has not changed yet.  Need to rework how BB is handled anyways, remainingPhases() is not working.
                        if (AddStageButton.Text == "Add Stage")
                        {
                            // if one of the two baily's beads stages has been configured, remove it from the start/end reference options
                            foreach (StepControl step in Composer.GetStepControls())
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
                        foreach (StepControl step in Composer.GetStepControls())
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
            if (Composer.StepList.Count > 0)
            {
                for (int i = Composer.StepList.Count - 1; i >= 0; i--)
                {
                    Composer.DeleteStep(i, true);
                }
                Composer.ReloadSequence();
                ResetPhaseList();
                // save the Composer state for 'auto session'
                SaveSession();
            }
        }

        private void RemoveStep(StepControl step) 
        {
            int idx = SeqFlowPanel.Controls.IndexOf(step);
            if (idx != -1)
            {
                Composer.DeleteStep(idx);
                // save the Composer state for 'auto session'
                SaveSession();
                ResetPhaseList();
            }
        }

        private void ResetPhaseList()
        {
            PhaseComboBox.Items.Clear();
            // reset the phase combo box selection
            PhaseComboBox.SelectedIndex = -1;
            foreach (string ph in Composer.GetRemainingPhases())
            {
                PhaseComboBox.Items.Add(ph);

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

            string Phase = PhaseComboBox.SelectedItem.ToString();
            string StartRef = StartRefComboBox.SelectedItem.ToString();
            TimeSpan StartOffset = new TimeSpan(0, 0, (int)StartOffsetUpDown.Value);
            string EndRef = EndRefComboBox.SelectedItem.ToString();
            TimeSpan EndOffset = new TimeSpan(0, 0, (int)EndOffsetUpDown.Value);
            CameraValue ISO = new CameraValue((double)SeqIsoCoBox.SelectedItem, PropertyID.ISO);
            CameraValue Av = new CameraValue(SeqAvCoBox.SelectedItem.ToString(), PropertyID.Av);
            string Script = ScriptTextBox.Text;
            DateTime StartDateTime = DateTime.MinValue;
            DateTime EndDateTime = DateTime.MinValue;

            // check for existing eclipse calc results
            if (SeResult != null)
            {
                if (!SeResult.Phase(StartRef).DateTime.IsNull)
                {
                    StartDateTime = SeResult.Phase(StartRef).DateTime.ComputeValue + StartOffset;
                }
                else
                {
                    // TODO: pop up a dialog saying there is no valid event time for that phase (not happening for the location calculated)
                }
                if (!SeResult.Phase(EndRef).DateTime.IsNull)
                {
                    EndDateTime = SeResult.Phase(EndRef).DateTime.ComputeValue + EndOffset;
                }
                else
                {
                    // TODO: pop up a dialog saying there is no valid event time for that phase (not happening for the location calculated)
                }
            }
            if (EndDateTime < StartDateTime)
            {
                MessageBox.Show("The start time cannot be later than the end time ('T minus n-seconds' refers to the point in time n-seconds prior to event time T).",
                "Input warning",
                MessageBoxButtons.OK);
                return;
            }

            TimeSpan Interval;
            if (IntervalRadioButton.Checked)
            {
                Interval = new TimeSpan(0, (int)IntervalMinUpDown.Value, (int)IntervalSecUpDown.Value);
            }
            else if (ContinuousRadioButton.Checked) { Interval = TimeSpan.Zero; }
            else { Interval = new TimeSpan(-99, -99, -99); }

            AEBValue AEB = new AEBValue();
            List<CameraValue> Tvs = new List<CameraValue>();
            List<CameraValue> AEBMinus = new List<CameraValue>();
            List<CameraValue> AEBPlus = new List<CameraValue>();

            foreach (string _tv in SeqTvListBox.SelectedItems)
            {
                CameraValue Tv = new CameraValue(_tv, PropertyID.Tv);
                Tvs.Add(Tv);

                if (AEBRadioButton.Checked)
                {
                    AEB = new AEBValue(AEBUpDown.SelectedItem.ToString());
                    AEBMinus.Add(GetAEB(Tv, AEB, false));
                    AEBPlus.Add(GetAEB(Tv, AEB, true));
                }
            }
            
            // if -not- in edit mode
            if (btn.Text == "Add Stage")
            {
                // add the StepBuilder to the StepComposer
                if (AEBRadioButton.Checked)
                {
                    Composer.AddStep(Phase, StartRef, StartOffset, EndRef, EndOffset, Interval, Script, Tvs, Av, ISO, AEB, AEBPlus, AEBMinus);
                }
                else
                {
                    Composer.AddStep(Phase, StartRef, StartOffset, EndRef, EndOffset, Interval, Script, Tvs, Av, ISO);
                }
            }
            else
            {
                // edit mode, edit the StepBuilder at the affected index
                int idx = SeqFlowPanel.Controls.IndexOf(_editStep);
                if (AEBRadioButton.Checked)
                {
                    Composer.DeleteStep(idx);
                    Composer.AddStep(Phase, StartRef, StartOffset, EndRef, EndOffset, Interval, Script, Tvs, Av, ISO, AEB, AEBPlus, AEBMinus);
                }
                else
                {
                    Composer.DeleteStep(idx);
                    Composer.AddStep(Phase, StartRef, StartOffset, EndRef, EndOffset, Interval, Script, Tvs, Av, ISO);
                }
            }
            // save the Composer state for 'auto session'
            SaveSession();
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
            //
        }

        #endregion

        #region Eclipse Panel
        private void SeCalcButton_Click(object sender, EventArgs e)
        {
            if (Elevation != -9999)
            {
                List<SeIndex> indices = SeIndex.SeIndices();
                indices.AddRange(SimIndices);
                SeIndex eclipse_index = SeIndex.GetValue(SeIndexComboBox.SelectedItem.ToString(), indices);
                int maxSeIndex = SeIndex.GetMaxIndex();
                if (eclipse_index.IntValue > maxSeIndex)
                {
                    // Simulated Eclipse!
                    // recompute the sim eclipse results
                    BuildSimulations();
                    int simIdx = eclipse_index.IntValue - maxSeIndex - 1;
                    SeResult = SimResults[simIdx];
                }
                else
                {
                    // variables
                    String language = "en";
                    String lat;
                    String lon;
                    String alt;
                    // make the calculations in UTC
                    String tzh = "0";
                    String tzm = "0";
                    String tzx = "1";
                    String dst = "0";

                    // gather inputs
                    lat = ShootingLocation.Lat.ToString("0.#######");
                    lon = ShootingLocation.Lng.ToString("0.#######");
                    alt = Elevation.ToString();
                    ComputedLatLngLabel.Text = string.Format("{0} lat, {1} lng @ {2}m Elv", lat, lon, alt);
                    // Xavier's code uses "west" longitude, inverse of convention
                    lat = ShootingLocation.Lat.ToString();
                    lon = (ShootingLocation.Lng * -1).ToString();
                    Object[] arglist = { language, lat, lon, alt, tzh, tzm, tzx, dst, eclipse_index.IntValue };

                    // make the call to the javascript calculator
                    SeWebBrowser.Document.InvokeScript("calc", arglist);
                    SeResult = new CalcResult(calcRaw);
                }

                // display the results to the Eclipse tab
                TypeLabel.Text = SeResult.SeType;
                EclipseDepthLabel.Text = SeResult.EclipseDepth;
                C1DateTimeLabel.Text = SeResult.Phase("C1").DateTime.DisplayValue;
                C2DateTimeLabel.Text = SeResult.Phase("C2").DateTime.DisplayValue;
                MxDateTimeLabel.Text = SeResult.Phase("Mx").DateTime.DisplayValue;
                C3DateTimeLabel.Text = SeResult.Phase("C3").DateTime.DisplayValue;
                C4DateTimeLabel.Text = SeResult.Phase("C4").DateTime.DisplayValue;
                C1AltLabel.Text = SeResult.Phase("C1").Altitude.DisplayValue;
                C2AltLabel.Text = SeResult.Phase("C2").Altitude.DisplayValue;
                MxAltLabel.Text = SeResult.Phase("Mx").Altitude.DisplayValue;
                C3AltLabel.Text = SeResult.Phase("C3").Altitude.DisplayValue;
                C4AltLabel.Text = SeResult.Phase("C4").Altitude.DisplayValue;
                C1AziLabel.Text = SeResult.Phase("C1").Azimuth.DisplayValue;
                C2AziLabel.Text = SeResult.Phase("C2").Azimuth.DisplayValue;
                MxAziLabel.Text = SeResult.Phase("Mx").Azimuth.DisplayValue;
                C3AziLabel.Text = SeResult.Phase("C3").Azimuth.DisplayValue;
                C4AziLabel.Text = SeResult.Phase("C4").Azimuth.DisplayValue;
                C1PLabel.Text = SeResult.Phase("C1").P.DisplayValue;
                C2PLabel.Text = SeResult.Phase("C2").P.DisplayValue;
                MxPLabel.Text = SeResult.Phase("Mx").P.DisplayValue;
                C3PLabel.Text = SeResult.Phase("C3").P.DisplayValue;
                C4PLabel.Text = SeResult.Phase("C4").P.DisplayValue;
                C1VLabel.Text = SeResult.Phase("C1").V.DisplayValue;
                C2VLabel.Text = SeResult.Phase("C2").V.DisplayValue;
                MxVLabel.Text = SeResult.Phase("Mx").V.DisplayValue;
                C3VLabel.Text = SeResult.Phase("C3").V.DisplayValue;
                C4VLabel.Text = SeResult.Phase("C4").V.DisplayValue;
                C2LcLabel.Text = SeResult.Phase("C2").LC.DisplayValue;
                C3LcLabel.Text = SeResult.Phase("C3").LC.DisplayValue;
                DurationLabel.Text = SeResult.Duration.DisplayValue;
                DurationCorrLabel.Text = SeResult.DurationCorr.DisplayValue;
                DeltaTLabel.Text = SeResult.DeltaT.DisplayValue;
                DepthLabel.Text = SeResult.Depth.DisplayValue;
                CoverageLabel.Text = SeResult.Coverage.DisplayValue;
                MagnitudeLabel.Text = SeResult.Magnitude.DisplayValue;
                SunMoonRatioLabel.Text = SeResult.SunMoonRatio.DisplayValue;
                LibrationLLabel.Text = SeResult.LibL.DisplayValue;
                LibrationBLabel.Text = SeResult.LibB.DisplayValue;
                LibrationCLabel.Text = SeResult.PaC.DisplayValue;
                WattsLinkLabel.Enabled = true;

                // TODO: cache the C2 and C3 limb corrections if available (tie them to the compute location)

                // Invalidate the sequence panel if these calc results differ from the circumstances stored in the Composer.
                //  The user will need to hit the sequence refresh button to enable the sequence (if they want
                //  to accept these calc results)
                EnableSequence(VerifyComposer());
                SaveSession();
            }
            else
            {
                MessageBox.Show("You must set a shooting location (on Location tab) prior to computing eclipse circumstances.");
            }
        }

        private void WattsLinkLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(SeResult.WattsChartLink);
        }

        private void SeIndexComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SeIndexComboBox.SelectedIndex != -1)
            {
                SeCalcButton.Enabled = true;
            }
        }
        #endregion

        #region Control Tab
        private void CreateSimEclipse(string name, int index, TimeSpan phaseLength, string eclType)
        {
            // create a simulated eclipse that starts in 1 minute--get your cameras out!
            SeIndex simIdx = new SeIndex(name, index);

            CalcRaw simRaw = new CalcRaw();
            DateTime c1 = DateTime.UtcNow + new TimeSpan(0,1,0);
            DateTime c2 = c1 + phaseLength;
            DateTime mx = c2 + phaseLength;
            DateTime c3 = mx + phaseLength;
            DateTime c4 = c3 + phaseLength;
            if (phaseLength == TimeSpan.Zero)
            {
                // Simulate a full duration eclipse using approximate durations from the Apr 8, 2024 eclipse in Rio Frio, TX.
                // TODO: add a function that copies the real CalcResult durations from an eclipse and uses those values instead
                c2 = c1 + new TimeSpan(1, 17, 17);
                mx = c2 + new TimeSpan(0, 2, 13);
                c3 = mx + new TimeSpan(0, 2, 13);
                c4 = c3 + new TimeSpan(1, 19, 9);
            }
            // populate the eclipse results
            simRaw.ecltype = eclType;
            simRaw.eclipse_depth = name;
            simRaw.watts_chart_link = "www.google.com";
            simRaw.tz = "0.0";
            simRaw.c1_date = c1.Date.ToString("yyyy/MM/dd");
            simRaw.c1_time = c1.ToString("HH:mm:ss.f");
            simRaw.c2_date = c2.Date.ToString("yyyy/MM/dd");
            simRaw.c2_time = c2.ToString("HH:mm:ss.f");
            simRaw.mid_date = mx.Date.ToString("yyyy/MM/dd");
            simRaw.mid_time = mx.ToString("HH:mm:ss.f");
            simRaw.c3_date = c3.Date.ToString("yyyy/MM/dd");
            simRaw.c3_time = c3.ToString("HH:mm:ss.f");
            simRaw.c4_date = c4.Date.ToString("yyyy/MM/dd");
            simRaw.c4_time = c4.ToString("HH:mm:ss.f");
            if (eclType == "Partial")
            {
                simRaw.c2_date = simRaw.c3_date = "n/a";
            }
            CalcResult simRes = new CalcResult(simRaw);
            // add the index to the Eclipse dropdown (if not present), and (re)save the results and index object
            int locIdx = GetSimLocalIndex(simIdx.IntValue);
            if (locIdx == -1)
            {
                // this sim is not yet added
                SeIndexComboBox.Items.Add(simIdx.StringValue);
                SimResults.Add(simRes);
                SimIndices.Add(simIdx);
            }
            else
            {
                // sim already on the list, just update the calcs
                SimResults[locIdx] = simRes;
            }
        }

        private void BuildSimulations()
        {
            // add some generic simulations
            // TODO: add an interface so user can add additional sims
            int idx = SeIndex.GetMaxIndex() + 1;
            CreateSimEclipse("SIM (T) - short (1min)", idx, new TimeSpan(0, 1, 0), "Total");
            idx++;
            CreateSimEclipse("SIM (T) - medium (5min)", idx, new TimeSpan(0, 5, 0), "Total");
            idx++; 
            CreateSimEclipse("SIM (T) - full length", idx, TimeSpan.Zero, "Total");
        }

        private int GetSimLocalIndex(int SeIndexIntValue)
        {
            // using the stored IntValue of the SeIndex object representing a simulation eclipse, determine this sim's
            // index in the SimResult and SimIndices lists (i.e. "Local Index").  Returns -1 if the SeIndexIntValue is not found.
            if (SimIndices.Count() > 0)
            {
                for (int i = 0; i < SimIndices.Count(); i++)
                {
                    if (SimIndices[i].IntValue == SeIndexIntValue) { return i;}
                }
            }
            return -1;
        }

        private void StartCaptureButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "Start Capture")
            {
                SessionIsLive = true;
                btn.Text = "Stop Capture";
            }
            else
            {
                SessionIsLive = false;
                btn.Text = "Start Capture";
                for (int i = 0; i < SeqFlowPanel.Controls.Count; i++)
                {
                    StepControl step = (StepControl)SeqFlowPanel.Controls[i];
                    step.Stop();
                }
            }
        }
        #endregion

        #region Time Keeper
        private void MasterTimer_Tick(object sender, EventArgs e)
        {
            // ticks every 100ms
            DateTime now = DateTime.Now;
            // update the clocks on the Control Tab
            UtcTimeLabel.Text = DateTime.UtcNow.ToString();
            LocalTimeLabel.Text = DateTime.Now.ToString();

            // update the phase reference timers on the Sequence Pane
            // TODO

            // check for any phase starts
            if (SeqFlowPanel.Controls.Count > 0)
            {
                DateTime sessionStart = ((StepControl)SeqFlowPanel.Controls[0]).StartDateTime;
                DateTime sessionEnd = ((StepControl)SeqFlowPanel.Controls[SeqFlowPanel.Controls.Count - 1]).EndDateTime;
                if (sessionStart - new TimeSpan(0,1,00) <= now && now <= sessionEnd + new TimeSpan(0,1,0))
                {
                    // only evaluate further if current time falls somewhere inside of the total event start/end times (with 1min margin on either end)
                    for (int i = 0; i < SeqFlowPanel.Controls.Count; i++)
                    {
                        StepControl step = (StepControl)SeqFlowPanel.Controls[i];
                        // check if "now" is between start time and end time
                        if (step.StartDateTime <= now && now < step.EndDateTime)
                        {
                            if (!step.IsActive() && SessionIsLive)
                            {
                                // If we're live, start the step if it isn't active, and break the forloop to stop evaluating the rest of the steps (steps cannot be parallel)
                                step.Start();
                                break;
                            }
                            else if (!step.IsRunning())
                            {
                                // run the step if it isn't running
                                step.RunStep();
                                break;
                            }
                        }
                    }
                }
            }
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

        private void SaveSession(string fpath=null)
        {
            // serialize the current session (shooting location, eclipse, steps)
            if (fpath == null) fpath = Path.Combine(AppDataDir, @"auto.session");

            Stream stream = new FileStream(fpath, FileMode.Create, System.IO.FileAccess.Write);
            
            var streamWriter = new StreamWriter(stream);
            var jsonWriter = new JsonTextWriter(streamWriter);
            var serializer = new JsonSerializer();
            // write to file
            serializer.Serialize(jsonWriter, Composer);
            jsonWriter.Flush();
            streamWriter.Flush();
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            stream.Dispose();
        }

        private void LoadSession(string fpath=null)
        {
            // point to the auto session if an alternative was not provided
            if (fpath == null) fpath = Path.Combine(AppDataDir, @"auto.session");

            if (File.Exists(fpath))
            {
                try
                {
                    // deserialize the session and populate the related fields
                    Stream stream = new FileStream(fpath, FileMode.Open, System.IO.FileAccess.Read);
                    var streamReader = new StreamReader(stream);
                    var jsonReader = new JsonTextReader(streamReader);
                    var serializer = new JsonSerializer();
                    Composer = serializer.Deserialize<StepComposer>(jsonReader);
                    streamReader.Close();
                    jsonReader.Close();
                    stream.Dispose();

                    // subscribe the event listeners to the StepComposer events
                    Composer.SequenceUpdated += new EventHandler(EventHandler_SequenceUpdated);
                    Composer.CircumstancesRequested += new EventHandler(EventHandler_CircumstancesRequested);
                    // restore lat/lng/elv
                    if (Composer.ShootingElv != -9999)
                    {
                        LatTextBox.Text = Composer.ShootingLat.ToString();
                        LonTextBox.Text = Composer.ShootingLng.ToString();
                        AltTextBox.Text = Composer.ShootingElv.ToString();
                        SettingsTabControl.SelectedTab = LocTabPage;
                        SetLocButton.PerformClick();
                    }
                    // restore eclipse selection/calculations
                    if (Composer.Eclipse != null)
                    {
                        SeIndexComboBox.SelectedItem = Composer.Eclipse.StringValue;
                        SettingsTabControl.SelectedTab = EclipseTabPage;
                        SeCalcButton.PerformClick();
                    }
                    SettingsTabControl.SelectedTab = SettingsTabPage;
                    // restore the step sequence, if present
                    Composer.ReloadSequence();
                    // Invalidate the sequence panel if the location/eclipse info differ from the circumstances stored in the Composer.
                    //  The user will need to hit the sequence refresh button to enable the sequence (if they want
                    //  to accept these calc results)
                    EnableSequence(VerifyComposer());
                }
                catch (Exception ex) { ReportError(ex.Message, false); }
            }
            else
            {
                // no auto session present, subscribe the event listeners to the StepComposer events
                Composer.SequenceUpdated += new EventHandler(EventHandler_SequenceUpdated);
                Composer.CircumstancesRequested += new EventHandler(EventHandler_CircumstancesRequested);
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
                    stream.Close();
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

            // ?
            // TODO: handle the script field

            // create a refernce to this step so it can be written to later
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

        private void RefreshSequenceButton_Click(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show("Refreshing the sequence will update the stages with the dates and times currently shown on the Eclipse tab.  This may result in stages being adjusted or removed depending on the presence and/or duration of eclipse phases at the current specified shooting location.\nThis cannot be undone!",
                "Caution",
                MessageBoxButtons.OKCancel);

            if (confirmResult == DialogResult.OK)
            {
                Composer.Refresh();
                SaveSession();
            }
            else
            {
                // don't reset...
            }
            // recheck the composer and toggle the sequence on if it's valid
            EnableSequence(VerifyComposer());
        }

        private void EventHandler_SequenceUpdated(object sender, EventArgs e)
        {
            SeqFlowPanel.Controls.Clear();
            List<StepControl> steps = Composer.GetStepControls();
            foreach (StepControl step in steps)
            {
                // add the event listeners
                step.EditStage += new EventHandler(EventHandler_EditStage);
                step.DeleteStage += new EventHandler(EventHandler_DeleteStage);
                step.TaskFired += EventHandler_TaskFired;
                // add the step to the flow panel
                SeqFlowPanel.Controls.Add(step);
            }
        }

        private async void EventHandler_TaskFired(object sender, TaskFiredEventArgs e)
        {
            StepControl step = (StepControl)sender;
            StepControl nextStep = null;
            int nextStepIdx = SeqFlowPanel.Controls.IndexOf(step) + 1;
            if (SeqFlowPanel.Controls.Count > nextStepIdx)
            {
            nextStep = (StepControl)SeqFlowPanel.Controls[nextStepIdx];
            }

            if (e.Repeat)
            {
                // continuous
                if (nextStep != null)
                {
                    // try not to bleed over into nextStep's timeframe
                    while (DateTime.Now <= step.EndDateTime - _averageTaskTime)
                    {
                        foreach (TaskControl task in step.GetTasks())
                        {
                            if (DateTime.Now > step.EndDateTime - _averageTaskTime) break;
                            await FireTask(task);
                        }
                    }
                }
                else
                {
                    // last step, not worried about bleedover (will complete the entire task list on the
                    // final pass, regardless of endtime)
                    while (DateTime.Now <= e.IntervalEndTime)
                    {
                        foreach (TaskControl task in step.GetTasks())
                        {
                            await FireTask(task);
                        }
                    }
                }
            }
            else if (e.IntervalStartTime >= step.EndDateTime)
            {
                // single
                // the start/end times are defined as equal, so the firing time may actually be
                // slightly later than the end time.  This task still needs to be fired one time though.
                // These "single" tasks do not get skipped if the IntervalStartTime has bled into the next
                // Step's timeframe
                foreach (TaskControl task in step.GetTasks())
                {
                    await FireTask(task);
                }
            }
            else
            {
                // interval
                if (nextStep != null)
                {
                    // try not to bleed over into nextStep's timeframe
                    foreach (TaskControl task in step.GetTasks())
                    {
                        if (DateTime.Now > step.EndDateTime - _averageTaskTime) break;
                        await FireTask(task);
                    }
                }
                else
                {
                    // last step, not worried about bleedover (will complete the entire task list on the
                    // final pass, regardless of endtime)
                    foreach (TaskControl task in step.GetTasks())
                    {
                        await FireTask(task);
                    }
                }
            }
        }

        private void EventHandler_CircumstancesRequested(object sender, EventArgs e)
        {
            // update circumstances
            if (SeResult != null)
            {
                // inject the latest circumstances into the StepComposer
                List<SeIndex> indices = SeIndex.SeIndices();
                indices.AddRange(SimIndices);
                Composer.Eclipse = SeIndex.GetValue(SeIndexComboBox.SelectedItem.ToString(), indices);
                Composer.C1 = SeResult.Phase("C1").DateTime;
                Composer.C2 = SeResult.Phase("C2").DateTime;
                Composer.Mx = SeResult.Phase("Mx").DateTime;
                Composer.C3 = SeResult.Phase("C3").DateTime;
                Composer.C4 = SeResult.Phase("C4").DateTime;
            }
            else
            {
                // inject some default datetimes that will allow for generic sequence generation
                Composer.Eclipse = null;
                TimeSpan onehour = new TimeSpan(1, 0, 0);
                Composer.C1 = new SeDateTime(DateTime.Now + new TimeSpan(100*365,0,0,0));
                Composer.C2 = new SeDateTime(Composer.C1.ComputeValue + onehour);
                Composer.Mx = new SeDateTime(Composer.C2.ComputeValue + onehour);
                Composer.C3 = new SeDateTime(Composer.Mx.ComputeValue + onehour);
                Composer.C4 = new SeDateTime(Composer.C3.ComputeValue + onehour);
            }

            // update location
            if (ShootingLocation != null)
            {
                Composer.ShootingLat = ShootingLocation.Lat;
                Composer.ShootingLng = ShootingLocation.Lng;
                Composer.ShootingElv = Elevation;
            }
            else
            {
                Composer.ShootingLat = 0;
                Composer.ShootingLng = 0;
                Composer.ShootingElv = -9999;
            }
        }

        private bool VerifyComposer()
        {
            if (Composer.StepList.Count() == 0) return true;
            if (SeResult != null)
            {
                if (Elevation != -9999)
                {
                    List<SeIndex> indices = SeIndex.SeIndices();
                    indices.AddRange(SimIndices);
                    SeIndex eclipse_index = SeIndex.GetValue(SeIndexComboBox.SelectedItem.ToString(), indices);
                    // location and eclipse set and calculated, check property equality
                    if (ShootingLocation.Lat == Composer.ShootingLat &&
                        ShootingLocation.Lng == Composer.ShootingLng &&
                        Elevation == Composer.ShootingElv &&
                        eclipse_index.IntValue == Composer.Eclipse.IntValue) 
                    {
                        // check each circumstance datetime
                        List<string> references = new List<string> { "C1", "C2", "Mx", "C3", "C4" };
                        foreach (string reference in references)
                        {
                            SeDateTime composerTime = (SeDateTime)typeof(StepComposer).GetProperty(reference).GetValue(Composer);
                            if (SeResult.Phase(reference).DateTime.ComputeValue != composerTime.ComputeValue) 
                                return false;
                        }
                        // if we reached this point, all circumstance times match, return true
                        return true;
                    }
                }
            }
            else
            {
                if (Elevation == -9999)
                    // both shooting location and eclipse are unset, return true to allow creation of generic sequences
                    return true;
                else
                    // shooting location is set, but eclipse is not, the sequence is no longer generic, but cannot yet be validated
                    return false;
            }
            return false;
        }

        private void EnableSequence(bool ComposerVerified)
        {
            // disable the sequence panel if ComposerVerified is false, otherwise, enable it
            if (ComposerVerified)
            {
                SeqFlowPanel.Enabled = true;
                SeqGenTabPage.Enabled = true;
                RefreshSequenceButton.Visible = false;
            }
            else
            {
                SeqFlowPanel.Enabled =false;
                SeqGenTabPage.Enabled = false;
                RefreshSequenceButton.Visible = true;
            }
        }

        #endregion        
    }
}
