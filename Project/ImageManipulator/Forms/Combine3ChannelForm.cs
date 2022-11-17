using ImageManipulator.Controls;
using ImageManipulator.Util;
using KiyLib.DIP;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulator.Forms
{
    public partial class Combine3ChannelForm : Form
    {
        private HistogramControl _redHistogramControl = null;
        private HistogramControl _greenHistogramControl = null;
        private HistogramControl _blueHistogramControl = null;
        private JImage _redImage = null;
        private JImage _greenImage = null;
        private JImage _blueImage = null;
        private JImage _displayImage = null;

        public Combine3ChannelForm()
        {
            InitializeComponent();
        }

        private void Combine3ChannelForm_Load(object sender, EventArgs e)
        {
            MultiLanguage();
            AddControl();
        }

        private void MultiLanguage()
        {
            this.Text = LangResource.Combine3Channels;
            btnOpenRedChannel.Text = LangResource.OpenRedChannelImage;
            btnOpenGreenChannel.Text = LangResource.OpenGreenChannelImage;
            btnOpenBlueChannel.Text = LangResource.OpenBlueChannelImage;
            btnPreview.Text = LangResource.Preview;
            btnConvertToImage.Text = LangResource.ConvertToImage;
        }

        private void AddControl()
        {
            _redHistogramControl = new HistogramControl();
            RedPanel.Controls.Add(_redHistogramControl);
            _redHistogramControl.Dock = DockStyle.Fill;

            _greenHistogramControl = new HistogramControl();
            GreenPanel.Controls.Add(_greenHistogramControl);
            _greenHistogramControl.Dock = DockStyle.Fill;

            _blueHistogramControl = new HistogramControl();
            BluePanel.Controls.Add(_blueHistogramControl);
            _blueHistogramControl.Dock = DockStyle.Fill;
        }

        private float ConvertIntToFloat(int input)
        {
            return (float)input;
        }

        private void btnOpenRedChannel_Click(object sender, EventArgs e)
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

                if (image.Depth != KiyLib.DIP.KDepthType.Dt_8)
                {
                    MessageBox.Show(LangResource.CombineImageErr);
                    return;
                }

                _redImage = (JImage)image.Clone();

                int[] greyHistogram;

                _redImage.GetHistoGray(out greyHistogram);
                float[] ret = Array.ConvertAll(greyHistogram, new Converter<int, float>(ConvertIntToFloat));

                List<HistogramParams> redParamList = new List<HistogramParams>();
                HistogramParams redParam = new HistogramParams();
                redParam.HistogramValue = ret;
                redParam.GraphColor = Color.Red;
                redParam.Width = _redImage.Width;
                redParam.Height = _redImage.Height;

                redParamList.Add(redParam);

                _redHistogramControl.ImageBit = 8;
                _redHistogramControl.IsColor = false;
                _redHistogramControl.HistogramGraphReNewal(redParamList);
            }
        }

        private void btnOpenGreenChannel_Click(object sender, EventArgs e)
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

                if (image.Depth != KiyLib.DIP.KDepthType.Dt_8)
                {
                    MessageBox.Show(LangResource.CombineImageErr);
                    return;
                }

                _greenImage = (JImage)image.Clone();

                int[] greyHistogram;

                _greenImage.GetHistoGray(out greyHistogram);
                float[] ret = Array.ConvertAll(greyHistogram, new Converter<int, float>(ConvertIntToFloat));

                List<HistogramParams> greenParamList = new List<HistogramParams>();
                HistogramParams greenParam = new HistogramParams();
                greenParam.HistogramValue = ret;
                greenParam.GraphColor = Color.Green;
                greenParam.Width = _greenImage.Width;
                greenParam.Height = _greenImage.Height;

                greenParamList.Add(greenParam);
          
                _greenHistogramControl.ImageBit = 8;
                _greenHistogramControl.IsColor = false;
                _greenHistogramControl.HistogramGraphReNewal(greenParamList);
            }
        }

        private void btnOpenBlueChannel_Click(object sender, EventArgs e)
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

                if (image.Depth != KiyLib.DIP.KDepthType.Dt_8)
                {
                    MessageBox.Show(LangResource.CombineImageErr);
                    return;
                }

                _blueImage = (JImage)image.Clone();

                int[] greyHistogram;

                _blueImage.GetHistoGray(out greyHistogram);
                float[] ret = Array.ConvertAll(greyHistogram, new Converter<int, float>(ConvertIntToFloat));

                List<HistogramParams> blueParamList = new List<HistogramParams>();
                HistogramParams blueParam = new HistogramParams();
                blueParam.HistogramValue = ret;
                blueParam.GraphColor = Color.Blue;
                blueParam.Width = _blueImage.Width;
                blueParam.Height = _blueImage.Height;

                blueParamList.Add(blueParam);

                _blueHistogramControl.ImageBit = 8;
                _blueHistogramControl.IsColor = false;
                _blueHistogramControl.HistogramGraphReNewal(blueParamList);
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if(_redImage == null || _greenImage == null || _blueImage == null)
            {
                MessageBox.Show(LangResource.Notenoughchannels);
                return;
            }
            if (_redImage.Width != _greenImage.Width || _greenImage.Width != _blueImage.Width || _blueImage.Width != _redImage.Width)
            {
                MessageBox.Show(LangResource.ChannelImageSizeERR);
                return;
            }
            if (_redImage.Height != _greenImage.Height || _greenImage.Height != _blueImage.Height || _blueImage.Height != _redImage.Height)
            {
                MessageBox.Show(LangResource.ChannelImageSizeERR);
                return;
            }
            JImage temp = new JImage(_redImage.Width, _redImage.Height, KDepthType.Dt_24);

            _displayImage = temp.ColorChannelCombine(_blueImage, _greenImage, _redImage);

            pbxMain.Image = _displayImage.ToBitmap();

            FitToScreen();
        }

        public void FitToScreen()
        {
            if (pbxMain.Image == null)
                return;

            int horizontalScrollValue = this.MainPanel.HorizontalScroll.Value;
            int verticalScrollValue = this.MainPanel.VerticalScroll.Value;

            SizeF sizef = new SizeF(_displayImage.Width, _displayImage.Height);

            float fScale = Math.Min(this.MainPanel.Width / sizef.Width, this.MainPanel.Height / sizef.Height);

            sizef.Width *= fScale;
            sizef.Height *= fScale;

            pbxMain.Width = (int)sizef.Width;
            pbxMain.Height = (int)sizef.Height;

            PictureBoxMoveToCenterPictureBox();
        }

        public void PictureBoxMoveToCenterPictureBox()
        {
            try
            {
                if (pbxMain.Image == null)
                    return;

                if (this.MainPanel.Width > pbxMain.Width)
                    pbxMain.Left = (this.MainPanel.Width / 2) - (this.pbxMain.Width / 2);
                else
                {
                    int left = 0;
                    int value = -this.MainPanel.HorizontalScroll.Value;
                    if (value < 0)
                        left += value;
                    pbxMain.Left = left;
                }
                if (this.MainPanel.Height > pbxMain.Height)
                    pbxMain.Top = (this.MainPanel.Height / 2) - (this.pbxMain.Height / 2);
                else
                {
                    int top = 0;
                    int value = -this.MainPanel.VerticalScroll.Value;
                    if (value < 0)
                        top += value;
                    pbxMain.Top = top;
                }
            }
            catch (Exception err)
            {
                //Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        private void btnConvertToImage_Click(object sender, EventArgs e)
        {
            if (_displayImage == null)
            {
                MessageBox.Show(LangResource.PreviewNotImage);
                return;
            }
            Viewer form = new Viewer(_displayImage, "");
            string date = DateTime.Now.ToString("yyMMddHHmmss");

            form.Text = "Combine_3Channel_" + date;
            form.Show();
            Status.Instance().PrevSelectedViewer = form;
            Status.Instance().SelectedViewer = form;
            if (Status.Instance().FilterForm != null)
                Status.Instance().FilterForm.ComboBoxUpdate();
            Status.Instance().UpdateSubForm();
            Status.Instance().LogManager.AddLogMessage("Combine 3 Channel", "");
        }
    }
}
