using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;
using XManager.Util;
using KiyLib.DIP;

namespace XManager.FigureData
{
    public struct tProfileResult
    {
        public bool isProJection;
        public bool ProjectionHeight;
        public PointF StartPoint;
        public PointF EndPoint;
        public PointF Mark1Point;
        public PointF Mark2Point;
        public int Mark1;
        public int Mark2;
    }

    public struct tProfileParams
    {
        public int Mark1;
        public int Mark2;
        public List<PointF> ProfilePoints;
    }

    public class ProfileFigure : Figure //  : LineFigure
    {
        private float _lineProfileHeight = 50;
        private int _mark1;
        private int _mark2;
        private PointF _mark1Point;
        private PointF _mark2Point;

        private PointF _minGirdPoint1 = new PointF();
        private PointF _minGirdPoint2 = new PointF();
        private PointF _maxGirdPoint1 = new PointF();
        private PointF _maxGirdPoint2 = new PointF();

        public PointF DrawLineStartPoint = new PointF();
        public PointF DrawLineEndPoint = new PointF();

        private PointF _startPoint = new PointF();
        private PointF _endPoint = new PointF();

        private RectangleF _startRectangle;
        private RectangleF _endRectangle;

        private List<PointF> _profilePoints = new List<PointF>();
        public ProfileFigure(PointF startPoint, PointF endPoint) //: base(startPoint, endPoint)
        {
            type = eFigureType.Profile;
            _startPoint = startPoint;
            _endPoint = endPoint;
            GetCalcPointListOfLine(_startPoint, _endPoint, true);
        }

        private void GetCalcPointListOfLine(PointF startPoint, PointF endPoint, bool isInitialize = false)
        {
            DrawHelper.InspectPoint(ref startPoint, ref endPoint);
            PointF orgStartPointF = base.GetOrgPoint(startPoint);
            PointF orgEndPointF = base.GetOrgPoint(endPoint);
            float width = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width - 1;
            float height = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height - 1;

            if (orgEndPointF.X >= width) orgEndPointF.X = width;
            if (orgEndPointF.Y >= height) orgEndPointF.Y = height;

            if (orgStartPointF.X >= width) orgStartPointF.X = width;
            if (orgStartPointF.Y >= height) orgStartPointF.Y = height;

            List<Point> pts = KLine.GetPointListOfLine(new Point((int)orgStartPointF.X, (int)orgStartPointF.Y), new Point((int)orgEndPointF.X, (int)orgEndPointF.Y), LineProFileAlgo.DDA);

            this._profilePoints.Clear();

            for (int i = 0; i < pts.Count(); i++)
            {
                PointF calcPoint = DrawHelper.ProfileCalibrationPointF(pts[i]);
                if (i == 0)
                {
                    DrawLineStartPoint = calcPoint;
                    if (isInitialize)
                        _mark1 = 0;
                }
                if (i == pts.Count() - 1)
                {
                    DrawLineEndPoint = calcPoint;
                    if (isInitialize)
                        _mark2 = pts.Count() - 1;
                }
                _profilePoints.Add(calcPoint);
            }
        }

