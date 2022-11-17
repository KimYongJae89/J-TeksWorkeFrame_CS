using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyEmguCV.DIP
{
    /// <summary>
    /// Blob을 정의한 클래스
    /// KBlobDetectorCV을 이용하여 찾은 Blob을 정의할때 사용된다
    /// </summary>
    public class KBlobCV
    {
        private string _label;
        private double _area;
        private Rectangle _boundingRect;
        private Point[] _contourPoints;

        /// <summary>
        /// 라벨
        /// </summary>
        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        /// <summary>
        /// Area의 크기
        /// </summary>
        public double Area
        {
            get { return _area; }
            set { _area = value; }
        }

        /// <summary>
        /// Area를 감싸는 사각형
        /// </summary>
        public Rectangle BoundingRect
        {
            get { return _boundingRect; }
            set { _boundingRect = value; }
        }

        /// <summary>
        /// 외곽선을 이루는 Point들의 집합
        /// </summary>
        public Point[] ContourPoints
        {
            get { return _contourPoints; }
            set { _contourPoints = value; }
        }


        /// <summary>
        /// Blob 클래스 생성자
        /// </summary>
        public KBlobCV() { }

        /// <summary>
        /// Blob 클래스 생성자
        /// </summary>
        /// <param name="label">객체의 라벨</param>
        /// <param name="area">Area의 크기</param>
        /// <param name="boundingRect">Area를 감싸는 사각형</param>
        /// <param name="contourPoints">Area 외곽선을 이루는 Point들의 집합</param>
        public KBlobCV(string label, double area, Rectangle boundingRect, Point[] contourPoints)
        {
            _label = label;
            _area = area;
            _boundingRect = boundingRect;
            _contourPoints = contourPoints;
        }
    }
}
