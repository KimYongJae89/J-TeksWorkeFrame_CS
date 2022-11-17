using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XManager.Util
{
    public enum eFormUpdate
    {
        Delete,
        Add,
    }
    public enum eLanguageType
    {
        Korea,
        English,
    }
    public enum eDerivativeType
    {
        None,
        OneDerivative,
        TwoDerivative,
    }

    public enum eMeasurementType
    {
        Fov_Calibration,
        Pixel_Calibration,
    }

    public enum eHistogramTrackPos
    {
        None, Inside, Left, Right
    }
    public enum eMarkTrackPos
    {
        None, Mark1, Mark2
    }


    public enum eMouseEventType
    {
        None, Down, Move, Up
    }

    public enum eDrawType
    {
        None, Panning, LineMeasurement, Roi, Protractor, Profile, Project,
    }

    public enum eModeType // 그리기 모드 , 일반 모드 , Crop 모드
    {
        None, Draw, Crop, Resize, Edit
    }


        public enum eTrackPosType
    {
        None, LeftTop, Top, RightTop, Right, RightBottom, Bottom, LeftBottom, Left, Rotate, Inside, Polygon, Point1, Point2
    }

    public enum eMouseType
    {
        Down, Move, Up
    }
    public enum eFigureMode
    {
        None, Edit
    }

    public enum eFigureType
    {
        None, Group, Line, Rectangle, Polygon, Protractor, Text, Image, Profile
    }
    public enum eImageTransform
    {
        CW,
        CCW,
        FlipX,
        FlipY,
    }
}
