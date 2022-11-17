using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.DIP
{
    /// <summary>
    /// 이미지 처리에 사용되는 전반적인 기능을 제공하는 클래스
    /// </summary>
    public partial class KImage
    {
        public static Image GetThumbnailImage(Image img, int width, int height)
        {
            return img.GetThumbnailImage(width, height, null, IntPtr.Zero);
        }

        public static void SetGrayscalePalette(Bitmap bmp)
        {
            if (bmp.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new NotSupportedException("8bit 이미지만 적용됩니다.");

            using (Bitmap bmpPalette = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                ColorPalette palette = bmpPalette.Palette;
                Color[] entries = palette.Entries;

                int len = byte.MaxValue;
                for (byte i = 0; i < len; i++)
                {
                    Color val = new Color();
                    val = Color.FromArgb(i, i, i);
                    entries[i] = val;
                }

                bmp.Palette = palette;
            }
        }

        public static ColorPalette GetGrayscalePalette()
        {
            var bmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed);
            SetGrayscalePalette(bmp);

            return bmp.Palette;
        }


        //public static Bitmap ConvertToBitmap(ushort[] rawData, int width, int height)
        //{
        //    Bitmap bmp = new Bitmap(width, height, PixelFormat.Format16bppGrayScale);
        //    BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),
        //        ImageLockMode.WriteOnly, PixelFormat.Format16bppGrayScale);

        //IntPtr startPtr = bmpData.Scan0;
        //IntPtr dstRowPtr = startPtr;
        //int offset = 0;
        //int rowStartIndex = 0;

        //    if (width % 4 != 0)
        //        offset = 4 - (width % 4);

        //    //width가 4의 배수일때
        //    if (offset == 0)
        //    {
        //        int len = rawData.Length;
        //        int[] tempRawData = new int[len];

        //        /*for (int i = 0; i < len; i++)
        //            tempRawData[i] = rawData[i];*/

        //        //Marshal.Copy(tempRawData, rowStartIndex, startPtr, len);
        //        //Marshal.Copy(rawData, rowStartIndex, startPtr, rawData.Length);

        //        //Buffer.BlockCopy(rawData, 0, tempRawData, 0, rawData.Length * sizeof(ushort));
        //        Array.Copy(rawData, tempRawData, len);
        //        //Marshal.Copy(tempRawData, rowStartIndex, startPtr, len);
        //    }
        //    else
        //    {
        //        /*unsafe
        //         {
        //             for (int y = 0; y < height; y++)
        //             {
        //                 rowStartIndex = y * width;
        //                 dstRowPtr = startPtr + y * width;

        //                 //Marshal.Copy(rawData, rowStartIndex, dstRowPtr, width);
        //                 Buffer.BlockCopy(rawData, 0, tempRawData, 0, rawData.Length);

        //                 startPtr += offset;
        //             }
        //         }*/
        //    }

        //    //SetGrayscalePalette(bmp);
        //    bmp.UnlockBits(bmpData);
        //    //bmp.Save(@"d:\ushortSaveTest.bmp");

        //    return bmp;
        //}

        public static Bitmap ConvertToBitmap(byte[] rawData, int width, int height)
        {
            //처리 속도 때문에 컴파일러 내부에서는 4의 배수로 영상을 처리한다. 
            //따라서 width가 4의 배수가 아닐때는 
            //다음 행으로 이동 하기 전에 4의 배수에서 차이나는만큼 포인터를 이동시켜서 처리해야 한다.

            //------------------------------------예시 및 설명-----------------------------------------
            //width가 1026일때 컴파일러 내부에서는 width보다 큰 바로다음 4의 배수인 1028로 처리(bmpData의 stride로 알수있다.)
            //따라서 다음 행 이동전에 포인터를 이동 시켜야할 오프셋은 아래와 같이 구한다
            //offset = bmpData.Stride - width;
            //-----------------------------------------------------------------------------------------

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            SetGrayscalePalette(bmp);

            IntPtr startPtr = bmpData.Scan0;
            IntPtr dstRowPtr = startPtr;
            int offset = 0;
            int rowStartIndex = 0;

            if (width % 4 != 0)
                offset = 4 - (width % 4);

            //width가 4의 배수일때
            if (offset == 0)
            {
                Marshal.Copy(rawData, rowStartIndex, startPtr, rawData.Length);
            }
            else
            {
                unsafe
                {
                    for (int y = 0; y < height; y++)
                    {
                        rowStartIndex = y * width;
                        dstRowPtr = startPtr + y * width;

                        Marshal.Copy(rawData, rowStartIndex, dstRowPtr, width);

                        startPtr += offset;
                    }
                }
            }

            bmp.UnlockBits(bmpData);

            return bmp;
        }


        public static byte[] ConvertToByteArray(Bitmap bmp)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            byte[] rstArr = new byte[w * h];

            KLockBitmap lckBmp = new KLockBitmap(bmp);
            lckBmp.LockBits();

            int len = rstArr.Length;
            for (int i = 0; i < len; i++)
                rstArr[i] = lckBmp[i];

            lckBmp.UnlockBits();

            return rstArr;
        }

        public static byte[] ConvertToByteArray(ushort[] pixelData) //매트록스로 변환한 bmp랑 비교했을때 같음을 확인
        {
            int orgLen = pixelData.Length;
            byte[] rstData = new byte[orgLen];

            for (int i = 0; i < orgLen; i++)
                rstData[i] = (byte)(pixelData[i] >> 8);

            return rstData;
        }

        public static ushort[] ConvertToUShortArray(byte[] pixelData) //매트록스로 변환한 bmp랑 비교했을때 같음을 확인
        {
            int orgLen = pixelData.Length;
            ushort[] rstData = new ushort[orgLen];

            for (int i = 0; i < orgLen; i++)
                rstData[i] = (ushort)(pixelData[i] * 257);

            return rstData;
        }


        public static ushort[] Convert2ByteToUShort(byte[] pixelData)
        {
            int len = pixelData.Length;
            ushort[] rstData = new ushort[len / 2];
            ushort temp;

            int dstIdx = 0;
            for (int i = 0; i < len; i += 2)
            {
                temp = BitConverter.ToUInt16(pixelData, i);
                rstData[dstIdx++] = temp;
            }

            return rstData;
        }

        public static byte[] ConvertUSortTo2Byte(ushort[] pixelData)
        {
            int len = pixelData.Length;
            byte[] rstData = new byte[len * 2];
            byte[] temp;

            int dstIdx = 0;
            for (int i = 0; i < len; i++)
            {
                temp = BitConverter.GetBytes(pixelData[i]);

                dstIdx = i * 2;
                rstData[dstIdx] = temp[0];
                rstData[dstIdx + 1] = temp[1];
            }

            return rstData;
        }


        public static ushort[] ConvertToNegative(ushort[] rawData)
        {
            int len = rawData.Length;
            ushort[] rst = new ushort[len];
            ushort max = ushort.MaxValue;

            for (int i = 0; i < len; i++)
            {
                rst[i] = (ushort)(max - rawData[i]);
            }

            return rst;
        }

        public static byte[] ConvertToNegative(byte[] rawData)
        {
            int len = rawData.Length;
            byte[] rst = new byte[len];
            byte max = byte.MaxValue;

            for (int i = 0; i < len; i++)
            {
                rst[i] = (byte)(max - rawData[i]);
            }

            return rst;
        }


        public static int[] LUT_Byte(KArith oper, double operVal)
        {
            int[] rstArr = new int[byte.MaxValue + 1];

            switch (oper)
            {
                case KArith.Add:
                    for (int i = 0; i < rstArr.Length; i++)
                        rstArr[i] = (int)(i + operVal);
                    break;
                case KArith.Sub:
                    for (int i = 0; i < rstArr.Length; i++)
                        rstArr[i] = (int)(i - operVal);
                    break;
                case KArith.Mult:
                    for (int i = 0; i < rstArr.Length; i++)
                        rstArr[i] = (int)(i * operVal);
                    break;
                case KArith.Div:
                    for (int i = 0; i < rstArr.Length; i++)
                        rstArr[i] = (int)(i / operVal);
                    break;
                default:
                    break;
            }

            return rstArr;
        }

        public static int[] LUT_UShort(KArith oper, double operVal)
        {
            int[] rstArr = new int[ushort.MaxValue + 1];

            switch (oper)
            {
                case KArith.Add:
                    for (int i = 0; i < rstArr.Length; i++)
                        rstArr[i] = (int)(i + operVal);
                    break;
                case KArith.Sub:
                    for (int i = 0; i < rstArr.Length; i++)
                        rstArr[i] = (int)(i - operVal);
                    break;
                case KArith.Mult:
                    for (int i = 0; i < rstArr.Length; i++)
                        rstArr[i] = (int)(i * operVal);
                    break;
                case KArith.Div:
                    for (int i = 0; i < rstArr.Length; i++)
                        rstArr[i] = (int)(i / operVal);
                    break;
                default:
                    break;
            }

            return rstArr;
        }

        /// <summary>
        /// 룩업 테이블을 구하는 함수
        /// </summary>
        /// <typeparam name="T">룩업 테이블을 구할 데이터 타입 (byte, ushort)</typeparam>
        /// <param name="oper">구할 테이블의 연산 (+, -, *, /)</param>
        /// <param name="operVal">연산에 사용할 값</param>
        /// <returns></returns>
        public static int[] LUT<T>(KArith oper, double operVal)
        {
            var type = typeof(T);
            int[] rstArr;

            if (type == typeof(ushort))
                rstArr = new int[ushort.MaxValue + 1];
            else if (type == typeof(byte))
                rstArr = new int[byte.MaxValue + 1];
            else
                throw new FormatException
                    ("KImgae.General (LUT(...)): T는 ushort, byte만 가능합니다.");


            switch (oper)
            {
                case KArith.Add:
                    for (int i = 0; i < rstArr.Length; i++)
                        rstArr[i] = (int)(i + operVal); // (T)Convert.ChangeType((int)(i + operVal), typeof(T));
                    break;
                case KArith.Sub:
                    for (int i = 0; i < rstArr.Length; i++)
                        rstArr[i] = (int)(i - operVal);
                    break;
                case KArith.Mult:
                    for (int i = 0; i < rstArr.Length; i++)
                        rstArr[i] = (int)(i * operVal);
                    break;
                case KArith.Div:
                    for (int i = 0; i < rstArr.Length; i++)
                        rstArr[i] = (int)(i / operVal);
                    break;
                default:
                    break;
            }

            return rstArr;
        }

        public static void LUT<T, U>(T[] srcData, T[] dstData, U[] lutData)
        {
            int len = srcData.Length;
            for (int i = 0; i < len; i++)
            {
                int val = Convert.ToInt32(srcData[i]);
                dstData[i] = (T)Convert.ChangeType(lutData[val], typeof(T));
            }
        }

        public static void LUT<T, U>(T[,,] srcData, T[,,] dstData, U[] lutData)
        {
            int hLen = srcData.GetLength(0), wLen = srcData.GetLength(1), cLen = srcData.GetLength(2);

            for (int h = 0; h < hLen; h++)
                for (int w = 0; w < wLen; w++)
                    for (int c = 0; c < cLen; c++)
                    {
                        int val = Convert.ToInt32(srcData[h, w, c]);
                        dstData[h, w, c] = (T)Convert.ChangeType(lutData[val], typeof(T));
                    }
        }


        /// <summary>
        /// 칼라 이미지의 1차원 Data배열을 각각 R, G, B 채널 배열로 분리하는 함수
        /// </summary>
        /// <param name="data">분류할 Data배열</param>
        /// <param name="width">이미지의 가로</param>
        /// <param name="height">이미지의 높이</param>
        /// <param name="R">Red채널로 분류할 배열</param>
        /// <param name="G">Green채널로 분류할 배열</param>
        /// <param name="B">Blue채널로 분류할 배열</param>
        /// <param name="orderIsRGB">True이면 RGB순서, false이면 BGR순서</param>
        public static void SeperateColorChannels(byte[] data, int width, int height,
           out byte[] R, out byte[] G, out byte[] B, bool orderIsRGB = true)
        {
            //SeperateColorChannels<byte>(data, width, height, out R, out G, out B, orderIsRGB);
            int len = data.Length;

            //채널이 3개인데 3의 배수가 아니라면
            if (len % 3 != 0)
                throw new ArgumentException("KImage.General - SeperateEachColorChannels(..): 원본 배열이 3의 배수가 아닙니다. 칼라 배열인지 확인하십시오.");

            R = new byte[len / 3];
            G = new byte[len / 3];
            B = new byte[len / 3];

            int stride = width * 3;
            int clrCnt = 0;

            for (int i = 0; i < len; i += 3)
            {
                if (orderIsRGB)
                {
                    R[clrCnt] = data[i];
                    G[clrCnt] = data[i + 1];
                    B[clrCnt] = data[i + 2];
                }
                else
                {
                    B[clrCnt] = data[i];
                    G[clrCnt] = data[i + 1];
                    R[clrCnt] = data[i + 2];
                }

                clrCnt++;
            }
        }

        /// <summary>
        /// Dcm(Dicom) 이미지에서 쓸용도로 만든 함수, 여러개의 프레임으로 이루어져도 Data는 1차원배열 하나에 들어가 있는데,
        /// 이를 프레임 단위로 분리해 List배열객체로 리턴한다.
        /// </summary>
        /// <param name="data">픽셀 데이터 배열</param>
        /// <param name="width">이미지의 가로길이</param>
        /// <param name="height">이미지의 세로길이</param>
        /// <param name="numberOfChannels">한 픽셀이 몇개의 비트로 이루어 져있는가,
        /// Ex)8비트 흑백은 1, 16비트 흑백은 1, 24비트 칼라는 3</param>
        /// <returns>분리된 데이터 List</returns>
        public static List<byte[]> DivideFrames(byte[] data, int width, int height, int numberOfChannels)
        {
            List<byte[]> rtDataList = new List<byte[]>();
            int start = 0, end = 0;
            int pixelCntEachFrame = width * height;
            int numOfFrame = width * height * numberOfChannels;

            for (int i = 0; i < numOfFrame; i++)
            {
                start = i * pixelCntEachFrame * numberOfChannels;
                end = start + pixelCntEachFrame * numberOfChannels;

                byte[] tempData8bit = new byte[pixelCntEachFrame * numberOfChannels];
                Array.Copy(data, start, tempData8bit, 0, pixelCntEachFrame * numberOfChannels);

                rtDataList.Add(tempData8bit);
            }

            return rtDataList;
        }

        /// <summary>
        /// Dcm(Dicom) 이미지에서 쓸용도로 만든 함수, 여러개의 프레임으로 이루어져도 Data는 1차원배열 하나에 들어가 있는데,
        /// 이를 프레임 단위로 분리해 List배열객체로 리턴한다.
        /// </summary>
        /// <param name="data">픽셀 데이터 배열</param>
        /// <param name="width">이미지의 가로길이</param>
        /// <param name="height">이미지의 세로길이</param>
        /// <param name="numberOfChannels">한 픽셀이 몇개의 비트로 이루어 져있는가,
        /// Ex)8비트 흑백은 1, 16비트 흑백은 1, 24비트 칼라는 3</param>
        /// <returns>분리된 데이터 List</returns>
        public static List<ushort[]> DivideFrames(ushort[] data, int width, int height, int numberOfChannels)
        {
            List<ushort[]> rtDataList = new List<ushort[]>();
            int start = 0, end = 0;
            int pixelCntEachFrame = width * height;
            int numOfFrame = width * height * numberOfChannels;

            for (int i = 0; i < numOfFrame; i++)
            {
                start = i * pixelCntEachFrame * numberOfChannels;
                end = start + pixelCntEachFrame * numberOfChannels;

                ushort[] tempData8bit = new ushort[pixelCntEachFrame * numberOfChannels];
                Array.Copy(data, start, tempData8bit, 0, pixelCntEachFrame * numberOfChannels);

                rtDataList.Add(tempData8bit);
            }

            return rtDataList;
        }

        public static byte[] GetFrameData(byte[] data, int width, int height, int numberOfChannels, int frameNumberToGet)
        {
            byte[] rtDataList = new byte[width * height * numberOfChannels];
            int start = 0, end = 0;
            int pixelCntEachFrame = width * height;

            start = frameNumberToGet * pixelCntEachFrame * numberOfChannels;
            end = start + pixelCntEachFrame * numberOfChannels;

            Array.Copy(data, start, rtDataList, 0, pixelCntEachFrame * numberOfChannels);

            return rtDataList;
        }

        public static ushort[] GetFrameData(ushort[] data, int width, int height, int numberOfChannels, int frameNumberToGet)
        {
            ushort[] rtDataList = new ushort[width * height * numberOfChannels];
            int start = 0, end = 0;
            int pixelCntEachFrame = width * height;

            start = frameNumberToGet * pixelCntEachFrame * numberOfChannels;
            end = start + pixelCntEachFrame * numberOfChannels;

            Array.Copy(data, start, rtDataList, 0, pixelCntEachFrame * numberOfChannels);

            return rtDataList;
        }


        /// <summary>
        /// Dcm(Dicom) 이미지에서 쓸용도로 만든 함수.
        /// Data배열의 Channel을 RGB면 BGR, BGR이면 RGB순서로 변경(Reverse)한다.
        /// </summary>
        /// <typeparam name="T">data배열의 타입</typeparam>
        /// <param name="data">원본 데이터</param>
        /// <returns>Channel을 Reverse시킨 Data배열</returns>
        public static T[,,] ReverseRGBChannel<T>(T[,,] data)
        {
            int hLen = data.GetLength(0), wLen = data.GetLength(1), cLen = data.GetLength(2);
            T[,,] rtArr = data.Clone() as T[,,];
            T[,,] orgArr = data.Clone() as T[,,];

            if (cLen != 3)
                throw new Exception("KImgae.General ReverseRGBChannel(..): data의 채널은 3이여야 합니다");

            dynamic temp;
            for (int h = 0; h < hLen; h++)
                for (int w = 0; w < wLen; w++)
                {
                    temp = orgArr[h, w, 0];
                    orgArr[h, w, 0] = rtArr[h, w, 2];
                    rtArr[h, w, 2] = temp;

                    temp = orgArr[h, w, 2];
                    orgArr[h, w, 2] = rtArr[h, w, 0];
                    rtArr[h, w, 0] = temp;
                }

            return rtArr;
        }


        // 속도 느림
        public static U[] Convert3Dto1D<T, U>(T[,,] data)
        {
            int hL = data.GetLength(0);
            int wL = data.GetLength(1);
            int cL = data.GetLength(2);
            int cnt = 0;
            U[] rtArr = new U[hL * wL * cL];

            for (int h = 0; h < hL; h++)
                for (int w = 0; w < wL; w++)
                    for (int c = 0; c < cL; c++)
                    {
                        rtArr[cnt++] = (U)Convert.ChangeType(data[h, w, c], typeof(U));
                    }

            return rtArr;
        }

        public static T[] Convert3Dto1D<T>(T[,,] data)
        {
            int len = data.Length;
            int cnt = len * Marshal.SizeOf(default(T));
            T[] rtArr = new T[len];

            Buffer.BlockCopy(data, 0, rtArr, 0, cnt);

            return rtArr;
        }


        public static U[,,] Convert1Dto3D<T, U>(T[] data, int width, int height, KColorType clr)
        {
            int hLen = height, wLen = width;
            int cLen = (clr == KColorType.Gray) ? 1 : 3;
            int cnt = 0;
            U[,,] rtArr = new U[hLen, wLen, cLen];

            if (clr == KColorType.Gray)
            {
                for (int h = 0; h < hLen; h++)
                    for (int w = 0; w < wLen; w++)
                    {
                        rtArr[h, w, 0] = (U)Convert.ChangeType(data[cnt++], typeof(U));
                    }
            }
            else
            {
                for (int h = 0; h < hLen; h++)
                    for (int w = 0; w < wLen; w++)
                        for (int c = 0; c < cLen; c++)
                        {
                            rtArr[h, w, c] = (U)Convert.ChangeType(data[cnt++], typeof(U));
                        }
            }

            return rtArr;
        }

        public static U[] ConvertType<T, U>(T[] data)
        {
            int len = data.Length;
            U[] rtArr = new U[len];

            for (int i = 0; i < len; i++)
                rtArr[i] = (U)Convert.ChangeType(data[i], typeof(U));

            return rtArr;
        }

        public static U[,,] ConvertType<T, U>(T[,,] data)
        {
            int hLen = data.GetLength(0);
            int wLen = data.GetLength(1);
            int cLen = data.GetLength(2);
            U[,,] rtArr = new U[hLen, wLen, cLen];

            for (int h = 0; h < hLen; h++)
                for (int w = 0; w < wLen; w++)
                    for (int c = 0; c < cLen; c++)
                    {
                        rtArr[h, w, c] = (U)Convert.ChangeType(data[h, w, c], typeof(U));
                    }

            return rtArr;
        }


        /// <summary>
        /// param보다 큰 4의 배수를 찾는다. (ex) param = 510, return 512)
        /// param이 4의 배수일 경우 param값을 그대로 리턴한다.
        /// 이미지의 width가 4의 배수가 아닐경우 offset맞춰줄 용도로 사용한다.
        /// </summary>
        /// <param name="param">4의 배수를 구할 원본 값</param>
        /// <returns>가장 가까운 4의 배수</returns>
        public static int FindMultiplesOf4GreaterThanParam(int param)
        {
            int rt = 0, quotient = 0, remainder = 0;

            remainder = param % 4;
            if (remainder == 0)
                return param;

            quotient = param / 4;
            rt = quotient * 4 + 4;

            return rt;
        }
    }
}
