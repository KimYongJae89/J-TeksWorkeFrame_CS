using ImageManipulator.Controls;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulator.Forms
{
    public partial class HistogramForm : Form
    { 
        MultiHistogramControl _histogramControl;

        public Action<int, int> SliderUpdate; //현재 slider low, high 값
        public Action CloseEventDelegate; // 폼 종료 시 low,high 설정 값
        public HistogramForm()
        {
            InitializeComponent();
        }

        private void HistgoramForm_Load(object sender, EventArgs e)
        {
            try
            {
                Status.Instance().LogManager.AddLogMessage("Histogram Form Open", "");

                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
         ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
         ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

                _histogramControl = new MultiHistogramControl();
                _histogramControl.ImageBit = Status.Instance().SelectedViewer.GetBit();
                _histogramControl.IsColor = Status.Instance().SelectedViewer.IsColor();
                _histogramControl.SetHistogramParam(Status.Instance().SelectedViewer.ImageManager.GetSelectedViewerHistogramParam());

                MainPanel.Controls.Add(this._histogramControl);
                this._histogramControl.Left = 0;
                this._histogramControl.Top = 0;
                _histogramControl.Dock = System.Windows.Forms.DockStyle.Fill;

                _histogramControl.TabStop = false;

                this.MinimumSize = new Size(530, 500);
                this.Text = LangResource.Histogram;
            }
            catch (Exception err )
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
          
        }

        public void HistogramGraphReNewal()
        {
            if (_histogramControl != null)
            {
                _histogramControl.ImageBit = Status.Instance().SelectedViewer.GetBit();
                _histogramControl.IsColor = Status.Instance().SelectedViewer.IsColor();
                _histogramControl.HistogramGraphReNewal(Status.Instance().SelectedViewer.ImageManager.GetSelectedViewerHistogramParam());
            }
        }

        public void MarkReset()
        {
            if (_histogramControl != null)
            {
                _histogramControl.MarkReset();
            }
        }

        private void HistgoramForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CloseEventDelegate != null)
                CloseEventDelegate();
        }

        public void HistogramPanelDataGridNewUpdate()
        {
            if(_histogramControl !=null)
            {
                _histogramControl.ImageBit = Status.Instance().SelectedViewer.GetBit();
                _histogramControl.IsColor = Status.Instance().SelectedViewer.IsColor();
                _histogramControl.DataGridNewUpdate();
            }
        }

        private void HistogramForm_Activated(object sender, EventArgs e)
        {
            if (_histogramControl != null)
            {
                if (_histogramControl.IsActive)
                    return;

                _histogramControl.IsActive = true;
            }
        }

        private void HistogramForm_Deactivate(object sender, EventArgs e)
        {
            if (_histogramControl != null)
                _histogramControl.IsActive = false;
        }
    }
}
