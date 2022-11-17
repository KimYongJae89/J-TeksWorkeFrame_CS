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
    public partial class Snapshot : UserControl
    {
        public Snapshot()
        {
            InitializeComponent();
        }

        private void Snapshot_Load(object sender, EventArgs e)
        {
            foreach (var ctrl in pnlLeft.Controls)
            {
                if (ctrl.GetType() == typeof(Label))
                {
                    var lb = ctrl as Label;

                    lb.ForeColor = Config.Inst.TextClr;
                }

                if (ctrl.GetType() == typeof(Button))
                {
                    var btn = ctrl as Button;

                    btn.BackColor = Config.Inst.ButtonClr;
                    btn.ForeColor = Color.White;
                }
            }

            BackColor = Config.Inst.BackGroundClr;
        }
    }
}
