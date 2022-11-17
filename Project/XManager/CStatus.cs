using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
//using UroView.Arduino;
using XManager.Camera;
using XManager.Controls;
using XManager.FigureData;
using XManager.Forms;
using XManager.ImageProcessingData;
using XManager.Util;
using XManager.Utill;
using static XManager.Controls.DrawBox;
//using static XManager.Utill.DBManager;

namespace XManager
{
    public unsafe class NewThumbnailInfo
    {
        public byte* OrgData;
        public int imageWidth;
        public int imageHeight;
        public int center;
        public int width;
        public int bufferSize;
        public string rawPath;
        public int Bits;

    }

    public enum IMAGE_LOAD_STAT
    {
        NONE,
        MANUAL,
        GRAB
    };

    public enum PMMode
    {
        PM_NONE,
        PM_WORKLIST,
        PM_LOCAL_VIEW,
        PM_LOCAL_EXAM,
    };

    public enum ACQUISITION
    {
        START,
        STOP,
    }

    public class CStatus
    {
        #region delegate
        public delegate void XrayConditionUpdateDelegate(bool isOn);
        public event XrayConditionUpdateDelegate XrayConditionUpdateHandler;

        public delegate void BootUpCloseFormDelegate(string message);
        public event BootUpCloseFormDelegate BootUpCloseFormHandler;

        public delegate void BootUpTimerStartDelegate();
        public event BootUpTimerStartDelegate BootUpTimerStartHandler;

        public delegate void BootUpTimerStopDelegate();
        public event BootUpTimerStopDelegate BootUpTimerStopHandler;

        public delegate void BootUpTextUpdateDelegate(string message);
        public event BootUpTextUpdateDelegate BootUpTextUpdateHandler;

        public delegate void HoldImageUpdateDelegate();
        public event HoldImageUpdateDelegate HoldImageUpdateHandler;
        #endregion

        public CameraManager CameraManager = new CameraManager();
        public ErrorLog ErrorLogManager = new ErrorLog();
        public Stopwatch DelayStopWatch = new Stopwatch();

        private static CStatus _instance;

        public EventWaitHandle BootupThreadHanlder = new EventWaitHandle(false, EventResetMode.ManualReset);

        private List<NewThumbnailInfo> _thumbnailInfoList = new List<NewThumbnailInfo>();
        string currentDir = Directory.GetCurrentDirectory();

        #region acA1300_30gm
        private int _crop_viewAreaCenterX = 640;
        public int Crop_ViewAreaCenterX
        {
            get { return _crop_viewAreaCenterX; }
            set { _crop_viewAreaCenterX = value; }
        }

        private int _crop_viewAreaCenterY = 480;
        public int Crop_ViewAreaCenterY
        {
            get { return _crop_viewAreaCenterY; }
            set { _crop_viewAreaCenterY = value; }
        }

        private int _crop_viewAreaLength = 960;
        public int Crop_ViewAreaLength
        {
            get { return _crop_viewAreaLength; }
            set { _crop_viewAreaLength = value; }
        }

        private int _basler_FrameRate = 30;
        public int Basler_FrameRate
        {
            get { return _basler_FrameRate; }
            set { _basler_FrameRate = value; }
        }

        private string _basler_exposureAuto = "Off";
        public string Basler_ExposureAuto
        {
            get { return _basler_exposureAuto; }
            set { _basler_exposureAuto = value; }
        }

        private int _basler_binning = 1;
        public int Basler_binning
        {
            get { return _basler_binning; }
            set { _basler_binning = value; }
        }

        #endregion

        #region Thales 2121S
        private byte[] _PU_ByteIpAddress = { 192, 168, 2, 2 };
        public byte[] PU_ByteIpAddress
        {
            get { return _PU_ByteIpAddress; }
            set { _PU_ByteIpAddress = value; }
        }

        private string _PU_Port = "30000";
        public string PU_Port
        {
            get { return _PU_Port; }
            set { _PU_Port = value; }
        }

        private string _cameraRows = "1344";
        public string CameraRows
        {
            get { return _cameraRows; }
            set { _cameraRows = value; }
        }

        private string _cameraColumns = "1344";
        public string CameraColumns
        {
            get { return _cameraColumns; }
            set { _cameraColumns = value; }
        }

