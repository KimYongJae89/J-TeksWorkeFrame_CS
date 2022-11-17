using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using XManager.Util;

namespace XManager.FigureData
{
    public struct tRectangleResult
    {
        public RectangleF resultRectangle;
    }

    public class RectangleFigure : Figure
    {
       
        private RectangleF _rect = new RectangleF();
        private RectangleF _drawRect = new RectangleF();
        private tRectangleResult _result = new tRectangleResult();
        private PointF fixedPoint = new PointF();

        private RectangleF _leftTopRectangle;
        private RectangleF _leftCenterRectangle;
        private RectangleF _leftBottomRectangle;
        private RectangleF _topCenterRectangle;
        private RectangleF _rightBottomRectangle;
        private RectangleF _rightCenterRectangle;
        private RectangleF _bottomCenterRectangle;
        private RectangleF _rightTopRectangle;

        public RectangleF Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }

        public RectangleFigure()
        {
            type = eFigureType.Rectangle;
          
        }

        public RectangleFigure(PointF startPoint, PointF endPoint, bool isTempRect = false)
        {
            IsTempFigure = isTempRect;
            type = eFigureType.Rectangle;
            _rect = base.CalcPointToRectangleF(startPoint, endPoint);
        }

        public override void TrackRectangleInitialize()
        {
            _leftTopRectangle = new RectangleF(0,0, base.TrackRectWidth, base.TrackRectWidth);
            _leftCenterRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _leftBottomRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _topCenterRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _rightBottomRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _rightCenterRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _bottomCenterRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
            _rightTopRectangle = new RectangleF(0, 0, base.TrackRectWidth, base.TrackRectWidth);
        }

