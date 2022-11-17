using CameraInterface;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XManager.Camera;
using XManager.Util;

namespace XManager.Forms
{
    public partial class SetUpForm : Form
    {
        public SetUpForm()
        {
            InitializeComponent();
        }

        private void FovSettingForm_Load(object sender, EventArgs e)
        {
            MultiLanguage();
            #region Camera

            for (int i = 0; i < Enum.GetNames(typeof(eCameraType)).Length; i++)
            {
                eCameraType type = (eCameraType)i;
                cbxCameraType.Items.Add(type.ToString());

                if (CStatus.Instance().Settings.CameraType == type)
                    cbxCameraType.SelectedIndex = i;
            }

            cbxAvgCnt.Items.Add("1");
            cbxAvgCnt.Items.Add("2");
            cbxAvgCnt.Items.Add("4");
            cbxAvgCnt.Items.Add("8");
            cbxAvgCnt.Items.Add("16");
            cbxAvgCnt.Items.Add("32");
            cbxAvgCnt.Items.Add("64");
            cbxAvgCnt.Items.Add("128");

            for (int i = 0; i < cbxAvgCnt.Items.Count; i++)
            {
                if (CStatus.Instance().Settings.AvgCount.ToString() == cbxAvgCnt.Items[i] as string)
                {
                    cbxAvgCnt.SelectedIndex = i;
                    break;
                }
            }

            #endregion

            #region FOV
            tbxFovWidth.Text = CStatus.Instance().Settings.FovWidth.ToString();
            tbxFovHeight.Text = CStatus.Instance().Settings.FovHeight.ToString();

            for (int i = 0; i < Enum.GetNames(typeof(eMeasurementType)).Length; i++)
            {
                eMeasurementType type = (eMeasurementType)i;
                cbxMeasurementType.Items.Add(type.ToString());

                if (CStatus.Instance().Settings.MeasureMentType == type)
                    cbxMeasurementType.SelectedIndex = i;
            }

            if (cbxMeasurementType.SelectedIndex != (int)eMeasurementType.Fov_Calibration)
            {
                tbxFovHeight.Enabled = false;
                tbxFovWidth.Enabled = false;
            }
            #endregion

            for (int i = 0; i < Enum.GetNames(typeof(eLanguageType)).Length; i++)
            {
                eLanguageType type = (eLanguageType)i;
                cbxLangugae.Items.Add(type.ToString());

                if (CStatus.Instance().Settings.Language == type)
                    cbxLangugae.SelectedIndex = i;
            }
        }

        private void MultiLanguage()
        {
            this.Text = LangResource.Setup;
            lblLanguage.Text = LangResource.LanguageType + " :";
            gbxDevice.Text = LangResource.Device;
            lblCameraType.Text = LangResource.CameraType + " :";
            lblAvgCount.Text = LangResource.AvgCount + " :";

            gbxMeasurement.Text = LangResource.Measurement;
            lblMeasurementType.Text = LangResource.Type + " :";
            btnCalibratePixelSize.Text = LangResource.CalibratePixelSize;
            lblFovMessage.Text = LangResource.FovMessage;
            lblWidth.Text = LangResource.Width + " :";
            lblHeight.Text = LangResource.Height + " :";

            btnApply.Text = LangResource.Apply;
            btnCancel.Text = LangResource.Cancel;
        }

        private void cbxMeasurementType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxMeasurementType.SelectedIndex != (int)eMeasurementType.Fov_Calibration)
            {
                tbxFovHeight.Enabled = false;
                tbxFovWidth.Enabled = false;
                btnCalibratePixelSize.Enabled = true;
            }
            else
            {
                tbxFovHeight.Enabled = true;
                tbxFovWidth.Enabled = true;
                btnCalibratePixelSize.Enabled = false;
            }
        }

        private void btnCalibratePixelSize_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "PixelCalibration")
                {
                    frm.Activate();
                    return;
                }
            }
            PixelCalibrationPage1Form form = new PixelCalibrationPage1Form();
            //form.FormCloseingDelegate = () => this.Close();
            form.Show();
        }

        private void btnFovApply_Click(object sender, EventArgs e)
        {
            CStatus.Instance().Settings.MeasureMentType = (eMeasurementType)cbxMeasurementType.SelectedIndex;
            CStatus.Instance().Settings.FovWidth = Convert.ToInt32(tbxFovWidth.Text);
            CStatus.Instance().Settings.FovHeight = Convert.ToInt32(tbxFovHeight.Text);
            CStatus.Instance().Settings.Save();

            if (CStatus.Instance().NULLCheckDrawBox())
                return;
            CStatus.Instance().GetDrawBox().ReUpdate();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if((eCameraType)cbxCameraType.SelectedIndex == eCameraType.NONE)
            {
                MessageBox.Show("Select Camera.");
                return;
            }
            CStatus.Instance().Settings.Language = (eLanguageType)cbxLangugae.SelectedIndex;
            CStatus.Instance().Settings.CameraType = (eCameraType)cbxCameraType.SelectedIndex;
            string selectAverageNum = cbxAvgCnt.SelectedItem as string;
            CStatus.Instance().Settings.AvgCount = Convert.ToInt32(selectAverageNum);
           
            CStatus.Instance().Settings.MeasureMentType = (eMeasurementType)cbxMeasurementType.SelectedIndex;
            CStatus.Instance().Settings.FovWidth = Convert.ToInt32(tbxFovWidth.Text);
            CStatus.Instance().Settings.FovHeight = Convert.ToInt32(tbxFovHeight.Text);

            CStatus.Instance().Settings.Save();
            MessageBox.Show(LangResource.ApplyMessage);

            if (CStatus.Instance().NULLCheckDrawBox())
                return;
            CStatus.Instance().GetDrawBox().ReUpdate();
        }

        private void tbxBit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    CStatus.Instance().Settings.CameraType = (tCameraType)cbxCameraType.SelectedIndex;
        //    string selectAverageNum = cbxAvgCnt.SelectedItem as string;
        //    CStatus.Instance().Settings.AvgCount = Convert.ToInt32(selectAverageNum);

        //    if (CStatus.Instance().NULLCheckDrawBox())
        //        return;
        //    CStatus.Instance().GetDrawBox().ReUpdate();
        //}
    }
}