        private string _cameraBitsAllocated = "16";
        public string CameraBitsAllocated
        {
            get { return _cameraBitsAllocated; }
            set { _cameraBitsAllocated = value; }
        }

        private string _cameraBitsStored = "16";
        public string CameraBitsStored
        {
            get { return _cameraBitsStored; }
            set { _cameraBitsStored = value; }
        }

        private string _cameraHighBit = "14";
        public string CameraHighBit
        {
            get { return _cameraHighBit; }
            set { _cameraHighBit = value; }
        }

        #endregion

        #region PACS
        private string _Pacs_LocalAE = "UROVIEW";
        public string Pacs_LocalAE
        {
            get { return _Pacs_LocalAE; }
            set { _Pacs_LocalAE = value; }
        }

        private string _Pacs_RemoteAE = "KPserver";
        public string Pacs_RemoteAE
        {
            get { return _Pacs_RemoteAE; }
            set { _Pacs_RemoteAE = value; }
        }

        private string _Pacs_RemoteIP = "192.168.0.8";
        public string Pacs_RemoteIP
        {
            get { return _Pacs_RemoteIP; }
            set { _Pacs_RemoteIP = value; }
        }

        private int _Pacs_RemotePort = 104;
        public int Pacs_RemotePort
        {
            get { return _Pacs_RemotePort; }
            set { _Pacs_RemotePort = value; }
        }

        //
        private string _Worklist_LocalAE = "UROVIEW";
        public string Worklist_LocalAE
        {
            get { return _Worklist_LocalAE; }
            set { _Worklist_LocalAE = value; }
        }

        private string _Worklist_RemoteAE = "MWL_SERVER";
        public string Worklist_RemoteAE
        {
            get { return _Worklist_RemoteAE; }
            set { _Worklist_RemoteAE = value; }
        }

        private string _Worklist_RemoteIP = "192.168.0.8";
        public string Worklist_RemoteIP
        {
            get { return _Worklist_RemoteIP; }
            set { _Worklist_RemoteIP = value; }
        }

        private int _Worklist_RemotePort = 105;
        public int Worklist_RemotePort
        {
            get { return _Worklist_RemotePort; }
            set { _Worklist_RemotePort = value; }
        }

        //private string _Worklist_InputPath = @"D:\UroViewConfig\Temp\Input";
        private string _Worklist_InputPath = "";
        public string Worklist_InputPath
        {
            get { return _Worklist_InputPath; }
            set { _Worklist_InputPath = value; }
        }

        //private string _Worklist_OutputPath = @"D:\UroViewConfig\Temp\output";
        private string _Worklist_OutputPath = "";
        public string Worklist_OutputPath
        {
            get { return _Worklist_OutputPath; }
            set { _Worklist_OutputPath = value; }
        }
        #endregion

        #region Property
        public static bool AdministratorMode = false;
        public float FontSize = 15;
        private ACQUISITION _acquistion = ACQUISITION.STOP;
        public ACQUISITION Acquistion
        {
            get { return _acquistion; }
            set { _acquistion = value; }
        }
        //private string _configPath = @"D:\UroViewConfig";
        private string _configPath = "";
        public string ConfigPath
        {
            get { return _configPath; }
            set { _configPath = value; }
        }

        private bool _isBootError = false;
        public bool IsBootError
        {
            get { return _isBootError; }
            set { _isBootError = value; }
        }

        private bool _isTestMode = true;
        public bool IsTestMode
        {
            get { return _isTestMode; }
            set { _isTestMode = value; }
        }

        private bool _isXrayDisableMode = true;
        public bool IsXrayDisableMode
        {
            get { return _isXrayDisableMode; }
            set { _isXrayDisableMode = value; }
        }

        private int _viewDelayTime = 300;
        public int ViewDelayTime
        {
            get { return _viewDelayTime; }
            set { _viewDelayTime = value; }
        }

        private int _bootUpDotCount = 0;
        public int BootUpDotCount
        {
            get { return _bootUpDotCount; }
            set { _bootUpDotCount = value; }
        }

        private bool _isNowDrawing = false;
        public bool IsNowDrawing
        {
            get { return _isNowDrawing; }
            set { _isNowDrawing = value; }
        }

