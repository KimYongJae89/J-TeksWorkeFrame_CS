using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;
using XManager.Utill;
using System.Drawing.Drawing2D;
using System.Reflection;
using static XManager.CStatus;
using System.Net.Sockets;
using KiyLib.Communication;
using XManager.Forms;
using KiyLib.DIP;
using XManager.FigureData;
using XManager.Util;

namespace XManager.Controls
{
    public partial class DrawBox : UserControl
    {
        #region 새로 추가된 내용
        public Action PanelSelectedUpdateDele;
        public Action<int, int, int> ParentFormResizeDelegate;
        public Action<RectangleF> CropImageDelegate;

        public EmguImageWrapper ImageManager = new EmguImageWrapper();
        public Tracker TrackerManager;
        private bool _isPrevEventMouseDown = false;
        public bool IsActive = false;

        private Point _startingPanningPt = Point.Empty;
        private Point _movingPanningPt = Point.Empty;
        private Point _endPanningPt = Point.Empty;

        private byte[] _orgBufferArr;
        #endregion


        private int _selectRectWidth = 6;
        private double _widthRate = 1;
        private double _heightRate = 1;
        private Image _orgImage = null; // ThumnailDisplay Image
        private Image _ZoomImage = null;
        private PointF _zoomPoint;
        private PointF _CenterWidthPoint;
        private int _orgThumbnailCenter;
        private int _orgThumbnailWidth;
        private ColorPalette remappedPalette;

        private byte[] _lutResultArray;
        private byte[] _org16bitRawData;
        private bool drawCross = false;
        private Shape _shape = Shape.NONE;
        private enum Shape
        {
            NONE,
            RULER,
            PROTRACTOR,
            REGION
        }
        private int _rulerNumLimit = 100;

        #region 직선 관련
        private PointF _drawLineStartPt;
        private PointF _drawLineEndPt;
        private PointF _lineMoveStartPt;
        private RectangleF _startRect;
        private RectangleF _endRect;
        private RectangleF _lineProfileMk1Rect;
        private RectangleF _lineProfileMk2Rect;
        #endregion

        #region 각도기 관련
        private PointF _drawProtractorStartPt;
        //private RectangleF _protractorRect;
        private enum QUADRANT
        {
            NONE,
            QUADRANT1,
            QUADRANT2,
            QUADRANT3,
            QUADRANT4,
        }
        #endregion

        public enum BufferType
        {
            ORG,
            ZOOM,
            RULER,
            PROTRACTOR,
        };

        private List<LineInfo> _linesInfo = new List<LineInfo>();
        public class LineInfo
        {
            public PointF startPt;
            public PointF endPt;
            public RectangleF startRect;
            public RectangleF endRect;
            public bool isSelected;
        }

        private List<ProtractorInfo> _protractorInfo = new List<ProtractorInfo>();
        public class ProtractorInfo : ICloneable
        {
            public PointF centerPt;
            public PointF p1;
            public PointF p2;
            public PointF calcP1;
            public PointF calcP2;
            public RectangleF p1Rect;
            public RectangleF p2Rect;
            public RectangleF centerRect;
            public RectangleF gridRect;
            public bool isSelected;
            public EDIT_MODE editMode;

            public object Clone()
            {
                var newProtInfo = new ProtractorInfo();

                newProtInfo.centerPt = this.centerPt;
                newProtInfo.p1 = this.p1;
                newProtInfo.p2 = this.p2;
                newProtInfo.calcP1 = this.calcP1;
                newProtInfo.calcP2 = this.calcP2;
                newProtInfo.p1Rect = this.p1Rect;
                newProtInfo.p2Rect = this.p2Rect;
                newProtInfo.centerRect = this.centerRect;
                newProtInfo.gridRect = this.gridRect;
                newProtInfo.isSelected = this.isSelected;
                newProtInfo.editMode = this.editMode;

                return newProtInfo;
            }
        }

        private EDIT_MODE _Edit_Mode = EDIT_MODE.NONE;
        public enum EDIT_MODE
        {
            NONE,
            START_RECT, // ruler
            END_RECT,   // ruler
            MOVE,       // ruler, Protractor
            GRID,
            P1_LINE_RECT1,
            P2_LINE_RECT2,
            P1_RECT,    // Protractor
            P2_RECT,    // Protractor

