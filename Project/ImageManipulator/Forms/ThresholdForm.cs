using ImageManipulator.FilterParamControl;
using ImageManipulator.ImageProcessingData;
using KiyLib.DIP;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulator.Forms
{
    public partial class ThresholdForm : Form
    {
        public Action CloseEventDelegate;
        private bool _isBlockSizeNotUpdate = false;
        private bool _isTypeNotUpdate = false;
        public ThresholdForm()
        {
            InitializeComponent();
        }

        private void ThresholdForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = new Size(224, 276);
            this.MinimumSize = new Size(224, 276);
            this.Text = LangResource.Threshold;
            UpdateForm();
        }

        private void NumbericUpDownInitialize()
        {
            int bit = Status.Instance().SelectedViewer.ImageManager.GetBit();
            nupdnValue.Value = 0;
            if (bit == 8)
            {
                nupdnValue.Maximum = 255;
                nupdnValue.Minimum = 0;
            }
            else if (bit == 16)
            {
                nupdnValue.Maximum = 65535;
                nupdnValue.Minimum = 0;
            }
            else { }

            if(!Status.Instance().SelectedViewer.IsColor())
            {
                if(bit == 8 || bit == 16)
                {
                    JImage displayImage = (JImage)Status.Instance().SelectedViewer.ImageManager.CalcImage.Clone();
                    int value;
                    displayImage.ThresholdOtsu(out value);
                    nupdnValue.Value = value;
                }
            }
        }

        private void ThresholdForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           // try
           // {

                Status.Instance().SelectedViewer.GetDrawBox().Image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
                if (CloseEventDelegate != null)
                    CloseEventDelegate();
           // }
           // catch (Exception err)
           // {
            //    Console.WriteLine(err.Message);
           // }
            
        }

        private void nupdnValue_ValueChanged(object sender, EventArgs e)
        {
            ThresholdUpdate();
        }

        private void cbxOtsuEnable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbxOtsuEnable.Checked)
                {
                    if(!Status.Instance().SelectedViewer.IsColor())
                    {
                        int bit = Status.Instance().SelectedViewer.GetBit();
                        if (bit == 8 || bit == 16)
                        {
                            JImage displayImage = (JImage)Status.Instance().SelectedViewer.ImageManager.CalcImage.Clone();
                            int value;
                            displayImage.ThresholdOtsu(out value);
                            nupdnValue.Value = value;
                        }
                    }

                    nupdnValue.Enabled = false;
                }
                else
                {
                    nupdnValue.Enabled = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }
        }

        private void cbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isTypeNotUpdate)
                return;
            ThresholdAdaptiveUpdate();
        }

        private void ThresholdAdaptiveUpdate()
        {
            try
            {
                ThresAdaptiveType type = (ThresAdaptiveType)cbxType.SelectedIndex;
                int blockSize = (int)nupdnBlockSize.Value;
                int weight = (int)nupdnWeight.Value;

                if (blockSize % 2 == 0)
                {
                    _isBlockSizeNotUpdate = true;
                    blockSize += 1;
                    if (blockSize >= 100)
                        blockSize = 99;
                    nupdnBlockSize.Value = blockSize;
                }
                JImage displayImage = (JImage)Status.Instance().SelectedViewer.ImageManager.CalcImage.Clone();
                int bit = Status.Instance().SelectedViewer.GetBit();
                if (!Status.Instance().SelectedViewer.IsColor())
                {
                    if (bit == 8 || bit == 16)
                    {
                        JImage image = displayImage.ThresholdAdaptive(type, blockSize, weight);
                        Status.Instance().SelectedViewer.GetDrawBox().Image = image.ToBitmap();
                    }
                }

                _isBlockSizeNotUpdate = false;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private void ThresholdUpdate()
        {
            JImage displayImage = (JImage)Status.Instance().SelectedViewer.ImageManager.CalcImage.Clone();
            if (!Status.Instance().SelectedViewer.IsColor())
            {
                int bit = Status.Instance().SelectedViewer.GetBit();
                if (bit == 8 || bit == 16)
                {
                    if (cbxOtsuEnable.Checked)
                    {
                        int value;
                        JImage image = displayImage.ThresholdOtsu(out value);
                        nupdnValue.Value = value;
                        Status.Instance().SelectedViewer.GetDrawBox().Image = image.ToBitmap();
                    }
                    else
                    {
                        int value = (int)nupdnValue.Value;
                        JImage image = displayImage.Threshold(value);
                        Status.Instance().SelectedViewer.GetDrawBox().Image = image.ToBitmap();
                    }
                }
            }
        }

        private void nupdnBlockSize_ValueChanged(object sender, EventArgs e)
        {
            if (_isBlockSizeNotUpdate)
                return;
            ThresholdAdaptiveUpdate();
        }

        private void nupdnWeight_ValueChanged(object sender, EventArgs e)
        {
            ThresholdAdaptiveUpdate();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            string tabName = MaintabControl.SelectedTab.Text;

            if(tabName == "Threshold")
            {
                ThresholdUpdate();
            }
            if(tabName == "Adaptive")
            {
                ThresholdAdaptiveUpdate();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (Status.Instance().SelectedViewer == null)
                    return;
                if (Status.Instance().SelectedViewer.IsColor())
                {
                    MessageBox.Show(LangResource.ER_JIMG_CantSupportColorImg);   
                    return;
                }
                string tabName = MaintabControl.SelectedTab.Text;

                //if (tabName == "Threshold")
                //{
                //    if (!cbxOtsuEnable.Checked)
                //    {
                //        IP_Threshold threshold = new IP_Threshold();
                //        IpThresholdParams param = new IpThresholdParams();
                //        param.value = (int)nupdnValue.Value;
                //        threshold.SetParam(param);
                //        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(threshold);
                //        Status.Instance().SelectedViewer.GetDrawBox().Image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
                //    }
                //    else
                //    {
                //        IP_ThresholdOtsu thresholdOtsu = new IP_ThresholdOtsu();
                //        IpThresholdOtsuParams param = new IpThresholdOtsuParams();
                //        param.value = (int)nupdnValue.Value;
                //        thresholdOtsu.SetParam(param);
                //        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(thresholdOtsu);
                //        Status.Instance().SelectedViewer.GetDrawBox().Image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
                //    }
                //}
                //if (tabName == "Adaptive")
                //{
                //    IP_ThresholdAdaptive thresholdAdaptive = new IP_ThresholdAdaptive();
                //    IpThresholdAdaptiveParams param = new IpThresholdAdaptiveParams();
                //    param.type = (ThresAdaptiveType)cbxType.SelectedIndex;
                //    param.blockSize = (int)nupdnBlockSize.Value;
                //    param.weight = (int)nupdnWeight.Value;
                //    thresholdAdaptive.SetParam(param);

                //    Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(thresholdAdaptive);
                //    Status.Instance().SelectedViewer.GetDrawBox().Image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
                //}

                Status.Instance().RoiListFormUpdate();
                Status.Instance().HistogramFormUpdate();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int gg = Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Count();
            Status.Instance().SelectedViewer.GetDrawBox().Image = Status.Instance().SelectedViewer.ImageManager.CalcDisplayImage().ToBitmap();
            this.Close();
        }

        public void UpdateForm()
        {
            cbxType.Items.Clear();
            bool isColor = Status.Instance().SelectedViewer.ImageManager.IsColor();
            if (isColor)
                return;

            NumbericUpDownInitialize();

            _isTypeNotUpdate = true;
            for (int i = 0; i < Enum.GetNames(typeof(ThresAdaptiveType)).Length; i++)
            {
                ThresAdaptiveType type = (ThresAdaptiveType)i;
                cbxType.Items.Add(type.ToString());
            }
            cbxType.SelectedIndex = 0;

            _isTypeNotUpdate = false;
        }
    }
}
