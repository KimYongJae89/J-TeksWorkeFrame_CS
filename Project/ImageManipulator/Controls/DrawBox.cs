using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using ImageManipulator.Util;
using ImageManipulator.Data;

namespace ImageManipulator.Controls
{
    public partial class DrawBox : UserControl
    {
        private Tracker _tracker;
        public Action<int, int, int> ParentFormResizeDelegate;
        public Action<PointF> CurrentPointDelegate;
        public Action<RectangleF> CropImageDelegate;
        public Action DrawTypeResetDelegate;

        public bool EnableGirdView = false;

        private int _horizontalScrollValue = 0;
        private int _verticalScrollValue = 0;

        public bool IsActive = false;
        private Point _startingPanningPt = Point.Empty;
        private Point _movingPanningPt = Point.Empty;
        private Point _endPanningPt = Point.Empty;

        private Point _startingDrawPt = Point.Empty;
        private Point _movingDrawPt = Point.Empty;
        private Point _endDrawPt = Point.Empty;

        private Point _panningPt = Point.Empty;

        private Point _startingRectanglePoint = Point.Empty;
        private Point endRectanglePoint = Point.Empty;
        private Point movingRectanglePoint = Point.Empty;
        private Rectangle zoomDisplayRect;
        private bool _isPrevEventMouseDown = false;

        private Point _panningOffset = Point.Empty;
        private bool _panning = false;

        private bool _enablePanning = false;
        public bool EnablePanning
        {
            get { return _enablePanning; }
            set { _enablePanning = value; }
        }

        public int Mark1Value = 0;
        public int Mark2Value = 0;
        public eDrawType DrawType
        {
            get { return _tracker.DrawType; }
            set { _tracker.DrawType = value; }
        }

        public eModeType ModeType
        {
            get { return _tracker.ModeType; }
            set { _tracker.ModeType = value; }
        }
        private Bitmap image;
        public Bitmap Image
        {
            get { return image; }
            set
            {
                image = value;
                pictureBox.Image = image;
            }
        }

        private float zoomScale = 1;
        public float ZoomScale
        {
            get { return zoomScale; }
            set { zoomScale = value; }
        }
       
