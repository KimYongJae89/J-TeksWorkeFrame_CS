using ImageManipulator.Data;
using ImageManipulator.PixelCalibrationControl;
using ImageManipulator.Util;
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
    public partial class PixelCalibrationPage1Form : Form
    {
        private int _chapter = 1;
        private TextControl _textControl = new TextControl();
        private SetPixelSizeControl _setPixelSizeControl = new SetPixelSizeControl();
        public Action FormCloseingDelegate;
        public PixelCalibrationPage1Form()
        {
            InitializeComponent();
        }

        private void PixelCalibrationPage1_Load(object sender, EventArgs e)
        {
            this.MaximumSize = new Size(318, 155);
            this.MinimumSize = new Size(318, 155);
            _chapter = 1;
            this.Text = LangResource.CalibratePixelSize;
            btnNext.Text = LangResource.Next;
            btnPrev.Text = LangResource.Prev;

            DispalyPanel.Controls.Add(_textControl);
            _textControl.SetMessage(LangResource.PixelCalibrationPage1 + "\n" + LangResource.PixelCalibrationPage1_1);
            _textControl.Dock = DockStyle.Fill;
            _textControl.Visible = true;
            AdjustButtonState();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Status.Instance().SelectedViewer == null)
                return;
            eFigureType type = Status.Instance().SelectedViewer.GetDrawBox().CheckMeasurementType();

            if(_chapter == 1)
            {
                if (type == eFigureType.Line || type == eFigureType.Profile)
                {
                    _textControl.Visible = false;

                    DispalyPanel.Controls.Add(_setPixelSizeControl);
                    _setPixelSizeControl.Dock = DockStyle.Fill;
                    _setPixelSizeControl.Visible = true;

                    _chapter++;
                    AdjustButtonState();
                }
                else
                {
                    MessageBox.Show(LangResource.PixelCalibrationPage1);
                }
            }
            else if(_chapter == 2)
            {
                string textDistance = _setPixelSizeControl.GetDistance_mm();
                if (textDistance == "")
                {
                    MessageBox.Show(LangResource.LengthError);
                    return;
                }
                double distance_mm = Convert.ToDouble(textDistance);
                if (distance_mm <= 0)
                {
                    MessageBox.Show(LangResource.LengthError);
                    return;
                }
                if(type == eFigureType.Line)
                {
                    LineFigure selectedLine = Status.Instance().SelectedViewer.GetDrawBox().GetSelectedLine();
                    PointF orgStartPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(selectedLine.StartPoint);
                    PointF orgEndPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(selectedLine.EndPoint);
                    Status.Instance().SetPitch(orgStartPoint, orgEndPoint, distance_mm);

                    string message = (eMeasurementType.Fov_Calibration).ToString() + "Start : " + orgStartPoint.ToString()
                        + " End : " + orgEndPoint.ToString() + " distance :" + textDistance + "__Line";

                    Status.Instance().LogManager.AddLogMessage("Measure Setting", message);
                }
                else if(type == eFigureType.Profile)
                {
                    ProfileFigure selectedProfile = Status.Instance().SelectedViewer.GetDrawBox().GetSelectedProfile();

                    if (selectedProfile == null)
                        return;

                    tProfileResult result = (tProfileResult)selectedProfile.GetResult();
                    PointF orgStartPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(result.Mark1Point);
                    PointF orgEndPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(result.Mark2Point);
                    Status.Instance().SetPitch(orgStartPoint, orgEndPoint, distance_mm);

                    string message = (eMeasurementType.Fov_Calibration).ToString() + "Start : " + orgStartPoint.ToString()
                            + " End : " + orgEndPoint.ToString() + " distance :" + textDistance + "__Profile";

                    Status.Instance().LogManager.AddLogMessage("Measure Setting", message);
                }
                else
                {
                    MessageBox.Show(LangResource.PixelCalibrationPage1);
                    return;
                }
                Status.Instance().MeasureMentType = eMeasurementType.Pixel_Calibration;
                Status.Instance().SelectedViewer.GetDrawBox().ReUpdate();

                this.Close();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if(_chapter == 2)
            {
                _setPixelSizeControl.Visible = false;

                DispalyPanel.Controls.Add(_textControl);
                _textControl.SetMessage(LangResource.PixelCalibrationPage1 + "\n" + LangResource.PixelCalibrationPage1_1);
                _textControl.Dock = DockStyle.Fill;
                _textControl.Visible = true;
                _chapter--;
                AdjustButtonState();
            }
        }

        private void AdjustButtonState()
        {
            if (_chapter == 1)
            {
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
                btnNext.Text = LangResource.Next;
            }
            else if(_chapter == 2)
            {
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
                btnNext.Text = LangResource.Apply;
            }
        }

        private void PixelCalibrationPage1Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (FormCloseingDelegate != null)
                FormCloseingDelegate();
        }
    }
}
