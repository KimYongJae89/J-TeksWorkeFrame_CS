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
    public partial class SobelControl : UserControl
    {
        public Action FilterEdit;
        public SobelControl()
        {
            InitializeComponent();
        }

        public void SetParam(IpSobelParams param)
        {
            tbxXorder.Text = param.Xorder.ToString();
            tbxYorder.Text = param.Yorder.ToString();
            tbxApertureSize.Text = param.ApertureSize.ToString();
        }

        public void EditParam(ref IpSobelParams param)
        {
            param.Xorder = Convert.ToInt32(tbxXorder.Text);
            param.Yorder = Convert.ToInt32(tbxYorder.Text);
            param.ApertureSize = Convert.ToInt32(tbxApertureSize.Text);
        }

        private void tbxXorder_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (FilterEdit != null)
                    FilterEdit();
            }
        }

        private void tbxYorder_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (FilterEdit != null)
                    FilterEdit();
            }
        }

        private void tbxApertureSize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (FilterEdit != null)
                    FilterEdit();
            }
        }
    }
}
