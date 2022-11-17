using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XManager.Util;

namespace XManager.FigureData
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
        
        public void MouseDown(PointF startTrackPos)
        {
            _mouseType = eMouseEventType.Down;
            _tempStartPoint = startTrackPos;
            _tempFigure = null;

            if (_figureManager != null)
                _figureManager.MouseDown(startTrackPos);
        }

        public void MouseMove(PointF point)
        {
            _tempEndPoint = point;

            if (_figureManager != null)
                _figureManager.MouseMove(point);

            ownerControl.Invalidate();
        }

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

        public void Draw(Graphics g)
        {
            if(_figureManager != null)
                _figureManager.Draw(g);
        }

        public void DrawTempFigure(Graphics g)
        {
            if (this.ModeType == eModeType.None || this.ModeType == eModeType.Crop)
            {
                if (_mouseType == eMouseEventType.Down)
                {
                    _tempFigure = new RectangleFigure(_tempStartPoint, _tempEndPoint, true);
                    _tempFigure.Draw(g);
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
                                    CStatus.Instance().UpdateRoiToRoiForm(eFormUpdate.Add);
                                    //_figureManager.ClearFigureSelected();
                                }
                                else if (this.DrawType == eDrawType.Profile)
                                {
                                    CStatus.Instance().UpdateProfileToForm(eFormUpdate.Add);
                                    //_figureManager.ClearFigureSelected();
                                }
                                else
                                    _figureManager.ClearFigureSelected();

                            }
                        }
                    }
                }
            }
        }

        public void Scale(ImageCoordTransformer coordTransformer)
        {
            _figureManager.Scale(coordTransformer);
        }

        public bool IsSeclected()
        {
            return _figureManager.IsSelected();
        }

        public bool IsMultiSelected()
        {
            return _figureManager.IsMultiSelected();
        }

        public void CheckPointInFigure(PointF point)
        {
            if (DrawType == eDrawType.None)
            {
                if (_figureManager != null)
                    _figureManager.CheckPointInFigure(point);
            }
        }

        public void ClearFigureSelected()
        {
            _figureManager.ClearFigureSelected();

            ownerControl.Invalidate();
            _tempFigure = null;
        }

        public void TempFigureMouseDown(PointF startTrackPos)
        {
            _mouseType = eMouseEventType.Down;
            _tempStartPoint = startTrackPos;
            _tempFigure = null;
        }

        public void TempFigureMouseMove(PointF point)
        {
            _tempEndPoint = point;
            ownerControl.Invalidate();
        }

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

        public void DeleteSelected()
        {
            _figureManager.DeleteSelected();
            ownerControl.Invalidate();
        }

        public void Transform(eImageTransform type, PointF centerPoint, double rotateAngle = 0)
        {
            _figureManager.Transform(type, centerPoint, rotateAngle);
            ownerControl.Invalidate();
        }

        public List<Figure> GetFigureGroup()
        {
            return _figureManager.GetFigureGroup();
        }

        public void ClearFigure()
        {
            _figureManager.ClearFigure();
        }

        public List<RectangleFigure> GetRoiAllList()
        {
            return _figureManager.GetRoiAllList();
        }

        public RectangleFigure GetSelectedRoi()
        {
            return _figureManager.GetSelectedRoi();
        }

        public void SetFirstRoi()
        {
            _figureManager.SetFirstRoi();
        }

        public void SetFirstProfile()
        {
            _figureManager.SetFirstProfile();
        }
        public bool GetSeletedFigure()
        {
            return _figureManager.GetSeletedFigure();
        }
        public List<ProfileFigure> GetProfileAllList()
        {
            return _figureManager.GetProfileAllList();
        }

        public ProfileFigure GetSelectedProfile()
        {
            return _figureManager.GetSelectedProfile();
        }

        public LineFigure GetSelectedLine()
        {
            return _figureManager.GetSelectedLine();
        }


        public void RoiSelectUpdate(int id)
        {
            _figureManager.RoiSelectUpdate(id);
            ownerControl.Invalidate();
        }

        public void ProfileSelectUpdate(int id)
        {
            _figureManager.ProfileSelectUpdate(id);
            ownerControl.Invalidate();
        }

        public RectangleF GetRoi(int id)
        {
            return _figureManager.GetRoi(id);
        }

        public int GetRoiCount()
        {
            return _figureManager.GetRoiCount();
        }

        public int GetProfileCount()
        {
            return _figureManager.GetProfileCount();
        }

        public int SelectedFigureCount()
        {
            return _figureManager.SelectedFigureCount();
        }

        private double CalcLengthPointToPoint(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2));
        }

        public void Invaldidate()
        {
            ownerControl.Invalidate();
        }

        public eFigureType CheckMeasurementType()
        {
            return _figureManager.CheckMeasurementType();
        }
    }
}