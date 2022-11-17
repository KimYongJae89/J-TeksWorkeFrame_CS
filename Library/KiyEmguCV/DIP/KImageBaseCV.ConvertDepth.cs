using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using KiyLib.DIP;
using LibraryGlobalization.Properties;

namespace KiyEmguCV.DIP
{
    public partial class KImageBaseCV : IKConvertDepth
    {
        /// <summary>
        /// 8비트 흑백 영상으로 변환합니다.
        /// </summary>
        /// <param name="src">변환할 원본 Mat객체</param>
        /// <returns>변환된 Mat객체</returns>
        public Mat To8BitG(Mat src)
        {
            if (cvMat == null)
                throw new Exception(LangResource.ER_JIMG_CvMatIsNull);

            if (this.Depth == KDepthType.Dt_8)
                return this.cvMat;

            if (this.Depth == KDepthType.Dt_16)
            {
                var orgData = cvMat.ToImage<Gray, ushort>();

                ushort[] usCvtData = KImage.Convert3Dto1D(orgData.Data);

                //윈도우 레벨링 (16 -> 8)
                int start, end;
                byte[] dstBuf = new byte[usCvtData.Length];
                int[] histo = KHistogram.GetHistoArr(usCvtData);
                KiyLib.General.KCommon.IndexOfFirstNonZeroAtBothSide(histo, out start, out end);
                KHistogram.WndLvlTranform(dstBuf, usCvtData, start, end);
                byte[,,] rstData = KImage.Convert1Dto3D<byte, byte>(dstBuf, Width, Height, KColorType.Gray);

                var img16To8 = cvMat.ToImage<Gray, byte>();
                img16To8.Data = rstData;

                return img16To8.Mat;
            }

            var img = cvMat.ToImage<Gray, byte>();

            return img.Mat;
        }

        //24->16변환시 width가 4의배수가 아니면 이미지가 어긋난다.
        //따라서 아래의 FindMultiplesOf4GreaterThanParam()함수로 4의 배수로 맞춰준뒤 이미지를 잘라낸다.
        /// <summary>
        /// 16비트 흑백 영상으로 변환합니다.
        /// </summary>
        /// <param name="src">변환할 원본 Mat객체</param>
        /// <returns>변환된 Mat객체</returns>
        public Mat To16BitG(Mat src)
        {
            if (cvMat == null)
                throw new Exception(LangResource.ER_JIMG_CvMatIsNull);

            if (this.Depth == KDepthType.Dt_16)
                return this.cvMat;

            if (this.Depth == KDepthType.Dt_24)
            {
                Mat rtMat;
                int width4Mul = KImage.FindMultiplesOf4GreaterThanParam(Width); //4의 배수로 새로 맞춰준 Width
                var tempImg24t16 = cvMat.ToImage<Gray, byte>();

                var btCvtData = KImage.ConvertToUShortArray(KImage.Convert3Dto1D<byte>(tempImg24t16.Data));

                var img24t16 = cvMat.ToImage<Gray, ushort>();
                img24t16.Data = KImage.Convert1Dto3D<ushort, ushort>(btCvtData, width4Mul, Height, KColorType.Gray);

                //4의 배수로 맞춰준 이미지를 다시 원본크기로 잘라낸다.
                if (Width % 4 == 0)
                    rtMat = img24t16.Mat;
                else
                    rtMat = new Mat(img24t16.Mat, new System.Drawing.Rectangle(0, 0, Width, Height));

                return rtMat;
            }

            var orgData = cvMat.GetData();
            var usCvtData = KImage.ConvertToUShortArray(orgData);
            var rstData = KImage.Convert1Dto3D<ushort, ushort>(usCvtData, Width, Height, KColorType.Gray);

            var img = cvMat.ToImage<Gray, ushort>();
            img.Data = rstData;

            return img.Mat;
        }

        /// <summary>
        /// 24비트 칼라 영상으로 변환합니다.
        /// </summary>
        /// <param name="src">변환할 원본 Mat객체</param>
        /// <returns>변환된 Mat객체</returns>
        public Mat To24BitC(Mat src)
        {
            if (cvMat == null)
                throw new Exception(LangResource.ER_JIMG_CvMatIsNull);

            if (this.Depth == KDepthType.Dt_16)
            {
                var orgData = cvMat.ToImage<Gray, ushort>();

                ushort[] usCvtData = KImage.Convert3Dto1D(orgData.Data);
                byte[] byteCvtData = KImage.ConvertToByteArray(usCvtData);
                byte[,,] rstData = KImage.Convert1Dto3D<byte, byte>(byteCvtData, Width, Height, KColorType.Gray);

                var img16To24 = cvMat.ToImage<Bgr, byte>();

                int hCnt, wCnt, cCnt;
                hCnt = rstData.GetLength(0);
                wCnt = rstData.GetLength(1);
                cCnt = 3;

                for (int h = 0; h < hCnt; h++)
                    for (int w = 0; w < wCnt; w++)
                        for (int c = 0; c < cCnt; c++)
                        {
                            img16To24.Data[h, w, c] = rstData[h, w, 0];
                        }

                return img16To24.Mat;
            }

            if (this.Depth == KDepthType.Dt_24)
                return this.cvMat;

            var img = cvMat.ToImage<Bgr, byte>();

            return img.Mat;
        }
    }
}
