using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XNPI
{
    public partial class XRay : UserControl
    {
        public XRay()
        {
            InitializeComponent();
        }

        private void XRay_Load(object sender, EventArgs e)
        {
            BackColor = Config.Inst.BackGroundClr;
        }
    }
}
