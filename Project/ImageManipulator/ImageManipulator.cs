using ImageManipulator.Data;
using ImageManipulator.Forms;
using ImageManipulator.ImageProcessingData;
using ImageManipulator.Util;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;

namespace ImageManipulator
{
    public partial class ImageManipulator : Form
    {
        private int _resizeWidth;
        private int _resizeHeight;
        public ImageManipulator()
        {
            InitializeComponent();
        }

        private void ImageManipulator_Load(object sender, EventArgs e)
        {
            Status.Instance().LogManager.AddLogMessage("Program Start","");
            this.AllowDrop = true;
            //this.MinimumSize = new Size(623, 63);
            this.MinimumSize = new Size(535, 90);
            this.MaximumSize = new Size(535, 90);
            Status.Instance().SetMainForm(this);

            MultiLanguage();

            MainFormToolTip.Active = true;
        }

        private void MultiLanguage()
        {
            //File
            fileToolStripMenuItem.Text = LangResource.File;
            openFileToolStripMenuItem.Text = LangResource.OpenFile;
            recentFileToolStripMenuItem.Text = LangResource.RecentFile;
            loadRoiToolStripMenuItem.Text = LangResource.LoadRoi;
            saveRoiToolStripMenuItem.Text = LangResource.SaveRoi;
            bitConverterToolStripMenuItem1.Text = LangResource.BitConverter;
            to8bitToolStripMenuItem1.Text = LangResource.To8bit;
            to16bitToolStripMenuItem1.Text = LangResource.To16bit;
            to24bitToolStripMenuItem1.Text = LangResource.To24bit;
            saveToolStripMenuItem.Text = LangResource.Save;
            saveImageAsToolStripMenuItem.Text = LangResource.SaveImageAs;
            saveFilesFoldersToolStripMenuItem.Text = LangResource.SaveFilesInFolders;
            saveFilterListToolStripMenuItem.Text = LangResource.SaveFilterList;
            loadFilterListToolStripMenuItem.Text = LangResource.LoadFilterList;
            separateColorImageToolStripMenuItem.Text = LangResource.SeparateColorImage;
            combine3ChannelsToolStripMenuItem.Text = LangResource.Combine3Channels;
            //View
            viewToolStripMenuItem.Text = LangResource.View;
            fitToScreenToolStripMenuItem.Text = LangResource.FitToScreen;
            zoomToolStripMenuItem.Text = LangResource.OneZoom;
            panningToolStripMenuItem.Text = LangResource.Panning;
            gridToolStripMenuItem.Text = LangResource.Grid;
            zoomInToolStripMenuItem.Text = LangResource.ZoomIn;
            zoomOutToolStripMenuItem.Text = LangResource.ZoomOut;
            transformToolStripMenuItem.Text = LangResource.Transform;
            rotation90DegreesRightToolStripMenuItem.Text = LangResource.Rotation90DegreesRight;
            rotation90DegreesLeftToolStripMenuItem.Text = LangResource.Rotation90DegreesLeft;
            flipHorizontallyToolStripMenuItem.Text = LangResource.FlipHorizontally;
            flipVerticallyToolStripMenuItem.Text = LangResource.FlipVertically;
            roiListToolStripMenuItem.Text = LangResource.RoiList;
            profileListToolStripMenuItem.Text = LangResource.ProfileList;
            metaInfoToolStripMenuItem.Text = LangResource.MetaInfo;

            //Process
            processToolStripMenuItem.Text = LangResource.Process;
            filtersToolStripMenuItem.Text = LangResource.Filters;
            cropToolStripMenuItem.Text = LangResource.Crop;
            resizeToolStripMenuItem.Text = LangResource.Resize;
            processingTimeToolStripMenuItem.Text = LangResource.ProcessingTime;
            //Analyze
            analyzeToolStripMenuItem.Text = LangResource.Analyze;
            histogramToolStripMenuItem.Text = LangResource.Histogram;
            profileToolStripMenuItem.Text = LangResource.AddProfile;
            roiToolStripMenuItem.Text = LangResource.AddRoi;
            measurementToolStripMenuItem.Text = LangResource.Measurement;
            lengthToolStripMenuItem.Text = LangResource.Length;
            angleToolStripMenuItem.Text = LangResource.Angle;

            //Settings
            settingsToolStripMenuItem1.Text = LangResource.Setting;
            measureSettingToolStripMenuItem.Text = LangResource.SetMeasurement;
            aboutImageManipulatorToolStripMenuItem.Text = LangResource.AboutImageManipulator;
            setLanguageToolStripMenuItem.Text = LangResource.SetLanguage;

            MainFormToolTip.SetToolTip(FileOpenPanel, LangResource.OpenFile);
            MainFormToolTip.SetToolTip(FileSavePanel, LangResource.SaveImageAs);
            MainFormToolTip.SetToolTip(DrawNonePanel, LangResource.DrawNone);
            MainFormToolTip.SetToolTip(DrawRoiPanel, LangResource.AddRoi);
            MainFormToolTip.SetToolTip(DrawProfilePanel, LangResource.AddProfile);
            MainFormToolTip.SetToolTip(DrawRulerPanel, LangResource.Length);
            MainFormToolTip.SetToolTip(DrawProtractorPanel, LangResource.Angle);
            MainFormToolTip.SetToolTip(WndLevelingPanel, LangResource.WndLeveling);
            MainFormToolTip.SetToolTip(FilterPanel, LangResource.Filters);
            MainFormToolTip.SetToolTip(HistogramPanel, LangResource.Histogram);
            MainFormToolTip.SetToolTip(RoiListPanel, LangResource.RoiList);
            MainFormToolTip.SetToolTip(ProfilePanel, LangResource.ProfileList);
            MainFormToolTip.SetToolTip(MetaInfoPanel, LangResource.MetaInfo);
            MainFormToolTip.SetToolTip(AboutPanel, LangResource.AboutImageManipulator);
        }
        private void ImageManipulator_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Normal)
            {
                if(Status.Instance().SelectedViewer != null)
                    Status.Instance().SelectedViewer.GetDrawBox().PictureBoxMoveToCenterPictureBox();
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileOpen();
        }

