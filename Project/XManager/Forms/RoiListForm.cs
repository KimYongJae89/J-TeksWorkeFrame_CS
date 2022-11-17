using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XManager.Controls;
using XManager.FigureData;

namespace XManager.Forms
{
    public partial class RoiListForm : Form
    {
        MultiHistogramControl _histogramControl;
        //private int _selectIndex = 0;

        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; }
        }

        public Action<int> DataGridViewClickDelegate;
        public Action CloseEventDelegate; // 폼 종료 시 low,high 설정 값
        public RoiListForm()
        {
            InitializeComponent();
        }

        private void RoiList_Load(object sender, EventArgs e)
        {
            this.Text = LangResource.RoiList;
            ControlAdd();
            RoiDatagridUpdate();
            SelectNumInitialize();
            this.MinimumSize = new Size(525, 386);

            foreach (DataGridViewColumn column in dataGridViewRoi.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void ControlAdd()
        {
            _histogramControl = new MultiHistogramControl();
            _histogramControl.ImageBit = CStatus.Instance().GetDrawBox().ImageManager.GetBit();
            _histogramControl.IsColor = CStatus.Instance().GetDrawBox().ImageManager.IsColor();
            _histogramControl.SetHistogramParam(CStatus.Instance().GetDrawBox().ImageManager.GetRoiHistogramParamList());

            MainPanel.Controls.Add(this._histogramControl);
            this._histogramControl.Left = 0;
            this._histogramControl.Top = 0;
            _histogramControl.Dock = System.Windows.Forms.DockStyle.Fill;

            _histogramControl.TabStop = false;
        }

        private void SelectNumInitialize()
        {
            int selectedCount = CStatus.Instance().GetDrawBox().TrackerManager.SelectedFigureCount();
            if (selectedCount == 0)
            {
                SelectedIndex = 0;
                dataGridViewRoi.Rows[0].Selected = true;
                if (DataGridViewClickDelegate != null)
                    DataGridViewClickDelegate(0);
            }
            else
            {
                RectangleFigure rect = CStatus.Instance().GetDrawBox().TrackerManager.GetSelectedRoi();
                if (rect == null)
                    return;
                for (int i = 0; i < dataGridViewRoi.RowCount; i++)
                {
                    string gridviewText = dataGridViewRoi.Rows[i].Cells[0].Value.ToString();
                    string id = Regex.Replace(gridviewText, @"\D", "");
                    if (id == rect.Id)
                    {
                        dataGridViewRoi.Rows[i].Selected = true;
                        SelectedIndex = i;
                    }
                }
            }
        }

        public delegate void ColorRoiClearToUpdateDelegate();
        public void RoiDatagridUpdate()
        {
            if (this.InvokeRequired)
            {
                ColorRoiClearToUpdateDelegate callback = RoiDatagridUpdate;
                Invoke(callback);
                return;
            }

            dataGridViewRoi.Rows.Clear();
            int index = 0;
            SelectedIndex = -1;


            foreach (RectangleFigure roi in CStatus.Instance().GetDrawBox().TrackerManager.GetRoiAllList())
            {
                index++;
                RectangleF orgRect = ((tRectangleResult)roi.GetResult()).resultRectangle;
               // RectangleF orgRect = Status.Instance().SelectedViewer.ImageManager.GetOrgRectangle(rect);
                string figureID = "Roi_" + roi.Id;
                string width = ((int)orgRect.Width).ToString();
                string height = ((int)orgRect.Height).ToString();

                if (!CStatus.Instance().GetDrawBox().ImageManager.IsColor())
                {
                    GreyHistogramParams roiInfo = CStatus.Instance().GetDrawBox().ImageManager.GetGreyRoiHistogramParam(orgRect);
                    string min = roiInfo.Min.ToString();
                    string max = roiInfo.Max.ToString();

                    dataGridViewRoi.Rows.Add(figureID, width, height, min.ToString(), max.ToString());
                }
                else
                {
                    ColorHistogramParams roiInfo = CStatus.Instance().GetDrawBox().ImageManager.GetColorRoiHistogramParam(orgRect);
                    string rMin = roiInfo.R_Min.ToString();
                    string rMax = roiInfo.R_Max.ToString();
                    string rAvg = roiInfo.R_Avg.ToString();
                    string gMin = roiInfo.G_Min.ToString();
                    string gMax = roiInfo.G_Max.ToString();
                    string gAvg = roiInfo.G_Avg.ToString();
                    string bMin = roiInfo.B_Min.ToString();
                    string bMax = roiInfo.B_Max.ToString();
                    string bAvg = roiInfo.B_Avg.ToString();

                    int max = -1 * Int32.MaxValue;
                    int min = Int32.MaxValue;

                    if (Convert.ToInt32(rMax) > max) max = Convert.ToInt32(rMax);
                    if (Convert.ToInt32(rMin) < min) min = Convert.ToInt32(rMin);
                    if (Convert.ToInt32(gMax) > max) max = Convert.ToInt32(gMax);
                    if (Convert.ToInt32(gMin) < min) min = Convert.ToInt32(gMin);
                    if (Convert.ToInt32(bMax) > max) max = Convert.ToInt32(bMax);
                    if (Convert.ToInt32(bMin) < min) min = Convert.ToInt32(bMin);

                    dataGridViewRoi.Rows.Add(figureID, width, height, min.ToString(), max.ToString());

                }

                if (roi.Selectable)
                {
                    SelectedIndex = index - 1;
                }
                dataGridViewRoi.Rows[index - 1].Selected = false;

            }

            if (SelectedIndex > 0)
                dataGridViewRoi.Rows[SelectedIndex].Selected = true;

        }

        public void HistogramGraphReNewal()
        {
            if (_histogramControl != null)
            {
                _histogramControl.ImageBit = CStatus.Instance().GetDrawBox().ImageManager.GetBit();
                _histogramControl.IsColor = CStatus.Instance().GetDrawBox().ImageManager.IsColor();
                _histogramControl.HistogramGraphReNewal(CStatus.Instance().GetDrawBox().ImageManager.GetRoiHistogramParamList());
            }
        }

        public void HistogramPanelDataGridNewUpdate()
        {
            if (_histogramControl != null)
            {
                _histogramControl.ImageBit = CStatus.Instance().GetDrawBox().ImageManager.GetBit();
                _histogramControl.IsColor = CStatus.Instance().GetDrawBox().ImageManager.IsColor();
                _histogramControl.DataGridNewUpdate();
            }
        }

        public void MarkReset()
        {
            if (_histogramControl != null)
            {
                _histogramControl.MarkReset();
            }
        }

        public void SelectedHistogramUpdate(RectangleF orgRoi)
        {
            if (CStatus.Instance().GetDrawBox() == null)
                return;
            if (!CStatus.Instance().GetDrawBox().ImageManager.IsColor())
                GreyHistogramUpdate(orgRoi);
            else
                ColorHistgoramUpdate(orgRoi);
        }

        public void SelectedHistogramUpdate()
        {
            if (CStatus.Instance().GetDrawBox() == null)
                return;
            if (!CStatus.Instance().GetDrawBox().ImageManager.IsColor())
                GreyHistogramUpdate();
            else
                ColorHistgoramUpdate();
        }
        public void DatagridViewClear()
        {
            dataGridViewRoi.Rows.Clear();
        }
        private void RoiListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CloseEventDelegate != null)
                CloseEventDelegate();
        }

        private void dataGridViewRoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CStatus.Instance().GetDrawBox().TrackerManager.GetRoiCount() <= 0)
                return;
            int index = e.RowIndex;
            if (index < 0)
                return;
            SelectedIndex = index;
            string roiNum = dataGridViewRoi.Rows[index].Cells[0].Value.ToString();
            string id = Regex.Replace(roiNum, @"\D", "");

            int value = Convert.ToInt32(id);
            if (DataGridViewClickDelegate != null)
                DataGridViewClickDelegate(value);

            SelectedHistogramUpdate();
        }

        public void RoiSelect(int id)
        {
            if (dataGridViewRoi.Rows.Count <= 0)
                return;
            string idName = "Roi_" + id.ToString();
            int selectedIndex = -1;
            for (int i = 0; i < dataGridViewRoi.Rows.Count; i++)
            {
                string idValue = dataGridViewRoi.Rows[i].Cells[0].Value.ToString();
                if (idName == idValue)
                {
                    dataGridViewRoi.Rows[i].Selected = true;
                    selectedIndex = i;
                }
            }

            if (selectedIndex < 0)
            {
                for (int i = 0; i < dataGridViewRoi.Rows.Count; i++)
                {
                    dataGridViewRoi.Rows[i].Selected = false;
                }
                SelectedIndex = -1;
                SelectedHistogramUpdate();

                //dataGridViewRoi.Rows[0].Selected = true;
                //SelectedIndex = 0;
            }
                
            else if (selectedIndex >= 0)
            {
                SelectedIndex = selectedIndex;
                
                SelectedHistogramUpdate();
            }
            
        }
        public void MoveEventUpdate(RectangleF orgRoi)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            MoveEventThread(orgRoi);

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds.ToString());
        }

        public void MoveEventThread(object param)
        {
            try
            {
                RectangleF orgRoi = (RectangleF)param;

                if (CStatus.Instance().GetDrawBox() == null)
                    return;
                if (!CStatus.Instance().GetDrawBox().ImageManager.IsColor())
                    GreyHistogramUpdate(orgRoi);
                else
                    ColorHistgoramUpdate(orgRoi);

                MarkReset();
                //Status.Instance().IsDrawingRoi = false;
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        private void GreyHistogramUpdate()
        {
            if (CStatus.Instance().GetDrawBox().TrackerManager.GetRoiCount() <= 0)
                return;
            RectangleFigure roi = CStatus.Instance().GetDrawBox().TrackerManager.GetSelectedRoi();
            if (roi == null)
            {
                _histogramControl.HistogramGraphReNewal(null);
                return;
            }
            RectangleF orgRoi = ((tRectangleResult)roi.GetResult()).resultRectangle;

            if (orgRoi.X < 0 || orgRoi.Y < 0 || orgRoi.X + orgRoi.Width > CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width
                || orgRoi.Y + orgRoi.Height > CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height)
                return;
            
            List<HistogramParams> paramList = new List<HistogramParams>();

            HistogramParams param = new HistogramParams();

            GreyHistogramParams roiInfo = CStatus.Instance().GetDrawBox().ImageManager.GetGreyRoiHistogramParam(orgRoi);
                 
            param.HistogramValue = roiInfo.HistogramValue;
            param.GraphColor = Color.Gray;
            param.Mark1Value = 0;
            param.Mark2Value = param.HistogramValue.Count();
            param.Width = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width;
            param.Height = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height;
            paramList.Add(param);
            _histogramControl.ImageBit = CStatus.Instance().GetDrawBox().ImageManager.GetBit();
            _histogramControl.IsColor = CStatus.Instance().GetDrawBox().ImageManager.IsColor();
            _histogramControl.HistogramGraphReNewal(paramList);


            //임시
            string width = orgRoi.Width.ToString();
            string height = orgRoi.Height.ToString();

            string min = roiInfo.Min.ToString();
            string max = roiInfo.Max.ToString();
            string avg = roiInfo.Avg.ToString();

            if (SelectedIndex < 0)
                return;

            dataGridViewRoi.Rows[SelectedIndex].Cells[1].Value = width;
            dataGridViewRoi.Rows[SelectedIndex].Cells[2].Value = height;
            dataGridViewRoi.Rows[SelectedIndex].Cells[3].Value = min;
            dataGridViewRoi.Rows[SelectedIndex].Cells[4].Value = max;
        }

        private void GreyHistogramUpdate(RectangleF orgRoi)
        {
            List<HistogramParams> paramList = new List<HistogramParams>();

            HistogramParams param = new HistogramParams();
            GreyHistogramParams roiInfo = CStatus.Instance().GetDrawBox().ImageManager.GetGreyRoiHistogramParam(orgRoi);

            param.HistogramValue = roiInfo.HistogramValue;
            param.GraphColor = Color.Gray;
            param.Mark1Value = 0;
            param.Mark2Value = param.HistogramValue.Count();
            param.Width = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width;
            param.Height = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height;

            paramList.Add(param);
            _histogramControl.ImageBit = CStatus.Instance().GetDrawBox().ImageManager.GetBit();
            _histogramControl.IsColor = CStatus.Instance().GetDrawBox().ImageManager.IsColor();
            _histogramControl.HistogramGraphReNewal(paramList);

            //임시
            string width = orgRoi.Width.ToString();
            string height = orgRoi.Height.ToString();

            string min = roiInfo.Min.ToString();
            string max = roiInfo.Max.ToString();
            string avg = roiInfo.Avg.ToString();

            if (SelectedIndex < 0)
                return;

            dataGridViewRoi.Rows[SelectedIndex].Cells[1].Value = width;
            dataGridViewRoi.Rows[SelectedIndex].Cells[2].Value = height;
            dataGridViewRoi.Rows[SelectedIndex].Cells[3].Value = min;
            dataGridViewRoi.Rows[SelectedIndex].Cells[4].Value = max;
        }

        private void ColorHistgoramUpdate()
        {
            if (CStatus.Instance().GetDrawBox().TrackerManager.GetRoiCount() <= 0)
                return;
            RectangleFigure roi = CStatus.Instance().GetDrawBox().TrackerManager.GetSelectedRoi();
            if (roi == null)
            {
                _histogramControl.HistogramGraphReNewal(null);
                return;
            }

            RectangleF orgRoi = ((tRectangleResult)roi.GetResult()).resultRectangle;
            //RectangleF orgRoi = Status.Instance().SelectedViewer.ImageManager.GetOrgRectangle(roi);

            if (orgRoi.X < 0 || orgRoi.Y < 0 || orgRoi.X + orgRoi.Width > CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width
                || orgRoi.Y + orgRoi.Height > CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height)
                return;

            List<HistogramParams> paramList = new List<HistogramParams>();

            ColorHistogramParams roiInfo = CStatus.Instance().GetDrawBox().ImageManager.GetColorRoiHistogramParam(orgRoi);

            HistogramParams rParam = new HistogramParams();
            rParam.HistogramValue = roiInfo.R_HistogramValue;
            rParam.GraphColor = Color.Red;
            rParam.Mark1Value = 0;
            rParam.Mark2Value = rParam.HistogramValue.Count();
            rParam.Width = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width;
            rParam.Height = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height;

            HistogramParams gParam = new HistogramParams();
            gParam.HistogramValue = roiInfo.G_HistogramValue;
            gParam.GraphColor = Color.Green;
            gParam.Mark1Value = 0;
            gParam.Mark2Value = rParam.HistogramValue.Count();
            gParam.Width = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width;
            gParam.Height = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height;

            HistogramParams bParam = new HistogramParams();
            bParam.HistogramValue = roiInfo.B_HistogramValue;
            bParam.GraphColor = Color.Blue;
            bParam.Mark1Value = 0;
            bParam.Mark2Value = rParam.HistogramValue.Count();
            bParam.Width = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width;
            bParam.Height = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height;

            string width = orgRoi.Width.ToString();
            string height = orgRoi.Height.ToString();

            string rMin = roiInfo.R_Min.ToString();
            string rMax = roiInfo.R_Max.ToString();
            string rAvg = roiInfo.R_Avg.ToString();
            string gMin = roiInfo.G_Min.ToString();
            string gMax = roiInfo.G_Max.ToString();
            string gAvg = roiInfo.G_Avg.ToString();
            string bMin = roiInfo.B_Min.ToString();
            string bMax = roiInfo.B_Max.ToString();
            string bAvg = roiInfo.B_Avg.ToString();

            int max = -1 * Int32.MaxValue;
            int min = Int32.MaxValue;

            if (Convert.ToInt32(rMax) > max) max = Convert.ToInt32(rMax);
            if (Convert.ToInt32(rMin) < min) min = Convert.ToInt32(rMin);
            if (Convert.ToInt32(gMax) > max) max = Convert.ToInt32(gMax);
            if (Convert.ToInt32(gMin) < min) min = Convert.ToInt32(gMin);
            if (Convert.ToInt32(bMax) > max) max = Convert.ToInt32(bMax);
            if (Convert.ToInt32(bMin) < min) min = Convert.ToInt32(bMin);

            if (SelectedIndex < 0)
                return;

            dataGridViewRoi.Rows[SelectedIndex].Cells[1].Value = width;
            dataGridViewRoi.Rows[SelectedIndex].Cells[2].Value = height;
            dataGridViewRoi.Rows[SelectedIndex].Cells[3].Value = min;
            dataGridViewRoi.Rows[SelectedIndex].Cells[4].Value = max;

            paramList.Add(rParam);
            paramList.Add(gParam);
            paramList.Add(bParam);

            _histogramControl.ImageBit = CStatus.Instance().GetDrawBox().ImageManager.GetBit();
            _histogramControl.IsColor = CStatus.Instance().GetDrawBox().ImageManager.IsColor();
            _histogramControl.HistogramGraphReNewal(paramList);
        }

        private void ColorHistgoramUpdate(RectangleF orgRoi)
        {
            List<HistogramParams> paramList = new List<HistogramParams>();

            ColorHistogramParams roiInfo = CStatus.Instance().GetDrawBox().ImageManager.GetColorRoiHistogramParam(orgRoi);

            HistogramParams rParam = new HistogramParams();
            rParam.HistogramValue = roiInfo.R_HistogramValue;
            rParam.GraphColor = Color.Red;
            rParam.Mark1Value = 0;
            rParam.Mark2Value = rParam.HistogramValue.Count();

            HistogramParams gParam = new HistogramParams();
            gParam.HistogramValue = roiInfo.G_HistogramValue;
            gParam.GraphColor = Color.Green;
            gParam.Mark1Value = 0;
            gParam.Mark2Value = rParam.HistogramValue.Count();

            HistogramParams bParam = new HistogramParams();
            bParam.HistogramValue = roiInfo.B_HistogramValue;
            bParam.GraphColor = Color.Blue;
            bParam.Mark1Value = 0;
            bParam.Mark2Value = rParam.HistogramValue.Count();

            string width = orgRoi.Width.ToString();
            string height = orgRoi.Height.ToString();

            string rMin = roiInfo.R_Min.ToString();
            string rMax = roiInfo.R_Max.ToString();
            string rAvg = roiInfo.R_Avg.ToString();
            string gMin = roiInfo.G_Min.ToString();
            string gMax = roiInfo.G_Max.ToString();
            string gAvg = roiInfo.G_Avg.ToString();
            string bMin = roiInfo.B_Min.ToString();
            string bMax = roiInfo.B_Max.ToString();
            string bAvg = roiInfo.B_Avg.ToString();

            int max = -1 * Int32.MaxValue;
            int min = Int32.MaxValue;

            if (Convert.ToInt32(rMax) > max) max = Convert.ToInt32(rMax);
            if (Convert.ToInt32(rMin) < min) min = Convert.ToInt32(rMin);
            if (Convert.ToInt32(gMax) > max) max = Convert.ToInt32(gMax);
            if (Convert.ToInt32(gMin) < min) min = Convert.ToInt32(gMin);
            if (Convert.ToInt32(bMax) > max) max = Convert.ToInt32(bMax);
            if (Convert.ToInt32(bMin) < min) min = Convert.ToInt32(bMin);

            if (SelectedIndex < 0)
                return;

            dataGridViewRoi.Rows[SelectedIndex].Cells[1].Value = width;
            dataGridViewRoi.Rows[SelectedIndex].Cells[2].Value = height;
            dataGridViewRoi.Rows[SelectedIndex].Cells[3].Value = min;
            dataGridViewRoi.Rows[SelectedIndex].Cells[4].Value = max;

            paramList.Add(rParam);
            paramList.Add(gParam);
            paramList.Add(bParam);

            _histogramControl.ImageBit = CStatus.Instance().GetDrawBox().ImageManager.GetBit();
            _histogramControl.IsColor = CStatus.Instance().GetDrawBox().ImageManager.IsColor();
            _histogramControl.HistogramGraphReNewal(paramList);
        }

        private void RoiListForm_Activated(object sender, EventArgs e)
        {
            if(_histogramControl != null)
            {
                if (_histogramControl.IsActive)
                    return;
                _histogramControl.IsActive = true;
            }
        }

        private void RoiListForm_Deactivate(object sender, EventArgs e)
        {
            if (_histogramControl != null)
                _histogramControl.IsActive = false;
        }
    }
}
