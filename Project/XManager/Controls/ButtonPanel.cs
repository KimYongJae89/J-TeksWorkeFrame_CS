using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XManager.Camera;
using System.Net.Sockets;
using System.Net;
using XManager.Utill;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using XManager.Forms;
using XManager.Util;
using XManager.ImageProcessingData;
using LibraryGlobalization.Properties;

namespace XManager.Controls
{
    public partial class ButtonPanel : UserControl
    {
        private bool _isAdministratorMode = false;
        private bool _preXrayIsOn = false;
        private bool _isAcqusitionExecute = false;
        private bool _isDSAPanelVisible = false;
        //private KDicom dcm = null;

        public ButtonPanel()
        {
            InitializeComponent();
        }

        bool test = false;
        private void ButtonPanel_Load(object sender, EventArgs e)
        {
            if (CStatus.AdministratorMode == true)
                lblAdministrator.Visible = true;
            else
                lblAdministrator.Visible = false;

            if (CStatus.Instance().XrayComPort == "")
            {
                //MessageBox.Show("Check the Serial Port.");
            }
            else
            {
                if (CStatus.Instance().IsTestMode == false)
                {
                    //임시 잠금
                    //CStatus.Instance().XrayManager.Disconnected += SerialDisConnected;
                    btnTestInputImage.Visible = false;
                }
            }

            toolTip.SetToolTip(lbl_Aquisition, LangResource.CameraAcquisition);
            toolTip.SetToolTip(lbHistogram, LangResource.Histogram);
            toolTip.SetToolTip(lbl_Rotate_CW, LangResource.ImageRotation);
            toolTip.SetToolTip(lbl_Rotate_CCW, LangResource.ImageRotation);
            toolTip.SetToolTip(lbl_flip_horizontal, LangResource.FlipHorizontally);
            toolTip.SetToolTip(lbl_flip_vertical, LangResource.FlipVertically);
            toolTip.SetToolTip(lbSave, LangResource.SaveImage);
            toolTip.SetToolTip(lbLoad, LangResource.LoadImage);
            toolTip.SetToolTip(lbRuler, LangResource.Ruler);
            toolTip.SetToolTip(lbProtractor, LangResource.Protractor);
            toolTip.SetToolTip(lbSelectRegion, LangResource.Roi);
            toolTip.SetToolTip(lbReset, LangResource.ToolReset);
            toolTip.SetToolTip(lbFilter, LangResource.Filters);
            toolTip.SetToolTip(lbLineProfile, LangResource.ProfileList);
            toolTip.SetToolTip(lbRegionHisto, LangResource.RoiList);
            toolTip.SetToolTip(lbFovSetting, LangResource.Setup);
            toolTip.SetToolTip(lblProfile, LangResource.Profile);
            toolTip.SetToolTip(lblProcessingTime, LangResource.ProcessingTime);
            toolTip.SetToolTip(lbl_Exit, LangResource.Exit);
        }

        #region Button Event
        private void lbl_Aquisition_Click(object sender, EventArgs e)
        {
            try
            {
                if (test == false)
                {

                    if (CStatus.Instance().RoiListForm != null)
                        CStatus.Instance().RoiListForm.Close();

                    if (CStatus.Instance().ProfileListForm != null)
                        CStatus.Instance().ProfileListForm.Close();

                    if (CStatus.Instance().HistogramForm == null)
                    {

                        CStatus.Instance().HistogramForm = new HistogramForm();
                        CStatus.Instance().HistogramForm.CloseEventDelegate = () => CStatus.Instance().HistogramForm = null;
                        CStatus.Instance().HistogramForm.Show();
                    }

                    XrayStatusChange(true);
                    CStatus.Instance().CameraManager.StartContinuousGrap();
                    test = true;

                    CStatus.Instance().CurImageLoadStat = IMAGE_LOAD_STAT.GRAB;
                }
                else
                {
                    XrayStatusChange(false);
                    CStatus.Instance().CameraManager.StopGrap();
                    test = false;

                    CStatus.Instance().CurImageLoadStat = IMAGE_LOAD_STAT.NONE;
                }
            }
            catch (Exception err)
            {
                string function = MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(err.Message + "-" + function);
            }
        }

