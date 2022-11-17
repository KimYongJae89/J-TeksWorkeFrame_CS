using ImageManipulator.Controls;
using ImageManipulator.Data;
using ImageManipulator.Util;
using KiyLib.DIP;
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

namespace ImageManipulator.Forms
{
    public partial class ProfileListForm : Form
    {
        MultiHistogramControl _histogramControl;
        public Action<int> DataGridViewClickDelegate;
        private int _selectIndex = 0;
        public Action CloseEventDelegate;
        public ProfileListForm()
        {
            InitializeComponent();
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {
            try
            {

                Status.Instance().LogManager.AddLogMessage("Profile Form Open", "");
                this.Text = LangResource.ProfileList;
                ControlAdd();
                ProfileDataGridClearReUpdate();
                SelectNumInitialize();

                cbxDerivativeType.Items.Clear();
                cbxDerivativeType.Items.Add("None");
                cbxDerivativeType.Items.Add("1st Derivative");
                cbxDerivativeType.Items.Add("2st Derivative");
                cbxDerivativeType.SelectedIndex = 0;
                this.MinimumSize = new Size(525, 386);

                foreach (DataGridViewColumn column in dataGridViewProfile.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        private void ControlAdd()
        {
            _histogramControl = new MultiHistogramControl();
            _histogramControl.ImageBit = Status.Instance().SelectedViewer.GetBit();
            _histogramControl.IsColor = Status.Instance().SelectedViewer.IsColor();
            _histogramControl.SetHistogramParam(Status.Instance().SelectedViewer.ImageManager.GetProfileHistogramParamList());
            _histogramControl.DrawType = HistogramPanelDrawType.Histogram;
            _histogramControl.MarkValueDelegate += MarkInfoUpdate;
            MainPanel.Controls.Add(this._histogramControl);
            this._histogramControl.Left = 0;
            this._histogramControl.Top = 0;
            _histogramControl.Dock = System.Windows.Forms.DockStyle.Fill;

            _histogramControl.TabStop = false;
        }

        private void MarkInfoUpdate(tMarkInfo MarkInfo)
        {
            try
            {
                ProfileFigure profile = Status.Instance().SelectedViewer.GetDrawBox().GetSelectedProfile();
                if (profile == null)
                    return;
                tProfileParams param = new tProfileParams();
                List<HistogramParams> histogramParams = _histogramControl.GetHistogramParams();
                param.ProfilePoints = histogramParams[0].Points;

                param.Mark1 = MarkInfo.Mark1Index;
                param.Mark2 = MarkInfo.Mark2Index;
                profile.SetParam(param);

                Status.Instance().SelectedViewer.GetDrawBox().ReUpdate();
            }
            catch (Exception err)
            {

                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
                ProfileFigure profile = Status.Instance().SelectedViewer.GetDrawBox().GetSelectedProfile();
                if (profile == null)
                {
                    return;
                }
                tProfileParams param = new tProfileParams();
                List<HistogramParams> histogramParams = _histogramControl.GetHistogramParams();
                param.ProfilePoints = histogramParams[0].Points;
                param.Mark1 = MarkInfo.Mark1Index;
                param.Mark2 = MarkInfo.Mark2Index;
                profile.SetParam(param);

                Status.Instance().SelectedViewer.GetDrawBox().ReUpdate();
            }
           
        }

        public delegate void ProfileDataGridUpdateDelegate();
        public void ProfileDataGridClearReUpdate()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    ProfileDataGridUpdateDelegate callback = ProfileDataGridClearReUpdate;
                    Invoke(callback);
                    return;
                }
                dataGridViewProfile.Rows.Clear();
                int index = 0;
                _selectIndex = -1;
                foreach (ProfileFigure profile in Status.Instance().SelectedViewer.GetDrawBox().GetProfileAllList())
                {
                    index++;
                    tProfileResult result = (tProfileResult)profile.GetResult();
                    PointF orgStartPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(result.StartPoint);
                    PointF orgEndPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(result.EndPoint);
                    string figureID = "P.F_" + profile.Id;


                    string strStartPoint = "(" + ((int)(orgStartPoint.X)).ToString() + "," + ((int)(orgStartPoint.Y)).ToString() + ")";
                    string strEndPoint = "(" + ((int)(orgEndPoint.X)).ToString() + "," + ((int)(orgEndPoint.Y)).ToString() + ")";
                    string distance = Status.Instance().SelectedViewer.ImageManager.DistancePointToPoint(orgStartPoint, orgEndPoint).ToString();
                    dataGridViewProfile.Rows.Add(figureID, strStartPoint, strEndPoint, distance);

                    if (profile.Selectable)
                    {
                        _selectIndex = index - 1;
                    }
                    dataGridViewProfile.Rows[index - 1].Selected = false;
                }
                if (_selectIndex > 0)
                    dataGridViewProfile.Rows[_selectIndex].Selected = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        
        }

        private void SelectNumInitialize()
        {
            int selectedCount = Status.Instance().SelectedViewer.GetDrawBox().SelectedFigureCount();
            if (selectedCount == 0)
            {
                _selectIndex = 0;
                dataGridViewProfile.Rows[0].Selected = true;
                if (DataGridViewClickDelegate != null)
                    DataGridViewClickDelegate(0);
            }
            else
            {
                ProfileFigure profile = Status.Instance().SelectedViewer.GetDrawBox().GetSelectedProfile();
                if (profile == null)
                    return;

                for (int i = 0; i < dataGridViewProfile.RowCount; i++)
                {
                    string gridviewText = dataGridViewProfile.Rows[i].Cells[0].Value.ToString();
                    string id = Regex.Replace(gridviewText, @"\D", "");
                    if (id == profile.Id)
                    {
                        dataGridViewProfile.Rows[i].Selected = true;
                        _selectIndex = i;
                    }
                }
            }
        }

        private void ProfileForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CloseEventDelegate != null)
                CloseEventDelegate();
            Status.Instance().ProfileWidth = 1;
            Status.Instance().DerivativeType = eDerivativeType.None;
        }

     
        public void ProfileSelect(int id)
        {
            string idName = "P.F_" + id.ToString();
            int selectedIndex = -1;
            for (int i = 0; i < dataGridViewProfile.Rows.Count; i++)
            {
                string idValue = dataGridViewProfile.Rows[i].Cells[0].Value.ToString();
                if (idName == idValue)
                {
                    dataGridViewProfile.Rows[i].Selected = true;
                    selectedIndex = i;
                }
            }

            if (selectedIndex < 0)
            {
                for (int i = 0; i < dataGridViewProfile.Rows.Count; i++)
                {
                    dataGridViewProfile.Rows[i].Selected = false;
                }
                _selectIndex = -1;
                HistogramGraphReNewal();
                //dataGridViewProfile.Rows[0].Selected = true;
                //_selectIndex = 0;
            }

            else if (selectedIndex >= 0)
            {
                _selectIndex = selectedIndex;
                HistogramGraphReNewal();
            }
        }

