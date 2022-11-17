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

namespace XManager.Forms
{
    public partial class PrintNewForm : Form
    {
        private PrintDocument printDoc = new PrintDocument();
        private PageSettings pgSettings = new PageSettings();
        private PrinterSettings prtSettings = new PrinterSettings();
        //private PrintMode _printMode = PrintMode.None;
        
        public PrintNewForm()
        {
            InitializeComponent();
        }

        private void PrintNewForm_Load(object sender, EventArgs e)
        {
            try
            {
                //string strPatientID = CStatus.Instance().ThumbnailPatientID;
                //string strPatientBirthday = CStatus.Instance().ThumbnailBirthday;
                //string strPatientName = CStatus.Instance().ThumbnailPatientName;
                //string strPatientSex = CStatus.Instance().ThumbnailPatientSex;
                //string strPatientStudy = CStatus.Instance().ThumbnailStudyDate;

                //lblPatientID.Text = strPatientID;
                //lblPatientName.Text = strPatientName;
                //lblPatientSex.Text = strPatientSex;
                //lblPatientBirthday.Text = strPatientBirthday;
                //lblPatientStudy.Text = strPatientStudy;

                printDoc.PrinterSettings = prtSettings;
                printDoc.DefaultPageSettings = pgSettings;
                printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintPreviewDialog dlg = new PrintPreviewDialog();
                dlg.Document = printDoc;
                dlg.ShowDialog();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
        private bool _informationWrite = false;
       // private int pageCount = 0;
        private int stringline = 0;

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //try
            //{
            //    float width = e.PageSettings.PaperSize.Width;
            //    float height = e.PageSettings.PaperSize.Height;
            //    int posY = 100;
            //    int margin = 30;
            //    int pageMaxPosY = (int)height - 100;
            //    Font printFont = new Font("Courier New", 10);

            //    string patientID = "ID : " + CStatus.Instance().ThumbnailPatientID;
            //    string patientNameNSex = "Name : " + CStatus.Instance().ThumbnailPatientName + "(" + CStatus.Instance().ThumbnailPatientSex + ")";
            //    string birthday = CStatus.Instance().ThumbnailBirthday;
            //    string patientBirthdayNAge = "Birthday : " + birthday.Substring(0, 4) + "-" + birthday.Substring(4, 2) + "-" + birthday.Substring(6, 2) + "(" + Age(CStatus.Instance().ThumbnailBirthday) + ")";
            //    string printText = patientID + "\r\n" +
            //                        patientNameNSex + "\r\n" +
            //                        patientBirthdayNAge + "\r\n" +
            //                        "[ Finding ]" + "\r\n" +
            //                        txtFinding.Text + "\r\n" +
            //                        "[ Conclusion ]" + "\r\n" +
            //                        txtConclusion.Text + "\r\n" +
            //                        "[ Recommand ]" + "\r\n" +
            //                        txtRecommand.Text + "\r\n" +
            //                        CStatus.Instance().InstitutionName + "\r\n" +
            //                        CStatus.Instance().HospitalTelNum +"\r\n" +
            //                        CStatus.Instance().HospitalFaxNum + "\r\n" +
            //                        CStatus.Instance().HospitalAddress;

            //    txtTemp.Text = printText;

               
            //    for (int i = stringline; i < txtTemp.Lines.Count(); i++)
            //    {
            //        if (posY > pageMaxPosY)
            //        {
            //            e.HasMorePages = true;
            //            stringline = i;
            //            break;
            //        }
            //        string text = txtTemp.Lines[i];
            //        if(text == CStatus.Instance().InstitutionName)
            //        {
            //            this._informationWrite = true;
            //        }
            //        if(this._informationWrite == true)
            //            e.Graphics.DrawString(text, printFont, Brushes.Black, width/2, posY);
            //        else
            //            e.Graphics.DrawString(text, printFont, Brushes.Black, 30, posY);
            //        posY += margin;
            //        if (i == txtTemp.Lines.Count() - 1)
            //        {
            //            e.HasMorePages = false;
            //            stringline = 0;
            //            this._informationWrite = false;
            //        }
            //    }
            //}
            //catch (Exception err)
            //{
            //    Console.WriteLine(err.Message);
            //}
        }
      
        public int Age(String BirthyyyyMMdd)
        {
            int retVal = -999;
            String Birth = BirthyyyyMMdd.Replace("-", "").Replace("/", "").Replace(".", "").Trim();

            try
            {
                if (Birth.Length != 8)
                {
                    return -1;
                }

                //if (!IsNumeric(Birth))
                float output;
                if (!float.TryParse(Birth, out output))
                {
                    return -2;
                }

                DateTime dt = Convert.ToDateTime(Birth.Substring(0, 4) + "-" + Birth.Substring(4, 2) + "-" + Birth.Substring(6, 2));
                DateTime now = DateTime.Now;

                int iAge = 0;
                int by = dt.Year;
                int bm = dt.Month;
                int bd = dt.Day;

                int ny = now.Year;
                int nm = now.Month;
                int nd = now.Day;

                if (bm < nm)
                {
                    iAge = ny - by;
                }
                else if (bm == nm)
                {
                    if (bd <= nd)
                    {
                        iAge = ny - by;
                    }
                    else
                    {
                        iAge = ny - by - 1;
                    }

                }
                else
                {
                    iAge = ny - by - 1;
                }

                retVal = iAge;
            }
            catch (Exception ex)
            {
                retVal = -888;
                Console.WriteLine(ex.Message);
            }

            return retVal;
        }
    }
}