        private int _instanceCount = 0;
        public int InstanceCount
        {
            get { return _instanceCount; }
            set { _instanceCount = value; }
        }

        private bool _isAcqusitionExecute = false;
        public bool IsAcqusitionExecute
        {
            get { return _isAcqusitionExecute; }
            set { _isAcqusitionExecute = value; }
        }

        private string _acquisitionTime = "";
        public string AcquisitionTime
        {
            get { return _acquisitionTime; }
            set { _acquisitionTime = value; }
        }

        private string _xrayComPort = "";
        public string XrayComPort
        {
            get { return _xrayComPort; }
            set { _xrayComPort = value; }
        }

        private bool _isCalbirationMode = false;
        public bool IsCalbirationMode
        {
            get { return _isCalbirationMode; }
            set { _isCalbirationMode = value; }
        }

        private bool _isBootUpMode = true;
        public bool IsBootUpMode
        {
            get { return _isBootUpMode; }
            set { _isBootUpMode = value; }
        }
        StringBuilder m_Builder;
        StringBuilder Builder
        {
            get
            {
                if (m_Builder == null) m_Builder = new StringBuilder(255);
                return m_Builder;
            }
            set { m_Builder = value; }
        }

        private bool _isCapture = false;
        public bool IsCapture
        {
            get { return _isCapture; }
            set { _isCapture = value; }
        }

        private int _liveCenter = 0;
        public int LiveCenter
        {
            get { return _liveCenter; }
            set { _liveCenter = value; }
        }

        private int _liveWidth = 0;
        public int LiveWidth
        {
            get { return _liveWidth; }
            set { _liveWidth = value; }
        }

        private int _administrator_LiveCenter = 2829;
        public int Administrator_LiveCenter
        {
            get { return _administrator_LiveCenter; }
            set { _administrator_LiveCenter = value; }
        }

        private int _administrator_LiveWidth = 3544;
        public int Administrator_LiveWidth
        {
            get { return _administrator_LiveWidth; }
            set { _administrator_LiveWidth = value; }
        }

        private Image _viewImage = null;
        public Image ViewImage
        {
            get { return _viewImage; }
            set { _viewImage = value; }
        }

        private PMMode _programMode = PMMode.PM_NONE;
        public PMMode ProgramMode
        {
            get { return _programMode; }
            set { _programMode = value; }
        }

        private double _fovX = 0;
        public double FovX
        {
            get { return _fovX; }
            set { _fovX = value; }
        }

        private double _fovY = 0;
        public double FovY
        {
            get { return _fovY; }
            set { _fovY = value; }
        }


        private int _flipXCount = 0;
        public int FlipXCount
        {
            get { return _flipXCount; }
            set { _flipXCount = value; }
        }

        private int _flipYCount = 0;
        public int FlipYCount
        {
            get { return _flipYCount; }
            set { _flipYCount = value; }
        }

        private int _cwClickCount = 0;
        public int CWClickCount
        {
            get { return _cwClickCount; }
            set { _cwClickCount = value; }
        }

        private int _ccwClickCount = 0;
        public int CCWClickCount
        {
            get { return _ccwClickCount; }
            set { _ccwClickCount = value; }
        }

        private int _histoSelectedMin = 0;
        public int HistoSelectedMin
        {
            get { return _histoSelectedMin; }
            set { _histoSelectedMin = value; }
        }

        private int _histoSelectedMax = 65535;
        public int HistoSelectedMax
        {
            get { return _histoSelectedMax; }
            set { _histoSelectedMax = value; }
        }

        //public DrawingPanel CamPanel
        //{
        //    get { return _camPanel; }
        //    set { _camPanel = value; }
        //}

        //private DrawingPanel _camPanel;

        private RectangleF _selectedRegion;

        //public ushort[] LatestImageArr { get; set; }

        public ushort[] LatestImageArrHistogramd { get; set; }

        public byte[] LatestImageArr8bitShown { get; set; }

        public RectangleF SelectedRegion
        {
            get { return _selectedRegion; }
            set { _selectedRegion = value; }
        }

        private IMAGE_LOAD_STAT curImageLoadStat = IMAGE_LOAD_STAT.NONE;
        public IMAGE_LOAD_STAT CurImageLoadStat
        {
            get { return curImageLoadStat; }
            set { curImageLoadStat = value; }
        }
        #endregion

