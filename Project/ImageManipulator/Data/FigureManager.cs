using ImageManipulator.Controls;
using ImageManipulator.Util;
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

namespace ImageManipulator.Data
{
    public class FigureManager
    {
        private List<Figure> _figureGroup = new List<Figure>();
        private List<Figure> _localHistogramFigure = new List<Figure>();
        /// <summary>
        /// Figure를 추가
        /// </summary>
        /// <param name="figure">Figure</param>
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

            //Log
            switch (figure.Type)
            {
                case eFigureType.None:
                    break;
                case eFigureType.Group:
                    break;
                case eFigureType.Line:
                    Status.Instance().LogManager.AddLogMessage("Add Figure", "Line");
                    break;
                case eFigureType.Rectangle:
                    Status.Instance().LogManager.AddLogMessage("Add Figure", "Roi");
                    break;
                case eFigureType.Polygon:
                    break;
                case eFigureType.Protractor:
                    Status.Instance().LogManager.AddLogMessage("Add Figure", "Protractor");
                    break;
                case eFigureType.Text:
                    break;
                case eFigureType.Image:
                    break;
                case eFigureType.Profile:
                    Status.Instance().LogManager.AddLogMessage("Add Figure", "Profile");
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Histogram 선택시 드래그할 Figure를 추가
        /// </summary>
        /// <param name="figure">Rectangle Figure</param>
        public void AddLocalHistogramFigure(Figure figure)
        {
            if (figure == null)
                return;
            if(figure.Type == eFigureType.Rectangle)
            {
                _localHistogramFigure.Add(figure);
            }
        }
        /// <summary>
        /// Histogram Figure 삭제
        /// </summary>
        public void ClearLocalHistogramFigure()
        {
            _localHistogramFigure.Clear();
        }
        /// <summary>
        /// Figure전체 그린다.
        /// </summary>
        /// <param name="g">Graphic 객체</param>
        public void Draw(Graphics g)
        {
            foreach (Figure figure in _figureGroup)
            {
                figure.Draw(g);
            }
            foreach (Figure figure in _localHistogramFigure)
            {
                figure.Draw(g);
            }
        }
        /// <summary>
        /// 전체 Figure 좌표를 기준에 맞춰 변경
        /// </summary>
        /// <param name="coordTransformer">ImageCoordTransformer 클래스</param>
        public void Scale(ImageCoordTransformer coordTransformer)
        {
            foreach (Figure figure in _figureGroup)
            {
                figure.Scale(coordTransformer);
            }
        }
        /// <summary>
        /// 전체 Figure 중 선택된 Figure가 있는지 여부 확인
        /// </summary>
        /// <returns>결과값</returns>
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
        /// <summary>
        /// 선택된 Figure 선택 해제
        /// </summary>
        public void ClearFigureSelected()
        {
            foreach (Figure figure in _figureGroup)
            {
   
                figure.StartingMovePoint = figure.MovingMovePoint;
                figure.Selectable = false;
                figure.MultiSelectable = false;
            }
        }
        /// <summary>
        /// 전체 Figure Mouse Down 함수 실행
        /// </summary>
        /// <param name="startTrackPos"></param>
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
        /// <summary>
        /// 전체 Figure Mouse Move 함수 실행
        /// </summary>
        /// <param name="pt"></param>
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

                    RectangleFigure roi = Status.Instance().SelectedViewer.GetDrawBox().GetSelectedRoi();
                    if (roi == null)
                        continue;

                    RectangleF orgRoi = ((tRectangleResult)roi.GetResult()).resultRectangle;
                    
                    if (orgRoi.X < 0 || orgRoi.Y < 0 || orgRoi.X + orgRoi.Width > Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width
                        || orgRoi.Y + orgRoi.Height > Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height)
                        continue;

                    Status.Instance().RoiListFormMoveUpdate(orgRoi);
                    continue;
                }
                if (figure.Type == eFigureType.Profile && figure.Selectable)
                {
                    if (Status.Instance().IsDrawingProfile)
                        continue;

                    figure.MouseMove(pt);

                    List<HistogramParams> param = Status.Instance().SelectedViewer.ImageManager.GetProfileHistogramParamList();

                    ProfileFigure profileFigure = Status.Instance().SelectedViewer.GetDrawBox().GetSelectedProfile();

                    if (profileFigure == null)
                        continue;

                    tProfileResult result = (tProfileResult)profileFigure.GetResult();

                    PointF orgStartPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(result.StartPoint);
                    PointF orgEndPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(result.EndPoint);

                    if (orgStartPoint.X < 0 || orgStartPoint.Y < 0
                       || orgEndPoint.X < 0 || orgEndPoint.Y < 0
                        || orgStartPoint.X > Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width
                        || orgEndPoint.X > Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width
                       || orgStartPoint.Y > Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height
                       || orgEndPoint.Y > Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height)
                        continue;

                    Status.Instance().ProfileListFormMoveUpdate(param, orgStartPoint, orgEndPoint);
                    continue;
                }
                
