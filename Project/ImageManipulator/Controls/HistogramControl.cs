using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace ImageManipulator.Controls
{
    public partial class HistogramControl : UserControl
    {
        public HistogramPanelDrawType DrawType = HistogramPanelDrawType.Histogram;
        private HistogramPanel _HistogramPanel;
        List<HistogramParams> _histogramParamList;
        DoubleBufferPanel _yAxisControl;
        DoubleBufferPanel _xAxisControl;
        public bool IsColor = false;
        public int ImageBit = 8;
        private tMarkInfo _markInfo;
        public Action<tMarkInfo> MarkValueDelegate;

        public bool IsActive
        {
            get
            {
                if (_HistogramPanel == null)
                    return false;
                return _HistogramPanel.isActive;
            }
            set
            {
                if (_HistogramPanel != null)
                    _HistogramPanel.isActive = value;
            }
        }
        public HistogramControl()
        {
            InitializeComponent();
        }

        private void HistogramControl_Load(object sender, EventArgs e)
        {
            ControlAdd();
        }

        private void ControlAdd()
        {
            _HistogramPanel = new HistogramPanel();
            _HistogramPanel.MarkVisible = false;
            _HistogramPanel.DrawType = this.DrawType;
            _HistogramPanel.SetHistogramParam(_histogramParamList);
            _HistogramPanel.MarkValueDelegate += UpdateMarkValue;

            _HistogramPanel.YAxisUpdateDelegate += UpdateYAxis;
            DrawPanel.Controls.Add(this._HistogramPanel);
            this._HistogramPanel.Left = 0;
            this._HistogramPanel.Top = 0;
            _HistogramPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            _HistogramPanel.TabStop = false;
            
            _yAxisControl = new DoubleBufferPanel();
            YAxisPanel.Controls.Add(_yAxisControl);
            _yAxisControl.BackColor = Color.DimGray;
            _yAxisControl.Dock = System.Windows.Forms.DockStyle.Fill;
            _yAxisControl.Paint += new System.Windows.Forms.PaintEventHandler(this._yAxisControl_Paint);

            _xAxisControl = new DoubleBufferPanel();
            XAxisPanel.Controls.Add(_xAxisControl);
             _yAxisControl.BackColor = Color.DimGray;
            _xAxisControl.Dock = System.Windows.Forms.DockStyle.Fill;
            _xAxisControl.Paint += new System.Windows.Forms.PaintEventHandler(this._xAxisControl_Paint);
        }

        private void UpdateYAxis()
        {
            if (_yAxisControl != null)
                _yAxisControl.Invalidate();
        }

        public delegate void DataGridNewUpdateDele();

        private void UpdateMarkValue(tMarkInfo markinfoList)
        {
            if (MarkValueDelegate != null)
                MarkValueDelegate(markinfoList);
        }

        public void SetHistogramParam(List<HistogramParams> paramList)
        {
            _histogramParamList = paramList;
        }

        public void HistogramGraphReNewal(List<HistogramParams> paramList)
        {
            if(paramList != null)
            {
                if (paramList.Count <= 0)
                    return;

                _histogramParamList = paramList;
                if (_HistogramPanel != null)
                {
                    _HistogramPanel.ReInitialize(_histogramParamList);
                }
                if (_yAxisControl != null)
                    _yAxisControl.Invalidate();
                if (_xAxisControl != null)
                    _xAxisControl.Invalidate();
            }
            else
            {
                _histogramParamList = null;
                _HistogramPanel.ReInitialize(_histogramParamList);
                if (_yAxisControl != null)
                    _yAxisControl.Invalidate();
                if (_xAxisControl != null)
                    _xAxisControl.Invalidate();
            }
        }

        private void _xAxisControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Control.DefaultBackColor);
            //e.Graphics.Clear(Color.DimGray);
            if (_histogramParamList == null)
                return;

            Brush brush = Brushes.Black;
            Font font = new Font("고딕", 10);
            // Slider가 0 일 경우
            string minValue = "0";
            PointF stringSliderMinValue = new PointF(0, 0);
            e.Graphics.DrawString(minValue, font, brush, stringSliderMinValue);

            // Slider 최대값일 경우
            string sliderMaxValue = (_histogramParamList[0].HistogramValue.Length - 1).ToString();
            SizeF stringSize = e.Graphics.MeasureString(sliderMaxValue, font);
            PointF stringSliderMaxValue = new PointF(XAxisPanel.Width - stringSize.Width, 0);
            e.Graphics.DrawString(sliderMaxValue, font, brush, stringSliderMaxValue);
        }

        private void _yAxisControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Control.DefaultBackColor);
            //e.Graphics.Clear(Color.DimGray);
            if (_histogramParamList == null)
                return;
            float max = _HistogramPanel.DrawMaxHistogramValue;
            float min = _HistogramPanel.MinHistogramValue;

            Brush brush = Brushes.Black;
            Font font = new Font("고딕", 10);
            // HistogramValue 중 최대 값
            
            Size markPanelSize = _HistogramPanel.GetMarkPanelSize();
            SizeF stringSize = e.Graphics.MeasureString(max.ToString(), font);
            PointF stringMaxValue = new PointF(YAxisPanel.Width - stringSize.Width, markPanelSize.Height - (stringSize.Height / 2));
            e.Graphics.DrawString(max.ToString(), font, brush, stringMaxValue);

            if (_HistogramPanel.DrawType == HistogramPanelDrawType.Histogram)
            {
                // HistogramValue가 0 일 경우
                stringSize = e.Graphics.MeasureString(min.ToString(), font);
                Size histogramSize = _HistogramPanel.Size;
                PointF stringMinValue = new PointF(YAxisPanel.Width - stringSize.Width, histogramSize.Height - (stringSize.Height / 2));
                e.Graphics.DrawString(min.ToString(), font, brush, stringMinValue);
            }
            else if(_HistogramPanel.DrawType == HistogramPanelDrawType.Derivative)
            {
            
                // HistogramValue가 0 일 경우
                stringSize = e.Graphics.MeasureString(min.ToString(), font);
                Size histogramSize = _HistogramPanel.Size;
                PointF stringMinValue = new PointF(YAxisPanel.Width - stringSize.Width, histogramSize.Height - (stringSize.Height / 2));
                e.Graphics.DrawString(min.ToString(), font, brush, stringMinValue);

               float standard = (Math.Abs(max) * _HistogramPanel.DrawControl.Height / (max - min));

                if (_HistogramPanel.DrawMaxHistogramValue < 0)
                    return;
                string value = "0";
                stringSize = e.Graphics.MeasureString(value, font);
                Size zeroHistogramSize = _HistogramPanel.Size;
                PointF stringZeroValue = new PointF(YAxisPanel.Width - stringSize.Width, markPanelSize.Height +  standard - (stringSize.Height / 2));
                e.Graphics.DrawString(value, font, brush, stringZeroValue);
            }
        }

        private void MultiHistogramControl_Resize(object sender, EventArgs e)
        {
            if (_xAxisControl != null)
                _xAxisControl.Invalidate();
            if (_yAxisControl != null)
                _yAxisControl.Invalidate();
        }

        public void MarkReset()
        {
            _HistogramPanel.ResetMark();
        }

        public List<HistogramParams> GetHistogramParams()
        {
            return _histogramParamList;
        }
        /// <summary>
        /// 오리지날 이미지 크기 일때의 Mark1 좌표 출력
        /// </summary>
        /// <returns></returns>
        public PointF GetMark1Point()
        {
            if (_histogramParamList == null)
                return new PointF();

            return new PointF(_histogramParamList[0].Points[_markInfo.Mark1Index].X, _histogramParamList[0].Points[_markInfo.Mark1Index].Y);
        }
        /// <summary>
        /// 오리지날 이미지 크기 일때의 Mark2 좌표 출력
        /// </summary>
        /// <returns></returns>
        public PointF GetMark2Point()
        {
            if (_histogramParamList == null)
                return new PointF();

            return new PointF(_histogramParamList[0].Points[_markInfo.Mark2Index].X, _histogramParamList[0].Points[_markInfo.Mark2Index].Y);
        }
    }
}
