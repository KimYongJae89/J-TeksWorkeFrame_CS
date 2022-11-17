using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.DIP
{
    /// <summary>
    /// 히스토그램 연산을 위한 클래스
    /// </summary>
    public class KHistogram
    {
        /// <summary>
        /// 히스토그램 배열을 구한다
        /// </summary>
        /// <param name="bmp">대상 이미지</param>
        /// <returns>결과 값</returns>
        public static int[] GetHistoArr(Bitmap bmp)
        {
            KLockBitmap lb = new KLockBitmap(bmp);
            return GetHistoArr(lb);
        }

        /// <summary>
        /// 히스토그램 배열을 구한다
        /// </summary>
        /// <param name="lckBmp">대상 이미지</param>
        /// <returns>결과 값</returns>
        public static int[] GetHistoArr(KLockBitmap lckBmp)
        {
            int arrLen = byte.MaxValue + 1;
            int[] histoArr = new int[arrLen];

            lckBmp.LockBits();

            byte gv = 0;
            for (int i = 0; i < lckBmp.Height; i++)
            {
                for (int j = 0; j < lckBmp.Width; j++)
                {
                    gv = lckBmp[i, j];
                    histoArr[gv]++;
                }
            }

            lckBmp.UnlockBits();

            return histoArr;
        }

        /// <summary>
        /// 16bit 이미지 배열의 히스토그램(밝기 분포)을 계산하여 반환
        /// </summary>
        /// <param name="imgArr">원본 16bit 이미지 배열</param>
        /// <returns>히스토그램(밝기 분포) 계산 결과</returns>
        public static int[] GetHistoArr(ushort[] imgArr)
        {
            int arrLen = ushort.MaxValue + 1;
            int[] rtArr = new int[arrLen];

            int gv = 0;
            for (int i = 0; i < imgArr.Length; i++)
            {
                gv = imgArr[i];
                rtArr[gv]++;
            }

            return rtArr;
        }

        /// <summary>
        /// 8bit 이미지 배열의 히스토그램(밝기 분포)을 계산하여 반환
        /// </summary>
        /// <param name="imgArr">원본 8bit 이미지 배열</param>
        /// <returns>히스토그램(밝기 분포) 계산 결과</returns>
        public static int[] GetHistoArr(byte[] imgArr)
        {
            int arrLen = byte.MaxValue + 1;
            int[] rtArr = new int[arrLen];

            for (int i = 0; i < imgArr.Length; i++)
            {
                int gv = imgArr[i];
                rtArr[gv]++;
            }

            return rtArr;
        }

        /// <summary>
        /// 16bit 이미지배열에서 특정 ROI영역의 히스토그램(밝기 분포)을 계산하여 반환
        /// </summary>
        /// <param name="imgArr">원본 16bit 이미지 배열</param>
        /// <param name="width">원본 이미지의 가로 길이</param>
        /// <param name="region">히스토그램을 계산할 ROI영역</param>
        /// <returns>히스토그램(밝기 분포) 계산 결과</returns>
        public static int[] GetHistoArr(ushort[] imgArr, int width, Rectangle region)
        {
            var usArr = KRectangle.GetRegionArr(imgArr, width, region);
            return GetHistoArr(usArr);
        }

        /// <summary>
        /// 히스토그램 배열을 구한다 (EmguCV에서 사용할 의도로 만듦)
        /// </summary>
        /// <param name="imgArr">이미지의 데이터 배열(KImageCV의 Data)</param>
        /// <param name="depth">이미지의 depth (KImageCV의 DepthType)</param>
        /// <returns>리턴 형식은 [histoArr, Channel(BGR순서)] 흑백 이미지일 경우 Channel길이는 1</returns>
        public static int[,] GetHistoArr(dynamic[,,] imgArr, KDepthType depth)
        {
            int hLen, wLen, cLen, gv;
            int[,] rtArr;
            hLen = imgArr.GetLength(0);
            wLen = imgArr.GetLength(1);
            cLen = imgArr.GetLength(2);

            //Gray
            if (cLen == 1)
            {
                rtArr = new int[(int)Math.Pow(2, (int)depth / cLen), cLen];

                for (int h = 0; h < hLen; h++)
                    for (int w = 0; w < wLen; w++)
                    {
                        //***- gv = imgArr[h, w, 0];

                        gv = 0;
                        rtArr[gv, 0]++;
                    }
            }
            //Color
            else if (cLen == 3)
            {
                rtArr = new int[(int)Math.Pow(2, (int)depth / cLen), cLen];

                for (int h = 0; h < hLen; h++)
                    for (int w = 0; w < wLen; w++)
                        for (int c = 0; c < cLen; c++)
                        {
                            gv = imgArr[h, w, c];
                            rtArr[gv, c]++;
                        }
            }
            else
                throw new Exception("채널은 1 혹은 3만 가능");

            return rtArr;
        }

        /// <summary>
        /// 히스토그램 배열을 구한다 (EmguCV에서 사용할 의도로 만듦)
        /// </summary>
        /// <typeparam name="T">픽셀 데이터의 데이터 타입</typeparam>
        /// <param name="imgArr">이미지의 데이터 배열(KImageCV의 Data)</param>
        /// <param name="depth">이미지의 depth (KImageCV의 DepthType)</param>
        /// <returns>리턴 형식은 [histoArr, Channel(BGR순서)] 흑백 이미지일 경우 Channel길이는 1</returns>
        public static int[,] GetHistoArr<T>(T[,,] imgArr, KDepthType depth)
        {
            int hLen, wLen, cLen, gv;
            int[,] rtArr;
            hLen = imgArr.GetLength(0);
            wLen = imgArr.GetLength(1);
            cLen = imgArr.GetLength(2);

            //Gray
            if (cLen == 1)
            {
                rtArr = new int[(int)Math.Pow(2, (int)depth / cLen), cLen];

                for (int h = 0; h < hLen; h++)
                    for (int w = 0; w < wLen; w++)
                    {
                        gv = (int)Convert.ChangeType(imgArr[h, w, 0], typeof(int));
                        rtArr[gv, 0]++;
                    }
            }
            //Color
            else if (cLen == 3)
            {
                rtArr = new int[(int)Math.Pow(2, (int)depth / cLen), cLen];

                for (int h = 0; h < hLen; h++)
                    for (int w = 0; w < wLen; w++)
                        for (int c = 0; c < cLen; c++)
                        {
                            gv = (int)Convert.ChangeType(imgArr[h, w, c], typeof(int));
                            rtArr[gv, c]++;
                        }
            }
            else
                throw new Exception("채널은 1 혹은 3만 가능");

            return rtArr;
        }



        // 16bit 영상을 8bit영상으로 변활할때 사용, 히스토그램으로 선택된 영역을 256단계의 밝기로 변경
        /// <summary>
        /// 윈도우 레벨링을 실행한다, 특정 영역을 256단계의 밝기로 변경
        /// Min, Max인자 입력시 Min이 Max보다 큰 경우등의 값 입력 오류는 검사하지 않는다
        /// 따라서 인자 입력전에 별도의 검사가 필요하다
        /// </summary>
        /// <param name="dstBuf">변경된 결과 배열</param>
        /// <param name="srcBuf">원본 데이터</param>
        /// <param name="histoSelectedMin">선택할 영역의 최소값</param>
        /// <param name="histoSelectedMax">선택할 영역의 최대값</param>
        public static void WndLvlTranform(byte[] dstBuf, byte[] srcBuf, int histoSelectedMin, int histoSelectedMax)
        {
            int len = srcBuf.Length;
            int min = histoSelectedMin;
            int max = histoSelectedMax;
            double divStep = 256d; /*byte.MaxValue + 1*/
            double div = Math.Abs(max - min) / divStep;

            int pixel = 0, tempArrVal = 0;
            for (int i = 0; i < len; i++)
            {
                tempArrVal = srcBuf[i];

                //min, max가 같을때
                if (div == 0)
                {
                    if (tempArrVal > min)
                        dstBuf[i] = 255;
                    else
                        dstBuf[i] = 0;

                    continue;
                }

                //0일떄
                if (tempArrVal == 0)
                {
                    dstBuf[i] = 0;
                    continue;
                }

                //65535일때 255로 변환
                if (tempArrVal == 65535)
                {
                    dstBuf[i] = 255;
                    continue;
                }

                //실제 히스토그램 선택영역
                pixel = (int)((tempArrVal - min) / div);

                if (pixel > 255) pixel = 255;
                if (pixel < 0) pixel = 0;

                dstBuf[i] = (byte)(pixel);
            }
        }

        /// <summary>
        /// 윈도우 레벨링을 실행한다, 특정 영역을 256단계의 밝기로 변경
        /// Min, Max인자 입력시 Min이 Max보다 큰 경우등의 값 입력 오류는 검사하지 않는다
        /// 따라서 인자 입력전에 별도의 검사가 필요하다
        /// </summary>
        /// <param name="dstBuf">변경된 결과 배열</param>
        /// <param name="srcBuf">원본 데이터</param>
        /// <param name="histoSelectedMin">선택할 영역의 최소값</param>
        /// <param name="histoSelectedMax">선택할 영역의 최대값</param>
        public static void WndLvlTranform(byte[] dstBuf, int[] srcBuf, int histoSelectedMin, int histoSelectedMax)
        {
            int len = srcBuf.Length;
            int min = histoSelectedMin;
            int max = histoSelectedMax;
            double divStep = 256d; /*byte.MaxValue + 1*/
            double div = Math.Abs(max - min) / divStep;

            int pixel = 0, tempArrVal = 0;
            for (int i = 0; i < len; i++)
            {
                tempArrVal = srcBuf[i];

                //min, max가 같을때
                if (div == 0)
                {
                    if (tempArrVal > min)
                        dstBuf[i] = 255;
                    else
                        dstBuf[i] = 0;

                    continue;
                }

                //0일떄
                if (tempArrVal == 0)
                {
                    dstBuf[i] = 0;
                    continue;
                }

                //65535일때 255로 변환
                if (tempArrVal == 65535)
                {
                    dstBuf[i] = 255;
                    continue;
                }

                //실제 히스토그램 선택영역
                pixel = (int)((tempArrVal - min) / div);

                if (pixel > 255) pixel = 255;
                if (pixel < 0) pixel = 0;

                dstBuf[i] = (byte)(pixel);
            }
        }

        /// <summary>
        /// 윈도우 레벨링을 실행한다, 특정 영역을 256단계의 밝기로 변경
        /// Min, Max인자 입력시 Min이 Max보다 큰 경우등의 값 입력 오류는 검사하지 않는다
        /// 따라서 인자 입력전에 별도의 검사가 필요하다
        /// </summary>
        /// <param name="dstBuf">변경된 결과 배열</param>
        /// <param name="srcBuf">원본 데이터</param>
        /// <param name="histoSelectedMin">선택할 영역의 최소값</param>
        /// <param name="histoSelectedMax">선택할 영역의 최대값</param>
        public static void WndLvlTranform(byte[] dstBuf, ushort[] srcBuf, int histoSelectedMin, int histoSelectedMax)
        {
            int len = srcBuf.Length;
            int min = histoSelectedMin;
            int max = histoSelectedMax;
            double divStep = 256d; /*byte.MaxValue + 1*/
            double div = Math.Abs(max - min) / divStep;

            int pixel = 0, tempArrVal = 0;
            for (int i = 0; i < len; i++)
            {
                tempArrVal = srcBuf[i];

                //min, max가 같을때
                if (div == 0)
                {
                    if (tempArrVal > min)
                        dstBuf[i] = 255;
                    else
                        dstBuf[i] = 0;

                    continue;
                }

                //0일떄
                if (tempArrVal == 0)
                {
                    dstBuf[i] = 0;
                    continue;
                }

                //65535일때 255로 변환
                if (tempArrVal == 65535)
                {
                    dstBuf[i] = 255;
                    continue;
                }

                //실제 히스토그램 선택영역
                pixel = (int)((tempArrVal - min) / div);

                if (pixel > 255) pixel = 255;
                if (pixel < 0) pixel = 0;

                dstBuf[i] = (byte)(pixel);
            }
        }


        /// <summary>
        /// 16bit 이미지 배열을 히스토그램 조정하여 반환
        /// </summary>
        /// <param name="imgArr">원본 16bit 이미지 픽셀 배열</param>
        /// <param name="histoSelectedMin">히스토그램의 min 값</param>
        /// <param name="histoSelectedMax">히스토그램의 max 값</param>
        /// <returns>히스토그램 처리된 이미지 배열</returns>
        public static ushort[] ConvertToHisto(ushort[] imgArr, int histoSelectedMin, int histoSelectedMax)
        {
            int len = imgArr.Length;
            ushort maxVal = ushort.MaxValue;
            ushort[] rstBuf = new ushort[len];

            int min = histoSelectedMin;
            int max = histoSelectedMax;
            int gv = 0;

            for (int i = 0; i < len; i++)
            {
                gv = imgArr[i];

                if (gv < min)
                {
                    rstBuf[i] = 0;
                    continue;
                }
                if (gv > max)
                {
                    rstBuf[i] = maxVal;
                    continue;
                }

                rstBuf[i] = imgArr[i];
            }

            return rstBuf;
        }

        /// <summary>
        /// Stretching연산을 실행한다
        /// orgMin -> targetMin, orgMax -> targetMax로 영역을 늘린다
        /// </summary>
        /// <param name="imgArr">원본 16bit 이미지 픽셀 배열</param>
        /// <param name="orgMin">늘릴 원본 영역의 최소값</param>
        /// <param name="orgMax">늘릴 원본 영역의 최대값</param>
        /// <param name="targetMin">목표로 하는 최소값</param>
        /// <param name="targetMax">목표로 하는 최대값</param>
        /// <returns>결과 값</returns>
        public static ushort[] ConvertToStretching(ushort[] imgArr, int orgMin, int orgMax, int targetMin = 0, int targetMax = 65535)
        {
            int len = imgArr.Length;
            ushort[] rstArr = new ushort[len];

            for (int i = 0; i < len; i++)
            {
                rstArr[i] = (ushort)((((targetMax - targetMin) / (orgMax - orgMin)) * (imgArr[i] - orgMin)) + targetMin);
            }

            return rstArr;
        }


        public static ushort[] Test(byte[] imgArr)
        {
            int len = imgArr.Length;
            ushort[] rstArr = new ushort[len];

            for (int i = 0; i < len; i++)
            {
                if (imgArr[i] == 0)
                {
                    rstArr[i] = 0;
                    continue;
                }

                if (imgArr[i] == 255)
                {
                    rstArr[i] = 65535;
                    continue;
                }

                rstArr[i] = (ushort)((imgArr[i]) * 256);
            }

            return rstArr;
        }
    }
}
