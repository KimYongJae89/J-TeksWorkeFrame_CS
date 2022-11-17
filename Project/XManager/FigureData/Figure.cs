using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using XManager.Util;

namespace XManager.FigureData
{

    public abstract class Figure : ICloneable
    {
        private bool _isTempFigure = false;
        public bool IsTempFigure
        {
            get { return _isTempFigure; }
            set { _isTempFigure = value; }
        }
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string label;
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        protected eFigureType type;
        public eFigureType Type
        {
            get { return type; }
        }

        private eFigureMode _figureMode = eFigureMode.None;
        public eFigureMode FigureMode
        {
            get { return _figureMode; }
            set { _figureMode = value; }
        }
        private eMouseType _mouseType = eMouseType.Up;
        public eMouseType MouseType
        {
            get { return _mouseType; }
            set { _mouseType = value; }
        }
        private eTrackPosType _trackerPosType = eTrackPosType.None;
        public eTrackPosType TrackerPosType
        {
            get { return _trackerPosType; }
            set { _trackerPosType = value; }
        }
        private bool _selectable = true;
        public bool Selectable
        {
            get { return _selectable; }
            set { _selectable = value; }
        }

        private bool _multiSelectable = true;
        public bool MultiSelectable
        {
            get { return _multiSelectable; }
            set { _multiSelectable = value; }
        }

        private bool _deletable = true;
        public bool Deletable
        {
            get { return _deletable; }
            set { _deletable = value; }
        }

        private bool _visible = true;
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        private PointF _startingMovePoint = new PointF();
        public PointF StartingMovePoint
        {
            get { return _startingMovePoint; }
            set { _startingMovePoint = value; }
        }

        private PointF _movingMovePoint = new PointF();
        public PointF MovingMovePoint
        {
            get { return _movingMovePoint; }
            set { _movingMovePoint = value; }
        }

        private PointF _endMovePoint = new PointF();
        public PointF EndMovePoint
        {
            get { return _endMovePoint; }
            set { _endMovePoint = value; }
        }

        private int _trackRectWidth = 6;
        public int TrackRectWidth
        {
            get { return _trackRectWidth; }
            set { _trackRectWidth = value; }
        }

        List<RectangleF> _trackRectangleList = new List<RectangleF>();
        public List<RectangleF> TrackRectangleList
        {
            get { return _trackRectangleList; }
            set { _trackRectangleList = value; }
        }


