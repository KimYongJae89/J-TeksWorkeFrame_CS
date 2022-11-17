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
    public partial class DilateControl : UserControl
    {
        public Action FilterEdit;
        public DilateControl()
        {
            InitializeComponent();
        }

        public void SetParam(IpDilateParams param)
        {
            tbxIterations.Text = param.Iterations.ToString();
        }

        public void EditParam(ref IpDilateParams param)
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
