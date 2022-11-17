using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.ExtensionMethod
{
    /// <summary>
    /// Arrayp의 확장메서드 구현을 위한 클래스
    /// </summary>
    public static class ArrayExt
    {
        /// <summary>
        /// 배열내에서 최대값을 찾은뒤 최대값의 index를 반환
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns>최대값의 index</returns>
        public static int MaxIndexOf<T>(this T[] input)
        {
            var max = input.Max();
            int index = Array.IndexOf(input, max);
            return index;
        }

        /// <summary>
        /// 배열내에서 최소값을 찾은뒤 최소값의 index를 반환
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns>최소값의 index</returns>
        public static int MinIndexOf<T>(this T[] input)
        {
            var min = input.Min();
            int index = Array.IndexOf(input, min);
            return index;
        }

        /// <summary>
        /// 3차원 배열을 1차원 배열로 바꾼다
        /// </summary>
        /// <typeparam name="T">바꿀 배열의 타입</typeparam>
        /// <param name="input">변경할 원본 3차원 배열</param>
        /// <returns>결과 값</returns>
        public static T[] ConvertTo1D<T>(this T[,,] input)
        {
            int width = input.GetLength(1);
            int height = input.GetLength(0);

            int cnt = 0, len = input.Length;
            T[] rstArr = new T[len];

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    rstArr[cnt] = input[h, w, 0];
                    cnt++;
                }
            }

            return rstArr;
        }
    }
}