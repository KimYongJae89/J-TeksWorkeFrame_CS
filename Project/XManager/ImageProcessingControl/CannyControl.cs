using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XManager.ImageProcessingData;

namespace XManager.ImageProcessingControl
{
    public partial class CannyControl : UserControl
    {
        public Action FilterEdit;
        public CannyControl()
        {
            InitializeComponent();
        }

        public void SetParam(IpCannyParams param)
        {
            tbxThresh.Text = param.Thresh.ToString();
            tbxThreshLinking.Text = param.ThreshLinking.ToString();
        }

        public void EditParam(ref IpCannyParams param)
        {
            param.Thresh = Convert.ToInt32(tbxThresh.Text);
            param.ThreshLinking = Convert.ToInt32(tbxThreshLinking.Text);
        }

        private void tbxThresh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (FilterEdit != null)
                    FilterEdit();
            }
        }

        private void tbxThreshLinking_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (FilterEdit != null)
                    FilterEdit();
            }
        }
    }
}