        private void EditDatagridView()
        {
            tProfileResult result = (tProfileResult)Status.Instance().SelectedViewer.GetDrawBox().GetSelectedProfile().GetResult();
            PointF orgStartPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(result.StartPoint);
            PointF orgEndPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(result.EndPoint);

            if (orgStartPoint.X < 0 || orgStartPoint.Y < 0
               || orgEndPoint.X < 0 || orgEndPoint.Y < 0
                || orgStartPoint.X > Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width
                || orgEndPoint.X > Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width
               || orgStartPoint.Y > Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height
               || orgEndPoint.Y > Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height)
                return;

            string strStartPoint = "(" + ((int)(orgStartPoint.X)).ToString() + "," + ((int)(orgStartPoint.Y)).ToString() + ")";
            string strEndPoint = "(" + ((int)(orgEndPoint.X)).ToString() + "," + ((int)(orgEndPoint.Y)).ToString() + ")";
            string distance = Status.Instance().SelectedViewer.ImageManager.DistancePointToPoint(orgStartPoint, orgEndPoint).ToString();
            dataGridViewProfile.Rows[_selectIndex].Cells[1].Value = strStartPoint;
            dataGridViewProfile.Rows[_selectIndex].Cells[2].Value = strEndPoint;
            dataGridViewProfile.Rows[_selectIndex].Cells[3].Value = distance;
        }

        private void EditDatagridView(PointF orgStartPoint, PointF orgEndPoint)
        {
            string strStartPoint = "(" + ((int)(orgStartPoint.X)).ToString() + "," + ((int)(orgStartPoint.Y)).ToString() + ")";
            string strEndPoint = "(" + ((int)(orgEndPoint.X)).ToString() + "," + ((int)(orgEndPoint.Y)).ToString() + ")";
            string distance = Status.Instance().SelectedViewer.ImageManager.DistancePointToPoint(orgStartPoint, orgEndPoint).ToString();
            dataGridViewProfile.Rows[_selectIndex].Cells[1].Value = strStartPoint;
            dataGridViewProfile.Rows[_selectIndex].Cells[2].Value = strEndPoint;
            dataGridViewProfile.Rows[_selectIndex].Cells[3].Value = distance;
        }


