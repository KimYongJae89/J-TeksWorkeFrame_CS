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
    public partial class LaplacianControl : UserControl
    {
        public Action FilterEdit;
        public LaplacianControl()
        {
            InitializeComponent();
        }

        public void SetParam(IpLaplacianParams param)
        {
            tbxApertureSize.Text = param.ApertureSize.ToString();
        }

        public void EditParam(ref IpLaplacianParams param)
        {
            param.ApertureSize = Convert.ToInt32(tbxApertureSize.Text);
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
