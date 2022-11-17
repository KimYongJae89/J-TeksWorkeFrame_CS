using Emgu.CV;
using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyEmguCV.DIP
{
    public partial class KImageBaseCV : IKGeometry
    {
        /// <summary>
        /// 이미지 크기를 변경한다.
        /// </summary>
        /// <param name="scale">변경할 비율</param>
        /// <param name="interType">사용할 보간법</param>
        /// <returns>결과 이미지</returns>
        public Mat Resize(double scale, KInterpolation interType = KInterpolation.Linear)
        {
            return Resize(scale, scale, interType);
        }

        /// <summary>
        /// 이미지 크기를 변경한다.
        /// </summary>
        /// <param name="widthScale">변경할 가로 비율</param>
        /// <param name="heightScale">변경할 세로 비율</param>
        /// <param name="interType">사용할 보간법</param>
        /// <returns>결과 이미지</returns>
        public Mat Resize(double widthScale, double heightScale, KInterpolation interType = KInterpolation.Linear)
        {
            return KImageCV.Resize(cvMat, widthScale, heightScale, interType);
        }

        /// <summary>
        /// 이미지의 특정영역을 잘라낸다.
        /// </summary>
        /// <param name="ROI">잘라낼 영역</param>
        /// <returns>결과 이미지</returns>
        public Mat Crop(Rectangle ROI)
        {
            return KImageCV.Crop(cvMat, ROI);
        }

        /// <summary>
        /// 이미지를 반전시킨다.
        /// </summary>
        /// <param name="flipType">반전시킬 방향</param>
        /// <returns>결과 이미지</returns>
        public Mat Flip(KFlipType flipType)
        {
            return KImageCV.Flip(cvMat, flipType);
        }

        /// <summary>
        /// 이미지를 회전시킨다.
        /// </summary>
        /// <param name="rotateType">회전시킬 방향</param>
        /// <returns>결과 이미지</returns>
        public Mat Rotate(KRotateType rotateType)
        {
            return KImageCV.Rotate(cvMat, rotateType);
        }
    }
}
