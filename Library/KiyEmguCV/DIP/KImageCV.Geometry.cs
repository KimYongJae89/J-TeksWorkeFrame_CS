using Emgu.CV;
using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace KiyEmguCV.DIP
{
    public partial class KImageCV
    {
        /// <summary>
        /// 이미지 크기를 변경한다
        /// </summary>
        /// <param name="mat">원본 이미지 Mat객체</param>
        /// <param name="scale">변경할 비율</param>
        /// <param name="interType">사용할 보간법</param>
        /// <returns>Resize된 이미지 Mat객체</returns>
        public static Mat Resize(Mat mat, double scale, KInterpolation interType = KInterpolation.Linear)
        {
            return Resize(mat, scale, scale, interType);
        }

        /// <summary>
        /// 이미지 크기를 변경한다
        /// </summary>
        /// <param name="mat">원본 이미지 Mat객체</param>
        /// <param name="widthScale">변경할 가로비율</param>
        /// <param name="heightScale">변경할 세로비율</param>
        /// <param name="interType">사용할 보간법</param>
        /// <returns>Resize된 이미지 Mat객체</returns>
        public static Mat Resize(Mat mat, double widthScale, double heightScale, KInterpolation interType = KInterpolation.Linear)
        {
            Mat cvtMat = new Mat();
            double wScal = widthScale;
            double hScal = heightScale;

            CvInvoke.Resize(mat, cvtMat, new Size(0, 0), wScal, hScal, (Emgu.CV.CvEnum.Inter)(int)interType);

            return cvtMat;
        }

        /// <summary>
        /// 이미지의 특정영역을 잘라낸다
        /// </summary>
        /// <param name="mat">원본 이미지 Mat객체</param>
        /// <param name="ROI">잘라낼 영역</param>
        /// <returns>결과 이미지</returns>
        public static Mat Crop(Mat mat, Rectangle ROI)
        {
            Mat destMat = new Mat(mat, ROI);
            return destMat;
        }

        /// <summary>
        /// 이미지를 반전시킨다
        /// </summary>
        /// <param name="mat">원본 이미지 Mat객체</param>
        /// <param name="flipType">반전시킬 방향</param>
        /// <returns>결과 이미지</returns>
        public static Mat Flip(Mat mat, KFlipType flipType)
        {
            Mat cvtMat = new Mat();
            CvInvoke.Flip(mat, cvtMat, (Emgu.CV.CvEnum.FlipType)(int)flipType);

            return cvtMat;
        }

        /// <summary>
        /// 이미지를 회전시킨다
        /// </summary>
        /// <param name="mat">원본 이미지 Mat객체</param>
        /// <param name="rotateType">회전시킬 방향</param>
        /// <returns>결과 이미지</returns>
        public static Mat Rotate(Mat mat, KRotateType rotateType)
        {
            int num = (int)rotateType;

            if (num == 3)
                num = 2;
            else if (num == 4)
                num = 1;
            else if (num == 5)
                num = 0;

            Mat cvtMat = new Mat();
            CvInvoke.Rotate(mat, cvtMat, (Emgu.CV.CvEnum.RotateFlags)num);

            return cvtMat;
        }
    }
}