        public void HistogramPanelDataGridNewUpdate()
        {
            if (_histogramControl != null)
            {
                _histogramControl.ImageBit = Status.Instance().SelectedViewer.GetBit();
                _histogramControl.IsColor = Status.Instance().SelectedViewer.IsColor();
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

        public void HistogramGraphReNewal()
        {
            try
            {

                if (_histogramControl != null)
                {
                    _histogramControl.ImageBit = Status.Instance().SelectedViewer.GetBit();
                    _histogramControl.IsColor = Status.Instance().SelectedViewer.IsColor();
                    _histogramControl.HistogramGraphReNewal(Status.Instance().SelectedViewer.ImageManager.GetProfileHistogramParamList());
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        public void HistogramGraphReNewal(List<HistogramParams> histogramParams)
        {
            try
            {

                if (_histogramControl != null)
                {
                    _histogramControl.ImageBit = Status.Instance().SelectedViewer.GetBit();
                    _histogramControl.IsColor = Status.Instance().SelectedViewer.IsColor();
                    _histogramControl.HistogramGraphReNewal(histogramParams);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        public void MoveEventUpdate(List<HistogramParams> param, PointF orgStartPt, PointF orgEndPt)
        {
            //Thread th = new Thread(new ParameterizedThreadStart(MoveEventThread));
            Thread th = new Thread(() => MoveEventThread(param, orgStartPt, orgEndPt));
            Status.Instance().IsDrawingProfile = true;
            th.Start();
        }

        public void MoveEventThread(List<HistogramParams> histgramParam, PointF orgStartPt, PointF orgEndPt)
        {
            try
            {
                HistogramGraphReNewal(histgramParam);
                EditDatagridView(orgStartPt, orgEndPt);
                Status.Instance().IsDrawingProfile = false;
                //MarkReset();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private void dataGridViewProfile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Status.Instance().SelectedViewer.GetDrawBox().GetProfileCount() <= 0)
                return;

            int index = e.RowIndex;
            if (index < 0)
                return;
            _selectIndex = index;
            string roiNum = dataGridViewProfile.Rows[index].Cells[0].Value.ToString();
            string id = Regex.Replace(roiNum, @"\D", "");

            int value = Convert.ToInt32(id);
            if (DataGridViewClickDelegate != null)
                DataGridViewClickDelegate(value);

            HistogramGraphReNewal();
            EditDatagridView();
        }

        private void cbxDerivative_CheckedChanged(object sender, EventArgs e)
        {
            
            //if(cbxDerivative.Checked)
            //{
            //    Status.Instance().IsDerivative = true;
            //}
            //else
            //{
            //    Status.Instance().IsDerivative = false;
            //}
            //HistogramGraphReNewal();
        }

        private void nupdnWidth_ValueChanged(object sender, EventArgs e)
        {
            if (Status.Instance().SelectedViewer.GetDrawBox().GetProfileCount() <= 0)
                return;
            decimal value = nupdnWidth.Value;
            if (value <= 0)
                return;
            Status.Instance().ProfileWidth = Convert.ToInt32(value);
            HistogramGraphReNewal();
            Status.Instance().SelectedViewer.GetDrawBox().ReUpdate();
        }

        private void cbxDerivativeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Status.Instance().SelectedViewer.GetDrawBox().GetProfileCount() <= 0)
                return;

            int index = cbxDerivativeType.SelectedIndex;

            if (index < 0)
                return;

            if (index == 0)
                Status.Instance().DerivativeType = eDerivativeType.None;
            if(index == 1)
                Status.Instance().DerivativeType = eDerivativeType.OneDerivative;
            if (index == 2)
                Status.Instance().DerivativeType = eDerivativeType.TwoDerivative;

            HistogramGraphReNewal();
        }

        private void ProfileListForm_Activated(object sender, EventArgs e)
        {
            if (_histogramControl != null)
            {
                if (_histogramControl.IsActive)
                    return;

                _histogramControl.IsActive = true;
            }
        }

        private void ProfileListForm_Deactivate(object sender, EventArgs e)
        {
            if (_histogramControl != null)
                _histogramControl.IsActive = false;
        }
    }
}
