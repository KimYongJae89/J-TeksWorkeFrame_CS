using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageManipulator.FilterParamControl;
using System.Text.RegularExpressions;
using KiyLib.DIP;
using System.Threading;
using System.Diagnostics;
using LibraryGlobalization.Properties;
using ImageManipulator.Controls;
using System.Reflection;
using ImageManipulator.ImageProcessingData;
using JTeksSplineGraph.Controls;

namespace ImageManipulator.Forms
{
    public partial class FilterForm : Form
    {
        private int _selectIndexUseFilter = -1;
        UserListBox _lbxFilterList;
        UserListBox _lbxUserFilterList;

        private BlurControl _blurControl = null;
        private CannyControl _cannyControl = null;
        private DilateControl _dilateControl = null;
        private ErodeControl _erodeControl = null;
        private LaplacianControl _laplacianControl = null;
        private MedianControl _medianControl = null;
        private NoneControl _noneControl = null;
        private SobelControl _sobelControl = null;

        private LutControl _lut8BitControl = null;
        private LutControl _lut16BitControl = null;

        private LevelingControl _level8BitControl = null;
        private LevelingControl _level16BitControl = null;

        private ThresholdControl _thresholdControl = null;
        private AdaptiveThresholdControl _adaptiveThresholdControl = null;

        private List<IPBase> _filterList = new List<IPBase>();
        //private int _selectedTabNum = 0;
        public Action CloseEventDelegate;
        DoubleBufferPanel _filterOptionControl;
        //private string _prevSelectedTabName;
        private int _levelingLow;
        private int _levelinghigh;
        //private int[] _lutValue;
        private bool inoreEvent = false;

        public FilterForm()
        {
            InitializeComponent();
        }

        private void FilterDialog_Load(object sender, EventArgs e)
        {
            try
            {
                Status.Instance().LogManager.AddLogMessage("Filter Form Open", "");
                this.MinimumSize = new Size(538, 452);

                CreateControls();
                MultiLanguage();

                inoreEvent = true;
                ComboBoxUpdate();
                inoreEvent = false;

                _filterOptionControl = new DoubleBufferPanel();
                FilterOptionPanel.Controls.Add(_filterOptionControl);
                _filterOptionControl.Dock = System.Windows.Forms.DockStyle.Fill;

                _filterOptionControl.Controls.Add(_noneControl);
                _noneControl.Dock = DockStyle.Fill;
                _noneControl.Visible = true;

                _lbxFilterList = new UserListBox();
                FilterListPanel.Controls.Add(_lbxFilterList);
                _lbxFilterList.Dock = DockStyle.Fill;
                _lbxFilterList.Visible = true;
                _lbxFilterList.DoubleClick += FilterListBoxDoubleClick;

                _lbxUserFilterList = new UserListBox();
                UseFilterListPanel.Controls.Add(_lbxUserFilterList);
                _lbxUserFilterList.Dock = DockStyle.Fill;
                _lbxUserFilterList.Visible = true;
                _lbxUserFilterList.Click += UserListBoxClick;
                _lbxUserFilterList.DoubleClick += UserListBoxDoubleClick;
                //

                for (int i = 0; i < Enum.GetNames(typeof(eImageProcessType)).Length; i++)
                {
                    eImageProcessType filtertype = (eImageProcessType)i;
                    if (filtertype == eImageProcessType.Resize || filtertype == eImageProcessType.Crop
                        || filtertype == eImageProcessType.UserFilter || filtertype == eImageProcessType.Basic)
                        continue;
                    _lbxFilterList.Items.Add(filtertype.ToString());
                }
                FilterListUpdate();
                UseFilterListUpdate();
                //
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        private void CreateControls()
        {
            _blurControl = new BlurControl();
            _cannyControl = new CannyControl();
            _dilateControl = new DilateControl();
            _erodeControl = new ErodeControl();
            _laplacianControl = new LaplacianControl();
            _medianControl = new MedianControl();
            _noneControl = new NoneControl();
            _sobelControl = new SobelControl();

            _lut8BitControl = new LutControl();
            _lut8BitControl.SendLutInfo = SendLutInfo;
            _lut8BitControl.Bit = 8;
            _lut16BitControl = new LutControl();
            _lut16BitControl.SendLutInfo = SendLutInfo;
            _lut16BitControl.Bit = 16;

            _level8BitControl = new LevelingControl();
            _level16BitControl = new LevelingControl();
            _thresholdControl = new ThresholdControl();
            _adaptiveThresholdControl = new AdaptiveThresholdControl();
        }
        private void UserListBoxDoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want delete Filter Option?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteFilter();
            }
        }

