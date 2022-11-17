using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XNPI.Forms;

namespace XNPI.Controls
{
    public partial class ETC : UserControl
    {
        public ETC()
        {
            InitializeComponent();
        }

        private void ETC_Load(object sender, EventArgs e)
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

            BackColor = Config.Inst.BackGroundClr;
        }

        private void btnETC_Click(object sender, EventArgs e)
        {
            var settingsFrm = new SettingsFrm();
            settingsFrm.ShowDialog();
        }
    }
}
