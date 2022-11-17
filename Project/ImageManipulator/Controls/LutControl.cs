using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JTeksSplineGraph.Controls;
using ImageManipulator.ImageProcessingData;

namespace ImageManipulator.Controls
{
    public partial class LutControl : UserControl
    {
        public Action FilterEdit;
        private ImageCurve _imageCurveBox;
        private DoubleBufferPanel _yAxisControl;
        private DoubleBufferPanel _xAxisControl;
        Rectangle _imCurveRect = new Rectangle();
        public Action<tCurveDataInfo> SendLutInfo;
        private int _bit = 8;
        public int Bit
        {   get {  return _bit; }
            set { _bit = value; }
        }

        public LutControl()
        {
            InitializeComponent();
        }

        private void LutControl_Load(object sender, EventArgs e)
        {
            _yAxisControl = new DoubleBufferPanel();
            YAxisPanel.Controls.Add(_yAxisControl);
            _yAxisControl.Dock = DockStyle.Fill;
            _yAxisControl.Paint += new System.Windows.Forms.PaintEventHandler(this.YAxisControl_Paint);

            _xAxisControl = new DoubleBufferPanel();
            XAxisPanel.Controls.Add(_xAxisControl);
            _xAxisControl.Dock = DockStyle.Fill;
            _xAxisControl.Paint += new System.Windows.Forms.PaintEventHandler(this.XAxisControl_Paint);

            _imageCurveBox = new ImageCurve();
            _imageCurveBox.ImageBit = Status.Instance().SelectedViewer.ImageManager.GetBit();
            this.ImageCurvePanel.Controls.Add(_imageCurveBox);
            _imageCurveBox.Dock = DockStyle.Fill;
            _imageCurveBox.ImageLevelChanged += new JTeksSplineGraph.Controls.ImageLevelChangedEventHandler(this.imageCurve_ImageLevelChanged);
            //_imageCurveBox.ResetKeyPoint();
        }
        public void SetLutParam(int imageBit)
        {
            _bit = imageBit;
            
            if(_imageCurveBox != null)
            {
                _imageCurveBox.ImageBit = imageBit;
                _imageCurveBox.ResetKeyPoint();
            }
        }

        public void UpdateLut()
        {
            if (_imageCurveBox != null)
            {
                _imageCurveBox.Invalidate();
               
            }

            if (_yAxisControl != null)
                _yAxisControl.Invalidate();
            if (_xAxisControl != null)
                _xAxisControl.Invalidate();
        }

        private void imageCurve_ImageLevelChanged(object sender, ImageLevelEventArgs e)
        {
            if (SendLutInfo != null)
            {
                //int[] lut = Array.ConvertAll(e.LevelValue, Convert.ToInt32);
                SendLutInfo(GetInformation());
            }
            if (FilterEdit != null)
                FilterEdit();
        }

        private void YAxisControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Control.DefaultBackColor);

            int rulerLine1 = 0;
            int rulerLine2 = 0;

            if (this.Bit == 8 || this.Bit == 24)
            {
                rulerLine1 = 10;
                rulerLine2 = 50;
            }
            else if (this.Bit == 16)
            {
                rulerLine1 = 2000;
                rulerLine2 = 10000;
            }

            // draw Y ruler
            int y0 = 0;
            int y2 = 0;// 255 , 65535
            if (this.Bit == 24)
                y2 = (int)Math.Pow(2, 8) - 1;
            else
                y2 = (int)Math.Pow(2, this.Bit) - 1;

