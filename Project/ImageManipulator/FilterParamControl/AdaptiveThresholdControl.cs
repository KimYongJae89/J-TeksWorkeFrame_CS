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
using KiyLib.DIP;

namespace ImageManipulator.FilterParamControl
{
    public partial class AdaptiveThresholdControl : UserControl
    {
        public Action FilterEdit;
        bool _isUpdate = false;
        public AdaptiveThresholdControl()
        {
            InitializeComponent();
        }

        private void AdaptiveThresholdControl_Load(object sender, EventArgs e)
        {
            _isUpdate = false;

            nupdnBlockSize.Minimum = 0;
            nupdnBlockSize.Maximum = 99;

            nupdnWeight.Minimum = 0;
            nupdnWeight.Maximum = 99;

         
            for (int i = 0; i < Enum.GetNames(typeof(ThresAdaptiveType)).Length; i++)
            {
                ThresAdaptiveType type = (ThresAdaptiveType)i;
                cbxType.Items.Add(type.ToString());
            }
            cbxType.SelectedIndex = 0;

            _isUpdate = true;

            if (FilterEdit != null)
                FilterEdit();
        }

        public void SetParam(IpThresholdAdaptiveParams param)
        {
            cbxType.SelectedIndex = (int)param.type;
            nupdnBlockSize.Value = param.BlockSize;
            nupdnWeight.Value = param.Weight;
        }

        public void EditParam(ref IpThresholdAdaptiveParams param)
        {
            param.type = (ThresAdaptiveType)cbxType.SelectedIndex;
            param.BlockSize = (int)nupdnBlockSize.Value;
            param.Weight = (int)nupdnWeight.Value;
        }

        private void cbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isUpdate)
                return;
            if (FilterEdit != null)
                FilterEdit();
        }

        private void nupdnBlockSize_ValueChanged(object sender, EventArgs e)
        {
            if (!_isUpdate)
                return;
            if (FilterEdit != null)
                FilterEdit();
        }

        private void nupdnWeight_ValueChanged(object sender, EventArgs e)
        {
            if (!_isUpdate)
                return;
            if (FilterEdit != null)
                FilterEdit();
        }
    }
}