        public DrawBox()
        {
            try
            {
                InitializeComponent();

                this.pictureBox.TabIndex = 8;
                this.pictureBox.TabStop = false;
                this._tracker = new Tracker(pictureBox);
                this._tracker.CropImageDelegate += CropImage;
                this.ViewPanel.MouseWheel += ViewPanel_MouseWheel;
                this.ViewPanel.PreviewKeyDown += KeyEvent;

            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        private void KeyEvent(object sender, PreviewKeyDownEventArgs e)
        {
            if (pictureBox.Image == null)
                return;
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    {
                        bool isSeletedFigure = Status.Instance().SelectedViewer.GetDrawBox().GetSeletedFigure();

                        if (isSeletedFigure == false)
                            return;
                        if (MessageBox.Show("Do you want Delete?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            _tracker.DeleteSelected();

                            Status.Instance().UpdateRoiToRoiForm(eFormUpdate.Delete);
                            Status.Instance().UpdateProfileToForm(eFormUpdate.Delete);
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        Status.Instance().GetPrevFileOpen();
                        break;
                    }
                case Keys.Right:
                    {
                        Status.Instance().GetNextFileOpen();
                        break;
                    }
            }
        }

        private void ViewPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            zoomScale += (((float)e.Delta) / 1000);
            _tracker.ClearFigureSelected();
            UpdateZoom();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (pictureBox.Image == null)
                    return;

                if(EnableGirdView)
                    DrawGridLine(e.Graphics);

                if (_panning)
                {
                    Panning();
                }

                if (_tracker != null)
                {
                    if (!_tracker.IsSeclected() && IsActive)
                    {
                        _tracker.DrawTempFigure(e.Graphics);
                    }
                    _tracker.Draw(e.Graphics);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        private void DrawGridLine(Graphics g)
        {
            float displayImageWidth = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width;
            float displayImageHeight = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height;

            float widthScale = pictureBox.Width / displayImageWidth;
            float heightScale = pictureBox.Height / displayImageHeight;

            int widthGridCount = (int)(pictureBox.Width / widthScale);
            int heightGirdCount = (int)(pictureBox.Height / heightScale);

            Pen whitePen = new Pen(Color.White);
            Pen blackPen = new Pen(Color.Black);
            blackPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            blackPen.DashPattern = new float[] { 2, 2 };

            //Width
            for (int i = 0; i <= widthGridCount; i++)
            {
                float posX = i * widthScale;
                if (posX >= pictureBox.Width)
                {
                    posX = pictureBox.Width - 1;
                }
                PointF point1 = new PointF(posX, 0);
                PointF point2 = new PointF(posX, pictureBox.Height -1);
                g.DrawLine(whitePen, point1, point2);
                g.DrawLine(blackPen, point1, point2);
            }
            //Height
            for (int i = 0; i <= heightGirdCount; i++)
            {
                float posY = i * heightScale;
                if(posY >= pictureBox.Height)
                {
                    posY = pictureBox.Height - 1;
                }
                PointF point1 = new PointF(0, posY);
                PointF point2 = new PointF(pictureBox.Width - 1, posY);
                g.DrawLine(whitePen, point1, point2);
                g.DrawLine(blackPen, point1, point2);
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                this.Focus();
                if (pictureBox.Image == null)
                    return;
                if (e.Button != System.Windows.Forms.MouseButtons.Left)
                    return;
                this.ViewPanel.Focus();
                this._isPrevEventMouseDown = true;
                _startingPanningPt = this.ViewPanel.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
                Point pt = this.pictureBox.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
     
                if (pt.X > pictureBox.Width - 1)
                    pt.X = pictureBox.Width - 1;
                if (pt.X < 0)
                    pt.X = 0;
                if (pt.Y > pictureBox.Height - 1)
                    pt.Y = pictureBox.Height - 1;
                if (pt.Y < 0)
                    pt.Y = 0;

                if (EnablePanning)
                {
                    _panning = true;
                }
                else
                {
                    _tracker.SetCursorType(pt);
                    if (!_tracker.IsSeclected())
                        _tracker.TempFigureMouseDown(pt);

                    _tracker.MouseDown(pt);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!this.IsActive)
                    return;
                if (pictureBox.Image == null || IsActive == false)
                    return;

                this._isPrevEventMouseDown = false;
                Point pt = this.pictureBox.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
                if (pt.X > pictureBox.Width - 1)
                    pt.X = pictureBox.Width - 1;
                if (pt.X < 0)
                    pt.X = 0;
                if (pt.Y > pictureBox.Height - 1)
                    pt.Y = pictureBox.Height - 1;
                if (pt.Y < 0)
                    pt.Y = 0;

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (!EnablePanning)
                    {
                        if (!_tracker.IsSeclected())
                        {
                            _tracker.TempFigureMouseMove(pt);
                        }
                        _tracker.MouseMove(pt);
                    }
                    else
                    {
                        pictureBox.Invalidate();
                    }
                }
                else
                {
                    if (_tracker.IsSeclected())
                    {
                        _tracker.SetCursorType(pt);
                    }
                    else
                    {
                    }
                }
                if (CurrentPointDelegate != null)
                {
                    CurrentPointDelegate(CalcOrgPoint(pt));
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        private PointF CalcOrgPoint(PointF point)
        {
            float orgWidth = image.Width;
            float orgHeight = image.Height;
            float nowWidth = GetPictureBoxSize().Width;
            float nowHeight = GetPictureBoxSize().Height;

            float x = (float)point.X * orgWidth / nowWidth;
            float y = (float)point .Y* orgHeight / nowHeight;

            return new PointF(x, y);
        }

        private void CalculateZoomScale()
        {
            try
            {
                zoomDisplayRect = new Rectangle(0, 0, Width, Height);

                float zoomScaleWidth = Width / ((float)image.Width);
                float zoomScaleHeight = Height / ((float)image.Height);

                zoomScale = Math.Min(zoomScaleWidth, zoomScaleHeight);

                UpdateZoom();
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            Point pt = this.pictureBox.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));

            if (pictureBox.Image == null)
                return;
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;

            if (pt.X < 0) pt.X = 0;
            if (pt.X >= pictureBox.Width)
                pt.X = pictureBox.Width - 1;

            if (pt.Y < 0) pt.Y = 0;
            if (pt.Y >= pictureBox.Height)
                pt.Y = pictureBox.Height - 1;

            if (this._isPrevEventMouseDown)
            {
                _tracker.CheckPointInFigure(pt);
            }
            else
            {
                _panning = false;

                if (!_tracker.IsSeclected() && ModeType != eModeType.Crop)
                    _tracker.TempFigureMouseUp(pt);
            }
            _tracker.MouseUp(pt);
        }

        private void UpdateZoom()
        {
            try
            {
                int newWidth = (int)(image.Width * zoomScale);
                int newHeight = (int)(image.Height * zoomScale);
                if (newWidth < 50 || newHeight < 50)
                {
                    newWidth = newHeight = 50;
                }

                ImageCoordTransformer coordTransformer = new ImageCoordTransformer();
                coordTransformer.OrgWidth = pictureBox.Width;
                coordTransformer.OrgHeight = pictureBox.Height;
                coordTransformer.NewWidth = newWidth;
                coordTransformer.NewHeight = newHeight;

                this._tracker.Scale(coordTransformer);
                pictureBox.Width = newWidth;
                pictureBox.Height = newHeight;
                PictureBoxMoveToCenterPictureBox();
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        private void UpdateFigureList(int newWidth, int newHeight)
        {
            try
            {
                ImageCoordTransformer coordTransformer = new ImageCoordTransformer();
                coordTransformer.OrgWidth = pictureBox.Width;
                coordTransformer.OrgHeight = pictureBox.Height;
                coordTransformer.NewWidth = newWidth;
                coordTransformer.NewHeight = newHeight;

                this._tracker.Scale(coordTransformer);
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        public void UpdateZoom(float scale)
        {
            try
            {
                _tracker.ClearFigureSelected();
                zoomScale += scale;
                int newWidth = (int)(image.Width * zoomScale);
                int newHeight = (int)(image.Height * zoomScale);
                if (newWidth < 50 || newHeight < 50)
                {
                    newWidth = newHeight = 50;
                }

                ImageCoordTransformer coordTransformer = new ImageCoordTransformer();
                coordTransformer.OrgWidth = pictureBox.Width;
                coordTransformer.OrgHeight = pictureBox.Height;
                coordTransformer.NewWidth = newWidth;
                coordTransformer.NewHeight = newHeight;

                this._tracker.Scale(coordTransformer);
                pictureBox.Width = newWidth;
                pictureBox.Height = newHeight;
                PictureBoxMoveToCenterPictureBox();
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        public void LoadFile(string path)
        {
            Bitmap bmp = new Bitmap(path);
            this.image = bmp;
            pictureBox.Size = new Size(bmp.Width, bmp.Height);
            pictureBox.Image = image;
            int margin = 2;// 스크롤 때문에 2pixel 차이로 인해 스크롤이 발생한다.
            pictureBox.Left = 0;
            pictureBox.Top = 0;
            if (ParentFormResizeDelegate != null)
                ParentFormResizeDelegate(bmp.Width + margin, bmp.Height + margin, 0);
        }

        public void LoadFile(Bitmap image)
        {
            if (image == null)
                return;

            this.image = image;
            pictureBox.Size = new Size(image.Width, image.Height);
            pictureBox.Image = image;
            pictureBox.Left = 0;
            pictureBox.Top = 0;
            if (ViewPanel.Size.Width < image.Width || ViewPanel.Size.Height <image.Height)
            {
                if (ParentFormResizeDelegate != null)
                    ParentFormResizeDelegate(image.Width, image.Height, 0);
            }
            else
            {
                PictureBoxMoveToCenterPictureBox();
            }
        }

        public void FitToScreen()
        {
            if (pictureBox.Image == null)
                return;

            int horizontalScrollValue = this.ViewPanel.HorizontalScroll.Value;
            int verticalScrollValue = this.ViewPanel.VerticalScroll.Value;

            pictureBox.Left = 0 - horizontalScrollValue;
            pictureBox.Top = 0 - verticalScrollValue;

            SizeF sizef = new SizeF(image.Width, image.Height);

            float fScale = Math.Min(this.ViewPanel.Width / sizef.Width, this.ViewPanel.Height / sizef.Height);

            sizef.Width *= fScale;
            sizef.Height *= fScale;

            ZoomScale = sizef.Width / (float)image.Width;
            UpdateFigureList((int)sizef.Width, (int)sizef.Height);

            pictureBox.Width = (int)sizef.Width;
            pictureBox.Height = (int)sizef.Height;

            pictureBox.Left = 0 - horizontalScrollValue;
            pictureBox.Top = 0 - verticalScrollValue;
          
            PictureBoxMoveToCenterPictureBox();
        }

        public void OneVerseOneFitZoom()
        {
            try
            {
                if (pictureBox.Image == null)
                    return;

                zoomScale = 1;
                UpdateFigureList(this.image.Width, this.image.Height);
                pictureBox.Width = this.image.Width;
                pictureBox.Height = this.image.Height;

                PictureBoxMoveToCenterPictureBox();
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }
        public void PictureBoxMoveToCenterPictureBox()
        {
            try
            {
                if (pictureBox.Image == null)
                    return;

                if (this.ViewPanel.Width > pictureBox.Width)
                    pictureBox.Left = (this.ViewPanel.Width / 2) - (this.pictureBox.Width / 2);
                else
                {
                    int left = 0;
                    int value = -this.ViewPanel.HorizontalScroll.Value;
                    if (value < 0)
                        left += value;
                    pictureBox.Left = left;
                }
                if (this.ViewPanel.Height > pictureBox.Height)
                    pictureBox.Top = (this.ViewPanel.Height / 2) - (this.pictureBox.Height / 2);
                else
                {
                    int top = 0;
                    int value = -this.ViewPanel.VerticalScroll.Value;
                    if (value < 0)
                        top += value;
                    pictureBox.Top = top;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        public void Panning()
        {
            this._movingPanningPt = this.ViewPanel.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));

            pictureBox.Left = pictureBox.Left - (int)(this._startingPanningPt.X - this._movingPanningPt.X);
            pictureBox.Top = pictureBox.Top - (int)(this._startingPanningPt.Y - this._movingPanningPt.Y);

            _endPanningPt = _movingPanningPt;
            this._startingPanningPt = _endPanningPt;
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            this.ModeType = eModeType.None;
            this.DrawType = eDrawType.None;
            FigureSelectedClear();
            if (DrawTypeResetDelegate != null)
                DrawTypeResetDelegate();
        }

        public void FigureSelectedClear()
        {
            _tracker.ClearFigureSelected();
        }

        public void Rotation(eImageTransform type)
        {
            PointF centerPoint = new PointF((float)this.pictureBox.Width / 2.0F, (float)this.pictureBox.Height / 2.0F);
            switch (type)
            {
                case eImageTransform.CW:
                    _tracker.Transform(eImageTransform.CW, centerPoint, 90);
                    // 이미지 변환
                    break;
                case eImageTransform.CCW:
                    _tracker.Transform(eImageTransform.CCW, centerPoint, -90);
                    break;
                case eImageTransform.FlipX:
                    _tracker.Transform(eImageTransform.FlipX, centerPoint);
                    break;
                case eImageTransform.FlipY:
                    _tracker.Transform(eImageTransform.FlipY, centerPoint);
                    break;
                default:
                    break;
            }
        }
        
        public void CropImage(RectangleF rect)
        {
            if (CropImageDelegate != null)
            {
                Status.Instance().LogManager.AddLogMessage("Crop Image", rect.ToString());
                CropImageDelegate(rect);
            }
        }

        public List<Figure> GetFigureGroup()
        {
            return _tracker.GetFigureGroup();
        }

        public List<RectangleFigure> GetRoiAllList()
        {
            return _tracker.GetRoiAllList();
        }

        public RectangleFigure GetSelectedRoi()
        {
            return _tracker.GetSelectedRoi();
        }

        public bool GetSeletedFigure()
        {
            return _tracker.GetSeletedFigure();
        }
        public List<ProfileFigure> GetProfileAllList()
        {
            return _tracker.GetProfileAllList();
        }

        public ProfileFigure GetSelectedProfile()
        {
            return _tracker.GetSelectedProfile();
        }

        public LineFigure GetSelectedLine()
        {
            return _tracker.GetSelectedLine();
        }

        public void RoiSelectUpdate(int id)
        {
            _tracker.RoiSelectUpdate(id);
        }

        public void ProfileSelectUpdate(int id)
        {
            _tracker.ProfileSelectUpdate(id);
        }

        public Size GetPictureBoxSize()
        {
            return pictureBox.Size;
        }

        public RectangleF GetRoi(int id)
        {
            return _tracker.GetRoi(id);
        }

        public int GetRoiCount()
        {
            return _tracker.GetRoiCount();
        }

        public int GetProfileCount()
        {
            return _tracker.GetProfileCount();
        }

        public int SelectedFigureCount()
        {
            return _tracker.SelectedFigureCount();
        }

        public void ReUpdate()
        {
            _tracker.Invaldidate();
        }

        private void pictureBox_LocationChanged(object sender, EventArgs e)
        {
        }
        int prevHorizontalValue = 0;
        int prevVerticalValue = 0;
        private void pictureBox_Move(object sender, EventArgs e)
        {
            if (prevHorizontalValue != 0 && this.ViewPanel.HorizontalScroll.Value == 0)
                return;
            if (prevVerticalValue != 0 && this.ViewPanel.VerticalScroll.Value == 0)
            {
                this._horizontalScrollValue = this.ViewPanel.HorizontalScroll.Value;
                return;
            }
            this._horizontalScrollValue = this.ViewPanel.HorizontalScroll.Value;
            this._verticalScrollValue = this.ViewPanel.VerticalScroll.Value;

            prevHorizontalValue = this._horizontalScrollValue;
            prevVerticalValue = this._verticalScrollValue;
        }

        private void ViewPanel_Scroll(object sender, ScrollEventArgs e)
        {
            this.ViewPanel.Focus();
        }

        public int PictureBoxWidth()
        {
            return this.pictureBox.Width;
        }

        public int PictureBoxHeight()
        {
            return this.pictureBox.Height;
        }

        public eFigureType CheckMeasurementType()
        {
            return _tracker.CheckMeasurementType();
        }
    }
}
