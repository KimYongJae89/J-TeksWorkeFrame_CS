using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XManager.Utill;

namespace XManager.Forms
{
    public partial class PrintForm : Form
    {
        private List<Image> _selectedImageList = new List<Image>();
        private int _imageCount = 0;
        private int _entryCount = 0;
        private PrintDocument printDoc = new PrintDocument();
        private PageSettings pgSettings = new PageSettings();
        private PrinterSettings prtSettings = new PrinterSettings();

        public PrintForm()
        {
            InitializeComponent();
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            printDoc.PrinterSettings = prtSettings;
            printDoc.DefaultPageSettings = pgSettings;
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //float width = e.PageSettings.PaperSize.Width;
            //float height = e.PageSettings.PaperSize.Height;
            //int margin = 10;
            //int startX = ((int)width / 2) - ((int)height / 2 / 2) + margin;
            //int topY = margin;
            //int bottomY = ((int)height / 2) + margin;
            //int resizeImageWidth = ((int)height / 2) - (2 * margin);
            //if (cbxIsAllSelected.Checked)
            //{
            //    int count = CStatus.Instance().GetThumbnailInfoCount();
            //    if (this._entryCount == 0)//정보 넣기
            //    {
            //        string patientName = "PatientName : " + CStatus.Instance().ThumbnailPatientName;
            //        string patientAge = "PatientAge : " + CStatus.Instance().ThumbnailPatientAge;
            //        string patientSex = "PatientSex : " + CStatus.Instance().ThumbnailPatientSex;
            //        Font printFont = new Font("Courier New", 10);
            //        e.Graphics.DrawString(patientName, printFont, Brushes.Black, 10, 0);
            //        e.Graphics.DrawString(patientAge, printFont, Brushes.Black, 10, 50);
            //        e.Graphics.DrawString(patientSex, printFont, Brushes.Black, 10, 100);

            //        NewThumbnailInfo info = CStatus.Instance().GetThumbnailInfo(this._entryCount);
            //        byte[] rawData = ImageHelper.GetRawImage(info.rawPath);
            //        byte[] Trans8BitArray = ImageHelper.TranslateLutValue(rawData, info.center, info.width, info.imageWidth, info.imageHeight, 16);
            //        Image img = ImageHelper.bufferArrayToImage(Trans8BitArray, info.imageWidth, info.imageHeight);

            //        e.Graphics.DrawImage(img, startX, bottomY, resizeImageWidth, resizeImageWidth);
            //        //e.Graphics.DrawImage(img, startX, bottomY, resizeImageWidth, 10);
            //        this._entryCount++;

            //        if (count > 1)
            //            e.HasMorePages = true;
            //        if (count == this._entryCount)
            //        {
            //            e.HasMorePages = false;
            //            this._entryCount = 0;
            //        }
            //    }
            //    else
            //    {
            //        if (this._entryCount > count)
            //        {
            //            e.HasMorePages = false;
            //            this._entryCount = 0;
            //        }
            //        else
            //        {
            //            NewThumbnailInfo info = CStatus.Instance().GetThumbnailInfo(this._entryCount);
            //            byte[] rawData = ImageHelper.GetRawImage(info.rawPath);
            //            byte[] Trans8BitArray = ImageHelper.TranslateLutValue(rawData, info.center, info.width, info.imageWidth, info.imageHeight, 16);
            //            Image img = ImageHelper.bufferArrayToImage(Trans8BitArray, info.imageWidth, info.imageHeight);

            //            e.Graphics.DrawImage(img, startX, topY, resizeImageWidth, resizeImageWidth);
            //            //e.Graphics.DrawImage(img, startX, topY, resizeImageWidth, 10);
            //            this._entryCount++;
            //            if (this._entryCount >= count)
            //            {
            //                e.HasMorePages = false;
            //                this._entryCount = 0;
            //            }
            //            else
            //            {
            //                info = CStatus.Instance().GetThumbnailInfo(this._entryCount);
            //                rawData = ImageHelper.GetRawImage(info.rawPath);
            //                Trans8BitArray = ImageHelper.TranslateLutValue(rawData, info.center, info.width, info.imageWidth, info.imageHeight, 16);
            //                img = ImageHelper.bufferArrayToImage(Trans8BitArray, info.imageWidth, info.imageHeight);

            //                e.Graphics.DrawImage(img, startX, bottomY, resizeImageWidth, resizeImageWidth);
            //                //e.Graphics.DrawImage(img, startX, bottomY, resizeImageWidth, 10);
            //                this._entryCount++;
            //                if (this._entryCount > count)
            //                {
            //                    e.HasMorePages = false;
            //                    this._entryCount = 0;
            //                }
            //                else
            //                {
            //                    e.HasMorePages = true;
            //                }
            //                if (count == this._entryCount)
            //                {
            //                    e.HasMorePages = false;
            //                    this._entryCount = 0;
            //                }
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    if (this._entryCount == 0)//정보 넣기
            //    {
            //        string patientName = "PatientName : " + CStatus.Instance().ThumbnailPatientName;
            //        string patientAge = "PatientAge : " + CStatus.Instance().ThumbnailPatientAge;
            //        string patientSex = "PatientSex : " + CStatus.Instance().ThumbnailPatientSex;
            //        Font printFont = new Font("Courier New", 10);
            //        e.Graphics.DrawString(patientName, printFont, Brushes.Black, 10, 0);
            //        e.Graphics.DrawString(patientAge, printFont, Brushes.Black, 10, 50);
            //        e.Graphics.DrawString(patientSex, printFont, Brushes.Black, 10, 100);

            //        e.Graphics.DrawImage(this._selectedImageList[this._entryCount], startX, bottomY, resizeImageWidth, resizeImageWidth);
            //        //e.Graphics.DrawImage(this._selectedImageList[this._entryCount], startX, bottomY, resizeImageWidth, 10);
            //        this._entryCount++;

            //        if (this._selectedImageList.Count() > 1)
            //            e.HasMorePages = true;
            //        if (this._selectedImageList.Count() == this._entryCount)
            //        {
            //            e.HasMorePages = false;
            //            this._entryCount = 0;
            //        }
            //    }
            //    else //이미지 가운데
            //    {
            //        if (this._entryCount > this._selectedImageList.Count())
            //        {
            //            e.HasMorePages = false;
            //            this._entryCount = 0;
            //        }
            //        else
            //        {
            //            e.Graphics.DrawImage(this._selectedImageList[this._entryCount], startX, topY, resizeImageWidth, resizeImageWidth);
            //            //e.Graphics.DrawImage(this._selectedImageList[this._entryCount], startX, topY, resizeImageWidth, 10);
            //            this._entryCount++;
            //            if (this._entryCount >= this._selectedImageList.Count())
            //            {
            //                e.HasMorePages = false;
            //                this._entryCount = 0;
            //            }
            //            else
            //            {
            //                e.Graphics.DrawImage(this._selectedImageList[this._entryCount], startX, bottomY, resizeImageWidth, resizeImageWidth);
            //                //e.Graphics.DrawImage(this._selectedImageList[this._entryCount], startX, bottomY, resizeImageWidth, 10);
            //                this._entryCount++;
            //                if (this._entryCount > this._selectedImageList.Count())
            //                {
            //                    e.HasMorePages = false;
            //                    this._entryCount = 0;
            //                }
            //                else
            //                {
            //                    e.HasMorePages = true;
            //                }
            //                if (this._selectedImageList.Count() == this._entryCount)
            //                {
            //                    e.HasMorePages = false;
            //                    this._entryCount = 0;
            //                }

            //            }
            //        }
            //    }
            //}
        }

