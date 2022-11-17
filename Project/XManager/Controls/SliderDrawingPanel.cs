using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XManager.Util;

namespace XManager.Controls
{
    public partial class SliderDrawingPanel : UserControl
    {
        private DoubleBufferPanel SliderControl;
        private DoubleBufferPanel StringControl;
        public event Action<int, int> UpdateSliderMinMax;
        
        private PointF _startPanningPoint;
        private PointF _movePanningPoint;
        private PointF _endPanningPoint;
        private RectangleF _sliderRectangle;
        private eHistogramTrackPos _sliderTrackPos = eHistogramTrackPos.None;
        private eModeType _drawMode = eModeType.None;
        private float _levelingMaxValue = 0;
        private int _lowLevelValue = 255;
        private int _highLevelValue = 0;
        private bool _isIgnoreEvent = false;
        private RectangleF _prevSliderRect;
        private int _prevLowLevelValue = 0;
        private int _prevHighLevelValue = 0;
       // private bool _initialize = false;
        public SliderDrawingPanel()
        {
            InitializeComponent();
        }

        private void SliderDrawingPanel_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
              ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            ControlAdd();

        }

        public void ControlAdd()
        {
            SliderControl = new DoubleBufferPanel();
            SliderPanel.Controls.Add(SliderControl);
            SliderControl.Dock = System.Windows.Forms.DockStyle.Fill;
            SliderControl.Paint += new System.Windows.Forms.PaintEventHandler(this.SliderControl_Paint);
            SliderControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SliderControl_MouseDown);
            SliderControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SliderControl_MouseMove);
            SliderControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SliderControl_MouseUp);

            StringControl = new DoubleBufferPanel();
            StringPanel.Controls.Add(StringControl);
            StringControl.Dock = System.Windows.Forms.DockStyle.Fill;
            StringControl.Paint += new System.Windows.Forms.PaintEventHandler(this.StringControl_Paint);
        }

        //public void SetSliderParam(int imageBit, bool isColor)
        public void SetSliderMinMax(int sliderMin = 0, int sliderMax = 255)
        {
            //_levelingMaxValue = GetHistogramLength(imageBit, isColor);
            _levelingMaxValue = sliderMax;
            _lowLevelValue = sliderMin;
            _highLevelValue = (int)_levelingMaxValue;
        }

        private void StringControl_Paint(object sender, PaintEventArgs e)
        {
            if (_levelingMaxValue == 0)
                return;
            e.Graphics.Clear(Control.DefaultBackColor);
            Brush brush = Brushes.Black;
            Font font = new Font("고딕", 10);
            // Slider가 0 일 경우
            string minValue = "0";
            PointF stringSliderMinValue = new PointF(0, 0);
            e.Graphics.DrawString(minValue, font, brush, stringSliderMinValue);

            // Slider 최대값일 경우
            string sliderMaxValue = _levelingMaxValue.ToString();
            SizeF stringSize = e.Graphics.MeasureString(sliderMaxValue, font);
            PointF stringSliderMaxValue = new PointF(StringControl.Width - stringSize.Width, 0);
            e.Graphics.DrawString(sliderMaxValue, font, brush, stringSliderMaxValue);
        }

        private void SliderControl_Paint(object sender, PaintEventArgs e)
        {
            if (_lowLevelValue == _highLevelValue)
            {
                _lowLevelValue = _prevLowLevelValue;
                _highLevelValue = _prevHighLevelValue;

                _sliderRectangle = _prevSliderRect;
            }
            else
            _sliderRectangle = GetSliderRantangle(_lowLevelValue, _highLevelValue);
            e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, SliderControl.Width - 1, SliderControl.Height - 1));
            e.Graphics.FillRectangle(Brushes.Black, _sliderRectangle);

            _prevSliderRect = _sliderRectangle;
            _prevLowLevelValue = _lowLevelValue;
            _prevHighLevelValue = _highLevelValue;
            if (_isIgnoreEvent)
            {
                _isIgnoreEvent = false;
            }
            else
            {
                if (UpdateSliderMinMax != null)
                {
                    UpdateSliderMinMax(_lowLevelValue, _highLevelValue);
                }
            }
        }

        private void SliderControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point pt = SliderControl.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    _startPanningPoint = pt;
                }
            }
        }

        private void SliderControl_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Point pt = SliderControl.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (_drawMode != eModeType.None)
                    {
                        if (_sliderTrackPos != eHistogramTrackPos.None)
                        {
                            _drawMode = eModeType.Edit;
                        }
                    }
                    else
                    {
                        _movePanningPoint = pt;

                        if (_sliderTrackPos == eHistogramTrackPos.Inside)
                        {
                            int moveStartLevel = GetHistogramLevelValue(_startPanningPoint.X);
                            int movepannelLevel = GetHistogramLevelValue(_movePanningPoint.X);
                            int moveX = moveStartLevel - movepannelLevel;

                            SetMoveHistogramLevel(moveX);
                        }
                        else if (_sliderTrackPos == eHistogramTrackPos.Left)
                        {
                            int level1 = GetHistogramLevelValue(pt.X);
                            //int level2 = GetHistogramLevelValue(_sliderRectangle.Right);

                            int level2 = _highLevelValue;
                            SetHistogramLevelValue(level1, level2);
                            if (level1 > level2)
                                _sliderTrackPos = eHistogramTrackPos.Right;
                        }
                        else if (_sliderTrackPos == eHistogramTrackPos.Right)
                        {
                            //int level1 = GetHistogramLevelValue(_sliderRectangle.X);
                            int level1 = _lowLevelValue;
                            int level2 = GetHistogramLevelValue(pt.X);
                            SetHistogramLevelValue(level1, level2);
                            if (level2 < level1)
                                _sliderTrackPos = eHistogramTrackPos.Left;
                        }

                        _endPanningPoint = _movePanningPoint;
                        _startPanningPoint = _endPanningPoint;
                        SliderControl.Invalidate();
                    }
                }
                else
                {
                    SetSliderTrackPos(pt);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
         
        }

        private void SliderControl_MouseUp(object sender, MouseEventArgs e)
        {
            _drawMode = eModeType.None;
            _sliderTrackPos = eHistogramTrackPos.None;
        }

        /// <summary>
        /// Bit와 Color 여부에 따른 Histogram level 수
        /// </summary>
        /// <param name="bit"></param>
        /// <param name="isColor"></param>
        /// <returns></returns>
        private int GetHistogramLength(int bit, bool isColor)
        {
            int ret = 0;
            if (isColor)
            {
                ret = (int)Math.Pow(2, 8);
            }
            else
            {
                if (bit == 24)
                    ret = (int)Math.Pow(2, 8);
                else
                    ret = (int)Math.Pow(2, bit);
            }
            return ret;
        }

        private int GetHistogramLevelValue(float point)
        {
            int level = 0;//
            if (point <= 0)
                level = 0;
            else if (point >= SliderControl.Width - 1)
                level = (int)_levelingMaxValue;
            else
                level = (int)((point * (_levelingMaxValue)) / SliderControl.Width);
            return level;
        }

        private void SetHistogramLevelValue(int value1, int value2)
        {
            if (value1 < 0) value1 = 0;
            if (value1 >= _levelingMaxValue) value1 = (int)_levelingMaxValue;
            if (value2 < 0) value2 = 0;
            if (value2 >= _levelingMaxValue) value2 = (int)_levelingMaxValue;

             _lowLevelValue = value1;
            _highLevelValue = value2;

            if (value1 > value2)
            {
                    _lowLevelValue = value2;
                    _highLevelValue = value1;
            }
            else
            {
                _lowLevelValue = value1;
                _highLevelValue = value2;
            }
        }

        private void SetMoveHistogramLevel(int value)
        {
            int min = _lowLevelValue;
            int max = _highLevelValue;

            int newMin = min - value;
            int newMax = max - value;
            int width = max - min;
            if (newMin < 0)
            {
                _lowLevelValue = 0;
                _highLevelValue = width;
            }
            else if (newMax > _levelingMaxValue)
            {
                _lowLevelValue = (int)_levelingMaxValue - width;
                _highLevelValue = (int)_levelingMaxValue;
            }
            else
            {
                _lowLevelValue -= value;
                _highLevelValue -= value;
            }
        }

        private void SetSliderTrackPos(Point point)
        {
            int margin = 3;
            int width = SliderControl.Width;

            RectangleF leftRect = new RectangleF(_sliderRectangle.X, 0, margin, _sliderRectangle.Height);
            RectangleF rightRect = new RectangleF(_sliderRectangle.Right - margin, 0, margin, _sliderRectangle.Height);
            RectangleF inSideRect = new RectangleF(leftRect.Right, 0, rightRect.Left - leftRect.Right, _sliderRectangle.Height);

            if (inSideRect.Contains(point))
            {
                _sliderTrackPos = eHistogramTrackPos.Inside;
                Cursor.Current = Cursors.SizeAll;
                return;
            }
            if (leftRect.Contains(point))
            {
                _sliderTrackPos = eHistogramTrackPos.Left;
                Cursor.Current = Cursors.SizeWE;
                return;
            }
            if (rightRect.Contains(point))
            {
                _sliderTrackPos = eHistogramTrackPos.Right;
                Cursor.Current = Cursors.SizeWE;
                return;
            }
            _sliderTrackPos = eHistogramTrackPos.None;
            Cursor.Current = Cursors.Default;
        }

        private RectangleF GetSliderRantangle(int lowLevel, int highLevel)
        {
          
            RectangleF rect = new RectangleF();
            float lowRealPoint = (float)SliderPanel.Width * (float)lowLevel / (float)(_levelingMaxValue - 1);
            float highRealPoint = (float)SliderPanel.Width * (float)highLevel / (float)(_levelingMaxValue - 1);
            rect.X = lowRealPoint;
            rect.Y = 0;
            rect.Width = highRealPoint - lowRealPoint;
            rect.Height = (float)SliderPanel.Height;

            return rect;
        }

        private void SliderDrawingPanel_SizeChanged(object sender, EventArgs e)
        {
            _isIgnoreEvent = true;
            SliderControl.Invalidate();
            StringControl.Invalidate();
        }

        public void Initialize()
        {
            _isIgnoreEvent = true;
            _lowLevelValue = CStatus.Instance().Settings.HistogramMin;
            _highLevelValue = CStatus.Instance().Settings.HistogramMax;
            if(SliderControl != null)
                SliderControl.Invalidate();
            if(StringControl !=null)
                StringControl.Invalidate();
        }

        public void SetSliderValue(int min, int max, bool isSendEvent = true)
        {
            _isIgnoreEvent = true;
            _lowLevelValue = min;
            _highLevelValue = max;
            if (SliderControl != null)
                SliderControl.Invalidate();
            if (StringControl != null)
                StringControl.Invalidate();
            if(isSendEvent)
            {
                if (UpdateSliderMinMax != null)
                    UpdateSliderMinMax(min, max);
            }
        }
    }
}
