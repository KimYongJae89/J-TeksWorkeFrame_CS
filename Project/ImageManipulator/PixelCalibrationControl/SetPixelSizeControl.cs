using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryGlobalization.Properties;

namespace ImageManipulator.PixelCalibrationControl
{
    public partial class SetPixelSizeControl : UserControl
    {
        public SetPixelSizeControl()
        {
            InitializeComponent();

            lblMessage.Text = LangResource.PixelCalibrationPage2 + "\n" + LangResource.PixelCalibrationPage1_1;
        }

        private void tbxActualLength_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            //숫자만 입력되도록 필터링
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || !(e.KeyChar == Convert.ToChar(Keys.Oemcomma))))    //숫자, 백스페이스, 콤마를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
        }

        public string GetDistance_mm()
        {
            return tbxActualLength.Text;
        }
    }
}
