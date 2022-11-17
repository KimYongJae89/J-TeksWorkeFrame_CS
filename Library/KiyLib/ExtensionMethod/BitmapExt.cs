using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.ExtensionMethod
{
    /// <summary>
    /// Bitmap의 확장메서드 구현을 위한 클래스
    /// </summary>
    public static class BitmapExt
    {
        /// <summary>
        /// Bitmap객체를 만든다
        /// </summary>
        /// <param name="bmp">원본 Bitmap객체</param>
        /// <param name="imgData">픽셀 데이터</param>
        /// <returns>결과 값</returns>
        public static Bitmap CreateByPixelData(this Bitmap bmp, byte[] imgData)
        {
            int w = bmp.Width;
            int h = bmp.Height;

            return KImage.ConvertToBitmap(imgData, w, h);
        }

        /// <summary>
        /// Bitmap객체를 만든다
        /// </summary>
        /// <param name="bmp">원본 Bitmap객체</param>
        /// <param name="imgData">픽셀 데이터</param>
        /// <returns>결과 값</returns>
        public static Bitmap CreateByPixelData(this Bitmap bmp, ushort[] imgData)
        {
            return CreateByPixelData(bmp, imgData, 0, ushort.MaxValue);
        }

        /// <summary>
        /// Bitmap객체를 만든다
        /// </summary>
        /// <param name="bmp">원본 Bitmap객체</param>
        /// <param name="imgData">픽셀 데이터</param>
        /// <param name="histoMin">레벨링에 사용할 최소값</param>
        /// <param name="histoMax">레벨링에 사용할 최대값</param>
        /// <returns>결과 값</returns>
        public static Bitmap CreateByPixelData(this Bitmap bmp, ushort[] imgData, int histoMin, int histoMax)
        {
            int w = bmp.Width;
            int h = bmp.Height;

            byte[] rstBuffer = new byte[imgData.Length];

            KHistogram.WndLvlTranform(rstBuffer, imgData, histoMin, histoMax);

            return KImage.ConvertToBitmap(rstBuffer, w, h);
        }
    }
}