        private void panningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().EnablePanning)
            {
                Status.Instance().SelectedViewer.GetDrawBox().EnablePanning = false;
                Status.Instance().LogManager.AddLogMessage("Panning", "Enable");
            }
            else
            {
                Status.Instance().SelectedViewer.GetDrawBox().EnablePanning = true;
                Status.Instance().LogManager.AddLogMessage("Panning", "Disable");
            }

        }

        private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().EnablePanning)
            {
                panningToolStripMenuItem.Checked = true;
            }
            else
            {
                panningToolStripMenuItem.Checked = false;
            }
            if (Status.Instance().SelectedViewer.GetDrawBox().EnableGirdView)
            {
                gridToolStripMenuItem.Checked = true;
            }
            else
            {
                gridToolStripMenuItem.Checked = false;
            }
        }

        private void lengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectRulerButton();
            PanelSelectedUpdate();
        }

        private void analyzeToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().DrawType == eDrawType.LineMeasurement)
            {
                lengthToolStripMenuItem.Checked = true;
            }
            else
            {
                lengthToolStripMenuItem.Checked = false;
            }
            if (Status.Instance().SelectedViewer.GetDrawBox().DrawType == eDrawType.Roi)
            {
                roiToolStripMenuItem.Checked = true;
            }
            else
            {
                roiToolStripMenuItem.Checked = false;
            }
            if (Status.Instance().SelectedViewer.GetDrawBox().DrawType == eDrawType.Protractor)
            {
                angleToolStripMenuItem.Checked = true;
            }
            else
            {
                angleToolStripMenuItem.Checked = false;
            }
            if (Status.Instance().SelectedViewer.GetDrawBox().DrawType == eDrawType.Profile)
            {
                profileToolStripMenuItem.Checked = true;
            }
            else
            {
                profileToolStripMenuItem.Checked = false;
            }
        }

        private void addRoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectRoiButton();
            PanelSelectedUpdate();
        }

        private void angleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAngleButton();
            PanelSelectedUpdate();
        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectProfileButton();
            PanelSelectedUpdate();
        }

        private void rotation90DegreesRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            IP_RotationCW cw = new IP_RotationCW();
            Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(cw);
            Bitmap image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
            Status.Instance().SelectedViewer.GetDrawBox().Image = image;
            Status.Instance().SelectedViewer.GetDrawBox().Rotation(eImageTransform.CW);
            Status.Instance().LogManager.AddLogMessage("Rotation", "CW");
        }

        private void rotation90DegreesLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            IP_RotationCCW ccw = new IP_RotationCCW();
            Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(ccw);
            Bitmap image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
            Status.Instance().SelectedViewer.GetDrawBox().Image = image;
            Status.Instance().SelectedViewer.GetDrawBox().Rotation(eImageTransform.CCW);
            Status.Instance().LogManager.AddLogMessage("Rotation", "CCW");
        }

        private void flipHorizontallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            IP_FlipHorizontal horizontal = new IP_FlipHorizontal();
            Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(horizontal);
            Bitmap image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
            Status.Instance().SelectedViewer.GetDrawBox().Image = image;
            Status.Instance().SelectedViewer.GetDrawBox().Rotation(eImageTransform.FlipX);
            Status.Instance().LogManager.AddLogMessage("Rotation", "FlipX");
        }

        private void flipVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            IP_FlipVetical vertical = new IP_FlipVetical();
            Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(vertical);
            Bitmap image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
            Status.Instance().SelectedViewer.GetDrawBox().Image = image;
            Status.Instance().SelectedViewer.GetDrawBox().Rotation(eImageTransform.FlipY);
            Status.Instance().LogManager.AddLogMessage("Rotation", "FlipY");
        }

        private void loadRoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Roi 문서(*.Roi)|*.Roi";
            dialog.DefaultExt = "Roi";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Status.Instance().LoadRoiXml(dialog.FileName.ToString());
                Status.Instance().LogManager.AddLogMessage("Load Roi", dialog.FileName.ToString());
            }
        }

        private void saveRoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().GetRoiCount() <= 0)
                return;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Xml 문서(*.Roi)|*.Roi";
            dialog.DefaultExt = "Roi";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Status.Instance().SaveRoiXml(dialog.FileName.ToString());
                Status.Instance().LogManager.AddLogMessage("Save Roi", dialog.FileName.ToString());
            }
        }

        private void LUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            //Status.Instance().FilterFormOpen(2);
        }

        private void wndLevelingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            //Status.Instance().FilterFormOpen(1);
        }

        private void cropToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().ModeType != eModeType.Crop)
            {
                Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.Crop;
                Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.None;
                Status.Instance().LogManager.AddLogMessage("Crop", "Enable");
            }
            else
            {
                Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.None;
                Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.None;
                Status.Instance().LogManager.AddLogMessage("Crop", "Disable");
            }
            DrawNonePanel.BorderStyle = BorderStyle.FixedSingle;
            DrawRoiPanel.BorderStyle = BorderStyle.FixedSingle;
            DrawProfilePanel.BorderStyle = BorderStyle.FixedSingle;
            DrawRulerPanel.BorderStyle = BorderStyle.FixedSingle;
            DrawProtractorPanel.BorderStyle = BorderStyle.FixedSingle;
            DrawNonePanel.BorderStyle = BorderStyle.Fixed3D;
        }

        private void processToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().ModeType == eModeType.Crop)
            {
                cropToolStripMenuItem.Checked = true;
            }
            else
            {
                cropToolStripMenuItem.Checked = false;
            }
        }

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().CheckReOpening("Resize"))
                return;
            Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.None;
            Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.None;

            ResizeForm form = new ResizeForm();
            form.Show();
        }

        private void ResizeParam(int width, int height)
        {
            _resizeWidth = width;
            _resizeHeight = height;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            Status.Instance().SelectedViewer.GetDrawBox().EnablePanning = false;
            Status.Instance().SelectedViewer.GetDrawBox().OneVerseOneFitZoom();
            Status.Instance().LogManager.AddLogMessage("1:1Zoom", "");
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            Status.Instance().HistogramFormOpen();
        }

        private void filtersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            Status.Instance().FilterFormOpen();
        }

        private void roiListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectRoiListFormOpenButton();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // Size t = this.ClientSize;
           // Status.Instance().SaveSettings();

            //Status.Instance().AddRecentFile("");
            //Status.Instance().GetPrevFileOpen();
            //List<HistogramParams> paramList = new List<HistogramParams>();
            //HistogramParams param = Status.Instance().SelectedViewer.GetGreyHistogramParam();
            //paramList.Add(param);
            //ProfileForm form = new ProfileForm();

            //form.SetHistogramParam(paramList);
            //form.Show();
        }

        private void profileListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectProfileListFormOpenButton();
        }

        private void test2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status.Instance().LoadSettings();
            //Status.Instance().GetNextFileOpen();
        }

        private void measureSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status.Instance().MesurementFormOpen();
        }

        private void metaInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MetaInfoFormOpen();
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;

            //Status.Instance().SelectedViewer.GetDrawBox().UpdateZoom((float)0.112);
            Status.Instance().SelectedViewer.GetDrawBox().UpdateZoom((float)0.2);
            Status.Instance().LogManager.AddLogMessage("ZoomIn", "");
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;

            Status.Instance().SelectedViewer.GetDrawBox().UpdateZoom((float)-0.2);
            Status.Instance().LogManager.AddLogMessage("ZoomOut", "");
        }

        private void fitToScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            Status.Instance().SelectedViewer.GetDrawBox().EnablePanning = false;
            Status.Instance().SelectedViewer.GetDrawBox().FitToScreen();
            Status.Instance().LogManager.AddLogMessage("FitToScreen", "");
        }

        private void ImageManipulator_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                try
                {
                    string[] strFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
                    foreach (string strFile in strFiles)
                    {
                        JImage image = new JImage(strFile);
                        string fileName = Path.GetFileNameWithoutExtension(strFile);
                        Viewer form = new Viewer(image, strFile);
                        form.Text = fileName;
                        form.Show();
                        form.Activate();
                        Status.Instance().PrevSelectedViewer = form;
                        Status.Instance().SelectedViewer = form;
                        if (Status.Instance().FilterForm != null)
                            Status.Instance().FilterForm.ComboBoxUpdate();

                        Status.Instance().AddRecentFile(strFile);
                        Status.Instance().SaveSettings();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("The file is not supported format.");
                }
            }

        }

        private void ImageManipulator_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy | DragDropEffects.Scroll;
            }

        }

        private void recentFileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            recentFileToolStripMenuItem.DropDownItems.Clear();
            foreach (string filePath in Status.Instance().GetRecentFiles())
            {
                recentFileToolStripMenuItem.DropDownItems.Add(filePath);
            }
        }

        private void recentFileToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string filePath = e.ClickedItem.ToString();

            FileInfo info = new FileInfo(filePath);
            if(!info.Exists)
            {
                MessageBox.Show("The file does not exist.");
                Status.Instance().DeleteRecentFile(filePath);
                Status.Instance().SaveSettings();
                return;
            }
            else
            {
                Status.Instance().DeleteRecentFile(filePath);

                JImage image = new JImage(filePath);
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                Viewer form = new Viewer(image, filePath);
                form.Text = fileName;
                form.Show();
                Status.Instance().PrevSelectedViewer = form;
                Status.Instance().SelectedViewer = form;
                if (Status.Instance().FilterForm != null)
                    Status.Instance().FilterForm.ComboBoxUpdate();

                Status.Instance().AddRecentFile(filePath);
                Status.Instance().SaveSettings();
            }
        }

        private void aboutImageManipulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void SelectRoiButton()
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().DrawType == eDrawType.Roi)
            {
                Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.None;
                Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.None;
            }
            else
            {
                Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.Roi;
                Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.Draw;
                Status.Instance().SelectedViewer.GetDrawBox().FigureSelectedClear();
            }
        }

        private void SelectAngleButton()
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().DrawType == eDrawType.Protractor)
            {
                Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.None;
                Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.None;
            }
            else
            {
                Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.Protractor;
                Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.Draw;
                Status.Instance().SelectedViewer.GetDrawBox().FigureSelectedClear();
            }
        }

        private void SelectProfileButton()
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().DrawType == eDrawType.Profile)
            {
                Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.None;
                Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.None;
            }
            else
            {
                Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.Profile;
                Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.Draw;
                Status.Instance().SelectedViewer.GetDrawBox().FigureSelectedClear();
            }
        }

        private void SelectNoneButton()
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.None;
            Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.None;
        }

        private void SelectRulerButton()
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().DrawType == eDrawType.LineMeasurement)
            {
                Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.None;
                Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.None;
            }
            else
            {
                Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.LineMeasurement;
                Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.Draw;
                Status.Instance().SelectedViewer.GetDrawBox().FigureSelectedClear();
            }
        }

        private void FileOpen()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if(Status.Instance().Language == eLanguageType.Korea)
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
                    if(Status.Instance().SelectedViewer != null)
                    {
                        Status.Instance().SelectedViewer.GetDrawBox().Image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
                    }
                    JImage image = new JImage(dialog.FileName.ToString());
                    string fileName = Path.GetFileNameWithoutExtension(dialog.FileName);
                    Viewer form = new Viewer(image, dialog.FileName.ToString());
                    form.Text = fileName;
                    form.Show();
                    Status.Instance().PrevSelectedViewer = form;
                    Status.Instance().SelectedViewer = form;
                    if (Status.Instance().FilterForm != null)
                        Status.Instance().FilterForm.ComboBoxUpdate();

                    Status.Instance().AddRecentFile(dialog.FileName);
                    Status.Instance().SaveSettings();


                    Status.Instance().LogManager.AddLogMessage("File Open ", dialog.FileName.ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The file is not supported format.");
                Status.Instance().LogManager.AddLogMessage("File Open","The file is not supported format._ERR");
            }
        }

        private void FileOpenPanel_Click(object sender, EventArgs e)
        {
            FileOpen();
        }


        private void FileSavePanel_Click(object sender, EventArgs e)
        {
            FileSave();
        }

        private void DrawRoiPanel_Click(object sender, EventArgs e)
        {
            SelectRoiButton();
            PanelSelectedUpdate();
        }

        private void DrawProfilePanel_Click(object sender, EventArgs e)
        {
            SelectProfileButton();
            PanelSelectedUpdate();
        }

        private void DrawNonePanel_Click(object sender, EventArgs e)
        {
            SelectNoneButton();
            PanelSelectedUpdate();
        }

        private void DrawProtractorPanel_Click(object sender, EventArgs e)
        {
            SelectAngleButton();
            PanelSelectedUpdate();
        }

        private void DrawRulerPanel_Click(object sender, EventArgs e)
        {
            SelectRulerButton();
            PanelSelectedUpdate();
        }

        private void SelectRoiListFormOpenButton()
        {
            // 만약 ROI 리스트가 없으면 실행 안됨
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().GetRoiCount() <= 0)
                return;

            Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.None;
            Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.None;
            PanelSelectedUpdate();
            Status.Instance().RoiListFormOpen();
        }

        private void SelectProfileListFormOpenButton()
        {
            // 만약 ROI 리스트가 없으면 실행 안됨
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().GetProfileCount() <= 0)
                return;

            Status.Instance().SelectedViewer.GetDrawBox().DrawType = eDrawType.None;
            Status.Instance().SelectedViewer.GetDrawBox().ModeType = eModeType.None;
            PanelSelectedUpdate();
            Status.Instance().ProfileListFormOpen();
        }

        private void MetaInfoFormOpen()
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            JImage image = Status.Instance().SelectedViewer.ImageManager.GetOrgImage();
            string filePath = Status.Instance().SelectedViewer.ImageManager.ImageFilePath;
            MetaInfoForm metaInfoForm = new MetaInfoForm(image, filePath);
            Status.Instance().LogManager.AddLogMessage("MetaInfo", filePath);
            metaInfoForm.ShowDialog();
        }

        public void PanelSelectedUpdate()
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            //Status.Instance().SelectedViewer.GetDrawBox().EnablePanning = false;
            eDrawType type = Status.Instance().SelectedViewer.GetDrawBox().DrawType;

            DrawNonePanel.BorderStyle = BorderStyle.FixedSingle;
            DrawRoiPanel.BorderStyle = BorderStyle.FixedSingle;
            DrawProfilePanel.BorderStyle = BorderStyle.FixedSingle;
            DrawRulerPanel.BorderStyle = BorderStyle.FixedSingle;
            DrawProtractorPanel.BorderStyle = BorderStyle.FixedSingle;
          
            switch (type)
            {
                case eDrawType.None:
                    Status.Instance().LogManager.AddLogMessage("Select", "None");
                    DrawNonePanel.BorderStyle = BorderStyle.Fixed3D;
                    break;
                case eDrawType.Panning:
                    DrawNonePanel.BorderStyle = BorderStyle.Fixed3D;
                    Status.Instance().LogManager.AddLogMessage("Select", "Panning");
                    break;
                case eDrawType.LineMeasurement:
                    DrawRulerPanel.BorderStyle = BorderStyle.Fixed3D;
                    Status.Instance().LogManager.AddLogMessage("Select", "LineMeasurement");
                    break;
                case eDrawType.Roi:
                    DrawRoiPanel.BorderStyle = BorderStyle.Fixed3D;
                    Status.Instance().LogManager.AddLogMessage("Select", "Roi");
                    break;
                case eDrawType.Protractor:
                    DrawProtractorPanel.BorderStyle = BorderStyle.Fixed3D;
                    Status.Instance().LogManager.AddLogMessage("Select", "Protractor");
                    break;
                case eDrawType.Profile:
                case eDrawType.Project:
                    DrawProfilePanel.BorderStyle = BorderStyle.Fixed3D;
                    Status.Instance().LogManager.AddLogMessage("Select", "Profile");
                    break;
                default:
                    break;
            }
        }

        private void WndLevelingPanel_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            //Status.Instance().FilterFormOpen(1);
        }

        private void FilterPanel_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            //Status.Instance().FilterFormOpen(0);
        }

        private void HistogramPanel_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            Status.Instance().HistogramFormOpen();
        }

        private void RoiListPanel_Click(object sender, EventArgs e)
        {
            SelectRoiListFormOpenButton();
        }

        private void ProfilePanel_Click(object sender, EventArgs e)
        {
            SelectProfileListFormOpenButton();
        }

        private void MetaInfoPanel_Click(object sender, EventArgs e)
        {
            MetaInfoFormOpen();
        }

        private void AboutPanel_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileSave();
        }

        private void FileSave()
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            SaveFileDialog dialog = new SaveFileDialog();
            if(Status.Instance().Language == eLanguageType.Korea)
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
                    Status.Instance().Save(dialog.FileName.ToString());
                    Status.Instance().SelectedViewer.Text = dialog.FileName.ToString();
                    Status.Instance().LogManager.AddLogMessage("File Save", dialog.FileName.ToString());
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                    Status.Instance().LogManager.AddLogMessage("File Save", err.Message +"_ERR");
                }
            }
        }
        private void saveFilesFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            //string message = "All files in the selected image path are overwritten. Do you want to continue saving files in the path?";
            string message = LangResource.FolderSave;
            if (MessageBox.Show(message, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Status.Instance().AllSaveInDirectory();
                Status.Instance().LogManager.AddLogMessage("Save Files", "In Folders");
            }
        }

        private void ImageManipulator_FormClosed(object sender, FormClosedEventArgs e)
        {
            Status.Instance().LogManager.AddLogMessage("Program Close", "");
            Status.Instance().StopCheckLicense();
        }

        private void to8bitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            try
            {
                JImage img = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.To8BitG();

                Viewer newForm = new Viewer(img, "");
                newForm.EnableKeyEvent = false;

                string newFormName = Status.Instance().GetNewFileName(Status.Instance().SelectedViewer.Text);
                newForm.Text = newFormName;
                newForm.Show();
                Status.Instance().LogManager.AddLogMessage("BitConverter", "To8Bit");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void to16bitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            try
            {
                JImage img = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.To16BitG();

                Viewer newForm = new Viewer(img, "");
                newForm.EnableKeyEvent = false;

                string newFormName = Status.Instance().GetNewFileName(Status.Instance().SelectedViewer.Text);
                newForm.Text = newFormName;
                newForm.Show();
                Status.Instance().LogManager.AddLogMessage("BitConverter", "To16Bit");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void to24bitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            try
            {
                JImage img = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.To24BitC();

                Viewer newForm = new Viewer(img, "");
                newForm.EnableKeyEvent = false;

                string newFormName = Status.Instance().GetNewFileName(Status.Instance().SelectedViewer.Text);
                newForm.Text = newFormName;
                newForm.Show();
                Status.Instance().LogManager.AddLogMessage("BitConverter", "To24Bit");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void addProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectProfileButton();
            PanelSelectedUpdate();
        }

        private void roiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectRoiButton();
            PanelSelectedUpdate();
        }

        private void setLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MultiLanguageSettingForm form = new MultiLanguageSettingForm();
            form.ShowDialog();
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox().EnableGirdView)
            {
                Status.Instance().SelectedViewer.GetDrawBox().EnableGirdView = false;
                Status.Instance().LogManager.AddLogMessage("Grid", "Enable");
            }
            else
            {
                Status.Instance().SelectedViewer.GetDrawBox().EnableGirdView = true;
                Status.Instance().LogManager.AddLogMessage("Grid", "Disable");
            }
            Status.Instance().SelectedViewer.GetDrawBox().ReUpdate();
        }

        private void thresholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            Status.Instance().ThresholdFormOpen();
        }

        private void saveFilterListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            if (Status.Instance().SelectedViewer.GetDrawBox() == null)
                return;
            if (Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Count == 0)
                return;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "FilterList 문서(*.ftl)|*.ftl";
            dialog.DefaultExt = "ftl";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Status.Instance().SaveFilterList(dialog.FileName.ToString());
                Status.Instance().LogManager.AddLogMessage("Save FilterList", dialog.FileName.ToString());
            }

        }

        private void loadFilterListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "FilterList 문서(*.ftl)|*.ftl";
            dialog.DefaultExt = "ftl";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Status.Instance().LoadFilterList(dialog.FileName.ToString());
                Status.Instance().LogManager.AddLogMessage("Load FilterList", dialog.FileName.ToString());
            }
        }

        private void processingTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status.Instance().OpenProcessingTimeForm();
        }

        private void separateColorImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSeparateColorImage();
        }

        private void OpenSeparateColorImage()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (Status.Instance().Language == eLanguageType.Korea)
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

                    if(image.Depth != KiyLib.DIP.KDepthType.Dt_24)
                    {
                        MessageBox.Show(LangResource.FileSeparateError);
                        return;
                    }
                    SeparateColorImageForm form = new SeparateColorImageForm();
                    form.FilePath = dialog.FileName;
                    form.ShowDialog();

                    Status.Instance().LogManager.AddLogMessage("File Open ", dialog.FileName.ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show(LangResource.FileOpenError);
                Status.Instance().LogManager.AddLogMessage("File Open", "The file is not supported format._ERR");
            }
        }

        private void combine3ChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Combine3ChannelForm form = new Combine3ChannelForm();
            form.ShowDialog();
        }
    }
}
