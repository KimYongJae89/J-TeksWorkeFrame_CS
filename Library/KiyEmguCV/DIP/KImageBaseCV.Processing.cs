using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using KiyLib.DIP;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyEmguCV.DIP
{
    public partial class KImageBaseCV : IKProcessing
    {
        /// <summary>
        /// 이진화 시킨다. 흑백 이미지에서만 사용가능하다.
        /// </summary>
        /// <param name="val">임계치 값</param>
        /// <returns>결과 이미지</returns>
        public Mat Threshold(int val)
        {
            if (this.Color == KColorType.Color)
                throw new Exception(LangResource.ER_JIMG_CantSupportColorImg);

            if (this.Depth == KDepthType.Dt_8 &&
                (byte.MaxValue < val || val < 0))
                throw new Exception("8bit에서는 0~255사이의 값만 입력할수 있습니다.");

            if (this.Depth == KDepthType.Dt_16 &&
                (ushort.MaxValue < val || val < 0))
                throw new Exception("16bit에서는 0~65535사이의 값만 입력할수 있습니다.");

            Mat dstMat = new Mat();
            int thresMaxVal = 0;

            if (Depth == KDepthType.Dt_8)
                thresMaxVal = byte.MaxValue;
            else if (Depth == KDepthType.Dt_16)
                thresMaxVal = ushort.MaxValue;
            else
                throw new Exception(LangResource.ER_JIMG_MethodExcute);

            CvInvoke.Threshold(cvMat, dstMat, val, thresMaxVal, ThresholdType.Binary);
            return dstMat;
        }

        /// <summary>
        /// Adaptive Threshold를 실행한다. 흑백 이미지에서만 사용가능하다.
        /// </summary>
        /// <param name="algoType">threshold 방식</param>
        /// <param name="blockSize">thresholding을 적용할 영역 사이즈, 
        /// 짝수 입력시 홀수로 변환돼서 적용됩니다.
        /// (ex)blockSize = 14; -> blockSize = 13로 내부에서 변경돼 적용</param>
        /// <param name="c">평균이나 가중평균에서 차감할 값</param>
        /// <returns>결과 이미지</returns>
        public Mat ThresholdAdaptive(ThresAdaptiveType algoType, int blockSize, int c)
        {
            if (this.Color == KColorType.Color)
                throw new Exception(LangResource.ER_JIMG_CantSupportColorImg);

            blockSize = (blockSize % 2 == 0) ? blockSize - 1 : blockSize;

            int thType = (int)algoType;
            Mat dstMat = new Mat();
            Mat mat16 = new Mat();

            if (Depth == KDepthType.Dt_8)
            {
                CvInvoke.AdaptiveThreshold(cvMat, dstMat, byte.MaxValue,
                    (AdaptiveThresholdType)thType,
                    ThresholdType.Binary,
                    blockSize, c);

                return dstMat;
            }
            else if (Depth == KDepthType.Dt_16)
            {
                CvInvoke.Normalize(cvMat, mat16, 0, byte.MaxValue, NormType.MinMax, DepthType.Cv8U);
                CvInvoke.AdaptiveThreshold(mat16, dstMat, ushort.MaxValue,
                   (AdaptiveThresholdType)thType,
                   ThresholdType.Binary,
                   blockSize, c);
                CvInvoke.Normalize(dstMat, dstMat, 0, ushort.MaxValue, NormType.MinMax, DepthType.Cv16U);

                return dstMat;
            }
            else
                throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        /// <summary>
        /// Otsu Threshold를  실행한다. 흑백 이미지에서만 사용가능하다.
        /// </summary>
        /// <param name="usedThresVal">Otsu threshold에 사용된 임계값</param>
        /// <returns>결과 이미지</returns>
        public Mat ThresholdOtsu(out int usedThresVal)
        {
            if (this.Color == KColorType.Color)
                throw new Exception(LangResource.ER_JIMG_CantSupportColorImg);

            Mat dstMat = new Mat();
            Mat mat16 = new Mat();

            if (Depth == KDepthType.Dt_8)
            {
                usedThresVal = (int)CvInvoke.Threshold(cvMat, dstMat, 0, byte.MaxValue, ThresholdType.Otsu);
                return dstMat;
            }
            else if (Depth == KDepthType.Dt_16)
            {
                CvInvoke.Normalize(cvMat, mat16, 0, byte.MaxValue, NormType.MinMax, DepthType.Cv8U);
                usedThresVal = (int)CvInvoke.Threshold(mat16, dstMat, 0, byte.MaxValue, ThresholdType.Otsu);
                CvInvoke.Normalize(dstMat, dstMat, 0, ushort.MaxValue, NormType.MinMax, DepthType.Cv16U);

                return dstMat;
            }
            else
                throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        /// <summary>
        /// 이미지 색상을 반전시킨다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public Mat Inverse()
        {
            switch (Depth)
            {
                case KDepthType.Dt_8:
                    var img8 = cvMat.ToImage<Gray, byte>();
                    return img8.Not().Mat;

                case KDepthType.Dt_16:
                    var img16 = cvMat.ToImage<Gray, ushort>();
                    return img16.Not().Mat;

                case KDepthType.Dt_24:
                    var img24 = cvMat.ToImage<Bgr, byte>();
                    return img24.Not().Mat;
            }

            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        /// <summary>
        /// Lut(Look Up Table)에 맞게 색상을 변경한다.
        /// </summary>
        /// <param name="lutData">매핑할 Lut 데이터</param>
        /// <returns>결과 이미지</returns>
        public Mat Lut(int[] lutData)
        {
            if (data8 == null && data16 == null)
                throw new Exception(LangResource.ER_JIMG_data8And16isNull);
            if (data8 != null && data16 != null)
                throw new Exception(LangResource.ER_JIMG_data8And16isNotNull); //둘중에 하나만 있어야 한다.

            int hL = 0, wL = 0, cL = 0;

            if (data8 != null)
            {
                hL = data8.GetLength(0); wL = data8.GetLength(1); cL = data8.GetLength(2);
            }

            if (data16 != null)
            {
                hL = data16.GetLength(0); wL = data16.GetLength(1); cL = data16.GetLength(2);
            }


            int len = lutData.Length;
            switch (Depth)
            {
                case KDepthType.Dt_8:
                    {
                        if (len != byte.MaxValue + 1)
                            throw new Exception(LangResource.ER_JIMG_LenIsMustBe256); //Dt_8에서 lutData.Length는 256이여야 합니다."

                        byte[,,] dst = new byte[hL, wL, cL];

                        KImage.LUT(data8, dst, lutData);
                        var img8 = cvIImg.Clone() as Image<Gray, byte>;

                        for (int h = 0; h < hL; h++)
                            for (int w = 0; w < wL; w++)
                            {
                                img8.Data[h, w, 0] = dst[h, w, 0];
                            }

                        var rtMat = img8.Mat.Clone();
                        img8.Dispose();
                        return rtMat;
                    }

                case KDepthType.Dt_16:
                    {
                        if (len != ushort.MaxValue + 1)
                            throw new Exception(LangResource.ER_JIMG_LenIsMustBe65536); //Dt_16에서 lutData.Length는 65536이여야 합니다.

                        ushort[,,] dst = new ushort[hL, wL, cL];

                        KImage.LUT(data16, dst, lutData);
                        var img16 = cvIImg.Clone() as Image<Gray, ushort>;

                        for (int h = 0; h < hL; h++)
                            for (int w = 0; w < wL; w++)
                            {
                                img16.Data[h, w, 0] = dst[h, w, 0];
                            }

                        var rtMat = img16.Mat.Clone();
                        img16.Dispose();
                        return rtMat;
                    }

                case KDepthType.Dt_24:
                    {
                        if (len != byte.MaxValue + 1)
                            throw new Exception(LangResource.ER_JIMG_LenIsMustBe256); //Dt_24에서 lutData.Length는 256이여야 합니다."

                        byte[,,] dst = new byte[hL, wL, cL];

                        KImage.LUT(data8, dst, lutData);
                        var img24 = cvIImg.Clone() as Image<Bgr, byte>;

                        for (int h = 0; h < hL; h++)
                            for (int w = 0; w < wL; w++)
                                for (int c = 0; c < cL; c++)
                                {
                                    img24.Data[h, w, c] = dst[h, w, c];
                                }

                        var rtMat = img24.Mat.Clone();
                        img24.Dispose();
                        return rtMat;
                    }
            }

            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }


        // Filter
        /// <summary>
        /// Blur 필터를 적용한다.
        /// </summary>
        /// <param name="width">필터에 적용할 width 값</param>
        /// <param name="height">필터에 적용할 height 값</param>
        /// <returns>결과 이미지</returns>
        public Mat FT_Blur(int width = 3, int height = 3)
        {
            switch (Depth)
            {
                case KDepthType.Dt_8:
                case KDepthType.Dt_16:
                    Image<Gray, float> imgG = cvMat.ToImage<Gray, float>();
                    return imgG.SmoothBlur(width, height).Mat.Clone();
                case KDepthType.Dt_24:
                    Image<Bgr, float> imgC = cvMat.ToImage<Bgr, float>();
                    return imgC.SmoothBlur(width, height).Mat.Clone();
            }

            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        /// <summary>
        /// Sharp1 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public Mat FT_Sharp1()
        {
            return Filter(KConvPreset.Sharp1);
        }

        /// <summary>
        /// Sharp2 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public Mat FT_Sharp2()
        {
            return Filter(KConvPreset.Sharp2);
        }

        /// <summary>
        /// Sobel 필터를 적용한다.
        /// </summary>
        /// <param name="xorder">필터에 적용할 xorder 값</param>
        /// <param name="yorder">필터에 적용할 yorder 값</param>
        /// <param name="apertureSize">필터에 적용할 apertureSize 값</param>
        /// <returns>결과 이미지</returns>
        public Mat FT_Sobel(int xorder = 1, int yorder = 1, int apertureSize = 3)
        {
            switch (Depth)
            {
                case KDepthType.Dt_8:
                case KDepthType.Dt_16:
                    Image<Gray, float> imgG = cvMat.ToImage<Gray, float>();
                    return imgG.Sobel(xorder, yorder, apertureSize).Mat.Clone();
                case KDepthType.Dt_24:
                    Image<Bgr, float> imgC = cvMat.ToImage<Bgr, float>();
                    return imgC.Sobel(xorder, yorder, apertureSize).Mat.Clone();
            }

            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        /// <summary>
        /// Laplacian 필터를 적용한다,
        /// </summary>
        /// <param name="apertureSize">필터에 적용할 apertureSize 값</param>
        /// <returns>결과 이미지</returns>
        public Mat FT_Laplacian(int apertureSize = 3)
        {
            switch (Depth)
            {
                case KDepthType.Dt_8:
                case KDepthType.Dt_16:
                    Image<Gray, float> imgG = cvMat.ToImage<Gray, float>();
                    return imgG.Laplace(apertureSize).Mat.Clone();
                case KDepthType.Dt_24:
                    Image<Bgr, float> imgC = cvMat.ToImage<Bgr, float>();
                    return imgC.Laplace(apertureSize).Mat.Clone();
            }

            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        /// <summary>
        /// Canny 필터를 적용한다.
        /// </summary>
        /// <param name="thresh">필터에 적용할 thresh 값</param>
        /// <param name="threshLinking">필터에 적용할 threshLinking 값</param>
        /// <returns>결과 이미지</returns>
        public Mat FT_Canny(double thresh = 30, double threshLinking = 200)
        {
            switch (Depth)
            {
                case KDepthType.Dt_8:
                    Image<Gray, byte> imgG8 = cvMat.ToImage<Gray, byte>();
                    return imgG8.Canny(thresh, threshLinking).Mat.Clone();
                case KDepthType.Dt_16:
                    Image<Gray, byte> imgG16 = cvMat.ToImage<Gray, byte>();
                    return imgG16.Canny(thresh, threshLinking).Mat.Clone();
                case KDepthType.Dt_24:
                    Image<Bgr, byte> imgC = cvMat.ToImage<Bgr, byte>();
                    return imgC.Canny(thresh, threshLinking).Mat.Clone();
            }
            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        /// <summary>
        /// HorizonEdge 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public Mat FT_HorizonEdge()
        {
            return Filter(KConvPreset.HorizonEdge);
        }

        /// <summary>
        /// VerticalEdge 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public Mat FT_VerticalEdge()
        {
            return Filter(KConvPreset.VerticalEdge);
        }

        /// <summary>
        /// Median 필터를 적용한다.
        /// </summary>
        /// <param name="size">필터에 적용할 size 값</param>
        /// <returns>결과 이미지</returns>
        public Mat FT_Median(int size = 3)
        {
            switch (Depth)
            {
                case KDepthType.Dt_8:
                case KDepthType.Dt_16:
                    Image<Gray, float> imgG = cvMat.ToImage<Gray, float>();
                    return imgG.SmoothMedian(size).Mat.Clone();
                case KDepthType.Dt_24:
                    Image<Bgr, float> imgC = cvMat.ToImage<Bgr, float>();
                    return imgC.SmoothMedian(size).Mat.Clone();
            }

            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        /// <summary>
        /// Dilate 필터를 적용한다.
        /// </summary>
        /// <param name="iterations">필터에 적용할 iterations 값</param>
        /// <returns>결과 이미지</returns>
        public Mat FT_Dilate(int iterations = 1)
        {
            switch (Depth)
            {
                case KDepthType.Dt_8:
                case KDepthType.Dt_16:
                    Image<Gray, float> imgG = cvMat.ToImage<Gray, float>();
                    return imgG.Dilate(iterations).Mat.Clone();
                case KDepthType.Dt_24:
                    Image<Bgr, float> imgC = cvMat.ToImage<Bgr, float>();
                    return imgC.Dilate(iterations).Mat.Clone();
            }

            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        /// <summary>
        /// Erode 필터를 적용한다.
        /// </summary>
        /// <param name="iterations">필터에 적용할 iterations 값</param>
        /// <returns>결과 이미지</returns>
        public Mat FT_Erode(int iterations = 1)
        {
            switch (Depth)
            {
                case KDepthType.Dt_8:
                case KDepthType.Dt_16:
                    Image<Gray, float> imgG = cvMat.ToImage<Gray, float>();
                    return imgG.Erode(iterations).Mat.Clone();
                case KDepthType.Dt_24:
                    Image<Bgr, float> imgC = cvMat.ToImage<Bgr, float>();
                    return imgC.Erode(iterations).Mat.Clone();
            }

            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        /// <summary>
        /// Average 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public Mat FT_Average()
        {
            return Filter(KConvPreset.Average);
        }

        /// <summary>
        /// Convolution 필터를 적용한다.
        /// </summary>
        /// <param name="kernel">적용할 kernel배열</param>
        /// <returns>결과 이미지</returns>
        public Mat Filter(float[,] kernel)
        {
            ConvolutionKernelF ck = new ConvolutionKernelF(kernel);

            switch (Depth)
            {
                case KDepthType.Dt_8:
                case KDepthType.Dt_16:
                    Image<Gray, float> imgG = cvMat.ToImage<Gray, float>();
                    var mt = imgG.Convolution(ck);


                    return imgG.Convolution(ck).Mat.Clone();

                case KDepthType.Dt_24:
                    Image<Bgr, float> imgC = cvMat.ToImage<Bgr, float>();
                    return imgC.Convolution(ck).Mat.Clone();
            }

            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }


        // WindowLeveling
        /// <summary>
        /// 윈도우 레벨링을 실행한다.(흑백 전용)
        /// </summary>
        /// <returns>결과 이미지</returns>
        public Mat WndLvGray()
        {
            int lowestVal, highestVal;

            switch (this.Depth)
            {
                case KDepthType.Dt_8:
                    {
                        //highestVal = data8.Cast<byte>().Max();
                        //lowestVal = data8.Cast<byte>().Min();

                        highestVal = byte.MaxValue;
                        lowestVal = byte.MinValue;

                        return WndLvGray(lowestVal, highestVal);
                    }

                case KDepthType.Dt_16:
                    {
                        highestVal = data16.Cast<ushort>().Max();
                        lowestVal = data16.Cast<ushort>().Min();

                        return WndLvGray(lowestVal, highestVal);
                    }

                case KDepthType.None:
                    throw new FormatException(LangResource.ER_JIMG_DepthFmt);
                case KDepthType.Dt_24:
                    throw new FormatException(LangResource.ER_JIMG_CantSupportColorImg);
            }

            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        /// <summary>
        /// 윈도우 레벨링을 실행한다.(흑백 전용)
        /// </summary>
        /// <param name="min">레벨링할 영역의 최소값</param>
        /// <param name="max">레벨링할 영역의 최대값</param>
        /// <param name="divStep">나눌 단계</param>
        /// <returns>결과 이미지</returns>
        public Mat WndLvGray(int min, int max, int divStep = 256)
        {
            if (min > max)
                throw new ArgumentException("min is bigger than max");

            double div, differ;
            int pixel, upperLim = divStep - 1;

            differ = max - min;
            div = differ / divStep;

            switch (this.Depth)
            {
                case KDepthType.Dt_8:
                    {
                        if (max > byte.MaxValue)
                            throw new ArgumentException("max is bigger than 255");
                        if (divStep > byte.MaxValue + 1)
                            throw new ArgumentException("8bit images can not be divided more than 256 steps");

                        Image<Gray, byte> src8 = cvIImg as Image<Gray, byte>;
                        Mat dstMat = new Mat(new Size(src8.Width, src8.Height), Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                        dstMat.SetTo(new MCvScalar(0));
                        Image<Gray, byte> dst8 = dstMat.ToImage<Gray, byte>();

                        byte[] lut = new byte[256];
                        int hL = src8.Height;
                        int wL = src8.Width;

                        //Lut 계산
                        for (int i = 0; i < lut.Length; i++)
                        {
                            pixel = (int)((i - min) / div);

                            if (pixel < 0) pixel = 0;
                            if (pixel > upperLim) pixel = upperLim;

                            lut[i] = (byte)pixel;
                        }

                        int idx;
                        for (int h = 0; h < hL; h++)
                        {
                            for (int w = 0; w < wL; w++)
                            {
                                idx = data8[h, w, 0];
                                dst8.Data[h, w, 0] = lut[idx];
                            }
                        }

                        return dst8.Mat;
                    }

                case KDepthType.Dt_16:
                    {
                        if (max > ushort.MaxValue)
                            throw new ArgumentException("max is bigger than 65535");
                        if (divStep > ushort.MaxValue + 1)
                            throw new ArgumentException("16bit images can not be divided more than 65535 steps");

                        Image<Gray, ushort> src16 = cvIImg as Image<Gray, ushort>;
                        Mat dstMat = new Mat(new Size(src16.Width, src16.Height), Emgu.CV.CvEnum.DepthType.Cv16U, 1);
                        dstMat.SetTo(new MCvScalar(0));
                        Image<Gray, ushort> dst16 = dstMat.ToImage<Gray, ushort>();

                        ushort[] lut = new ushort[65536];
                        int hL = src16.Height;
                        int wL = src16.Width;

                        //Lut 계산
                        for (int i = 0; i < lut.Length; i++)
                        {
                            pixel = (int)((i - min) / div);

                            if (pixel < 0) pixel = 0;
                            if (pixel > upperLim) pixel = upperLim;

                            lut[i] = (ushort)pixel;
                        }

                        int idx;
                        for (int h = 0; h < hL; h++)
                        {
                            for (int w = 0; w < wL; w++)
                            {
                                idx = data16[h, w, 0];
                                dst16.Data[h, w, 0] = lut[idx];
                            }
                        }

                        return dst16.Mat;
                    }

                case KDepthType.None:
                    throw new FormatException(LangResource.ER_JIMG_DepthFmt);
                case KDepthType.Dt_24:
                    throw new FormatException(LangResource.ER_JIMG_CantSupportColorImg);
            }

            throw new Exception(LangResource.ER_JIMG_MethodExcute);
        }

        /// <summary>
        /// 윈도우 레벨링을 실행한다.(칼라 전용)
        /// </summary>
        /// <param name="min">레벨링할 영역의 최소값</param>
        /// <param name="max">레벨링할 영역의 최대값</param>
        /// <param name="divStep">나눌 단계</param>
        /// <param name="clrMem">레벨링할 칼라 채널</param>
        /// <returns>결과 이미지</returns>
        public Mat WndLvColor(int min, int max, int divStep, KColorChannel clrMem)
        {
            if (Depth != KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_CantSupportGrayImg);
            if (min > max)
                throw new ArgumentException("main is bigger than max");
            if (max > byte.MaxValue)
                throw new ArgumentException("max is bigger than 255");
            if (divStep > byte.MaxValue + 1)
                throw new ArgumentException("can not be divided more than 256 steps");

            double div, differ;
            int pixel, upperLim = divStep - 1;

            differ = max - min;
            div = differ / divStep;

            Image<Bgr, byte> src = cvIImg as Image<Bgr, byte>;
            Image<Bgr, byte> dst = src.Mat.Clone().ToImage<Bgr, byte>();

            byte[] lut = new byte[256];
            int hL = src.Height;
            int wL = src.Width;
            int cNum = (int)clrMem;

            //Lut 계산
            for (int i = 0; i < lut.Length; i++)
            {
                pixel = (int)((i - min) / div);

                if (pixel < 0) pixel = 0;
                if (pixel > upperLim) pixel = upperLim;

                lut[i] = (byte)pixel;
            }

            int idx;
            for (int h = 0; h < hL; h++)
            {
                for (int w = 0; w < wL; w++)
                {
                    idx = data8[h, w, cNum];
                    dst.Data[h, w, cNum] = lut[idx];
                }
            }

            return dst.Mat;
        }

        /// <summary>
        /// 윈도우 레벨링을 실행한다. R,G,B 채널 전부 동일한 값으로 실행된다.(칼라 전용)
        /// </summary>
        /// <param name="min">레벨링할 영역의 최소값</param>
        /// <param name="max">레벨링할 영역의 최대값</param>
        /// <param name="divStep">나눌 단계</param>
        /// <returns>결과 이미지</returns>
        public Mat WndLvColor(int min, int max, int divStep = 256)
        {
            if (Depth != KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_CantSupportGrayImg);
            if (min > max)
                throw new ArgumentException("main is bigger than max");
            if (max > byte.MaxValue)
                throw new ArgumentException("max is bigger than 255");
            if (divStep > byte.MaxValue + 1)
                throw new ArgumentException("can not be divided more than 256 steps");

            double div, differ;
            int pixel, upperLim = divStep - 1;

            differ = max - min;
            div = differ / divStep;

            Image<Bgr, byte> src = cvIImg as Image<Bgr, byte>;
            Image<Bgr, byte> dst = src.Mat.Clone().ToImage<Bgr, byte>();

            byte[] lut = new byte[256];
            int hL = src.Height;
            int wL = src.Width;

            //Lut 계산
            for (int i = 0; i < lut.Length; i++)
            {
                pixel = (int)((i - min) / div);

                if (pixel < 0) pixel = 0;
                if (pixel > upperLim) pixel = upperLim;

                lut[i] = (byte)pixel;
            }

            for (int h = 0; h < hL; h++)
            {
                for (int w = 0; w < wL; w++)
                {
                    dst.Data[h, w, 0] = lut[data8[h, w, 0]];
                    dst.Data[h, w, 1] = lut[data8[h, w, 1]];
                    dst.Data[h, w, 2] = lut[data8[h, w, 2]];
                }
            }

            return dst.Mat;
        }


        // Color Channel
        /// <summary>
        /// 칼라 이미지의 각 색상채널을 분리한다 (BGR순서)
        /// 추출된 채널은 흑백 이미지로 변환된다
        /// 칼라 이미지 에서만 사용 가능하며, 흑백 이미지에서 예외를 발생시킨다
        /// </summary>
        /// <returns>분리된 색상 채널 (BGR순서)</returns>
        public Mat[] ColorChannelSeparate()
        {
            Mat[] rtMatArr = new Mat[3];

            if (Depth == KDepthType.Dt_24)
            {
                using (VectorOfMat vctMat = new VectorOfMat())
                {
                    CvInvoke.Split(cvMat, vctMat);

                    for (int i = 0; i < rtMatArr.Length; i++)
                    {
                        rtMatArr[i] = new Mat();
                        vctMat[i].CopyTo(rtMatArr[i]);
                    }
                }

                return rtMatArr;
            }
            else
                throw new Exception(LangResource.ER_JIMG_CantSupportGrayImg);
        }

        /// <summary>
        /// 칼라 이미지의 색상채널을 추출한다
        /// 추출된 채널은 흑백 이미지로 변환된다
        /// 칼라 이미지 에서만 사용 가능하며, 흑백 이미지에서 예외를 발생시킨다
        /// </summary>
        /// <param name="channelToExtract">추출할 색상채널</param>
        /// <returns>추출된 색상 채널</returns>
        public Mat ColorChannelSeparate(KColorChannel channelToExtract)
        {
            int idx = (int)channelToExtract;
            Mat rtMat;

            if (Depth == KDepthType.Dt_24)
            {
                using (VectorOfMat vctMat = new VectorOfMat())
                {
                    rtMat = new Mat();
                    CvInvoke.Split(cvMat, vctMat);

                    vctMat[idx].CopyTo(rtMat);
                }

                return rtMat;
            }
            else
                throw new Exception(LangResource.ER_JIMG_CantSupportGrayImg);
        }

        /// <summary>
        /// 흑백 이미지를 각 색상 채널에 할당하여 칼라 이미지를 만든다
        /// 각각의 인자는 8bit 흑백 이미지여야 하며, 같은 크기(가로, 세로)여야 한다.
        /// </summary>
        /// <param name="imageToBeBlue">Blue 채널로 사용할 흑백 이미지</param>
        /// <param name="imageToBeGreen">Green 채널로 사용할 흑백 이미지</param>
        /// <param name="imageToBeRed">Red 채널로 사용할 흑백 이미지</param>
        /// <returns>합쳐진 칼라 이미지</returns>
        public Mat ColorChannelCombine(Mat imageToBeBlue, Mat imageToBeGreen, Mat imageToBeRed)
        {
            Size szOfBlueCh = imageToBeBlue.Size;

            // 각 색상 채널들이 같은 크기가 아닐시 예외발생
            if (imageToBeGreen.Size != szOfBlueCh &&
                imageToBeRed.Size != szOfBlueCh)
            {
                throw new Exception(LangResource.ER_JIMG_NotSameSizes);
            }

            var rtMat = new Mat();
            var vctMat = new VectorOfMat(imageToBeBlue, imageToBeGreen, imageToBeRed);

            CvInvoke.Merge(vctMat, rtMat);

            return rtMat;
        }
    }
}
