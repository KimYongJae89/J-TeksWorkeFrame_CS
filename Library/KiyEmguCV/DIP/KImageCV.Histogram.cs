using KiyLib.DIP;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KiyEmguCV.DIP
{
    public partial class KImageCV
    {
        /* public static void GetHistogramGray(dynamic[,,] data, KDepthType depth, out int[] destHistoArr)
         {
             var histoArr = KHistogram.GetHistoArr(data, depth);
             int len = histoArr.GetLength(0);
             destHistoArr = new int[len];

             if (histoArr.GetLength(1) != 1)
                 throw new FormatException("KImageCV - GetHistogramGray(..): 흑백 이미지가 아닙니다.");

             for (int i = 0; i < len; i++)
                 destHistoArr[i] = histoArr[i, 0];
         }*/

        /* public static void GetHistogramColor(dynamic[,,] data, KDepthType depth, 
             out int[] destHistoArrB, out int[] destHistoArrG, out int[] destHistoArrR)
         {
             var histoArr = KHistogram.GetHistoArr(data, depth);
             int len = histoArr.GetLength(0);

             if (histoArr.GetLength(1) != 3)
                 throw new FormatException("KImageCV - GetHistogramColor(..): 칼라 이미지가 아닙니다.");

             destHistoArrB = new int[len];
             destHistoArrG = new int[len];
             destHistoArrR = new int[len];

             for (int i = 0; i < len; i++)
             {
                 destHistoArrB[i] = histoArr[i, 0];
                 destHistoArrG[i] = histoArr[i, 1];
                 destHistoArrR[i] = histoArr[i, 2];
             }
         }*/

        /// <summary>
        /// 흑백 이미지의 히스토그램을 구한다
        /// </summary>
        /// <typeparam name="T">data의 타입</typeparam>
        /// <param name="data">픽셀 data의 배열</param>
        /// <param name="depth">이미지의 Depth(bit)</param>
        /// <param name="dstHistoArr">결과 히스토그램 배열</param>
        public static void GetHistogramGray<T>(T[,,] data, KDepthType depth, out int[] dstHistoArr)
        {
            var histoArr = KHistogram.GetHistoArr<T>(data, depth);
            int len = histoArr.GetLength(0);
            dstHistoArr = new int[len];

            if (histoArr.GetLength(1) != 1)
                throw new FormatException(LangResource.ER_JIMG_IsNotGrayImg);

            for (int i = 0; i < len; i++)
                dstHistoArr[i] = histoArr[i, 0];
        }

        /// <summary>
        /// 칼라 이미지의 히스토그램을 구한다
        /// </summary>
        /// <typeparam name="T">data의 타입</typeparam>
        /// <param name="data">픽셀 data의 배열</param>
        /// <param name="depth">이미지의 Depth(bit)</param>
        /// <param name="dstHistoArrB">결과 히스토그램 배열 (Blue)</param>
        /// <param name="dstHistoArrG">결과 히스토그램 배열 (Green)</param>
        /// <param name="dstHistoArrR">결과 히스토그램 배열 (Red)</param>
        public static void GetHistogramColor<T>(T[,,] data, KDepthType depth,
            out int[] dstHistoArrB, out int[] dstHistoArrG, out int[] dstHistoArrR)
        {
            var histoArr = KHistogram.GetHistoArr(data, depth);
            int len = histoArr.GetLength(0);

            if (histoArr.GetLength(1) != 3)
                throw new FormatException(LangResource.ER_JIMG_IsNotColorImg);

            dstHistoArrB = new int[len];
            dstHistoArrG = new int[len];
            dstHistoArrR = new int[len];

            for (int i = 0; i < len; i++)
            {
                dstHistoArrB[i] = histoArr[i, 0];
                dstHistoArrG[i] = histoArr[i, 1];
                dstHistoArrR[i] = histoArr[i, 2];
            }
        }
    }
}