        private void UserListBoxClick(object sender, EventArgs e)
        {
            if (_selectIndexUseFilter < 0)
                return;
            if((eImageProcessType)_lbxUserFilterList.Items[_lbxUserFilterList.SelectedIndex] == eImageProcessType.LUT_8)
            {
                if (Status.Instance().SelectedViewer.ImageManager.GetBit() == 16)
                    return;
            }

            if ((eImageProcessType)_lbxUserFilterList.Items[_lbxUserFilterList.SelectedIndex] == eImageProcessType.LUT_16)
            {
                if (Status.Instance().SelectedViewer.ImageManager.GetBit() == 8)
                    return;
            }
            if ((eImageProcessType)_lbxUserFilterList.Items[_lbxUserFilterList.SelectedIndex] == eImageProcessType.Leveling_8)
            {
                if (Status.Instance().SelectedViewer.ImageManager.GetBit() == 16)
                    return;
            }
            if ((eImageProcessType)_lbxUserFilterList.Items[_lbxUserFilterList.SelectedIndex] == eImageProcessType.Leveling_16)
            {
                if (Status.Instance().SelectedViewer.ImageManager.GetBit() == 8 || Status.Instance().SelectedViewer.ImageManager.IsColor())
                    return;
            }
            if (Status.Instance().SelectedViewer.ImageManager.IsColor())
            {
                if ((eImageProcessType)_lbxUserFilterList.Items[_lbxUserFilterList.SelectedIndex] == eImageProcessType.Threshold
                    || (eImageProcessType)_lbxUserFilterList.Items[_lbxUserFilterList.SelectedIndex] == eImageProcessType.ThresholdAdaptive)
                    return;
            }
            EditParam(_selectIndexUseFilter);
            _selectIndexUseFilter = _lbxUserFilterList.SelectedIndex;

            eImageProcessType type = (eImageProcessType)_lbxUserFilterList.Items[_selectIndexUseFilter];
            ViewFilterControl(type);
            FIlterThumbnailDisplay();
        }