                figure.MouseMove(pt);
            }
        }
        /// <summary>
        /// 전체 Figure Mouse Up 함수 실행
        /// </summary>
        /// <param name="endTrackPos"></param>
        public void MouseUp(PointF endTrackPos)
        {
            foreach (Figure figure in _figureGroup)
            {
                figure.MouseUp(endTrackPos);
            }
        }
        /// <summary>
        /// Pt 위치를 구분하여 마우스 커서 모양을 바꾼다.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>커서 모양</returns>
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
        /// <summary>
        /// 선택된 Figure 갯수
        /// </summary>
        /// <returns>선택된 갯수</returns>
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
        /// <summary>
        /// 2개 이상 Figure가 선택됬는지 여부
        /// </summary>
        /// <returns>결과값</returns>
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
        /// <summary>
        /// Rectangle 안에 Figure가 있는지 여부 확인
        /// </summary>
        /// <param name="startPoint">Rectangle Left,Top</param>
        /// <param name="endPoint">Rectangle Right,Bottom</param>
        /// <returns>결과값</returns>
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
                        Status.Instance().SelectRoi(Convert.ToInt32(figure.Id));
                        if(Status.Instance().RoiListForm != null)
                            return ret;
                    }
                    if (figure.Selectable && figure.Type == eFigureType.Profile)
                    {
                        Status.Instance().SelectProfile(Convert.ToInt32(figure.Id));
                        if (Status.Instance().ProfileListForm != null)
                            return ret;
                    }
                }
                else
                    figure.Selectable = false;

            }

            if (!ret)
            {
                Status.Instance().SelectRoi(-1);
                Status.Instance().SelectProfile(-1);
            }

            return ret;
        }
        /// <summary>
        /// Point안에 Figure가 있는지 여부
        /// </summary>
        /// <param name="point">Point</param>
        /// <returns>결과값</returns>
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
                        Status.Instance().SelectRoi(Convert.ToInt32(figure.Id));
                        if (Status.Instance().RoiListForm != null)
                            return ret;
                    }

                    if (figure.Selectable && figure.Type == eFigureType.Profile)
                    {
                        Status.Instance().SelectProfile(Convert.ToInt32(figure.Id));
                        if (Status.Instance().ProfileListForm != null)
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
                Status.Instance().SelectRoi(-1);
                Status.Instance().SelectProfile(-1);
            }

            return ret;
        }
        /// <summary>
        /// 전체 Figure 의 MultiSeleted를 변경한다.
        /// </summary>
        /// <param name="isMultiSelected">변경할 값</param>
        public void SetMultiSelectProperty(bool isMultiSelected)
        {
            foreach (Figure figure in _figureGroup)
            {
                figure.MultiSelectable = isMultiSelected;
            }
        }
        /// <summary>
        /// FigureType별 Figure의 Result값을 가져온다.
        /// </summary>
        /// <param name="type">FigureType</param>
        /// <returns>Result 리스트</returns>
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
        /// <summary>
        /// 선택된 Figure 삭제
        /// </summary>
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

            Status.Instance().LogManager.AddLogMessage("Delete Figure", "");
        }
        /// <summary>
        /// eImageTransform 타입별 Figure 회전, 반전
        /// </summary>
        /// <param name="type">eImageTransform 타입</param>
        /// <param name="centerPoint">centerPoint</param>
        /// <param name="rotateAngle">rotateAngle</param>
        public void Transform(eImageTransform type, PointF centerPoint, double rotateAngle = 0)
        {
            foreach (Figure figure in _figureGroup)
            {
                figure.Transform(type, centerPoint, rotateAngle);
            }
        }
        /// <summary>
        /// Figure Group을 가져온다.
        /// </summary>
        /// <returns>Figure Group</returns>
        public List<Figure> GetFigureGroup()
        {
            return _figureGroup;
        }
        /// <summary>
        /// 전체 Figure 삭제한다.
        /// </summary>
        public void ClearFigure()
        {
            List<Figure> removeFigures = new List<Figure>();
            foreach (Figure figure in _figureGroup)
            {
                removeFigures.Add(figure);
            }
            _figureGroup.RemoveAll(removeFigures.Contains);
        }
        /// <summary>
        /// Rectangle의 id를 찾아 선택합니다.
        /// </summary>
        /// <param name="id">선택할 id</param>
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
        /// <summary>
        /// Profile의 id를 찾아 선택합니다.
        /// </summary>
        /// <param name="id">선택할 id</param>
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
        /// <summary>
        /// id값을 가진 Roi의 Rectangle을 가져온다.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Rectangle을</returns>
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
        /// <summary>
        /// 모든 Roi를 가져온다.
        /// </summary>
        /// <returns>Roi 리스트</returns>
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
        /// <summary>
        /// 모든 Profile을 가져온다.
        /// </summary>
        /// <returns>Profile 리스트</returns>
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
        /// <summary>
        /// 선택된 Roi를 가져온다.
        /// </summary>
        /// <returns>RectangleFigure</returns>
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
        /// <summary>
        /// 전체 Figure 중 선택된 Figure가 있는지 여부 확인
        /// </summary>
        /// <returns>결과값</returns>
        public bool GetSeletedFigure()
        {
            foreach (Figure figure in _figureGroup)
            {
                if (figure.Selectable)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 선택된 Profile Figure를 가져온다.
        /// </summary>
        /// <returns>결과값</returns>
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
        /// <summary>
        /// 선택된 Line Figure를 가져온다.
        /// </summary>
        /// <returns>결과값</returns>
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
        /// <summary>
        /// Roi 갯수
        /// </summary>
        /// <returns>결과값</returns>
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
        /// <summary>
        /// Roi id가 0부터 증가할 경우 빈 번호를 가져온다.
        /// </summary>
        /// <returns>결과값</returns>
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
        /// <summary>
        /// Profile id가 0부터 증가할 경우 빈 번호를 가져온다.
        /// </summary>
        /// <returns>결과값</returns>
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
        /// <summary>
        /// Profiel 갯수
        /// </summary>
        /// <returns>결과값</returns>
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
        /// <summary>
        /// 선택된 Figure 갯수
        /// </summary>
        /// <returns>결과값</returns>
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
        /// <summary>
        /// 선택된 Figure가 Measure 타입인지 여부 확인
        /// </summary>
        /// <returns>결과값</returns>
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
