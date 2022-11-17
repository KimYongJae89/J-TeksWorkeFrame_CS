using ImageManipulator.Controls;
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
    public partial class SeparateColorImageForm : Form
    {
        public string FilePath = "";
        public JImage MainImage;
        private HistogramControl _redHistogramControl = null;
        private HistogramControl _greenHistogramControl = null;
        private HistogramControl _blueHistogramControl = null;

        public SeparateColorImageForm()
        {
            InitializeComponent();
        }

        private void SeparateColorImageForm_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            MultiLanguage();

            MainImage = new JImage(FilePath);
            pbxMain.Image = MainImage.ToBitmap();
            FitToScreen();

            _redHistogramControl = new HistogramControl();
            
            RedPanel.Controls.Add(_redHistogramControl);
            _redHistogramControl.Dock = DockStyle.Fill;

            _greenHistogramControl = new HistogramControl();
            //_greenHistogramControl.IsMarkVisible(false);
            GreenPanel.Controls.Add(_greenHistogramControl);
            _greenHistogramControl.Dock = DockStyle.Fill;

            _blueHistogramControl = new HistogramControl();
            //_blueHistogramControl.IsMarkVisible(false);
            BluePanel.Controls.Add(_blueHistogramControl);
            _blueHistogramControl.Dock = DockStyle.Fill;

            HistogramUpdate();
        }
        private void MultiLanguage()
        {
            this.Text = LangResource.SeparateColorImage;
            btnCovertRedChannel.Text = LangResource.ConvertRedChannelTo8BitGreyImage;
            btnCovertGreenChannel.Text = LangResource.ConvertGreenChannelTo8BitGreyImage;
            btnCovertBlueChannel.Text = LangResource.ConvertBlueChannelTo8BitGreyImage;
        }

        public void FitToScreen()
        {
            if (pbxMain.Image == null)
                return;

            int horizontalScrollValue = this.MainPanel.HorizontalScroll.Value;
            int verticalScrollValue = this.MainPanel.VerticalScroll.Value;

            SizeF sizef = new SizeF(MainImage.Width, MainImage.Height);

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

        private void SeparateColorImageForm_Resize(object sender, EventArgs e)
        {
            FitToScreen();
        }

        private void HistogramUpdate()
        {
            List<HistogramParams> redParamList = new List<HistogramParams>();
            List<HistogramParams> greenParamList = new List<HistogramParams>();
            List<HistogramParams> blueParamList = new List<HistogramParams>();

            ColorHistogram param = new ColorHistogram();
            MainImage.GetHistoColor(out param.B, out param.G, out param.R);
            //Red
            HistogramParams redParam = new HistogramParams();
            redParam.HistogramValue = Array.ConvertAll(param.R, new Converter<int, float>(ConvertIntToFloat));
            redParam.GraphColor = Color.Red;
            redParam.Width = MainImage.Width;
            redParam.Height = MainImage.Height;

            redParamList.Add(redParam);

            _redHistogramControl.ImageBit = 8;
            _redHistogramControl.IsColor = false;
            _redHistogramControl.HistogramGraphReNewal(redParamList);
            //Green
            HistogramParams greenParam = new HistogramParams();
            greenParam.HistogramValue = Array.ConvertAll(param.G, new Converter<int, float>(ConvertIntToFloat));
            greenParam.GraphColor = Color.Green;
            greenParam.Width = MainImage.Width;
            greenParam.Height = MainImage.Height;

            greenParamList.Add(greenParam);

            _greenHistogramControl.ImageBit = 8;
            _greenHistogramControl.IsColor = false;
            _greenHistogramControl.HistogramGraphReNewal(greenParamList);
            //Blue
            HistogramParams blueParam = new HistogramParams();
            blueParam.HistogramValue = Array.ConvertAll(param.B, new Converter<int, float>(ConvertIntToFloat));
            blueParam.GraphColor = Color.Blue;
            blueParam.Width = MainImage.Width;
            blueParam.Height = MainImage.Height;

            blueParamList.Add(blueParam);

            _blueHistogramControl.ImageBit = 8;
            _blueHistogramControl.IsColor = false;
            _blueHistogramControl.HistogramGraphReNewal(blueParamList);
        }

        private float ConvertIntToFloat(int input)
        {
            return (float)input;
        }

        private void btnCovertRedChannel_Click(object sender, EventArgs e)
        {
            JImage[] separateImage = MainImage.ColorChannelSeparate();
            
            Viewer form = new Viewer(separateImage[2], "");
            string fileName = Path.GetFileNameWithoutExtension(this.FilePath);
            string date = DateTime.Now.ToString("yyMMddHHmmss");

            form.Text = fileName + "_CovertRedChannelTo8BitGreyImage_" + date;
            form.Show();
            Status.Instance().PrevSelectedViewer = form;
            Status.Instance().SelectedViewer = form;
            if (Status.Instance().FilterForm != null)
                Status.Instance().FilterForm.ComboBoxUpdate();

            Status.Instance().LogManager.AddLogMessage("Convert ", "Covert Red Channel To 8Bit Grey Image");
        }

        private void btnCovertGreenChannel_Click(object sender, EventArgs e)
        {
            JImage[] separateImage = MainImage.ColorChannelSeparate();

            Viewer form = new Viewer(separateImage[1], "");
            string fileName = Path.GetFileNameWithoutExtension(this.FilePath);
            string date = DateTime.Now.ToString("yyMMddHHmmss");

            form.Text = fileName + "_CovertGreenChannelTo8BitGreyImage_" + date;
            form.Show();
            Status.Instance().PrevSelectedViewer = form;
            Status.Instance().SelectedViewer = form;
            if (Status.Instance().FilterForm != null)
                Status.Instance().FilterForm.ComboBoxUpdate();

            Status.Instance().LogManager.AddLogMessage("Convert ", "Covert Green Channel To 8Bit Grey Image");
        }

        private void btnCovertBlueChannel_Click(object sender, EventArgs e)
        {
            JImage[] separateImage = MainImage.ColorChannelSeparate();

            Viewer form = new Viewer(separateImage[0], "");
            string fileName = Path.GetFileNameWithoutExtension(this.FilePath);
            string date = DateTime.Now.ToString("yyMMddHHmmss");

            form.Text = fileName + "_CovertBlueChannelTo8BitGreyImage_" + date;
            form.Show();
            Status.Instance().PrevSelectedViewer = form;
            Status.Instance().SelectedViewer = form;
            if (Status.Instance().FilterForm != null)
                Status.Instance().FilterForm.ComboBoxUpdate();

            Status.Instance().LogManager.AddLogMessage("Convert ", "Covert Blue Channel To 8Bit Grey Image");
        }
    }
}
