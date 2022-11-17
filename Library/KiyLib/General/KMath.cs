using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.General
{
    /// <summary>
    /// 수학에 관련된 연산을 제공하는 클래스
    /// </summary>
    public class KMath
    {
        /// <summary>
        /// 중간값을 구한다
        /// </summary>
        /// <typeparam name="T">중간값을 구할 data의 타입</typeparam>
        /// <param name="data">중간값을 구할 data 배열</param>
        /// <returns>중간값</returns>
        public static T CalcMedian<T>(T[] data)
        {
            int len = data.Length;

            T[] tempArr = new T[len];

            data.CopyTo(tempArr, 0);
            Array.Sort(tempArr);

            if (len % 2 == 0)
                return tempArr[len / 2];
            else
                return tempArr[(len - 1) / 2];
        }

        /// <summary>
        /// stdev(표준편차)를 구한다
        /// </summary>
        /// <param name="data">data 배열</param>
        /// <returns>stdev(표준편차)</returns>
        public static double CalcStandardDeviation(double[] data)
        {
            double sum = 0, mean = 0;
            int len = data.Length;

            mean = data.Sum() / len;

            for (int i = 0; i < len; i++)
                sum += Math.Pow(data[i] - mean, 2);

            if (sum != 0 && len - 1 != 0)
                return Math.Sqrt(sum / (len - 1));
            else
                return 0;
        }
    }
}
