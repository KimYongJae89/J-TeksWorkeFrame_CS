using Aladdin.HASP;
using ImageManipulator.Controls;
using ImageManipulator.Data;
using ImageManipulator.Forms;
using ImageManipulator.ImageProcessingData;
using ImageManipulator.Util;
using KiyEmguCV.DIP;
using KiyLib.DIP;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ImageManipulator
{

    public class Status
    {
        private ImageManipulator _mainForm;
       
        public int FovWidth = 100;
        public int FovHeight = 100;
        public double Pitch = 0;
        public int ProfileWidth = 1;
        public eMeasurementUnit MeasurementUnit = eMeasurementUnit.MM;

        public bool IsDrawingRoi = false;
        public bool IsDrawingProfile = false;
        private List<string> _recentFiles = new List<string>(5);

        public Viewer SelectedViewer = null;
        public Viewer PrevSelectedViewer = null;

        public RoiListForm RoiListForm = null;
        public HistogramForm HistogramForm = null;

        public FilterForm FilterForm = null;
        public ProfileListForm ProfileListForm = null;

        public MeasurementSettingForm MeasureMentForm = null;
        public ThresholdForm ThresholdForm = null;
        public ProcessingTimeForm ProcessingTimeForm = null;

        public Action DrawTypeResetDelegate;

        public LogHelper LogManager = new LogHelper();
        public eDerivativeType DerivativeType = eDerivativeType.None;
        public eLanguageType Language = eLanguageType.English;

        public eMeasurementType MeasureMentType = eMeasurementType.Fov_Calibration;

        private Stopwatch _licenseStopWatch = new Stopwatch();
        public List<ProcessingTime> ProcessingTime = new List<ProcessingTime>();

        private bool _threadRun = false;
        public static Status _instance;

        public static Status Instance()
        {
            if (_instance == null)
            {
                _instance = new Status();
            }
            return _instance;
        }

        public void SetMainForm(ImageManipulator mainForm)
        {
            this._mainForm = mainForm;
        }

        public ImageManipulator GetMainForm()
        {
            return this._mainForm;
        }
        public void SaveRoiXml(string filePath)
        {
            List<Figure> figureGroup = SelectedViewer.GetDrawBox().GetFigureGroup();
            if (figureGroup != null)
            {
                XmlWriterManager xmlWriter = XmlWriterBuilder.Create(filePath, eXmlType.Roi);
                xmlWriter.Write(filePath, figureGroup);
            }
        }

        public void LoadRoiXml(string filePath)
        {
            XmlReaderManager roiReader = XmlReaderBuilder.Create(filePath, eXmlType.Roi);

            if (roiReader != null)
            {
                List<Figure> figureGroup = this.SelectedViewer.GetDrawBox().GetFigureGroup();

                figureGroup.Clear();
                figureGroup.AddRange(roiReader.Load<Figure>(filePath));
                Status.Instance().SelectedViewer.GetDrawBox().FigureSelectedClear();// ClearFigureSelected();
            }
            else
            {
                MessageBox.Show(LangResource.LoadFilterListMessage);
            }
        }

        public void SaveFilterList(string filePath)
        {
            XmlWriterManager xmlWriter = XmlWriterBuilder.Create(filePath, eXmlType.FilterList);
            List<string> empty = new List<string>();
            xmlWriter.Write(filePath, empty);
        }

        public void LoadFilterList(string filePath)
        {
            XmlReaderManager xmlReader = XmlReaderBuilder.Create(filePath, eXmlType.FilterList);
            if (xmlReader != null)
            {
                xmlReader.Load<string>(filePath);
            }
            else
            {
                MessageBox.Show(LangResource.LoadFilterListMessage);
            }
            Status.Instance().SelectedViewer.GetDrawBox().Image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
        }

        public void AddRecentFile(string filePath)
        {
            if (_recentFiles.Count > 5)
                _recentFiles.RemoveAt(0);

            _recentFiles.Add(filePath);
        }

        public void DeleteRecentFile(string filePath)
        {
            _recentFiles.Remove(filePath);
        }

        public void DeleteRecentFile(int index)
        {
            _recentFiles.RemoveAt(index);
        }

        public List<string> GetRecentFiles()
        {
            return _recentFiles;
        }

        public void SaveSettings()
        {
            string currentDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ImageManipulator");

            DirectoryInfo di = new DirectoryInfo(currentDir);
            if (!di.Exists)
                di.Create();

            string filePath = Path.Combine(currentDir, "Settings.xml");
            List<string> empty = new List<string>();
            XmlWriterManager xmlWriter = XmlWriterBuilder.Create(filePath, eXmlType.Settings);
            xmlWriter.Write(filePath, empty);
        }

        public void LoadSettings()
        {
            string currentDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ImageManipulator");

            DirectoryInfo di = new DirectoryInfo(currentDir);
            if (!di.Exists)
                di.Create();

            string filePath = Path.Combine(currentDir, "Settings.xml");

            FileInfo info = new FileInfo(filePath);
            if (!info.Exists)
            {
                SaveSettings();
                return;
            }

            XmlReaderManager SettingsReader = XmlReaderBuilder.Create(filePath, eXmlType.Settings);

            if (SettingsReader != null)
            {
                SettingsReader.Load<string>(filePath);
            }
        }

        public void GetNextFileOpen()
        {
            if (!Status.Instance().SelectedViewer.EnableKeyEvent)
                return;
            string selectedImageFilePath = Status.Instance().SelectedViewer.ImageManager.ImageFilePath;
            JImage displayImage = Status.Instance().SelectedViewer.ImageManager.GetOrgImage();

            string displayImageFileName = Path.GetFileName(selectedImageFilePath);
            string fileFolder = Path.GetDirectoryName(selectedImageFilePath);
            DirectoryInfo di = new DirectoryInfo(fileFolder);

            bool isSearched = false;
            foreach (FileInfo File in di.GetFiles())
            {
                if(isSearched)
                {
                    if (!IsEnableFileFormat(File.FullName))
                        continue;
                    JImage newImage = new JImage(File.FullName);

                    if (newImage.Width == displayImage.Width && newImage.Height == displayImage.Height)
                    {
                        //Console.WriteLine(File.FullName);
                        Status.Instance().SelectedViewer.ImageManager.SetOrgImage((JImage)newImage.Clone());
                        Status.Instance().SelectedViewer.Text = Path.GetFileNameWithoutExtension(File.FullName);

                        Status.Instance().SelectedViewer.ImageManager.ImageFilePath = File.FullName;
                        Status.Instance().SelectedViewer.GetDrawBox().Image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
                        Status.Instance().UpdateSubForm(true);
                        Status.Instance().LogManager.AddLogMessage("Move File", "Right");
                        return;
                    }
                    else
                        continue;
                }
                if(File.Name == displayImageFileName)
                {
                    isSearched = true;
                }
            }
        }

        public void GetPrevFileOpen()
        {
            if (!Status.Instance().SelectedViewer.EnableKeyEvent)
                return;
            string selectedImageFilePath = Status.Instance().SelectedViewer.ImageManager.ImageFilePath;
            JImage displayImage = Status.Instance().SelectedViewer.ImageManager.GetOrgImage();

            string displayImageFileName = Path.GetFileName(selectedImageFilePath);
            string fileFolder = Path.GetDirectoryName(selectedImageFilePath);
            DirectoryInfo di = new DirectoryInfo(fileFolder);
            FileInfo[] info = di.GetFiles();

            bool isSearched = false;
            for (int i = info.Count() - 1; i >= 0; i--)
            {
                if (isSearched)
                {
                    if (!IsEnableFileFormat(info[i].FullName))
                        continue;
                    JImage newImage = new JImage(info[i].FullName);
                    if (newImage.Width == displayImage.Width && newImage.Height == displayImage.Height)
                    {
                        //Console.WriteLine(info[i].FullName);
                        Status.Instance().SelectedViewer.ImageManager.SetOrgImage((JImage)newImage.Clone());
                        Status.Instance().SelectedViewer.Text = Path.GetFileNameWithoutExtension(info[i].FullName);

                        Status.Instance().SelectedViewer.ImageManager.ImageFilePath = info[i].FullName;
                        Status.Instance().SelectedViewer.GetDrawBox().Image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
                        Status.Instance().UpdateSubForm(true);
                        Status.Instance().LogManager.AddLogMessage("Move File", "Left");
                        return;
                    }
                    else
                        continue;
                }
                if (info[i].Name == displayImageFileName)
                {
                    isSearched = true;
                }
            }
        }

        private bool IsEnableFileFormat(string filePath)
        {
            //*.jpeg,*.png,*.bmp,*.tif,*.tiff,*.raw,*.dcm
            bool ret = false;
            string format = Path.GetExtension(filePath).ToLower();
            if (format == ".jpeg")
                ret = true;
            else if (format == ".png")
                ret = true;
            else if (format == ".bmp")
                ret = true;
            else if (format == ".tif")
                ret = true;
            else if (format == ".tiff")
                ret = true;
            else if (format == ".raw")
                ret = true;
            else if (format == ".dcm")
                ret = true;
            else
                ret = false;

            return ret;
        }

        public bool CheckReOpening(string formText)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Text == formText)
                    return true;
            }
            return false;
        }

        public void HistogramFormUpdate()
        {
            if(this.HistogramForm != null)
            {
                this.HistogramForm.HistogramPanelDataGridNewUpdate();
                this.HistogramForm.HistogramGraphReNewal();
                this.HistogramForm.MarkReset();
            }
        }

        public void RoiListFormUpdate()
        {
            if(this.RoiListForm != null)
            {
                if (Status.Instance().SelectedViewer.GetDrawBox().GetRoiCount() <= 0)
                    return;

                this.RoiListForm.RoiDatagridUpdate();
                this.RoiListForm.HistogramPanelDataGridNewUpdate();
                this.RoiListForm.HistogramGraphReNewal();
                this.RoiListForm.MarkReset();
            }
        }

        public void ProfileListFormUpdate()
        {
            if(this.ProfileListForm !=null)
            {
                if (Status.Instance().SelectedViewer.GetDrawBox().GetProfileCount() <= 0)
                    return;
                this.ProfileListForm.ProfileDataGridClearReUpdate();
                this.ProfileListForm.HistogramPanelDataGridNewUpdate();
                this.ProfileListForm.HistogramGraphReNewal();
                //this.ProfileListForm.MarkReset();
            }
        }

        public void MesurementFormOpen()
        {
            if (this.MeasureMentForm == null)
            {
                this.MeasureMentForm = new MeasurementSettingForm();
                this.MeasureMentForm.CloseEventDelegate = () => this.MeasureMentForm = null;
                this.MeasureMentForm.Show();
            }
        }

        public void HistogramFormOpen()
        {
            if (this.HistogramForm == null)
            {
                this.HistogramForm = new HistogramForm();
                this.HistogramForm.CloseEventDelegate = () => this.HistogramForm = null;
                this.HistogramForm.Show();
            }
        }

        public void FilterFormOpen()
        {
            if (this.FilterForm == null)
            {
                this.FilterForm = new FilterForm();
                this.FilterForm.CloseEventDelegate = () => this.FilterForm = null;
                this.FilterForm.Show();
            }
            else
            {
                //this.FilterForm.SelectTabNum(selectTabIndex);
            }
        }

        public void ThresholdFormOpen()
        {
            if(this.ThresholdForm == null)
            {
                this.ThresholdForm = new ThresholdForm();
                this.ThresholdForm.CloseEventDelegate = () => this.ThresholdForm = null;
                this.ThresholdForm.Show();
            }
        }

        public void RoiListFormOpen()
        {
            if(RoiListForm == null)
            {   
                RoiListForm = new RoiListForm();
                RoiListForm.DataGridViewClickDelegate = (id) => this.SelectedViewer.GetDrawBox().RoiSelectUpdate(id);
                RoiListForm.CloseEventDelegate = () => this.RoiListForm = null;
                RoiListForm.Show();
            }
        }

        public void UpdateRoiToRoiForm(eFormUpdate type)
        {
            if (this.SelectedViewer == null)
                return;
            int roiCount = Status.Instance().SelectedViewer.GetDrawBox().GetRoiCount();
            if (roiCount <= 0)
            {
                if (this.RoiListForm != null)
                {
                    this.RoiListForm.DatagridViewClear();
                }
                return;
            }

            if(type != eFormUpdate.Delete)
                RoiListFormOpen();

            if (this.RoiListForm != null)
            {
                if (this.SelectedViewer.GetDrawBox().GetRoiCount() <= 0)
                {
                    this.RoiListForm.DatagridViewClear();
                    return;
                }
                if (this.SelectedViewer.GetDrawBox().GetRoiCount() == 1)
                {
                    RoiListFormUpdate();
                }
                this.RoiListForm.RoiDatagridUpdate();
                this.RoiListForm.SelectedHistogramUpdate();
            }
        }

        public void UpdateProfileToForm(eFormUpdate type)
        {
            if (this.SelectedViewer == null)
                return;
            int profileCount = Status.Instance().SelectedViewer.GetDrawBox().GetProfileCount();
            if (profileCount <= 0)
            {
                if(this.ProfileListForm != null)
                {
                    this.ProfileListForm.ProfileDataGridClearReUpdate();
                }
                return;
            }
            if(type != eFormUpdate.Delete)
                ProfileListFormOpen();

            if (this.ProfileListForm != null)
            {
                if (this.SelectedViewer.GetDrawBox().GetProfileCount() <= 0)
                    return;
                if (this.SelectedViewer.GetDrawBox().GetProfileCount() == 1)
                {
                    ProfileListFormUpdate();
                }
                this.ProfileListForm.ProfileDataGridClearReUpdate();
                this.ProfileListForm.HistogramGraphReNewal();
            }
        }

        
        public void RoiListFormMoveUpdate(RectangleF orgRoi)
        {
            if (this.SelectedViewer.GetDrawBox().Image == null)
                return;

            if (this.RoiListForm != null)
            {
                this.RoiListForm.MoveEventUpdate(orgRoi);
            }
        }

        public void ProfileListFormMoveUpdate(List<HistogramParams> param, PointF orgStartPt, PointF orgEndPt)
        {
            if (this.SelectedViewer.GetDrawBox().Image == null)
                return;

            if (this.ProfileListForm != null)
            {
                this.ProfileListForm.MoveEventUpdate(param, orgStartPt, orgEndPt);
            }
        }

        public void SelectRoi(int id)
        {
            if (this.SelectedViewer.GetDrawBox().Image == null)
                return;
            if (this.RoiListForm != null)
            {
                this.RoiListForm.RoiSelect(id);
            }
        }

        public void SelectProfile(int id)
        {
            if (this.SelectedViewer.GetDrawBox().Image == null)
                return;
            if (this.ProfileListForm != null)
            {
                this.ProfileListForm.ProfileSelect(id);
            }
        }

        public string GetNewFileName(string sourceImage)
        {
            //string str = Regex.Replace(sourceImage, @"\d", "");
            //string newFileName = str;
            string newFileName = sourceImage;
            int formCount = 0;
            foreach (Form openForm in Application.OpenForms)
            {
                string tag = (string)openForm.Tag;
                if (tag == "Viewer")
                    formCount++;
            }

            int index = 0;
            for (int i = 0; i < formCount; i++)
            {
                string formName = sourceImage + "(" + index.ToString() + ")";
                if (IsNameExist(formName))
                {
                    index++;
                }

            }
            newFileName += "(" + index.ToString() + ")";
            return newFileName;
        }

        private bool IsNameExist(string formName)
        {
            bool ret = false;
            foreach (Form openForm in Application.OpenForms)
            {
                string tag = (string)openForm.Tag;
                if (tag == "Viewer")
                {
                    if (formName == openForm.Text)
                        ret = true;
                }
            }
            return ret;
        }

        public void UpdateFilterForm(bool isNewUpdate = false)
        {
            if (this.FilterForm != null)
            {
                if (!isNewUpdate)
                    this.FilterForm.UpdateForm();
                else
                    this.FilterForm.ComboBoxUpdate();
            }
        }

        public void UpdateThresholdForm()
        {
            if(this.ThresholdForm != null)
            {
                this.ThresholdForm.UpdateForm();
            }
        }
        public void ViewerFormClose()
        {
            if(this.FilterForm != null)
            {
                this.FilterForm.CloseViewerEvent();
            }
        }

        public void ProfileListFormOpen()
        {
            if(ProfileListForm == null)
            {
                this.ProfileListForm = new ProfileListForm();
                this.ProfileListForm.DataGridViewClickDelegate = (id) => this.SelectedViewer.GetDrawBox().ProfileSelectUpdate(id);
                this.ProfileListForm.CloseEventDelegate = () => this.ProfileListForm = null;
                this.ProfileListForm.Show();
            }
        }

        public void UpdateSubForm(bool isNewUpdate = false)
        {
            Status.Instance().HistogramFormUpdate();

            if (Status.Instance().SelectedViewer.GetDrawBox().GetRoiCount() >= 0)
                Status.Instance().RoiListFormUpdate();
            if (Status.Instance().SelectedViewer.GetDrawBox().GetProfileCount() >= 0)
                Status.Instance().ProfileListFormUpdate();

            Status.Instance().UpdateThresholdForm();
            Status.Instance().UpdateFilterForm(isNewUpdate);
        }

        public bool IsOpenViewerForm()
        {
            bool isOpen = false;
            foreach (Form openForm in Application.OpenForms)
            {
                string tag = (string)openForm.Tag;
                if (tag == "Viewer")
                {
                    isOpen = true;
                    return isOpen;
                }
            }

            return isOpen;
        }

        public void MainDrawButtonUpdate()
        {
            if (_mainForm != null)
                _mainForm.PanelSelectedUpdate();
        }

        public void Save(string filePath)
        {
            Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Save(filePath);
        }

        /// <summary>
        /// Directory 내부 전체 저장
        /// </summary>
        public void AllSaveInDirectory()
        {
            string selectedImageFilePath = Status.Instance().SelectedViewer.ImageManager.ImageFilePath;
            if (selectedImageFilePath == null || selectedImageFilePath == "")
                return;

            JImage displayImage = Status.Instance().SelectedViewer.ImageManager.GetOrgImage();
            bool orgIsColor = Status.Instance().SelectedViewer.ImageManager.IsColor();
            string displayImageFileName = Path.GetFileName(selectedImageFilePath);
            string fileFolder = Path.GetDirectoryName(selectedImageFilePath);
            DirectoryInfo di = new DirectoryInfo(fileFolder);
            FileInfo[] info = di.GetFiles();

            foreach (FileInfo File in di.GetFiles())
            {
                if (!IsEnableFileFormat(File.FullName))
                    continue;
                JImage newImage = new JImage(File.FullName);

                if (newImage.Width == displayImage.Width && newImage.Height == displayImage.Height)
                {
                    if (this.IsColor(newImage) != orgIsColor)
                        continue;
                    Status.Instance().SelectedViewer.ImageManager.SetOrgImage((JImage)newImage.Clone());
                    Status.Instance().SelectedViewer.ImageManager.ImageFilePath = File.FullName;
                    JImage destImage = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage();
                    destImage.Save(File.FullName);
                }
                else
                    continue;
            }

            Status.Instance().SelectedViewer.ImageManager.SetOrgImage((JImage)displayImage.Clone());
            Status.Instance().SelectedViewer.Text = Path.GetFileNameWithoutExtension(selectedImageFilePath);

            Status.Instance().SelectedViewer.ImageManager.ImageFilePath = selectedImageFilePath;
            Status.Instance().SelectedViewer.GetDrawBox().Image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
            Status.Instance().UpdateSubForm(true);
        }

        public bool IsColor(JImage image)
        {
            bool isColor = false;
            switch (image.Color)
            {
                case KColorType.Gray:
                    isColor = false;
                    break;
                case KColorType.Color:
                    isColor = true;
                    break;
                default:
                    break;
            }
            return isColor;
        }
        /// <summary>
        /// Sentinel LDK License 검사
        /// </summary>
        /// <returns>검사 여부</returns>
        private bool CheckLicense()
        {
            HaspFeature feature = HaspFeature.FromFeature(5);

            string vendorCode =
            "AzIceaqfA1hX5wS+M8cGnYh5ceevUnOZIzJBbXFD6dgf3tBkb9cvUF/Tkd/iKu2fsg9wAysYKw7RMAsV" +
            "vIp4KcXle/v1RaXrLVnNBJ2H2DmrbUMOZbQUFXe698qmJsqNpLXRA367xpZ54i8kC5DTXwDhfxWTOZrB" +
            "rh5sRKHcoVLumztIQjgWh37AzmSd1bLOfUGI0xjAL9zJWO3fRaeB0NS2KlmoKaVT5Y04zZEc06waU2r6" +
            "AU2Dc4uipJqJmObqKM+tfNKAS0rZr5IudRiC7pUwnmtaHRe5fgSI8M7yvypvm+13Wm4Gwd4VnYiZvSxf" +
            "8ImN3ZOG9wEzfyMIlH2+rKPUVHI+igsqla0Wd9m7ZUR9vFotj1uYV0OzG7hX0+huN2E/IdgLDjbiapj1" +
            "e2fKHrMmGFaIvI6xzzJIQJF9GiRZ7+0jNFLKSyzX/K3JAyFrIPObfwM+y+zAgE1sWcZ1YnuBhICyRHBh" +
            "aJDKIZL8MywrEfB2yF+R3k9wFG1oN48gSLyfrfEKuB/qgNp+BeTruWUk0AwRE9XVMUuRbjpxa4YA67SK" +
            "unFEgFGgUfHBeHJTivvUl0u4Dki1UKAT973P+nXy2O0u239If/kRpNUVhMg8kpk7s8i6Arp7l/705/bL" +
            "Cx4kN5hHHSXIqkiG9tHdeNV8VYo5+72hgaCx3/uVoVLmtvxbOIvo120uTJbuLVTvT8KtsOlb3DxwUrwL" +
            "zaEMoAQAFk6Q9bNipHxfkRQER4kR7IYTMzSoW5mxh3H9O8Ge5BqVeYMEW36q9wnOYfxOLNw6yQMf8f9s" +
            "JN4KhZty02xm707S7VEfJJ1KNq7b5pP/3RjE0IKtB2gE6vAPRvRLzEohu0m7q1aUp8wAvSiqjZy7FLaT" +
            "tLEApXYvLvz6PEJdj4TegCZugj7c8bIOEqLXmloZ6EgVnjQ7/ttys7VFITB3mazzFiyQuKf4J6+b/a/Y";

            Hasp hasp = new Hasp(feature);
            HaspStatus status = hasp.Login(vendorCode);


            if (HaspStatus.StatusOk != status)
            {
                return false;
                //Console.WriteLine("Login Failed with status " + status.ToString());
            }
            else
            {
                return true;
                //Console.WriteLine("Login Successful with status " + status.ToString());
            }

        }
        /// <summary>
        /// License 체크 스레드 시작
        /// </summary>
        public void StartCheckLicense()
        {
          
            if(!CheckLicense())
            {
                LicenseWarnForm form = new LicenseWarnForm();
                form.ShowDialog();
            }

            _threadRun = true;
            _licenseStopWatch.Restart();

            Thread th = new Thread(CheckLicenseThread);
            th.Start();
        }

        private void CheckLicenseThread()
        {
            while (this._threadRun)
            {
                int time = 60000;// 60sec
                if(_licenseStopWatch.ElapsedMilliseconds >= time)
                {
                    if (!CheckLicense())
                    {
                        LicenseWarnForm form = new LicenseWarnForm();
                        form.ShowDialog();
                    }
                    else
                        _licenseStopWatch.Restart();
                }
            }
        }

        public void StopCheckLicense()
        {
            this._threadRun = false;
            _licenseStopWatch.Stop();
            _licenseStopWatch.Reset();
        }

        public string GetDistance(PointF orgStartPoint, PointF orgEndPoint)
        {
            double length = 0;
            if(Status.Instance().MeasurementUnit == eMeasurementUnit.Pixel)
            {
                double xAmount = Math.Abs(orgStartPoint.X - orgEndPoint.X);
                double yAmount = Math.Abs(orgStartPoint.Y - orgEndPoint.Y);
                double temp = (xAmount * xAmount) + (yAmount * yAmount);
                int pixelCount = (int)Math.Sqrt(temp);

                return pixelCount.ToString() + "pixel";
            }
            else
            {
                if (Status.Instance().MeasureMentType == eMeasurementType.Fov_Calibration)
                {
                    double pitchX = (double)Status.Instance().FovWidth / Status.Instance().SelectedViewer.ImageManager.GetOrgImage().Width;
                    double pitchY = (double)Status.Instance().FovHeight / Status.Instance().SelectedViewer.ImageManager.GetOrgImage().Height;

                    double lengthX = Math.Abs((int)orgStartPoint.X - (int)orgEndPoint.X) * pitchX;
                    double lengthY = Math.Abs((int)orgStartPoint.Y - (int)orgEndPoint.Y) * pitchY;
                    double temp = Math.Pow(lengthX, 2) + Math.Pow(lengthY, 2);
                    length = Math.Sqrt(temp);

                    if (Status.Instance().MeasurementUnit == eMeasurementUnit.Inch)
                        return Math.Round(KLine.ConvertMMToINCH(length), 2).ToString() + "inch";
                    else
                        return Math.Round(length, 2).ToString() + "mm";
                }
                else // if (Status.Instance().MeasureMentType == eMeasurementType.Pixel_Calibration)
                {
                    length = GetDistanceFromPitch(orgStartPoint, orgEndPoint);

                    if (Status.Instance().MeasurementUnit == eMeasurementUnit.Inch)
                        return Math.Round(KLine.ConvertMMToINCH(length), 2).ToString() + "inch";
                    else
                        return Math.Round(length, 2).ToString() + "mm";
                }
            }
            
        }

        public void SetPitch(PointF startPoint, PointF endPoint, double distance_mm)
        {
            if (startPoint.X == endPoint.X) // 세로직선
            {
                double length = Math.Abs(startPoint.Y - endPoint.Y);
                Status.Instance().Pitch = distance_mm / length;
            }
            else if (startPoint.Y == endPoint.Y) // 가로 직선
            {
                double length = Math.Abs(startPoint.X - endPoint.X);
                Status.Instance().Pitch = distance_mm / length;
            }
            else // 대각선
            {
                double xAmount = Math.Abs(startPoint.X - endPoint.X);
                double yAmount = Math.Abs(startPoint.Y - endPoint.Y);

                Status.Instance().Pitch = distance_mm / Math.Sqrt((xAmount * xAmount) + (yAmount * yAmount));
            }
        }

        public double GetDistanceFromPitch(PointF startPoint, PointF endPoint)
        {
            double distance = 0;
            if (startPoint.X == endPoint.X) // 세로직선
            {
                double length = Math.Abs(startPoint.Y - endPoint.Y);
                distance = Pitch * length;
            }
            else if (startPoint.Y == endPoint.Y) // 가로 직선
            {
                double length = Math.Abs(startPoint.X - endPoint.X);
                distance = Pitch * length;
            }
            else // 대각선
            {
                double xAmount = Math.Abs(startPoint.X - endPoint.X);
                double yAmount = Math.Abs(startPoint.Y - endPoint.Y);

                distance = Math.Sqrt(((xAmount * xAmount) + (yAmount * yAmount)) * Pitch * Pitch);
            }

            return distance;
        }


        public bool CheckEanbleProcessing(eImageProcessType type)
        {
            if (eImageProcessType.LUT_8 == type)
            {
                if (Status.Instance().SelectedViewer.ImageManager.GetBit() == 16)
                    return false;
            }

            if (eImageProcessType.LUT_16 == type)
            {
                if (Status.Instance().SelectedViewer.ImageManager.GetBit() == 8)
                    return false;
            }

            if (eImageProcessType.Leveling_8 == type)
            {
                if (Status.Instance().SelectedViewer.ImageManager.GetBit() == 16)
                    return false;
            }

            if (eImageProcessType.Leveling_16 == type)
            {
                if (Status.Instance().SelectedViewer.ImageManager.GetBit() == 8 || Status.Instance().SelectedViewer.ImageManager.IsColor())
                    return false;
            }

            if (Status.Instance().SelectedViewer.ImageManager.IsColor())
            {
                if (eImageProcessType.Threshold == type || eImageProcessType.ThresholdAdaptive == type)
                    return false;
            }
            return true;
        }

        public void OpenProcessingTimeForm()
        {
            if (Status.Instance().ProcessingTimeForm == null)
            {
                Status.Instance().ProcessingTimeForm = new ProcessingTimeForm();
                Status.Instance().ProcessingTimeForm.CloseEventDelegate = () => Status.Instance().ProcessingTimeForm = null;
                Status.Instance().ProcessingTimeForm.Show();
            }
        }

        public void ProcessingTimeFormUpdate()
        {
            if (Status.Instance().ProcessingTimeForm != null)
                Status.Instance().ProcessingTimeForm.ProcessingUpdate();
        }
    }

}
