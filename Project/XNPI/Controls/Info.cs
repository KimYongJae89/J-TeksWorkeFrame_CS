using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XNPI.Controls
{
    public partial class Info : UserControl
    {
        public Info()
        {
            InitializeComponent();
        }

        private void Info_Load(object sender, EventArgs e)
        {
            foreach (var ctrl in grbx.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    var btn = ctrl as Button;

                    btn.BackColor = Config.Inst.ButtonClr;
                    btn.ForeColor = Color.White;
                }
            }
        }
    }
}
