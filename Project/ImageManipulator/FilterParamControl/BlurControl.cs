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
    public partial class BlurControl : UserControl
    {
        public Action FilterEdit;
        public BlurControl()
        {
            InitializeComponent();
        }

        public void SetParam(IpBlurParams param)
        {
            tbxBlurWidth.Text = param.Width.ToString();
            tbxBlurHeight.Text = param.Height.ToString();
        }

        public void EditParam(ref IpBlurParams param)
        {
            param.Width = Convert.ToInt32(tbxBlurWidth.Text);
            param.Height = Convert.ToInt32(tbxBlurHeight.Text);
        }

        private void tbxBlurWidth_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (FilterEdit != null)
                    FilterEdit();
            }
        }

        private void tbxBlurHeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (FilterEdit != null)
                    FilterEdit();
            }
        }
    }
}
