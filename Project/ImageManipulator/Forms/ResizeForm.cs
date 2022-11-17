using ImageManipulator.ImageProcessingData;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulator.Forms
{
    public partial class ResizeForm : Form
    {
        public ResizeForm()
        {
            InitializeComponent();
        }

        private void ResizeForm_Load(object sender, EventArgs e)
        {
            Status.Instance().LogManager.AddLogMessage("Resize Form Open", "");

            this.MinimumSize = new Size(300, 155);
            this.MaximumSize = new Size(300, 155);

            this.Text = LangResource.Resize;
            lblMessage.Text = LangResource.ResizeMessage;
            btnApply.Text = LangResource.Apply;
            tbxWidthValue.Text = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width.ToString();
            tbxHeightValue.Text = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height.ToString();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int width = Convert.ToInt32(tbxWidthValue.Text);
            int height = Convert.ToInt32(tbxHeightValue.Text);

            if(width <=0 || height <=0)
            {
                MessageBox.Show("Width and height must be greater than zero.");
                return;
            }
            List<IPBase> newIPList = Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.ToList();
            JImage orgImage = (JImage)(Status.Instance().SelectedViewer.ImageManager.GetOrgImage()).Clone();

            double widthRatio = (double)width / orgImage.Width;
            double heightRatio = (double)height / orgImage.Height;

            string sourceImageName = Status.Instance().SelectedViewer.Text;
            Viewer newForm = new Viewer(orgImage, Status.Instance().SelectedViewer.ImageManager.ImageFilePath);
            string newFormName = Status.Instance().GetNewFileName(sourceImageName);
            newForm.Text = newFormName;
            newForm.ImageManager.ImageProcessingList.AddRange(newIPList);

            IP_Resize resize = new IP_Resize();
            IpResizeParams param = new IpResizeParams();
            param.WidthScale = widthRatio;
            param.HeightScale = heightRatio;
            resize.SetParam(param);

            newForm.ImageManager.ImageProcessingList.Add(resize);
            newForm.Show();

            Status.Instance().LogManager.AddLogMessage("Apply Resize", "");

            this.Close();
        }



    }
}