        private void FilterListBoxDoubleClick(object sender, EventArgs e)
        {
            UseFilter();
            FIlterThumbnailDisplay();
        }
        private void FilterListUpdate()
        {
            for (int i = 0; i < _lbxFilterList.Items.Count; i++)
            {
                _lbxFilterList.EnableItem(i);
            }

            for (int i = 0; i < _lbxFilterList.Items.Count; i++)
            {
                if (eImageProcessType.LUT_8.ToString() == _lbxFilterList.Items[i].ToString())
                {
                    if (Status.Instance().SelectedViewer.ImageManager.GetBit() == 16)
                        _lbxFilterList.DisableItem(i);
                }

                if (eImageProcessType.LUT_16.ToString() == _lbxFilterList.Items[i].ToString())
                {
                    if (Status.Instance().SelectedViewer.ImageManager.GetBit() == 8)
                        _lbxFilterList.DisableItem(i);
                }

                if (eImageProcessType.Leveling_8.ToString() == _lbxFilterList.Items[i].ToString())
                {
                    if (Status.Instance().SelectedViewer.ImageManager.GetBit() == 16)
                        _lbxFilterList.DisableItem(i);
                }

                if (eImageProcessType.Leveling_16.ToString() == _lbxFilterList.Items[i].ToString())
                {
                    if (Status.Instance().SelectedViewer.ImageManager.GetBit() == 8 || Status.Instance().SelectedViewer.ImageManager.IsColor())
                        _lbxFilterList.DisableItem(i);
                }

                if (Status.Instance().SelectedViewer.ImageManager.IsColor())
                {
                    if (eImageProcessType.Threshold.ToString() == _lbxFilterList.Items[i].ToString()
                        || eImageProcessType.ThresholdAdaptive.ToString() == _lbxFilterList.Items[i].ToString())
                        _lbxFilterList.DisableItem(i);
                }
                _lbxFilterList.Invalidate();
            }
        }
        private void UseFilterListUpdate()
        {
            try
            {
                for (int i = 0; i < _lbxUserFilterList.Items.Count; i++)
                {
                    _lbxUserFilterList.EnableItem(i);
                }
                _lbxUserFilterList.Items.Clear();
                _filterList.Clear();
                lblFilterDescription.Text = "";
                AllFilterControlFalseVisible();
                foreach (IPBase item in Status.Instance().SelectedViewer.ImageManager.ImageProcessingList)
                {
                    int value = (int)item.ProcessType;
                    _lbxFilterList.SetSelected(value, true);
                    UseFilter();
                }
                FIlterThumbnailDisplay();
                _lbxUserFilterList.Invalidate();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
           
        }

        private void MultiLanguage()
        {
            this.Text = LangResource.Filters;
            lblSourceImage.Text = LangResource.SourceImage + " : ";
            lblDestinationImage.Text = LangResource.DestinationImage + " : ";
            btnFilterApply.Text = LangResource.Apply;
            btnFilterClose.Text = LangResource.Close;
        }

        private void SendLutInfo(tCurveDataInfo info)
        {
            IpLutParams param = (IpLutParams)_filterList[_selectIndexUseFilter].GetParam();

            param.keyPt = info.keyPt;
            param.Width = info.Width;
            param.Height = info.Height;
            param.LUT = info.LUT;

            FIlterThumbnailDisplay();
            //Status.Instance().RoiListFormUpdate();
            //Status.Instance().HistogramFormUpdate();
        }

        private void SliderUpdate(int low, int high)
        {
            _levelingLow = low;
            _levelinghigh = high;
    
            JImage displayImage = (JImage)(Status.Instance().SelectedViewer.ImageManager.CalcImage.Clone());
        

            if (!Status.Instance().SelectedViewer.IsColor())
            {
                JImage image = displayImage.WndLvGray(low, high);
                Status.Instance().SelectedViewer.ImageManager.DisPlayImage = image;
                Status.Instance().SelectedViewer.GetDrawBox().Image = image.ToBitmap();
            }
            else
            {
                JImage image = displayImage.WndLvColor(low, high, 256);
                Status.Instance().SelectedViewer.ImageManager.DisPlayImage = image;
                Status.Instance().SelectedViewer.GetDrawBox().Image = image.ToBitmap();
            }
       
            Status.Instance().RoiListFormUpdate();
            Status.Instance().HistogramFormUpdate();
        }

        public void ComboBoxUpdate()
        {
            inoreEvent = true;
            string selectedFormName = Status.Instance().SelectedViewer.Text;
            cbxSourceImage.Items.Clear();
            cbxDestinationImage.Items.Clear();


            foreach (Form openForm in Application.OpenForms)
            { 
                string tag = (string)openForm.Tag;
                if (tag == "Viewer")
                {
                    string formName = openForm.Text;
                    cbxSourceImage.Items.Add(formName);
                }
            }
            for (int i = 0; i < cbxSourceImage.Items.Count; i++)
            {
                if(selectedFormName == cbxSourceImage.Items[i].ToString())
                {
                    cbxSourceImage.SelectedIndex = i;
                }
            }

            cbxDestinationImage.Items.Add("Source Image");
            cbxDestinationImage.Items.Add("New Image");
            cbxDestinationImage.SelectedIndex = 1;

            inoreEvent = false;
        }

        private void btnUseFilter_Click(object sender, EventArgs e)
        {
            UseFilter();
        }

        private void btnDeleteFilter_Click(object sender, EventArgs e)
        {
            DeleteFilter();
        }

        private void UseFilter()
        {
            int index = _lbxFilterList.SelectedIndex;
            if (index < 0)
                return;

            _lbxUserFilterList.Items.Add((eImageProcessType)index);
            _lbxUserFilterList.SelectedIndex = _lbxUserFilterList.Items.Count - 1;

            FilterListAdd((eImageProcessType)index);

            _selectIndexUseFilter = _lbxUserFilterList.SelectedIndex;
            ViewFilterControl((eImageProcessType)index);
        }

        private void DeleteFilter()
        {
            int index = _lbxUserFilterList.SelectedIndex;
            if (index < 0)
                return;

            _selectIndexUseFilter--;

            _lbxUserFilterList.Items.RemoveAt(index);
            _lbxUserFilterList.SelectedIndex = _selectIndexUseFilter;
            _filterList.RemoveAt(index);

            if (_selectIndexUseFilter < 0)
            {
                AllFilterControlFalseVisible();
                None();
                lblFilterDescription.Text = "";
                return;
            }
            EditParam(_selectIndexUseFilter);
            _selectIndexUseFilter = _lbxUserFilterList.SelectedIndex;

            eImageProcessType type = (eImageProcessType)_lbxUserFilterList.Items[_selectIndexUseFilter];
            ViewFilterControl(type);

        }

        private void btnUserFilterListUp_Click(object sender, EventArgs e)
        {
            if (_lbxUserFilterList.SelectedIndex <= 0)
                return;
            int up_item_num = _lbxUserFilterList.SelectedIndex;
            int down_item_num = _lbxUserFilterList.SelectedIndex - 1;
            if (_lbxUserFilterList.SelectedIndex < 0 || up_item_num < 0 || down_item_num < 0)
                return;

            //_filterList 순서 이동
            _filterList.Reverse(_selectIndexUseFilter - 1, 2);

            eImageProcessType upMessage = (eImageProcessType)_lbxUserFilterList.Items[up_item_num];
            eImageProcessType downMessage = (eImageProcessType)_lbxUserFilterList.Items[down_item_num];

            _lbxUserFilterList.Items[down_item_num] = upMessage;
            _lbxUserFilterList.Items[up_item_num] = downMessage;

            FilterListUpdate();
            UseFilterListUpdate();

            _lbxUserFilterList.SelectedIndex = down_item_num;
            _selectIndexUseFilter = _lbxUserFilterList.SelectedIndex;
        }

        private void btnUserFilterListDown_Click(object sender, EventArgs e)
        {
            if (_lbxUserFilterList.SelectedIndex < 0)
                return;
            int down_item_num = _lbxUserFilterList.SelectedIndex;
            int up_item_num = _lbxUserFilterList.SelectedIndex + 1;
            if (_lbxUserFilterList.SelectedIndex < 0 || up_item_num < 0 || down_item_num < 0 || up_item_num >= _lbxUserFilterList.Items.Count )
                return;

            //_filterList 순서 이동
            _filterList.Reverse(_selectIndexUseFilter, 2);

            eImageProcessType downMessage = (eImageProcessType)_lbxUserFilterList.Items[down_item_num];
            eImageProcessType upMessage = (eImageProcessType)_lbxUserFilterList.Items[up_item_num];

            _lbxUserFilterList.Items[down_item_num] = upMessage;
            _lbxUserFilterList.Items[up_item_num] = downMessage;

            FilterListUpdate();
            UseFilterListUpdate();

            _lbxUserFilterList.SelectedIndex = up_item_num;
            _selectIndexUseFilter = _lbxUserFilterList.SelectedIndex;
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
                case eImageProcessType.LUT_8:
                    Lut_8();
                    break;
                case eImageProcessType.LUT_16:
                    Lut_16();
                    break;
                case eImageProcessType.Leveling_8:
                    Leveling_8();
                    break;
                case eImageProcessType.Leveling_16:
                    Leveling_16();
                    break;
                case eImageProcessType.Threshold:
                    Threshold();
                    break;
                case eImageProcessType.ThresholdAdaptive:
                    ThresholdAdaptive();
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
                case eImageProcessType.Leveling_8:
                case eImageProcessType.Leveling_16:
                    lblFilterDescription.Text = "";
                    break;
                case eImageProcessType.LUT_8:
                case eImageProcessType.LUT_16:
                    lblFilterDescription.Text = "";
                    break;
                case eImageProcessType.Threshold:
                case eImageProcessType.ThresholdAdaptive:
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
            _lut8BitControl.Visible = false;
            _lut16BitControl.Visible = false;
            _level8BitControl.Visible = false;
            _level16BitControl.Visible = false;
            _thresholdControl.Visible = false;
            _adaptiveThresholdControl.Visible = false;
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
                case eImageProcessType.Leveling_8:
                    filter = new IP_Leveling8();
                    break;
                case eImageProcessType.Leveling_16:
                    filter = new IP_Leveling16();
                    break;
                case eImageProcessType.LUT_8:
                    filter = new IP_LUT8Bit();
                    break;
                case eImageProcessType.LUT_16:
                    filter = new IP_LUT16Bit();
                    break;
                case eImageProcessType.Threshold:
                    filter = new IP_Threshold();
                    break;
                case eImageProcessType.ThresholdAdaptive:
                    filter = new IP_ThresholdAdaptive();
                    break;
                default:
                    break;
            }
            if (filter != null)
                _filterList.Add(filter);
        }

