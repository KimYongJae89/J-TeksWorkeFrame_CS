using JMatrox;
using Matrox.MatroxImagingLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XNPI
{
    public partial class TestFrm : Form
    {
        public TestFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = @"D:\4\16bitG.tif";

            MIL_ID mImg = MIL.M_NULL;
            MIL.MbufRestore(path, JMapp.Inst.MilSys, ref mImg);

            int x = 0, y = 0;
            MIL.MbufInquire(mImg, MIL.M_SIZE_X, ref x);
            MIL.MbufInquire(mImg, MIL.M_SIZE_Y, ref y);

            ushort[] data = new ushort[x * y];
            MIL.MbufGet(mImg, data);
            MIL.MbufFree(mImg);

            JMbuf2d buf = new JMbuf2d(x, y, 16 + MIL.M_UNSIGNED, MIL.M_IMAGE + MIL.M_PROC);
            buf.Put(data);


            var wndLv = buf.WndLvlTranform(30000, 35000, 0, 65535);
            ushort[] wndLvData = new ushort[wndLv.NumberOfPixels];
            wndLv.Get(wndLvData);

            //this.pbx.Image = ;

            //buf.Dispose();

            Console.WriteLine();
        }

        private void TestFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            JMapp.Inst.Dispose();
        }
    }
}