        private void lbHistogram_Click(object sender, EventArgs e)
        {
            if (CStatus.Instance().GetDrawBox().GetPictureBox().Image == null)
            {
                MessageBox.Show("Image is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ButtonInitialize();
            if (CStatus.Instance().HistogramForm == null)
            {
               
                CStatus.Instance().HistogramForm = new HistogramForm();
                CStatus.Instance().HistogramForm.CloseEventDelegate = () => CStatus.Instance().HistogramForm = null;
                CStatus.Instance().HistogramForm.Show();
            }

            //HistogramForm form = new HistogramForm();
            //form.Show();
        }

        private void HFrm_SelectedChanged(object sender, EventArgs e)
        {
        }

        private void lbl_Rotate_CCW_Click(object sender, EventArgs e)
        {
            if (CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage == null)
                return;
            IP_RotationCCW ccw = new IP_RotationCCW();
            CStatus.Instance().GetDrawBox().ImageManager.ImageProcessingList.Add(ccw);
            Bitmap image = CStatus.Instance().GetDrawBox().ImageManager.CalcDisplayImage().ToBitmap();

            CStatus.Instance().GetDrawBox().FigureRotation(eImageTransform.CCW);
            CStatus.Instance().GetDrawBox().GetPictureBox().Image = image;
            CStatus.Instance().GetDrawBox().ReUpdate();
        }

        private void lbl_Rotate_CW_Click(object sender, EventArgs e)
        {
            if (CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage == null)
                return;
            IP_RotationCW cw = new IP_RotationCW();
            CStatus.Instance().GetDrawBox().ImageManager.ImageProcessingList.Add(cw);
            Bitmap image = CStatus.Instance().GetDrawBox().ImageManager.CalcDisplayImage().ToBitmap();

            CStatus.Instance().GetDrawBox().FigureRotation(eImageTransform.CW);
            CStatus.Instance().GetDrawBox().GetPictureBox().Image = image;
            CStatus.Instance().GetDrawBox().ReUpdate();
        }

        private void lbl_flip_horizontal_Click(object sender, EventArgs e)
        {
            if (CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage == null)
                return;
            IP_FlipHorizontal horizontal = new IP_FlipHorizontal();
            CStatus.Instance().GetDrawBox().ImageManager.ImageProcessingList.Add(horizontal);
            Bitmap image = CStatus.Instance().GetDrawBox().ImageManager.CalcDisplayImage().ToBitmap();

            CStatus.Instance().GetDrawBox().FigureRotation(eImageTransform.FlipX);
            CStatus.Instance().GetDrawBox().GetPictureBox().Image = image;
            CStatus.Instance().GetDrawBox().ReUpdate();

            if (lbl_flip_horizontal.BorderStyle == BorderStyle.Fixed3D)
                lbl_flip_horizontal.BorderStyle = BorderStyle.None;
            else
                lbl_flip_horizontal.BorderStyle = BorderStyle.Fixed3D;
        }

        private void lbl_flip_vertical_Click(object sender, EventArgs e)
        {
            if (CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage == null)
                return;
            IP_FlipVetical vertical = new IP_FlipVetical();
            CStatus.Instance().GetDrawBox().ImageManager.ImageProcessingList.Add(vertical);
            Bitmap image = CStatus.Instance().GetDrawBox().ImageManager.CalcDisplayImage().ToBitmap();

            CStatus.Instance().GetDrawBox().FigureRotation(eImageTransform.FlipY);
            CStatus.Instance().GetDrawBox().GetPictureBox().Image = image;
            CStatus.Instance().GetDrawBox().ReUpdate();

            if (lbl_flip_vertical.BorderStyle == BorderStyle.Fixed3D)
                lbl_flip_vertical.BorderStyle = BorderStyle.None;
            else
                lbl_flip_vertical.BorderStyle = BorderStyle.Fixed3D;
        }

        private void lbSave_Click(object sender, EventArgs e)
        {
            FileSave();
        }

        private void lbLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (CStatus.Instance().IsAcqusitionExecute)
                {
                    MessageBox.Show("Camera Activated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FileOpen();
            }
            catch (Exception err)
            {
                string function = MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(err.Message + "-" + function);
            }
        }

        private void lbRuler_Click(object sender, EventArgs e)
        {
            if (CStatus.Instance().IsAcqusitionExecute)
            {
                MessageBox.Show("'Ruler' or 'Protractor' can not use during Image Acquisition",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CStatus.Instance().GetDrawBox().GetPictureBox().Image == null)
                return;

            if(lbRuler.BorderStyle == BorderStyle.Fixed3D)
            {
                lbRuler.BorderStyle = BorderStyle.None;
                CStatus.Instance().GetDrawBox().TrackerManager.DrawType = eDrawType.None;
                CStatus.Instance().GetDrawBox().TrackerManager.ModeType = eModeType.None;
            }
            else
            {
                lbProtractor.BorderStyle = BorderStyle.None;
                lbSelectRegion.BorderStyle = BorderStyle.None;
                lbLineProfile.BorderStyle = BorderStyle.None;
                lbRuler.BorderStyle = BorderStyle.Fixed3D;
                lblProfile.BorderStyle = BorderStyle.None;
                CStatus.Instance().GetDrawBox().TrackerManager.DrawType = eDrawType.LineMeasurement;
                CStatus.Instance().GetDrawBox().TrackerManager.ModeType = eModeType.Draw;
            }
        }

        private void lbProtractor_Click(object sender, EventArgs e)
        {
            if (CStatus.Instance().IsAcqusitionExecute)
            {
                MessageBox.Show("'Ruler' or 'Protractor' can not use during Image Acquisition",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CStatus.Instance().GetDrawBox().GetPictureBox().Image == null)
                return;

            if (lbProtractor.BorderStyle == BorderStyle.Fixed3D)
            {
                lbProtractor.BorderStyle = BorderStyle.None;
                CStatus.Instance().GetDrawBox().TrackerManager.DrawType = eDrawType.None;
                CStatus.Instance().GetDrawBox().TrackerManager.ModeType = eModeType.None;
            }
            else
            {
                lbProtractor.BorderStyle = BorderStyle.Fixed3D;
                lbSelectRegion.BorderStyle = BorderStyle.None;
                lbLineProfile.BorderStyle = BorderStyle.None;
                lbRuler.BorderStyle = BorderStyle.None;
                lblProfile.BorderStyle = BorderStyle.None;
                CStatus.Instance().GetDrawBox().TrackerManager.DrawType = eDrawType.Protractor;
                CStatus.Instance().GetDrawBox().TrackerManager.ModeType = eModeType.Draw;
            }
        }

        private void lbSelectRegion_Click(object sender, EventArgs e)
        {
            if (CStatus.Instance().IsAcqusitionExecute)
            {
                MessageBox.Show("'Region Select' can not use during Image Acquisition",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CStatus.Instance().GetDrawBox().GetPictureBox().Image == null)
                return;

            if (lbSelectRegion.BorderStyle == BorderStyle.Fixed3D)
            {
                lbSelectRegion.BorderStyle = BorderStyle.None;
                CStatus.Instance().GetDrawBox().TrackerManager.DrawType = eDrawType.None;
                CStatus.Instance().GetDrawBox().TrackerManager.ModeType = eModeType.None;
            }
            else
            {
                lbProtractor.BorderStyle = BorderStyle.None;
                lbSelectRegion.BorderStyle = BorderStyle.Fixed3D;
                lbLineProfile.BorderStyle = BorderStyle.None;
                lbRuler.BorderStyle = BorderStyle.None;
                lblProfile.BorderStyle = BorderStyle.None;
                CStatus.Instance().GetDrawBox().TrackerManager.DrawType = eDrawType.Roi;
                CStatus.Instance().GetDrawBox().TrackerManager.ModeType = eModeType.Draw;
            }
        }

        private void lbReset_Click(object sender, EventArgs e)
        {
            lbProtractor.BorderStyle = BorderStyle.None;
            lbSelectRegion.BorderStyle = BorderStyle.None;
            lbRuler.BorderStyle = BorderStyle.None;
            lblProfile.BorderStyle = BorderStyle.None;

            if (CStatus.Instance().GetDrawBox().GetPictureBox().Image == null)
                return;
            CStatus.Instance().GetDrawBox().TrackerManager.DrawType = eDrawType.None;
            CStatus.Instance().GetDrawBox().TrackerManager.ModeType = eModeType.None;
            CStatus.Instance().GetDrawBox().TrackerManager.ClearFigure();
            CStatus.Instance().GetDrawBox().ReUpdate();
        }

        private void lbFilter_Click(object sender, EventArgs e)
        {
            if (CStatus.Instance().GetDrawBox().GetPictureBox().Image == null)
            {
                MessageBox.Show("Image is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "Filter")
                {
                    item.Activate();
                    return;
                }
            }
            FilterForm form = new FilterForm();
            form.Show();
        }

        private void lbLineProfile_Click(object sender, EventArgs e)
        {
            if (CStatus.Instance().IsAcqusitionExecute)
            {
                MessageBox.Show("Camera Activated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ButtonInitialize();
            CStatus.Instance().ProfileFormOpen();
        }

        private void lbRegionHisto_Click(object sender, EventArgs e)
        {

            if (CStatus.Instance().IsAcqusitionExecute)
            {
                MessageBox.Show("Camera Activated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ButtonInitialize();
            CStatus.Instance().RoiFormOpen();
        }

        private void lbFovSetting_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "SetUp")
                {
                    frm.Activate();
                    return;
                }
            }

            SetUpForm form = new SetUpForm();
            form.Show();
        }

        private void lbl_Settings_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Text == "Settings")
                    return;
            }

            //Settings form = new Settings();
            //form.TopMost = true;
            //form.Show();
        }

        private void lbl_CameraSettings_Click(object sender, EventArgs e)
        {
            //CStatus.Instance().CameraManager.ShowParamForm();
            #region 기존
            //foreach (Form openForm in Application.OpenForms)
            //{
            //    if (openForm.Text == "CameraParams")
            //        return;
            //}

            //ThalesCameraParamForm form = new ThalesCameraParamForm();
            //form.Show();
            #endregion

        }

        private void lbl_Capture_Click(object sender, EventArgs e)
        {
            /*if (CStatus.Instance().ProgramMode == PMMode.PM_NONE)
            {
                MessageBox.Show("NO Patient Information ");
                return;
            }
            if (CStatus.Instance().ProgramMode == PMMode.PM_LOCAL_VIEW)
            {
                MessageBox.Show("Now View mode.");
                return;
            }
            if (CStatus.Instance().ProgramMode != PMMode.PM_LOCAL_VIEW)*/
            {
                CStatus.Instance().IsCapture = true;
                if (lblAcqusitionState.Text == "Cam OFF")
                    CStatus.Instance().HoldLiveImageCaptureUpdate();
            }
        }

        private void lbl_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();

                if (MessageBox.Show("Do you want to exit the program?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    CStatus.Instance().CameraManager.Close();

                    this.Cursor = Cursors.Default;
                    Application.Exit();
                    CStatus.Instance().ErrorLogManager.WriteErrorLog(MethodBase.GetCurrentMethod().Name, "Close");
                }
            }
            catch (Exception err)
            {
                string function = MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(err.Message + "-" + function);
            }
        }
        #endregion

        private void InitializeAfterLoadImage()
        {
            int histoMin = CStatus.Instance().HistoSelectedMin;
            int histoMax = CStatus.Instance().HistoSelectedMax;

            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "HistogramForm" ||
                    item.Name == "LineProfileFrm" ||
                    item.Name == "RegionHistoFrm" ||
                    item.Name == "FilterFrm" ||
                    item.Name == "FilterMultipleFrm" ||
                    item.Name == "FovSettingForm")
                {
                    item.Close();
                }
            }

            //CStatus.Instance().LatestImageArr8bitShown = new byte[CStatus.Instance().LatestImageArr.Length];
            //KHistogram.WndLvlTranform(
            //    CStatus.Instance().LatestImageArr8bitShown,
            //    CStatus.Instance().LatestImageArr, histoMin, histoMax);
            CStatus.Instance().CurImageLoadStat = IMAGE_LOAD_STAT.MANUAL;
            lbReset_Click(null, null);   //자, 각도기 Clear
            lbHistogram_Click(null, null);
        }

        private void CheckXrayOn(bool xrayIsOn)
        {
            try
            {
                if (this._preXrayIsOn == xrayIsOn)
                    return;

                if (xrayIsOn == true && this._preXrayIsOn == false) // Xray On 
                {
                    if (CStatus.Instance().IsTestMode != true)
                    {
                        CStatus.Instance().DelayStopWatch.Restart();
                        CStatus.Instance().CameraManager.StartContinuousGrap();
                        XrayStatusChange(true);
                    }
                }
                if (xrayIsOn == false && this._preXrayIsOn == true) // XrayOff
                {
                    if (CStatus.Instance().IsTestMode != true)
                    {
                        CStatus.Instance().DelayStopWatch.Stop();
                        CStatus.Instance().CameraManager.StopGrap();
                        XrayStatusChange(false);
                    }
                }

                this._preXrayIsOn = xrayIsOn;
            }
            catch (Exception err)
            {
                string function = MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(err.Message + "-" + function);
            }
        }

        private delegate void XrayStatusChangeDelegate(bool isAcqusitionExecute);
        private void XrayStatusChange(bool isAcqusitionExecute)
        {
            if (this.InvokeRequired)
            {
                XrayStatusChangeDelegate callback = XrayStatusChange;
                Invoke(callback, isAcqusitionExecute);
                return;
            }
            if (isAcqusitionExecute == true)
            {
                if (lblAcqusitionState.Text != "Cam ON")
                {
                    this._isAcqusitionExecute = true;
                    lbl_Aquisition.ImageIndex = 1;
                    lblAcqusitionState.Text = "Cam ON";
                    CStatus.Instance().IsAcqusitionExecute = true;
                }
            }
            else
            {
                if (lblAcqusitionState.Text != "Cam OFF")
                {
                    this._isAcqusitionExecute = false;
                    lbl_Aquisition.ImageIndex = 0;
                    lblAcqusitionState.Text = "Cam OFF";
                    CStatus.Instance().IsAcqusitionExecute = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                if (this._isAdministratorMode == false)
                {
                    this._isAdministratorMode = true;
                    lbl_CameraSettings.Visible = true;
                    lbl_Settings.Visible = true;
                }
                else
                {
                    this._isAdministratorMode = false;
                    lbl_CameraSettings.Visible = false;
                    lbl_Settings.Visible = false;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnTestInputImage_Click(object sender, EventArgs e)
        {
            //if (start == false)
            //{
            //    start = true;
            //    timerTest.Start();
            //}
            //else
            //{
            //    timerTest.Stop();
            //    start = false;
            //}
            // SerialCommunication sc = new SerialCommunication();
            // sc.SendBrightValueToXray(12345);
            //string filePath = @"D:\TestImage\Raw0.raw";
            //int bufferSize = 1344 * 1344 * 2;
            //ushort[] ushort_file = new ushort[bufferSize];

            //byte[] fileBytes = ImageHelper.GetRawImage(filePath);
            //MemoryStream ms = new MemoryStream(fileBytes);
            //BinaryReader read = new BinaryReader(ms);
            //int index = 0;
            //while (ms.Position < ms.Length)
            //{
            //    ushort_file[index] = (ushort)read.ReadUInt16();
            //    index++;
            //}
            //// Crop(ushort_file, 672, 672, 600, 1344, 1344);
            //ImageHelper.SaveuShortRawData(Crop(ushort_file, 672, 672, 600, 1344, 1344), 600*600, @"D:\dydwo.raw");
            //CStatus.Instance().CameraManager.TestAvg(fileBytes);
        }

        private ushort[] Crop(ushort[] buffer, int centerX, int centerY, int length, int imageWidth, int imageHeight)
        {
            int leftTopX = centerX - (length / 2);
            int leftTopY = centerY - (length / 2);

            int newBufferSize = length * length;
            ushort[] newBuffer = new ushort[newBufferSize];
            try
            {
                int index = 0;
                for (int w = 0; w < imageWidth; w++)
                {
                    for (int h = 0; h < imageHeight; h++)
                    {
                        if (w >= leftTopX && w < leftTopX + length)
                        {
                            if (h >= leftTopY && h < leftTopY + length)
                            {
                                newBuffer[index] = buffer[w * imageWidth + h];
                                index++;
                            }
                        }
                    }
                }

            }
            catch (Exception err)
            {
                string function = MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(err.Message + "-" + function);
            }

            return newBuffer;
        }

        private void HFrm_SaveClicked(object sender, EventArgs e)
        {
            //CStatus.Instance().SaveHistogram();
        }

        private void FileOpen()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (CStatus.Instance().Settings.Language == eLanguageType.Korea)
                {
                    dialog.Filter = "이미지 파일(*.jpeg,*.jpg,*.png,*.bmp,*.tif,*.tiff,*.raw,*.dcm) |*.jpeg;*.jpg;*.png;*.bmp;*.tif;*.tiff;*.raw;*.dcm;|"
                        + "jpeg 파일(*.jpeg)|*.jpeg; |"
                        + "jpg 파일(*.jpg)|*.jpg; |"
                        + "png 파일(*.png) | *.png; |"
                        + "bmp 파일(*.bmp) | *.bmp; |"
                        + "tif 파일(*.tif,*.tiff) | *.tif;*.tiff;|"
                        + "raw 파일(*.raw) | *.raw;|"
                        + "dcm 파일(*.dcm) | *.dcm;|"
                        + "모든 파일(*.*) | *.*;";
                }
                else
                {
                    dialog.Filter = "Image files(*.jpeg,*.png,*.bmp,*.tif,*.raw,*.dcm) |*.jpeg;*.png;*.bmp;*.tif;*.raw;*.dcm;|"
                         + "jpeg file(*.jpeg)|*.jpeg; |"
                         + "png file(*.png) | *.png; |"
                         + "bmp file(*.bmp) | *.bmp; |"
                         + "tif file(*.tif, *.tiff) | *.tif;*.tiff;|"
                         + "raw file(*.raw) | *.raw;|"
                         + "dcm file(*.dcm) | *.dcm;|"
                         + "All files(*.*) | *.*;";
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    JImage image = new JImage(dialog.FileName.ToString());
                    image.GetHistoAvgGray();
                    if (image.Color == KiyLib.DIP.KColorType.Color)
                    {
                        MessageBox.Show("8,16 bit 흑백이미지만 가능합니다.");
                        return;
                    }

                    CStatus.Instance().Settings.HistogramMin = 0;
                        
                    if (image.Depth == KiyLib.DIP.KDepthType.Dt_8)
                        CStatus.Instance().Settings.HistogramMax = 255;
                    else if(image.Depth == KiyLib.DIP.KDepthType.Dt_16)
                        CStatus.Instance().Settings.HistogramMax = 65535;

                    CStatus.Instance().GetDrawBox().TrackerManager.ClearFigure();
                    CStatus.Instance().GetDrawBox().ImageManager.SetOrgImage(image);
                    Bitmap bmp = CStatus.Instance().GetDrawBox().ImageManager.CalcDisplayImage().ToBitmap();

                    CStatus.Instance().GetDrawBox().LoadFile(bmp);
                    //CStatus.Instance().Settings.Save();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The file is not supported format.");
            }
        }

        private void FileSave()
        {
            if (CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage ==null)
                return;
            SaveFileDialog dialog = new SaveFileDialog();
            if (CStatus.Instance().Settings.Language == eLanguageType.Korea)
            {
                dialog.Filter = "이미지 파일(*.jpeg,*.png,*.bmp,*.tif,*.raw,*.dcm) |*.jpeg;*.png;*.bmp;*.tif;*.raw;*.dcm;|"
                       + "jpeg 파일(*.jpeg)|*.jpeg; |"
                       + "png 파일(*.png) | *.png; |"
                       + "bmp 파일(*.bmp) | *.bmp; |"
                       + "tif 파일(*.tif, *.tiff) | *.tif;*.tiff;|"
                       + "raw 파일(*.raw) | *.raw;|"
                       + "dcm 파일(*.dcm) | *.dcm;|"
                       + "모든 파일(*.*) | *.*;";

            }
            else
            {
                dialog.Filter = "Image files(*.jpeg,*.png,*.bmp,*.tif,*.raw,*.dcm) |*.jpeg;*.png;*.bmp;*.tif;*.raw;*.dcm;|"
                     + "jpeg file(*.jpeg)|*.jpeg; |"
                     + "png file(*.png) | *.png; |"
                     + "bmp file(*.bmp) | *.bmp; |"
                     + "tif file(*.tif, *.tiff) | *.tif;*.tiff;|"
                     + "raw file(*.raw) | *.raw;|"
                     + "dcm file(*.dcm) | *.dcm;|"
                     + "All files(*.*) | *.*;";
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Save(dialog.FileName.ToString());
                 
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        public void PanelUpdate(eDrawType type)
        {
            lbRuler.BorderStyle = BorderStyle.None;
            lbProtractor.BorderStyle = BorderStyle.None;
            lbSelectRegion.BorderStyle = BorderStyle.None;
            lblProfile.BorderStyle = BorderStyle.None;
            switch (type)
            {
                case eDrawType.LineMeasurement:
                    lbRuler.BorderStyle = BorderStyle.Fixed3D;
                    break;
                case eDrawType.Roi:
                    lbSelectRegion.BorderStyle = BorderStyle.Fixed3D;
                    break;
                case eDrawType.Protractor:
                    lblProfile.BorderStyle = BorderStyle.Fixed3D;
                    break;
                case eDrawType.Profile:
                    lbLineProfile.BorderStyle = BorderStyle.Fixed3D;
                    break;
                case eDrawType.Project:
                    break;
                default:
                    break;
            }
        }

        private void lblProfile_Click(object sender, EventArgs e)
        {
            if (CStatus.Instance().IsAcqusitionExecute)
            {
                MessageBox.Show("'Region Select' can not use during Image Acquisition",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CStatus.Instance().GetDrawBox().GetPictureBox().Image == null)
                return;

            if (lblProfile.BorderStyle == BorderStyle.Fixed3D)
            {
                lblProfile.BorderStyle = BorderStyle.None;
                CStatus.Instance().GetDrawBox().TrackerManager.DrawType = eDrawType.None;
                CStatus.Instance().GetDrawBox().TrackerManager.ModeType = eModeType.None;
            }
            else
            {
                lbProtractor.BorderStyle = BorderStyle.None;
                lbSelectRegion.BorderStyle = BorderStyle.None;
                lbLineProfile.BorderStyle = BorderStyle.None;
                lbRuler.BorderStyle = BorderStyle.None;
                lblProfile.BorderStyle = BorderStyle.Fixed3D;
                CStatus.Instance().GetDrawBox().TrackerManager.DrawType = eDrawType.Profile;
                CStatus.Instance().GetDrawBox().TrackerManager.ModeType = eModeType.Draw;
            }
        }

        private void ButtonInitialize()
        {
            lbProtractor.BorderStyle = BorderStyle.None;
            lbSelectRegion.BorderStyle = BorderStyle.None;
            lbLineProfile.BorderStyle = BorderStyle.None;
            lbRuler.BorderStyle = BorderStyle.None;
            lblProfile.BorderStyle = BorderStyle.None;
            CStatus.Instance().GetDrawBox().TrackerManager.DrawType = eDrawType.None;
            CStatus.Instance().GetDrawBox().TrackerManager.ModeType = eModeType.None;
        }

        private void lblProcessingTime_Click(object sender, EventArgs e)
        {
            CStatus.Instance().OpenProcessingTimeForm();
        }
    }
}