            LT_REGION,
            RT_REGION,
            LB_REGION,
            RB_REGION
        }

        private MOUSE_MODE _mouse_Mode = MOUSE_MODE.NONE;
        private enum MOUSE_MODE
        {
            NONE,
            DOWN,
            MOVE,
            UP,
        }

        public delegate void ThumbnailGridviewUpdateDelegate(Image img, string rawDataPath);
        public event ThumbnailGridviewUpdateDelegate ThumbnailGridviewUpdateHandler;

        private float zoomScale = 1;
        public float ZoomScale
        {
            get { return zoomScale; }
            set { zoomScale = value; }
        }

        public Bitmap Image
        {
            get { return pbx.Image as Bitmap; }
            set
            {
                pbx.Image = value;
                orgloadedImage = null;

                if (value != null)
                    orgImgSize = value.Size;

                //if (zoomScale == -1)
                //    CalculateZoomScale();
                //else
                //    UpdateZoom();
            }
        }

        private int _maxValue = 16384; // 2의 14승
        public int MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        private int _minValue = 0;
        public int MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }

        private int _rawWidth = 0;
        public int RawWidth
        {
            get { return _rawWidth; }
            set { _rawWidth = value; }
        }

        private int _rawHeight = 0;
        public int RawHeight
        {
            get { return _rawHeight; }
            set { _rawHeight = value; }
        }

        private bool _isThumbnailPanel = false;
        public bool IsThumbnailPanel
        {
            get { return _isThumbnailPanel; }
            set { _isThumbnailPanel = value; }
        }

        //Region
        private RectangleF region = new RectangleF();
        private bool regionSelected;


        //zoom, panning 변수
        private Point mouseDown;
        public bool IsShiftKeyPressed { get; set; }
        private Size orgImgSize;
        private float orgImgAspect; //원본 이미지의 가로, 세로 비율private float zoomFactor = 1;
        private float zoomFactor = 1;
        private Bitmap orgloadedImage;  //이미지 로드시 여기에 원본저장
                                        // byte[] lastCallbackImagebuffer;

        private ToolStripItem pixelSizeSetToolStrip;

        public DrawBox()
        {
            InitializeComponent();
            TrackerManager = new Tracker(pbx);
            TrackerManager.CropImageDelegate += CropImage;
            CStatus.Instance().CameraManager.RegisterCallBack(DisplayUpdate);
            this.pnlDrawing.PreviewKeyDown += KeyEvent;
        }

        private void KeyEvent(object sender, PreviewKeyDownEventArgs e)
        {
            if (pbx.Image == null)
                return;
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    {
                        bool isSeletedFigure = CStatus.Instance().GetDrawBox().TrackerManager.GetSeletedFigure();

                        if (isSeletedFigure == false)
                            return;
                        if (MessageBox.Show("Do you want Delete?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            TrackerManager.DeleteSelected();

                            CStatus.Instance().UpdateRoiToRoiForm(eFormUpdate.Delete);
                            CStatus.Instance().UpdateProfileToForm(eFormUpdate.Delete);
                        }
                        break;
                    }
            }
        }

        public void FitToScreen()
        {
            if (pbx.Image == null)
                return;
            int horizontalScrollValue = this.pnlDrawing.HorizontalScroll.Value;
            int verticalScrollValue = this.pnlDrawing.VerticalScroll.Value;

            //pbx.Left = 0 - horizontalScrollValue;
            //pbx.Top = 0 - verticalScrollValue;

            SizeF sizef = new SizeF(ImageManager.DisPlayImage.Width, ImageManager.DisPlayImage.Height);

            float fScale = Math.Min(this.pnlDrawing.Width / sizef.Width, this.pnlDrawing.Height / sizef.Height);

            sizef.Width *= fScale;
            sizef.Height *= fScale;

            ZoomScale = sizef.Width / (float)ImageManager.DisPlayImage.Width;
            UpdateFigureList((int)sizef.Width, (int)sizef.Height);

            pbx.Width = (int)sizef.Width;
            pbx.Height = (int)sizef.Height;

            pbx.Left = 0 - horizontalScrollValue;
            pbx.Top = 0 - verticalScrollValue;

            PictureBoxMoveToCenter();
        }