            float unitY = (float)ImageCurvePanel.Height / (float)y2;
            for (int i = y0; i <= y2; i++)
            {
                if (i % rulerLine1 == 0) g.DrawLine(new Pen(Color.Black), new PointF(_yAxisControl.Width - 5, _yAxisControl.Height - (i - y0) * unitY),
                                     new PointF(_yAxisControl.Width, _yAxisControl.Height - (i - y0) * unitY)); // ruler line
                if (i % rulerLine2 == 0)
                {
                    g.DrawLine(new Pen(Color.Black, 2f), new PointF(_yAxisControl.Width - 10, _yAxisControl.Height - (i - y0) * unitY),
                             new PointF(_yAxisControl.Width, _yAxisControl.Height - (i - y0) * unitY)); // ruler line
                    SizeF stringSize = g.MeasureString(i.ToString(), this.Font);
                    PointF stringLoc = new PointF(_yAxisControl.Width - 10 - stringSize.Width, _yAxisControl.Height - (i - y0) * unitY - stringSize.Height / 2);
                    if (stringLoc.Y - stringSize.Height < 0)
                        stringLoc.Y = 0;
                    if (stringLoc.Y + stringSize.Height > YAxisPanel.Height)
                        stringLoc.Y = _yAxisControl.Height - stringSize.Height;
                    g.DrawString(i.ToString(), this.Font, new SolidBrush(Color.Black), stringLoc);
                }
            }
        }

        private void XAxisControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Control.DefaultBackColor);
            // draw X ruler
            int x0 = 0;
            int x2 = 0;// 255 , 65535
            if (this._bit == 24)
                x2 = (int)Math.Pow(2, 8) - 1;
            else
                x2 = (int)Math.Pow(2, this._bit) - 1;

            float unitX = (float)_imageCurveBox.Width / (float)x2;

            int rulerLine1 = 0;
            int rulerLine2 = 0;

            if (this._bit == 8 || this._bit == 24)
            {
                rulerLine1 = 10;
                rulerLine2 = 50;
            }
            else if (this._bit == 16)
            {
                rulerLine1 = 2000;
                rulerLine2 = 10000;
            }
            for (int i = x0; i <= x2; i++)
            {
                if (i % rulerLine1 == 0)
                    g.DrawLine(new Pen(Color.Black), new PointF((i - x0) * unitX, 0), new PointF((i - x0) * unitX, 5)); // ruler line
                if (i % rulerLine2 == 0)
                {
                    g.DrawLine(new Pen(Color.Black, 2f), new PointF((i - x0) * unitX,
                        0), new PointF((i - x0) * unitX, 10));
                    SizeF stringSize = g.MeasureString(i.ToString(), this.Font);
                    PointF stringLoc = new PointF((i - x0) * unitX - (stringSize.Width / 2), 10);
                    if (stringLoc.X < 0)
                        stringLoc.X = 0;
                    if (stringLoc.X + stringSize.Width > _xAxisControl.Width)
                        stringLoc.X = _xAxisControl.Width - stringSize.Width;

                    g.DrawString(i.ToString(), this.Font, new SolidBrush(Color.Black), stringLoc);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _imCurveRect = new Rectangle(ImageCurvePanel.Left, ImageCurvePanel.Top, ImageCurvePanel.Width, ImageCurvePanel.Height);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _imCurveRect = new Rectangle(ImageCurvePanel.Left, ImageCurvePanel.Top, ImageCurvePanel.Width, ImageCurvePanel.Height);
            Invalidate();
            if (_xAxisControl != null)
                _xAxisControl.Invalidate();
            if (_yAxisControl != null)
                _yAxisControl.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        public void Reset()
        {
            if(_imageCurveBox != null)
                _imageCurveBox.ResetKeyPoint();
        }

        public void SetInformation(ref tCurveDataInfo info)
        {
            if (_imageCurveBox != null)
                _imageCurveBox.SetInformation(ref info);
        }
        public List<PointF> GetRealPoint()
        {
            return _imageCurveBox.GetRealPoints();
        }

        public void EditParam(ref IpLutParams param)
        {
            if (_imageCurveBox == null)
                return;
            tCurveDataInfo info = _imageCurveBox.GetInformation();

            param.keyPt = info.keyPt;
            param.Width = info.Width;
            param.Height = info.Height;
            param.LUT = info.LUT;
            // param.LUT = Array.ConvertAll(e.LevelValue, Convert.ToInt32);
        }

        public tCurveDataInfo GetInformation()
        {
            tCurveDataInfo info = _imageCurveBox.GetInformation();
            return info;
        }
    }
}
