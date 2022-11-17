using Emgu.CV;
using Emgu.CV.CvEnum;
using KiyEmguCV.DIP;
using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiyEmguCV.Forms
{
    /// <summary>
    /// Raw 이미지를 불러올때 사용되는 Form
    /// </summary>
    public partial class RawImageConvertForm : Form
    {
        private string path;
        private Mat cvMat;
        private int height = 0, width = 0, numberOfChannels = 1;
        private DepthType depthCV = DepthType.Cv8U;
        private KDepthType depth = KDepthType.None;

        /// <summary>
        /// 이미지의 색상 채널수 (흑백:1, 칼라:3)
        /// </summary>
        public int NumberOfChannels
        {
            get { return numberOfChannels; }
            private set { numberOfChannels = value; }
        }

        /// <summary>
        /// 이미지의 Depth
        /// </summary>
        public KDepthType Depth
        {
            get { return depth; }
            private set { depth = value; }
        }

        /// <summary>
        /// 이미지 변환및 생성에 사용할 EmguCV Mat객체
        /// </summary>
        public Mat CvMat
        {
            get { return cvMat; }
            private set { cvMat = value; }
        }


        /// <summary>
        /// RawImageConvertForm의 생성자
        /// </summary>
        /// <param name="path_rawFile">변환할 Raw파일의 경로</param>
        public RawImageConvertForm(string path_rawFile)
            : this()
        {
            path = path_rawFile;
        }

        private RawImageConvertForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// FileSize를 Byte단위로 계산한다.
        /// 계산은 픽셀의 사이즈만 해당되며 헤더파일등의 사이즈는 고려되지 않는다
        /// </summary>
        /// <returns>계산된 결과값, Byte 단위이다</returns>
        private int CalcFileSize_Byte()
        {
            int rtResultSize = 0;
            int pixelCnt = width * height;
            int depthSize = 0;

            if (depthCV == DepthType.Cv8U)
                depthSize = 1;
            if (depthCV == DepthType.Cv16U)
                depthSize = 2;

            rtResultSize = pixelCnt * depthSize * numberOfChannels;

            return rtResultSize;
        }


        // Control Events
        private void RawImageConvertForm_Load(object sender, EventArgs e)
        {
            lbSrcFilePath.Text = path;
            lbSrcFileSize.Text = new System.IO.FileInfo(path).Length.ToString("N0");
        }

        private void nmrImgSize_ValueChanged(object sender, EventArgs e)
        {
            height = (int)nmrImgHeight.Value;
            width = (int)nmrImgWidth.Value;

            lbCalcFileSize.Text = CalcFileSize_Byte().ToString("N0");
        }

        private void radBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (radBtn8BG.Checked)
            {
                depthCV = DepthType.Cv8U;
                numberOfChannels = 1;
            }
            if (radBtn16BG.Checked)
            {
                depthCV = DepthType.Cv16U;
                numberOfChannels = 1;
            }
            if (radBtn24BC.Checked)
            {
                depthCV = DepthType.Cv8U;
                numberOfChannels = 3;
            }

            lbCalcFileSize.Text = CalcFileSize_Byte().ToString("N0");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            cvMat = new Mat(height, width, depthCV, numberOfChannels);
            byte[] data8;
            ushort[] data16;
            int index = 0;
            depth = KDepthType.None;

            if (depthCV == DepthType.Cv8U && numberOfChannels == 1)
                depth = KDepthType.Dt_8;
            if (depthCV == DepthType.Cv16U && numberOfChannels == 1)
                depth = KDepthType.Dt_16;
            if (depthCV == DepthType.Cv8U && numberOfChannels == 3)
                depth = KDepthType.Dt_24;

            using (var inputStream = File.Open(path, FileMode.Open))
            using (var reader = new BinaryReader(inputStream))
            {
                if (depth == KDepthType.Dt_8 ||
                    depth == KDepthType.Dt_24)
                {
                    data8 = new byte[width * height * numberOfChannels];

                    while (inputStream.Position < inputStream.Length)
                    {
                        if (index >= data8.Length)
                            break;

                        data8[index] = reader.ReadByte();
                        index++;
                    }

                    cvMat.SetTo(data8);
                }
                else // depth == KDepthType.Dt_16
                {
                    data16 = new ushort[width * height * numberOfChannels];

                    while (inputStream.Position < inputStream.Length)
                    {
                        if (index >= data16.Length)
                            break;

                        data16[index] = reader.ReadUInt16();
                        index++;
                    }

                    cvMat.SetTo(data16);
                }
            }

            this.Close();
        }
    }
}
