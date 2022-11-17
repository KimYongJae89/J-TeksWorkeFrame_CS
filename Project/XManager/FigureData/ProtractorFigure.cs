using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XManager.Util;

namespace XManager.FigureData
{
    public struct tProtractorResult
    {
        public double degreeResult;
    }
    public class ProtractorFigure : Figure
    {
        private enum eQuadrant
        {
            NONE,
            QUADRANT1,
            QUADRANT2,
            QUADRANT3,
            QUADRANT4,
        }
        tProtractorResult _result = new tProtractorResult();
        private RectangleF _gridRect = new RectangleF();
        private PointF fixedPoint = new PointF();

        private PointF _gridCenterPoint = new PointF(); // 원의 중심
        private PointF _point1 = new PointF(); // 이동하는 Point1
        private PointF _point2 = new PointF();// 이동하는 Point1

        private RectangleF _leftTopRectangle;
        private RectangleF _leftBottomRectangle;
        private RectangleF _rightTopRectangle;
        private RectangleF _rightBottomRectangle;
        private RectangleF _point1Rectangle;
        private RectangleF _point2Rectangle;
        private double _degree = 0.0F;
        public ProtractorFigure()
        {
            type = eFigureType.Protractor;
        }

        public ProtractorFigure(PointF startPoint, PointF endPoint)
        {
            type = eFigureType.Protractor;
            _gridRect = base.CalcPointToRectangleF(startPoint, endPoint);
            SetGridRectangleAndPoint(ref _gridRect);

        }

        public override void TrackRectangleInitialize()
        {
            _leftTopRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _leftBottomRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _rightTopRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _rightBottomRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _point1Rectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _point2Rectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
        }

        private void SetGridRectangleAndPoint(ref RectangleF rect)
        {
            if(rect.Width > rect.Height)
            {
                rect.Width = rect.Height;
            }
            else
            {
                rect.Height = rect.Width;
            }
            _gridCenterPoint.X = rect.X + rect.Width / 2;
            _gridCenterPoint.Y = rect.Y + rect.Height / 2;
            _point1.X = rect.X + rect.Width / 2;
            _point1.Y = rect.Bottom + rect.Height / 2;
            _point2.X = rect.Right + rect.Width / 2;
            _point2.Y = rect.Y + rect.Height / 2;
        }

        public override bool CheckPointInFigure(PointF point)
        {
            _gridCenterPoint.X = _gridRect.X + _gridRect.Width / 2;
            _gridCenterPoint.Y = _gridRect.Y + _gridRect.Height / 2;
            bool ret = _gridRect.Contains(point) || base.IsOnLine(_gridCenterPoint, _point1, point) || base.IsOnLine(_gridCenterPoint, _point2, point);

            return ret;
        }

        public override bool CheckRegionInFigure(PointF startPoint, PointF endPoint)
        {
            RectangleF rect = base.CalcPointToRectangleF(startPoint, endPoint);
            if (rect.Width == 0 || rect.Height == 0)
            {
                return false;
            }

            List<RectangleF> editRect = GetTrackerRectangle();
            int containCount = 0;
            foreach (RectangleF edgeRect in editRect)
            {
                bool isContain = rect.Contains(edgeRect);
                if (isContain)
                {
                    containCount++;
                }
            }
            if (containCount == editRect.Count)
            {
                FigureMode = eFigureMode.Edit;
                return true;
            }
            else
                return false;
        }

