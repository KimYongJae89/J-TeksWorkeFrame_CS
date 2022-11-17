using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageManipulator.ImageProcessingData;

namespace ImageManipulator.Controls
{
    public partial class LevelingControl : UserControl
    {
        public Action FilterEdit;
        HistogramPanel _histogramPanel;
        SliderDrawingPanel _slider;
        List<HistogramParams> _histogramParamList;
        DoubleBufferPanel YAxisControl;

        //private bool _markVisible = true;
        private int _lowLevel = 0;
        private int _highLevel = 0;
        private float _mark1Value = 0;
        private float _mark2Value = 0;
        private float _markMin = 0;
        private float _markMax = 0;

        private bool _nupdnMinIgnore = false;
        private bool _nupdnMaxIgnore = false;

        //public event Action<int, int> SliderUpdate; //min, max

        public bool IsActive
        {
            get
            {
                if (_histogramPanel == null)
                    return false;
                return _histogramPanel.isActive;
            }
            set
            {
                if (_histogramPanel != null)
                    _histogramPanel.isActive = value;
            }
        }

        public LevelingControl()
        {
            InitializeComponent();
        }

        private void HistogramControl_Load(object sender, EventArgs e)
        {
            nupdnInitializate();

            _histogramPanel = new HistogramPanel();
            _histogramPanel.MarkVisible = false;
            _histogramPanel.SetHistogramParam(Status.Instance().SelectedViewer.ImageManager.GetSelectedViewerHistogramParam());
            _histogramPanel.YAxisUpdateDelegate += UpdateYAxis;


            DisplayPanel.Controls.Add(this._histogramPanel);
            this._histogramPanel.Left = 0;
            this._histogramPanel.Top = 0;
            _histogramPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            _histogramPanel.TabStop = false;


            _slider = new SliderDrawingPanel();
            //수정
            _slider.SetSliderMinMax(0,Status.Instance().SelectedViewer.ImageManager.GetHistogramMax());
            _slider.UpdateSliderMinMax += UpdateSliderMinMax;
            SliderPanel.Controls.Add(_slider);
            this._slider.Left = 0;
            this._slider.Top = 0;
            _slider.Dock = System.Windows.Forms.DockStyle.Fill;

            //_slider.TabStop = false;

            ////_yAxisControl

            YAxisControl = new DoubleBufferPanel();
            YaxisPanel.Controls.Add(YAxisControl);
            YAxisControl.Dock = System.Windows.Forms.DockStyle.Fill;
            YAxisControl.Paint += new System.Windows.Forms.PaintEventHandler(this.YAxisControl_Paint);

        
        }

        public void nupdnInitializate()
        {
            if (Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Depth == KiyLib.DIP.KDepthType.Dt_8)
            {
                nupdnMin.Minimum = nupdnMax.Minimum = 0;
                nupdnMin.Maximum = nupdnMax.Maximum = 255;
            }
            else if (Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Depth == KiyLib.DIP.KDepthType.Dt_16)
            {
                nupdnMin.Minimum = nupdnMax.Minimum = 0;
                nupdnMin.Maximum = nupdnMax.Maximum = 65535;
            }
            else
            {
                nupdnMin.Minimum = nupdnMax.Minimum = 0;
                nupdnMin.Maximum = nupdnMax.Maximum = 255;
            }
            int min = 0;
            int max = Status.Instance().SelectedViewer.ImageManager.GetHistogramMax();

            if (nupdnMin.Maximum < min)
                min = Convert.ToInt32(nupdnMin.Maximum);

            if (nupdnMin.Maximum < max)
                max = Convert.ToInt32(nupdnMin.Maximum);

            nupdnMin.Value = min;
            nupdnMax.Value = max;
        }

        private void UpdateYAxis()
        {
            if (YAxisControl != null)
                YAxisControl.Invalidate();
        }

        private void YAxisControl_Paint(object sender, PaintEventArgs e)
        {
            //float max = _histogramPanel.MaxHistogramValue;
            float max = _histogramPanel.DrawMaxHistogramValue;
            //float min = _histogramPanel.MinHistogramValue;
            float min = 0;

            e.Graphics.Clear(Control.DefaultBackColor);
            Brush brush = Brushes.Black;
            Font font = new Font("고딕", 10);
            // HistogramValue 중 최대 값
            Size markPanelSize = _histogramPanel.GetMarkPanelSize();
            SizeF stringSize = e.Graphics.MeasureString(max.ToString(), font);
            PointF stringMaxValue = new PointF(YaxisPanel.Width - stringSize.Width, markPanelSize.Height - (stringSize.Height / 2));
            e.Graphics.DrawString(max.ToString(), font, brush, stringMaxValue);

            // HistogramValue가 0 일 경우
            stringSize = e.Graphics.MeasureString(min.ToString(), font);
            Size histogramSize = DisplayPanel.Size;
            PointF stringMinValue = new PointF(YaxisPanel.Width - stringSize.Width, histogramSize.Height - (stringSize.Height / 2));
            e.Graphics.DrawString(min.ToString(), font, brush, stringMinValue);
        }

        private void UpdateMarkValue(tMarkInfo markInfo)
        {
            _mark1Value = markInfo.Mark1Value;
            _mark2Value = markInfo.Mark2Value;
            _markMin = markInfo.AreaMin;
            _markMax = markInfo.AreaMax;
        }

