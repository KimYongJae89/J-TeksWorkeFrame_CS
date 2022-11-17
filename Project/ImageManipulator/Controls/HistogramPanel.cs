using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Reflection;
using ImageManipulator.Util;

namespace ImageManipulator.Controls
{
    public enum HistogramPanelDrawType
    {
        Histogram,
        Derivative,
    }

    public struct tMarkInfo
    {
        public int Mark1Index;
        public int Mark2Index;

        public float Mark1Value; // histogram 1개 쓸때 사용하는 Value
        public float Mark2Value;
        public float AreaMin; //Mark1~>Mark2 사이의 최소값
        public float AreaMax; //Mark1~>Mark2 사이의 최대값
        public float Area;    //Mark1~>Mark2 사이 전부 합
        public float AreaMean; //Mark1~>Mark2 사이의 평균값

        //RGB 단계로 받을 때 사용
        public float RedMark1Value;
        public float RedMark2Value;
        public float RedAreaMin; //Mark1~>Mark2 사이의 최소값
        public float RedAreaMax; //Mark1~>Mark2 사이의 최대값
        public float RedArea;    //Mark1~>Mark2 사이 전부 합
        public float RedAreaMean; //Mark1~>Mark2 사이의 평균값

        public float GreenMark1Value;
        public float GreenMark2Value;
        public float GreenAreaMin; //Mark1~>Mark2 사이의 최소값
        public float GreenAreaMax; //Mark1~>Mark2 사이의 최대값
        public float GreenArea;    //Mark1~>Mark2 사이 전부 합
        public float GreenAreaMean; //Mark1~>Mark2 사이의 평균값

        public float BlueMark1Value;
        public float BlueMark2Value;
        public float BlueAreaMin; //Mark1~>Mark2 사이의 최소값
        public float BlueAreaMax; //Mark1~>Mark2 사이의 최대값
        public float BlueArea;    //Mark1~>Mark2 사이 전부 합
        public float BlueAreaMean; //Mark1~>Mark2 사이의 평균값

        public float Mean; // 0~ 끝까지 평균값( 단, 0값은 제외)
    }

    public partial class HistogramPanel : UserControl
    {
        public bool isActive = true;
        public int ImageBit = 8;
        public HistogramPanelDrawType DrawType = HistogramPanelDrawType.Histogram;
        public DoubleBufferPanel DrawControl = null;
        private DoubleBufferPanel MarkWriteControl = null;
        private List<HistogramParams> _histogramParamList = null;
        private eMarkTrackPos _markTrackPos = eMarkTrackPos.None;
        private eModeType _drawMode = eModeType.None;
        private int _levelingMaxValue = 255;
        public int LevelingMaxValue
        {
            get {   return _levelingMaxValue;  }
            set {   _levelingMaxValue = value; }
        }

        private int _levelingMinValue = 0;
        public int LevelingMinValue
        {
            get { return _levelingMinValue; }
            set { _levelingMinValue = value; }
        }

        private int _mark1Value = 0; // 0~255 / 0~65535 Leveling 값 
        private int _mark2Value = 0;// 0~255 / 0~65535 Leveling 값 
        public bool MarkVisible = true;

        public event Action YAxisUpdateDelegate;
        public Action<tMarkInfo> MarkValueDelegate;
        private float _maxHistogramValue = 0;
        public float MaxHistogramValue
        {
            get { return _maxHistogramValue; }
            set {   _maxHistogramValue = value;  }
        }

        private float _drawMaxHistogramValue = 0;
        public float DrawMaxHistogramValue
        {
            get { return _drawMaxHistogramValue; }
            set { _drawMaxHistogramValue = value; }
        }

        private float _minHistogramValue = 0;
        public float MinHistogramValue
        {
            get { return _minHistogramValue; }
            set { _minHistogramValue = value; }
        }

    

        public tMarkInfo MarkInfo = new tMarkInfo();
        public HistogramPanel()
        {
            InitializeComponent();
        }

