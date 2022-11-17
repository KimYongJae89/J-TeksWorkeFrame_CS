using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using XManager.Controls;
using XManager.Util;

namespace XManager.FigureData
{
    public class FigureManager
    {
        private List<Figure> _figureGroup = new List<Figure>();
        public void AddFigure(Figure figure)
        {
            if (figure == null)
                return;
            if(figure.Type == eFigureType.Rectangle)
            {
                int newId = GetRoiRectangleBlankID();
                figure.Id = newId.ToString();
            }
            if(figure.Type == eFigureType.Profile)
            {
                int newId = GetProfileRectangleBlankID();
                figure.Id = newId.ToString();
                figure.Selectable = true;
            }
            _figureGroup.Add(figure);
        }
  
        public void Draw(Graphics g)
        {
            foreach (Figure figure in _figureGroup)
            {
                figure.Draw(g);
            }
        }

        public void Scale(ImageCoordTransformer coordTransformer)
        {
            foreach (Figure figure in _figureGroup)
            {
                figure.Scale(coordTransformer);
            }
        }

        public bool IsSelected()
        {
            bool isSelected = false;
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Selectable)
                    isSelected = true;
            }
            return isSelected;
        }

        public void ClearFigureSelected()
        {
            foreach (Figure figure in _figureGroup)
            {
   
                figure.StartingMovePoint = figure.MovingMovePoint;
                figure.Selectable = false;
                figure.MultiSelectable = false;
            }
        }

        public void MouseDown(PointF startTrackPos)
        {
            if (SetCursorType(startTrackPos) == eTrackPosType.None)
            {
                return;
            }

            foreach (Figure figure in _figureGroup)
            { 
                figure.MouseDown(startTrackPos);
            }
        }

        public void MouseMove(PointF pt)
        {
            bool isMultiSelected = IsMultiSelected();

            foreach (Figure figure in _figureGroup)
            {
                if (isMultiSelected && figure.Selectable)
                {
                    if (figure.FigureMode != eFigureMode.None)
                    {
                        figure.FigureMode = eFigureMode.Edit;
                        figure.TrackerPosType = eTrackPosType.Inside;
                    }
                }
                if (figure.Type == eFigureType.Rectangle && figure.Selectable)
                {
                    //if (Status.Instance().IsDrawingRoi)
                    //{
                    //    continue;
                    //}

                    figure.MouseMove(pt);

                    RectangleFigure roi = CStatus.Instance().GetDrawBox().TrackerManager.GetSelectedRoi();
                    if (roi == null)
                        continue;

                    RectangleF orgRoi = ((tRectangleResult)roi.GetResult()).resultRectangle;
                    
                    if (orgRoi.X < 0 || orgRoi.Y < 0 || orgRoi.X + orgRoi.Width > CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width
                        || orgRoi.Y + orgRoi.Height > CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height)
                        continue;

                    CStatus.Instance().RoiListFormMoveUpdate(orgRoi);
                    continue;
                }
                if (figure.Type == eFigureType.Profile && figure.Selectable)
                {
                    if (CStatus.Instance().IsDrawingProfile)
                        continue;

                    figure.MouseMove(pt);

                    List<HistogramParams> param = CStatus.Instance().GetDrawBox().ImageManager.GetProfileHistogramParamList();

                    ProfileFigure profileFigure = CStatus.Instance().GetDrawBox().TrackerManager.GetSelectedProfile();

                    if (profileFigure == null)
                        continue;

                    tProfileResult result = (tProfileResult)profileFigure.GetResult();

                    PointF orgStartPoint = CStatus.Instance().GetDrawBox().ImageManager.GetOrgPoint(result.StartPoint);
                    PointF orgEndPoint = CStatus.Instance().GetDrawBox().ImageManager.GetOrgPoint(result.EndPoint);

                    if (orgStartPoint.X < 0 || orgStartPoint.Y < 0
                       || orgEndPoint.X < 0 || orgEndPoint.Y < 0
                        || orgStartPoint.X > CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width
                        || orgEndPoint.X > CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width
                       || orgStartPoint.Y > CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height
                       || orgEndPoint.Y > CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height)
                        continue;

                    CStatus.Instance().ProfileListFormMoveUpdate(param, orgStartPoint, orgEndPoint);
                    continue;
                }
                
                figure.MouseMove(pt);
            }
        }

        public void MouseUp(PointF endTrackPos)
        {
            foreach (Figure figure in _figureGroup)
            {
                figure.MouseUp(endTrackPos);
            }
        }

        public eTrackPosType SetCursorType(PointF pt)
        {
            Cursor cursor = Cursors.Default;
            eTrackPosType pos = eTrackPosType.None;
            bool isMultiInsizePoint = false;
            bool isMultiSelected = IsMultiSelected();

            foreach (Figure figure in _figureGroup)
            {
                if(figure.Selectable)
                {
                    pos |= figure.CheckTrackPos(pt);
                    if (figure.Type != eFigureType.None)
                    {
                        if(pos == eTrackPosType.LeftTop || pos == eTrackPosType.RightBottom)
                        {
                            Cursor.Current = Cursors.SizeNWSE;
                            figure.FigureMode = eFigureMode.Edit;
                            isMultiInsizePoint = true;
                        }
                        else if(pos == eTrackPosType.LeftBottom || pos == eTrackPosType.RightTop)
                        {
                            Cursor.Current = Cursors.SizeNESW;
                            figure.FigureMode = eFigureMode.Edit;
                            isMultiInsizePoint = true;
                        }
      
                        else if(pos == eTrackPosType.Inside)
                        {
                            Cursor.Current = Cursors.SizeAll;
                            figure.FigureMode = eFigureMode.Edit;
                            isMultiInsizePoint = true;
                        }
                        else if(pos == eTrackPosType.Top || pos == eTrackPosType.Bottom)
                        {
                            Cursor.Current = Cursors.SizeNS;
                            figure.FigureMode = eFigureMode.Edit;
                            isMultiInsizePoint = true;
                        }
                        else if (pos == eTrackPosType.Left || pos == eTrackPosType.Right)
                        {
                            Cursor.Current = Cursors.SizeWE;
                            figure.FigureMode = eFigureMode.Edit;
                            isMultiInsizePoint = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            if (pos == eTrackPosType.None)
            {
                foreach (Figure figure in _figureGroup)
                {
                    figure.FigureMode = eFigureMode.None;
                }
                Cursor.Current = Cursors.Default;
            }
            else
            {
                if(IsMultiSelected() && isMultiInsizePoint == true)
                {
                    foreach (Figure figure in _figureGroup)
                    {
                        figure.FigureMode = eFigureMode.Edit;
                    }
                    Cursor.Current = Cursors.SizeAll;
                }
            }
            return pos;
        }

        public int SelectedCount()
        {
            int count = 0;
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Selectable)
                    count++;
            }
            return count;
        }

        public bool IsMultiSelected()
        {
            bool isMultiSelected = false;
            foreach (Figure figure in _figureGroup)
            {
                if (figure.MultiSelectable)
                    isMultiSelected = true;
            }
            return isMultiSelected;
        }

        public bool CheckRegionInFigure(PointF startPoint, PointF endPoint)
        {
            bool ret = false;
            foreach (Figure figure in _figureGroup)
                figure.Selectable = false;
            foreach (Figure figure in _figureGroup)
            {
                if (figure.CheckRegionInFigure(startPoint, endPoint))
                {
                    figure.Selectable = true;
                    ret = true;

                    if (figure.Selectable && figure.Type == eFigureType.Rectangle)
                    {
                        CStatus.Instance().SelectRoi(Convert.ToInt32(figure.Id));
                        if(CStatus.Instance().RoiListForm != null)
                            return ret;
                    }
                    if (figure.Selectable && figure.Type == eFigureType.Profile)
                    {
                        CStatus.Instance().SelectProfile(Convert.ToInt32(figure.Id));
                        //if (Status.Instance().ProfileListForm != null)
                            return ret;
                    }
                }
                else
                    figure.Selectable = false;

            }

            if (!ret)
            {
                CStatus.Instance().SelectRoi(-1);
                CStatus.Instance().SelectProfile(-1);
            }

            return ret;
        }

        public bool CheckPointInFigure(PointF point)
        {
            bool ret = false;
            bool detect = false;
            foreach (Figure figure in _figureGroup)
                figure.Selectable = false;

            foreach (Figure figure in _figureGroup)
            {
                if (figure.CheckPointInFigure(point) && !detect)
                {
                    detect = true;
                    figure.Selectable = true;
                    ret = true;

                    if (figure.Selectable && figure.Type == eFigureType.Rectangle)
                    {
                        CStatus.Instance().SelectRoi(Convert.ToInt32(figure.Id));
                        if (CStatus.Instance().RoiListForm != null)
                            return ret;
                    }

                    if (figure.Selectable && figure.Type == eFigureType.Profile)
                    {
                        CStatus.Instance().SelectProfile(Convert.ToInt32(figure.Id));
                        //if (CStatus.Instance().ProfileListForm != null)
                            return ret;
                    }

                    //return ret;
                }
                else
                {

                    figure.Selectable = false;
                }

            }
            if (!ret)
            {
                CStatus.Instance().SelectRoi(-1);
                CStatus.Instance().SelectProfile(-1);
            }

            return ret;
        }

        public void SetMultiSelectProperty(bool isMultiSelected)
        {
            foreach (Figure figure in _figureGroup)
            {
                figure.MultiSelectable = isMultiSelected;
            }
        }

        public List<object> GetResult(eFigureType type)
        {
            List<object> result = new List<object>();
            foreach (Figure figure in _figureGroup)
            {
                if(type == eFigureType.None)
                {
                    result.Add(figure.GetResult());
                }
                if(figure.Type == type)
                {
                    result.Add(figure.GetResult());
                }
            }
            return result;
        }

        public void DeleteSelected()
        {
            List<Figure> removeFigures = new List<Figure>();
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Selectable)
                {
                    removeFigures.Add(figure);
                }
            }
            _figureGroup.RemoveAll(removeFigures.Contains);
        }

        public void Transform(eImageTransform type, PointF centerPoint, double rotateAngle = 0)
        {
            foreach (Figure figure in _figureGroup)
            {
                figure.Transform(type, centerPoint, rotateAngle);
            }
        }

        public List<Figure> GetFigureGroup()
        {
            return _figureGroup;
        }

        public void ClearFigure()
        {
            List<Figure> removeFigures = new List<Figure>();
            foreach (Figure figure in _figureGroup)
            {
                removeFigures.Add(figure);
            }
            _figureGroup.RemoveAll(removeFigures.Contains);
        }

        public void RoiSelectUpdate(int id)
        {
            ClearFigureSelected();
            foreach (Figure figure in _figureGroup)
            {
                if(figure.Type == eFigureType.Rectangle)
                {
                    if (figure.Id == id.ToString())
                        figure.Selectable = true;
                    else
                        figure.Selectable = false;
                }
            }
        }

        public void ProfileSelectUpdate(int id)
        {
            ClearFigureSelected();
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Type == eFigureType.Profile)
                {
                    if (figure.Id == id.ToString())
                        figure.Selectable = true;
                    else
                        figure.Selectable = false;
                }
            }
        }

        public RectangleF GetRoi(int id)
        {
            //return _figureManager.GetRoi(id);
            RectangleF rect = new RectangleF();
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Type == eFigureType.Rectangle && figure.Id == id.ToString())
                {
                    RectangleF roi = ((tRectangleResult)figure.GetResult()).resultRectangle;
                    rect.X = roi.X;
                    rect.Y = roi.Y;
                    rect.Width = roi.Width;
                    rect.Height = roi.Height;
                }
            }
            return rect;
        }

        public List<RectangleFigure> GetRoiAllList()
        {
            List<RectangleFigure> _roiList = new List<RectangleFigure>();

            foreach (Figure figure in _figureGroup)
            {
                if(figure.Type == eFigureType.Rectangle)
                {
                    RectangleFigure roi = (RectangleFigure)figure;
                    _roiList.Add(roi);
                }
            }

            return _roiList;
        }

        public List<ProfileFigure> GetProfileAllList()
        {
            List<ProfileFigure> _profileList = new List<ProfileFigure>();

            foreach (Figure figure in _figureGroup)
            {
                if (figure.Type == eFigureType.Profile)
                {
                    ProfileFigure profile = (ProfileFigure)figure;
                    _profileList.Add(profile);
                }
            }

            return _profileList;
        }

        public RectangleFigure GetSelectedRoi()
        {
            RectangleFigure ret;
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Type == eFigureType.Rectangle && figure.Selectable)
                {
                    return ret = (RectangleFigure)figure;
                }
            }
            //return GetRoiAllList()[0];
            return null;
        }

        public void SetFirstRoi()
        {
            this.ClearFigureSelected();
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Type == eFigureType.Rectangle)
                {
                    figure.Selectable = true;
                    return;
                }
            }
        }

        public void SetFirstProfile()
        {
            this.ClearFigureSelected();
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Type == eFigureType.Profile)
                {
                    figure.Selectable = true;
                    return;
                }
            }
        }

        public bool GetSeletedFigure()
        {
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Selectable)
                    return true;
            }
            return false;
        }

        public ProfileFigure GetSelectedProfile()
        {
            ProfileFigure ret;
            foreach (Figure figure in _figureGroup)
            {
                if(figure.Type == eFigureType.Profile && figure.Selectable)
                {
                    figure.Selectable = true;
                    return ret = (ProfileFigure)figure;
                }
            }

            List<ProfileFigure> profileList = GetProfileAllList();
            if (profileList.Count == 0)
                return null;
            else
            {
                return null;
                //profileList[0].Selectable = true;
                //return profileList[0];
            }
        }

        public LineFigure GetSelectedLine()
        {
            LineFigure ret;
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Type == eFigureType.Line && figure.Selectable)
                {
                    return ret = (LineFigure)figure;
                }
            }

            return null;
        }

        public int GetRoiCount()
        {
            int count = 0;
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Type == eFigureType.Rectangle)
                    count++;
            }
            return count;
        }

        private int GetRoiRectangleBlankID()
        {
            int blackIDNum = 0;
            int roiNum = GetRoiCount();
            for (int i = 0; i < roiNum; i++)
            {
                bool isId = false;
                foreach (Figure figure in _figureGroup)
                {
                    if (i.ToString() == figure.Id && figure.Type == eFigureType.Rectangle)
                        isId = true;
                }
                if (isId == true)
                    blackIDNum++;
                else
                    break;
            }
            return blackIDNum;
        }

        private int GetProfileRectangleBlankID()
        {
            int blackIDNum = 0;
            int roiNum = GetProfileCount();
            for (int i = 0; i < roiNum; i++)
            {
                bool isId = false;
                foreach (Figure figure in _figureGroup)
                {
                    if (i.ToString() == figure.Id && figure.Type == eFigureType.Profile)
                        isId = true;
                }
                if (isId == true)
                    blackIDNum++;
                else
                    break;
            }
            return blackIDNum;

        }


        public int GetProfileCount()
        {
            int count = 0;
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Type == eFigureType.Profile)
                {
                    count++;
                }
            }
            return count;
        }


        public int SelectedFigureCount()
        {
            int count = 0;
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Selectable)
                {
                    count++;
                }
            }
            return count;
        }

        public eFigureType CheckMeasurementType()
        {
            eFigureType type = eFigureType.None;

            foreach (Figure figure in _figureGroup)
            {
                if(figure.Selectable)
                {
                    if (figure.Type == eFigureType.Profile || figure.Type == eFigureType.Line)
                    {
                        type = figure.Type;
                        break;
                    }
                }
            }
            return type;
        }
    }
}