        public void SetHistogramParam(List<HistogramParams> paramList)
        {
            _histogramParamList = paramList;
        
        }

        public void FormUpdate(HistogramParams param)
        {
            //_histogramParam = param;
            //_histogramPanel.SetHistogramParam(_histogramParam);
            //_histogramPanel.DrawControlInvalidate();
        }

        public void UpdateSliderMinMax(int min, int max)
        {
            _lowLevel = min;
            _highLevel = max;

            _nupdnMinIgnore = true;
            _nupdnMaxIgnore = true;

            nupdnMin.Value = min;
            nupdnMax.Value = max;

            if (FilterEdit != null)
                FilterEdit();
            //if (SliderUpdate != null)
            //{

            //    SliderUpdate(min, max);
            //}
        }

        private void GreyHistogramForm_SizeChanged(object sender, EventArgs e)
        {
            // YaxisPanel.Invalidate();
            if (YAxisControl != null)
                YAxisControl.Invalidate();
        }

        private void HistogramControl_SizeChanged(object sender, EventArgs e)
        {
            if(YAxisControl != null)
                YAxisControl.Invalidate();
        }

        public int GetLowLevel()
        {
            return _lowLevel;
        }

        public int GetHighLevel()
        {
            return _highLevel;
        }
        public void SetSliderValue(int min, int max)
        {
            if (_slider != null)
                _slider.SetSliderValue(min, max);
        }
        public void SliderInitialize()
        {
            if(_slider != null)
                _slider.Initialize();
        }

        public void UpdateHistogram()
        {
            if (_histogramParamList == null)
                return;
            
            if (_histogramPanel != null)
            {
                _histogramPanel.SetHistogramParam(_histogramParamList);
                _histogramPanel.DrawControlInvalidate();
            }
            if(_slider !=null)
            {
                //_slider.SetSliderParam(_histogramParamList[0].ImageBit, _histogramParamList[0].IsColor);
                _slider.SetSliderMinMax(0, _histogramParamList[0].HistogramValue.Count() - 1);
                _slider.Initialize();
            }
            if(YAxisControl !=null)
                YAxisControl.Invalidate();
        }

        private void nupdnMin_ValueChanged(object sender, EventArgs e)
        {
            if (_nupdnMinIgnore)
            {
                _nupdnMinIgnore = false;
                return;
            }
            int min = (int)nupdnMin.Value;
            int max = (int)nupdnMax.Value;

            nupdnMin.Maximum = max;

            SetSliderValue(min, max);
        }

        private void nupdnMax_ValueChanged(object sender, EventArgs e)
        {
            if (_nupdnMaxIgnore)
            {
                _nupdnMaxIgnore = false;
                return;
            }

            int min = (int)nupdnMin.Value;
            int max = (int)nupdnMax.Value;

            nupdnMax.Minimum = min;
            nupdnMin.Maximum = max;

            SetSliderValue(min, max);
        }

        public void EditParam(ref IpLevelingParams param)
        {
            if(nupdnMin.Value != nupdnMax.Value)
            {
                param.Low = Convert.ToInt32(nupdnMin.Value);
                param.High = Convert.ToInt32(nupdnMax.Value);
            }

            if(_slider !=null)
                _slider.SetSliderValue(param.Low, param.High, false);
        }

        public void UpdateHistogram(JImage image)
        {
            if (image == null)
                return;
            if (_histogramParamList == null)
                _histogramParamList = new List<HistogramParams>();
            _histogramParamList.Clear();
                
            int[] greyHistogram;

            if(image.Color != KiyLib.DIP.KColorType.Color)
            {
                image.GetHistoGray(out greyHistogram);

                float[] ret = Array.ConvertAll(greyHistogram, new Converter<int, float>(ConvertIntToFloat));

                HistogramParams param = new HistogramParams();
                param.HistogramValue = ret;
                param.GraphColor = Color.Gray;
                param.Width = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width;
                param.Height = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height;
                _histogramParamList.Add(param);
            }
            else
            {
                ColorHistogram param = new ColorHistogram();
                image.GetHistoColor(out param.B, out param.G, out param.R);

                HistogramParams redParam = new HistogramParams();
                redParam.HistogramValue = Array.ConvertAll(param.R, new Converter<int, float>(ConvertIntToFloat));
                redParam.GraphColor = Color.Red;
                redParam.Width = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width;
                redParam.Height = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height;
                ////
                HistogramParams greenParam = new HistogramParams();
                greenParam.HistogramValue = Array.ConvertAll(param.G, new Converter<int, float>(ConvertIntToFloat));
                greenParam.GraphColor = Color.Green;
                greenParam.Width = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width;
                greenParam.Height = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height;
                ////
                HistogramParams blueParam = new HistogramParams();
                blueParam.HistogramValue = Array.ConvertAll(param.B, new Converter<int, float>(ConvertIntToFloat));
                blueParam.GraphColor = Color.Blue;
                blueParam.Width = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width;
                blueParam.Height = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height;

                _histogramParamList.Add(redParam);
                _histogramParamList.Add(greenParam);
                _histogramParamList.Add(blueParam);
            }

            if (_histogramPanel != null)
            {
                _histogramPanel.SetHistogramParam(_histogramParamList);
                _histogramPanel.DrawControlInvalidate();
            }
          
            if (YAxisControl != null)
                YAxisControl.Invalidate();
        }

        private float ConvertIntToFloat(int input)
        {
            return (float)input;
        }
    }
}
