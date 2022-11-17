using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.DIP
{
    /// <summary>
    /// Line에 대해 자주쓰이는 함수들을 모아놓은 클래스
    /// </summary>
    public static class KLine
    {
        /// <summary>
        /// 두점간의 거리를 구한다.
        /// </summary>
        /// <param name="p1">대상 PointF 1</param>
        /// <param name="p2">대상 PointF 2</param>
        /// <returns>두점간의 거리</returns>
        public static double GetDistance(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
        }

        /// <summary>
        /// 두점간의 선에 특정 포인트가 포함되어 있는지 확인한다.
        /// </summary>
        /// <param name="p1">대상 PointF 1</param>
        /// <param name="p2">대상 PointF 2</param>
        /// <param name="targetPt">p1, p2간의 선에 포함되어 있는지 확인할 Point</param>
        /// <param name="margin">값이 클수록 허용오차가 커진다, p1, p2간의 선이 값만큼 굵어진다고 생각하면 된다.</param>
        /// <returns></returns>
        public static bool IsOnLine(PointF p1, PointF p2, PointF targetPt, int margin = 10)
        {
            var isOnLine = false;

            using (var path = new GraphicsPath())
            using (var pen = new Pen(Brushes.Black, margin))
            {
                path.AddLine(p1, p2);
                isOnLine = path.IsOutlineVisible(targetPt, pen);
            }

            return isOnLine;
        }

        /// <summary>
        /// 두점간의 선에서, 그 선을 이루는 포인트들의 집합(List<Point>)을 구한다. [Bresenham's line algorithm 사용]
        /// </summary>
        /// <param name="pt1">대상 PointF 1</param>
        /// <param name="pt2">대상 PointF 2</param>
        /// <returns>선을 이루는 포인트들의 집합</returns>
        public static List<PointF> GetPointListOfLine(PointF pt1, PointF pt2, LineProFileAlgo algo = LineProFileAlgo.DDA)
        {
            List<PointF> rstPoints = new List<PointF>();

            switch (algo)
            {
                case LineProFileAlgo.Bresenham:
                    {
                        float dxB, dyB, sx, sy, err, e2;

                        dxB = Math.Abs(pt2.X - pt1.X);
                        sx = pt1.X < pt2.X ? 1 : -1;

                        dyB = -Math.Abs(pt2.Y - pt1.Y);
                        sy = pt1.Y < pt2.Y ? 1 : -1;

                        err = dxB + dyB;

                        while (true)
                        {
                            rstPoints.Add(new PointF(pt1.X, pt1.Y));

                            if (pt1.X == pt2.X &&
                                pt1.Y == pt2.Y)
                                break;

                            e2 = 2 * err;

                            // EITHER horizontal OR vertical step (but not both!)
                            if (e2 > dyB)
                            {
                                err += dyB;
                                pt1.X += sx;
                            }
                            else if (e2 < dxB)
                            { // <--- this "else" makes the difference
                                err += dxB;
                                pt1.Y += sy;
                            }
                        }

                        return rstPoints;
                    }

                case LineProFileAlgo.DDA:
                    {
                        float x, y, dxD, dyD, step;
                        int cnt = 0;

                        dxD = (pt2.X - pt1.X);
                        dyD = (pt2.Y - pt1.Y);

                        if (Math.Abs(dxD) >= Math.Abs(dyD))
                            step = Math.Abs(dxD);
                        else
                            step = Math.Abs(dyD);

                        dxD = dxD / step;
                        dyD = dyD / step;
                        x = pt1.X;
                        y = pt1.Y;

                        while (cnt <= step)
                        {
                            rstPoints.Add(new PointF(x, y));
                            x = x + dxD;
                            y = y + dyD;
                            cnt += 1;
                        }

                        return rstPoints;
                    }
            }

            throw new Exception("함수가 정상적으로 실행되지 않았습니다.");
        }

        /// <summary>
        /// 두점간의 선에서, 그 선을 이루는 포인트들의 집합(List<Point>)을 구한다. [Bresenham's line algorithm 사용]
        /// </summary>
        /// <param name="pt1">대상 Point 1</param>
        /// <param name="pt2">대상 Point 2</param>
        /// <returns>선을 이루는 포인트들의 집합</returns>
        public static List<Point> GetPointListOfLine(Point pt1, Point pt2, LineProFileAlgo algo = LineProFileAlgo.DDA)
        {
            List<Point> rstPoints = new List<Point>();

            switch (algo)
            {
                case LineProFileAlgo.Bresenham:
                    {
                        int dxB, dyB, sx, sy, err, e2;

                        dxB = Math.Abs((int)(pt2.X - pt1.X));
                        sx = pt1.X < pt2.X ? 1 : -1;

                        dyB = -Math.Abs((int)(pt2.Y - pt1.Y));
                        sy = pt1.Y < pt2.Y ? 1 : -1;

                        err = dxB + dyB;

                        while (true)
                        {
                            rstPoints.Add(new Point(
                                (int)pt1.X,
                                (int)pt1.Y));

                            if (pt1.X == pt2.X &&
                                pt1.Y == pt2.Y)
                                break;

                            e2 = 2 * err;

                            // EITHER horizontal OR vertical step (but not both!)
                            if (e2 > dyB)
                            {
                                err += dyB;
                                pt1.X += sx;
                            }
                            else if (e2 < dxB)
                            { // <--- this "else" makes the difference
                                err += dxB;
                                pt1.Y += sy;
                            }
                        }

                        return rstPoints;
                    }

                case LineProFileAlgo.DDA:
                    {
                        float x, y, dxD, dyD, step;
                        int cnt = 0;

                        dxD = (pt2.X - pt1.X);
                        dyD = (pt2.Y - pt1.Y);

                        if (Math.Abs(dxD) >= Math.Abs(dyD))
                            step = Math.Abs(dxD);
                        else
                            step = Math.Abs(dyD);

                        dxD = dxD / step;
                        dyD = dyD / step;
                        x = pt1.X;
                        y = pt1.Y;

                        while (cnt <= step)
                        {
                            rstPoints.Add(new Point(
                                (int)Math.Round(x, 0),
                                (int)Math.Round(y, 0)));

                            x = x + dxD;
                            y = y + dyD;

                            cnt += 1;
                        }

                        return rstPoints;
                    }
            }

            throw new Exception("함수가 정상적으로 실행되지 않았습니다.");
        }

        /// <summary>
        /// Line의 각도를 구한다
        /// </summary>
        /// <param name="pt1">Line의 시작점</param>
        /// <param name="pt2">Line의 끝점</param>
        /// <returns></returns>
        public static float CalcDegreeOfLine(PointF pt1, PointF pt2)
        {
            float rt = 0;

            float xIncrease = pt1.X - pt2.X;
            float yIncrease = pt1.Y - pt2.Y;

            rt = yIncrease / xIncrease;

            return rt;
        }

        /// <summary>
        /// 인자로 주어진 선분을 offset 만큼 연장했을때, 그 끝점을 구한다.
        /// ex) start = (1,1); end = (3,1); offset = 5; 이면 결과는 (8,1)
        /// Vector는 start에서 end방향으로 확장했을때를 기준으로 한다.
        /// </summary>
        /// <param name="start">시작점</param>
        /// <param name="end">끝점</param>
        /// <param name="offset">연장될 길이</param>
        /// <returns></returns>
        public static PointF CalcExtendedPoint(PointF start, PointF end, float offset)
        {
            PointF rt = new PointF();

            var vector = new PointF()
            {
                X = end.X - start.X,
                Y = end.Y - start.Y
            };

            float magnitude = (float)Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y)); // = length
            vector.X /= magnitude;
            vector.Y /= magnitude;
            PointF translation = new PointF()
            {
                X = offset * vector.X,
                Y = offset * vector.Y
            };

            using (Matrix m = new Matrix())
            {
                m.Translate(translation.X, translation.Y);
                PointF[] pts = new PointF[] { end };
                m.TransformPoints(pts);
                rt = pts[0];
            }

            return rt;
        }

        /// <summary>
        /// mm단위를 inch단위로 변환한다
        /// </summary>
        /// <param name="length_mm">변환할 mm 길이</param>
        /// <returns>변환된 Inch 길이</returns>
        public static double ConvertMMToINCH(double length_mm)
        {
            double rtInch = 0;
            double mmToInchVal = 0.0393701;

            rtInch = length_mm * mmToInchVal;

            return rtInch;
        }

        /// <summary>
        /// inch단위를 mm단위로 변환한다
        /// </summary>
        /// <param name="length_inch">변환할 inch 길이</param>
        /// <returns>변환된 mm 길이</returns>
        public static double ConvertINCHToMM(double length_inch)
        {
            double rtMM = 0;
            double inchToMMVal = 25.4;

            rtMM = length_inch * inchToMMVal;

            return rtMM;
        }
    }

    /// <summary>
    /// Rectangle에 대해 자주쓰이는 함수들을 모아놓은 클래스
    /// </summary>
    public static class KRectangle
    {
        /// <summary>
        /// 특정 PointF가 RectangleF의 테두리에 위치하는지 알아낸다
        /// </summary>
        /// <param name="rect">대상 RectangleF</param>
        /// <param name="targetPt">대상 위치 PointF</param>
        /// <param name="margin">적용할 margin(여유값)</param>
        /// <returns>결과 값</returns>
        public static bool IsOnBorder(RectangleF rect, PointF targetPt, int margin = 10)
        {
            PointF lt = rect.Location;
            PointF rt = new PointF(rect.Right, rect.Top);
            PointF lb = new PointF(rect.Left, rect.Bottom);
            PointF rb = new PointF(rect.Right, rect.Bottom);

            bool onUp = KLine.IsOnLine(lt, rt, targetPt, margin);
            bool onDown = KLine.IsOnLine(lb, rb, targetPt, margin);
            bool onLeft = KLine.IsOnLine(lt, lb, targetPt, margin);
            bool onRight = KLine.IsOnLine(rt, rb, targetPt, margin);

            var rst = onUp | onDown | onLeft | onRight;

            return rst;
        }

        /// <summary>
        /// 특정 PointF가 RectangleF의 모서리에 위치하는지 알아낸다
        /// </summary>
        /// <param name="rect">대상 RectangleF</param>
        /// <param name="targetPt">대상 위치 PointF</param>
        /// <param name="margin">적용할 margin(여유값)</param>
        /// <returns>결과 값</returns>
        public static bool IsOnCorner(RectangleF rect, PointF targetPt, int margin = 6)
        {
            var rst = GetWhichCornerClicked(rect, targetPt, margin);

            if (rst == KRectCorner.NONE)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 특정 PointF가 RectangleF의 '어떤' 모서리에 위치하는지 알아낸다
        /// </summary>
        /// <param name="rect">대상 RectangleF</param>
        /// <param name="targetPt">대상 위치 PointF</param>
        /// <param name="margin">적용할 margin(여유값)</param>
        /// <returns>결과 값</returns>
        public static KRectCorner GetWhichCornerClicked(RectangleF rect, PointF targetPt, int margin = 6)
        {
            Size sz = new Size(margin, margin);
            float pad = margin / 2f;

            PointF lt = rect.Location;
            PointF rt = new PointF(rect.Right, rect.Top);
            PointF lb = new PointF(rect.Left, rect.Bottom);
            PointF rb = new PointF(rect.Right, rect.Bottom);

            lt.X -= pad; lt.Y -= pad;
            rt.X -= pad; rt.Y -= pad;
            lb.X -= pad; lb.Y -= pad;
            rb.X -= pad; rb.Y -= pad;

            RectangleF[] crnRct = new RectangleF[4];
            crnRct[0] = new RectangleF(lt, sz);
            crnRct[1] = new RectangleF(rt, sz);
            crnRct[2] = new RectangleF(lb, sz);
            crnRct[3] = new RectangleF(rb, sz);

            float x = targetPt.X;
            float y = targetPt.Y;

            if (crnRct[0].Contains(x, y)) return KRectCorner.LT;
            if (crnRct[1].Contains(x, y)) return KRectCorner.RT;
            if (crnRct[2].Contains(x, y)) return KRectCorner.LB;
            if (crnRct[3].Contains(x, y)) return KRectCorner.RB;
            else return KRectCorner.NONE;
        }

        /// <summary>
        /// 이미지내에서 특정 region을 지정하여, 그 픽셀 데이터 배열을 가져온다
        /// </summary>
        /// <param name="imageArr">원본 이미지 배열</param>
        /// <param name="width">원본 이미지의 가로 길이</param>
        /// <param name="region">적용할 Region</param>
        /// <returns>결과 값</returns>
        public static ushort[] GetRegionArr(ushort[] imageArr, int width, Rectangle region)
        {
            ushort[] rstArr = new ushort[region.Width * region.Height];

            int h = region.Y;
            int w = region.X;
            int rgnH = region.Height + h;
            int rgnW = region.Width + w;
            int cnt = 0;

            for (int i = h; i < rgnH; i++)
            {
                for (int j = w; j < rgnW; j++)
                {
                    rstArr[cnt] = imageArr[i * width + j];
                    cnt++;
                }
            }

            return rstArr;
        }
    }
}
