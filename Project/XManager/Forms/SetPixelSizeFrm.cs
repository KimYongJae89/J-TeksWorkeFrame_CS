using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XManager.Forms
{
    public partial class SetPixelSizeFrm : Form
    {
        private double pitchX;
        private double pitchY;
        private double distance_pixel;
        public event EventHandler ApplyClicked;

        public double PixelSize
        {
            get { return (double)numUpDn.Value; }
            set { numUpDn.Value = (decimal)value; }
        }

        public SetPixelSizeFrm()
        {
            InitializeComponent();

            pitchX = CStatus.Instance().FovX;
            pitchY = CStatus.Instance().FovY;
        }

        public void SetPixelSize(PointF pt1, PointF pt2)
        {
            //distance_pixel = KiyLine.GetDistance(pt1, pt2);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            double sz = PixelSize;

            var onePixelRealSize_mm = sz / distance_pixel;

            CStatus.Instance().FovX = Math.Round(onePixelRealSize_mm, 5);
            CStatus.Instance().FovY = Math.Round(onePixelRealSize_mm, 5);

            ApplyClicked?.Invoke(this, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnApply.PerformClick();

            //CStatus.Instance().SetAllConfig();
        }
    }
}
