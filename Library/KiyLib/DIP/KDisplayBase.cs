using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiyLib.DIP
{
    /// <summary>
    /// 현재는 사용되지 않는다
    /// </summary>
    public class KDisplayBase
    {
        //protected PointF realCoordinate;
        protected PointF currentPosition;
        protected float zoomFactor = 1f;
        protected bool useCrossHair;
        protected bool isShiftPressed;
        private Color crosshairColor = Color.Red;
        private Pen crosshairPen = new Pen(Color.Black, 1);
        private int width;
        private int height;

        public int Width
        {
            get { return width; }
            private set { width = value; }
        }
        public int Height
        {
            get { return height; }
            private set { height = value; }
        }
        public PointF Coordinate
        {
            get { return currentPosition; }
            set { currentPosition = value; }
        }
        public float ZoomFactor
        {
            get { return zoomFactor; }
            set
            {
                zoomFactor = value;
                Zoom(zoomFactor);
            }
        }
        public float AspectRatio
        {
            get { return Width / (float)Height; }
        }
        public bool UseCrossHair
        {
            get { return useCrossHair; }
            set { useCrossHair = value; }
        }
        public Color CrosshairColor
        {
            get { return crosshairColor; }
            set
            {
                crosshairColor = value;
                crosshairPen.Color = crosshairColor;
            }
        }


        public KDisplayBase()
        {
            crosshairPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }

        protected void DrawCrosshair(Graphics g)
        {
            var pt = KImage.GetCenterPoint(width, height);
            int cw = pt.X;
            int ch = pt.Y;

            g.DrawLine(crosshairPen, 0, ch, Width, ch);
            g.DrawLine(crosshairPen, cw, 0, cw, Height);
        }

        //아직 미구현
        protected void Zoom(double zoomFactor)
        {
            double val = zoomFactor;
            //zoom
            //현재 위치 기준으로 줌인 줌아웃
        }

        protected void FitZoom()
        {
            float val = 0;
            Zoom(val);
        }

        protected void OriginSizeZoom()
        {
            float val = 0;
            Zoom(val);
            //위치 변경
        }
    }
}