        #region 새로 추가된 내용

        private DrawBox _drawBox = null;
        
        public eDerivativeType DerivativeType = eDerivativeType.None;

        public bool IsDrawingProfile = false;
        public int ProfileWidth = 1;

        public Settings Settings = new Settings();
        public HistogramForm HistogramForm = null;
        public RoiListForm RoiListForm = null;
        public ProfileListForm ProfileListForm = null;
        public ProcessingTimeForm ProcessingTimeForm = null;
        public List<ProcessingTime> ProcessingTime = new List<ProcessingTime>();
        #endregion
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(String section, String key, String def, StringBuilder retVal, int size, String filePath);

        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(String section, String key, String val, String filepath);


        public bool Test = false;
        public static CStatus Instance()
        {
            if (_instance == null)
                _instance = new CStatus();

            return _instance;
        }

        public void GetAdministrator()
        {
            //System.IO.DirectoryInfo folder = new System.IO.DirectoryInfo(this.ConfigPath);

            //if (folder.Exists == false)
            //    folder.Create();

            //string txtFilePath = Path.Combine(this.ConfigPath, "Amdinistrator.txt");
            //if (File.Exists(txtFilePath))
            //{
            //    //파일 존재
            //    string[] textValue = File.ReadAllLines(txtFilePath, Encoding.Default);

            //    if (textValue.Length > 0)
            //    {
            //        for (int i = 0; i < textValue.Length; i++)
            //        {
            //            this._doctors.Add(textValue[i]);
            //        }
            //    }
            //}
            //else
            //{
            //    File.Create(txtFilePath);
            //}
        }

        public void SetAdministrator(string txtPath)
        {
            //StreamWriter writer;
            //writer = File.CreateText(txtPath);
            //for (int i = 0; i < this._doctors.Count; i++)
            //{
            //    string temp = this._doctors[i].ToString();
            //    writer.WriteLine(temp);
            //}
            //writer.Close();
        }

        private string GetIniValue(string section, string key)
        {
            try
            {
                string iniFilePath = Path.Combine(this.ConfigPath, "config.ini");
                if (!File.Exists(iniFilePath))
                    throw new Exception(string.Format("{0} file not exist", iniFilePath));
                int i = GetPrivateProfileString(section, key, "", Builder, 255, iniFilePath);
                return Builder.ToString();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return string.Empty;
            }
        }

