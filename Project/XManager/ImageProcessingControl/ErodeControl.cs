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
    public partial class ErodeControl : UserControl
    {
        public Action FilterEdit;
        public ErodeControl()
        {
            InitializeComponent();
        }

        public void SetParam(IpErodeParams param)
        {
            tbxIterations.Text = param.Iterations.ToString();
        }

        public void EditParam(ref IpErodeParams param)
        {
            param.Iterations = Convert.ToInt32(tbxIterations.Text);
        }

        private void tbxIterations_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (FilterEdit != null)
                    FilterEdit();
            }
        }
    }
}
