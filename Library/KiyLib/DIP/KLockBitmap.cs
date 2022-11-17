using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.DIP
{
    /// <summary>
    /// Bitmap에 관련된 연산을 위한 클래스
    /// </summary>
    public class KLockBitmap
    {
        private Bitmap _bmp = null;
        private IntPtr _addrOfFirstPixel = IntPtr.Zero;
        private BitmapData _bitmapData = null;
        private int stride = -1;    //인덱서 get, set할때 프로퍼티 접근시간 줄이려고 bitmapData.Stride대신 사용, 이 용도로만 씀
        private byte[] _pixels;

        /// <summary>
        /// Lock 상태
        /// </summary>
        public bool IsLocked { get; private set; }

        /// <summary>
        /// Depth(bit)
        /// </summary>
        public int Depth { get; private set; }

        /// <summary>
        /// 가로 길이
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// 세로 길이
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// 픽셀 배열
        /// </summary>
        public byte[] Pixels
        {
            get { return _pixels; }
            private set { _pixels = value; }
        }

        /// <summary>
        /// 픽셀 데이터
        /// </summary>
        /// <param name="y">y 좌표</param>
        /// <param name="x">x 좌표</param>
        /// <returns>결과 값</returns>
        public byte this[int y, int x]
        {
            get
            {
                if (!IsLocked)
                    throw new Exception("현재 Lock상태가 아닙니다.");

                return Pixels[y * stride + x];
            }
            set
            {
                if (!IsLocked)
                    throw new Exception("현재 Lock상태가 아닙니다.");

                Pixels[y * stride + x] = value;
            }
        }

        /// <summary>
        /// 픽셀 데이터
        /// </summary>
        /// <param name="index">데이터의 index</param>
        /// <returns>결과 값</returns>
        public byte this[int index]
        {
            get
            {
                if (!IsLocked)
                    throw new Exception("현재 Lock상태가 아닙니다.");

                return Pixels[index];
            }
            set
            {
                if (!IsLocked)
                    throw new Exception("현재 Lock상태가 아닙니다.");

                Pixels[index] = value;
            }
        }

        /// <summary>
        /// 첫번째 픽셀의 주소
        /// </summary>
        public IntPtr AddrOfFirstPixel
        {
            get { return _addrOfFirstPixel; }
            private set { _addrOfFirstPixel = value; }
        }


        public KLockBitmap(Bitmap bmp)
        {
            this._bmp = bmp;
        }

        public KLockBitmap(string bmpPath)
        {
            Bitmap bmp = new Bitmap(bmpPath);
            this._bmp = bmp;
        }


        /// <summary>
        /// 이미지를 Lock상태로 전환. Lock상태에서만 포인터 연산이 가능하다.
        /// </summary>
        public void LockBits()
        {
            try
            {
                if (this._bmp == null)
                {
                    new Exception("Bmp is Null!");
                }

                Width = _bmp.Width;
                Height = _bmp.Height;

                int PixelCount = Width * Height;

                Rectangle rect = new Rectangle(0, 0, Width, Height);

                Depth = System.Drawing.Bitmap.GetPixelFormatSize(_bmp.PixelFormat);

                if (Depth != 8 && Depth != 16 && Depth != 24 && Depth != 32)
                {
                    throw new ArgumentException("Only 8, 16, 24 and 32 bpp images are supported.");
                }

                _bitmapData = _bmp.LockBits(rect, ImageLockMode.ReadWrite, _bmp.PixelFormat);
                stride = _bitmapData.Stride;
                IsLocked = true;

                int step = Depth / 8;
                Pixels = new byte[PixelCount * step];
                _addrOfFirstPixel = _bitmapData.Scan0;

                Marshal.Copy(_addrOfFirstPixel, Pixels, 0, Pixels.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// 이미지를 Unlock상태로 전환. LockBits()을 실행한뒤에 반드시 실행해줘야한다.
        /// </summary>
        public void UnlockBits()
        {
            try
            {
                Marshal.Copy(Pixels, 0, _addrOfFirstPixel, Pixels.Length);
                _bmp.UnlockBits(_bitmapData);

                IsLocked = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