        private object _result;
        public object Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public abstract object Clone();
        public abstract void Draw(Graphics g);
        public abstract void TrackRectangleInitialize();
        public abstract List<RectangleF> GetTrackerRectangle();
        public abstract eTrackPosType CheckTrackPos(PointF pt);
        public abstract bool CheckRegionInFigure(PointF startPoint, PointF endPoint);
        public abstract bool CheckPointInFigure(PointF point);
        public abstract void Scale(ImageCoordTransformer coordTransformer);
        public abstract void MouseDown(PointF startTrackPos);
        public abstract void MouseMove(PointF pt);
        public abstract void MouseUp(PointF endTrackPos);
        public abstract object GetResult();
        public abstract void SetParam(object param);
        public abstract void Transform(eImageTransform type, PointF centerPoint, double rotateAngle = 0);
        public abstract void WriteXml(XmlElement element);
        public PointF PointFlipX(PointF sourcePoint, PointF centerPoint)
        {
            PointF targetPoint = new PointF();
            targetPoint = sourcePoint;
            float interval = Math.Abs(sourcePoint.X - centerPoint.X);
            if (sourcePoint.X < centerPoint.X)
            {
                targetPoint.X = centerPoint.X + interval;
            }
            else if(sourcePoint.X == centerPoint.X)
            {

            }
            else
            {
                targetPoint.X = centerPoint.X - interval;
            }
            return targetPoint;
        }
        public PointF PointFlipY(PointF sourcePoint, PointF centerPoint)
        {
            PointF targetPoint = new PointF();
            targetPoint = sourcePoint;
            float interval = Math.Abs(sourcePoint.Y - centerPoint.Y);
            if (sourcePoint.Y < centerPoint.Y)
            {
                targetPoint.Y = centerPoint.Y + interval;
            }
            else if (sourcePoint.X == centerPoint.X)
            {

            }
            else
            {
                targetPoint.Y = centerPoint.Y - interval;
            }
            return targetPoint;
        }
        public PointF PointRoation(PointF sourcePoint, PointF centerPoint, double rotateAngle)
        {
            PointF targetPoint = new PointF();
            double radian = rotateAngle / 180 * Math.PI;

            double newXPos = (Math.Cos(radian) * (sourcePoint.X - centerPoint.X) - Math.Sin(radian) * (sourcePoint.Y - centerPoint.Y) + centerPoint.X);
            double newYPos = (Math.Sin(radian) * (sourcePoint.X - centerPoint.X) + Math.Cos(radian) * (sourcePoint.Y - centerPoint.Y) + centerPoint.Y);
            targetPoint.X = (float)newXPos;
            targetPoint.Y = (float)newYPos;
            return targetPoint;
        }
        public void RotateFlipRect(ref RectangleF rect, float newContainerWidth, float newContainerHeight, eImageTransform type)
        {
            float left = rect.Left;
            float top = rect.Top;
            float right = rect.Bottom;
            float bottom = rect.Bottom;
            switch (type)
            {
                case eImageTransform.CW:
                    left = newContainerWidth - rect.Bottom;
                    top = rect.Left;
                    right = newContainerWidth - rect.Top;
                    bottom = rect.Right;
                    break;
                case eImageTransform.CCW:
                    left = rect.Top;
                    top = newContainerHeight - rect.Right;
                    right = rect.Bottom;
                    bottom = newContainerHeight - rect.Left;
                    break;
                case eImageTransform.FlipX:
                    left = newContainerWidth - rect.Right;
                    top = rect.Top;
                    right = newContainerWidth - rect.Left;
                    bottom = rect.Bottom;
                    break;
                case eImageTransform.FlipY:
                    left = rect.Left;
                    top = newContainerHeight - rect.Bottom;
                    right = rect.Right;
                    bottom = newContainerHeight - rect.Top;
                    break;
            }

            rect = new RectangleF(left, top, right - left, bottom - top);
        }
        public RectangleF CalcPointToRectangleF(PointF p1, PointF p2, bool isSquare = false)
        {
            RectangleF rect = new RectangleF();
            float width = Math.Abs(p1.X - p2.X);
            float height = Math.Abs(p1.Y - p2.Y);
            if (isSquare == true)
            {
                float newLength = 0;
                if (width > height)
                    newLength = height;
                else
                    newLength = width;
                
               if(p1.X > p2.X)
                {
                    if(p1.Y > p2.Y)
                    {
                        rect.X = p2.X;
                        rect.Y = p2.Y;
                    }
                    else
                    {
                        rect.X = p2.X;
                        rect.Y = p2.Y - newLength;
                    }
                }
               else
                {
                    if (p1.Y < p2.Y)
                    {
                        rect.X = p2.X- newLength;
                        rect.Y = p2.Y - newLength;
                    }
                    else
                    {
                        rect.X = p2.X - newLength;
                        rect.Y = p2.Y;
                    }
                }
                rect.Width = newLength;
                rect.Height = newLength;
            }
            else
            {
                if (p1.X > p2.X)
                {
                    if (p1.Y > p2.Y)
                    {
                        rect.X = p2.X;
                        rect.Y = p2.Y;
                    }
                    else
                    {
                        rect.X = p1.X - width;
                        rect.Y = p1.Y;
                    }
                }
                else
                {
                    if (p1.Y < p2.Y)
                    {
                        rect.X = p1.X;
                        rect.Y = p1.Y;
                    }
                    else
                    {
                        rect.X = p2.X - width;
                        rect.Y = p2.Y;
                    }
                }
                rect.Width = width;
                rect.Height = height;
            }
            return rect;
        }
        public double CalcLengthPointToPoint(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2));
        }
        public bool IsOnLine(PointF p1, PointF p2, PointF p, int width = 10)
        {
            var isOnLine = false;
            using (var path = new GraphicsPath())
            {
                using (var pen = new Pen(Brushes.Black, width))
                {
                    path.AddLine(p1, p2);
                    isOnLine = path.IsOutlineVisible(p, pen);
                }
            }
            return isOnLine;
        }

        public PointF GetOrgPoint(PointF point)
        {
            if (CStatus.Instance().GetDrawBox() == null)
                return new PointF(0,0);

            int nowWidth = CStatus.Instance().GetDrawBox().GetPictureBox().Width;
            int nowHeight = CStatus.Instance().GetDrawBox().GetPictureBox().Height;
            int orgWidth = CStatus.Instance().GetDrawBox().ImageManager.GetOrgImage().Width;
            int orgHeight = CStatus.Instance().GetDrawBox().ImageManager.GetOrgImage().Height;

            float newX = point.X * orgWidth / (float)nowWidth;
            float newY = point.Y * orgHeight / (float)nowHeight;

            if (newX < 0)
                newX = 0;
            if (newX >= orgWidth - 1)
                newX = orgWidth;// - 1;
            if (newY < 0)
                newY = 0;
            if (newY >= orgHeight - 1)
                newY = orgHeight;// - 1;

            return new PointF(newX, newY);
        }

        public PointF GetDisplayPoint(PointF point)
        {
            int nowWidth = CStatus.Instance().GetDrawBox().GetPictureBox().Width;
            int nowHeight = CStatus.Instance().GetDrawBox().GetPictureBox().Height;
            int orgWidth = CStatus.Instance().GetDrawBox().ImageManager.GetOrgImage().Width;
            int orgHeight = CStatus.Instance().GetDrawBox().ImageManager.GetOrgImage().Height;

            float newX = point.X * (float)nowWidth / (float)orgWidth;
            float newY = point.Y * (float)nowHeight / (float)orgHeight;

            if (newX < 0)
                newX = 0;
            if (newX >= nowWidth - 1)
                newX = nowWidth;// - 1;
            if (newY < 0)
                newY = 0;
            if (newY >= nowHeight - 1)
                newY = nowHeight;// - 1;

            return new PointF(newX, newY);
        }
    }

 
}
