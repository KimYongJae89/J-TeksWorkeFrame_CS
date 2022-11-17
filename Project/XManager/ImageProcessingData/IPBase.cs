using KiyLib.DIP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XManager.ImageProcessingData
{
    public enum eImageProcessType
    {
        Blur, Sharp1, Sharp2, Sobel, Laplacian, Canny, HorizonEdge, VerticalEdge, Median, Dilate, Erode, Average, 
        LUT, GreyLeveling, // 나중에 COLOR R,G,B 로 생각해서 추가해야함
        Rotate_CW, Rotate_CCW,
        Flip_Vertical, Flip_Horizontal,
        Resize, Crop, UserFilter, Basic,
    }

    //public enum eImageFilterType
    //{
    //    Blur, Sharp1, Sharp2, Sobel, Laplacian, Canny, HorizonEdge, VerticalEdge, Median, Dilate, Erode, Average,// Basic, UserFilter,
    //    LUT, Leveling,
    //}

    public enum eThresholdType
    {
        None, Threshold, ThresholdAdaptive, ThresholdOtsu,
    }
    public abstract class IPBase : ICloneable
    {
        public KDepthType BitType;
        //public void GetBitType()
        //{
        //    KDepthType bitType = KDepthType.Dt_8;

        //    if (CStatus.Instance().NULLCheckDrawBox())
        //        bitType = KDepthType.Dt_8;
        //    else
        //        bitType = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Depth;

        //    BitType = bitType;
        //}
        public eImageProcessType ProcessType { get; set; }
        public abstract void SetParam(object param);
        public abstract object GetParam();
        public abstract JImage Execute(JImage inputImage);
        public abstract void Save(XmlDocument xmlDocument, XmlElement element);
        public abstract void Load(XmlNode node);
        public abstract object Clone();

        //public abstract IPBase Copy();
        // public abstract void ConsoleMessage();
    }
}
