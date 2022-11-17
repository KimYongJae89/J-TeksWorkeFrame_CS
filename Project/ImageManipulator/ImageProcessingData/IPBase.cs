using KiyLib.DIP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageManipulator.ImageProcessingData
{
    public enum eImageProcessType
    {
        Blur, Sharp1, Sharp2, Sobel, Laplacian, Canny, HorizonEdge, VerticalEdge, Median, Dilate, Erode, Average, 
        LUT_8, LUT_16, Leveling_8, Leveling_16, // 나중에 COLOR R,G,B 로 생각해서 추가해야함
        Rotate_CW, Rotate_CCW,
        Flip_Vertical, Flip_Horizontal,
        Threshold, ThresholdAdaptive,
        Resize, Crop, UserFilter, Basic,
    }

    public abstract class IPBase : ICloneable
    {
        /// <summary>
        /// KepthType 타입
        /// </summary>
        public KDepthType BitType;
        /// <summary>
        /// Process 타입
        /// </summary>
        public eImageProcessType ProcessType { get; set; }
        /// <summary>
        /// Parameter 설정
        /// </summary>
        /// <param name="param"></param>
        public abstract void SetParam(object param);
        /// <summary>
        /// Parameter 가져오기
        /// </summary>
        /// <returns>Parameter</returns>
        public abstract object GetParam();
        /// <summary>
        /// 객체 실행
        /// </summary>
        /// <param name="inputImage">실행할 이미지</param>
        /// <returns>실행된 이미지</returns>
        public abstract JImage Execute(JImage inputImage);
        /// <summary>
        /// Xml형식로 저장한다.
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="element"></param>
        public abstract void Save(XmlDocument xmlDocument, XmlElement element);
        /// <summary>
        /// Xml형식으로 값을 읽는다.
        /// </summary>
        /// <param name="node"></param>
        public abstract void Load(XmlNode node);
        /// <summary>
        /// 객체를 복사한다.
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();
    }
}
