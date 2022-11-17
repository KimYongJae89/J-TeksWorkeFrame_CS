using ImageManipulator.Util;
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

namespace ImageManipulator.Data
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
        /// <summary>
        /// 복제한다.
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();
        /// <summary>
        /// Figure를 그린다.
        /// </summary>
        /// <param name="g"></param>
        public abstract void Draw(Graphics g);
        /// <summary>
        /// Figure가 선택됬을때 선택모양(사각형)을 그릴 위치 초기화
        /// </summary>
        public abstract void TrackRectangleInitialize();
        /// <summary>
        /// Figure가 선택됬을때 선택모양(사각형) 가져오기
        /// </summary>
        /// <returns>Rectangle 리스트</returns>
        public abstract List<RectangleF> GetTrackerRectangle();
        /// <summary>
        /// 해당 위치의 커서 모양을 가져온다.
        /// </summary>
        /// <param name="pt">Point</param>
        /// <returns>커서 모양</returns>
        public abstract eTrackPosType CheckTrackPos(PointF pt);
        /// <summary>
        /// Rectangle 안에 Figure가 있는지 여부 확인
        /// </summary>
        /// <param name="startPoint">Rectangle Left,Top</param>
        /// <param name="endPoint">Rectangle Right,Bottom</param>
        /// <returns>결과값</returns>
        public abstract bool CheckRegionInFigure(PointF startPoint, PointF endPoint);
        /// <summary>
        /// Point안에 Figure가 있는지 여부
        /// </summary>
        /// <param name="point">Point</param>
        /// <returns>결과값</returns>
        public abstract bool CheckPointInFigure(PointF point);
        /// <summary>
        /// 새로운 크기에 맞춰 Figure 크기 조정
        /// </summary>
        /// <param name="coordTransformer"></param>
        public abstract void Scale(ImageCoordTransformer coordTransformer);
        /// <summary>
        /// 전체 Figure Mouse Down 함수 실행
        /// </summary>
        /// <param name="startTrackPos"></param>
        public abstract void MouseDown(PointF startTrackPos);
        /// <summary>
        /// 전체 Figure Mouse Move 함수 실행
        /// </summary>
        /// <param name="pt"></param>
        public abstract void MouseMove(PointF pt);
        /// <summary>
        /// 전체 Figure Mouse Up 함수 실행
        /// </summary>
        /// <param name="endTrackPos"></param>
        public abstract void MouseUp(PointF endTrackPos);
        /// <summary>
        /// Figure의 Result 값 가져오기
        /// </summary>
        /// <returns>결과값</returns>
        public abstract object GetResult();
        /// <summary>
        /// Figure Parameter 설정
        /// </summary>
        /// <param name="param"></param>
        public abstract void SetParam(object param);
        /// <summary>
        /// eImageTransform 타입별 Figure 회전, 반전
        /// </summary>
        /// <param name="type">eImageTransform 타입</param>
        /// <param name="centerPoint">centerPoint</param>
        /// <param name="rotateAngle">rotateAngle</param>
        public abstract void Transform(eImageTransform type, PointF centerPoint, double rotateAngle = 0);
        /// <summary>
        /// Xml 작성
        /// </summary>
        /// <param name="element">XmlElement</param>
        public abstract void WriteXml(XmlElement element);
        /// <summary>
        /// Point의 X(수평방향) Flip
        /// </summary>
        /// <param name="sourcePoint">sourcePoint</param>
        /// <param name="centerPoint">centerPoint</param>
        /// <returns></returns>
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
        /// <summary>
        /// Point의 Y(수평방향) Flip
        /// </summary>
        /// <param name="sourcePoint">sourcePoint</param>
        /// <param name="centerPoint">centerPoint</param>
        /// <returns></returns>
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
        /// <summary>
        /// Point 회전
        /// </summary>
        /// <param name="sourcePoint">sourcePoint</param>
        /// <param name="centerPoint">centerPoint</param>
        /// <param name="rotateAngle">각도</param>
        /// <returns></returns>
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
        /// <summary>
        /// Rectangle을 Flip 한다.
        /// </summary>
        /// <param name="rect">rect</param>
        /// <param name="newContainerWidth"></param>
        /// <param name="newContainerHeight"></param>
        /// <param name="type"></param>
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
        /// <summary>
        /// Point1(Left,Top)과 Point2(Right,Bottom)을 가지는 RectangleF 값 가져오기
        /// </summary>
        /// <param name="p1">Left,Top</param>
        /// <param name="p2">Right,Bottom</param>
        /// <param name="isSquare">정사각형 여부</param>
        /// <returns>결과값</returns>
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
        /// <summary>
        /// Point1과 Point2 의 사이의 거리
        /// </summary>
        /// <param name="p1">Point1</param>
        /// <param name="p2">Point2</param>
        /// <returns>결과값</returns>
        public double CalcLengthPointToPoint(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2));
        }
        /// <summary>
        /// point1과 point2를 잊는 직선에 p가 존재하는지 여부
        /// </summary>
        /// <param name="p1">point1</param>
        /// <param name="p2">point2</param>
        /// <param name="p">point</param>
        /// <param name="width">직선의 Width</param>
        /// <returns></returns>
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
        /// <summary>
        /// Point를 실제 이미지 크기에 맞춰 좌표 출력한다.
        /// </summary>
        /// <param name="point">Point를</param>
        /// <returns>결과값</returns>
        public PointF GetOrgPoint(PointF point)
        {
            int nowWidth = Status.Instance().SelectedViewer.GetDrawBox().PictureBoxWidth();
            int nowHeight = Status.Instance().SelectedViewer.GetDrawBox().PictureBoxHeight();
            int orgWidth = Status.Instance().SelectedViewer.ImageManager.GetOrgImage().Width;
            int orgHeight = Status.Instance().SelectedViewer.ImageManager.GetOrgImage().Height;

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
    }
}
