using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using KiyLib.DIP;
using KiyLib.ExtensionMethod;
using KiyLib.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyEmguCV.DIP
{
    /// <summary>
    /// ROI에 관련된 처리를 하는 클래스
    /// 현재 ROI의 영역이 클수록 처리속도가 느려지는 단점이 있다, 개선필요
    /// </summary>
    public class KRoiCV : IKHistogram, IDisposable
    {
        private readonly double bsrMultipliVal = 0.0886;
        private dynamic[,,] data;
        private Mat cvMat, srcMat;
        private Rectangle region;
        private KDepthType depth;
        private DepthType depthCV;
        private IImage imgCV;
        private int numberOfChannels;
        private int[] histoGray, histoB, histoG, histoR;
        private int min, max;
        private double mean;

        /// <summary>
        /// 픽셀의 최소값
        /// </summary>
        public int Min
        {
            get { return min; }
            private set { min = value; }
        }

        /// <summary>
        /// 픽셀의 최대값
        /// </summary>
        public int Max
        {
            get { return max; }
            private set { max = value; }
        }

        /// <summary>
        /// 픽셀들의 평균값
        /// </summary>
        public double Mean
        {
            get { return mean; }
            private set { mean = value; }
        }

        /// <summary>
        /// 원본 이미지에서의 ROI 좌표
        /// </summary>
        public Rectangle Region
        {
            get { return region; }
            set
            {
                region = value;
                this.cvMat = new Mat(srcMat, value);
                this.Initialize();
            }
        }

        /// <summary>
        /// ROI의 Area(전체 픽셀개수)
        /// </summary>
        public int Area
        {
            get { return region.Width * region.Height; }
        }


        /// <summary>
        /// 생성자
        /// 원본 이미지에서 특정 ROI를 지정하여 ROI에 대한 각종 기능들을 실행할수 있다
        /// </summary>
        /// <param name="srcMat">ROI를 지정할 원본 이미지 Mat객체</param>
        /// <param name="region">지정할 ROI</param>
        public KRoiCV(Mat srcMat, Rectangle region)
        {
            this.srcMat = srcMat;
            this.cvMat = new Mat(srcMat, region);
            this.Region = region;
            this.depthCV = cvMat.Depth;
            this.numberOfChannels = cvMat.NumberOfChannels;

            this.Initialize();
        }


        // IKHistogram
        /// <summary>
        /// 흑빅 이미지일때 히스토그램 배열을 가져온다.
        /// </summary>
        /// <param name="dstHistoArr">결과를 저장할 배열</param>
        public void GetHistoGray(out int[] dstHistoArr)
        {
            KImageCV.GetHistogramGray(data, depth, out dstHistoArr);
        }

        /// <summary>
        /// 칼라 이미지일때 히스토그램 배열을 가져온다.(BGR 순서)
        /// </summary>
        /// <param name="dstHistoArrB">결과를 저장할 배열(Blue)</param>
        /// <param name="dstHistoArrG">결과를 저장할 배열(Green)</param>
        /// <param name="dstHistoArrR">결과를 저장할 배열(Red)</param>
        public void GetHistoColor(out int[] dstHistoArrB, out int[] dstHistoArrG, out int[] dstHistoArrR)
        {
            KImageCV.GetHistogramColor(data, depth, out dstHistoArrB, out dstHistoArrG, out dstHistoArrR);
        }

        /// <summary>
        /// 히스토그램 배열에서 최저값의 개수와 index를 구한다. (흑백 전용)
        /// </summary>
        /// <param name="index">최저값의 index</param>
        /// <param name="count">최저값의 개수</param>
        public void GetHistoMinGray(out int index, out int count)
        {
            if (depth == KDepthType.Dt_24)
                throw new FormatException("해당 함수는 칼라타입을 지원하지 않습니다");

            index = histoGray.MinIndexOf();
            count = histoGray[index];
        }

        /// <summary>
        /// 히스토그램 배열에서 최대값의 개수와 index를 구한다. (흑백 전용)
        /// </summary>
        /// <param name="index">최대값의 index</param>
        /// <param name="count">최대값의 개수</param>
        public void GetHistoMaxGray(out int index, out int count)
        {
            if (depth == KDepthType.Dt_24)
                throw new FormatException("해당 함수는 칼라타입을 지원하지 않습니다");

            index = histoGray.MaxIndexOf();
            count = histoGray[index];
        }

        /// <summary>
        /// 히스토그램의 평균을 구한다.
        /// </summary>
        /// <returns>결과값</returns>
        public double GetHistoAvgGray()
        {
            if (depth == KDepthType.Dt_24)
                throw new FormatException("해당 함수는 칼라타입을 지원하지 않습니다");

            double rtAvg = 0, sum = 0;

            for (int i = 0; i < histoGray.Length; i++)
                sum += histoGray[i] * i;

            rtAvg = sum / Area;

            return rtAvg;
        }

        /// <summary>
        /// 히스토그램 배열에서 최저값의 개수와 index를 구한다. (칼라 전용)
        /// </summary>
        /// <param name="indexB">최저값의 index (Blue)</param>
        /// <param name="countB">최저값의 개수 (Blue)</param>
        /// <param name="indexG">최저값의 index (Green)</param>
        /// <param name="countG">최저값의 개수 (Green)</param>
        /// <param name="indexR">최저값의 index (Red)</param>
        /// <param name="countR">최저값의 개수 (Red)</param>
        public void GetHistoMinColor(out int indexB, out int countB,
                                     out int indexG, out int countG,
                                     out int indexR, out int countR)
        {
            if (depth == KDepthType.Dt_8 || depth == KDepthType.Dt_16)
                throw new FormatException("해당 함수는 흑백 이미지를 지원하지 않습니다");

            indexB = histoB.MinIndexOf();
            countB = histoB[indexB];

            indexG = histoG.MinIndexOf();
            countG = histoG[indexG];

            indexR = histoR.MinIndexOf();
            countR = histoR[indexR];
        }
        /// <summary>
        /// 히스토그램 배열에서 최대값의 개수와 index를 구한다. (칼라 전용)
        /// </summary>
        /// <param name="indexB">최대값의 index (Blue)</param>
        /// <param name="countB">최대값의 개수 (Blue)</param>
        /// <param name="indexG">최대값의 index (Green)</param>
        /// <param name="countG">최대값의 개수 (Green)</param>
        /// <param name="indexR">최대값의 index (Red)</param>
        /// <param name="countR">최대값의 개수 (Red)</param>
        public void GetHistoMaxColor(out int indexB, out int countB,
                                     out int indexG, out int countG,
                                     out int indexR, out int countR)
        {
            if (depth == KDepthType.Dt_8 || depth == KDepthType.Dt_16)
                throw new FormatException("해당 함수는 흑백 이미지를 지원하지 않습니다");

            indexB = histoB.MaxIndexOf();
            countB = histoB[indexB];

            indexG = histoG.MaxIndexOf();
            countG = histoG[indexG];

            indexR = histoR.MaxIndexOf();
            countR = histoR[indexR];
        }
        /// <summary>
        /// 히스토그램의 평균을 구한다.
        /// </summary>
        /// <param name="avgB">결과값 (Blue)</param>
        /// <param name="avgG">결과값 (Green)</param>
        /// <param name="avgR">결과값 (Red)</param>
        public void GetHistoAvgColor(out double bAvg, out double gAvg, out double rAvg)
        {
            if (depth == KDepthType.Dt_8 || depth == KDepthType.Dt_16)
                throw new FormatException("해당 함수는 흑백 이미지를 지원하지 않습니다");

            double sum = 0;

            for (int i = 0; i < histoB.Length; i++)
                sum += histoB[i] * i;

            bAvg = sum / Area;
            sum = 0;

            for (int i = 0; i < histoG.Length; i++)
                sum += histoG[i] * i;

            gAvg = sum / Area;
            sum = 0;

            for (int i = 0; i < histoR.Length; i++)
                sum += histoR[i] * i;

            rAvg = sum / Area;
        }

        /// <summary>
        /// 히스토그램에서 FirstData와 LastData의 Index를 구한다(ISee! 뷰어에서 드래그 윈도우 레벨링에 사용)
        /// </summary>
        /// <param name="indexOfFirstData">검색된 FirstData의 Index</param>
        /// <param name="indexOfLastData">검색된 LastData의 Index</param>
        public void GetHistoFirstNonZeroAtBothSideGray(out int indexOfFirstData, out int indexOfLastData)
        {
            if (depth == KDepthType.Dt_24)
                throw new FormatException("해당 함수는 칼라타입을 지원하지 않습니다");

            int len8 = 256, len16 = 65536;
            float[] histArr8 = new float[len8];
            float[] histArr16 = new float[len16];

            switch (depth)
            {
                case KDepthType.Dt_8:
                    using (var hist = new DenseHistogram(len8, new RangeF(0, len8)))
                    {
                        var imgHist = cvMat.ToImage<Gray, byte>();
                        hist.Calculate(new Image<Gray, byte>[] { imgHist }, true, null);
                        hist.CopyTo(histArr8);

                        KiyLib.General.KCommon.IndexOfFirstNonZeroAtBothSide(histArr8, out indexOfFirstData, out indexOfLastData);
                        return;
                    }

                case KDepthType.Dt_16:
                    using (var hist = new DenseHistogram(len16, new RangeF(0, len16)))
                    {
                        var imgHist = cvMat.ToImage<Gray, ushort>();
                        hist.Calculate(new Image<Gray, ushort>[] { imgHist }, true, null);
                        hist.CopyTo(histArr16);

                        KiyLib.General.KCommon.IndexOfFirstNonZeroAtBothSide(histArr16, out indexOfFirstData, out indexOfLastData);
                        return;
                    }
            }

            throw new Exception("함수가 정상적으로 실행되지 않았습니다.");
        }

        // IDisposable
        /// <summary>
        /// 사용된 리소스를 해제한다
        /// </summary>
        public void Dispose()
        {
            cvMat?.Dispose();
            srcMat?.Dispose();
            imgCV?.Dispose();

            if (data != null)
                Array.Clear(data, 0, data.Length);
        }


        /// <summary>
        /// stdev(표준편차)를 구한다. (흑백 전용)
        /// </summary>
        /// <returns>stdev(표준편차)</returns>
        public double GetStandardDeviationGray()
        {
            double sum = 0;

            for (int h = 0; h < region.Height; h++)
                for (int w = 0; w < region.Width; w++)
                {
                    sum += Math.Pow(data[h, w, 0] - mean, 2);
                }

            if (sum != 0 && mean != 0)
                return Math.Sqrt(sum / ((region.Height * region.Width) - 1));
            else
                return 0;
        }

        /// <summary>
        /// SNR(Signal to noise ratio)에 관련된 data들을 구한다
        /// </summary>
        /// <param name="medianMean">ROI에서 각 행의 평균(mean)들을 구한뒤 그의 중간값 (ISee 뷰어의 median single line mean)</param>
        /// <param name="medianStdev">ROI에서 각 행의 stdev(표준편차)들을 구한뒤 그의 중간값 (ISee 뷰어의 median single line stdev)</param>
        /// <param name="unNormSNR">unnormalized SNR값 (ISee 뷰어의 unnormalized SNR) </param>
        /// <param name="normSNR">Normalized SNR값, BSR수치가 0이면 이 값도 0이 된다 (ISee 뷰어의 Normalized SNR)</param>
        /// <param name="basicSpatialResolution">BSR(basicSpatialResolution - 분해능)값, 0으로 설정시 normSNR값도 0이 된다</param>
        public void GetSNRParamsGray(out double medianMean, out double medianStdev, out double unNormSNR, out double normSNR, double basicSpatialResolution = 0)
        {
            var bsr = basicSpatialResolution;

            int width = region.Width;
            int height = region.Height;
            double sum = 0, avg = 0;
            double[] sgLineMean = new double[height];
            double[] sgLineStdev = new double[height];
            double[] colPixels = new double[width];

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    sum += data[h, w, 0];
                    colPixels[w] = data[h, w, 0];
                }

                sgLineMean[h] = sum / width;
                sgLineStdev[h] = KMath.CalcStandardDeviation(colPixels);
                avg = sum = 0;
            }

            medianMean = KMath.CalcMedian(sgLineMean);
            medianStdev = KMath.CalcMedian(sgLineStdev);

            if (medianMean != 0 && medianStdev != 0)
            {
                unNormSNR = medianMean / medianStdev;

                if (bsr != 0)
                {
                    normSNR = unNormSNR * (bsrMultipliVal / bsr);
                }
                else
                    normSNR = 0;
            }
            else
            {
                unNormSNR = 0;
                normSNR = 0;
            }
        }

        /// <summary>
        /// 초기화 함수
        /// 생성자또는 ROI변수 재설정시 실행된다
        /// </summary>
        private void Initialize()
        {
            //min, max, mean 구하기
            var dummyPt = new Point();
            double tmp_min = 0, tmp_max = 0;
            CvInvoke.MinMaxLoc(cvMat, ref tmp_min, ref tmp_max, ref dummyPt, ref dummyPt);
            min = (int)tmp_min;
            max = (int)tmp_max;
            mean = CvInvoke.Mean(cvMat).V0;

            if (depthCV == Emgu.CV.CvEnum.DepthType.Cv8U)
                depth = (KDepthType)((int)KDepthType.Dt_8 * (int)numberOfChannels);
            else if (depthCV == Emgu.CV.CvEnum.DepthType.Cv16U)
                depth = KDepthType.Dt_16;

            switch (depth)
            {
                case KDepthType.Dt_8:
                    imgCV = cvMat.ToImage<Gray, byte>();
                    DataCopyFromCVImgs<Gray, byte>(imgCV);
                    GetHistoGray(out histoGray);
                    break;

                case KDepthType.Dt_16:
                    imgCV = cvMat.ToImage<Gray, ushort>();
                    DataCopyFromCVImgs<Gray, ushort>(imgCV);
                    GetHistoGray(out histoGray);
                    break;

                case KDepthType.Dt_24:
                    imgCV = cvMat.ToImage<Bgr, byte>();
                    DataCopyFromCVImgs<Bgr, byte>(imgCV);
                    GetHistoColor(out histoB, out histoG, out histoR);
                    break;
            }
        }

        /// <summary>
        /// 픽셀 데이터를 복사한다
        /// </summary>
        /// <typeparam name="TColor">복사할 원본 이미지의 칼라</typeparam>
        /// <typeparam name="TDepth">복사할 원본 이미지의 Depth</typeparam>
        /// <param name="imgCV">복사할 원본 이미지</param>
        private void DataCopyFromCVImgs<TColor, TDepth>(IImage imgCV)
            where TColor : struct, IColor
            where TDepth : IConvertible, new()
        {
            var img = imgCV as Image<TColor, TDepth>;
            data = new dynamic[img.Height, img.Width, img.NumberOfChannels];

            for (int h = 0; h < img.Height; h++)
                for (int w = 0; w < img.Width; w++)
                    for (int c = 0; c < img.NumberOfChannels; c++)
                        data[h, w, c] = img.Data[h, w, c];
        }
    }
}
