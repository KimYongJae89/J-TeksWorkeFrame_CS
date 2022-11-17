using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XManager.Util
{
    public class DrawHelper
    {
        /// <summary>
        /// 직선 그리기. 이후 좌표 변경
        /// </summary>
        /// <param name="g">그래픽 객체</param>
        /// <param name="startPoint">시작점</param>
        /// <param name="endPoint">끝점</param>
        public static void DrawLine(Graphics g, ref PointF startPoint, ref PointF endPoint)
        {
            InspectPoint(ref startPoint, ref endPoint);
            CalibrationPointF(ref startPoint);
            CalibrationPointF(ref endPoint);

            //Color myColor = Color.PaleVioletRed;

            //// Create the ColorConverter.
            //System.ComponentModel.TypeConverter converter =
            //    System.ComponentModel.TypeDescriptor.GetConverter(myColor);

            //string colorAsString = converter.ConvertToString(Color.PaleVioletRed);
            //g.DrawLine(new Pen(myColor), startPoint, endPoint);

            g.DrawLine(new Pen(Color.Yellow), startPoint, endPoint);
        }

        public static void DrawLinesInList(Graphics g,ref List<PointF> points)
        {
            for (int i = 0; i < points.Count(); i++)
            {
                int now = i;
                int next = i + 1;

                if (now == points.Count() - 1)
                    break;

                PointF nowPoint = points[now];
                PointF nextPoint = points[next];
                CalibrationPointF(ref nowPoint);
                CalibrationPointF(ref nextPoint);

                g.DrawLine(new Pen(Color.Yellow), nowPoint, nextPoint);
            }
        }
  
        public static void DrawRectangleF(Graphics g, ref RectangleF rect, bool isDash = false)
        {
            Pen pen = new Pen(Color.Yellow);
            if (isDash)
            {
                pen.DashStyle = DashStyle.Dash;
                pen.DashPattern = new float[] { 3, 3 };
            }
            CalibrationRectangleF(ref rect);

            g.DrawRectangle(pen, Rectangle.Round(rect));
        }
        public static void CalibrationRectangleF(ref RectangleF rect)
        {
            PointF leftTop = new PointF(rect.X, rect.Y);
            PointF rightBottom = new PointF(rect.X + rect.Width, rect.Y + rect.Height);
    
            float widthInterval = (float)CStatus.Instance().GetDrawBox().GetPictureBox().Width / (float)CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width;
            float heightInterval = (float)CStatus.Instance().GetDrawBox().GetPictureBox().Height / (float)CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height;
            float pictureBoxWidth = (float)CStatus.Instance().GetDrawBox().GetPictureBox().Width - (widthInterval / 2);
            float pictureBoxHeight = (float)CStatus.Instance().GetDrawBox().GetPictureBox().Height - (heightInterval / 2);

            InspectPoint(ref leftTop, ref rightBottom);
            
            CalibrationPointF(ref leftTop);
            CalibrationPointF(ref rightBottom);

            float width = Math.Abs(rightBottom.X - leftTop.X);
            float height = Math.Abs(rightBottom.Y - leftTop.Y);

            if (width <= 0)
                width = 1;
            if (height <= 0)
                height = 1;
            rect.X = leftTop.X;
            rect.Y = leftTop.Y;
            rect.Width = width;
            rect.Height = height;
        }

        public static RectangleF CalibrationRectangleF(RectangleF rect)
        {
            RectangleF calibrationRect = new RectangleF();

            PointF leftTop = new PointF(rect.X, rect.Y);
            PointF rightBottom = new PointF(rect.X + rect.Width, rect.Y + rect.Height);

            float pictureBoxWidth = (float)CStatus.Instance().GetDrawBox().GetPictureBox().Width - 1;
            float pictureBoxHeight = (float)CStatus.Instance().GetDrawBox().GetPictureBox().Height - 1;

            if (leftTop.X <= 0)
            {
                float aMount = Math.Abs(leftTop.X);
                leftTop.X = 0;
                rightBottom.X += aMount;
            }
           
            if(leftTop.X + rect.Width >= pictureBoxWidth)
            {
                float aMount = Math.Abs(leftTop.X + rect.Width - pictureBoxWidth);
                leftTop.X -= aMount;
            }

            if(leftTop.Y <=0)
            {
                float aMount = Math.Abs(leftTop.Y);
                leftTop.Y = 0;
                rightBottom.Y += aMount;
            }

            if (leftTop.Y + rect.Height >= pictureBoxHeight)
            {
                float aMount = Math.Abs(leftTop.Y + rect.Height - pictureBoxHeight);
                leftTop.Y -= aMount;
            }

            CalibrationPointF(ref leftTop);
            CalibrationPointF(ref rightBottom);

            float width = Math.Abs(rightBottom.X - leftTop.X);
            float height = Math.Abs(rightBottom.Y - leftTop.Y);

            if (width <= 0)
                width = 1;
            if (height <= 0)
                height = 1;
            calibrationRect.X = leftTop.X;
            calibrationRect.Y = leftTop.Y;
            calibrationRect.Width = width;
            calibrationRect.Height = height;
            
            return calibrationRect;
        }

        public static void CalibrationPointF(ref PointF point)
        {
            float displayImageWidth = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width;
            float displayImageHeight = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height;
            float pictureBoxWidth = CStatus.Instance().GetDrawBox().GetPictureBox().Width;
            float pictureBoxHeight = CStatus.Instance().GetDrawBox().GetPictureBox().Height;

            float widthScale = pictureBoxWidth / displayImageWidth;
            float heightScale = pictureBoxHeight / displayImageHeight;


            int widthGridCount = (int)(pictureBoxWidth / widthScale);
            int heightGirdCount = (int)(pictureBoxHeight / heightScale);

            int x = 0;
            //XPosition
            for (int i = 0; i < widthGridCount; i++)
            {
                float posX = i * widthScale;
                float nextPosX = (i + 1) * widthScale;

                if(point.X >= posX && point.X <nextPosX)
                {
                    x = i;
                    break;
                }
                if (i == widthGridCount - 1)
                {
                    x = i + 1;
                    break;
                }
            }

            int y = 0;
            for (int i = 0; i < heightGirdCount; i++)
            {
                float posY = i * heightScale;
                float nextPosY = (i + 1) * heightScale;

                if (point.Y >= posY && point.Y < nextPosY)
                {
                    y = i;
                    break;
                }
                if (i == heightGirdCount - 1)
                {
                    y = i + 1;
                    break;
                }
            }
            float xPos = (widthScale * x) + (widthScale / 2);
            float yPos = (heightScale * y) + (heightScale / 2);
            float width = (float)CStatus.Instance().GetDrawBox().GetPictureBox().Width;
            float height = (float)CStatus.Instance().GetDrawBox().GetPictureBox().Height;

            if (xPos >= width)
                xPos = width;

            if (yPos >= height)
                yPos = height;

            point.X = xPos;
            point.Y = yPos;
        }

        public static PointF ProfileCalibrationPointF(PointF point)
        {
            PointF calibrationPoint = new PointF();
            float displayImageWidth = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width;
            float displayImageHeight = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height;
            float pictureBoxWidth = CStatus.Instance().GetDrawBox().GetPictureBox().Width;
            float pictureBoxHeight = CStatus.Instance().GetDrawBox().GetPictureBox().Height;

            float widthScale = pictureBoxWidth / displayImageWidth;
            float heightScale = pictureBoxHeight / displayImageHeight;

            int widthGridCount = (int)(pictureBoxWidth / widthScale);
            int heightGirdCount = (int)(pictureBoxHeight / heightScale);


            calibrationPoint.X = (widthScale * point.X) +  (widthScale / 2);
            calibrationPoint.Y = (heightScale * point.Y) + (heightScale / 2);

            return calibrationPoint;
        }

        public static void InspectPoint(ref PointF startPoint, ref PointF endPoint)
        {
            float pictureBoxWidth = (float)CStatus.Instance().GetDrawBox().GetPictureBox().Width - 1;
            float pictureBoxHeight = (float)CStatus.Instance().GetDrawBox().GetPictureBox().Height - 1;
            if (startPoint.X <= 0)
            {
                float aMount = Math.Abs(startPoint.X);
                startPoint.X = 0;
                endPoint.X += aMount;
            }
            if (startPoint.X >= pictureBoxWidth)
            {
                float aMount = Math.Abs(pictureBoxWidth - startPoint.X);
                startPoint.X = pictureBoxWidth;
                endPoint.X -= aMount;
            }

            if (startPoint.Y <= 0)
            {
                float aMount = Math.Abs(startPoint.Y);
                startPoint.Y = 0;
                endPoint.Y += aMount;
            }
            if (startPoint.Y >= pictureBoxHeight)
            {
                float aMount = Math.Abs(pictureBoxHeight - startPoint.Y);
                startPoint.Y = pictureBoxHeight;
                endPoint.Y -= aMount;
            }

            if (endPoint.X <= 0)
            {
                float aMount = Math.Abs(endPoint.X);
                endPoint.X = 0;
                startPoint.X += aMount;
            }
            if (endPoint.X >= pictureBoxWidth)
            {
                float aMount = Math.Abs(pictureBoxWidth - endPoint.X);
                endPoint.X = pictureBoxWidth;
                startPoint.X -= aMount;
            }

            if (endPoint.Y <= 0)
            {
                float aMount = Math.Abs(endPoint.Y);
                endPoint.Y = 0;
                startPoint.Y += aMount;
            }
            if (endPoint.Y >= pictureBoxHeight)
            {
                float aMount = Math.Abs(pictureBoxHeight - endPoint.Y);
                endPoint.Y = pictureBoxHeight;
                //endPoint.Y -= aMount;
                startPoint.Y -= aMount;
            }
        }
    }
}
