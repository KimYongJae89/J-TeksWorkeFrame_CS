using Emgu.CV;
using Emgu.CV.Structure;
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
        /// 지정한 산술 연산을 실행한다
        /// </summary>
        /// <param name="src">대상 이미지의 Mat객체</param>
        /// <param name="val">연산에 사용할 값</param>
        /// <param name="oper">실행할 연산의 종류</param>
        /// <returns>연산을 실행한 Mat객체</returns>
        public static Mat Arith(Mat src, double val, KArith oper)
        {
            Mat src2 = new Mat(src.Clone(), new Rectangle(0, 0, src.Width, src.Height));

            if (src2.NumberOfChannels == 1)
                src2.SetTo(new MCvScalar(val));
            if (src2.NumberOfChannels == 3)
                src2.SetTo(new MCvScalar(val, val, val));

            return Arith(src, src2, oper);
        }

        /// <summary>
        /// 지정한 산술 연산을 실행한다
        /// 두개의 Mat객체에 특정 산술연산을 실행하여 결과를 구한다
        /// </summary>
        /// <param name="src1"></param>
        /// <param name="src2"></param>
        /// <param name="oper"></param>
        /// <returns>연산을 실행한 Mat객체</returns>
        public static Mat Arith(Mat src1, Mat src2, KArith oper)
        {
            Mat rtMat = new Mat(src1.Clone(), new Rectangle(0, 0, src1.Width, src1.Height));

            switch (oper)
            {
                case KArith.Add:
                    CvInvoke.Add(src1, src2, rtMat);
                    break;
                case KArith.Sub:
                    CvInvoke.Subtract(src1, src2, rtMat);
                    break;
                case KArith.Mult:
                    CvInvoke.Multiply(src1, src2, rtMat);
                    break;
                case KArith.Div:
                    CvInvoke.Divide(src1, src2, rtMat);
                    break;
            }

            return rtMat;
        }
    }
}