        public override void Draw(Graphics g)
        {
            try
            {
                GetCalcPointListOfLine(_startPoint, _endPoint);

                DrawHelper.DrawLinesInList(g, ref this._profilePoints);

                DrawLineStartPoint = this._profilePoints[0];
                DrawLineEndPoint = this._profilePoints[this._profilePoints.Count() - 1];

                DrawHelper.CalibrationPointF(ref DrawLineStartPoint);
                DrawHelper.CalibrationPointF(ref DrawLineEndPoint);

                DrawProfileLine(g);

                if (this.Selectable)
                {
                    List<RectangleF> rectangles = GetTrackerRectangle();
                    foreach (RectangleF rect in base.TrackRectangleList)
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
        private void DrawStringLabel(Graphics g)
        {
            string labelText = "P.F_" + this.Id;
            Brush brush = Brushes.Black;
            Font font = new Font("고딕", 7);
            int halfTrackRectWidth = TrackRectWidth / 2;
            SizeF stringSize = g.MeasureString(labelText, font);
            RectangleF labelRect = new RectangleF(DrawLineStartPoint.X + halfTrackRectWidth + 1, DrawLineStartPoint.Y - stringSize.Height, stringSize.Width, stringSize.Height);
            g.FillRectangle(Brushes.White, labelRect);
            g.DrawString(labelText, font, brush, new PointF(labelRect.X, labelRect.Y));
        }

        public override List<RectangleF> GetTrackerRectangle()
        {
            if (base.TrackRectangleList.Count > 0)
                base.TrackRectangleList.Clear();

            TrackRectangleInitialize();

            int halfTrackRectWidth = TrackRectWidth / 2;

            _startRectangle.Location = new PointF(DrawLineStartPoint.X - halfTrackRectWidth, DrawLineStartPoint.Y - halfTrackRectWidth);
            _endRectangle.Location = new PointF(DrawLineEndPoint.X - halfTrackRectWidth, DrawLineEndPoint.Y - halfTrackRectWidth);

            base.TrackRectangleList.Add(_startRectangle);
            base.TrackRectangleList.Add(_endRectangle);

            return base.TrackRectangleList;
        }

        private void DrawProfileLine(Graphics g)
        {
            DrawCircle(g);
            DrawMarkGrid(g);

            //if (this.Selectable && Status.Instance().ProfileWidth != 1)

            if (this.Selectable && CStatus.Instance().ProfileWidth != 1)
            {
                PointF orgStartPt = base.GetOrgPoint(this.DrawLineStartPoint);
                PointF orgEndPt = base.GetOrgPoint(this.DrawLineEndPoint);
                List<Point> realPts = KLineProfile.GetProjectionRectPoints((int)orgStartPt.X, (int)orgStartPt.Y, (int)orgEndPt.X, (int)orgEndPt.Y, CStatus.Instance().ProfileWidth);
                List<PointF> calcPts = new List<PointF>();

                foreach (Point point in realPts)
                {
                    PointF calcPoint = DrawHelper.ProfileCalibrationPointF(new PointF((float)point.X, (float)point.Y));

                    calcPts.Add(calcPoint);
                }

                Pen pen = new Pen(Color.White, 2);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                for (int i = 0; i < calcPts.Count; i++)
                {
                    if (i + 1 >= calcPts.Count)
                        g.DrawLine(pen, calcPts[i], calcPts[0]);
                    else
                        g.DrawLine(pen, calcPts[i], calcPts[i + 1]);
                }
            }
        }
        private void DrawCircle(Graphics g)
        {
            float width = 10;
            PointF CenterPoint = DrawLineStartPoint;

            DrawHelper.CalibrationPointF(ref CenterPoint);

            RectangleF rect = new RectangleF(CenterPoint.X - (width / 2), CenterPoint.Y - (width / 2), width, width);
            g.DrawEllipse(new Pen(Color.Yellow), rect);
        }

        private void DrawMarkGrid(Graphics g)
        {
            try
            {
                float inclination = CalcInclination(this.DrawLineStartPoint, this.DrawLineEndPoint);//기울기
                float newInclination = -1 / inclination;//직각 기울기
                float tan = Math.Abs(newInclination); // 기울기 = tan
                double setha = Math.Atan((double)tan); // 라디안

                if (this._mark1 < 0) this._mark1 = 0;
                if (this._mark1 >= this._profilePoints.Count() - 1) this._mark1 = this._profilePoints.Count() - 1;

                if (this._mark2 < 0) this._mark2 = 0;
                if (this._mark2 >= this._profilePoints.Count() - 1) this._mark2 = this._profilePoints.Count() - 1;

                PointF mark1CenterPoint = this._profilePoints[this._mark1];
                PointF mark2CenterPoint = this._profilePoints[this._mark2];

                this._mark1Point = mark1CenterPoint;
                this._mark2Point = mark2CenterPoint;

                PointF Mark1GridPoint1 = new PointF();
                PointF Mark1GridPoint2 = new PointF();
                PointF Mark2GridPoint1 = new PointF();
                PointF Mark2GridPoint2 = new PointF();

                PointF stringPosition1 = new PointF();
                PointF stringPosition2 = new PointF();

                float girdWidth = 50;
                float stringPosWidth = 30;

                if (newInclination > 0)
                {
                    Mark1GridPoint1.X = mark1CenterPoint.X - (float)Math.Cos(setha) * (float)girdWidth;
                    Mark1GridPoint1.Y = mark1CenterPoint.Y - (float)Math.Sin(setha) * (float)girdWidth;
                    Mark1GridPoint2.X = mark1CenterPoint.X + (float)Math.Cos(setha) * (float)girdWidth;
                    Mark1GridPoint2.Y = mark1CenterPoint.Y + (float)Math.Sin(setha) * (float)girdWidth;

                    Mark2GridPoint1.X = mark2CenterPoint.X - (float)Math.Cos(setha) * (float)girdWidth;
                    Mark2GridPoint1.Y = mark2CenterPoint.Y - (float)Math.Sin(setha) * (float)girdWidth;
                    Mark2GridPoint2.X = mark2CenterPoint.X + ((float)Math.Cos(setha) * (float)girdWidth);
                    Mark2GridPoint2.Y = mark2CenterPoint.Y + ((float)Math.Sin(setha) * (float)girdWidth);
                    //
                    stringPosition1.X = mark1CenterPoint.X - (float)Math.Cos(setha) * (float)stringPosWidth;
                    stringPosition1.Y = mark1CenterPoint.Y - (float)Math.Sin(setha) * (float)stringPosWidth;

                    stringPosition2.X = mark2CenterPoint.X - (float)Math.Cos(setha) * (float)stringPosWidth;
                    stringPosition2.Y = mark2CenterPoint.Y - (float)Math.Sin(setha) * (float)stringPosWidth;
                }
                else
                {
                    Mark1GridPoint1.X = mark1CenterPoint.X + (float)Math.Cos(setha) * (float)girdWidth;
                    Mark1GridPoint1.Y = mark1CenterPoint.Y - (float)Math.Sin(setha) * (float)girdWidth;
                    Mark1GridPoint2.X = mark1CenterPoint.X - (float)Math.Cos(setha) * (float)girdWidth;
                    Mark1GridPoint2.Y = mark1CenterPoint.Y + (float)Math.Sin(setha) * (float)girdWidth;

                    Mark2GridPoint1.X = mark2CenterPoint.X + (float)Math.Cos(setha) * (float)girdWidth;
                    Mark2GridPoint1.Y = mark2CenterPoint.Y - (float)Math.Sin(setha) * (float)girdWidth;
                    Mark2GridPoint2.X = mark2CenterPoint.X - ((float)Math.Cos(setha) * (float)girdWidth);
                    Mark2GridPoint2.Y = mark2CenterPoint.Y + ((float)Math.Sin(setha) * (float)girdWidth);
                    //
                    stringPosition1.X = mark1CenterPoint.X + (float)Math.Cos(setha) * (float)stringPosWidth;
                    stringPosition1.Y = mark1CenterPoint.Y - (float)Math.Sin(setha) * (float)stringPosWidth;

                    stringPosition2.X = mark2CenterPoint.X + (float)Math.Cos(setha) * (float)stringPosWidth;
                    stringPosition2.Y = mark2CenterPoint.Y - (float)Math.Sin(setha) * (float)stringPosWidth;
                }
                Pen pen = new Pen(Color.Yellow);

                g.DrawLine(pen, Mark1GridPoint1, Mark1GridPoint2);
                g.DrawLine(pen, Mark2GridPoint1, Mark2GridPoint2);

                g.DrawLine(pen, stringPosition1, stringPosition2);
                g.DrawLine(pen, stringPosition1, stringPosition2);
                double length = 0;
                PointF orgStartPt = base.GetOrgPoint(mark1CenterPoint);
                PointF orgEndPt = base.GetOrgPoint(mark2CenterPoint);

                length = CStatus.Instance().GetDistance(orgStartPt, orgEndPt);

                string labelText = length.ToString() + "mm";
                Brush brush = Brushes.Black;
                Font font = new Font("고딕", 7);
                int halfTrackRectWidth = TrackRectWidth / 2;
                float XPos = (stringPosition1.X + stringPosition2.X) / 2;
                float YPos = (stringPosition1.Y + stringPosition2.Y) / 2;
                SizeF stringSize = g.MeasureString(labelText, font);

                RectangleF labelRect = new RectangleF(XPos - stringSize.Width / 2, YPos - stringSize.Height / 2, stringSize.Width, stringSize.Height);
                g.FillRectangle(Brushes.White, labelRect);
                g.DrawString(labelText, font, brush, new PointF(labelRect.X, labelRect.Y));
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }
        /// <summary>
        /// P1 과 P2 사이의 기울기 출력
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private float CalcInclination(PointF p1, PointF p2)
        {
            float inclination = 0;
            float xIncrease = p1.X - p2.X;
            float yIncrease = p1.Y - p2.Y;

            inclination = yIncrease / xIncrease;

            return inclination;
        }
        /// <summary>
        /// LeftTop에서 value만큼 떨어진 Point 구하기
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private PointF CalcValueToProfilePoint(float value)
        {
            PointF ret = new PointF();
            PointF leftPoint = new PointF();
            PointF rightPoint = new PointF();

            leftPoint = this.DrawLineStartPoint;
            rightPoint = this.DrawLineEndPoint;

            double startToEndLength = base.CalcLengthPointToPoint(leftPoint, rightPoint);
            double xInterval = Math.Abs(leftPoint.X - rightPoint.X);
            double cos = (rightPoint.X - leftPoint.X) / startToEndLength;
            double sin = (rightPoint.Y - leftPoint.Y) / startToEndLength;

            ret.X = leftPoint.X + (float)(value * cos);
            ret.Y = leftPoint.Y + (float)(value * sin);

            return ret;
        }
        public override void SetParam(object param)
        {
            tProfileParams markParams = (tProfileParams)param;
            this._mark1 = markParams.Mark1;
            this._mark2 = markParams.Mark2;
            this._profilePoints = markParams.ProfilePoints;
        }


        public override object GetResult()
        {
            tProfileResult param = new tProfileResult();
            DrawHelper.InspectPoint(ref this._startPoint, ref this._endPoint);
            param.StartPoint = this._startPoint;
            param.EndPoint = this._endPoint;
            param.Mark1 = _mark1;
            param.Mark2 = _mark2;
            param.Mark1Point = _mark1Point;
            param.Mark2Point = _mark2Point;
            return param;
        }

        public override void Scale(ImageCoordTransformer coordTransformer)
        {
            List<PointF> tempPoint = new List<PointF>();
            tempPoint.AddRange(this._profilePoints);
            this._profilePoints.Clear();
            foreach (PointF pt in tempPoint)
            {
                PointF calcPoint = new PointF();
                calcPoint.X = pt.X * (float)coordTransformer.NewWidth / (float)coordTransformer.OrgWidth;
                calcPoint.Y = pt.Y * (float)coordTransformer.NewHeight / (float)coordTransformer.OrgHeight;
                this._profilePoints.Add(calcPoint);
            }
            _startPoint.X = _startPoint.X * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _startPoint.Y = _startPoint.Y * coordTransformer.NewHeight / coordTransformer.OrgHeight;

            _endPoint.X = _endPoint.X * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _endPoint.Y = _endPoint.Y * coordTransformer.NewHeight / coordTransformer.OrgHeight;

            _minGirdPoint1.X = _minGirdPoint1.X * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _minGirdPoint1.Y = _minGirdPoint1.Y * coordTransformer.NewHeight / coordTransformer.OrgHeight;

            _minGirdPoint2.X = _minGirdPoint2.X * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _minGirdPoint2.Y = _minGirdPoint2.Y * coordTransformer.NewHeight / coordTransformer.OrgHeight;

            _maxGirdPoint1.X = _maxGirdPoint1.X * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _maxGirdPoint1.Y = _maxGirdPoint1.Y * coordTransformer.NewHeight / coordTransformer.OrgHeight;

            _maxGirdPoint2.X = _maxGirdPoint2.X * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _maxGirdPoint2.Y = _maxGirdPoint2.Y * coordTransformer.NewHeight / coordTransformer.OrgHeight;

            _lineProfileHeight = (float)base.CalcLengthPointToPoint(_minGirdPoint1, _minGirdPoint2) / 2;
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
                    else { }
                }
            }
        }

        public override void Transform(eImageTransform type, PointF centerPoint, double rotateAngle = 0)
        {
            if (type == eImageTransform.CW || type == eImageTransform.CCW)
            {
                this._startPoint = base.PointRoation(this._startPoint, centerPoint, rotateAngle);
                this._endPoint = base.PointRoation(this._endPoint, centerPoint, rotateAngle);
            }
            if (type == eImageTransform.FlipX)
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

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void TrackRectangleInitialize()
        {
            _startRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _endRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
        }

        public override eTrackPosType CheckTrackPos(PointF pt)
        {
            List<RectangleF> editRect = GetTrackerRectangle();
            bool isOnLine = CheckPointInFigure(pt);
            foreach (RectangleF rect in editRect)
            {
                bool isContain = rect.Contains(pt);
                if (isContain)
                {
                    float centerX = rect.X + rect.Width / 2;
                    float centerY = rect.Y + rect.Height / 2;
                    PointF rectCenter = new PointF(centerX, centerY);
                    if (rectCenter == DrawLineStartPoint || (rectCenter == DrawLineStartPoint && isOnLine))
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.LeftTop;
                        return eTrackPosType.LeftTop;
                    }
                    if (rectCenter == DrawLineEndPoint || (rectCenter == DrawLineEndPoint && isOnLine))
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

        public override bool CheckPointInFigure(PointF point)
        {
            PointF calcPoint = point;
            DrawHelper.CalibrationPointF(ref calcPoint);


            float displayImageWidth = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width;
            float displayImageHeight = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height;
            float pictureBoxWidth = CStatus.Instance().GetDrawBox().GetPictureBox().Width;
            float pictureBoxHeight = CStatus.Instance().GetDrawBox().GetPictureBox().Height;

            float widthScale = (pictureBoxWidth / displayImageWidth) * 5;
            float heightScale = (pictureBoxHeight / displayImageHeight) * 5;

            RectangleF pixelRect = new RectangleF(calcPoint.X - (widthScale / 2), calcPoint.Y - (heightScale / 2), widthScale, heightScale);
            for (int i = 0; i < this._profilePoints.Count(); i++)
            {
                if (pixelRect.Contains(this._profilePoints[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public override void MouseDown(PointF startTrackPos)
        {
            MouseType = eMouseType.Down;
            StartingMovePoint = startTrackPos;
        }

        public override void MouseUp(PointF endTrackPos)
        {
            MouseType = eMouseType.Up;
            TrackerPosType = eTrackPosType.None;
            FigureMode = eFigureMode.None;
        }

        public override void WriteXml(XmlElement element)
        {
            throw new NotImplementedException();
        }
    }
}
