using KiyLib.DIP;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyEmguCV.DIP
{
    /// <summary>
    /// Line Profile를 실행하기 위한 클래스 (칼라 이미지 전용)
    /// </summary>
    public class KLineProfileColorCV : KLineProfileBaseCV
    {
        // 픽셀 데이터
        private byte[,,] data8;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="width">Line이 속한 이미지의 가로길이</param>
        /// <param name="height">Line이 속한 이미지의 세로길이</param>
        /// <param name="data">Line이 속한 이미지의 픽셀 데이터</param>
        /// <param name="depth">Line이 속한 이미지의 Depth(bit)</param>
        /// <param name="start">Line의 시작점</param>
        /// <param name="end">Line의 끝점</param>
        public KLineProfileColorCV(int width, int height, byte[,,] data,
             KDepthType depth, Point start, Point end)
        {
            this.Width = width;
            this.Height = height;
            this.data8 = data;
            this.Depth = depth;
            this.start = start;
            this.end = end;

            Initialize();
        }


        /// <summary>
        /// Line에서 값이 가장 작은 픽셀의 정보를 가져온다.(칼라 전용 함수)
        /// </summary>
        /// <param name="clrMem">가져올 색상 채널</param>
        /// <returns>Index(Line에서 몇번째 픽셀인가), Point(픽셀의 좌표), PixelValue(밝기값) 순서</returns>
        public Tuple<int, Point, int> GetMinValue(KColorChannel clrMem)
        {
            if (depth != KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_IsNotColorImg);

            var data = this.GetPixelDatas();
            Point rtPt = new Point();
            Tuple<Point, KColor> min = null;
            int clrVal = 0, index = 0;

            switch (clrMem)
            {
                case KColorChannel.B:
                    min = data.OrderBy(x => x.Item2.B).ToList().FirstOrDefault();
                    clrVal = min.Item2.B;
                    break;

                case KColorChannel.G:
                    min = data.OrderBy(x => x.Item2.G).ToList().FirstOrDefault();
                    clrVal = min.Item2.G;
                    break;

                case KColorChannel.R:
                    min = data.OrderBy(x => x.Item2.R).ToList().FirstOrDefault();
                    clrVal = min.Item2.R;
                    break;
            }

            rtPt = min.Item1;
            index = data.IndexOf(min);

            return new Tuple<int, Point, int>(index, rtPt, clrVal);
        }

        /// <summary>
        /// Line에서 값이 가장 큰 픽셀의 정보를 가져온다.(칼라 전용 함수)
        /// </summary>
        /// <param name="clrMem">가져올 색상 채널</param>
        /// <returns>Index(Line에서 몇번째 픽셀인가), Point(픽셀의 좌표), PixelValue(밝기값) 순서</returns>
        public Tuple<int, Point, int> GetMaxValue(KColorChannel clrMem)
        {
            if (depth != KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_IsNotColorImg);

            var data = this.GetPixelDatas();
            Point rtPt = new Point();
            Tuple<Point, KColor> max = null;
            int clrVal = 0, index = 0;

            switch (clrMem)
            {
                case KColorChannel.B:
                    max = data.OrderByDescending(x => x.Item2.B).ToList().FirstOrDefault();
                    clrVal = max.Item2.B;
                    break;

                case KColorChannel.G:
                    max = data.OrderByDescending(x => x.Item2.G).ToList().FirstOrDefault();
                    clrVal = max.Item2.G;
                    break;

                case KColorChannel.R:
                    max = data.OrderByDescending(x => x.Item2.R).ToList().FirstOrDefault();
                    clrVal = max.Item2.R;
                    break;
            }

            rtPt = max.Item1;
            index = data.IndexOf(max);

            return new Tuple<int, Point, int>(index, rtPt, clrVal);
        }

        /// <summary>
        /// Line의 픽셀 데이터를 가져온다.(칼라 전용 함수)
        /// </summary>
        /// <returns>Point(픽셀의 좌표), 칼라구조체(밝기값) 순서</returns>
        public List<Tuple<Point, KColor>> GetPixelDatas()
        {
            if (Depth != KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_IsNotColorImg);

            var pts = pointsInLine;
            List<Tuple<Point, KColor>> rtList = new List<Tuple<Point, KColor>>();

            foreach (var pt in pts)
            {
                KColor clr = new KColor(data8[pt.Y, pt.X, 0], data8[pt.Y, pt.X, 1], data8[pt.Y, pt.X, 2]);
                rtList.Add(new Tuple<Point, KColor>(new Point(pt.X, pt.Y), clr));
            }

            return rtList;
        }

        /// <summary>
        /// 프로젝션 방식으로 Line의 픽셀 데이터를 가져온다.(칼라이미지 전용 함수)
        /// </summary>
        /// <param name="thickness">프로젝션 방식에 사용할 thickness값 (1이면 GetPixelDatas()함수와 동일)</param>
        /// <returns>Point(픽셀의 좌표), PixelValue(밝기평균값) 순서</returns>
        public List<Tuple<Point, KColorF>> GetPixelDatasByProjection(int thickness = 1)
        {
            if (depth != KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_IsNotColorImg);

            float sumB = 0, sumG = 0, sumR = 0;
            List<Tuple<Point, KColorF>> rtList = new List<Tuple<Point, KColorF>>();

            if (thickness == 1)
            {
                var dts = GetPixelDatas();

                foreach (var item in dts)
                {
                    var pt = item.Item1;
                    var clr = item.Item2;

                    rtList.Add(new Tuple<Point, KColorF>(pt, new KColorF(clr.B, clr.G, clr.R)));
                }

                return rtList;
            }

            foreach (var pt in pointsInLine)
            {
                var rst = GetPointsUseTheta(pt, thickness);

                int cnt = rst.Count, divStep = 0;
                for (int i = 0; i < cnt; i++)
                {
                    if ((rst[i].Y >= data8.GetLength(0) || rst[i].Y < 0) ||
                        (rst[i].X >= data8.GetLength(1) || rst[i].X < 0))
                        continue;

                    Point rPt = rst[i];
                    sumB += data8[rPt.Y, rPt.X, 0];
                    sumG += data8[rPt.Y, rPt.X, 1];
                    sumR += data8[rPt.Y, rPt.X, 2];
                    divStep++;
                }

                rtList.Add(new Tuple<Point, KColorF>(
                    new Point(pt.X, pt.Y),
                    new KColorF(
                        (float)Math.Round(sumB / divStep, 2),
                        (float)Math.Round(sumG / divStep, 2),
                        (float)Math.Round(sumR / divStep, 2)
                        )));

                sumB = sumG = sumR = divStep = 0;
            }

            return rtList;
        }

        /// <summary>
        /// 1차 미분(변화량) 데이터를 가져온다.
        /// </summary>
        /// <param name="thickness">thickness값</param>
        /// <returns>Point(픽셀의 좌표), 1차미분값</returns>
        public List<Tuple<Point, KColorF>> Calc1stDerivative(int thickness = 1)
        {
            if (depth != KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_IsNotColorImg);

            List<Tuple<Point, KColorF>> rtList = new List<Tuple<Point, KColorF>>();

            if (thickness == 1)
            {
                var dts = GetPixelDatas();

                for (int i = 0; i < dts.Count - 1; i++)
                {
                    var pt = dts[i].Item1;
                    var prevClr = dts[i].Item2;
                    var nextClr = dts[i + 1].Item2;

                    var b = nextClr.B - prevClr.B;
                    var g = nextClr.G - prevClr.G;
                    var r = nextClr.R - prevClr.R;

                    rtList.Add(new Tuple<Point, KColorF>(pt, new KColorF(b, g, r)));
                }

                return rtList;
            }

            var rst = GetPixelDatasByProjection(thickness);

            for (int i = 0; i < rst.Count - 1; i++)
            {
                var pt = rst[i].Item1;
                var prevClr = rst[i].Item2;
                var nextClr = rst[i + 1].Item2;

                rtList.Add(new Tuple<Point, KColorF>(pt, nextClr - prevClr));
            }

            return rtList;
        }

        /// <summary>
        /// 2차 미분(변화량) 데이터를 가져온다.
        /// </summary>
        /// <param name="thickness">thickness값</param>
        /// <returns>Point(픽셀의 좌표), 2차미분값</returns>
        public List<Tuple<Point, KColorF>> Calc2stDerivative(int thickness = 1)
        {
            if (depth != KDepthType.Dt_24)
                throw new FormatException(LangResource.ER_JIMG_IsNotColorImg);

            List<Tuple<Point, KColorF>> rtList = new List<Tuple<Point, KColorF>>();
            int rtListCnt = 0;
            Tuple<Point, KColorF>[] tempList;

            if (thickness == 1)
            {
                var dts = GetPixelDatas();

                //1차 미분
                for (int i = 0; i < dts.Count - 1; i++)
                {
                    var pt = dts[i].Item1;
                    var prevClr = dts[i].Item2;
                    var nextClr = dts[i + 1].Item2;

                    var b = nextClr.B - prevClr.B;
                    var g = nextClr.G - prevClr.G;
                    var r = nextClr.R - prevClr.R;

                    rtList.Add(new Tuple<Point, KColorF>(pt, new KColorF(b, g, r)));
                }

                //2차 미분
                rtListCnt = rtList.Count;
                tempList = new Tuple<Point, KColorF>[rtListCnt];
                rtList.CopyTo(tempList);
                rtList.Clear();

                for (int i = 0; i < tempList.Length - 1; i++)
                {
                    var pt = tempList[i].Item1;
                    var prevClr = tempList[i].Item2;
                    var nextClr = tempList[i + 1].Item2;

                    rtList.Add(new Tuple<Point, KColorF>(pt, nextClr - prevClr));
                }

                return rtList;
            }

            var rst = GetPixelDatasByProjection(thickness);

            //1차 미분
            for (int i = 0; i < rst.Count - 1; i++)
            {
                var pt = rst[i].Item1;
                var prevClr = rst[i].Item2;
                var nextClr = rst[i + 1].Item2;

                rtList.Add(new Tuple<Point, KColorF>(pt, nextClr - prevClr));
            }

            //2차 미분
            rtListCnt = rtList.Count;
            tempList = new Tuple<Point, KColorF>[rtListCnt];
            rtList.CopyTo(tempList);
            rtList.Clear();

            for (int i = 0; i < tempList.Length - 1; i++)
            {
                var pt = tempList[i].Item1;
                var prevClr = tempList[i].Item2;
                var nextClr = tempList[i + 1].Item2;

                rtList.Add(new Tuple<Point, KColorF>(pt, nextClr - prevClr));
            }

            return rtList;
        }

        /// <summary>
        /// 데이터를 다루기 쉽도록 분류한다.
        /// </summary>
        /// <param name="src">분류할 원본 List</param>
        /// <param name="pts">분류된 Point배열</param>
        /// <param name="B">분류된 data의 Blue값 배열</param>
        /// <param name="G">분류된 data의 Green값 배열</param>
        /// <param name="R">분류된 data의 Red값 배열</param>
        public void SeparateData(List<Tuple<Point, KColor>> src,
            out Point[] pts,
            out byte[] B, out byte[] G, out byte[] R)
        {
            int cnt = src.Count;
            pts = new Point[cnt];
            B = new byte[cnt];
            G = new byte[cnt];
            R = new byte[cnt];

            for (int i = 0; i < cnt; i++)
            {
                pts[i] = src[i].Item1;
                B[i] = src[i].Item2.B;
                G[i] = src[i].Item2.G;
                R[i] = src[i].Item2.R;
            }
        }

        /// <summary>
        /// 데이터를 다루기 쉽도록 분류한다.
        /// </summary>
        /// <param name="src">분류할 원본 List</param>
        /// <param name="pts">분류된 Point배열</param>
        /// <param name="B">분류된 data의 Blue값 배열</param>
        /// <param name="G">분류된 data의 Green값 배열</param>
        /// <param name="R">분류된 data의 Red값 배열</param>
        public void SeparateData(List<Tuple<Point, KColorF>> src,
            out Point[] pts,
            out float[] B, out float[] G, out float[] R)
        {
            int cnt = src.Count;
            pts = new Point[cnt];
            B = new float[cnt];
            G = new float[cnt];
            R = new float[cnt];

            for (int i = 0; i < cnt; i++)
            {
                pts[i] = src[i].Item1;
                B[i] = src[i].Item2.B;
                G[i] = src[i].Item2.G;
                R[i] = src[i].Item2.R;
            }
        }
    }
}