        private void UpdateFigureList(int newWidth, int newHeight)
        {
            try
            {
                ImageCoordTransformer coordTransformer = new ImageCoordTransformer();
                coordTransformer.OrgWidth = pbx.Width;
                coordTransformer.OrgHeight = pbx.Height;
                coordTransformer.NewWidth = newWidth;
                coordTransformer.NewHeight = newHeight;

                this.TrackerManager.Scale(coordTransformer);
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        public void PictureBoxMoveToCenter()
        {
            if (pbx.Image == null)
                return;

            if (this.pnlDrawing.Width > pbx.Width)
                pbx.Left = (this.pnlDrawing.Width / 2) - (this.pbx.Width / 2);
            else
            {
                int left = 0;
                int value = -this.pnlDrawing.HorizontalScroll.Value;
                if (value < 0)
                    left += value;
                pbx.Left = left;
            }
            if (this.pnlDrawing.Height > pbx.Height)
                pbx.Top = (this.pnlDrawing.Height / 2) - (this.pbx.Height / 2);
            else
            {
                int top = 0;
                int value = -this.pnlDrawing.VerticalScroll.Value;
                if (value < 0)
                    top += value;
                pbx.Top = top;
            }
        }

        private void PictureBoxClear()
        {
            this.pbx.Image = null;
        }

        /// <summary>
        /// 라이브 화면 갱신
        /// </summary>
        /// <param name="buffer">Avg 계산된 raw Data(Wnd 적용X)</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="bit"></param>
        public unsafe void DisplayUpdate(int[] buffer, int width, int height, int bit)
        {
            try
            {
                if (bit == 8)
                {
                    JImage image = new JImage(width, height, KDepthType.Dt_8, buffer);
                    CStatus.Instance().GetDrawBox().ImageManager.SetOrgImage(image);
                    CStatus.Instance().GetDrawBox().Image = CStatus.Instance().GetDrawBox().ImageManager.CalcDisplayImage().ToBitmap();
                }
                else if(bit == 16)
                {
                    JImage image = new JImage(width, height, KDepthType.Dt_16, buffer);
                    CStatus.Instance().GetDrawBox().ImageManager.SetOrgImage(image);
                    CStatus.Instance().GetDrawBox().Image = CStatus.Instance().GetDrawBox().ImageManager.CalcDisplayImage().ToBitmap();
                }
            }
            catch (Exception err)
            {
                string function = MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(err.Message + "-" + function);
            }
        }

        private void BufferInitialize(ushort[] buffer, int width, int height, int bit)
        {
            int size = 0;
            if (bit == 8)
                size = width * height;
            else if (bit == 16)
                size = width * height * 2;
            else { }

            if (size != _orgBufferArr.Length)
            {
                _orgBufferArr = null;
                _orgBufferArr = new byte[size];
            }
            Array.Copy(_orgBufferArr, buffer, size);
        }
  
        private void CameraDawingPannel_Load(object sender, EventArgs e)
        {
            int widthHalf = pnlDrawing.Width / 2;
            int heightHalf = pnlDrawing.Height / 2;
            pbx.Location = new Point(widthHalf - heightHalf);
            pbx.Size = new Size(heightHalf * 2, heightHalf * 2);
        }

        private void CameraDawingPannel_Resize(object sender, EventArgs e)
        {
            //UserControl Resize
            int widthHalf = pnlDrawing.Width / 2;
            int heightHalf = pnlDrawing.Height / 2;
            //FitToScreen();
            PictureBoxMoveToCenter();
        }

        public void FigureRotation(eImageTransform type)
        {
            PointF centerPoint = new PointF((float)this.pbx.Width / 2.0F, (float)this.pbx.Height / 2.0F);
            switch (type)
            {
                case eImageTransform.CW:
                    TrackerManager.Transform(eImageTransform.CW, centerPoint, 90);
                    // 이미지 변환
                    break;
                case eImageTransform.CCW:
                    TrackerManager.Transform(eImageTransform.CCW, centerPoint, -90);
                    break;
                case eImageTransform.FlipX:
                    TrackerManager.Transform(eImageTransform.FlipX, centerPoint);
                    break;
                case eImageTransform.FlipY:
                    TrackerManager.Transform(eImageTransform.FlipY, centerPoint);
                    break;
                default:
                    break;
            }
        }

        private double LengthPointToPoint(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2));
        }