        public override eTrackPosType CheckTrackPos(PointF pt)
        {
            List<RectangleF> editRect = GetTrackerRectangle();

            _gridCenterPoint.X = _gridRect.X + _gridRect.Width / 2;
            _gridCenterPoint.Y = _gridRect.Y + _gridRect.Height / 2;

            bool isOnLine = this._gridRect.Contains(pt) || base.IsOnLine(_gridCenterPoint, _point1, pt) 
                                                        || base.IsOnLine(_gridCenterPoint, _point2, pt);
            foreach (RectangleF rect in editRect)
            {
                bool isContain = rect.Contains(pt);
                if (isContain)
                {
                    if (rect == _leftTopRectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.LeftTop;
                        return eTrackPosType.LeftTop;
                    }
                    else if (rect == _leftBottomRectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.LeftBottom;
                        return eTrackPosType.LeftBottom;
                    }
                    else if (rect == _rightTopRectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.RightTop;
                        return eTrackPosType.RightTop;
                    }
                    else if (rect == _rightBottomRectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.RightBottom;
                        return eTrackPosType.RightBottom;
                    }
                    else if (rect == _point1Rectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.Point1;
                        return eTrackPosType.LeftTop;
                    }
                    else if (rect == _point2Rectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.Point2;
                        return eTrackPosType.LeftTop;
                    }
                    else
                    { }
                }
            }

            if (isOnLine)
            {
                FigureMode = eFigureMode.Edit;
                TrackerPosType = eTrackPosType.Inside;
                return eTrackPosType.Inside;
            }
            else
            {
                FigureMode = eFigureMode.None;
                TrackerPosType = eTrackPosType.None;
                return eTrackPosType.None;
            }
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void Draw(Graphics g)
        {
            try
            {

                Pen pen = new Pen(Color.Yellow, 1);
                Brush brush = Brushes.White;
                Pen gridPen = new Pen(Color.White, 1);
                Font font = new Font("고딕", 10);
                gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                gridPen.DashPattern = new float[] { 3, 3 };
                g.DrawArc(gridPen, _gridRect, 0, 360);

                g.DrawLine(pen, _gridCenterPoint, _point1);
                g.DrawLine(pen, _gridCenterPoint, _point2);
                List<RectangleF> rectangles = GetTrackerRectangle();

                eQuadrant QuardrantPoint1 = CheckQuadrant(_gridCenterPoint, _point1);
                eQuadrant QuardrantPoint2 = CheckQuadrant(_gridCenterPoint, _point2);
                double degreePoint1 = CalcDegreeFromZeroDegreeToPt(_gridCenterPoint, _point2, QuardrantPoint2);
                double degreePoint2 = CalcDegreeFromZeroDegreeToPt(_gridCenterPoint, _point1, QuardrantPoint1);

                double ResultDegree = 0;
                double drawStartDegree = 0;
                if (degreePoint1 >= degreePoint2)
                {
                    ResultDegree = 360.0d - Math.Abs(degreePoint1 - degreePoint2);
                    drawStartDegree = degreePoint1;
                }

                else
                {
                    ResultDegree = Math.Abs(degreePoint1 - degreePoint2);
                    drawStartDegree = degreePoint1;
                }
                this._degree = ResultDegree;
                g.DrawArc(pen, _gridRect, (float)drawStartDegree, (float)ResultDegree);
                g.DrawString(Math.Round(ResultDegree, 2).ToString(), font, brush, _gridRect.Right, _gridRect.Bottom);

                if (Selectable)
                {
                    foreach (RectangleF rect in rectangles)
                    {
                        g.FillRectangle(Brushes.White, rect);
                    }
                }

            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
           
        }

        public override List<RectangleF> GetTrackerRectangle()
        {
            if (base.TrackRectangleList.Count > 0)
                base.TrackRectangleList.Clear();

            TrackRectangleInitialize();

            int halfTrackRectWidth = TrackRectWidth / 2;

            _leftTopRectangle.Location = new PointF(_gridRect.X - halfTrackRectWidth, _gridRect.Y - halfTrackRectWidth);
            _leftBottomRectangle.Location = new PointF(_gridRect.X - halfTrackRectWidth, _gridRect.Bottom - halfTrackRectWidth);
            _rightTopRectangle.Location = new PointF(_gridRect.Right - halfTrackRectWidth, _gridRect.Y - halfTrackRectWidth);
            _rightBottomRectangle.Location = new PointF(_gridRect.Right - halfTrackRectWidth, _gridRect.Bottom - halfTrackRectWidth);

            _point1Rectangle.Location = new PointF(_point1.X - halfTrackRectWidth, _point1.Y - halfTrackRectWidth);
            _point2Rectangle.Location = new PointF(_point2.X - halfTrackRectWidth, _point2.Y - halfTrackRectWidth);

            base.TrackRectangleList.Add(_leftTopRectangle);
            base.TrackRectangleList.Add(_leftBottomRectangle);
            base.TrackRectangleList.Add(_rightTopRectangle);
            base.TrackRectangleList.Add(_rightBottomRectangle);

            base.TrackRectangleList.Add(_point1Rectangle);
            base.TrackRectangleList.Add(_point2Rectangle);

            return base.TrackRectangleList;
        }

        public override void MouseDown(PointF startTrackPos)
        {
            StartingMovePoint = startTrackPos;
            CalcFixPoint();
            MouseType = eMouseType.Down;
        }

        public override void MouseMove(PointF pt)
        {
            MovingMovePoint = pt;
            MouseType = eMouseType.Move;
            if (Selectable)
            {
                if (FigureMode == eFigureMode.None)
                {
                    if (TrackerPosType == eTrackPosType.RightBottom)
                    {
                        FigureMode = eFigureMode.Edit;
                    }
                    else if (TrackerPosType == eTrackPosType.LeftTop)
                    {
                        FigureMode = eFigureMode.Edit;
                    }
                    else if (TrackerPosType == eTrackPosType.None)
                    {
                        FigureMode = eFigureMode.None;
                    }
                }
                else if (FigureMode == eFigureMode.Edit)
                {

                    if (TrackerPosType == eTrackPosType.RightBottom || TrackerPosType == eTrackPosType.LeftTop
                        || TrackerPosType == eTrackPosType.RightTop || TrackerPosType == eTrackPosType.LeftBottom)
                    {
                        _gridRect = base.CalcPointToRectangleF(MovingMovePoint, fixedPoint,true);
                        _gridCenterPoint.X = _gridRect.X + _gridRect.Width / 2;
                        _gridCenterPoint.Y = _gridRect.Y + _gridRect.Height / 2;
                    }
                    else if(TrackerPosType == eTrackPosType.Point1)
                    {
                        _point1 = MovingMovePoint;
                    }
                    else if(TrackerPosType == eTrackPosType.Point2)
                    {
                        _point2 = MovingMovePoint;
                    }
                    else if (TrackerPosType == eTrackPosType.Inside)
                    {
                        float moveX = StartingMovePoint.X - MovingMovePoint.X;
                        float moveY = StartingMovePoint.Y - MovingMovePoint.Y;
                        _gridRect.X -= moveX;
                        _gridRect.Y -= moveY;
                        _gridCenterPoint.X -= moveX;
                        _gridCenterPoint.Y -= moveY;
                        _point1.X -= moveX;
                        _point1.Y -= moveY;
                        _point2.X -= moveX;
                        _point2.Y -= moveY;
                    }
                    else
                    {
                    }
                }
            }
            EndMovePoint = MovingMovePoint;
            StartingMovePoint = EndMovePoint;
        }

        public override void MouseUp(PointF endTrackPos)
        {
            MouseType = eMouseType.Up;
            TrackerPosType = eTrackPosType.None;
            FigureMode = eFigureMode.None;
        }

        public override void Scale(ImageCoordTransformer coordTransformer)
        {
            _gridRect.X = _gridRect.X * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _gridRect.Y = _gridRect.Y * coordTransformer.NewHeight / coordTransformer.OrgHeight;

            _gridRect.Width = _gridRect.Width * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _gridRect.Height = _gridRect.Height * coordTransformer.NewHeight / coordTransformer.OrgHeight;

            _point1.X = _point1.X * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _point1.Y = _point1.Y * coordTransformer.NewHeight / coordTransformer.OrgHeight;

            _point2.X = _point2.X * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _point2.Y = _point2.Y * coordTransformer.NewHeight / coordTransformer.OrgHeight;

            _gridCenterPoint.X = _gridRect.X + _gridRect.Width / 2;
            _gridCenterPoint.Y = _gridRect.Y + _gridRect.Height / 2;

        }

        private eQuadrant CheckQuadrant(PointF centerPt, PointF pt)
        {
            float rectWidth = Math.Abs(centerPt.X - pt.X) * 2;
            float rectHeight = Math.Abs(centerPt.Y - pt.Y) * 2;
            RectangleF quadrant1_Rect = new RectangleF(centerPt.X, centerPt.Y - rectHeight, rectWidth, rectHeight);
            RectangleF quadrant2_Rect = new RectangleF(centerPt.X - rectWidth, centerPt.Y - rectHeight, rectWidth, rectHeight);
            RectangleF quadrant3_Rect = new RectangleF(centerPt.X - rectWidth, centerPt.Y, rectWidth, rectHeight);
            RectangleF quadrant4_Rect = new RectangleF(centerPt.X, centerPt.Y, rectWidth, rectHeight);

            if (quadrant1_Rect.Contains(pt) == true)
                return eQuadrant.QUADRANT1;
            else if (quadrant2_Rect.Contains(pt) == true)
                return eQuadrant.QUADRANT2;
            else if (quadrant3_Rect.Contains(pt) == true)
                return eQuadrant.QUADRANT3;
            else if (quadrant4_Rect.Contains(pt) == true)
                return eQuadrant.QUADRANT4;
            else
                return eQuadrant.NONE;
        }

        private PointF calcNewPointInCircle(PointF centerPoint, PointF point, float gridRectWidth, float gridRectHeight)
        {
            if (gridRectWidth != gridRectHeight)
                return new PointF(0, 0);
            float girdRectRadius = gridRectWidth / 2;

            PointF tempPoint = new PointF(point.X, centerPoint.Y); // y축을 center 좌표로 내림
            double LengthCenterPointToTempPoint = Math.Sqrt(Math.Pow((tempPoint.X - centerPoint.X), 2));
            double centerPointToPoint = Math.Sqrt(Math.Pow((point.X - centerPoint.X), 2) + Math.Pow((point.Y - centerPoint.Y), 2));
            double cosRadian = (double)LengthCenterPointToTempPoint / centerPointToPoint; // 라디안

            double degree = Math.Acos(cosRadian) * 180.0d / Math.PI; // 각도

            double newPointX = 0;
            if (degree == 0)
                newPointX = centerPoint.X + girdRectRadius;
            if (centerPoint.X >= point.X)
                newPointX = centerPoint.X - (double)girdRectRadius * cosRadian;
            else
                newPointX = centerPoint.X + (double)girdRectRadius * cosRadian;


            double m = (point.Y - centerPoint.Y) / (point.X - centerPoint.X); // 기울기
            double y = 0;
            if (point.X - centerPoint.X == 0)
            {
                newPointX = centerPoint.X;
                y = centerPoint.Y + girdRectRadius;
            }
            else
            {
                y = m * (newPointX - centerPoint.X) + centerPoint.Y;
            }

            return new PointF((float)newPointX, (float)y);
        }

        private void CalcFixPoint()
        {
            if (TrackerPosType == eTrackPosType.LeftTop)
            {
                fixedPoint.X = _gridRect.X + _gridRect.Width;
                fixedPoint.Y = _gridRect.Y + _gridRect.Height;
            }
            else if (TrackerPosType == eTrackPosType.RightBottom)
            {
                fixedPoint.X = _gridRect.X;
                fixedPoint.Y = _gridRect.Y;
            }
            else if (TrackerPosType == eTrackPosType.LeftBottom)
            {
                fixedPoint.X = _gridRect.X + _gridRect.Width;
                fixedPoint.Y = _gridRect.Y;
            }
            else if (TrackerPosType == eTrackPosType.RightTop)
            {
                fixedPoint.X = _gridRect.X;
                fixedPoint.Y = _gridRect.Y + _gridRect.Height;
            }
        }

        private double CalcDegreeFromZeroDegreeToPt(PointF centerPt, PointF point, eQuadrant quadrant)
        {// 제2코사인법칙
            double result = 0;
            PointF tempPoint = new PointF(centerPt.X + 1, centerPt.Y); // 임의점
            double centerToPointLength = base.CalcLengthPointToPoint(centerPt, point);// 세변
            double centerToTempPointLength = base.CalcLengthPointToPoint(centerPt, tempPoint);// 세변
            double tempPointTopointLength = base.CalcLengthPointToPoint(point, tempPoint);// 세변
            double temp = Math.Pow(centerToPointLength, 2) + Math.Pow(centerToTempPointLength, 2) - Math.Pow(tempPointTopointLength, 2);
            double cos = temp / (2.0d * centerToPointLength * centerToTempPointLength);
            result = Math.Acos(cos) * 180.0d / Math.PI;
            if (quadrant == eQuadrant.QUADRANT2 || quadrant == eQuadrant.QUADRANT1)
                result = 360.0d - result;

            return result;
        }

     

        public override object GetResult()
        {
            _result.degreeResult = this._degree;
            return _result;
        }

        public override void SetParam(object param)
        {
            throw new NotImplementedException();
        }

        public override void Transform(eImageTransform type, PointF centerPoint, double rotateAngle = 0)
        {
            base.RotateFlipRect(ref _gridRect, centerPoint.X * 2, centerPoint.Y * 2, type);
            if (type == eImageTransform.CW || type == eImageTransform.CCW)
            {
                this._point1 = base.PointRoation(this._point1, centerPoint, rotateAngle);
                this._point2 = base.PointRoation(this._point2, centerPoint, rotateAngle);
                this._gridCenterPoint = base.PointRoation(this._gridCenterPoint, centerPoint, rotateAngle);
            }
            else if(type == eImageTransform.FlipX)
            {
                this._point1 = base.PointFlipX(this._point1, centerPoint);
                this._point2 = base.PointFlipX(this._point2, centerPoint);
                this._gridCenterPoint = base.PointFlipX(this._gridCenterPoint, centerPoint);
            }
            else if(type == eImageTransform.FlipY)
            {
                this._point1 = base.PointFlipY(this._point1, centerPoint);
                this._point2 = base.PointFlipY(this._point2, centerPoint);
                this._gridCenterPoint = base.PointFlipY(this._gridCenterPoint, centerPoint);
            }
        }

        public override void WriteXml(XmlElement element)
        {
            throw new NotImplementedException();
        }
    }
}
