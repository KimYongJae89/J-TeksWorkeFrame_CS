using Emgu.CV;
using Emgu.CV.Structure;
using KiyLib.DIP;
using KiyLib.ExtensionMethod;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyEmguCV.DIP
{
    public partial class KImageBaseCV : IKHistogram, IKLineProfile
    {
        private int[] histoGray, histoB, histoG, histoR;


        // IKHistogram
        /// <summary>
        /// 히스토그램 배열을 구한다. (흑백 전용)
        /// </summary>
        /// <param name="dstHistoArr">히스토그램 데이터가 복사될 배열</param>
        public void GetHistoGray(out int[] dstHistoArr)
        {
            if (Depth == KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_IsNotGrayImg);
            if (Depth == KDepthType.None)
                throw new FormatException(LangResource.ER_JIMG_DepthFmt);
            if (Depth == KDepthType.Dt_8)
                KImageCV.GetHistogramGray(data8, this.Depth, out dstHistoArr);
            else
                KImageCV.GetHistogramGray(data16, this.Depth, out dstHistoArr);
        }

        /// <summary>
        /// 히스토그램 배열을 구한다. (칼라 전용)
        /// </summary>
        /// <param name="dstHistoArrB">히스토그램 데이터 배열 (Blue)</param>
        /// <param name="destHistoArrG">히스토그램 데이터 배열 (Green)</param>
        /// <param name="destHistoArrR">히스토그램 데이터 배열 (Red)</param>
        public void GetHistoColor(out int[] dstHistoArrB, out int[] destHistoArrG, out int[] destHistoArrR)
        {
            if (Depth == KDepthType.Dt_8 || Depth == KDepthType.Dt_16)
                throw new FormatException(LangResource.ER_JIMG_IsNotColorImg);
            if (Depth == KDepthType.None)
                throw new FormatException(LangResource.ER_JIMG_DepthFmt);

            KImageCV.GetHistogramColor(data8, this.Depth, out dstHistoArrB, out destHistoArrG, out destHistoArrR);
        }

        /// <summary>
        /// 히스토그램 배열에서 최저값의 개수와 index를 구한다. (흑백 전용)
        /// </summary>
        /// <param name="index">최저값의 index</param>
        /// <param name="count">최저값의 개수</param>
        public void GetHistoMinGray(out int index, out int count)
        {
            if (Depth == KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_CantSupportColorImg);

            if (histoGray == null)
                GetHistoGray(out histoGray);

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
            if (Depth == KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_CantSupportColorImg);

            if (histoGray == null)
                GetHistoGray(out histoGray);

            index = histoGray.MaxIndexOf();
            count = histoGray[index];
        }

        /// <summary>
        /// 히스토그램의 평균을 구한다.
        /// </summary>
        /// <returns>결과값</returns>
        public double GetHistoAvgGray()
        {
            if (Depth == KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_CantSupportColorImg);

            if (histoGray == null)
                GetHistoGray(out histoGray);

            double rtAvg = 0, sum = 0;

            for (int i = 0; i < histoGray.Length; i++)
                sum += histoGray[i] * i;

            rtAvg = sum / (Width * Height);

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
            if (Depth == KDepthType.Dt_8 || Depth == KDepthType.Dt_16)
                throw new FormatException(LangResource.ER_JIMG_CantSupportGrayImg);

            if (histoB == null || histoG == null || histoR == null)
                GetHistoColor(out histoB, out histoG, out histoR);

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
            if (Depth == KDepthType.Dt_8 || Depth == KDepthType.Dt_16)
                throw new FormatException(LangResource.ER_JIMG_CantSupportGrayImg);

            if (histoB == null || histoG == null || histoR == null)
                GetHistoColor(out histoB, out histoG, out histoR);

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
        public void GetHistoAvgColor(out double avgB, out double avgG, out double avgR)
        {
            if (Depth == KDepthType.Dt_8 || Depth == KDepthType.Dt_16)
                throw new FormatException(LangResource.ER_JIMG_CantSupportGrayImg);

            if (histoB == null || histoG == null || histoR == null)
                GetHistoColor(out histoB, out histoG, out histoR);

            double sum = 0;
            int area = Width * Height;

            for (int i = 0; i < histoB.Length; i++)
                sum += histoB[i] * i;

            avgB = sum / area;
            sum = 0;

            for (int i = 0; i < histoG.Length; i++)
                sum += histoG[i] * i;

            avgG = sum / area;
            sum = 0;

            for (int i = 0; i < histoR.Length; i++)
                sum += histoR[i] * i;

            avgR = sum / area;
        }

        /// <summary>
        /// 히스토그램에서 FirstData와 LastData의 Index를 구한다(ISee! 뷰어에서 드래그 윈도우 레벨링에 사용)
        /// </summary>
        /// <param name="indexOfFirstData">검색된 FirstData의 Index</param>
        /// <param name="indexOfLastData">검색된 LastData의 Index</param>
        public void GetHistoFirstNonZeroAtBothSideGray(out int indexOfFirstData, out int indexOfLastData)
        {
            if (Depth == KDepthType.Dt_24)
                throw new FormatException("해당 함수는 칼라타입을 지원하지 않습니다");

            int len8 = 256, len16 = 65536;
            float[] histArr8 = new float[len8];
            float[] histArr16 = new float[len16];

            switch (Depth)
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

        /// <summary>
        /// 히스토그램에서 FirstData와 LastData의 Index를 구한다(ISee! 뷰어에서 드래그 윈도우 레벨링에 사용)
        /// </summary>
        /// <param name="region">대상 ROI 지정</param>
        /// <param name="indexOfFirstData">검색된 FirstData의 Index</param>
        /// <param name="indexOfLastData">검색된 LastData의 Index</param>
        public void GetHistoFirstNonZeroAtBothSideGray(Rectangle region, out int indexOfFirstData, out int indexOfLastData)
        {
            if (Depth == KDepthType.Dt_24)
                throw new FormatException("해당 함수는 칼라타입을 지원하지 않습니다");

            Mat matRegion = new Mat(cvMat, region);

            int len8 = 256, len16 = 65536;
            float[] histArr8 = new float[len8];
            float[] histArr16 = new float[len16];

            switch (Depth)
            {
                case KDepthType.Dt_8:
                    using (var hist = new DenseHistogram(len8, new RangeF(0, len8)))
                    {
                        var imgHist = matRegion.ToImage<Gray, byte>();
                        hist.Calculate(new Image<Gray, byte>[] { imgHist }, true, null);
                        hist.CopyTo(histArr8);

                        KiyLib.General.KCommon.IndexOfFirstNonZeroAtBothSide(histArr8, out indexOfFirstData, out indexOfLastData);
                        return;
                    }

                case KDepthType.Dt_16:
                    using (var hist = new DenseHistogram(len16, new RangeF(0, len16)))
                    {
                        var imgHist = matRegion.ToImage<Gray, ushort>();
                        hist.Calculate(new Image<Gray, ushort>[] { imgHist }, true, null);
                        hist.CopyTo(histArr16);

                        KiyLib.General.KCommon.IndexOfFirstNonZeroAtBothSide(histArr16, out indexOfFirstData, out indexOfLastData);
                        return;
                    }
            }

            throw new Exception("함수가 정상적으로 실행되지 않았습니다.");
        }


        // IKLineProfile
        /// <summary>
        /// 라인 프로파일을 실행할 객체를 가져옵니다.
        /// </summary>
        /// <param name="start">프로파일 시작점</param>
        /// <param name="end">프로파일 끝점</param>
        /// <returns>라인 프로파일 객체</returns>
        public KLineProfileBaseCV GetLineProfileInfo(Point start, Point end)
        {
            Rectangle imgArea = new Rectangle(0, 0, Width, Height);

            if (imgArea.Contains(start) == false)
                throw new ArgumentOutOfRangeException("Start" + LangResource.ER_JIMG_PointIsOutOfArea);
            if (imgArea.Contains(end) == false)
                throw new ArgumentOutOfRangeException("End" + LangResource.ER_JIMG_PointIsOutOfArea);

            switch (Depth)
            {
                case KDepthType.Dt_8:
                    return new KLineProfileGrayCV(Width, Height, data8, Depth, start, end);
                case KDepthType.Dt_16:
                    return new KLineProfileGrayCV(Width, Height, data16, Depth, start, end);
                case KDepthType.Dt_24:
                    return new KLineProfileColorCV(Width, Height, data8, Depth, start, end);
                case KDepthType.None:
                default:
                    break;
            }

            return null;
        }

        /// <summary>
        /// 라인 프로파일을 실행할 객체를 가져옵니다.
        /// </summary>
        /// <param name="startX">>프로파일 시작점 X좌표</param>
        /// <param name="startY">>프로파일 시작점 Y좌표</param>
        /// <param name="endX">프로파일 끝점 X좌표</param>
        /// <param name="endY">프로파일 끝점 Y좌표</param>
        /// <returns>라인 프로파일 객체</returns>
        public KLineProfileBaseCV GetLineProfileInfo(int startX, int startY, int endX, int endY)
        {
            Rectangle imgArea = new Rectangle(0, 0, Width, Height);

            if (imgArea.Contains(new Point(startX, startY)) == false)
                throw new ArgumentOutOfRangeException("Start" + LangResource.ER_JIMG_PointIsOutOfArea);
            if (imgArea.Contains(new Point(endX, endY)) == false)
                throw new ArgumentOutOfRangeException("End" + LangResource.ER_JIMG_PointIsOutOfArea);

            return GetLineProfileInfo(new Point(startX, startY), new Point(endX, endY));
        }
    }
}