        private bool IsOnLine(PointF p1, PointF p2, PointF p, int width = 1)
        {
            var isOnLine = false;
            using (var path = new GraphicsPath())
            {
                using (var pen = new Pen(Brushes.Black, width))
                {
                    path.AddLine(p1, p2);
                    isOnLine = path.IsOutlineVisible(p, pen);
                }
            }

            return isOnLine;
        }

        private void ZoomDisplay(float x, float y)
        {
            try
            {
                int zoominWidth = 200;
                int zoominHeight = 200;
                PointF picturePt = new PointF();
                picturePt.X = x;
                picturePt.Y = y;
                this._zoomPoint.X = picturePt.X;
                this._zoomPoint.Y = picturePt.Y;
                PointF newPt = new PointF();

                newPt.X = picturePt.X / (float)this._widthRate;
                newPt.Y = picturePt.Y / (float)this._heightRate;
                RectangleF RoiRect = new RectangleF(newPt.X - 100.0F, newPt.Y - 100.0F, zoominWidth, zoominHeight);

                Size size = new Size(zoominWidth, zoominHeight);
                Bitmap img = (Bitmap)this._orgImage;
                Bitmap CropImage = new Bitmap(zoominWidth, zoominHeight);

                for (int i = 0; i < zoominWidth; i++)
                {
                    for (int j = 0; j < zoominHeight; j++)
                    {
                        int row = (int)(i + RoiRect.X);
                        int col = (int)(j + RoiRect.Y);
                        if (row < 0 || col < 0 || row >= Convert.ToInt32(CStatus.Instance().CameraRows) || col >= Convert.ToInt32(CStatus.Instance().CameraColumns))
                            CropImage.SetPixel(i, j, Color.Black);
                        else
                            CropImage.SetPixel(i, j, img.GetPixel(row, col));
                    }
                }

                Size newSize = new Size(400, 400);
                this._ZoomImage = new Bitmap(CropImage, newSize);
            }
            catch (Exception err)
            {
                string function = MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(err.Message + "-" + function);
            }
        }

        private void CameraDawingPannel_Move(object sender, EventArgs e)
        {
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            zoomFactor = pbx.Size.Width / (float)orgImgSize.Width;

            //기존 코드
            /*if (this._isThumbnailPanel == true)
             {
                 // panel 
                  if (CStatus.Instance().ResizeInfo == null)
                      return;

                  DrawingPatientInfoinThumbnailPanel();
                  DrawSelectCaptureImage(CStatus.Instance().ResizeInfo);
             }*/
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (pbx.Image == null)
                return;

            //if (EnableGirdView)
            //    DrawGridLine(e.Graphics);

            //if (_panning)
            //{
            //    Panning();
            //}

            if (TrackerManager != null)
            {
                if (!TrackerManager.IsSeclected() && IsActive)
                {
                    TrackerManager.DrawTempFigure(e.Graphics);
                }
                TrackerManager.Draw(e.Graphics);
            }
        }

        private void FitZoom()
        {
            Size frmSize = this.pnlDrawing.Size;
            pbx.Size = orgImgSize;
            float fitFactor = 1;

            if (orgImgSize.Width > orgImgSize.Height)
            {
                fitFactor = frmSize.Width / (float)orgImgSize.Width;
                pbx.Scale(new SizeF(fitFactor, fitFactor));
                pbx.Location = new Point(0, (frmSize.Height / 2 - pbx.Height / 2));
            }
            else
            {
                fitFactor = frmSize.Height / (float)orgImgSize.Height;
                pbx.Scale(new SizeF(fitFactor, fitFactor));
                pbx.Location = new Point((frmSize.Width / 2 - pbx.Width / 2), 0);
            }

            zoomFactor = pbx.Size.Width / (float)orgImgSize.Width;

            pbx.Update();
        }

        private void OriginSizeZoom()
        {
            //if (orgloadedImage == null)
            //   return;

            Size CamDrawPnlSize = this.pnlDrawing.Size;
            pbx.Size = orgImgSize;
            pbx.Location = new Point((CamDrawPnlSize.Width / 2 - pbx.Width / 2),
                                     (CamDrawPnlSize.Height / 2 - pbx.Height / 2));

            pbx.Update();
        }