        private void HistogramDrawingPanel_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
              ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            LevelingMinValue = 0;
            //LevelingMaxValue = 255;
            if (_histogramParamList == null)
                LevelingMaxValue = 255;
            else
                LevelingMaxValue = _histogramParamList[0].HistogramValue.Count() - 1;


            ControlAdd();

            if (MarkVisible)
            {
                _mark1Value = 0;
                _mark2Value = LevelingMaxValue;

                //if (_histogramParamList == null)
                //    return;
                SetMark();
                SendMarkInfo();
            }
        }

        private void ControlAdd()
        {
            try
            {
                DrawControl = new DoubleBufferPanel();
                DrawPanel.Controls.Add(DrawControl);
                DrawControl.Dock = System.Windows.Forms.DockStyle.Fill;
                DrawControl.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawControl_Paint);
                DrawControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawControl_MouseDown);
                DrawControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawControl_MouseMove);
                DrawControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawControl_MouseUp);
                DrawControl.MouseWheel += new System.Windows.Forms.MouseEventHandler(OnMouse_Wheel);
                if (MarkVisible)
                {
                    MarkWriteControl = new DoubleBufferPanel();
                    MarkPanel.Controls.Add(MarkWriteControl);
                    MarkWriteControl.Dock = System.Windows.Forms.DockStyle.Fill;
                    MarkWriteControl.Paint += new System.Windows.Forms.PaintEventHandler(this.MarkWriteControl_Paint);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
           
        }

        private void OnMouse_Wheel(object sender, MouseEventArgs e)
        {
            if (_histogramParamList == null)
                return;
            float tempValue = (float)Math.Truncate(_drawMaxHistogramValue + (((float)e.Delta) / 1000));

            if (((float)e.Delta) / 1000 >= 0)
                tempValue += 10;
            else
                tempValue -= 10;

            if (tempValue <= _minHistogramValue)
                return;
            if (tempValue >= MaxHistogramValue)
                tempValue = MaxHistogramValue;

            _drawMaxHistogramValue = tempValue;

            if (YAxisUpdateDelegate != null)
                YAxisUpdateDelegate();

            DrawControl.Invalidate();
        }

        public void SetHistogramParam(List<HistogramParams> paramList)
        {
            if (paramList == null)
                return;
            CalcMinMax(paramList);
        }

        private void CalcMinMax(List<HistogramParams> paramList)
        {
            try
            {
                if (paramList == null)
                    return;
                if (paramList.Count <= 0)
                    return;

                float max = -1 * Int32.MaxValue;
                float min = Int32.MaxValue;
                _histogramParamList = paramList;

                LevelingMaxValue = _histogramParamList[0].HistogramValue.Length - 1;

                foreach (HistogramParams param in _histogramParamList)
                {
                    for (int i = 0; i < param.HistogramValue.Length; i++)
                    {
                        if (param.HistogramValue[i] > max)
                            max = param.HistogramValue[i];
                        if (param.HistogramValue[i] < min)
                            min = param.HistogramValue[i];
                    }
                    //if (min < 0)
                    //  min = 0;
                }
                _maxHistogramValue = max;
                _drawMaxHistogramValue = _maxHistogramValue;
                _minHistogramValue = min;
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
   
        }

        public void DrawControlInvalidate()
        {
            DrawControl.Invalidate();
            if(MarkVisible)
            {
                SetMark();
                SendMarkInfo();
            }
        }

        private void DrawControl_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.Clear(Color.White);
                if (_histogramParamList == null)
                {
                    DrawInitializeGrid(e.Graphics);
                    return;
                }

                DrawDotGrid(e.Graphics);
                if (_histogramParamList.Count >= 0)
                {
                    if (DrawType == HistogramPanelDrawType.Histogram)
                    {
                        DrawHistogram(e.Graphics);
                    }
                    else if (DrawType == HistogramPanelDrawType.Derivative)
                    {
                        DrawDerivativeHistogram(e.Graphics);
                    }
                }
                //Console.WriteLine("Mark 1 : " + this._mark1Value + "   Mark2 : " + this._mark2Value);
                if(MarkVisible)
                {
                    DrawMark(e.Graphics);
                    MarkWriteControl.Invalidate();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
      
        }

        private void DrawInitializeGrid(Graphics g)
        {
            Pen dotPen = new Pen(Color.Gray, 1);
            dotPen.DashStyle = DashStyle.Dash;

            Rectangle rect = DrawControl.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            g.DrawRectangle(new Pen(Color.Black), rect);

            int regionCount = 4;
            int xInterval = DrawControl.Width / regionCount;
            for (int i = 1; i < regionCount; i++)
            {
                PointF p1 = new PointF(xInterval * i, 0);
                PointF p2 = new PointF(xInterval * i, DrawControl.Height - 1);
                g.DrawLine(dotPen, p1, p2);
            }

            int yInterval = DrawControl.Height / regionCount;
            for (int i = 1; i < regionCount; i++)
            {
                PointF p1 = new PointF(0, yInterval * i);
                PointF p2 = new PointF(DrawControl.Width - 1, yInterval * i);
                g.DrawLine(dotPen, p1, p2);
            }
        }
        private void MarkWriteControl_Paint(object sender, PaintEventArgs e)
        {
            if (_histogramParamList == null)
                return;
            if (!MarkVisible)
                return;
            Font font = new Font("고딕", 7);
            Brush brush = Brushes.Black;
            Pen pen = new Pen(Color.Black);
            string stringMark1 = "Mark1";
            string stringMark2 = "Mark2";
            SizeF mark1Size = e.Graphics.MeasureString(stringMark1, font);
            SizeF mark2Size = e.Graphics.MeasureString(stringMark2, font);
            float mark1Pos = calcMarkPoint(_mark1Value);
            float mark2Pos = calcMarkPoint(_mark2Value);

            if (mark1Pos < 0) mark1Pos = 0;
            if (mark1Pos >= DrawControl.Width) mark1Pos = DrawControl.Width - 1;

            if (mark2Pos < 0) mark2Pos = 0;
            if (mark2Pos >= DrawControl.Width) mark2Pos = DrawControl.Width - 1;

            float mark1StringPositionX = mark1Pos - (mark1Size.Width / 2);
            float mark1StringPositionY = MarkWriteControl.Height - mark1Size.Height;

            if (mark1StringPositionX < 0) mark1StringPositionX = 0;
            if (mark1StringPositionX + mark1Size.Width / 2 > MarkWriteControl.Width - 1) mark1StringPositionX = MarkWriteControl.Width - 1 - mark1Size.Width;

            float mark2StringPositionX = mark2Pos - (mark2Size.Width / 2);
            float mark2StringPositionY = MarkWriteControl.Height - mark2Size.Height;

            if (mark2StringPositionX < 0) mark2StringPositionX = 0;
            if (mark2StringPositionX + mark2Size.Width > MarkWriteControl.Width - 1) mark2StringPositionX = MarkWriteControl.Width - 1 - mark1Size.Width;

            PointF mark1 = new PointF(mark1StringPositionX, mark1StringPositionY);
            PointF mark2 = new PointF(mark2StringPositionX, mark2StringPositionY);
            e.Graphics.DrawString(stringMark1, font, brush, mark1);
            e.Graphics.DrawString(stringMark2, font, brush, mark2);
        }

        private void DrawControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (_histogramParamList == null)
                return;
            _drawMode = eModeType.None;
            _markTrackPos = eMarkTrackPos.None;
        }

        private void DrawControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isActive)
                return;
            if (_histogramParamList == null)
                return;
            Point pt = DrawControl.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
            LevelingMaxValue = _histogramParamList[0].HistogramValue.Length - 1;
            this.DrawControl.Focus();
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (_drawMode == eModeType.Edit)
                {
                    if (_markTrackPos == eMarkTrackPos.Mark1)
                    {
                        _mark1Value = GetHistogramLevelValue(pt.X);
                        SetMark();
                        SendMarkInfo();
                    }
                    if (_markTrackPos == eMarkTrackPos.Mark2)
                    {
                        float g = this.DrawControl.Left;
                        _mark2Value = GetHistogramLevelValue(pt.X);
                        //Console.WriteLine("Pt : " + pt.X + "   Panel : " + this.DrawControl.Width.ToString() + " Mark2 : " + _mark2Value);
                        SetMark();
                        SendMarkInfo();
                    }
                }
                DrawControl.Invalidate();
            }
            else
            {
                if(MarkVisible)
                    SetMarkTracker(pt);
            }
        }

        private void DrawControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (_markTrackPos != eMarkTrackPos.None)
                    _drawMode = eModeType.Edit;
                else
                    _drawMode = eModeType.None;
            }
        }

        private int GetHistogramLevelValue(float point)
        {
            int level = 0;//

            if (point <= 0)
                level = 0;
            else if (point >= DrawControl.Width - 1)
                level = this._histogramParamList[0].HistogramValue.Length - 1;
            else
                level = (int)((point * (float)(this._histogramParamList[0].HistogramValue.Length - 1)) / (float)DrawControl.Width);
            return level;
        }

        public void ResetMark()
        {
            try
            {
                if (_histogramParamList == null)
                    return;
                _mark1Value = 0;
                _mark2Value = LevelingMaxValue;


                int startNum = 0;
                int endNum = 0;

                if (_mark1Value < _mark2Value)
                {
                    startNum = _mark1Value;
                    endNum = _mark2Value;
                }
                else
                {
                    startNum = _mark2Value;
                    endNum = _mark1Value;
                }

                CalcMarkInfo(startNum, endNum);
                SendMarkInfo();
                MarkWriteControl.Invalidate();
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
            //Console.WriteLine("ResetMark");

        }
        private void SetMark()
        {
            try
            {
                //Console.WriteLine("SetMark 전 Mark1 : " + _mark1Value + " Mark2 : " + _mark2Value);
                if (_mark1Value < 0) _mark1Value = 0;
                if (_mark1Value >= LevelingMaxValue) _mark1Value = LevelingMaxValue;

                if (_mark2Value < 0) _mark2Value = 0;
                if (_mark2Value >= LevelingMaxValue) _mark2Value = LevelingMaxValue;

                int startNum = 0;
                int endNum = 0;

                if (_mark1Value < _mark2Value)
                {
                    startNum = _mark1Value;
                    endNum = _mark2Value;
                }
                else
                {
                    startNum = _mark2Value;
                    endNum = _mark1Value;
                }
                CalcMarkInfo(startNum, endNum);

                //Console.WriteLine("SetMark 후 Mark1 : " + _mark1Value + " Mark2 : " + _mark2Value);
                MarkWriteControl.Invalidate();
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
    
        }

        private void CalcMarkInfo(int startNum, int endNum)
        {
            try
            {
                int max = -1 * Int32.MaxValue;
                int min = Int32.MaxValue;
                int sum = 0;
                int size = 0;
                int area = 0;
                if (_histogramParamList == null)
                    return;
                if (_histogramParamList.Count == 1)
                {
                    size = _histogramParamList[0].Width * _histogramParamList[0].Height;
                       
                    for (int i = startNum; i <=endNum; i++)
                    {
                        int value = Convert.ToInt32(_histogramParamList[0].HistogramValue[i]);
                        sum += value * i;
                        area += value;
                        if (value > max)
                        {
                            max = value;
                        }
                        if (value < min)
                            min = value;
                    }

                    MarkInfo.AreaMin = min;
                    MarkInfo.AreaMax = max;
    
                    MarkInfo.Area = area;
                    MarkInfo.AreaMean = (float)sum / size;
                }
                else
                {
                    foreach (HistogramParams param in _histogramParamList)
                    {
                        size = param.Width * param.Height;

                        max = -1 * Int32.MaxValue;
                        min = Int32.MaxValue;
                        sum = 0;
                        area = 0;
                        for (int i = startNum; i <= endNum; i++)
                        {
                            int value = Convert.ToInt32(param.HistogramValue[i]);
                            sum += value * i;
                            area += value;
            
                            if (param.HistogramValue[i] > max)
                            {
                                max = Convert.ToInt32(param.HistogramValue[i]);
                            }
                            if (param.HistogramValue[i] < min)
                                min = Convert.ToInt32(param.HistogramValue[i]);
                        }
           
                        if (param.GraphColor == Color.Red)
                        {
                            MarkInfo.RedAreaMin = min;
                            MarkInfo.RedAreaMax = max;

                            MarkInfo.RedArea = area;
                            MarkInfo.RedAreaMean = sum / size;
                        }
                        if (param.GraphColor == Color.Green)
                        {
                            MarkInfo.GreenAreaMin = min;
                            MarkInfo.GreenAreaMax = max;

                            MarkInfo.GreenArea = area;
                            MarkInfo.GreenAreaMean = sum / size;
                        }
                        if (param.GraphColor == Color.Blue)
                        {
                            MarkInfo.BlueAreaMin = min;
                            MarkInfo.BlueAreaMax = max;

                            MarkInfo.BlueArea = area;
                            MarkInfo.BlueAreaMean = sum / size;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
           
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

        private void DrawDotGrid(Graphics g)
        {
            Pen dotPen = new Pen(Color.Gray, 1);
            dotPen.DashStyle = DashStyle.Dash;
            Rectangle rect = new Rectangle(0, 0, DrawControl.Width -1 , DrawControl.Height -1);
            g.DrawRectangle(new Pen(Color.Black), rect);

            int regionCount = 4;
            int xInterval = DrawControl.Width / regionCount;
            for (int i = 1; i < regionCount; i++)
            {
                PointF p1 = new PointF(xInterval * i, 0);
                PointF p2 = new PointF(xInterval * i, DrawControl.Height - 1);
                g.DrawLine(dotPen, p1, p2);
            }

            if (DrawType == HistogramPanelDrawType.Histogram)
            {
                int yInterval = DrawControl.Height / regionCount;
                for (int i = 1; i < regionCount; i++)
                {
                    PointF p1 = new PointF(0, yInterval * i);
                    PointF p2 = new PointF(DrawControl.Width - 1, yInterval * i);
                    g.DrawLine(dotPen, p1, p2);
                }
            }
            else if (DrawType == HistogramPanelDrawType.Derivative)
            {
                if (DrawMaxHistogramValue < 0)
                    return;
                float standard = (Math.Abs(DrawMaxHistogramValue) * DrawControl.Height / (DrawMaxHistogramValue - MinHistogramValue));
                if (DrawControl.Height > standard)
                    g.DrawLine(dotPen, new PointF(0 ,standard), new PointF(DrawControl.Width - 1, standard));
                else
                {
                    int yInterval = DrawControl.Height / regionCount;
                    for (int i = 1; i < regionCount; i++)
                    {
                        PointF p1 = new PointF(0, yInterval * i);
                        PointF p2 = new PointF(DrawControl.Width - 1, yInterval * i);
                        g.DrawLine(dotPen, p1, p2);
                    }
                }
            }
        }

        private void DrawDerivativeHistogram(Graphics g)
        {
            try
            {
                if (MaxHistogramValue == 0 || MaxHistogramValue == MinHistogramValue)
                    return;
                if (LevelingMaxValue == 1)
                    LevelingMaxValue = 2;
                float xInterval = (float)DrawControl.Width / (LevelingMaxValue);
                float yInterval = 0;
                float standard = DrawControl.Height;

                yInterval = (float)DrawControl.Height / (DrawMaxHistogramValue - MinHistogramValue);
                standard = (Math.Abs(DrawMaxHistogramValue) * DrawControl.Height / (DrawMaxHistogramValue - MinHistogramValue));

                if (DrawMaxHistogramValue <= 0)
                    standard = standard * -1;
                foreach (HistogramParams param in _histogramParamList)
                {
                    PointF[] pointGroup = new PointF[param.HistogramValue.Length];
                    for (int i = 0; i < param.HistogramValue.Length; i++)
                    {
                        float newXPos = (i * xInterval);
                        float newYpos = (standard - (param.HistogramValue[i] * yInterval));

                        if (newYpos > DrawControl.Height)
                            newYpos = DrawControl.Height;
                        if (newYpos <= 0)
                            newYpos = 0;
                        pointGroup[i].X = newXPos;
                        if (newYpos == DrawControl.Height)
                        {
                            newYpos -= 1;
                        }

                        pointGroup[i].Y = newYpos;

                    }
                    g.DrawLines(new Pen(param.GraphColor), pointGroup);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
      
        }

        private void DrawHistogram(Graphics g)
        {
            try
            {
                if (MaxHistogramValue == 0)
                    return;

                float xInterval = (float)DrawControl.Width / (LevelingMaxValue);
                float yInterval = (float)DrawControl.Height / (DrawMaxHistogramValue);
                if(LevelingMaxValue == 1)
                {
                    xInterval = (float)DrawControl.Width;
                }
                foreach (HistogramParams param in _histogramParamList)
                {
                    PointF[] pointGroup = new PointF[param.HistogramValue.Length];
                    for (int i = 0; i < param.HistogramValue.Length; i++)
                    {
                        float newXPos = (i * xInterval);
                        float newYpos = ((float)DrawControl.Height - (param.HistogramValue[i] * yInterval));
                        float te = param.HistogramValue[i];
                        if (newYpos > DrawControl.Height)
                            newYpos = DrawControl.Height;
                        if (newYpos <= 0)
                            newYpos = 0;
                        pointGroup[i].X = newXPos;
                        if (newYpos == DrawControl.Height)
                        {
                            newYpos -= 1;
                        }

                        pointGroup[i].Y = newYpos;

                    }
                    g.DrawLines(new Pen(param.GraphColor), pointGroup);
                }
            }
            catch (Exception err)
            {

                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
                throw;
            }


        }

        private void DrawMark(Graphics g)
        {
            float mark1Pos = calcMarkPoint(_mark1Value);
            float mark2Pos = calcMarkPoint(_mark2Value);

            Pen pen = new Pen(Color.Blue, 2);

            if (mark1Pos <= 0) mark1Pos = 1;
            if (mark1Pos >= DrawControl.Width) mark1Pos = DrawControl.Width - 1;

            if (mark2Pos <= 0) mark2Pos = 1;
            if (mark2Pos >= DrawControl.Width) mark2Pos = DrawControl.Width - 1;

            g.DrawLine(pen, new PointF(mark1Pos, 0), new PointF(mark1Pos, DrawControl.Height - 1));
            g.DrawLine(pen, new PointF(mark2Pos, 0), new PointF(mark2Pos, DrawControl.Height - 1));
        }

        private float calcMarkPoint(int value)
        {
            return (float)(value) * DrawControl.Width / LevelingMaxValue;
        }

        private void HistogramDrawingPanel_SizeChanged(object sender, EventArgs e)
        {
            DrawControl.Invalidate();
            if (MarkVisible)
                MarkWriteControl.Invalidate();
        }

        private void SetMarkTracker(Point point)
        {
            float mark1Pos = calcMarkPoint(_mark1Value);
            float mark2Pos = calcMarkPoint(_mark2Value);
            int interval = 4;

            if (mark1Pos < 0) mark1Pos = 0;
            if (mark1Pos >= DrawControl.Width) mark1Pos -= 1;

            if (mark2Pos < 0) mark2Pos = 0;
            if (mark2Pos >= DrawControl.Width) mark2Pos -= 1;

            if (mark1Pos + interval > point.X && mark1Pos - interval < point.X)
            {
                _markTrackPos = eMarkTrackPos.Mark1;
                Cursor.Current = Cursors.SizeWE;
            }
            else if (mark2Pos + interval > point.X && mark2Pos - interval < point.X)
            {
                _markTrackPos = eMarkTrackPos.Mark2;
                Cursor.Current = Cursors.SizeWE;
            }
            else
            {
                _markTrackPos = eMarkTrackPos.None;
                Cursor.Current = Cursors.Default;
            }
        }

        private void SendMarkInfo()
        {
            try
            {
                if (MarkVisible)
                {
                    //Console.WriteLine("Mark : " + _mark1Value + "    Mark2 : " + _mark2Value);
                    if (MarkValueDelegate != null)
                    {
                        if(_mark1Value > _histogramParamList[0].HistogramValue.Count() - 1)
                        {
                            _mark1Value = _histogramParamList[0].HistogramValue.Count() - 1;
                        }
                        if (_mark2Value > _histogramParamList[0].HistogramValue.Count() - 1)
                        {
                            _mark2Value = _histogramParamList[0].HistogramValue.Count() - 1;
                        }

                        if (_histogramParamList.Count == 1)
                        {
                            MarkInfo.Mark1Value = _histogramParamList[0].HistogramValue[_mark1Value];
                            MarkInfo.Mark2Value = _histogramParamList[0].HistogramValue[_mark2Value];
                            MarkInfo.Mark1Index = _mark1Value;
                            MarkInfo.Mark2Index = _mark2Value;
                        }
                        if (_histogramParamList.Count == 3)
                        {
                            MarkInfo.Mark1Index = _mark1Value;
                            MarkInfo.Mark2Index = _mark2Value;
                            foreach (HistogramParams param in _histogramParamList)
                            {
                                if (param.GraphColor == Color.Red)
                                {
                                    MarkInfo.RedMark1Value = param.HistogramValue[_mark1Value];
                                    MarkInfo.RedMark2Value = param.HistogramValue[_mark2Value];
                                }
                                if (param.GraphColor == Color.Green)
                                {
                                    MarkInfo.GreenMark1Value = param.HistogramValue[_mark1Value];
                                    MarkInfo.GreenMark2Value = param.HistogramValue[_mark2Value];
                                }
                                if (param.GraphColor == Color.Blue)
                                {
                                    MarkInfo.BlueMark1Value = param.HistogramValue[_mark1Value];
                                    MarkInfo.BlueMark2Value = param.HistogramValue[_mark2Value];
                                }
                            }
                        }
                        if (MarkValueDelegate != null)
                            MarkValueDelegate(MarkInfo);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
       
        }

        public Size GetMarkPanelSize()
        {
            return new Size(MarkPanel.Width, MarkPanel.Height);
        }

        public void ReInitialize(List<HistogramParams> paramList)
        {
            try
            {
                if(paramList !=null)
                {
                    CalcMinMax(paramList);

                    if (DrawControl != null)
                    {
                        DrawControl.Invalidate();
                    }

                    if (MarkWriteControl != null)
                    {
                        if (MarkVisible)
                        {
                            this._mark1Value = paramList[0].Mark1Value;
                            this._mark2Value = paramList[0].Mark2Value;

                            SetMark();
                            SendMarkInfo();
                            MarkWriteControl.Invalidate();
                        }
                    }
                }
                else
                {
                    if (DrawControl != null)
                    {
                        if(_histogramParamList != null)
                            _histogramParamList.Clear();
                        _histogramParamList = null;
                        DrawControl.Invalidate();
                    }
                }

            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
       
        }
    }

    public class HistogramParams
    {
        private bool _isDerivative = false;
        public bool IsDerivative
        {
            get { return _isDerivative; }
            set { _isDerivative = value; }
        }
        public Color GraphColor { get; set; }
        public float[] HistogramValue { get; set; }
        public List<PointF> Points { get; set; }

        public int Mark1Value { get; set; }
        public int Mark2Value { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
