using ImageManipulator.Util;
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

namespace ImageManipulator.Forms
{
    public partial class MeasurementSettingForm : Form
    {
        public Action CloseEventDelegate; // 폼 종료 시 low,high 설정 값
        public MeasurementSettingForm()
        {
            InitializeComponent();
        }

        private void MeasurementSettingForm_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(286, 302);
            this.MaximumSize = new Size(286, 302);

            MultiLanguage();

            tbxFovWidth.Text = Status.Instance().FovWidth.ToString();
            tbxFovHeight.Text = Status.Instance().FovHeight.ToString();

            for (int i = 0; i < Enum.GetNames(typeof(eMeasurementType)).Length; i++)
            {
                eMeasurementType type = (eMeasurementType)i;
                cbxMeasurementType.Items.Add(type.ToString());

                if (Status.Instance().MeasureMentType == type)
                    cbxMeasurementType.SelectedIndex = i;
            }

            for (int i = 0; i < Enum.GetNames(typeof(eMeasurementUnit)).Length; i++)
            {
                eMeasurementUnit type = (eMeasurementUnit)i;
                cbxMeasurementUnit.Items.Add(type.ToString());

                if (Status.Instance().MeasurementUnit == type)
                    cbxMeasurementUnit.SelectedIndex = i;
            }

            if (cbxMeasurementType.SelectedIndex != (int)eMeasurementType.Fov_Calibration)
            {
                tbxFovHeight.Enabled = false;
                tbxFovWidth.Enabled = false;
            }
            UnitUpdate();
        }

        private void MultiLanguage()
        {
            this.Text = LangResource.SetMeasurement;
            lblFovMessage.Text = LangResource.FovMessage;
            btnFovApply.Text = LangResource.Apply;
            btnCancel.Text = LangResource.Cancel;
            btnCalibratePixelSize.Text = LangResource.CalibratePixelSize;
            lblUnit.Text = LangResource.Unit + " :";
            lblType.Text = LangResource.Type + " :";
            lblWidth.Text = LangResource.Width + " :";
            lblHeight.Text = LangResource.Height + " :";
        }

        private void UnitUpdate()
        {
            if((eMeasurementUnit)cbxMeasurementUnit.SelectedIndex == eMeasurementUnit.Pixel)
            {
                cbxMeasurementType.Enabled = false;
                tbxFovHeight.Enabled = false;
                tbxFovWidth.Enabled = false;
                btnCalibratePixelSize.Enabled = false;
            }
            else
            {
                cbxMeasurementType.Enabled = true;
                if (cbxMeasurementType.SelectedIndex != (int)eMeasurementType.Fov_Calibration)
                {
                    tbxFovHeight.Enabled = false;
                    tbxFovWidth.Enabled = false;
                    btnCalibratePixelSize.Enabled = false;
                }
                else
                {
                    tbxFovHeight.Enabled = true;
                    tbxFovWidth.Enabled = true;
                    btnCalibratePixelSize.Enabled = true;
                }
               
            
            }
        }
        private void TextBoxKeyPress_Event(object sender, KeyPressEventArgs e)
        {
            //숫자만 입력되도록 필터링
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
        }

        private void btnFovApply_Click(object sender, EventArgs e)
        {
            Status.Instance().MeasurementUnit = (eMeasurementUnit)cbxMeasurementUnit.SelectedIndex;
            Status.Instance().MeasureMentType = (eMeasurementType)cbxMeasurementType.SelectedIndex;
            Status.Instance().FovWidth = Convert.ToInt32(tbxFovWidth.Text);
            Status.Instance().FovHeight = Convert.ToInt32(tbxFovHeight.Text);
            Status.Instance().SaveSettings();

            if (!Status.Instance().IsOpenViewerForm())
                return;
            if (Status.Instance().SelectedViewer == null)
                return;
            Status.Instance().SelectedViewer.GetDrawBox().ReUpdate();

            string message = (eMeasurementType.Fov_Calibration).ToString() + "Width : " + tbxFovWidth.Text + " Height : " + tbxFovHeight.Text;
            Status.Instance().LogManager.AddLogMessage("Measure Setting", message);
        }

        private void MeasurementSettingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CloseEventDelegate != null)
                CloseEventDelegate();
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
            PixelCalibrationPage1Form form = new PixelCalibrationPage1Form();
            form.Show();
            form.FormCloseingDelegate = () => this.Close();
                       //this.FilterForm.CloseEventDelegate = () => this.FilterForm = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxMeasurementUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitUpdate();
        }
    }
}
