using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.DIP
{
    public partial class KImage
    {
        // 상대좌표
        public static PointF ConvertToRelativePoint(Point realCoordi, float zoomFactor)
        {
            zoomFactor = 1f / zoomFactor;

            float x = realCoordi.X * zoomFactor;
            float y = realCoordi.Y * zoomFactor;

            return new PointF(x, y);
        }

        public static PointF ConvertToRelativePoint(PointF realCoordi, float zoomFactor)
        {
            zoomFactor = 1f / zoomFactor;

            float x = (float)Math.Round(realCoordi.X * zoomFactor);
            float y = (float)Math.Round(realCoordi.Y * zoomFactor);

            return new PointF(x, y);
        }

        public static PointF ConvertToAbsPoint(Point relativePoint, float zoomFactor)
        {
            float x = relativePoint.X * zoomFactor;
            float y = relativePoint.Y * zoomFactor;

            return new PointF(x, y);
        }

        public static PointF ConvertToAbsPoint(PointF relativePoint, float zoomFactor)
        {
            float x = (float)Math.Round(relativePoint.X * zoomFactor);
            float y = (float)Math.Round(relativePoint.Y * zoomFactor);

            return new PointF(x, y);
        }

        public static RectangleF ConvertToRelativeRect(RectangleF realCoordi, float zoomFactor)
        {
            zoomFactor = 1f / zoomFactor;

            return new RectangleF(realCoordi.X * zoomFactor, realCoordi.Y * zoomFactor,
                                  realCoordi.Width * zoomFactor, realCoordi.Height * zoomFactor);
        }

        public static RectangleF ConvertToAbsRect(RectangleF relativeRect, float zoomFactor)
        {
            return new RectangleF(relativeRect.X * zoomFactor, relativeRect.Y * zoomFactor,
                                   relativeRect.Width * zoomFactor, relativeRect.Height * zoomFactor);
        }


        // 중심좌표
        public static Point GetCenterPoint(Size size)
        {
            return GetCenterPoint(size.Width, size.Height);
        }

        public static Point GetCenterPoint(int width, int height)
        {
            Point pt = new Point();
            int cw = 0, ch = 0;

            if (width % 2 == 0)
                cw = width / 2;
            else if (width % 2 == 1)
                cw = (width + 1) / 2;

            if (height % 2 == 0)
                ch = height / 2;
            else if (height % 2 == 1)
                ch = (height + 1) / 2;

            pt.X = cw;
            pt.Y = ch;

            return pt;
        }

        public static Point GetCenterLocation(Size containerSize, Size imageSize)
        {
            return GetCenterLocation(containerSize.Width, containerSize.Height,
                                     imageSize.Width, imageSize.Height);
        }

        public static Point GetCenterLocation(int containerWidth, int containerHeight,
                                              int imageWidth, int imageHeight)
        {
            var pt = new Point();

            int ctW = containerWidth;
            int ctH = containerHeight;
            int imgW = imageWidth;
            int imgH = imageHeight;

            pt.X = (ctW / 2) - (imgW / 2);
            pt.Y = (imgH / 2) - (imgH / 2);

            return pt;
        }


        // 회전
        public static Point RotatePointAxis(float angle, Point axisOfRotation, Point srcPt)
        {
            Point[] ptArr = new Point[1];
            ptArr[0] = srcPt;

            using (var mat = new Matrix())
            {
                mat.RotateAt(angle, axisOfRotation);
                mat.TransformPoints(ptArr);
            }

            return ptArr[0];
        }

        public static PointF RotatePointAxis(float angle, Point axisOfRotation, PointF srcPt)
        {
            PointF[] ptArr = new PointF[1];
            ptArr[0] = srcPt;

            using (var mat = new Matrix())
            {
                mat.RotateAt(angle, axisOfRotation);
                mat.TransformPoints(ptArr);
            }

            return ptArr[0];
        }

        public static PointF FlipPointAxis(bool isHorizonFlip/*좌우반전*/, PointF srcPt, PointF centerAxis)
        {
            PointF rstPt = new PointF();
            int absDist;
            int pad;

            if (isHorizonFlip)
            {
                absDist = (int)Math.Round(centerAxis.X - srcPt.X);
                pad = absDist * 2;
                srcPt.X += pad;
            }
            else
            {
                absDist = (int)Math.Round(centerAxis.Y - srcPt.Y);
                pad = absDist * 2;
                srcPt.Y += pad;
            }

            rstPt = srcPt;
            return rstPt;
        }

        public static PointF FlipPointAxis(bool isHorizonFlip/*좌우반전*/, bool isVerticalFlip, PointF srcPt, PointF centerAxis)
        {
            PointF rstPt = new PointF();
            int absDist;
            int pad;

            if (isHorizonFlip)
            {
                absDist = (int)Math.Round(centerAxis.X - srcPt.X);
                pad = absDist * 2;
                srcPt.X += pad;
            }
            if (isVerticalFlip)
            {
                absDist = (int)Math.Round(centerAxis.Y - srcPt.Y);
                pad = absDist * 2;
                srcPt.Y += pad;
            }

            rstPt = srcPt;
            return rstPt;
        }
    }
}
