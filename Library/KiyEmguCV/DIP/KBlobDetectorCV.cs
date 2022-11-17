using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyEmguCV.DIP
{
    //public struct CCStatsOp
    //{
    //    public Rectangle Rectangle;
    //    public int Area;
    //}

    /// <summary>
    /// Blob을 찾는데 쓰이는 클래스, 흑백 이미지에서만 사용가능하다
    /// 찾아낸 Blob들은 KBlobCV클래스로 정의된다
    /// </summary>
    public class KBlobDetectorCV : IDisposable
    {
        private List<IDisposable> _disposables;
        private MCvScalar _contourBorderClr = new MCvScalar(255, 0, 0);
        private MCvScalar _contourBoundRectClr = new MCvScalar(0, 255, 0);
        private MCvScalar _blobCentroidClr = new MCvScalar(0, 0, 255);
        private Mat _srcMat, _cvtBinMat, _cvtClrMat;
        private KBinaryColorType _blobColor = KBinaryColorType.Black;
        //private float _minDistBetweenBlobs = 10;
        private int _threshold = 128;
        private float _minArea = 25, _maxArea = 5000;
        private KConnectivity _connectivity = KConnectivity.Neighbors8;

        /// <summary>
        /// Blob의 색상 타입, 검은색 Blob을 검출할 것인지 하얀색 Blob을 검출할 것인지 결정한다
        /// </summary>
        public KBinaryColorType BlobColor
        {
            get { return _blobColor; }
            set { _blobColor = value; }
        }

        /// <summary>
        /// Blob으로 검출할 최소 Area크기(기본값: 25)
        /// 이 값보다 작은 Area크기를 가진 Blob은 검출하지 않는다
        /// </summary>
        public float MinArea
        {
            get { return _minArea; }
            set { _minArea = value; }
        }

        /// <summary>
        /// Blob으로 검출할 최대 Area크기(기본값: 5000)
        /// 이 값보다 큰 Area크기를 가진 Blob은 검출하지 않는다
        /// </summary>
        public float MaxArea
        {
            get { return _maxArea; }
            set { _maxArea = value; }
        }

        //public float MinDistBetweenBlobs { get => _minDistBetweenBlobs; set => _minDistBetweenBlobs = value; }

        /// <summary>
        /// Blob검출전에 시행하는 Threshold(이진화)의 임계값
        /// 이미지내에서 Blob을 검출하기전에 Threshold(이진화)를 실행하는데 그 임계값을 설정한다
        /// </summary>
        public int Threshold
        {
            get { return _threshold; }
            set { _threshold = value; }
        }

        /// <summary>
        /// Blob을 검출할때 Connected-component labeling 알고리즘을 사용하는데, 그에 사용되는 설정값(기본값: Neighbors8)
        /// Neighbors4는 4방향, Neighbors8는 픽셀 주변 8방향을 기준으로 연결된 객체로 선별한다
        /// </summary>
        public KConnectivity Connectivity
        {
            get { return _connectivity; }
            set { _connectivity = value; }
        }


        /// <summary>
        /// KBlobDetectorCV의 생성자
        /// </summary>
        /// <param name="srcMat">Blob을 찾을 이미지의 Mat객체</param>
        public KBlobDetectorCV(Mat srcMat)
        {
            _srcMat = new Mat();
            _cvtBinMat = new Mat(srcMat.Size, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
            _cvtClrMat = new Mat(srcMat.Size, Emgu.CV.CvEnum.DepthType.Cv8U, 3);

            srcMat.CopyTo(_srcMat);

            _disposables = new List<IDisposable>();
            _disposables.Add(_srcMat);
            _disposables.Add(_cvtBinMat);
        }


        /// <summary>
        /// 이미지내의 Blob을 찾는다. 이미지는 흑백이여야 한다.
        /// </summary>
        /// <returns>찾은 Blob들의 List를 리턴한다</returns>
        public List<KBlobCV> FindBlob()
        {
            // Blob전 이진화
            CvInvoke.Threshold(_srcMat, _cvtBinMat, Threshold, 255, ThresholdType.Binary);
            // Contour, Label등을 표시하기 위한 결과 이미지(칼라)
            CvInvoke.CvtColor(_cvtBinMat, _cvtClrMat, ColorConversion.Gray2Bgr);

            // 흑, 백 Blob 색 종류에 따라 이미지 역상실행
            if (BlobColor == KBinaryColorType.Black)
                CvInvoke.BitwiseNot(_cvtBinMat, _cvtBinMat);

            List<KBlobCV> blobList = new List<KBlobCV>();


            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hier = new Mat();

            CvInvoke.FindContours(_cvtBinMat, contours, hier, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            int blobCnt = 1;
            for (int i = 0; i < contours.Size; i++)
            {
                Rectangle rect = CvInvoke.BoundingRectangle(contours[i]);
                double area = CvInvoke.ContourArea(contours[i]);

                if (area > MinArea && area < MaxArea)
                {
                    blobList.Add(new KBlobCV($"{blobCnt}", area, rect, contours[i].ToArray()));
                    blobCnt++;
                }
            }

            var imgMat = DrawBlobs(blobList);

            imgMat.Save(@"d:\_cvtClrMat.bmp");

            return blobList;
        }

        /// <summary>
        /// 인자로 전달된 Blob을 그린다. 리턴되는 이미지는 24비트 칼라이미지이다 (Contour:blue, Rectangle:Green)
        /// </summary>
        /// <param name="listOfBlobs">Blob객체들의 List</param>
        /// <param name="drawBoundingRectangle">Blob을 감싸는 사각형을 그릴지의 여부</param>
        /// <returns>Blob이 그려진 이미지(칼라)</returns>
        public Mat DrawBlobs(List<KBlobCV> listOfBlobs, bool drawBoundingRectangle = false)
        {
            Mat rtImg = new Mat();
            CvInvoke.CvtColor(_srcMat, rtImg, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);

            VectorOfPoint[] vctPt = new VectorOfPoint[listOfBlobs.Count];

            for (int i = 0; i < listOfBlobs.Count; i++)
                vctPt[i] = new VectorOfPoint(listOfBlobs[i].ContourPoints);

            VectorOfVectorOfPoint vvctPt = new VectorOfVectorOfPoint(vctPt);

            for (int i = 0; i < listOfBlobs.Count; i++)
            {
                vctPt[i] = new VectorOfPoint(listOfBlobs[i].ContourPoints);

                if (drawBoundingRectangle)
                    CvInvoke.Rectangle(rtImg, listOfBlobs[i].BoundingRect, _contourBoundRectClr);

                CvInvoke.DrawContours(rtImg, vvctPt, i, _contourBorderClr);

                Point txtLocPt = new Point(listOfBlobs[i].BoundingRect.X, listOfBlobs[i].BoundingRect.Y + 10);

                KDrawCV.DrawText(rtImg, (i + 1).ToString(), txtLocPt, new KColor(0, 0, 255));
                KDrawCV.DrawText(rtImg, listOfBlobs[i].Area.ToString(),
                    new Point(txtLocPt.X + 20, txtLocPt.Y + 20),
                    new KColor(255, 0, 255));
            }

            return rtImg;
        }


        /// <summary>
        /// 사용된 리소스를 해제한다
        /// </summary>
        public void Dispose()
        {
            foreach (var item in _disposables)
            {
                item.Dispose();
            }
        }
    }
}