        private void SetIniValue(string section, string key, string value)
        {
            try
            {
                string iniFilePath = Path.Combine(this.ConfigPath, "config.ini");
                WritePrivateProfileString(section, key, value, iniFilePath);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public void HoldLiveImageCaptureUpdate()
        {
            if (HoldImageUpdateHandler != null)
                HoldImageUpdateHandler();
        }

        public void ParamFormXrayConditionUpdate(bool isOn)
        {
            if (this.XrayConditionUpdateHandler != null)
                XrayConditionUpdateHandler(isOn);
        }

        public void SetDrawBox(DrawBox drawBox)
        {
            _drawBox = drawBox;
        }

        public DrawBox GetDrawBox()
        {
            return _drawBox;
        }

        public double GetDistance(PointF orgStartPoint, PointF orgEndPoint)
        {
            double length = 0;
            if (CStatus.Instance().Settings.MeasureMentType == eMeasurementType.Fov_Calibration)
            {
                double pitchX = (double)CStatus.Instance().Settings.FovWidth / CStatus.Instance().GetDrawBox().ImageManager.GetOrgImage().Width;
                double pitchY = (double)CStatus.Instance().Settings.FovHeight / CStatus.Instance().GetDrawBox().ImageManager.GetOrgImage().Height;

                double lengthX = Math.Abs((int)orgStartPoint.X - (int)orgEndPoint.X) * pitchX;
                double lengthY = Math.Abs((int)orgStartPoint.Y - (int)orgEndPoint.Y) * pitchY;
                double temp = Math.Pow(lengthX, 2) + Math.Pow(lengthY, 2);
                length = Math.Sqrt(temp);
                length = Math.Round(length, 2);
            }
            else if (CStatus.Instance().Settings.MeasureMentType == eMeasurementType.Pixel_Calibration)
            {
                length = GetDistanceFromPitch(orgStartPoint, orgEndPoint);
            }
            return Math.Round(length, 2);
        }

        public double GetDistanceFromPitch(PointF startPoint, PointF endPoint)
        {
            double distance = 0;
            if (startPoint.X == endPoint.X) // 세로직선
            {
                double length = Math.Abs(startPoint.Y - endPoint.Y);
                distance = Settings.Pitch * length;
            }
            else if (startPoint.Y == endPoint.Y) // 가로 직선
            {
                double length = Math.Abs(startPoint.X - endPoint.X);
                distance = Settings.Pitch * length;
            }
            else // 대각선
            {
                double xAmount = Math.Abs(startPoint.X - endPoint.X);
                double yAmount = Math.Abs(startPoint.Y - endPoint.Y);

                distance = Math.Sqrt(((xAmount * xAmount) + (yAmount * yAmount)) * Settings.Pitch * Settings.Pitch);
            }

            return distance;
        }

        public void RoiListFormMoveUpdate(RectangleF orgRoi)
        {
            if (this.GetDrawBox().Image == null)
                return;

            if (this.RoiListForm != null)
            {
                this.RoiListForm.MoveEventUpdate(orgRoi);
            }
        }

        public void ProfileListFormMoveUpdate(List<HistogramParams> param, PointF orgStartPt, PointF orgEndPt)
        {
            if (this.GetDrawBox().Image == null)
                return;

            if (this.ProfileListForm != null)
            {
                this.ProfileListForm.MoveEventUpdate(param, orgStartPt, orgEndPt);
            }
        }

        public void SelectRoi(int id)
        {
            if (this.GetDrawBox().Image == null)
                return;
            if (this.RoiListForm != null)
            {
                this.RoiListForm.RoiSelect(id);
            }
        }

        public void SelectProfile(int id)
        {
            if (this.GetDrawBox().Image == null)
                return;
            //if (this.ProfileListForm != null)
            //{
            //    this.ProfileListForm.ProfileSelect(id);
            //}
        }

        public void ProfileListFormOpen()
        {
            //if (ProfileListForm == null)
            //{
            //    this.ProfileListForm = new ProfileListForm();
            //    this.ProfileListForm.DataGridViewClickDelegate = (id) => this.SelectedViewer.GetDrawBox().ProfileSelectUpdate(id);
            //    this.ProfileListForm.CloseEventDelegate = () => this.ProfileListForm = null;
            //    this.ProfileListForm.Show();
            //}
        }

        public void UpdateRoiToRoiForm(eFormUpdate type)
        {
            if (this.GetDrawBox().Image == null)
                return;
            int roiCount = CStatus.Instance().GetDrawBox().TrackerManager.GetRoiCount();
            if (roiCount <= 0)
                return;

            if (this.RoiListForm != null)
            {
                if (this.GetDrawBox().TrackerManager.GetRoiCount() <= 0)
                {
                    this.RoiListForm.DatagridViewClear();
                    return;
                }
                if (this.GetDrawBox().TrackerManager.GetRoiCount() == 1)
                {
                    RoiListFormUpdate();
                }
                this.RoiListForm.RoiDatagridUpdate();
                this.RoiListForm.SelectedHistogramUpdate();
            }
        }

        public void UpdateProfileToForm(eFormUpdate type)
        {
            if (this.GetDrawBox().Image == null)
                return;
            int profileCount = CStatus.Instance().GetDrawBox().TrackerManager.GetProfileCount();
            if (profileCount <= 0)
                return;

            if (this.ProfileListForm != null)
            {
                if (this.GetDrawBox().TrackerManager.GetProfileCount() <= 0)
                    return;
                if (this.GetDrawBox().TrackerManager.GetProfileCount() == 1)
                {
                    ProfileListFormUpdate();
                }
                this.ProfileListForm.ProfileDataGridClearReUpdate();
                this.ProfileListForm.HistogramGraphReNewal();
            }
        }

        public void SetPitch(PointF startPoint, PointF endPoint, double distance_mm)
        {
            if (startPoint.X == endPoint.X) // 세로직선
            {
                double length = Math.Abs(startPoint.Y - endPoint.Y);
                CStatus.Instance().Settings.Pitch = distance_mm / length;
            }
            else if (startPoint.Y == endPoint.Y) // 가로 직선
            {
                double length = Math.Abs(startPoint.X - endPoint.X);
                CStatus.Instance().Settings.Pitch = distance_mm / length;
            }
            else // 대각선
            {
                double xAmount = Math.Abs(startPoint.X - endPoint.X);
                double yAmount = Math.Abs(startPoint.Y - endPoint.Y);

                CStatus.Instance().Settings.Pitch = distance_mm / Math.Sqrt((xAmount * xAmount) + (yAmount * yAmount));
            }
        }

        public bool NULLCheckDrawBox()
        {
            if (CStatus.Instance().GetDrawBox() == null)
                return true;

            if (CStatus.Instance().GetDrawBox().Image == null)
                return true;

            return false;
        }

        public void HistogramFormUpdate(JImage image)
        {
            if (HistogramForm != null)
            {
                HistogramForm.ReNewalGraphUpdate(image);
            }
        }

        public void HistogramFormUpdate()
        {
            if (HistogramForm != null)
            {
                HistogramForm.ReNewalGraphUpdate();
            }
        }

        public void RoiFormOpen()
        {
            if(RoiListForm == null)
            {
                RectangleFigure roi = CStatus.Instance().GetDrawBox().TrackerManager.GetSelectedRoi();
                if(roi == null)
                {
                    CStatus.Instance().GetDrawBox().TrackerManager.SetFirstRoi();
                    CStatus.Instance().GetDrawBox().ReUpdate();
                }
                RoiListForm = new RoiListForm();
                RoiListForm.DataGridViewClickDelegate = (id) => this.GetDrawBox().TrackerManager.RoiSelectUpdate(id);
                RoiListForm.CloseEventDelegate = () => this.RoiListForm = null;
                RoiListForm.Show();
            }
        }

        public void RoiListFormUpdate()
        {
            if (this.RoiListForm != null)
            {
                if (CStatus.Instance().GetDrawBox().TrackerManager.GetRoiCount() <= 0)
                    return;

                this.RoiListForm.RoiDatagridUpdate();
                this.RoiListForm.HistogramPanelDataGridNewUpdate();
                this.RoiListForm.HistogramGraphReNewal();
                this.RoiListForm.MarkReset();
            }
        }

        public void ProfileFormOpen()
        {
            if (ProfileListForm == null)
            {
                ProfileFigure profile = CStatus.Instance().GetDrawBox().TrackerManager.GetSelectedProfile();
                if(profile == null)
                {
                    CStatus.Instance().GetDrawBox().TrackerManager.SetFirstProfile();
                    CStatus.Instance().GetDrawBox().ReUpdate();
                }
                this.ProfileListForm = new ProfileListForm();
                this.ProfileListForm.DataGridViewClickDelegate = (id) => this.GetDrawBox().TrackerManager.ProfileSelectUpdate(id);
                this.ProfileListForm.CloseEventDelegate = () => this.ProfileListForm = null;
                this.ProfileListForm.Show();
            }
        }

        public void ProfileListFormUpdate()
        {
            if (this.ProfileListForm != null)
            {
                if (CStatus.Instance().GetDrawBox().TrackerManager.GetProfileCount() <= 0)
                    return;
                this.ProfileListForm.ProfileDataGridClearReUpdate();
                this.ProfileListForm.HistogramPanelDataGridNewUpdate();
                this.ProfileListForm.HistogramGraphReNewal();
                //this.ProfileListForm.MarkReset();
            }
        }

        public void OpenProcessingTimeForm()
        {
            if (CStatus.Instance().ProcessingTimeForm == null)
            {
                CStatus.Instance().ProcessingTimeForm = new ProcessingTimeForm();
                CStatus.Instance().ProcessingTimeForm.CloseEventDelegate = () => CStatus.Instance().ProcessingTimeForm = null;
                CStatus.Instance().ProcessingTimeForm.Show();
            }
        }

        public void ProcessingTimeFormUpdate()
        {
            if (CStatus.Instance().ProcessingTimeForm != null)
                CStatus.Instance().ProcessingTimeForm.ProcessingUpdate();
        }
    }
}
