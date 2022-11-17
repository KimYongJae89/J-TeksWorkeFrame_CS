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
    public partial class MedianControl : UserControl
    {
        public Action FilterEdit;
        public MedianControl()
        {
            InitializeComponent();
        }

        public void SetParam(IpMedianParams param)
        {
            tbxSize.Text = param.Size.ToString();
        }

        public void EditParam(ref IpMedianParams param)
        {
            param.Size = Convert.ToInt32(tbxSize.Text);
        }

        private void tbxSize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (FilterEdit != null)
                    FilterEdit();
            }
        }
    }
}
