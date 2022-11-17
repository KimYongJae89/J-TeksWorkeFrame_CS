using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KiyEmguCV.DIP
{
    /// <summary>
    /// 이미지 처리에 사용되는 전반적인 기능을 제공하는 클래스
    /// </summary>
    public partial class KImageCV
    {
        /// <summary>
        /// 이미지의 포맷이 무엇인지 확장자를 이용해 판별하는 함수(지원포맷: RAW, TIFF, BMP, JPEG, PNG, DCM)
        /// </summary>
        /// <param name="path">이미지의 경로</param>
        /// <returns>판별된 이미지의 포맷</returns>
        public static KImageFormat CheckImageFormat(string path)
        {
            string ext = Path.GetExtension(path);

            if (ext.Equals(".raw", StringComparison.OrdinalIgnoreCase))
                return KImageFormat.RAW;

            if (ext.Equals(".tif", StringComparison.OrdinalIgnoreCase) ||
                ext.Equals(".tiff", StringComparison.OrdinalIgnoreCase))
                return KImageFormat.TIFF;

            if (ext.Equals(".bmp", StringComparison.OrdinalIgnoreCase))
                return KImageFormat.BMP;

            if (ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                ext.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
                return KImageFormat.JPEG;

            if (ext.Equals(".png", StringComparison.OrdinalIgnoreCase))
                return KImageFormat.PNG;

            if (ext.Equals(".dcm", StringComparison.OrdinalIgnoreCase))
                return KImageFormat.DCM;

            //지원되는 포맷이 아닌경우
            return KImageFormat.None;
        }

        /// <summary>
        /// 이미지의 Depth를 판별하는 함수
        /// </summary>
        /// <param name="path">이미지의 경로</param>
        /// <returns>Depth의 타입</returns>
        public static KDepthType CheckDepthType(string path)
        {
            KDepthType depth = KDepthType.None;

            //dcm은 EmguCV에서 처리 하지 못한다. 따라서 예외처리 필요
            if (KImageCV.CheckImageFormat(path) == KImageFormat.DCM)
            {
                KDicom dcm = new KDicom(path);
                return dcm.Depth;
            }

            if (KImageCV.CheckImageFormat(path) == KImageFormat.RAW)
                return KDepthType.None;

            Mat mat = CvInvoke.Imread(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);

            if (mat.Depth == Emgu.CV.CvEnum.DepthType.Cv8U)
                depth = KDepthType.Dt_8;
            else if (mat.Depth == Emgu.CV.CvEnum.DepthType.Cv16U)
                depth = KDepthType.Dt_16;

            //color 24bit 영상의 경우 depth는 8에 채널은 3개이므로 실제depth를 구하기 위해서는 아래의 연산이 필요
            depth = (KDepthType)((int)depth * KImageCV.CheckNumOfChannels(path));

            return depth;
        }

        /// <summary>
        /// 이미지의 채널의 개수를 판별하는 함수. 채널수가 1이면 흑백, 3이면 칼라
        /// </summary>
        /// <param name="path">이미지의 경로</param>
        /// <returns>채널의 개수</returns>
        public static int CheckNumOfChannels(string path)
        {
            //dcm은 EmguCV에서 처리 하지 못한다. 따라서 예외처리 필요
            if (KImageCV.CheckImageFormat(path) == KImageFormat.DCM)
            {
                KDicom dcm = new KDicom(path);
                return dcm.NumberOfChannels;
            }

            if (KImageCV.CheckImageFormat(path) == KImageFormat.RAW)
                return 0;

            Mat mat = CvInvoke.Imread(path, ImreadModes.AnyDepth | ImreadModes.AnyColor);
            return mat.NumberOfChannels;
        }

        /// <summary>
        /// 흑백 8bit 이미지를 흑백 16bit 이미지로 변환한다.
        /// </summary>
        /// <param name="src">원본 흑백 8bit 이미지</param>
        /// <returns>변환된 16bit 이미지</returns>
        public static Mat Convert8To16_Gray(Mat src)
        {
            if (src.Depth != DepthType.Cv8U)
                throw new ArgumentException("8bit 이미지가 아닙니다");
            if (src.NumberOfChannels != 1)
                throw new ArgumentException("흑백 이미지가 아닙니다");

            Mat rst = new Mat();

            CvInvoke.Normalize(src, rst, 0, byte.MaxValue, NormType.MinMax, DepthType.Cv8U);

            return rst;
        }

        /// <summary>
        /// 흑백 16bit 이미지를 흑백 8bit 이미지로 변환한다.
        /// </summary>
        /// <param name="src">원본 흑백 16bit 이미지</param>
        /// <returns>변환된 8bit 이미지</returns>
        public static Mat Convert16To8_Gray(Mat src)
        {
            if (src.Depth != DepthType.Cv16U)
                throw new ArgumentException("16bit 이미지가 아닙니다");
            if (src.NumberOfChannels != 1)
                throw new ArgumentException("흑백 이미지가 아닙니다");

            Mat rst = new Mat();

            CvInvoke.Normalize(src, rst, 0, ushort.MaxValue, NormType.MinMax, DepthType.Cv16U);

            return rst;
        }

        //width가 4의 배수가 아닐때 체크 필요
        /// <summary>
        /// 8bit 흑백 이미지 Mat객체를 생선한다
        /// </summary>
        /// <param name="width">이미지의 가로길이</param>
        /// <param name="height">이미지의 세로길이</param>
        /// <param name="data">픽셀 data 배열</param>
        /// <returns>생성된 Mat객체</returns>
        public static Mat Create8BitGray(int width, int height, byte[] data)
        {
            var mat = new Mat(height, width, DepthType.Cv8U, 1);
            var img = mat.ToImage<Gray, byte>();

            int pixelCnt = width * height;

            Buffer.BlockCopy(data, 0, img.Data, 0, pixelCnt);

            /*int cnt = 0;
            for (int h = 0; h < 256; h++)
                for (int w = 0; w < 256; w++)
                {
                    if(data[cnt] != img.Data[h, w, 0])
                    {
                        Console.WriteLine();
                    }
                    cnt++;
                }*/

            return img.Mat;
        }

        //width가 4의 배수가 아닐때 체크 필요
        /// <summary>
        /// 16bit 흑백 이미지 Mat객체를 생선한다
        /// </summary>
        /// <param name="width">이미지의 가로길이</param>
        /// <param name="height">이미지의 세로길이</param>
        /// <param name="data">픽셀 data 배열</param>
        /// <returns>생성된 Mat객체</returns>
        public static Mat Create16BitGray(int width, int height, ushort[] data)
        {
            var mat = new Mat(height, width, DepthType.Cv16U, 1);
            var img = mat.ToImage<Gray, ushort>();

            int pixelCnt = width * height;

            //byte -> ushort 복사이므로 pixelCnt * 2 해준다
            Buffer.BlockCopy(data, 0, img.Data, 0, pixelCnt * sizeof(ushort));

            /*int cnt = 0;
            for (int h = 0; h < 620; h++)
                for (int w = 0; w < 1024; w++)
                {
                    if(data[cnt] != img.Data[h, w, 0])
                    {
                        Console.WriteLine();
                    }
                    cnt++;
                }*/

            return img.Mat;
        }

        /// <summary>
        /// 이미지 Mat객체를 생성한다
        /// </summary>
        /// <param name="width">이미지의 가로길이</param>
        /// <param name="height">이미지의 세로길이</param>
        /// <param name="depth">이미지의 Depth(bit)</param>
        /// <param name="data">픽셀 data 배열</param>
        /// <returns>생성된 Mat객체</returns>
        public static Mat Create(int width, int height, KDepthType depth, int[] data)
        {
            //var sw = new System.Diagnostics.Stopwatch();
            //sw.Start();

            int pixelCnt = width * height;

            switch (depth)
            {
                case KDepthType.Dt_8:
                    {
                        var mat = new Mat(height, width, DepthType.Cv8U, 1);
                        var img = mat.ToImage<Gray, byte>();

                        byte[] bData = Array.ConvertAll(data, val => (byte)val);
                        Buffer.BlockCopy(bData, 0, img.Data, 0, pixelCnt);

                        return img.Mat;
                    }
                case KDepthType.Dt_16:
                    {
                        Mat rtMat = new Mat();

                        using (var mat = new Mat(height, width, DepthType.Cv16U, 1))
                        {
                            var img = mat.ToImage<Gray, int>();

                            Buffer.BlockCopy(data, 0, img.Data, 0, pixelCnt * sizeof(int));

                            img.Mat.ConvertTo(rtMat, DepthType.Cv16U);

                            //var img2 = rtMat.ToImage<Gray, ushort>();

                            /*   int cnt = 0;
                               for (int h = 0; h < 620; h++)
                                   for (int w = 0; w < 1024; w++)
                                   {
                                       if (data[cnt] != img2.Data[h, w, 0])
                                       {
                                           Console.WriteLine();
                                       }
                                       cnt++;
                                   }

                               Console.WriteLine();*/

                            //sw.Stop();
                            //Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                        }

                        return rtMat;
                    }
                case KDepthType.None:
                case KDepthType.Dt_24:
                default:
                    throw new FormatException(string.Format("Not Surpported depth type: {0}", depth.ToString("g")));
            }


            throw new Exception(LibraryGlobalization.Properties.LangResource.ER_JIMG_MethodExcute);
        }
    }
}
