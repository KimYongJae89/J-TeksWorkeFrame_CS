using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XManager.Controls;
using XManager.Util;
using XManager.Utill;

namespace XManager
{
    public partial class MainForm : Form
    {
        private ButtonPanel _buttonControl;
        private DrawBox _drawingControl;
        public MainForm()
        {
            //this.MinimumSize = new System.Drawing.Size(1300, 1000);
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // add button control
            _buttonControl = new ButtonPanel();
            this.pnlButton.Controls.Add(_buttonControl);

            _buttonControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            _buttonControl.Dock = System.Windows.Forms.DockStyle.Fill;
            _buttonControl.TabStop = false;

            // add drawingcontol control
            _drawingControl = new DrawBox();
            this.pnlDrawing.Controls.Add(_drawingControl);

            _drawingControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            _drawingControl.Dock = System.Windows.Forms.DockStyle.Fill;
            _drawingControl.TabStop = false;
            _drawingControl.ParentFormResizeDelegate += FormResize;
            _drawingControl.PanelSelectedUpdateDele = PanelSelectedUpdate;
            _drawingControl.FitToScreen();
            CStatus.Instance().SetDrawBox(_drawingControl);
            CStatus.Instance().GetDrawBox().ImageManager.ImageProcessingList.AddRange(CStatus.Instance().Settings.TempImageProcessingList);
        }

        private void PanelSelectedUpdate()
        {
            eDrawType type = CStatus.Instance().GetDrawBox().TrackerManager.DrawType;
            _buttonControl.PanelUpdate(type);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to exit the program?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    //CStatus.Instance().CameraManager.Close();

                    this.Cursor = Cursors.Default;
                    CStatus.Instance().ErrorLogManager.WriteErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "Close");
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception err)
            {
                string function = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(err.Message + "-" + function);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //buttonPanel.Focus();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.ShiftKey ||
            //    e.KeyCode == Keys.Shift)
            //    this.cameraDawingPannel.IsShiftKeyPressed = true;
            //if (e.KeyCode == Keys.Delete)
            //    this.cameraDawingPannel.DeleteSelectedRulerOrProtractor();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.ShiftKey ||
            //    e.KeyCode == Keys.Shift)
            //    this.cameraDawingPannel.IsShiftKeyPressed = false;
        }

        private void FormResize(int width, int height, int statusBarHeight)
        {
            int margin = 2;
            int clientWidth = this.ClientSize.Width;
            int clientHeight = this.ClientSize.Height;
            if (clientWidth <= width || clientHeight <= height)
            {
                this.ClientSize = new Size(width + margin, height + margin);
            }
            else
            {
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (this._drawingControl.IsActive)
                return;

            this._drawingControl.IsActive = true;
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            this._drawingControl.IsActive = false;
        }
    }
}