        #region Pictubox Mouse Events
        private void pbx_MouseEnter(object sender, EventArgs e)
        {
            //pbx.Focus();
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            int newWidth = pbx.Width,
                newHeight = pbx.Height,
                newX = pbx.Location.X,
                newY = pbx.Location.Y;

            if (e.Delta > 0)    //+
            {
                if (zoomFactor >= 5)
                    return;

                zoomFactor += 0.1f;
            }

            else if (e.Delta < 0)   //-
            {
                if (zoomFactor <= 0.1f)
                    return;

                zoomFactor -= 0.1f;
            }

            newWidth = (int)Math.Round((zoomFactor * (float)orgImgSize.Width));
            newHeight = (int)Math.Round((zoomFactor * (float)orgImgSize.Height));

            pbx.Size = new Size(newWidth, newHeight);
            pbx.Location = new Point((Size.Width / 2) - (newWidth / 2),
                                     (Size.Height / 2) - (newHeight / 2));

            pbx.Update();
        }

        private void pbx_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                this.Focus();
                if (pbx.Image == null)
                    return;

                if (CStatus.Instance().IsAcqusitionExecute)
                    return;

                if (e.Button != System.Windows.Forms.MouseButtons.Left)
                    return;
                this.pnlDrawing.Focus();
                this._isPrevEventMouseDown = true;
                _startingPanningPt = this.pnlDrawing.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
                Point pt = this.pbx.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));

                if (pt.X > pbx.Width - 1)
                    pt.X = pbx.Width - 1;
                if (pt.X < 0)
                    pt.X = 0;
                if (pt.Y > pbx.Height - 1)
                    pt.Y = pbx.Height - 1;
                if (pt.Y < 0)
                    pt.Y = 0;

                //if (EnablePanning)
                //{
                //    _panning = true;
                //}
                //else
                {
                    TrackerManager.SetCursorType(pt);
                    if (!TrackerManager.IsSeclected())
                        TrackerManager.TempFigureMouseDown(pt);

                    TrackerManager.MouseDown(pt);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        private void pbx_MouseUp(object sender, MouseEventArgs e)
        {
            Point pt = this.pbx.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));

            if (pbx.Image == null)
                return;
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;
            if (CStatus.Instance().IsAcqusitionExecute)
                return;

            if (pt.X < 0) pt.X = 0;
            if (pt.X >= pbx.Width)
                pt.X = pbx.Width - 1;

            if (pt.Y < 0) pt.Y = 0;
            if (pt.Y >= pbx.Height)
                pt.Y = pbx.Height - 1;

            if (this._isPrevEventMouseDown)
            {
                TrackerManager.CheckPointInFigure(pt);
            }
            else
            {
                //_panning = false;

                if (!TrackerManager.IsSeclected() && TrackerManager.ModeType != eModeType.Crop)
                    TrackerManager.TempFigureMouseUp(pt);
            }
            TrackerManager.MouseUp(pt);
        }

        private void pbx_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!this.IsActive)
                    return;
                if (pbx.Image == null || IsActive == false)
                    return;
                Point pt = new Point();
                if (!CStatus.Instance().IsAcqusitionExecute)
                {
                    this._isPrevEventMouseDown = false;
                    pt = this.pbx.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
                    if (pt.X > pbx.Width - 1)
                        pt.X = pbx.Width - 1;
                    if (pt.X < 0)
                        pt.X = 0;
                    if (pt.Y > pbx.Height - 1)
                        pt.Y = pbx.Height - 1;
                    if (pt.Y < 0)
                        pt.Y = 0;

                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        //if (!EnablePanning)
                        {
                            if (!TrackerManager.IsSeclected())
                            {
                                TrackerManager.TempFigureMouseMove(pt);
                            }
                            TrackerManager.MouseMove(pt);
                        }
                        //else
                        //{
                        //    pictureBox.Invalidate();
                        //}
                    }
                    else
                    {
                        if (TrackerManager.IsSeclected())
                        {
                            TrackerManager.SetCursorType(pt);
                        }
                        else
                        {
                        }
                    }
                }
                CurrentPointValue(CalcOrgPoint(pt));
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }
        #endregion

        #region MenuStrip
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FitZoom();
        }

        private void originSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OriginSizeZoom();
        }

        private void drawCrossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawCross = !drawCross;
        }

        private void SetRealPixelSize_Click(object sender, EventArgs e)
        {
            //foreach (Form openFrm in Application.OpenForms)
            //{
            //    if (openFrm.Name == "SetPixelSizeFrm")
            //    {
            //        openFrm.Activate();
            //        return;
            //    }
            //}

            //if (_selectedLine == null)
            //    return;

            //var pt1 = _selectedLine.startPt;
            //var pt2 = _selectedLine.endPt;

            //SetPixelSizeFrm frm = new SetPixelSizeFrm();
            //frm.ApplyClicked += SetPixelSizeFrmFrm_ApplyClicked;
            //frm.SetPixelSize(pt1, pt2);
            //frm.Show();
        }

        private void SetPixelSizeFrmFrm_ApplyClicked(object sender, EventArgs e)
        {
            //if (_selectedLine == null)
            //    return;

            //var pt1 = _selectedLine.startPt;
            //var pt2 = _selectedLine.endPt;

            //var frm = sender as SetPixelSizeFrm;
            //frm.SetPixelSize(pt1, pt2);

        }

        private void originRawImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //var img = CStatus.Instance().LatestImageArr;
                //if (img == null)
                //{
                //    MessageBox.Show("Image is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //string localIp = "127.0.0.1";
                //if (!iseeViewCommu.Connected)
                //    iseeViewCommu.Connect(localIp);

                //if (!iseeViewCommu.Connected)
                //{
                //    MessageBox.Show("ISee! connection failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //string curDirPath = Directory.GetCurrentDirectory();
                //string fileName = "16bitOrgRawImage.tif";
                //string rstPath = Path.Combine(curDirPath, fileName);

                //using (KTiff tiff = new KTiff(Image.Width, Image.Height))
                //{
                //    tiff.ImageArr = img;
                //    tiff.Save(rstPath);
                //}

                //iseeViewCommu.SendFileopenCmd(rstPath);
            }
            catch (SocketException err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var img = CStatus.Instance().LatestImageArrHistogramd;
                if (img == null)
                {
                    MessageBox.Show("Image is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string localIp = "127.0.0.1";
                //if (!iseeViewCommu.Connected)
                //    iseeViewCommu.Connect(localIp);

                //if (!iseeViewCommu.Connected)
                //{
                //    MessageBox.Show("ISee! connection failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                string curDirPath = Directory.GetCurrentDirectory();
                string fileName = "16bitHistogramImage.tif";
                string rstPath = Path.Combine(curDirPath, fileName);

                //using (KTiff tiff = new KTiff(Image.Width, Image.Height))
                //{
                //    tiff.ImageArr = img;
                //    tiff.Save(rstPath);
                //}

                //iseeViewCommu.SendFileopenCmd(rstPath);
            }
            catch (SocketException err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        public void SaveColorImage(string path)
        {
            try
            {
                this._orgImage = Image;

                float prevZoomFactor = zoomFactor;
                zoomFactor = 1;

                using (var tempBmp = Image.Clone(new Rectangle(0, 0, Image.Width, Image.Height), PixelFormat.Format24bppRgb))
                using (var backBufferBmp = new Bitmap(Image.Width, Image.Height, PixelFormat.Format24bppRgb))
                using (var backGraphics = Graphics.FromImage(backBufferBmp))
                using (var g = Graphics.FromImage(tempBmp))
                {
                    Rectangle pictureBoxRect = new Rectangle(0, 0, pbx.Image.Width, pbx.Image.Height);
                    Rectangle sourceRect = new Rectangle(0, 0, pbx.Image.Width, pbx.Image.Height);

                    _widthRate = pbx.DisplayRectangle.Width / (double)sourceRect.Width;
                    _heightRate = pbx.DisplayRectangle.Height / (double)sourceRect.Height;

                    backGraphics.DrawImage(pbx.Image, pictureBoxRect, sourceRect, GraphicsUnit.Pixel);

                    //자 그리기
                    //if (CStatus.Instance().IsRulerMode == true) // draw mode 
                    //{
                    //    if (_mouse_Mode == MOUSE_MODE.DOWN)
                    //        RulerThumbnailDisplayUpdate(backGraphics);
                    //    else if (_mouse_Mode == MOUSE_MODE.UP)
                    //        DrawAndNewAddLine(backGraphics);
                    //    else
                    //        RulerThumbnailDisplayUpdate(backGraphics);
                    //}

                    //if (CStatus.Instance().IsRulerMode == false)        // && this._shape == Shape.RULER)// edit mode
                    //    DrawLineUpdate(backGraphics);

                    //if (CStatus.Instance().IsProtractorMode == true)    // draw mode 
                    //    DrawProtractorUpdate(backGraphics);

                    //if (CStatus.Instance().IsProtractorMode == false)   // && this._shape == Shape.PROTRACTOR)
                    //    DrawProtractorUpdate(backGraphics);

                    //if (CStatus.Instance().IsRulerMode == false &&
                    //    CStatus.Instance().IsProtractorMode == false &&
                    //    _shape == Shape.NONE)
                    //{
                    //    DrawLineUpdate(backGraphics);
                    //    DrawProtractorUpdate(backGraphics);
                    //}

                    g.DrawImage(backBufferBmp, 0, 0);
                    tempBmp.Save(path);
                }

                zoomFactor = prevZoomFactor;
            }
            catch (Exception err)
            {
                string function = MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(err.Message + "-" + function);
            }
        }

        public PictureBox GetPictureBox()
        {
            return pbx;
        }

        public void CropImage(RectangleF rect)
        {
            if (CropImageDelegate != null)
                CropImageDelegate(rect);
        }

        public void ReUpdate()
        {
            pbx.Invalidate();
        }

        public void LoadFile(Bitmap image)
        {
            if (image == null)
                return;

            pbx.Size = new Size(image.Width, image.Height);
            pbx.Image = image;
            pbx.Left = 0;
            pbx.Top = 0;
            if (pnlDrawing.Size.Width < image.Width || pnlDrawing.Size.Height < image.Height)
            {
                if (ParentFormResizeDelegate != null)
                    ParentFormResizeDelegate(image.Width, image.Height, 0);
            }
            else
            {
                PictureBoxMoveToCenter();
            }
        }

        private PointF CalcOrgPoint(PointF point)
        {
            float orgWidth = ImageManager.DisPlayImage.Width;
            float orgHeight = ImageManager.DisPlayImage.Height;
            float nowWidth = pbx.Width;
            float nowHeight = pbx.Height;

            float x = (float)point.X * orgWidth / nowWidth;
            float y = (float)point.Y * orgHeight / nowHeight;

            return new PointF(x, y);
        }

        private void CurrentPointValue(PointF point)
        {
            StatusLabelPoint.Text = "";
            StatusLabelPoint.Text = "";

            ColorParam result = ImageManager.GetPixelValue(point);

            string pointMessage = "(" + ((int)point.X).ToString() + "," + ((int)point.Y).ToString() + ")";
            string sizeMessage = "[W : " + ImageManager.DisPlayImage.Width.ToString() + " H : " +
                ImageManager.DisPlayImage.Height.ToString() + "]";

            StatusLabelPoint.Text = pointMessage;
            StatusLabelSize.Text = sizeMessage;

            if (ImageManager.DisPlayImage.Color == KiyLib.DIP.KColorType.Gray)
            {
                StatusLabelStringValue1.Text = "Grey";
                StatusLabelValue1.Text = result.Grey.ToString();
            }
            else
            {
                StatusLabelStringValue1.Text = "R";
                StatusLabelValue1.Text = result.R.ToString();
                StatusLabelStringValue2.Text = "G";
                StatusLabelValue2.Text = result.G.ToString();
                StatusLabelStringValue3.Text = "B";
                StatusLabelValue3.Text = result.B.ToString();
            }
        }

        private void pbx_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.TrackerManager.ModeType = eModeType.None;
            this.TrackerManager.DrawType = eDrawType.None;
            this.TrackerManager.ClearFigureSelected();
            if(PanelSelectedUpdateDele !=null)
                PanelSelectedUpdateDele();
        }     
    }
}