        //private void lbxUseFilterList_Click(object sender, EventArgs e)
        //{

        //    EditParam(_selectIndexUseFilter);
        //    _selectIndexUseFilter = _lbxUserFilterList.SelectedIndex;

        //    eImageProcessType type = (eImageProcessType)_lbxUserFilterList.Items[_selectIndexUseFilter];
        //    ViewFilterControl(type);

        //    FIlterThumbnailDisplay();
        //}

        private void EditParam(int selectIndex)
        {
            if (selectIndex < 0)
                return;
            eImageProcessType type = (eImageProcessType)_lbxUserFilterList.Items[selectIndex];
            object paramObj  = _filterList[selectIndex].GetParam();
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
                case eImageProcessType.LUT_8:
                    IpLutParams lut8 = (IpLutParams)paramObj;
                    _lut8BitControl.EditParam(ref lut8);
                    break;
                case eImageProcessType.LUT_16:
                    IpLutParams lut16 = (IpLutParams)paramObj;
                    _lut16BitControl.EditParam(ref lut16);
                    break;
                case eImageProcessType.Leveling_8:
                    IpLevelingParams level8 = (IpLevelingParams)paramObj;
                    _level8BitControl.EditParam(ref level8);
                    JImage current8BitImage = GetHistogramImage(selectIndex);
                    _level8BitControl.UpdateHistogram(current8BitImage);
                    break;
                case eImageProcessType.Leveling_16:
                    IpLevelingParams level16 = (IpLevelingParams)paramObj;
                    _level16BitControl.EditParam(ref level16);
                    JImage current16BitImage = GetHistogramImage(selectIndex);
                    _level16BitControl.UpdateHistogram(current16BitImage);
                    break;
                case eImageProcessType.Threshold:
                    IpThresholdParams threshold = (IpThresholdParams)paramObj;
                    _thresholdControl.EditParam(ref threshold);
                    break;
                case eImageProcessType.ThresholdAdaptive:
                    IpThresholdAdaptiveParams adaptive = (IpThresholdAdaptiveParams)paramObj;
                    _adaptiveThresholdControl.EditParam(ref adaptive);
                    break;
                default:
                    break;
            }
        }

