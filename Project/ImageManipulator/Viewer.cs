using ImageManipulator.Controls;
using ImageManipulator.ImageProcessingData;
using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulator
{
    public partial class Viewer : Form
    {
        private DrawBox _displayView;
        private JImage _orgImage = null;
        public bool EnableKeyEvent = true;
        //private string _filePath = "";
        public EmguImageWrapper ImageManager = new EmguImageWrapper();
        public Viewer()
        {
            InitializeComponent();
        }

        public Viewer(string filePath)
        {
            InitializeComponent();
            ImageManager.ImageFilePath = filePath;
           // _filePath = filePath;
        }

        public Viewer(JImage image, string filePath)
        {
            InitializeComponent();
            _orgImage = image;
            ImageManager.ImageFilePath = filePath;
        }

        private void Viewer_Load(object sender, EventArgs e)
        {
            _displayView = new DrawBox();
            this.DisplayPanel.Controls.Add(this._displayView);
            
            _displayView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            _displayView.Dock = System.Windows.Forms.DockStyle.Fill;
            _displayView.ParentFormResizeDelegate += FormResize;
            _displayView.CurrentPointDelegate += CurrentPointValue;
            _displayView.CropImageDelegate += CropImage;
            _displayView.DrawTypeResetDelegate = Status.Instance().MainDrawButtonUpdate;
            _displayView.TabStop = false;

            if (_orgImage != null)
            {
                ImageManager.SetOrgImage(_orgImage);
                JImage img = ImageManager.CalcDisplayImage();
                _displayView.LoadFile(ImageManager.CalcDisplayImage().ToBitmap());
            }


            //else
            //{
            //    JImage img = ImageManager.LoadImage(_filePath);
            //    _displayView.LoadFile(img.ToBitmap());
            //}

        }

        private void FormResize(int width, int height, int statusBarHeight)
        {
            int margin = 2;
            int clientWidth = this.ClientSize.Width;
            int clientHeight = this.ClientSize.Height;
            if (clientWidth <= width || clientHeight <= height + statusStrip.Height)
            {
                this.ClientSize = new Size(width + margin, height + margin + statusStrip.Height);
            }
            else
            {
            }
        }

        private void CropImage(RectangleF rect)
        {
            try
            {
                if (rect.Width == 0 || rect.Height == 0)
                    return;

                int orgWidth = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width;
                int orgHeight = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height;
                int nowWidth = Status.Instance().SelectedViewer.GetDrawBox().GetPictureBoxSize().Width;
                int nowHeight = Status.Instance().SelectedViewer.GetDrawBox().GetPictureBoxSize().Height;

                float left = rect.Left * orgWidth / (float)nowWidth;
                float top = rect.Top * orgHeight / (float)nowHeight;
                float right = rect.Right * orgWidth / (float)nowWidth;
                float bottom = rect.Bottom * orgHeight / (float)nowHeight;

                Rectangle cropRect = new Rectangle((int)left, (int)top, (int)Math.Abs(right - left), (int)Math.Abs(top - bottom));

                JImage displayImage = (JImage)(this.ImageManager.DisPlayImage.Clone());
                JImage newImage = displayImage.Crop(cropRect);

               // List<IPBase> newIPList = this.ImageManager.ImageProcessingList.ToList();
                Viewer newForm = new Viewer(newImage, this.ImageManager.ImageFilePath);

                string newFormName = Status.Instance().GetNewFileName(this.Text);
                newForm.Text = newFormName;
                //newForm.ImageManager.ImageProcessingList.AddRange(newIPList);
                newForm.Show();
                Status.Instance().SelectedViewer = newForm;

            //    _displayView.DrawType = eDrawType.None;
            //    _displayView.ModeType = eModeType.None;
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }

        }

        private void CurrentPointValue(PointF point)
        {
            StatusLabelPoint.Text = "";
            StatusLabelPoint.Text = "";

            ColorParam result = ImageManager.GetPixelValue(point);

            string pointMessage = "(" + ((int)point.X).ToString() + "," + ((int)point.Y).ToString() + ")";
            string sizeMessage = "[W : " + ImageManager.DisPlayImage.Width.ToString() + " H : " +
                ImageManager.DisPlayImage.Height.ToString() + "]";

            StatusLabelPoint.Text = pointMessage;
            StatusLabelSize.Text = sizeMessage;

            if (ImageManager.DisPlayImage.Color == KiyLib.DIP.KColorType.Gray)
            {
                StatusLabelStringValue1.Text = "Grey";
                StatusLabelValue1.Text = result.Grey.ToString();
            }
            else
            {
                StatusLabelStringValue1.Text = "R";
                StatusLabelValue1.Text = result.R.ToString();
                StatusLabelStringValue2.Text = "G";
                StatusLabelValue2.Text = result.G.ToString();
                StatusLabelStringValue3.Text = "B";
                StatusLabelValue3.Text = result.B.ToString();
            }
        }

        public bool IsColor()
        {
            bool isColor = false;
            switch (_orgImage.Color)
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

        public int GetBit()
        {
            int bit = 0;
            switch (_orgImage.Depth)
            {
                case KDepthType.Dt_8:
                    bit = 8;
                    break;
                case KDepthType.Dt_16:
                    bit = 16;
                    break;
                case KDepthType.Dt_24:
                    bit = 24;
                    break;
                case KDepthType.None:
                default:
                    break;
            }
            return bit;
        }

        public DrawBox GetDrawBox()
        {
            return _displayView;
        }
        private void Viewer_Activated(object sender, EventArgs e)
        {
            try
            {
                if (_displayView.IsActive)
                    return;

                Console.WriteLine("Active");

                 if (Status.Instance().PrevSelectedViewer == null)
                    Status.Instance().PrevSelectedViewer = this;

                if (Status.Instance().SelectedViewer == null)
                    Status.Instance().SelectedViewer = this;
                //else
                //Status.Instance().PrevSelectedViewer.GetDrawBox().Image = Status.Instance().PrevSelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();

                foreach (Form openForm in Application.OpenForms)
                {
                    if ((string)openForm.Tag == "Viewer")
                    {
                        ((Viewer)openForm).ChangeStatusStripColor(Color.Transparent);
                    }
                }
                this.ChangeStatusStripColor(Color.RosyBrown);
              
                _displayView.IsActive = true;

                if(Status.Instance().SelectedViewer.Text != this.Text)
                {
                    Status.Instance().SelectedViewer.GetDrawBox().Image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
                    Status.Instance().SelectedViewer = this;
                    Status.Instance().UpdateSubForm();
                }
                Status.Instance().SelectedViewer = this;

                Status.Instance().MainDrawButtonUpdate();


            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
       
        }

        private void Viewer_Deactivate(object sender, EventArgs e)
        {
            //_displayView.SuspendLayout();
           // _displayView.TabStop = false;
            _displayView.IsActive = false;
            Status.Instance().PrevSelectedViewer = this;
            Console.WriteLine("Deactivate");
            Console.WriteLine("-----------------------");
           // Status.Instance().SelectedViewer.GetDrawBox().SetViewPanelAutoScroll(false);
        }

        private void Viewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Status.Instance().ViewerFormClose();
           // Status.Instance().SelectedViewer = null;
        }

        private void Viewer_SizeChanged(object sender, EventArgs e)
        {
            if (Status.Instance().SelectedViewer == null)
                return;

            Status.Instance().SelectedViewer.GetDrawBox().PictureBoxMoveToCenterPictureBox();
            
        }

        private void DisplayPanel_SizeChanged(object sender, EventArgs e)
        {
        }

        private void Viewer_Click(object sender, EventArgs e)
        {
           // _displayView.SuspendLayout();
        }

        public void ChangeStatusStripColor(Color color)
        {
            statusStrip.BackColor = color;
        }
    }
}
