using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyEmguCV.DIP
{
    /// <summary>
    /// Polygon(다각형)에 대한 기능을 가진 클래스
    /// </summary>
    public class KPolygonCV
    {
        /// <summary>
        /// 다각형의 면적을 구한다
        /// 인자로 전달받는 Point 배열을 순서대로 이었을때 완성되는 다각형의 면적을 구한다
        /// </summary>
        /// <param name="PolygonPts">다각형을 이루는 Point들 배열의 순서대로 다각형을 그린다고 가정한다</param>
        /// <returns>결과 값</returns>
        public static double CalcArea(Point[] PolygonPts)
        {
            using (VectorOfPoint vctPts = new VectorOfPoint(PolygonPts))
            {
                return CvInvoke.ContourArea(vctPts);
            }
        }
    }
}