        private JImage GetHistogramImage(int index)
        {
            JImage image = (JImage)Status.Instance().SelectedViewer.ImageManager.GetOrgImage().Clone();
            if (_filterList.Count <= 0)
                return image;
            int count = 0;
            foreach (IPBase item in _filterList)
            {
                if (count == index)
                    break;
                if (!Status.Instance().CheckEanbleProcessing(item.ProcessType))
                {
                    count++;
                    continue;
                }

                image = item.Execute(image);

           
                count++;
            }
            return image;
        }
        #region Filter Function

        private void Blur()
        {
            _filterOptionControl.Controls.Add(_blurControl);
            _blurControl.Dock = DockStyle.Fill;
            _blurControl.Visible = true;
            _erodeControl.FilterEdit = FIlterEdit;

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
            _erodeControl.FilterEdit = FIlterEdit;

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
            _erodeControl.FilterEdit = FIlterEdit;

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

        private void Laplacian()
        {
            _filterOptionControl.Controls.Add(_laplacianControl);
            _laplacianControl.Dock = DockStyle.Fill;
            _laplacianControl.Visible = true;
            _erodeControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpLaplacianParams param = (IpLaplacianParams)_filterList[_selectIndexUseFilter].GetParam();
            _laplacianControl.SetParam(param);
        }

        private void Lut_8()
        {
            _filterOptionControl.Controls.Add(_lut8BitControl);
            _lut8BitControl.Dock = DockStyle.Fill;
            _lut8BitControl.Visible = true;
            _lut8BitControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpLutParams param = (IpLutParams)_filterList[_selectIndexUseFilter].GetParam();
            tCurveDataInfo info = new tCurveDataInfo();
            info.keyPt = param.keyPt;
            info.Width = param.Width;
            info.Height = param.Height;
            info.LUT = param.LUT;
            _lut8BitControl.SetInformation(ref info);

            param.keyPt = info.keyPt;
            param.Width = info.Width;
            param.Height = info.Height;
            param.LUT = info.LUT;
        }

        private void Lut_16()
        {
            _filterOptionControl.Controls.Add(_lut16BitControl);
            _lut16BitControl.Dock = DockStyle.Fill;
            _lut16BitControl.Visible = true;
            _lut16BitControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpLutParams param = (IpLutParams)_filterList[_selectIndexUseFilter].GetParam();
            tCurveDataInfo info = new tCurveDataInfo();
            info.keyPt = param.keyPt;
            info.Width = param.Width;
            info.Height = param.Height;
            info.LUT = param.LUT;
            _lut16BitControl.SetInformation(ref info);

            param.keyPt = info.keyPt;
            param.Width = info.Width;
            param.Height = info.Height;
            param.LUT = info.LUT;
        }

        private void Leveling_8()
        {
            _filterOptionControl.Controls.Add(_level8BitControl);
            _level8BitControl.Dock = DockStyle.Fill;
            _level8BitControl.Visible = true;
            _level8BitControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpLevelingParams param = (IpLevelingParams)_filterList[_selectIndexUseFilter].GetParam();
            _level8BitControl.SetSliderValue(param.Low, param.High);
        }

        private void Leveling_16()
        {
            _filterOptionControl.Controls.Add(_level16BitControl);
            _level16BitControl.Dock = DockStyle.Fill;
            _level16BitControl.Visible = true;
            _level16BitControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpLevelingParams param = (IpLevelingParams)_filterList[_selectIndexUseFilter].GetParam();
            _level16BitControl.SetSliderValue(param.Low, param.High);
        }

        private void Threshold()
        {
            _filterOptionControl.Controls.Add(_thresholdControl);
            _thresholdControl.Dock = DockStyle.Fill;
            _thresholdControl.Visible = true;
            _thresholdControl.FilterEdit = FIlterEdit;
        }

        private void ThresholdAdaptive()
        {
            _filterOptionControl.Controls.Add(_adaptiveThresholdControl);
            _adaptiveThresholdControl.Dock = DockStyle.Fill;
            _adaptiveThresholdControl.Visible = true;
            _adaptiveThresholdControl.FilterEdit = FIlterEdit;
        }

        private void Median()
        {
            _filterOptionControl.Controls.Add(_medianControl);
            _medianControl.Dock = DockStyle.Fill;
            _medianControl.Visible = true;
            _erodeControl.FilterEdit = FIlterEdit;

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
            _erodeControl.FilterEdit = FIlterEdit;

            if (_selectIndexUseFilter < 0)
                return;
            IpSobelParams param = (IpSobelParams)_filterList[_selectIndexUseFilter].GetParam();
            _sobelControl.SetParam(param);
        }

        private void FIlterEdit()
        {
            EditParam(_selectIndexUseFilter);
            FIlterThumbnailDisplay();
        }
        #endregion

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            if (_filterList == null || _filterList.Count <= 0)
                return;
            if(cbxSourceImage.SelectedItem as string == "" || cbxSourceImage.SelectedItem == null)
            {
                MessageBox.Show("Source Image is not exist.");
                return;
            }
            Viewer sourceViewer;
            string sourceImageName = cbxSourceImage.SelectedItem as string;
            string destinationImageName = cbxDestinationImage.SelectedItem as string;
            foreach (Form openForm in Application.OpenForms)
            {
                string tag = (string)openForm.Tag;
                if (tag == "Viewer") //Viewer만
                {
                    string formName = openForm.Text;
                    if(sourceImageName == formName) 
                    {
                        sourceViewer = (Viewer)openForm;
                        if (destinationImageName == "New Image")
                        {
                            //
                            Status.Instance().SelectedViewer.GetDrawBox().Image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
                            //
                            JImage orgImage = (JImage)(sourceViewer.ImageManager.GetOrgImage()).Clone();
                            Viewer newForm = new Viewer(orgImage, Status.Instance().SelectedViewer.ImageManager.ImageFilePath);

                            newForm.EnableKeyEvent = false;
                            string newFormName = Status.Instance().GetNewFileName(sourceImageName);
                            newForm.Text = newFormName;
                            newForm.ImageManager.ImageProcessingList = _filterList.Select(item => item.Clone() as IPBase).ToList();
                            newForm.Show();

                            break;
                        }
                        if (destinationImageName == "Source Image")
                        {
                            sourceViewer.ImageManager.ImageProcessingList.Clear();
                            sourceViewer.ImageManager.ImageProcessingList = _filterList.Select(item => item.Clone() as IPBase).ToList();
                            sourceViewer.GetDrawBox().Image = sourceViewer.ImageManager.CalcDisplayImage().ToBitmap();
                            break;
                        }
                    }
                }
            }
            ComboBoxUpdate();
            Status.Instance().RoiListFormUpdate();
            Status.Instance().HistogramFormUpdate();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            JImage displayImage = (JImage)(Status.Instance().SelectedViewer.ImageManager.CalcImage.Clone());

            Status.Instance().SelectedViewer.GetDrawBox().Image = displayImage.ToBitmap();

            this.Close();
        }

