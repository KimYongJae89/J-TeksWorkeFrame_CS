using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageManipulator.ImageProcessingData;

namespace ImageManipulator.FilterParamControl
{
    public partial class ThresholdControl : UserControl
    {
        bool _isUpdate = false;
        public Action FilterEdit;
        public ThresholdControl()
        {
            InitializeComponent();
        }

        private void ThresholdControl_Load(object sender, EventArgs e)
        {
            _isUpdate = false;
            NumbericUpDownInitialize();
            _isUpdate = true;

            if (FilterEdit != null)
                FilterEdit();
        }

        public void SetParam(IpThresholdParams param)
        {
            nupdnValue.Value = param.Value;
            cbxOtsuEnable.Checked = param.IsOust;
        }

        public void EditParam(ref IpThresholdParams param)
        {
            param.Value = (int)nupdnValue.Value;
            param.IsOust = cbxOtsuEnable.Checked;

            if(param.IsOust)
            {
                JImage img = (JImage)Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Clone();
                int value;
                img = img.ThresholdOtsu(out value);
                nupdnValue.Value = (int)value;
            }
        }

            private void nupdnValue_ValueChanged(object sender, EventArgs e)
        {
            if (!_isUpdate)
                return;
            if (FilterEdit != null)
                FilterEdit();
        }

        private void cbxOtsuEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isUpdate)
                return;
            if (cbxOtsuEnable.Checked)
                nupdnValue.Enabled = false;
            else
                nupdnValue.Enabled = true;
            if (FilterEdit != null)
                FilterEdit();
        }

        private void nupdnValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (FilterEdit != null)
                    FilterEdit();
            }
        }

        private void NumbericUpDownInitialize()
        {
            int bit = Status.Instance().SelectedViewer.ImageManager.GetBit();
            nupdnValue.Value = 0;
            if (bit == 8)
            {
                nupdnValue.Maximum = 255;
                nupdnValue.Minimum = 0;
            }
            else if (bit == 16)
            {
                nupdnValue.Maximum = 65535;
                nupdnValue.Minimum = 0;
            }
            else { }

            if (!Status.Instance().SelectedViewer.IsColor())
            {
                if (bit == 8 || bit == 16)
                {
                    JImage displayImage = (JImage)Status.Instance().SelectedViewer.ImageManager.CalcImage.Clone();
                    int value;
                    displayImage.ThresholdOtsu(out value);
                    nupdnValue.Value = value;
                }
            }
        }
    }
}
