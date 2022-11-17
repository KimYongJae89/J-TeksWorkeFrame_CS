using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using XManager.Util;

namespace XManager.FigureData
{
    public struct tLineResult
    {
        public PointF resultStartPoint;
        public PointF resultEndPoint;
    }
    public class LineFigure : Figure
    {
        PointF _drawLineStartPoint = new PointF();
        PointF _drawLineEndPoint = new PointF();
        tLineResult _result = new tLineResult();
        PointF _startPoint = new PointF();
        public PointF StartPoint
        {
            get { return _startPoint; }
            set { _startPoint = value; }
        }

        PointF _endPoint = new PointF();
        public PointF EndPoint
        {
            get { return _endPoint; }
            set { _endPoint = value; }
        }

        private RectangleF _startRectangle;
        private RectangleF _endRectangle;
        public LineFigure()
        {
            type = eFigureType.Line;
        }

        public LineFigure(PointF startPoint, PointF endPoint)
        {
            type = eFigureType.Line;
            float length1 = startPoint.X * startPoint.Y;
            float length2 = endPoint.X * endPoint.Y;
            if(startPoint.X < endPoint.X || startPoint.X == endPoint.X)
            {
                this._startPoint = startPoint;
                this._endPoint = endPoint;
            }
            else
            {
                this._startPoint = endPoint;
                this._endPoint = startPoint;
            }
        }

        public override void TrackRectangleInitialize()
        {
            _startRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _endRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.Yellow);
            
            DrawHelper.DrawLine(g, ref this._startPoint, ref this._endPoint);
            _drawLineStartPoint = this._startPoint;
            _drawLineEndPoint = this._endPoint;
            
            if (type == eFigureType.Line)
            {
                double length = 0;
                PointF orgStartPt = base.GetOrgPoint(_drawLineStartPoint);
                PointF orgEndPt = base.GetOrgPoint(_drawLineEndPoint);

                length = CStatus.Instance().GetDistance(orgStartPt, orgEndPt);

                string labelText = length.ToString() + "mm";
                Brush brush = Brushes.Black;
                Font font = new Font("고딕", 7);
                int halfTrackRectWidth = TrackRectWidth / 2;
                SizeF stringSize = g.MeasureString(labelText, font);
                RectangleF labelRect = new RectangleF(_drawLineStartPoint.X + halfTrackRectWidth + 1, _drawLineStartPoint.Y - stringSize.Height, stringSize.Width, stringSize.Height);
                g.FillRectangle(Brushes.White, labelRect);
                g.DrawString(labelText, font, brush, new PointF(labelRect.X, labelRect.Y));

            }

            if (this.Selectable)
            {
                List<RectangleF> rectangles = GetTrackerRectangle();
                foreach (RectangleF rect in rectangles)
                {
                    g.FillRectangle(Brushes.White, rect);
                }
            }
        }
        private double CalcLength()
        {
            PointF orgStartPt = base.GetOrgPoint(this._startPoint);
            PointF orgEndPt = base.GetOrgPoint(this._endPoint);

            double pitchX = (double)CStatus.Instance().Settings.FovWidth / (double)CStatus.Instance().GetDrawBox().ImageManager.GetOrgImage().Width;
            double pitchY = (double)CStatus.Instance().Settings.FovHeight / (double)CStatus.Instance().GetDrawBox().ImageManager.GetOrgImage().Height;

            double lengthX = Math.Abs(orgStartPt.X - orgEndPt.X) * pitchX;
            double lengthY = Math.Abs(orgStartPt.Y - orgEndPt.Y) * pitchY;
            double temp = Math.Pow(lengthX, 2) + Math.Pow(lengthY, 2);
            double length = Math.Sqrt(temp);
            
            return Math.Round(length, 2);
        }

      

        public override List<RectangleF> GetTrackerRectangle()
        {
            if (base.TrackRectangleList.Count > 0)
                base.TrackRectangleList.Clear();

            TrackRectangleInitialize();

            int halfTrackRectWidth = TrackRectWidth / 2;

            _startRectangle.Location = new PointF(_drawLineStartPoint.X - halfTrackRectWidth, _drawLineStartPoint.Y - halfTrackRectWidth);
            _endRectangle.Location = new PointF(_drawLineEndPoint.X - halfTrackRectWidth, _drawLineEndPoint.Y - halfTrackRectWidth);

            base.TrackRectangleList.Add(_startRectangle);
            base.TrackRectangleList.Add(_endRectangle);

            return base.TrackRectangleList;
        }