        private void FilterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Status.Instance().LogManager.AddLogMessage("Filter Form Close", "");
            if (CloseEventDelegate != null)
                CloseEventDelegate();
        }

        public void CloseViewerEvent()
        {
            cbxSourceImage.Items.Clear();
            cbxSourceImage.SelectedIndex = -1;
            foreach (Form openForm in Application.OpenForms)
            {
                string tag = (string)openForm.Tag;
                if (tag == "Viewer")
                {
                    cbxSourceImage.Items.Add(openForm.Text);
                }
            }
        }

        public void UpdateForm()
        {
            string formName = Status.Instance().SelectedViewer.Text;
            inoreEvent = true;
            for (int i = 0; i < cbxSourceImage.Items.Count; i++)
            {
                if (formName == cbxSourceImage.Items[i].ToString())
                    cbxSourceImage.SelectedIndex = i;
            }
            FilterListUpdate();
            UseFilterListUpdate();

            inoreEvent = false;
        }

        private void cbxSourceImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inoreEvent)
                return;
            string selectedFormName = cbxSourceImage.SelectedItem as string;
            //Console.WriteLine("FilterForm 업데이트");
            //모든 폼 업데이트
            foreach (Form openForm in Application.OpenForms)
            {
                string tag = (string)openForm.Tag;
                if (tag == "Viewer") //Viewer만
                {
                    if (selectedFormName == openForm.Text)
                    {
                        Status.Instance().LogManager.AddLogMessage("Filter Source Image", "Change");
                        Status.Instance().PrevSelectedViewer =  Status.Instance().SelectedViewer;
                        Status.Instance().SelectedViewer = (Viewer)openForm;
                        FilterListUpdate();
                        UseFilterListUpdate();
                    }
                }
            }
        }

        private void lbxFilterList_DoubleClick(object sender, EventArgs e)
        {
            UseFilter();
        }

        private void lbxUseFilterList_DoubleClick(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want delete Filter Option?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteFilter();
            }
        }

        private void FilterForm_Activated(object sender, EventArgs e)
        {
            //if(_levelControl != null)
            //_levelControl.IsActive = true;
        }

        private void FilterForm_Deactivate(object sender, EventArgs e)
        {
            //if (_levelControl != null)
            //    _levelControl.IsActive = false;
        }

        private void FIlterThumbnailDisplay()
        {
            JImage image = (JImage)(Status.Instance().SelectedViewer.ImageManager.GetOrgImage().Clone());
            Status.Instance().ProcessingTime.Clear();
            int count = 0;
            foreach (IPBase item in _filterList)
            {
                if(!Status.Instance().CheckEanbleProcessing(item.ProcessType))
                {
                    count++;
                    continue;
                }
                
                image = item.Execute(image);
                Status.Instance().LogManager.AddLogMessage("Filter ThumbnailDisplay", item.ProcessType.ToString());
                Status.Instance().ProcessingTimeFormUpdate();

                if (count == _selectIndexUseFilter)
                    break;
                count++;
            }

            Status.Instance().SelectedViewer.GetDrawBox().Image = image.ToBitmap();
        }

        public void LocalHistogram(RectangleF orgRect)
        {
            if (_selectIndexUseFilter < 0)
                return;
            eImageProcessType type = (eImageProcessType)_lbxUserFilterList.Items[_selectIndexUseFilter];

            if(type == eImageProcessType.Leveling_16 || type == eImageProcessType.Leveling_8)
            {
                JImage image = (JImage)(Status.Instance().SelectedViewer.ImageManager.GetOrgImage().Clone());
                int count = 0;
                foreach (IPBase item in _filterList)
                {
                    if (!Status.Instance().CheckEanbleProcessing(item.ProcessType))
                    {
                        count++;
                        continue;
                    }

                    if (count == _selectIndexUseFilter)
                    {
                        break;
                    }
                  
                    image = item.Execute(image);
                    count++;
                }

                int min, max;
                image.GetROIInfo(new Rectangle((int)orgRect.X, (int)orgRect.Y, (int)orgRect.Width, (int)orgRect.Height)).GetHistoFirstNonZeroAtBothSideGray(out min, out max);

                if(image.Depth == KDepthType.Dt_8)
                {
                    if (_level8BitControl == null)
                        return;
                    _level8BitControl.SetSliderValue(min, max);
                }
                if(image.Depth == KDepthType.Dt_16)
                {
                    if (_level16BitControl == null)
                        return;
                    _level16BitControl.SetSliderValue(min, max);
                }
            }

        }
    }
}
