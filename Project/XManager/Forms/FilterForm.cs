using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using KiyLib.DIP;
using System.Threading;
using System.Diagnostics;
using LibraryGlobalization.Properties;
using System.Reflection;
using XManager.Controls;
using XManager.ImageProcessingData;
using XManager.ImageProcessingControl;
using JTeksSplineGraph.Controls;

namespace XManager.Forms
{
    public partial class FilterForm : Form
    {
        private int _selectIndexUseFilter = -1;
        private BlurControl _blurControl = null;
        private CannyControl _cannyControl = null;
        private DilateControl _dilateControl = null;
        private ErodeControl _erodeControl = null;
        private LaplacianControl _laplacianControl = null;
        private MedianControl _medianControl = null;
        private NoneControl _noneControl = null;
        private SobelControl _sobelControl = null;
        private LevelingControl _greyLevelControl = null;
        private LutControl _lutControl = null;
        private LevelingControl _levelControl = null;

        private List<IPBase> _filterList = new List<IPBase>();
        public Action CloseEventDelegate;
        DoubleBufferPanel _filterOptionControl;

        public FilterForm()
        {
            InitializeComponent();
        }

        private void FilterDialog_Load(object sender, EventArgs e)
        {
            try
            {
                this.MinimumSize = new Size(538, 452);
                MultiLanguage();

                for (int i = 0; i < Enum.GetNames(typeof(eImageProcessType)).Length; i++)
                {
                    eImageProcessType filtertype = (eImageProcessType)i;
                    if (filtertype == eImageProcessType.Resize || filtertype == eImageProcessType.Crop 
                        || filtertype == eImageProcessType.UserFilter || filtertype == eImageProcessType.Basic)
                        continue;
                    lbxFilterList.Items.Add(filtertype.ToString());
                }
                AddControl();

                _filterList = CStatus.Instance().GetDrawBox().ImageManager.ImageProcessingList.Select(item => item.Clone() as IPBase).ToList();
                //Leveling 자동 추가
                if (_filterList.Count == 0)
                {
                    lbxFilterList.SelectedIndex = (int)eImageProcessType.GreyLeveling;
                    UseFilter();
                    FIlterThumbnailDisplay();
                }
                else
                {
                    foreach (IPBase processing in _filterList)
                    {
                        lbxUseFilterList.Items.Add(processing.ProcessType);
                        _selectIndexUseFilter++;

                    }
                    lbxUseFilterList.SelectedIndex = _selectIndexUseFilter;

                    UseListBoxSelect();
                }
              
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }
        private void AddControl()
        {
            _blurControl = new BlurControl();
            _cannyControl = new CannyControl();
            _dilateControl = new DilateControl();
            _erodeControl = new ErodeControl();
            _laplacianControl = new LaplacianControl();
            _medianControl = new MedianControl();
            _sobelControl = new SobelControl();

            _noneControl = new NoneControl();
            _noneControl.Dock = DockStyle.Fill;
            _noneControl.Visible = true;

            _greyLevelControl = new LevelingControl();
            _greyLevelControl.SliderUpdate += new Action<int, int>(SliderUpdate);

            _lutControl = new LutControl();
            _lutControl.SendLutInfo = SendLutInfo;
            _lutControl.Bit = CStatus.Instance().GetDrawBox().ImageManager.GetBit();

            _filterOptionControl = new DoubleBufferPanel();
            FilterOptionPanel.Controls.Add(_filterOptionControl);
            _filterOptionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            _filterOptionControl.Controls.Add(_noneControl);

            
            _lutControl.Dock = System.Windows.Forms.DockStyle.Fill;
        }
        private void MultiLanguage()
        {
            this.Text = LangResource.Filters;
            btnFilterApply.Text = LangResource.Apply;
            btnFilterSave.Text = LangResource.Save;
        }

        private void SendLutInfo(tCurveDataInfo info)
        {
            IpLutParams param = (IpLutParams)_filterList[_selectIndexUseFilter].GetParam();

            param.keyPt = info.keyPt;
            param.Width = info.Width;
            param.Height = info.Height;
            param.LUT = info.LUT;

            //Status.Instance().RoiListFormUpdate();
            //Status.Instance().HistogramFormUpdate();
        }

        private void SliderUpdate(int low, int high)
        {
            FIlterThumbnailDisplay();
            
            ////Status.Instance().RoiListFormUpdate();
            ////Status.Instance().HistogramFormUpdate();
        }

        private void btnUseFilter_Click(object sender, EventArgs e)
        {
            UseFilter();
            FIlterThumbnailDisplay();
        }

        private void btnDeleteFilter_Click(object sender, EventArgs e)
        {
            DeleteFilter();
            FIlterThumbnailDisplay();
        }

        private void UseFilter()
        {
            int index = lbxFilterList.SelectedIndex;
            if (index < 0)
                return;

            lbxUseFilterList.Items.Add((eImageProcessType)index);
            lbxUseFilterList.SelectedIndex = lbxUseFilterList.Items.Count - 1;

            FilterListAdd((eImageProcessType)index);

            _selectIndexUseFilter = lbxUseFilterList.SelectedIndex;
            ViewFilterControl((eImageProcessType)index);
        }

        private void DeleteFilter()
        {
            int index = lbxUseFilterList.SelectedIndex;
            if (index < 0)
                return;

            _selectIndexUseFilter--;

            lbxUseFilterList.Items.RemoveAt(index);
            lbxUseFilterList.SelectedIndex = _selectIndexUseFilter;
            _filterList.RemoveAt(index);

            if (_selectIndexUseFilter < 0)
            {
                AllFilterControlFalseVisible();
                None();
                lblFilterDescription.Text = "";
                return;
            }
            EditParam(_selectIndexUseFilter);
            _selectIndexUseFilter = lbxUseFilterList.SelectedIndex;

            eImageProcessType type = (eImageProcessType)lbxUseFilterList.Items[_selectIndexUseFilter];
            ViewFilterControl(type);

        }

        private void btnUserFilterListUp_Click(object sender, EventArgs e)
        {
            if (lbxUseFilterList.SelectedIndex <= 0)
                return;
            int up_item_num = lbxUseFilterList.SelectedIndex;
            int down_item_num = lbxUseFilterList.SelectedIndex - 1;
            if (lbxUseFilterList.SelectedIndex < 0 || up_item_num < 0 || down_item_num < 0)
                return;

            //_filterList 순서 이동
            _filterList.Reverse(_selectIndexUseFilter - 1, 2);

            eImageProcessType upMessage = (eImageProcessType)lbxUseFilterList.Items[up_item_num];
            eImageProcessType downMessage = (eImageProcessType)lbxUseFilterList.Items[down_item_num];

            lbxUseFilterList.Items[down_item_num] = upMessage;
            lbxUseFilterList.Items[up_item_num] = downMessage;
            lbxUseFilterList.SelectedIndex = down_item_num;
            _selectIndexUseFilter = lbxUseFilterList.SelectedIndex;
            FIlterThumbnailDisplay();
        }

        private void btnUserFilterListDown_Click(object sender, EventArgs e)
        {
            if (lbxUseFilterList.SelectedIndex < 0)
                return;
            int down_item_num = lbxUseFilterList.SelectedIndex;
            int up_item_num = lbxUseFilterList.SelectedIndex + 1;
            if (lbxUseFilterList.SelectedIndex < 0 || up_item_num < 0 || down_item_num < 0 || up_item_num >= lbxUseFilterList.Items.Count)
                return;

            //_filterList 순서 이동
            _filterList.Reverse(_selectIndexUseFilter, 2);

            eImageProcessType downMessage = (eImageProcessType)lbxUseFilterList.Items[down_item_num];
            eImageProcessType upMessage = (eImageProcessType)lbxUseFilterList.Items[up_item_num];

            lbxUseFilterList.Items[down_item_num] = upMessage;
            lbxUseFilterList.Items[up_item_num] = downMessage;

            lbxUseFilterList.SelectedIndex = up_item_num;
            _selectIndexUseFilter = lbxUseFilterList.SelectedIndex;
            FIlterThumbnailDisplay();
        }

        private void ViewFilterControl(eImageProcessType type)
        {
            AllFilterControlFalseVisible();
            DescriptionLabelUpdate(type);
            switch (type)
            {
                case eImageProcessType.Blur:
                    Blur();
                    break;
                case eImageProcessType.Rotate_CW:
                case eImageProcessType.Rotate_CCW:
                case eImageProcessType.Flip_Horizontal:
                case eImageProcessType.Flip_Vertical:
                case eImageProcessType.Sharp1:
                case eImageProcessType.Sharp2:
                case eImageProcessType.HorizonEdge:
                case eImageProcessType.VerticalEdge:
                case eImageProcessType.Average:
                    //case eImageFilterType.Basic:
                    None();
                    break;
                case eImageProcessType.Sobel:
                    Sobel();
                    break;
                case eImageProcessType.Laplacian:
                    Laplacian();
                    break;
                case eImageProcessType.Canny:
                    Canny();
                    break;
                case eImageProcessType.Median:
                    Median();
                    break;
                case eImageProcessType.Dilate:
                    Dilate();
                    break;
                case eImageProcessType.Erode:
                    Erode();
                    break;
                case eImageProcessType.GreyLeveling:
                    GreyLeveling();
                    break;
                case eImageProcessType.LUT:
                    Lut();
                    break;
                default:
                    break;
            }
        }

        private void DescriptionLabelUpdate(eImageProcessType type)
        {
            switch (type)
            {
                case eImageProcessType.Blur:
                    lblFilterDescription.Text = LangResource.FT_Blur;
                    break;
                case eImageProcessType.Sharp1:
                    lblFilterDescription.Text = LangResource.FT_Sharp1;
                    break;
                case eImageProcessType.Sharp2:
                    lblFilterDescription.Text = LangResource.FT_Sharp2;
                    break;
                case eImageProcessType.HorizonEdge:
                    lblFilterDescription.Text = LangResource.FT_HorizonEdge;
                    break;
                case eImageProcessType.VerticalEdge:
                    lblFilterDescription.Text = LangResource.FT_VerticalEdge;
                    break;
                case eImageProcessType.Average:
                    lblFilterDescription.Text = LangResource.FT_Average;
                    break;
                case eImageProcessType.Sobel:
                    lblFilterDescription.Text = LangResource.FT_Sobel;
                    break;
                case eImageProcessType.Laplacian:
                    lblFilterDescription.Text = LangResource.FT_Laplacian;
                    break;
                case eImageProcessType.Canny:
                    lblFilterDescription.Text = LangResource.FT_Canny;
                    break;
                case eImageProcessType.Median:
                    lblFilterDescription.Text = LangResource.FT_Median;
                    break;
                case eImageProcessType.Dilate:
                    lblFilterDescription.Text = LangResource.FT_Dilate;
                    break;
                case eImageProcessType.Erode:
                    lblFilterDescription.Text = LangResource.FT_Erode;
                    break;
                case eImageProcessType.Rotate_CW:
                    lblFilterDescription.Text = LangResource.FT_RotateCW;
                    break;
                case eImageProcessType.Rotate_CCW:
                    lblFilterDescription.Text = LangResource.FT_RotateCCW;
                    break;
                case eImageProcessType.Flip_Horizontal:
                    lblFilterDescription.Text = LangResource.FT_FlipHorizontal;
                    break;
                case eImageProcessType.Flip_Vertical:
                    lblFilterDescription.Text = LangResource.FT_FlipVertical;
                    break;
                case eImageProcessType.GreyLeveling:
                    lblFilterDescription.Text = "";
                    break;
                case eImageProcessType.LUT:
                    lblFilterDescription.Text = "";
                    break;
                default:
                    break;
            }
        }

        private void AllFilterControlFalseVisible()
        {
            _blurControl.Visible = false;
            _cannyControl.Visible = false;
            _dilateControl.Visible = false;
            _erodeControl.Visible = false;
            _laplacianControl.Visible = false;
            _medianControl.Visible = false;
            _noneControl.Visible = false;
            _sobelControl.Visible = false;
            _greyLevelControl.Visible = false;
            _lutControl.Visible = false;
        }

        private void FilterListAdd(eImageProcessType type)
        {
            IPBase filter = null;
            switch (type)
            {
                case eImageProcessType.Rotate_CW:
                    filter = new IP_RotationCW();
                    break;
                case eImageProcessType.Rotate_CCW:
                    filter = new IP_RotationCCW();
                    break;
                case eImageProcessType.Flip_Horizontal:
                    filter = new IP_FlipHorizontal();
                    break;
                case eImageProcessType.Flip_Vertical:
                    filter = new IP_FlipVetical();
                    break;

                case eImageProcessType.Blur:
                    filter = new IP_Blur();
                    break;
                case eImageProcessType.Sharp1:
                    filter = new IP_Sharp1();
                    break;
                case eImageProcessType.Sharp2:
                    filter = new IP_Sharp2();
                    break;
                case eImageProcessType.Sobel:
                    filter = new IP_Sobel();
                    break;
                case eImageProcessType.Laplacian:
                    filter = new IP_Laplacian();
                    break;
                case eImageProcessType.Canny:
                    filter = new IP_Canny();
                    break;
                case eImageProcessType.HorizonEdge:
                    filter = new IP_HorizonEdge();
                    break;
                case eImageProcessType.VerticalEdge:
                    filter = new IP_VerticalEdge();
                    break;
                case eImageProcessType.Median:
                    filter = new IP_Median();
                    break;
                case eImageProcessType.Dilate:
                    filter = new IP_Dilate();
                    break;
                case eImageProcessType.Erode:
                    filter = new IP_Erode();
                    break;
                case eImageProcessType.Average:
                    filter = new IP_Average();
                    break;
                case eImageProcessType.GreyLeveling:
                    filter = new IP_GreyLeveling();
                    break;
                case eImageProcessType.LUT:
                    filter = new IP_LUT();
                    break;

                //case eImageFilterType.Basic:
                //    filter = new IP_Basic();
                //    break;
                //case eImageFilterType.UserFilter:
                //    filter = new IP_UserFilter();
                //    break;
                default:
                    break;
            }
            if (filter != null)
                _filterList.Add(filter);
        }

        private void lbxUseFilterList_Click(object sender, EventArgs e)
        {
            UseListBoxSelect();
        }

        private void UseListBoxSelect()
        {
            EditParam(_selectIndexUseFilter);
            _selectIndexUseFilter = lbxUseFilterList.SelectedIndex;

            eImageProcessType type = (eImageProcessType)lbxUseFilterList.Items[_selectIndexUseFilter];
            ViewFilterControl(type);
        }
        private void EditParam(int selectIndex)
        {
            if (selectIndex < 0)
                return;
            eImageProcessType type = (eImageProcessType)lbxUseFilterList.Items[selectIndex];
            object paramObj = _filterList[selectIndex].GetParam();
            switch (type)
            {
                case eImageProcessType.Blur:
                    IpBlurParams blur = (IpBlurParams)paramObj;
                    _blurControl.EditParam(ref blur);
                    break;
                case eImageProcessType.Rotate_CW:
                case eImageProcessType.Rotate_CCW:
                case eImageProcessType.Flip_Horizontal:
                case eImageProcessType.Flip_Vertical:
                case eImageProcessType.Sharp1:
                case eImageProcessType.Sharp2:
                case eImageProcessType.HorizonEdge:
                case eImageProcessType.VerticalEdge:
                case eImageProcessType.Average:
                    //case eImageFilterType.Basic:
                    break;
                case eImageProcessType.Sobel:
                    IpSobelParams sobel = (IpSobelParams)paramObj;
                    _sobelControl.EditParam(ref sobel);
                    break;
                case eImageProcessType.Laplacian:
                    IpLaplacianParams lapacian = (IpLaplacianParams)paramObj;
                    _laplacianControl.EditParam(ref lapacian);
                    break;
                case eImageProcessType.Canny:
                    IpCannyParams canny = (IpCannyParams)paramObj;
                    _cannyControl.EditParam(ref canny);
                    break;
                case eImageProcessType.Median:
                    IpMedianParams median = (IpMedianParams)paramObj;
                    _medianControl.EditParam(ref median);
                    break;
                case eImageProcessType.Dilate:
                    IpDilateParams dilate = (IpDilateParams)paramObj;
                    _dilateControl.EditParam(ref dilate);
                    break;
                case eImageProcessType.Erode:
                    IpErodeParams erode = (IpErodeParams)paramObj;
                    _erodeControl.EditParam(ref erode);
                    break;
                case eImageProcessType.GreyLeveling:
                    IpGreyLevelingParams level = (IpGreyLevelingParams)paramObj;
                    _greyLevelControl.EditParam(ref level);
                    JImage currentPosImage =  GetGreyHistogram(selectIndex);
                    _greyLevelControl.UpdateHistogram(currentPosImage);
                    break;
                case eImageProcessType.LUT:
                    IpLutParams lut = (IpLutParams)paramObj;
                    _lutControl.EditParam(ref lut);
                    break;
                default:
                    break;
            }
        }
        private JImage GetGreyHistogram(int index)
        {
            JImage image = (JImage)CStatus.Instance().GetDrawBox().ImageManager.GetOrgImage().Clone();
            if (_filterList.Count <= 0)
                return image;
            int count = 0;
            foreach (IPBase item in _filterList)
            {
                if (index == count)
                    break;
                image = item.Execute(image);
                count++;
            }



            return image;
        }
        #region Filter Function
        private void FIlterEdit()
        {
            EditParam(_selectIndexUseFilter);
            FIlterThumbnailDisplay();
        }

        private void Blur()
        {
            _filterOptionControl.Controls.Add(_blurControl);
            _blurControl.Dock = DockStyle.Fill;
            _blurControl.Visible = true;
            _blurControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpBlurParams param = (IpBlurParams)_filterList[_selectIndexUseFilter].GetParam();
            _blurControl.SetParam(param);
        }

        private void Canny()
        {
            _filterOptionControl.Controls.Add(_cannyControl);
            _cannyControl.Dock = DockStyle.Fill;
            _cannyControl.Visible = true;
            _cannyControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpCannyParams param = (IpCannyParams)_filterList[_selectIndexUseFilter].GetParam();
            _cannyControl.SetParam(param);
        }

        private void Dilate()
        {
            _filterOptionControl.Controls.Add(_dilateControl);
            _dilateControl.Dock = DockStyle.Fill;
            _dilateControl.Visible = true;
            _dilateControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpDilateParams param = (IpDilateParams)_filterList[_selectIndexUseFilter].GetParam();
            _dilateControl.SetParam(param);
        }

        private void Erode()
        {
            _filterOptionControl.Controls.Add(_erodeControl);
            _erodeControl.Dock = DockStyle.Fill;
            _erodeControl.Visible = true;
            _erodeControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpErodeParams param = (IpErodeParams)_filterList[_selectIndexUseFilter].GetParam();
            _erodeControl.SetParam(param);
        }

        private void GreyLeveling()
        {
            _filterOptionControl.Controls.Add(_greyLevelControl);
            _greyLevelControl.Dock = DockStyle.Fill;
            _greyLevelControl.Visible = true;
            _greyLevelControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpGreyLevelingParams param = (IpGreyLevelingParams)_filterList[_selectIndexUseFilter].GetParam();
            _greyLevelControl.SetSliderValue(param.Low, param.High);
        }

        private void Lut()
        {
            _filterOptionControl.Controls.Add(_lutControl);
            _lutControl.Dock = DockStyle.Fill;
            _lutControl.Visible = true;
            _lutControl.FilterEdit = FIlterEdit;
           
            if (_selectIndexUseFilter < 0)
                return;
            IpLutParams param = (IpLutParams)_filterList[_selectIndexUseFilter].GetParam();
            tCurveDataInfo info = new tCurveDataInfo();
            info.keyPt = param.keyPt;
            info.Width = param.Width;
            info.Height = param.Height;
            info.LUT = param.LUT;
            _lutControl.SetInformation(ref info);

            param.keyPt = info.keyPt;
            param.Width = info.Width;
            param.Height = info.Height;
            param.LUT = info.LUT;
            //_lutControl.SetRealPoint();
        }
        private void Laplacian()
        {
            _filterOptionControl.Controls.Add(_laplacianControl);
            _laplacianControl.Dock = DockStyle.Fill;
            _laplacianControl.Visible = true;
            _laplacianControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpLaplacianParams param = (IpLaplacianParams)_filterList[_selectIndexUseFilter].GetParam();
            _laplacianControl.SetParam(param);
        }

        private void Median()
        {
            _filterOptionControl.Controls.Add(_medianControl);
            _medianControl.Dock = DockStyle.Fill;
            _medianControl.Visible = true;
            _medianControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpMedianParams param = (IpMedianParams)_filterList[_selectIndexUseFilter].GetParam();
            _medianControl.SetParam(param);
        }

        private void None()
        {
            _filterOptionControl.Controls.Add(_noneControl);
            _noneControl.Dock = DockStyle.Fill;
            _noneControl.Visible = true;
            if (_selectIndexUseFilter < 0)
                return;
        }

        private void Sobel()
        {
            _filterOptionControl.Controls.Add(_sobelControl);
            _sobelControl.Dock = DockStyle.Fill;
            _sobelControl.Visible = true;
            _sobelControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpSobelParams param = (IpSobelParams)_filterList[_selectIndexUseFilter].GetParam();
            _sobelControl.SetParam(param);
        }

        //private void Threshold()
        //{
        //    _filterOptionControl.Controls.Add(_thresholdControl);
        //    _thresholdControl.Dock = DockStyle.Fill;
        //    _thresholdControl.Visible = true;
        //    if (_selectIndexUseFilter < 0)
        //        return;
        //    IpThresholdParams param = (IpThresholdParams)_filterList[_selectIndexUseFilter].GetParam();
        //    _thresholdControl.SetParam(param);
        //}

        //private void ThresholdAdaptive()
        //{
        //    _filterOptionControl.Controls.Add(_thresholdAdaptiveControl);
        //    _thresholdAdaptiveControl.Dock = DockStyle.Fill;
        //    _thresholdAdaptiveControl.Visible = true;
        //    if (_selectIndexUseFilter < 0)
        //        return;
        //    IpThresholdAdaptiveParams param = (IpThresholdAdaptiveParams)_filterList[_selectIndexUseFilter].GetParam();
        //    _thresholdAdaptiveControl.SetParam(param);
        //}

        //private void ThresholdOtsu()
        //{
        //    _filterOptionControl.Controls.Add(_thresholdOtsuControl);
        //    _thresholdOtsuControl.Dock = DockStyle.Fill;
        //    _thresholdOtsuControl.Visible = true;
        //    if (_selectIndexUseFilter < 0)
        //        return;
        //    IpThresholdOtsuParams param = (IpThresholdOtsuParams)_filterList[_selectIndexUseFilter].GetParam();
        //    _thresholdOtsuControl.SetParam(param);
        //}

        #endregion

        private void FIlterThumbnailDisplay()
        {
            //if (_filterList.Count <= 0)
           // {
                //CStatus.Instance().GetDrawBox().Image = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.ToBitmap();
               // return;
           // }

            JImage image = (JImage)(CStatus.Instance().GetDrawBox().ImageManager.GetOrgImage().Clone());
            CStatus.Instance().ProcessingTime.Clear();

            foreach (IPBase item in _filterList)
            {
                if (item.ProcessType == eImageProcessType.LUT)
                {
                    IpLutParams param = (IpLutParams)item.GetParam();
                    if (param.LUT == null)
                        continue;
                }
                image = item.Execute(image);
            }

            CStatus.Instance().GetDrawBox().Image = image.ToBitmap();
            CStatus.Instance().HistogramFormUpdate(image);
            CStatus.Instance().ProcessingTimeFormUpdate();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            if (_filterList == null || _filterList.Count <= 0)
                return;
            CStatus.Instance().GetDrawBox().ImageManager.ImageProcessingList.Clear();
            CStatus.Instance().GetDrawBox().ImageManager.ImageProcessingList = _filterList.Select(item => item.Clone() as IPBase).ToList();

            CStatus.Instance().HistogramFormUpdate();
            //Status.Instance().RoiListFormUpdate();
        }

        private void btnFilterSave_Click(object sender, EventArgs e)
        {
            CStatus.Instance().GetDrawBox().ImageManager.ImageProcessingList.Clear();
            CStatus.Instance().GetDrawBox().ImageManager.ImageProcessingList = _filterList.Select(item => item.Clone() as IPBase).ToList();
            CStatus.Instance().HistogramFormUpdate();
            CStatus.Instance().Settings.Save();
            //Status.Instance().RoiListFormUpdate();
        }

        private void FilterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CStatus.Instance().GetDrawBox().Image = CStatus.Instance().GetDrawBox().ImageManager.CalcDisplayImage().ToBitmap();
            CStatus.Instance().HistogramFormUpdate();
        }

        private void LutTabInvalidate()
        {
            //_lutControl.SetLutParam(Status.Instance().SelectedViewer.GetBit());
            //_lutControl.UpdateLut();
        }

        private void lbxFilterList_DoubleClick(object sender, EventArgs e)
        {
            UseFilter();
            FIlterThumbnailDisplay();
        }

        private void lbxUseFilterList_DoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want delete Filter Option?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteFilter();
                FIlterThumbnailDisplay();
            }
        }

        private void FilterForm_Activated(object sender, EventArgs e)
        {
            if (_levelControl != null)
                _levelControl.IsActive = true;
        }

        private void FilterForm_Deactivate(object sender, EventArgs e)
        {
            if (_levelControl != null)
                _levelControl.IsActive = false;
        }
    }
}