        private void cbxIsAllSelected_CheckedChanged(object sender, EventArgs e)
        {
            if(cbxIsAllSelected.Checked)
            {
                //btnApply.Enabled = false;
           
            }
            else
            {
                //btnApply.Enabled = true;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //if (CStatus.Instance().ThumbnailImage == null)
            //{
            //    MessageBox.Show("Image does not exist.");
            //    return;
            //}
                
            //if(cbxIsAllSelected.Checked)
            //{
            //    MessageBox.Show("All images are selected.");
            //    return;
            //}
            //this._imageCount++;
            //this._selectedImageList.Add(CStatus.Instance().ThumbnailImage);
            //lblImageCount.Text = this._imageCount.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(cbxIsAllSelected.Checked)
            {
                AllSelectedImagePrint();
            }
            else
            {
                SelectedImagePrint();
            }
        }

        private void AllSelectedImagePrint()
        {
            //if (CStatus.Instance().GetThumbnailInfoCount() == 0)
            //{
            //    MessageBox.Show("There is no Data");
            //    return;
            //}
            //this._entryCount = 0;
            //PrintPreviewDialog dlg = new PrintPreviewDialog();
            //dlg.Document = printDoc;
            //dlg.ShowDialog();
        }

        private void SelectedImagePrint()
        {
            if(this._selectedImageList.Count() == 0)
            {
                MessageBox.Show("There is no selected Image");
                return;
            }
            this._entryCount = 0;
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = printDoc;
            dlg.ShowDialog();
          
        }
    }
}