        public override eTrackPosType CheckTrackPos(PointF pt)
        {
            List<RectangleF> editRect = GetTrackerRectangle();
            bool isOnLine = this._rect.Contains(pt);
            foreach (RectangleF rect in editRect)
            {
                bool isContain = rect.Contains(pt);
                if (isContain)
                {
                    if(rect == _leftTopRectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.LeftTop;
                        return eTrackPosType.LeftTop;
                    }
                    else if(rect == _leftCenterRectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.Left;
                        return eTrackPosType.Left;
                    }
                    else if (rect == _leftBottomRectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.LeftBottom;
                        return eTrackPosType.LeftBottom;
                    }
                    else if (rect == _topCenterRectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.Top;
                        return eTrackPosType.Top;
                    }
                    else if (rect == _rightTopRectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.RightTop;
                        return eTrackPosType.RightTop;
                    }
                    else if (rect == _rightCenterRectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.Right;
                        return eTrackPosType.Right;
                    }
                    else if (rect == _rightBottomRectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.RightBottom;
                        return eTrackPosType.RightBottom;
                    }
                    else if (rect == _bottomCenterRectangle)
                    {
                        FigureMode = eFigureMode.Edit;
                        TrackerPosType = eTrackPosType.Bottom;
                        return eTrackPosType.Bottom;
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
            DrawHelper.DrawRectangleF(g, ref _rect, IsTempFigure);
            _drawRect = _rect;

            if (!IsTempFigure)
            {
                string labelText = "Roi_" + this.Id;
                Brush brush = Brushes.Black;
                Font font = new Font("고딕", 7);
                int halfTrackRectWidth = TrackRectWidth / 2;
                SizeF stringSize = g.MeasureString(labelText, font);
                RectangleF labelRect = new RectangleF(_drawRect.X + halfTrackRectWidth + 1, _drawRect.Y - stringSize.Height, stringSize.Width, stringSize.Height);
                g.FillRectangle(Brushes.White, labelRect);
                g.DrawString(labelText, font, brush, new PointF(labelRect.X, labelRect.Y));
            }

            if (this.Selectable && !IsTempFigure)
            {
                List<RectangleF> rectangles = GetTrackerRectangle();
                foreach (RectangleF rect in rectangles)
                {
                    g.FillRectangle(Brushes.White, rect);
                }
            }
        }

        public override List<RectangleF> GetTrackerRectangle()
        {
            if (base.TrackRectangleList.Count > 0)
                base.TrackRectangleList.Clear();

            TrackRectangleInitialize();

            int halfTrackRectWidth = TrackRectWidth / 2;

            _leftTopRectangle.Location = new PointF(this._drawRect.X - halfTrackRectWidth, this._drawRect.Y - halfTrackRectWidth);
            _leftCenterRectangle.Location = new PointF(this._drawRect.X - halfTrackRectWidth, this._drawRect.Y + (this._drawRect.Height / 2) - halfTrackRectWidth);
            _leftBottomRectangle.Location = new PointF(this._drawRect.X - halfTrackRectWidth, this._drawRect.Y + this._drawRect.Height - halfTrackRectWidth);

            _rightTopRectangle.Location = new PointF(this._drawRect.X + this._drawRect.Width - halfTrackRectWidth, this._drawRect.Y - halfTrackRectWidth);
            _rightCenterRectangle.Location = new PointF(this._drawRect.X + this._drawRect.Width - halfTrackRectWidth, this._drawRect.Y + this._drawRect.Height / 2 - halfTrackRectWidth);
            _rightBottomRectangle.Location = new PointF(this._drawRect.X + this._drawRect.Width - halfTrackRectWidth, this._drawRect.Y + this._drawRect.Height - halfTrackRectWidth);

            _topCenterRectangle.Location = new PointF(this._drawRect.X + this._drawRect.Width / 2 - halfTrackRectWidth, this._drawRect.Y - (TrackRectWidth / 2));
            _bottomCenterRectangle.Location = new PointF(this._drawRect.X + this._drawRect.Width / 2 - halfTrackRectWidth, this._drawRect.Y + this._drawRect.Height - halfTrackRectWidth);

            base.TrackRectangleList.Add(_leftTopRectangle);
            base.TrackRectangleList.Add(_leftCenterRectangle);
            base.TrackRectangleList.Add(_leftBottomRectangle);

            base.TrackRectangleList.Add(_rightTopRectangle);
            base.TrackRectangleList.Add(_rightCenterRectangle);
            base.TrackRectangleList.Add(_rightBottomRectangle);

            base.TrackRectangleList.Add(_topCenterRectangle);
            base.TrackRectangleList.Add(_bottomCenterRectangle);

            return base.TrackRectangleList;
        }
       
        public override void MouseDown(PointF startTrackPos)
        {
            StartingMovePoint = startTrackPos;
            MouseType = eMouseType.Down;
            CalcFixPoint();
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
                        _rect = base.CalcPointToRectangleF(MovingMovePoint, fixedPoint);
                    }
                    else if (TrackerPosType == eTrackPosType.Left)
                    {
                        PointF movePoint = new PointF(MovingMovePoint.X, fixedPoint.Y - _rect.Height);
                        _rect = base.CalcPointToRectangleF(movePoint, fixedPoint);
                    }
                    else if (TrackerPosType == eTrackPosType.Right)
                    {
                        PointF movePoint = new PointF(MovingMovePoint.X, fixedPoint.Y + _rect.Height);
                        _rect = base.CalcPointToRectangleF(movePoint, fixedPoint);
                    }
                    else if (TrackerPosType == eTrackPosType.Top)
                    {
                        PointF movePoint = new PointF(fixedPoint.X - _rect.Width, MovingMovePoint.Y);
                        _rect = base.CalcPointToRectangleF(movePoint, fixedPoint);
                    }
                    else if (TrackerPosType == eTrackPosType.Bottom)
                    {
                        PointF movePoint = new PointF(fixedPoint.X + _rect.Width, MovingMovePoint.Y);
                        _rect = base.CalcPointToRectangleF(movePoint, fixedPoint);
                    }
                    else if (TrackerPosType == eTrackPosType.Inside)
                    {
                        float moveX = StartingMovePoint.X - MovingMovePoint.X;
                        float moveY = StartingMovePoint.Y - MovingMovePoint.Y;
                        //float leftTopNewX = _rect.X - moveX;
                        //float leftTopNewY = _rect.Y - moveY;
                        //PointF leftTopPoint = new PointF(leftTopNewX, leftTopNewY);
                        //DrawHelper.CalibrationPointF(ref leftTopPoint);

                        //float rightBottomNewX = _rect.X + _rect.Width - moveX;
                        //float rightBottomNewY = _rect.Y + _rect.Height - moveY;
                        //PointF rightBottomPoint = new PointF(rightBottomNewX, rightBottomNewY);
                        //DrawHelper.CalibrationPointF(ref rightBottomPoint);

                        //_rect.X = leftTopPoint.X;
                        //_rect.Y = leftTopPoint.Y;
                        //_rect.Width = Math.Abs(leftTopPoint.X - rightBottomPoint.X);
                        //_rect.Height = Math.Abs(leftTopPoint.Y - rightBottomPoint.Y);

                        _rect.X -= moveX;
                        _rect.Y -= moveY;
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
            _rect.X = _rect.X * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _rect.Y = _rect.Y * coordTransformer.NewHeight / coordTransformer.OrgHeight;

            _rect.Width = _rect.Width * coordTransformer.NewWidth / coordTransformer.OrgWidth;
            _rect.Height = _rect.Height * coordTransformer.NewHeight / coordTransformer.OrgHeight;
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

        private void CalcFixPoint()
        {
            if (TrackerPosType == eTrackPosType.LeftTop)
            {
                fixedPoint.X = _rect.X + _rect.Width;
                fixedPoint.Y = _rect.Y + _rect.Height;
            }
            else if (TrackerPosType == eTrackPosType.RightBottom)
            {
                fixedPoint.X = _rect.X;
                fixedPoint.Y = _rect.Y;
            }
            else if(TrackerPosType == eTrackPosType.Left)
            {
                fixedPoint.X = _rect.X + _rect.Width;
                fixedPoint.Y = _rect.Y + _rect.Height;
            }
            else if(TrackerPosType == eTrackPosType.Right)
            {
                fixedPoint.X = _rect.X;
                fixedPoint.Y = _rect.Y;
            }
            else if (TrackerPosType == eTrackPosType.Top)
            {
                fixedPoint.X = _rect.X + _rect.Width;
                fixedPoint.Y = _rect.Y + _rect.Height;
            }
            else if (TrackerPosType == eTrackPosType.Bottom)
            {
                fixedPoint.X = _rect.X;
                fixedPoint.Y = _rect.Y;
            }
            else if (TrackerPosType == eTrackPosType.LeftBottom)
            {
                fixedPoint.X = _rect.X + _rect.Width;
                fixedPoint.Y = _rect.Y;
            }
            else if (TrackerPosType == eTrackPosType.RightTop)
            {
                fixedPoint.X = _rect.X;
                fixedPoint.Y = _rect.Y + _rect.Height;
            }
        }

        public override bool CheckPointInFigure(PointF point)
        {
            bool ret = _rect.Contains(point);
            return ret;
        }

        public override object GetResult()
        {
            PointF leftTop = CStatus.Instance().GetDrawBox().ImageManager.GetOrgPoint(new PointF(_rect.X, _rect.Y));
            PointF rightBottom = CStatus.Instance().GetDrawBox().ImageManager.GetOrgPoint(new PointF(_rect.X + _rect.Width, _rect.Y + _rect.Height));
            Point orgLeftTop = new Point((int)leftTop.X, (int)leftTop.Y);
            PointF orgRightBottom = new PointF((int)rightBottom.X, (int)rightBottom.Y);
            int orgWidth = (int)Math.Abs(orgLeftTop.X - orgRightBottom.X);
            int orgHeight = (int)Math.Abs(orgLeftTop.Y - orgRightBottom.Y);

            float pictureBoxWidth = (float)CStatus.Instance().GetDrawBox().GetPictureBox().Width;
            float pictureBoxHeight = (float)CStatus.Instance().GetDrawBox().GetPictureBox().Height;

            RectangleF resultRect = new RectangleF(leftTop.X, leftTop.Y, orgWidth, orgHeight);
            _result.resultRectangle = resultRect;

            return _result;
        }

        public override void SetParam(object param)
        {
            
        }

        public override void Transform(eImageTransform type, PointF centerPoint, double rotateAngle = 0)
        {
            base.RotateFlipRect(ref _rect, centerPoint.X * 2, centerPoint.Y * 2, type);
        }

        public override void WriteXml(XmlElement element)
        {
            //XmlHelper.SetValue(element, "Id", this.Id);
            //XmlHelper.SetValue(element, "Left", _rect.Left.ToString());
            //XmlHelper.SetValue(element, "Top", _rect.Top.ToString());
            //XmlHelper.SetValue(element, "Right", _rect.Right.ToString());
            //XmlHelper.SetValue(element, "Bottom", _rect.Bottom.ToString());
        }
    }
}
