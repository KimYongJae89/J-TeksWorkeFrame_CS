using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace JTeksSplineGraph.Controls
{
    /// <summary>
    /// For image curve control
    /// </summary>
    public struct tCurveDataInfo
    {
        public List<PointF> keyPt;
        public float Width;
        public float Height;
        public int[] LUT;
    }

    public class ImageLevelEventArgs : EventArgs
    {
        private int[] levelValue;

        public ImageLevelEventArgs(int[] LevelValue)
        {
            levelValue = LevelValue;
        }

        public int[] LevelValue
        {
            get { return levelValue; }
        }
    }

    //Declare a delegate 
    public delegate void ImageLevelChangedEventHandler(object sender, ImageLevelEventArgs e);
    public enum eXmlType
    {
        Roi, LUT
    }
    //public class ImageCurveStatus
    //{
    //    public int ImageBit { get; set; }
    //    private static ImageCurveStatus _instance;
    //    public static ImageCurveStatus Instance()
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = new ImageCurveStatus();
    //        }

    //        return _instance;
    //    }
    //    public object PrevList;
    //    public int GetImageBitSize()
    //    {
    //        if (ImageBit == 24)
    //            return (int)Math.Pow(2, 8);
    //        return (int)Math.Pow(2, ImageBit);
    //    }
    //}

    public class ImageCurve : UserControl
    {
        List<PointF> keyPt = new List<PointF>();
        int[] level = null;
        public int ImageBit = 8;
        public event ImageLevelChangedEventHandler ImageLevelChanged;

        public ImageCurve()
        {
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
              ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        protected virtual void OnLevelChanged(ImageLevelEventArgs e)
        {
            if (ImageLevelChanged != null) // Make sure there are methods to execute.
                ImageLevelChanged(this, e); // Raise the event.
        }

        public int[] LevelValue
        {
            get { getImageLevel(); return level; }
        }

        private void getImageLevel()
        {
            if (keyPt.Count == 0)
            {
                return;
            }
            PointF[] pts = new PointF[keyPt.Count];
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X = keyPt[i].X * GetImageBitSize() / this.Width;
                pts[i].Y = GetImageBitSize() - keyPt[i].Y * GetImageBitSize() / this.Height;
            }

            for (int i = 0; i < pts[0].X; i++)
                level[i] = (byte)pts[0].Y;
            for (int i = (int)pts[pts.Length - 1].X; i < GetImageBitSize(); i++)
                level[i] = (byte)pts[pts.Length - 1].Y;

            JTeksSplineGraph.Geometry.Spline sp = new JTeksSplineGraph.Geometry.Spline();

            sp.DataPointF = pts;
            sp.Precision = 1.0;
            PointF[] spt = sp.SplinePointF;

            int minusStartNum = 0;
            for (int i = 0; i < spt.Length; i++)
            {
                if (spt[i].X < 0)
                    minusStartNum = i;
            }

            if (minusStartNum != 0)
            {
                for (int i = 0; i <= minusStartNum; i++)
                {
                    spt[i].X = 0;
                    spt[i].Y = spt[minusStartNum + 1].Y;
                }
            }
            for (int i = 0; i < spt.Length; i++)
            {
                int n = (int)spt[i].Y;
                if (n < 0) n = 0;
                if (n > GetImageMaxValue()) n = GetImageMaxValue();
                level[(int)pts[0].X + i] = n;
            }
        }

        int ww, hh;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (level == null)
            {
                level = new int[GetImageBitSize()];
            }

            ww = this.Width;
            hh = this.Height;
            keyPt.Clear();
            keyPt.Add(new Point(0, hh));
            keyPt.Add(new Point(ww, 0));

            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            for (int i = 0; i < keyPt.Count; i++)
            {
                keyPt[i] = new PointF(keyPt[i].X * this.Width / ww, keyPt[i].Y * this.Height / hh);
            }
            ww = this.Width;
            hh = this.Height;

            Invalidate();
        }

        int moveflag;
        bool drag = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            for (int i = 1; i < keyPt.Count; i++)
            {
                if (e.X > keyPt[i - 1].X + 20 && e.Y > 0 && e.X < keyPt[i].X - 20 && e.Y < this.Height)
                {
                    keyPt.Insert(i, e.Location);
                    drag = true;
                    moveflag = i;
                    this.Cursor = Cursors.Hand;
                    Invalidate();
                }
            }

            RectangleF r = new RectangleF(e.X - 20, e.Y - 20, 40, 40);
            for (int i = 0; i < keyPt.Count; i++)
            {
                if (r.Contains(keyPt[i]))
                {
                    drag = true;
                    moveflag = i;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            //mouse cursor
            bool handCursor = false;
            for (int i = 0; i < keyPt.Count; i++)
            {
                RectangleF r = new RectangleF(keyPt[i].X - 2, keyPt[i].Y - 2, 4, 4);
                if (r.Contains(e.Location))
                    handCursor = true;
            }
            if (handCursor) this.Cursor = Cursors.Hand;
            else this.Cursor = Cursors.Default;

            // move the picked point
            if (this.ClientRectangle.Contains(e.Location))
            {
                if (drag && moveflag > 0 && moveflag < keyPt.Count - 1)
                {
                    if (e.X > keyPt[moveflag - 1].X + 20 && e.X < keyPt[moveflag + 1].X - 20)
                    {
                        keyPt[moveflag] = e.Location;
                    }
                    else
                    {
                        keyPt.RemoveAt(moveflag);
                        drag = false;
                    }
                }

                if (drag && moveflag == 0 && e.X < keyPt[1].X - 20)
                    keyPt[0] = e.Location;

                if (drag && moveflag == keyPt.Count - 1 && e.X > keyPt[keyPt.Count - 2].X + 20)
                    keyPt[moveflag] = e.Location;

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (drag)
            {
                drag = false;
                getImageLevel();
                OnLevelChanged(new ImageLevelEventArgs(level));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.Clear(Color.White);


            // draw ruler
            DrawRulerLine(g);

            if (keyPt.Count == 0)
                return;
            // draw curve
            g.DrawLine(new Pen(Color.Black), new PointF(0, keyPt[0].Y), keyPt[0]);
            g.DrawLine(new Pen(Color.Black), new PointF(this.Width, keyPt[keyPt.Count - 1].Y), keyPt[keyPt.Count - 1]);

            JTeksSplineGraph.Geometry.Spline spline = new JTeksSplineGraph.Geometry.Spline();
            spline.SetControlSize(new Size(this.Width, this.Height));
            spline.ListDataPointF = keyPt;
            spline.Precision = 1;

            Point[] splinePt = spline.SplinePoint;

            g.DrawLines(new Pen(Color.Black), splinePt);
            g.DrawLine(new Pen(Color.Black), keyPt[keyPt.Count - 1], splinePt[splinePt.Length - 1]);


            foreach (PointF pt in keyPt)
            {
                PointF[] pts = new PointF[]{new PointF(pt.X,pt.Y-3),new PointF(pt.X-3,pt.Y),
                    new PointF(pt.X,pt.Y+3),new PointF(pt.X+3,pt.Y)};
                g.FillPolygon(new SolidBrush(Color.Red), pts);
            }
            g.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, this.Size.Width - 1, this.Size.Height - 1));
        }

        private void DrawRulerLine(Graphics g)
        {
            // draw X ruler
            //int imageBIt = ImageBit;
            Pen dotPen = new Pen(Color.Gray, 1);
            dotPen.DashStyle = DashStyle.Dash;
            int x0 = 0;
            int x2 = 0;// 255 , 65535
            if (ImageBit == 24)
                x2 = (int)Math.Pow(2, 8) - 1;
            else
                x2 = (int)Math.Pow(2, ImageBit) - 1;

            float unitX = (float)this.Width / (float)x2;

            int rulerLine1 = 0;
            int rulerLine2 = 0;

            if (ImageBit == 8 || ImageBit == 24)
            {
                rulerLine1 = 10;
                rulerLine2 = 50;
            }
            else if (ImageBit == 16)
            {
                rulerLine1 = 2000;
                rulerLine2 = 10000;
            }
            for (int i = x0; i <= x2; i++)
            {
                //if (i % rulerLine1 == 0) g.DrawLine(dotPen, new PointF((i - x0) * unitX + this.Left,
                //            this.Top), new PointF((i - x0) * unitX + this.Left, this.Bottom)); // ruler line
                if (i % rulerLine2 == 0)
                {
                    g.DrawLine(dotPen, new PointF((i - x0) * unitX + this.Left,
                        this.Top), new PointF((i - x0) * unitX + this.Left, this.Bottom));
                    //SizeF stringSize = g.MeasureString(i.ToString(), this.Font);
                    //PointF stringLoc = new PointF((i - x0) * unitX + this.Left - stringSize.Width / 2, this.Bottom + 12);
                    //g.DrawString(i.ToString(), this.Font, new SolidBrush(Color.Black), stringLoc);
                }
            }

            // draw Y ruler
            int y0 = 0;
            int y2 = 0;// 255 , 65535
            if (ImageBit == 24)
                y2 = (int)Math.Pow(2, 8) - 1;
            else
                y2 = (int)Math.Pow(2, ImageBit) - 1;

            float unitY = (float)this.Height / (float)y2;
            for (int i = y0; i <= y2; i++)
            {
                //if (i % rulerLine1 == 0) g.DrawLine(new Pen(Color.Black), new PointF(this.Left - 5, this.Bottom - (i - y0) * unitY),
                //                     new PointF(this.Left, this.Bottom - (i - y0) * unitY)); // ruler line
                if (i % rulerLine2 == 0)
                {
                    g.DrawLine(dotPen, new PointF(this.Left, this.Bottom - (i - y0) * unitY),
                             new PointF(this.Right, this.Bottom - (i - y0) * unitY)); // ruler line
                    //SizeF stringSize = g.MeasureString(i.ToString(), this.Font);
                    //PointF stringLoc = new PointF(this.Left - 10 - stringSize.Width, this.Bottom - (i - y0) * unitY - stringSize.Height / 2);
                    //g.DrawString(i.ToString(), this.Font, new SolidBrush(Color.Black), stringLoc);
                }
            }

            // draw rect for control imageCurve
            //g.DrawRectangle(new Pen(Color.Black, 2), _imCurveRect);
        }

        public void SaveKeyPointXml(string filePath)
        {
            //XmlWriter roiWriter = XmlWriterBuilder.Create(filePath, Data.eXmlType.LUT);
            //roiWriter.Write(keyPt, filePath);
        }

        public void LoadKeyPointXml(string filePath)
        {
            //XmlReader roiReader = XmlReaderBuilder.Create(filePath, Data.eXmlType.LUT);

            //if (roiReader != null)
            //{
            //    List<Point> ret = roiReader.Load<Point>(filePath);
            //    if(ret !=null)
            //    {
            //        keyPt.Clear();
            //        keyPt.AddRange(ret);
            //        Invalidate();
            //    }
            //    else
            //    {
            //        MessageBox.Show("The file format is different.");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("The file format is different.");
            //}
        }

        public int[] ResetKeyPoint()
        {
            ww = this.Width;
            hh = this.Height;
            //if (level == null)
            //{
            level = new int[GetImageBitSize()];
            //}
            keyPt.Clear();
            keyPt.Add(new Point(0, hh));
            //keyPt.Add(new Point(ww/2, hh));
            keyPt.Add(new Point(ww, 0));
            getImageLevel();

            Invalidate();

            return level;
        }
        public void SetInformation(ref tCurveDataInfo info)
        {
            //keyPt.Clear();
            keyPt = info.keyPt;
            ww = (int)info.Width;
            hh = (int)info.Height;

            for (int i = 0; i < keyPt.Count; i++)
            {
                keyPt[i] = new PointF(keyPt[i].X * this.Width / ww, keyPt[i].Y * this.Height / hh);
            }
            ww = this.Width;
            hh = this.Height;
            info.keyPt = keyPt;
            info.Width = this.ww;
            info.Height = this.hh;
            Invalidate();
        }

        public void SetKeyPoint(List<PointF> points, int lutSize)
        {
            keyPt.Clear();

            foreach (PointF point in points)
            {
                float newX = point.X * this.Width / lutSize;
                float newY = (lutSize - point.Y) * this.Height / lutSize;


                keyPt.Add(new PointF(newX, newY));
            }
            getImageLevel();

            Invalidate();
        }

        public List<PointF> GetRealPoints()
        {
            List<PointF> realPoint = new List<PointF>();
            int size = GetImageBitSize();
            foreach (PointF point in keyPt)
            {
                float newX = point.X * size / this.Width;
                float newY = size - (point.Y * size / this.Height);

                realPoint.Add(new PointF(newX, newY));
            }

            return realPoint;
        }

        private int GetImageBitSize()
        {

            if (ImageBit == 24)
                return (int)Math.Pow(2, 8);
            return (int)Math.Pow(2, ImageBit);
        }

        private int GetImageMaxValue()
        {
            if (ImageBit == 24)
                return (int)Math.Pow(2, 8) - 1;
            return (int)Math.Pow(2, ImageBit) - 1;
        }
        public tCurveDataInfo GetInformation()
        {
            tCurveDataInfo info = new tCurveDataInfo();
            if (this.keyPt.Count == 0)
            {
                if (level == null)
                {
                    level = new int[GetImageBitSize()];
                }

                ww = this.Width;
                hh = this.Height;
                keyPt.Clear();
                keyPt.Add(new Point(0, hh));
                keyPt.Add(new Point(ww, 0));
            }
            info.keyPt = this.keyPt;
            info.Width = ww;
            info.Height = hh;
            getImageLevel();
            info.LUT = this.level;
            return info;
        }
    }
}

