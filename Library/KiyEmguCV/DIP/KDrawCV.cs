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
    /// <summary>
    /// Draw에 관련된 기능을 가진 클래스
    /// </summary>
    public class KDrawCV
    {
        /// <summary>
        /// 이미지에 문자열을 그린다
        /// </summary>
        /// <param name="srcMat">문자열을 그릴 대상 Mat객체</param>
        /// <param name="txt">그릴 문자열</param>
        /// <param name="location">문자열을 그릴 위치</param>
        /// <param name="clr">문자열의 색상</param>
        /// <param name="fontScale">문자열의 크기</param>
        /// <param name="thickness">문자열의 두께</param>
        public static void DrawText(Mat srcMat, string txt, Point location, KColor clr, double fontScale = 0.4, int thickness = 1)
        {
            var clrScr = new Emgu.CV.Structure.MCvScalar(clr.B, clr.G, clr.R);

            CvInvoke.PutText(srcMat, txt, location,
              Emgu.CV.CvEnum.FontFace.HersheyScriptSimplex,
              fontScale, clrScr, 1);
        }

        /// <summary>
        /// 이미지에 문자열을 그린다
        /// 문자열의 색상은 검은색으로 고정
        /// </summary>
        /// <param name="srcMat">문자열을 그릴 대상 Mat객체</param>
        /// <param name="txt">그릴 문자열</param>
        /// <param name="location">문자열을 그릴 위치</param>
        /// <param name="fontScale">문자열의 크기</param>
        /// <param name="thickness">문자열의 두께</param>
        public static void DrawText(Mat srcMat, string txt, Point location, double fontScale = 0.4, int thickness = 1)
        {
            DrawText(srcMat, txt, location, new KColor(), fontScale, thickness);
        }
    }
}
