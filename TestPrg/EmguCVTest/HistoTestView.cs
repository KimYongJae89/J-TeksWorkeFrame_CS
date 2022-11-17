using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmguCVTest
{
    public partial class HistoTestView : Form
    {
        public HistoTestView()
        {
            InitializeComponent();
        }

        private void HistoTestView_Load(object sender, EventArgs e)
        {
            chart.Series[0].ChartType =
                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            chart.Series[0].Color = Color.Blue;

            chart.Series[1].ChartType =
               System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            chart.Series[1].Color = Color.Green;

            chart.Series[2].ChartType =
               System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            chart.Series[2].Color = Color.Red;
        }

        public void UpdateData(float[] data)
        {
            chart.ChartAreas[0].AxisX.IsMarginVisible = false;
            chart.ChartAreas[0].AxisY.IsMarginVisible = false;
            //chart.ChartAreas[0].AxisY.Minimum = 36400;

            chart.Series[0].Points.DataBindY(data);
            chart.Update();
        }

        public void UpdateData(float[] data0, float[] data1, float[] data2)
        {
            chart.Series[0].Points.DataBindY(data0);
            chart.Series[1].Points.DataBindY(data1);
            chart.Series[2].Points.DataBindY(data2);
            chart.Update();
        }
    }
}
