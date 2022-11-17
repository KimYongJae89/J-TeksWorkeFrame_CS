using ImageManipulator.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulator.Util
{

    public class Tracker
    {
        private Control ownerControl;
        private Figure _tempFigure;
        private FigureManager _figureManager = new FigureManager();
        private eMouseEventType _mouseType = eMouseEventType.None;
        public eDrawType DrawType { get; set; }
        public eModeType ModeType { get; set; }
        private PointF _tempStartPoint;
        private PointF _tempEndPoint;
        public Action<RectangleF> CropImageDelegate;
        public Tracker(Control control)
        {
            ownerControl = control;
        }
        /// <summary>
        /// 전체 Figure Mouse Down 함수 실행
        /// </summary>
        /// <param name="startTrackPos"></param>
        public void MouseDown(PointF startTrackPos)
        {
            _mouseType = eMouseEventType.Down;
            _tempStartPoint = startTrackPos;
            _tempFigure = null;

            if (_figureManager != null)
                _figureManager.MouseDown(startTrackPos);
        }
        /// <summary>
        /// 전체 Figure Mouse Move 함수 실행
        /// </summary>
        /// <param name="pt"></param>
        public void MouseMove(PointF point)
        {
            _tempEndPoint = point;

            if (_figureManager != null)
                _figureManager.MouseMove(point);

            ownerControl.Invalidate();
        }
        /// <summary>
        /// Pt 위치를 구분하여 마우스 커서 모양을 바꾼다.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>커서 모양</returns>
        public void SetCursorType(PointF point)
        {
            _tempEndPoint = point;
            if (DrawType == eDrawType.None)
                _figureManager.SetCursorType(point);
            else
            {
                if(_mouseType == eMouseEventType.Up)
                {
                    if (!_figureManager.CheckPointInFigure(point))
                        _figureManager.ClearFigureSelected();
                }
            }
        }
        /// <summary>
        /// 전체 Figure Mouse Up 함수 실행
        /// </summary>
        /// <param name="endTrackPos"></param>
        public void MouseUp(PointF endTrackPos)
        {
            _tempEndPoint = endTrackPos;
            _mouseType = eMouseEventType.Up;

            if (this.ModeType == eModeType.Crop)
            {
                Figure temp = new LineFigure();
                RectangleF rectF = temp.CalcPointToRectangleF(_tempStartPoint, _tempEndPoint);

                if(!_figureManager.GetSeletedFigure())
                {
                    if (CropImageDelegate != null)
                        CropImageDelegate(rectF);
                }
                
                //ClearFigure();
            }
            else
            {
                if (_figureManager != null)
                    _figureManager.MouseUp(endTrackPos);
            }

            ownerControl.Invalidate();
        }
        /// <summary>
        /// Figure전체 그린다.
        /// </summary>
        /// <param name="g">Graphic 객체</param>
        public void Draw(Graphics g)
        {
            if(_figureManager != null)
                _figureManager.Draw(g);
        }
        /// <summary>
        /// Temp Figure를 그린다.
        /// </summary>
        /// <param name="g"></param>
        public void DrawTempFigure(Graphics g)
        {
            if (this.ModeType == eModeType.None || this.ModeType == eModeType.Crop)
            {
                if (_mouseType == eMouseEventType.Down)
                {
                    _tempFigure = new RectangleFigure(_tempStartPoint, _tempEndPoint, true);
                    _tempFigure.Draw(g);

                    //_figureManager.AddLocalHistogramFigure(_tempFigure);
                    RectangleFigure figure = (RectangleFigure)_tempFigure;
                    Status.Instance().SelectedViewer.ImageManager.LocalHistogram(figure.Rect);
                }
            }
            else
            {
                if (_mouseType == eMouseEventType.Down)
                {
                    if(CalcLengthPointToPoint(_tempStartPoint, _tempEndPoint)<3)
                    {
                        return;
                    }
                    if (this.DrawType == eDrawType.LineMeasurement)
                    {
                        _tempFigure = new LineFigure(_tempStartPoint, _tempEndPoint);
                    }
                    else if (this.DrawType == eDrawType.Roi)
                    {
                        _tempFigure = new RectangleFigure(_tempStartPoint, _tempEndPoint);
                    }
                    else if (this.DrawType == eDrawType.Protractor)
                    {
                        _tempFigure = new RectangleFigure(_tempStartPoint, _tempEndPoint, true);
                    }
                    else if (this.DrawType == eDrawType.Profile)
                    {
                        _tempFigure = new ProfileFigure(_tempStartPoint, _tempEndPoint);

                    }
                    if(_tempFigure != null)
                        _tempFigure.Draw(g);
                }

                if (_mouseType == eMouseEventType.Up)
                {
                    _mouseType = eMouseEventType.None;
                    if (this.ModeType == eModeType.Draw)
                    {
                        if (this.DrawType != eDrawType.None)
                        {
                            if (_tempFigure != null)
                            {
                                if (this.DrawType == eDrawType.Protractor)
                                {
                                    _tempFigure = new ProtractorFigure(_tempStartPoint, _tempEndPoint);
                                }

                                _figureManager.AddFigure(_tempFigure);
               
                                if(this.DrawType == eDrawType.Roi)
                                {
                                    Status.Instance().UpdateRoiToRoiForm(eFormUpdate.Add);
                                    //_figureManager.ClearFigureSelected();
                                }
                                else if (this.DrawType == eDrawType.Profile)
                                {
                                    Status.Instance().UpdateProfileToForm(eFormUpdate.Add);
                                    //_figureManager.ClearFigureSelected();
                                }
                                else
                                    _figureManager.ClearFigureSelected();

                            }
                        }
                        else
                        {
                           
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 전체 Figure 좌표를 기준에 맞춰 변경
        /// </summary>
        /// <param name="coordTransformer">ImageCoordTransformer 클래스</param>
        public void Scale(ImageCoordTransformer coordTransformer)
        {
            _figureManager.Scale(coordTransformer);
        }
        /// <summary>
        /// 전체 Figure 중 선택된 Figure가 있는지 여부 확인
        /// </summary>
        /// <returns>결과값</returns>
        public bool IsSeclected()
        {
            return _figureManager.IsSelected();
        }
        /// <summary>
        /// 2개 이상 Figure가 선택됬는지 여부
        /// </summary>
        /// <returns>결과값</returns>
        public bool IsMultiSelected()
        {
            return _figureManager.IsMultiSelected();
        }
        /// <summary>
        /// Point안에 Figure가 있는지 여부
        /// </summary>
        /// <param name="point">Point</param>
        /// <returns>결과값</returns>
        public void CheckPointInFigure(PointF point)
        {
            if (DrawType == eDrawType.None)
            {
                if (_figureManager != null)
                    _figureManager.CheckPointInFigure(point);
            }
        }
        /// <summary>
        /// 선택된 Figure 선택 해제
        /// </summary>
        public void ClearFigureSelected()
        {
            _figureManager.ClearFigureSelected();

            ownerControl.Invalidate();
            _tempFigure = null;
        }
        /// <summary>
        /// Temp Figure Mouse Down 함수 실행
        /// </summary>
        /// <param name="startTrackPos"></param>
        public void TempFigureMouseDown(PointF startTrackPos)
        {
            _mouseType = eMouseEventType.Down;
            _tempStartPoint = startTrackPos;
            _tempFigure = null;
        }
        /// <summary>
        /// Temp Figure Mouse Move 함수 실행
        /// </summary>
        /// <param name="pt"></param>
        public void TempFigureMouseMove(PointF point)
        {
            _tempEndPoint = point;
            ownerControl.Invalidate();
        }
        /// <summary>
        /// Temp Figure Mouse Up 함수 실행
        /// </summary>
        /// <param name="endTrackPos"></param>
        public void TempFigureMouseUp(PointF endTrackPos)
        {
            _tempEndPoint = endTrackPos;
            _mouseType = eMouseEventType.Up;
            if(DrawType == eDrawType.None)
                _figureManager.CheckRegionInFigure(_tempStartPoint, _tempEndPoint);
            int count = _figureManager.SelectedCount();
            if(count >=2)
            {
                _figureManager.SetMultiSelectProperty(true);
            }
            else
            {
                _figureManager.SetMultiSelectProperty(false);
            }
            ownerControl.Invalidate();
        }
        /// <summary>
        /// 선택된 Figure 삭제
        /// </summary>
        public void DeleteSelected()
        {
            _figureManager.DeleteSelected();
            ownerControl.Invalidate();
        }
        /// <summary>
        /// eImageTransform 타입별 Figure 회전, 반전
        /// </summary>
        /// <param name="type">eImageTransform 타입</param>
        /// <param name="centerPoint">centerPoint</param>
        /// <param name="rotateAngle">rotateAngle</param>
        public void Transform(eImageTransform type, PointF centerPoint, double rotateAngle = 0)
        {
            _figureManager.Transform(type, centerPoint, rotateAngle);
            ownerControl.Invalidate();
        }
        /// <summary>
        /// Figure Group을 가져온다.
        /// </summary>
        /// <returns>Figure Group</returns>
        public List<Figure> GetFigureGroup()
        {
            return _figureManager.GetFigureGroup();
        }
        /// <summary>
        /// 전체 Figure 삭제한다.
        /// </summary>
        public void ClearFigure()
        {
            _figureManager.ClearFigure();
        }
        /// <summary>
        /// 모든 Roi를 가져온다.
        /// </summary>
        /// <returns>Roi 리스트</returns>
        public List<RectangleFigure> GetRoiAllList()
        {
            return _figureManager.GetRoiAllList();
        }
        /// <summary>
        /// 선택된 Roi를 가져온다.
        /// </summary>
        /// <returns>RectangleFigure</returns>
        public RectangleFigure GetSelectedRoi()
        {
            return _figureManager.GetSelectedRoi();
        }
        /// <summary>
        /// 전체 Figure 중 선택된 Figure가 있는지 여부 확인
        /// </summary>
        /// <returns>결과값</returns>
        public bool GetSeletedFigure()
        {
            return _figureManager.GetSeletedFigure();
        }
        /// <summary>
        /// 모든 Profile을 가져온다.
        /// </summary>
        /// <returns>Profile 리스트</returns>
        public List<ProfileFigure> GetProfileAllList()
        {
            return _figureManager.GetProfileAllList();
        }
        /// <summary>
        /// 선택된 Profile Figure를 가져온다.
        /// </summary>
        /// <returns>결과값</returns>
        public ProfileFigure GetSelectedProfile()
        {
            return _figureManager.GetSelectedProfile();
        }
        /// <summary>
        /// 선택된 Line Figure를 가져온다.
        /// </summary>
        /// <returns>결과값</returns>
        public LineFigure GetSelectedLine()
        {
            return _figureManager.GetSelectedLine();
        }
        /// <summary>
        /// Rectangle의 id를 찾아 선택합니다.
        /// </summary>
        /// <param name="id">선택할 id</param>
        public void RoiSelectUpdate(int id)
        {
            _figureManager.RoiSelectUpdate(id);
            ownerControl.Invalidate();
        }
        /// <summary>
        /// Profile의 id를 찾아 선택합니다.
        /// </summary>
        /// <param name="id">선택할 id</param>
        public void ProfileSelectUpdate(int id)
        {
            _figureManager.ProfileSelectUpdate(id);
            ownerControl.Invalidate();
        }
        /// <summary>
        /// id값을 가진 Roi의 Rectangle을 가져온다.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Rectangle을</returns>
        public RectangleF GetRoi(int id)
        {
            return _figureManager.GetRoi(id);
        }
        /// <summary>
        /// Roi 갯수
        /// </summary>
        /// <returns>결과값</returns>
        public int GetRoiCount()
        {
            return _figureManager.GetRoiCount();
        }
        /// <summary>
        /// Profiel 갯수
        /// </summary>
        /// <returns>결과값</returns>
        public int GetProfileCount()
        {
            return _figureManager.GetProfileCount();
        }
        /// <summary>
        /// 선택된 Figure 갯수
        /// </summary>
        /// <returns>결과값</returns>
        public int SelectedFigureCount()
        {
            return _figureManager.SelectedFigureCount();
        }
        /// <summary>
        /// point1과 point2 사이의 거리를 구한다.
        /// </summary>
        /// <param name="p1">point1</param>
        /// <param name="p2">point2</param>
        /// <returns>결과값</returns>
        private double CalcLengthPointToPoint(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2));
        }
        /// <summary>
        /// Display 객체를 다시 그린다.
        /// </summary>
        public void Invaldidate()
        {
            ownerControl.Invalidate();
        }
        /// <summary>
        /// 선택된 Figure가 Measure 타입인지 여부 확인
        /// </summary>
        /// <returns>결과값</returns>
        public eFigureType CheckMeasurementType()
        {
            return _figureManager.CheckMeasurementType();
        }
    }
}