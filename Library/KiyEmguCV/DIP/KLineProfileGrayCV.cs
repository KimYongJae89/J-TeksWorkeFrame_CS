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
    /// Line Profile를 실행하기 위한 클래스 (흑백 이미지 전용)
    /// </summary>
    public class KLineProfileGrayCV : KLineProfileBaseCV
    {
        // 픽셀 데이터
        private byte[,,] data8;
        private ushort[,,] data16;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="width">Line이 속한 이미지의 가로길이</param>
        /// <param name="height">Line이 속한 이미지의 세로길이</param>
        /// <param name="data">Line이 속한 이미지의 픽셀 데이터</param>
        /// <param name="depth">Line이 속한 이미지의 Depth(bit)</param>
        /// <param name="start">Line의 시작점</param>
        /// <param name="end">Line의 끝점</param>
        public KLineProfileGrayCV(int width, int height, byte[,,] data,
          KDepthType depth, Point start, Point end)
        {
            if (depth != KDepthType.Dt_8)
                throw new FormatException("8bit " + LangResource.ER_JIMG_IsNotGrayImg);

            this.Width = width;
            this.Height = height;
            this.data8 = data;
            this.Depth = depth;
            this.start = start;
            this.end = end;

            Initialize();
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="width">Line이 속한 이미지의 가로길이</param>
        /// <param name="height">Line이 속한 이미지의 세로길이</param>
        /// <param name="data">Line이 속한 이미지의 픽셀 데이터</param>
        /// <param name="depth">Line이 속한 이미지의 Depth(bit)</param>
        /// <param name="start">Line의 시작점</param>
        /// <param name="end">Line의 끝점</param>
        public KLineProfileGrayCV(int width, int height, ushort[,,] data,
         KDepthType depth, Point start, Point end)
        {
            if (depth != KDepthType.Dt_16)
                throw new FormatException("16bit " + LangResource.ER_JIMG_IsNotGrayImg);

            this.Width = width;
            this.Height = height;
            this.data16 = data;
            this.Depth = depth;
            this.start = start;
            this.end = end;

            Initialize();
        }


        /// <summary>
        /// Line에서 값이 가장 작은 픽셀의 정보를 가져온다.(흑백이미지 전용 함수)
        /// </summary>
        /// <returns>Index(Line에서 몇번째 픽셀인가), Point(픽셀의 좌표), PixelValue(밝기값) 순서</returns>
        public Tuple<int, Point, int> GetMinValue()
        {
            if (depth != KDepthType.Dt_8 && depth != KDepthType.Dt_16)
                throw new FormatException(LangResource.ER_JIMG_IsNotGrayImg);

            var data = this.GetPixelDatas();

            var min = data.OrderBy(x => x.Item2).ToList().FirstOrDefault();
            var index = data.IndexOf(min);

            return new Tuple<int, Point, int>(index, min.Item1, min.Item2);
        }

        /// <summary>
        /// Line에서 값이 가장 큰 픽셀의 정보를 가져온다.(흑백 전용 함수)
        /// </summary>
        /// <returns>Index(Line에서 몇번째 픽셀인가), Point(픽셀의 좌표), PixelValue(밝기값) 순서</returns>
        public Tuple<int, Point, int> GetMaxValue()
        {
            if (depth != KDepthType.Dt_8 && depth != KDepthType.Dt_16)
                throw new FormatException(LangResource.ER_JIMG_IsNotGrayImg);

            var data = this.GetPixelDatas();

            var max = data.OrderByDescending(x => x.Item2).ToList().FirstOrDefault();
            var index = data.IndexOf(max);

            return new Tuple<int, Point, int>(index, max.Item1, max.Item2);
        }

        /// <summary>
        /// Line의 픽셀 데이터를 가져온다.(흑백이미지 전용 함수)
        /// </summary>
        /// <returns>Point(픽셀의 좌표), PixelValue(밝기값) 순서</returns>
        public List<Tuple<Point, int>> GetPixelDatas()
        {
            if (depth != KDepthType.Dt_8 && depth != KDepthType.Dt_16)
                throw new FormatException(LangResource.ER_JIMG_IsNotGrayImg);

            var pts = pointsInLine;
            List<Tuple<Point, int>> rtList = new List<Tuple<Point, int>>();

            if (Depth == KDepthType.Dt_8)
            {
                foreach (var pt in pts)
                    rtList.Add(new Tuple<Point, int>(new Point(pt.X, pt.Y), data8[pt.Y, pt.X, 0]));
            }
            if (Depth == KDepthType.Dt_16)
            {
                foreach (var pt in pts)
                    rtList.Add(new Tuple<Point, int>(new Point(pt.X, pt.Y), data16[pt.Y, pt.X, 0]));
            }

            return rtList;
        }

        /// <summary>
        /// 프로젝션 방식으로 Line의 픽셀 데이터를 가져온다.(흑백이미지 전용 함수)
        /// </summary>
        /// <param name="thickness">프로젝션 방식에 사용할 thickness값 (1이면 GetPixelDatas()함수와 동일)</param>
        /// <returns>Point(픽셀의 좌표), PixelValue(밝기평균값) 순서</returns>
        public List<Tuple<Point, float>> GetPixelDatasByProjection(int thickness = 1)
        {
            if (depth != KDepthType.Dt_8 && depth != KDepthType.Dt_16)
                throw new FormatException(LangResource.ER_JIMG_IsNotGrayImg);

            float sum = 0;
            List<Tuple<Point, float>> rtList = new List<Tuple<Point, float>>();

            if (thickness == 1)
            {
                var dts = GetPixelDatas();

                foreach (var item in dts)
                {
                    var pt = item.Item1;
                    var gv = item.Item2;

                    rtList.Add(new Tuple<Point, float>(pt, gv));
                }

                return rtList;
            }

            foreach (var pt in pointsInLine)
            {
                var rst = GetPointsUseTheta(pt, thickness);

                int cnt = rst.Count, divStep = 0;
                for (int i = 0; i < cnt; i++)
                {
                    if (Depth == KDepthType.Dt_8)
                    {
                        if ((rst[i].Y >= data8.GetLength(0) || rst[i].Y < 0) ||
                            (rst[i].X >= data8.GetLength(1) || rst[i].X < 0))
                            continue;

                        sum += data8[rst[i].Y, rst[i].X, 0];
                        divStep++;
                    }
                    if (Depth == KDepthType.Dt_16)
                    {
                        if ((rst[i].Y >= data16.GetLength(0) || rst[i].Y < 0) ||
                            (rst[i].X >= data16.GetLength(1) || rst[i].X < 0))
                            continue;

                        sum += data16[rst[i].Y, rst[i].X, 0];
                        divStep++;
                    }
                }

                rtList.Add(new Tuple<Point, float>(new Point(pt.X, pt.Y), (float)Math.Round(sum / divStep, 2)));
                sum = divStep = 0;
            }

            return rtList;
        }

        /// <summary>
        /// 1차 미분(변화량) 데이터를 가져온다.
        /// </summary>
        /// <param name="thickness">thickness값</param>
        /// <returns>Point(픽셀의 좌표), 1차미분값</returns>
        public List<Tuple<Point, float>> Calc1stDerivative(int thickness = 1)
        {
            if (depth != KDepthType.Dt_8 && depth != KDepthType.Dt_16)
                throw new FormatException(LangResource.ER_JIMG_IsNotGrayImg);

            List<Tuple<Point, float>> rtList = new List<Tuple<Point, float>>();

            if (thickness == 1)
            {
                var dts = GetPixelDatas();

                for (int i = 0; i < dts.Count - 1; i++)
                {
                    var pt = dts[i].Item1;
                    var prevVal = dts[i].Item2;
                    var nextVal = dts[i + 1].Item2;

                    rtList.Add(new Tuple<Point, float>(pt, nextVal - prevVal));
                }

                return rtList;
            }

            var rst = GetPixelDatasByProjection(thickness);

            for (int i = 0; i < rst.Count - 1; i++)
            {
                var pt = rst[i].Item1;
                var prevVal = rst[i].Item2;
                var nextVal = rst[i + 1].Item2;

                rtList.Add(new Tuple<Point, float>(pt, (float)Math.Round((nextVal - prevVal), 2)));
            }

            return rtList;
        }

        /// <summary>
        /// 2차 미분(변화량) 데이터를 가져온다.
        /// </summary>
        /// <param name="thickness">thickness값</param>
        /// <returns>Point(픽셀의 좌표), 2차미분값</returns>
        public List<Tuple<Point, float>> Calc2stDerivative(int thickness = 1)
        {
            if (depth != KDepthType.Dt_8 && depth != KDepthType.Dt_16)
                throw new FormatException(LangResource.ER_JIMG_IsNotGrayImg);

            List<Tuple<Point, float>> rtList = new List<Tuple<Point, float>>();
            int rtListCnt = 0;
            Tuple<Point, float>[] tempList;

            if (thickness == 1)
            {
                var dts = GetPixelDatas();

                //1차 미분
                for (int i = 0; i < dts.Count - 1; i++)
                {
                    var pt = dts[i].Item1;
                    var prevVal = dts[i].Item2;
                    var nextVal = dts[i + 1].Item2;

                    rtList.Add(new Tuple<Point, float>(pt, nextVal - prevVal));
                }

                //2차 미분
                rtListCnt = rtList.Count;
                tempList = new Tuple<Point, float>[rtListCnt];
                rtList.CopyTo(tempList);
                rtList.Clear();

                for (int i = 0; i < tempList.Length - 1; i++)
                {
                    var pt = tempList[i].Item1;
                    var prevVal = tempList[i].Item2;
                    var nextVal = tempList[i + 1].Item2;

                    rtList.Add(new Tuple<Point, float>(pt, (float)Math.Round((nextVal - prevVal), 2)));
                }

                return rtList;
            }

            var rst = GetPixelDatasByProjection(thickness);

            //1차 미분
            for (int i = 0; i < rst.Count - 1; i++)
            {
                var pt = rst[i].Item1;
                var prevVal = rst[i].Item2;
                var nextVal = rst[i + 1].Item2;

                rtList.Add(new Tuple<Point, float>(pt, (nextVal - prevVal)));
            }

            //2차 미분
            rtListCnt = rtList.Count;
            tempList = new Tuple<Point, float>[rtListCnt];
            rtList.CopyTo(tempList);
            rtList.Clear();

            for (int i = 0; i < tempList.Length - 1; i++)
            {
                var pt = tempList[i].Item1;
                var prevVal = tempList[i].Item2;
                var nextVal = tempList[i + 1].Item2;

                rtList.Add(new Tuple<Point, float>(pt, (float)Math.Round((nextVal - prevVal), 2)));
            }

            return rtList;
        }

        /// <summary>
        /// 데이터를 다루기 쉽도록 분류한다.
        /// </summary>
        /// <param name="src">분류할 원본 List</param>
        /// <param name="pts">분류된 Point배열</param>
        /// <param name="data">분류된 data배열</param>
        public void SeparateData(List<Tuple<Point, int>> src,
           out Point[] pts, out int[] data)
        {
            int cnt = src.Count;
            pts = new Point[cnt];
            data = new int[cnt];

            for (int i = 0; i < cnt; i++)
            {
                pts[i] = src[i].Item1;
                data[i] = src[i].Item2;
            }
        }

        /// <summary>
        /// 데이터를 다루기 쉽도록 분류한다.
        /// </summary>
        /// <param name="src">분류할 원본 List</param>
        /// <param name="pts">분류된 Point배열</param>
        /// <param name="data">분류된 data배열</param>
        public void SeparateData(List<Tuple<Point, float>> src,
           out Point[] pts, out float[] data)
        {
            int cnt = src.Count;
            pts = new Point[cnt];
            data = new float[cnt];

            for (int i = 0; i < cnt; i++)
            {
                pts[i] = src[i].Item1;
                data[i] = src[i].Item2;
            }
        }
    }
}
