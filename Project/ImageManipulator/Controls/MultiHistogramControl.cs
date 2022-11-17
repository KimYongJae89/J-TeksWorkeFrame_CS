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
    public partial class MultiHistogramControl : UserControl
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
        public MultiHistogramControl()
        {
            InitializeComponent();
        }

        private void MultiHistogramControl_Load(object sender, EventArgs e)
        {
            ControlAdd();
            DataGridNewUpdate();
            //if (_histogramParamList.Count() == 1)
            //    DataGridNewUpdate(false);
            //else
            //    DataGridNewUpdate(true);
        }

        private void ControlAdd()
        {
            _HistogramPanel = new HistogramPanel();
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
        public void DataGridNewUpdate()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    DataGridNewUpdateDele callback = DataGridNewUpdate;
                    Invoke(callback);
                    return;

                }
                dataGridViewInfo.Columns.Clear();
                if (!IsColor)
                {
                    dataGridViewInfo.Columns.Add("Index", "Index");
                    dataGridViewInfo.Columns.Add("Count", "Count");
                }
                else
                {
                    dataGridViewInfo.Columns.Add("Index", "Index");
                    dataGridViewInfo.Columns.Add("CountR", "Count R");
                    dataGridViewInfo.Columns.Add("CountG", "Count G");
                    dataGridViewInfo.Columns.Add("CountB", "Count B");

                }
                int index = dataGridViewInfo.Rows.Add();
                dataGridViewInfo.Rows[index].HeaderCell.Value = "Mark1";
                index = dataGridViewInfo.Rows.Add();
                dataGridViewInfo.Rows[index].HeaderCell.Value = "Mark2";

                index = dataGridViewInfo.Rows.Add();
                dataGridViewInfo.Rows[index].HeaderCell.Value = "Min";

                index = dataGridViewInfo.Rows.Add();
                dataGridViewInfo.Rows[index].HeaderCell.Value = "Max";

                index = dataGridViewInfo.Rows.Add();
                dataGridViewInfo.Rows[index].HeaderCell.Value = "Area";

                index = dataGridViewInfo.Rows.Add();
                dataGridViewInfo.Rows[index].HeaderCell.Value = "Mean";

                foreach (DataGridViewColumn column in dataGridViewInfo.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                ColumnHeaderUpdate(_markInfo);
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
       
        }

        private void UpdateMarkValue(tMarkInfo markinfoList)
        {
            if(dataGridViewInfo.Rows.Count == 0)
            {
                DataGridNewUpdate();
            }
            
            ColumnHeaderUpdate(markinfoList);

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
                    ColumnHeaderUpdate(_markInfo);
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

        private void ColumnHeaderUpdate(tMarkInfo markinfoList)
        {
            try
            {
                dataGridViewInfo.CurrentCell = null;
                _markInfo = markinfoList;
                if (dataGridViewInfo.Rows.Count <= 0)
                    return;
                if (!IsColor)
                {
                    dataGridViewInfo.Rows[0].Cells["Index"].Value = markinfoList.Mark1Index;
                    dataGridViewInfo.Rows[0].Cells["Count"].Value = markinfoList.Mark1Value;

                    dataGridViewInfo.Rows[1].Cells["Index"].Value = markinfoList.Mark2Index;
                    dataGridViewInfo.Rows[1].Cells["Count"].Value = markinfoList.Mark2Value;

                    dataGridViewInfo.Rows[2].Cells["Index"].Value = "-";
                    dataGridViewInfo.Rows[2].Cells["Count"].Value = markinfoList.AreaMin;

                    dataGridViewInfo.Rows[3].Cells["Index"].Value = "-";
                    dataGridViewInfo.Rows[3].Cells["Count"].Value = markinfoList.AreaMax;

                    dataGridViewInfo.Rows[4].Cells["Index"].Value = "-";
                    dataGridViewInfo.Rows[4].Cells["Count"].Value = markinfoList.Area;

                    dataGridViewInfo.Rows[5].Cells["Index"].Value = "-";
                    dataGridViewInfo.Rows[5].Cells["Count"].Value = markinfoList.AreaMean;
                }
                else
                {
                    dataGridViewInfo.Rows[0].Cells["Index"].Value = markinfoList.Mark1Index;
                    dataGridViewInfo.Rows[0].Cells["CountR"].Value = markinfoList.RedMark1Value;
                    dataGridViewInfo.Rows[0].Cells["CountG"].Value = markinfoList.GreenMark1Value;
                    dataGridViewInfo.Rows[0].Cells["CountB"].Value = markinfoList.BlueMark1Value;

                    dataGridViewInfo.Rows[1].Cells["Index"].Value = markinfoList.Mark2Index;
                    dataGridViewInfo.Rows[1].Cells["CountR"].Value = markinfoList.RedMark2Value;
                    dataGridViewInfo.Rows[1].Cells["CountG"].Value = markinfoList.GreenMark2Value;
                    dataGridViewInfo.Rows[1].Cells["CountB"].Value = markinfoList.BlueMark2Value;

                    dataGridViewInfo.Rows[2].Cells["Index"].Value = "-";
                    dataGridViewInfo.Rows[2].Cells["CountR"].Value = markinfoList.RedAreaMin;
                    dataGridViewInfo.Rows[2].Cells["CountG"].Value = markinfoList.GreenAreaMin;
                    dataGridViewInfo.Rows[2].Cells["CountB"].Value = markinfoList.BlueAreaMin;

                    dataGridViewInfo.Rows[3].Cells["Index"].Value = "-";
                    dataGridViewInfo.Rows[3].Cells["CountR"].Value = markinfoList.RedAreaMax;
                    dataGridViewInfo.Rows[3].Cells["CountG"].Value = markinfoList.GreenAreaMax;
                    dataGridViewInfo.Rows[3].Cells["CountB"].Value = markinfoList.BlueAreaMax;

                    dataGridViewInfo.Rows[4].Cells["Index"].Value = "-";
                    dataGridViewInfo.Rows[4].Cells["CountR"].Value = markinfoList.RedArea;
                    dataGridViewInfo.Rows[4].Cells["CountG"].Value = markinfoList.GreenArea;
                    dataGridViewInfo.Rows[4].Cells["CountB"].Value = markinfoList.BlueArea;

                    dataGridViewInfo.Rows[5].Cells["Index"].Value = "-";
                    dataGridViewInfo.Rows[5].Cells["CountR"].Value = markinfoList.RedAreaMean;
                    dataGridViewInfo.Rows[5].Cells["CountG"].Value = markinfoList.GreenAreaMean;
                    dataGridViewInfo.Rows[5].Cells["CountB"].Value = markinfoList.BlueAreaMean;
                }
                dataGridViewInfo.CurrentCell = null;
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
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

        private void dataGridViewInfo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           // dataGridViewInfo.CurrentCell = null;
        }

        private void dataGridViewInfo_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dataGridViewInfo.CurrentCell = null;
        }

        private void dataGridViewInfo_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dataGridViewInfo.CurrentCell = null;
        }

        private void dataGridViewInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewInfo.CurrentCell = null;
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
