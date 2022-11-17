using KiyLib.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.DIP
{
    /// <summary>
    /// ROI의 히스토그램 연산을 위한 클래스
    /// </summary>
    public class KRegionHistogram
    {
        // Tuple<int, int>은 모두 <index, count>순
        private int[] rgnHistoArr;
        private Rectangle regionRect;
        // Histogram Tab
        private int area;       // mk1, mk2 사이에 있는, 값이 0이아닌 픽셀개수의 합
        private double mean;    // 밝기평균
        private Tuple<int, int> marker1;
        private Tuple<int, int> marker2;
        // Extreams Tab
        private Tuple<int, int> maxValue;   // 가장 count가 많이된 밝기값(index)
        private Tuple<int, int> minValue;   // 가장 count가 적게된 밝기값(index)
        private Tuple<int, int> firstData;
        private Tuple<int, int> lastData;
        private int graphStartVal;
        private int graphEndVal;

        /// <summary>
        /// Marker1와 Marker2 사이에 있는 값이 0이아닌 픽셀개수의 합
        /// </summary>
        public int Area
        {
            get { return area; }
            set { area = value; }
        }

        /// <summary>
        /// 평균값
        /// </summary>
        public double Mean
        {
            get { return mean; }
            set { mean = value; }
        }

        /// <summary>
        /// Marker1, (index, 히스토그램의 count값)
        /// </summary>
        public Tuple<int, int> Marker1
        {
            get { return marker1; }
            set { marker1 = value; }
        }

        /// <summary>
        /// Marker2, (index, 히스토그램의 count값)
        /// </summary>
        public Tuple<int, int> Marker2
        {
            get { return marker2; }
            set { marker2 = value; }
        }

        /// <summary>
        /// 히스토그램에서 가장 값이 큰 count (index, 히스토그램의 count값)
        /// </summary>
        public Tuple<int, int> MaxValue
        {
            get { return maxValue; }
            private set { maxValue = value; }
        }

        /// <summary>
        /// 히스토그램에서 가장 값이 작은 count (index, 히스토그램의 count값)
        /// </summary>
        public Tuple<int, int> MinValue
        {
            get { return minValue; }
            private set { minValue = value; }
        }

        /// <summary>
        /// count가 0이 아닌 히스토그램의 첫번째 값 (index, 히스토그램의 count값)
        /// </summary>
        public Tuple<int, int> FirstData
        {
            get { return firstData; }
            private set { firstData = value; }
        }

        /// <summary>
        /// count가 0이 아닌 히스토그램의 마지막 값 (index, 히스토그램의 count값)
        /// </summary>
        public Tuple<int, int> LastData
        {
            get { return lastData; }
            private set { lastData = value; }
        }

        /// <summary>
        /// 히스토그램 배열
        /// </summary>
        public int[] RgnHistoArr
        {
            get { return rgnHistoArr; }
            private set { rgnHistoArr = value; }
        }


        public KRegionHistogram() { }


        /// <summary>
        /// 히스토그램 데이터를 대입한다
        /// </summary>
        /// <param name="imgArr">이미지의 픽셀 데이터</param>
        /// <param name="width">이미지의 가로 길이</param>
        /// <param name="region">이미지 내에서 히스토그램 데이터를 얻어올 영역</param>
        public void SetHistoData(ushort[] imgArr, int width, Rectangle region)
        {
            RgnHistoArr = KHistogram.GetHistoArr(imgArr, width, region);
            regionRect = region;

            SetExtremesData();

            graphStartVal = firstData.Item1;
            graphEndVal = lastData.Item2;

            Marker1 = new Tuple<int, int>(firstData.Item1, firstData.Item2);
            Marker2 = new Tuple<int, int>(lastData.Item1, lastData.Item2);

            SetHistogramData();
        }

        /// <summary>
        /// 히스토그램 데이터를 대입한다
        /// </summary>
        /// <param name="imgArr">이미지의 픽셀 데이터</param>
        /// <param name="width">이미지의 가로 길이</param>
        /// <param name="region">이미지 내에서 히스토그램 데이터를 얻어올 영역</param>
        public void SetHistoData(int[] imgArr, int width, Rectangle region)
        {
            ushort[] cvtArr = new ushort[imgArr.Length];

            for (int i = 0; i < imgArr.Length; i++)
                cvtArr[i] = (ushort)imgArr[i];

            SetHistoData(cvtArr, width, region);
        }

        /// <summary>
        /// 히스토그램 데이터를 대입한다
        /// </summary>
        /// <param name="imgArr">이미지의 픽셀 데이터</param>
        /// <param name="width">이미지의 가로 길이</param>
        /// <param name="region">이미지 내에서 히스토그램 데이터를 얻어올 영역</param>
        public void SetHistoData(ushort[] imgArr, int width, RectangleF region)
        {
            SetHistoData(imgArr, width, Rectangle.Round(region));
        }

        
        /// <summary>
        /// Marker들의 위치에 따라 히스토그램 데이터를 갱신한다
        /// </summary>
        private void SetHistogramData()
        {
            //int len = lastData.Item1 - firstData.Item1;
            int sum = 0;

            int frontIndex = Marker1.Item1 < Marker2.Item1 ? Marker1.Item1 : Marker2.Item1;
            int rearIndex = Marker1.Item1 > Marker2.Item1 ? Marker1.Item1 : Marker2.Item1;

            for (int i = frontIndex; i <= rearIndex; i++)
                sum += RgnHistoArr[i];

            area = sum;
        }

        /// <summary>
        /// Extremes Data들을 대입한다
        /// 매트록스 히스토그램의 Extremes탭과 같다
        /// </summary>
        private void SetExtremesData()
        {
            int fdIndex = Array.IndexOf(RgnHistoArr, RgnHistoArr.FirstOrDefault(val => val != 0));
            FirstData = new Tuple<int, int>(fdIndex, RgnHistoArr[fdIndex]);

            int ldIndex = Array.LastIndexOf(RgnHistoArr, RgnHistoArr.LastOrDefault(val => val != 0));
            LastData = new Tuple<int, int>(ldIndex, RgnHistoArr[ldIndex]);

            int maxIndex = RgnHistoArr.MaxIndexOf();
            MaxValue = new Tuple<int, int>(maxIndex, RgnHistoArr[maxIndex]);

            if(ldIndex == fdIndex)
            {
                MinValue = new Tuple<int, int>(ldIndex, RgnHistoArr[ldIndex]);
                return;
            }

            int[] tempArr = new int[ldIndex - fdIndex];
            Array.Copy(RgnHistoArr, fdIndex, tempArr, 0, tempArr.Length);
            int minIndex = tempArr.MinIndexOf() + fdIndex;
            MinValue = new Tuple<int, int>(minIndex, RgnHistoArr[minIndex]);
        }
    }
}