        public override void Scale(ImageCoordTransformer coordTransformer)
        {
            _startPoint.X = _startPoint.X * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _startPoint.Y = _startPoint.Y * coordTransformer.NewHeight / coordTransformer.OrgHeight;

            _endPoint.X = _endPoint.X * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _endPoint.Y = _endPoint.Y * coordTransformer.NewHeight / coordTransformer.OrgHeight;
        }

        public override void MouseDown(PointF startTrackPos)
        {
            MouseType = eMouseType.Down;
            StartingMovePoint = startTrackPos;
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
                    if (TrackerPosType == eTrackPosType.RightBottom)
                    { 
                        _endPoint = pt;
                    }
                    else if (TrackerPosType == eTrackPosType.LeftTop)
                    {
                        _startPoint = pt;
                    }
                    else if (TrackerPosType == eTrackPosType.Inside)
                    {
                        float moveX = StartingMovePoint.X - MovingMovePoint.X;
                        float moveY = StartingMovePoint.Y - MovingMovePoint.Y;

                        _startPoint.X -= moveX;
                        _startPoint.Y -= moveY;
                        _endPoint.X -= moveX;
                        _endPoint.Y -= moveY;

                        EndMovePoint = MovingMovePoint;
                        StartingMovePoint = EndMovePoint;
                    }
                    else
                    {
                    }
                }
            }
        }

        public override void MouseUp(PointF endTrackPos)
        {
            MouseType = eMouseType.Up;
            TrackerPosType = eTrackPosType.None;
            FigureMode = eFigureMode.None;
        }

        public override eTrackPosType CheckTrackPos(PointF pt)
        {
            List<RectangleF> editRect = GetTrackerRectangle();
            bool isOnLine = base.IsOnLine(_startPoint, _endPoint, pt);
            foreach (RectangleF rect in editRect)
            {
                bool isContain = rect.Contains(pt);
                if (isContain)
                {
                    float centerX = rect.X + rect.Width / 2;
                    float centerY = rect.Y + rect.Height / 2;
                    PointF rectCenter = new PointF(centerX, centerY);
                    if (rectCenter == _startPoint || (rectCenter == _startPoint && isOnLine))
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.LeftTop;
                        return eTrackPosType.LeftTop;
                    }
                    if (rectCenter == _endPoint || (rectCenter == _endPoint && isOnLine))
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.RightBottom;
                        return eTrackPosType.RightBottom;
                    }
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

        public override bool CheckRegionInFigure(PointF startPoint, PointF endPoint)
        {

            RectangleF rect = base.CalcPointToRectangleF(startPoint, endPoint);
            if(rect.Width == 0 || rect.Height == 0)
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

       

        public override bool CheckPointInFigure(PointF point)
        {
            //Console.WriteLine("Start : " + _startPoint.X.ToString() + "  " + _startPoint.Y.ToString() + "  End : " + _endPoint.X.ToString() + "  " + _endPoint.Y.ToString() + "  Point : " + point.X.ToString() + "  " + point.Y.ToString());
            bool ret = base.IsOnLine(_startPoint, _endPoint, point);
            return ret;
        }

        public override object GetResult()
        {
            _result.resultStartPoint = this._startPoint;
            _result.resultEndPoint = this._endPoint;

            return _result;
        }

        public override void SetParam(object param)
        {
        }

        public override void Transform(eImageTransform type, PointF centerPoint, double rotateAngle = 0)
        {
            if(type == eImageTransform.CW || type == eImageTransform.CCW)
            {
                this._startPoint = base.PointRoation(this._startPoint, centerPoint, rotateAngle);
                this._endPoint = base.PointRoation(this._endPoint, centerPoint, rotateAngle);
            }
            if(type == eImageTransform.FlipX)
            {
                this._startPoint = base.PointFlipX(this._startPoint, centerPoint);
                this._endPoint = base.PointFlipX(this._endPoint, centerPoint);
            }
            if (type == eImageTransform.FlipY)
            {
                this._startPoint = base.PointFlipY(this._startPoint, centerPoint);
                this._endPoint = base.PointFlipY(this._endPoint, centerPoint);
            }
        }

        public override void WriteXml(XmlElement element)
        {
            throw new NotImplementedException();
        }
    }
}
