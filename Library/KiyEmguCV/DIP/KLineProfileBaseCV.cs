using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyEmguCV.DIP
{
    /// <summary>
    /// Line Profile를 실행하기 위한 상위 클래스
    /// </summary>
    public class KLineProfileBaseCV
    {
        protected Point start;
        protected Point end;
        protected int width, height;
        protected KDepthType depth;
        protected List<Point> pointsInLine;
        // Line의 기울기
        protected float degree;

        /// <summary>
        /// Line의 시작점
        /// </summary>
        public Point Start
        {
            get { return start; }
            set
            {
                start = value;
                Initialize();
            }
        }

        /// <summary>
        /// Line의 끝점
        /// </summary>
        public Point End
        {
            get { return end; }
            set
            {
                end = value;
                Initialize();
            }
        }

        /// <summary>
        /// 가로 길이
        /// </summary>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// 시작점과 끝점사이의 거리(단위: 픽셀)
        /// </summary>
        public double Length
        {
            get { return KLine.GetDistance(Start, End); }
        }

        /// <summary>
        /// 이미지의 Depth(bit)
        /// </summary>
        public KDepthType Depth
        {
            get { return depth; }
            protected set { depth = value; }
        }

        /// <summary>
        /// Line을 Point로 변환했을때, 그 Point들의 집합
        /// </summary>
        public List<Point> PointsInLine
        {
            get { return pointsInLine; }
            protected set { pointsInLine = value; }
        }


        /// <summary>
        /// 초기화 함수
        /// 시작점과 끝점의 값이 변경될때 실행된다
        /// </summary>
        protected void Initialize()
        {
            this.PointsInLine = this.GetLinePoints();
            this.degree = CalcDegreeBetweenPoints(start, end);
        }

        /// <summary>
        /// Line을 Point로 변환했을때, 그 Point들의 집합을 구한다
        /// DDA알고리즘을 사용한다 (매트록스에서 사용하는 알고리즘과 동일)
        /// </summary>
        /// <returns>결과 값</returns>
        protected List<Point> GetLinePoints()
        {
            //기본은 DDA 알고리즘 사용 - 매트록스에서 사용하는 알고리즘
            return KLine.GetPointListOfLine(start, end, LineProFileAlgo.DDA);
        }

        // P1 과 P2 사이의 기울기 출력
        /// <summary>
        /// Line의 기울기(degree)를 구한다
        /// </summary>
        /// <param name="p1">시작점</param>
        /// <param name="p2">끝점</param>
        /// <returns>결과 값</returns>
        protected float CalcDegreeBetweenPoints(PointF p1, PointF p2)
        {
            float rt = 0;

            float xIncrease = p1.X - p2.X;
            float yIncrease = p1.Y - p2.Y;

            rt = yIncrease / xIncrease;

            return rt;
        }

        /// <summary>
        /// Line의 두께가 1이상일때, 특정 Point에 수직으로 직교하는 Line의 Point집합을 구한다
        /// Projection방식으로 Profile을 실행할때 사용된다.
        /// </summary>
        /// <param name="pt">Line에 속한 특정 좌표</param>
        /// <param name="thickness">Line의 두께, 최소크기는 1이다</param>
        /// <returns>결과 값</returns>
        protected List<Point> GetPointsUseTheta(Point pt, int thickness)
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


        /// <summary>
        /// 입력된 인자를 기반으로 객체를 초기화 한다.(생성자에서 자동으로 1회 실행)
        /// </summary>
        /// <param name="start">시작점</param>
        /// <param name="end">끝점</param>
        public void Proc(Point start, Point end)
        {
            this.start = start;
            this.end = end;
            Initialize();
        }

        /// <summary>
        /// 프로젝션방식으로 라인프로파일 했을때, Rect영역의 각 모서리의 좌표를 리턴한다.
        /// Rectangle객체로 리턴하면 Rotate했을때 정확히 나타낼수 없어, 각 모서리의 좌표를 리턴한다.
        /// </summary>
        /// <param name="thickness">프로젝션에 사용한 thickness값</param>
        /// <returns>각 모서리의 좌표</returns>
        public List<Point> GetProjectionRectPoints(int thickness)
        {
            if (thickness <= 0)
                throw new ArgumentException("thickness must be at least 1");

            if (pointsInLine.Count == 0)
                throw new ArgumentException("The number of pointsInLine must be at least 1");

            List<Point> rtPts = new List<Point>();

            var sPt = pointsInLine[0];
            var ePt = pointsInLine[pointsInLine.Count - 1];

            var sPtList = GetPointsUseTheta(sPt, thickness);
            var ePtList = GetPointsUseTheta(ePt, thickness);

            if (sPtList.Count == 0)
                throw new ArgumentException("The number of sPtList must be at least 1");

            if (ePtList.Count == 0)
                throw new ArgumentException("The number of ePtList must be at least 1");

            rtPts.Add(sPtList[0]);
            rtPts.Add(sPtList[sPtList.Count - 1]);

            rtPts.Add(ePtList[ePtList.Count - 1]);
            rtPts.Add(ePtList[0]);

            return rtPts;
        }
    }
}
