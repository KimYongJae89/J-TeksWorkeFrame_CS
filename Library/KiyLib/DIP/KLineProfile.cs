using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.DIP
{
    /// <summary>
    /// Line Profile 연산을 위한 클래스
    /// </summary>
    public class KLineProfile
    {
        private Point _start;
        private Point _end;
        private int _width;
        private int _height;

        /// <summary>
        /// Line의 시작점
        /// </summary>
        public Point Start
        {
            get { return _start; }
            set { _start = value; }
        }

        /// <summary>
        /// Line의 끝점
        /// </summary>
        public Point End
        {
            get { return _end; }
            set { _end = value; }
        }

        /// <summary>
        /// 이미지의 가로길이
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// 이미지의 세로길이
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }



        public KLineProfile()
        {
            _start = new Point(0, 0);
            _end = new Point(0, 0);
        }

        public KLineProfile(int width, int height, Point start, Point end)
        {
            this.Width = width;
            this.Height = height;
            this._start = start;
            this._end = end;
        }

        public KLineProfile(int width, int height,
                            int x1, int y1,
                            int x2, int y2)
        {
            this._start = new Point(x1, y1);
            this._end = new Point(x2, y2);
        }


        /// <summary>
        ///  Line을 Point들의 집합으로 변환한다
        ///  DDA알고리즘을 사용한다 (매트록스에서 사용하는 방식)
        /// </summary>
        /// <param name="start">시작점</param>
        /// <param name="end">끝점</param>
        /// <returns>결과 값</returns>
        public List<Point> GetPointListOfLine(Point start, Point end)
        {
            return KLine.GetPointListOfLine(start, end);
        }

        /// <summary>
        ///  Line을 Point들의 집합으로 변환한다
        ///  DDA알고리즘을 사용한다 (매트록스에서 사용하는 방식)
        /// </summary>
        /// <returns>결과 값</returns>
        public List<Point> GetPointListOfLine()
        {
            return GetPointListOfLine(this._start, this._end);
        }

        /// <summary>
        /// Line을 Point들의 집합으로 변환했을때, 그 각각의 Point들에 해당하는 픽셀값의 집합을 가져온다
        /// </summary>
        /// <param name="imgArr">이미지의 픽셀 데이터</param>
        /// <param name="width">이미지의 가로 길이</param>
        /// <param name="start">Line의 시작점</param>
        /// <param name="end">Line의 끝점</param>
        /// <returns>결과 값</returns>
        public ushort[] GetPixelValues(ushort[] imgArr, int width, Point start, Point end)
        {
            //var stPt = ModifyPointRange(start, bmp);
            //var endPt = ModifyPointRange(end, bmp);
            var stPt = start;
            var endPt = end;

            List<ushort> rstGvalueList = new List<ushort>();
            ushort gVal;

            int x = 0, y = 0;
            foreach (var item in GetPointListOfLine(stPt, endPt))
            {
                x = item.X;
                y = item.Y;

                gVal = imgArr[y * width + x];
                rstGvalueList.Add(gVal);
            }

            return rstGvalueList.ToArray();

            /*byte gVal;
            var lck = new KiyLockBitmap(bmp);
            lck.LockBits();

            int x = 0, y = 0;
            foreach (var item in GetPointListOfLine(stPt, endPt))
            {
                x = item.X;
                y = item.Y;

                gVal = lck[x, y];
                rstGvalueList.Add(gVal);
            }

            lck.UnlockBits();

            return rstGvalueList.ToArray();*/

        }

        /// <summary>
        /// Line을 Point들의 집합으로 변환했을때, 그 각각의 Point들에 해당하는 픽셀값의 집합을 가져온다
        /// </summary>
        /// <param name="imgArr">이미지의 픽셀 데이터</param>
        /// <returns>결과 값</returns>
        public ushort[] GetPixelValues(ushort[] imgArr)
        {
            return GetPixelValues(imgArr, this._width, this._start, this._end);
        }

        /// <summary>
        /// Line을 Point들의 집합으로 변환했을때, 그 각각의 Point들에 해당하는 픽셀값의 집합을 가져온다
        /// </summary>
        /// <param name="imgArr">이미지의 픽셀 데이터</param>
        /// <returns>결과 값</returns>
        /// 
        /// <summary>
        /// Line을 Point들의 집합으로 변환했을때, 그 각각의 Point와 밝기값 쌍을 가져온다
        /// </summary>
        /// <param name="imgArr">이미지의 픽셀 데이터</param>
        /// <returns>결과 값, (좌표, 밝기값)</returns>
        public Dictionary<Point, ushort> GetInfoByDictionary(ushort[] imgArr)
        {
            try
            {
                //var stPt = ModifyPointRange(start, bmp);
                //var endPt = ModifyPointRange(end, bmp);
                var stPt = _start;
                var endPt = _end;
                Dictionary<Point, ushort> rstDic = new Dictionary<Point, ushort>();
                ushort gVal = 0;

                foreach (var item in GetPointListOfLine(stPt, endPt))
                {
                    int x = item.X;
                    int y = item.Y;

                    var pt = new Point(x, y);
                    gVal = imgArr[y * _width + x];

                    rstDic.Add(pt, gVal);
                }
                /*
                foreach (var item in GetPointListOfLine(stPt, endPt))
                {
                    var pt = new Point(item.X, item.Y);
                    byte gVal = bmp.GetPixel((int)item.X, (int)item.Y).R;

                    rstDic.Add(pt, gVal);
                }
                */
                return rstDic;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return null;
            }
        }

        /// <summary>
        /// Projection방식으로 Profile을 할때 두께가 1보다 크면 선이 아닌 사격형이된다
        /// 이때 사각형의 각 꼭지점 좌표의 집합을 리턴한다
        /// </summary>
        /// <param name="startX">시작점 x</param>
        /// <param name="startY">시작점 y</param>
        /// <param name="endX">끝점 x</param>
        /// <param name="endY">끝점 y</param>
        /// <param name="thickness">두께</param>
        /// <returns>결과 값</returns>
        public static List<Point> GetProjectionRectPoints(int startX, int startY, int endX, int endY, int thickness)
        {
            return GetProjectionRectPoints(new Point(startX, startY), new Point(endX, endY), thickness);
        }

        /// <summary>
        /// Projection방식으로 Profile을 할때 두께가 1보다 크면 선이 아닌 사격형이된다
        /// 이때 사각형의 각 꼭지점 좌표의 집합을 리턴한다
        /// </summary>
        /// <param name="start">시작점</param>
        /// <param name="end">끝점</param>
        /// <param name="thickness">두께</param>
        /// <returns>결과 값</returns>
        public static List<Point> GetProjectionRectPoints(Point start, Point end, int thickness)
        {
            if (thickness <= 0)
                throw new ArgumentException("thickness는 1이상 이어야 합니다.");

            float degree = KLine.CalcDegreeOfLine(start, end);
            List<Point> rtPts = new List<Point>();
            List<Point> pointsInLine = KLine.GetPointListOfLine(start, end, LineProFileAlgo.DDA);

            if (pointsInLine.Count == 0)
                throw new ArgumentException("pointsInLine의 개수는 1이상 이어야 합니다.");

            var sPt = pointsInLine[0];
            var ePt = pointsInLine[pointsInLine.Count - 1];

            var sPtList = GetPointsUseTheta(sPt, degree, thickness);
            var ePtList = GetPointsUseTheta(ePt, degree, thickness);

            if (sPtList.Count == 0)
                throw new ArgumentException("sPtList의 개수는 1이상 이어야 합니다.");

            if (ePtList.Count == 0)
                throw new ArgumentException("ePtList의 개수는 1이상 이어야 합니다.");

            rtPts.Add(sPtList[0]);
            rtPts.Add(sPtList[sPtList.Count - 1]);

            rtPts.Add(ePtList[ePtList.Count - 1]);
            rtPts.Add(ePtList[0]);

            return rtPts;
        }
        /*
        public static List<Point> GetProjectionPolyPoints(Point start, Point end, int thickness)
        {
            if (thickness <= 0)
                throw new ArgumentException("thickness는 1이상 이어야 합니다.");

            float degree = KiyLine.CalcDegreeOfLine(start, end);



            List<Point> rtPts = new List<Point>();

            return rtPts;
        }*/

        /// <summary>
        /// Projection방식으로 Profile을 할때 두께가 1보다 크면 Point는 점이 아닌 Line이 된다
        /// 이때 Line을 Point들의 집합으로 변환했을때, 그 각각의 Point들의 집합을 가져온다
        /// </summary>
        /// <param name="pt">대상 Point</param>
        /// <param name="degree">점이 기울어진 각도</param>
        /// <param name="thickness">점의 두께</param>
        /// <returns></returns>
        public static List<Point> GetPointsUseTheta(Point pt, float degree, int thickness)
        {
            if (thickness <= 0)
                throw new ArgumentException("thickness must be at least 1");

            bool isThickEvenNum =
                thickness % 2 == 0 ? true : false;

            if (isThickEvenNum)
                thickness += 1;

            PointF rstStPt = new PointF();
            PointF rstEdPt = new PointF();

            int thick = thickness / 2;
            float cos = 0, sin = 0;
            float newInclination = -1 / degree;     // 직각 기울기
            float tan = Math.Abs(newInclination);   // 기울기 = tan
            double theta = Math.Atan(tan);          // 라디안

            cos = (float)Math.Cos(theta) * thick;
            sin = (float)Math.Sin(theta) * thick;

            if (newInclination > 0)
            {
                rstStPt.X = pt.X - cos;
                rstStPt.Y = pt.Y - sin;
                rstEdPt.X = pt.X + cos;
                rstEdPt.Y = pt.Y + sin;
            }
            else
            {
                rstStPt.X = pt.X + cos;
                rstStPt.Y = pt.Y - sin;
                rstEdPt.X = pt.X - cos;
                rstEdPt.Y = pt.Y + sin;
            }

            if (isThickEvenNum)
            {
                var extdPT = KLine.CalcExtendedPoint(rstStPt, rstEdPt, -1f);
                rstEdPt = extdPT;
            }

            var rtListPt = KLine.GetPointListOfLine(
                Point.Round(rstStPt),
                Point.Round(rstEdPt),
                LineProFileAlgo.DDA);

            //if (isThickEvenNum && rt.Count > 0)
            //    rt.RemoveAt(0);

            //var rtListPtF = rtListPt.ConvertAll(ptF => Point.Round(ptF));

            return rtListPt;
        }
    }
}
