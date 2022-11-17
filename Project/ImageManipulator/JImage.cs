using Emgu.CV;
using KiyEmguCV.DIP;
using KiyLib.DIP;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulator
{
    public class JImage : KImageBaseCV, ICloneable
    {
        /// <summary>
        /// 이미지를 생성한다.
        /// </summary>
        private JImage() { }

        /// <summary>
        /// 이미지를 생성한다.
        /// </summary>
        /// <param name="path">원본 이미지의 경로</param>
        public JImage(string path)
             : base(path) { }

        /// <summary>
        /// 이미지를 생성한다.
        /// </summary>
        /// <param name="width">이미지의 가로길이</param>
        /// <param name="height">이미지의 세로길이</param>
        /// <param name="depth">이미지의 Depth(bit)</param>
        public JImage(int width, int height, KDepthType depth)
            : base(width, height, depth) { }

        /// <summary>
        /// 이미지를 생성한다.
        /// </summary>
        /// <param name="mat">원본 Mat객체</param>
        /// <param name="depth">이미지의 Depth(bit)</param>
        public JImage(Mat mat, KDepthType depth)
            : base(mat, depth) { }

        /// <summary>
        /// 8bit 흑백 이미지를 생성한다.
        /// </summary>
        /// <param name="width">이미지의 가로 길이</param>
        /// <param name="height">이미지의 세로 길이</param>
        /// <param name="data">픽셀 데이터</param>
        public JImage(int width, int height, byte[] data)
           : base(KImageCV.Create8BitGray(width, height, data), KDepthType.Dt_8) { }

        /// <summary>
        /// 16bit 흑백 이미지를 생성한다.
        /// </summary>
        /// <param name="width">이미지의 가로 길이</param>
        /// <param name="height">이미지의 세로 길이</param>
        /// <param name="data">픽셀 데이터</param>
        public JImage(int width, int height, ushort[] data)
            : base(KImageCV.Create16BitGray(width, height, data), KDepthType.Dt_16) { }

        /// <summary>
        /// 이미지를 생성한다. (현재 칼라는 미지원, 속도 Create8BitGray, Create16BitGray메서드보다 느림)
        /// </summary>
        /// <param name="width">이미지의 가로 길이</param>
        /// <param name="height">이미지의 세로 길이</param>
        /// <param name="depth">이미지의 Depth(bit)</param>
        /// <param name="data">픽셀 데이터</param>
        public JImage(int width, int height, KDepthType depth, int[] data)
            : base(KImageCV.Create(width, height, depth, data), depth) { }



        // Geometry
        /// <summary>
        /// 이미지 크기를 변경한다
        /// </summary>
        /// <param name="scale">변경할 비율</param>
        /// <param name="interType">사용할 보간법</param>
        /// <returns>결과 이미지</returns>
        public new JImage Resize(double scale, KInterpolation interType = KInterpolation.Linear)
        {
            return Resize(scale, scale, interType);
        }

        /// <summary>
        /// 이미지 크기를 변경한다
        /// </summary>
        /// <param name="widthScale">변경할 가로 비율</param>
        /// <param name="heightScale">변경할 세로 비율</param>
        /// <param name="interType">사용할 보간법</param>
        /// <returns>결과 이미지</returns>
        public new JImage Resize(double widthScale, double heightScale, KInterpolation interType = KInterpolation.Linear)
        {
            var cvtMat = base.Resize(widthScale, heightScale, interType);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// 이미지의 특정영역을 잘라낸다.
        /// </summary>
        /// <param name="ROI">잘라낼 영역</param>
        /// <returns>결과 이미지</returns>
        public new JImage Crop(Rectangle ROI)
        {
            var cvtMat = base.Crop(ROI);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// 이미지를 반전시킨다.
        /// </summary>
        /// <param name="flipType">반전시킬 방향</param>
        /// <returns>결과 이미지</returns>
        public new JImage Flip(KFlipType flipType)
        {
            var cvtMat = base.Flip(flipType);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// 이미지를 회전시킨다.
        /// </summary>
        /// <param name="rotateType">회전시킬 방향</param>
        /// <returns>결과 이미지</returns>
        public new JImage Rotate(KRotateType rotateType)
        {
            var cvtMat = base.Rotate(rotateType);
            return new JImage(cvtMat, this.Depth);
        }


        // Processing
        /// <summary>
        /// 이진화 시킨다. 흑백 이미지에서만 사용가능하다.
        /// </summary>
        /// <param name="val">임계치 값</param>
        /// <returns>결과 이미지</returns>
        public new JImage Threshold(int val)
        {
            var cvtMat = base.Threshold(val);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Adaptive Threshold를 실행한다. 흑백 이미지에서만 사용가능하다.
        /// </summary>
        /// <param name="algoType">threshold 방식</param>
        /// <param name="blockSize">thresholding을 적용할 영역 사이즈, 
        /// 짝수 입력시 홀수로 변환돼서 적용됩니다.
        /// (ex)blockSize = 14; -> blockSize = 13로 내부에서 변경돼 적용</param>
        /// <param name="c">평균이나 가중평균에서 차감할 값</param>
        /// <returns>결과 이미지</returns>
        public new JImage ThresholdAdaptive(ThresAdaptiveType algoType, int blockSize, int c)
        {
            var cvtMat = base.ThresholdAdaptive(algoType, blockSize, c);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Otsu Threshold를  실행한다. 흑백 이미지에서만 사용가능하다.
        /// </summary>
        /// <param name="usedThresVal">Otsu threshold에 사용된 임계값</param>
        /// <returns>결과 이미지</returns>
        public new JImage ThresholdOtsu(out int usedThresVal)
        {
            var cvtMat = base.ThresholdOtsu(out usedThresVal);
            return new JImage(cvtMat, this.Depth);
        }


        /// <summary>
        /// 이미지 색상을 반전시킨다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public new JImage Inverse()
        {
            var cvtMat = base.Inverse();
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Lut(Look Up Table)에 맞게 색상을 변경한다.
        /// </summary>
        /// <param name="lutData">매핑할 Lut 데이터</param>
        /// <returns>결과 이미지</returns>
        public new JImage Lut(int[] lutData)
        {
            var cvtMat = base.Lut(lutData);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Blur 필터를 적용한다.
        /// </summary>
        /// <param name="width">필터에 적용할 width 값</param>
        /// <param name="height">필터에 적용할 height 값</param>
        /// <returns>결과 이미지</returns>
        public new JImage FT_Blur(int width = 3, int height = 3)
        {
            var cvtMat = base.FT_Blur(width, height);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Sharp1 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public new JImage FT_Sharp1()
        {
            var cvtMat = base.FT_Sharp1();
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Sharp2 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public new JImage FT_Sharp2()
        {
            var cvtMat = base.FT_Sharp2();
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Sobel 필터를 적용한다.
        /// </summary>
        /// <param name="xorder">필터에 적용할 xorder 값</param>
        /// <param name="yorder">필터에 적용할 yorder 값</param>
        /// <param name="apertureSize">필터에 적용할 apertureSize 값</param>
        /// <returns>결과 이미지</returns>
        public new JImage FT_Sobel(int xorder = 1, int yorder = 1, int apertureSize = 3)
        {
            if (apertureSize != 1 && apertureSize != 3 &&
                apertureSize != 5 && apertureSize != 7)
            {
                //throw new ArgumentException("apertureSize 값은 1,3,5,7중 하나여야 합니다");
                throw new ArgumentException(LangResource.ER_FT_ApertureSizeRange);
            }

            var cvtMat = base.FT_Sobel(xorder, yorder, apertureSize);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Laplacian 필터를 적용한다,
        /// </summary>
        /// <param name="apertureSize">필터에 적용할 apertureSize 값</param>
        /// <returns>결과 이미지</returns>
        public new JImage FT_Laplacian(int apertureSize = 3)
        {
            var cvtMat = base.FT_Laplacian(apertureSize);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Canny 필터를 적용한다.
        /// </summary>
        /// <param name="thresh">필터에 적용할 thresh 값</param>
        /// <param name="threshLinking">필터에 적용할 threshLinking 값</param>
        /// <returns>결과 이미지</returns>
        public new JImage FT_Canny(double thresh = 30, double threshLinking = 200)
        {
            var cvtMat = base.FT_Canny(thresh, threshLinking);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// HorizonEdge 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public new JImage FT_HorizonEdge()
        {
            var cvtMat = base.FT_HorizonEdge();
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// VerticalEdge 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public new JImage FT_VerticalEdge()
        {
            var cvtMat = base.FT_VerticalEdge();
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Median 필터를 적용한다.
        /// </summary>
        /// <param name="size">필터에 적용할 size 값</param>
        /// <returns>결과 이미지</returns>
        public new JImage FT_Median(int size = 3)
        {
            var cvtMat = base.FT_Median(size);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Dilate 필터를 적용한다.
        /// </summary>
        /// <param name="iterations">필터에 적용할 iterations 값</param>
        /// <returns>결과 이미지</returns>
        public new JImage FT_Dilate(int iterations = 1)
        {
            var cvtMat = base.FT_Dilate(iterations);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Erode 필터를 적용한다.
        /// </summary>
        /// <param name="iterations">필터에 적용할 iterations 값</param>
        /// <returns>결과 이미지</returns>
        public new JImage FT_Erode(int iterations = 1)
        {
            var cvtMat = base.FT_Erode(iterations);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Average 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public new JImage FT_Average()
        {
            var cvtMat = base.FT_Average();
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// Convolution 필터를 적용한다.
        /// </summary>
        /// <param name="kernel">적용할 kernel배열</param>
        /// <returns>결과 이미지</returns>
        public new JImage Filter(float[,] kernel)
        {
            var cvtMat = base.Filter(kernel);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// 칼라 이미지의 각 색상채널을 분리한다 (BGR순서)
        /// 추출된 채널은 흑백 이미지로 변환된다
        /// 칼라 이미지 에서만 사용 가능하며, 흑백 이미지에서 예외를 발생시킨다
        /// </summary>
        /// <returns>분리된 색상 채널 (BGR순서)</returns>
        public new JImage[] ColorChannelSeparate()
        {
            JImage[] rtImgs = new JImage[3];

            var cvtMat = base.ColorChannelSeparate();

            rtImgs[0] = new JImage(cvtMat[0], KDepthType.Dt_8);
            rtImgs[1] = new JImage(cvtMat[1], KDepthType.Dt_8);
            rtImgs[2] = new JImage(cvtMat[2], KDepthType.Dt_8);

            return rtImgs;
        }

        /// <summary>
        /// 칼라 이미지의 색상채널을 추출한다
        /// 추출된 채널은 흑백 이미지로 변환된다
        /// 칼라 이미지 에서만 사용 가능하며, 흑백 이미지에서 예외를 발생시킨다
        /// </summary>
        /// <param name="channelToExtract">추출할 색상채널</param>
        /// <returns>추출된 색상 채널</returns>
        public new JImage ColorChannelSeparate(KColorChannel channelToExtract)
        {
            var cvtMat = base.ColorChannelSeparate(channelToExtract);
            return new JImage(cvtMat, KDepthType.Dt_8);
        }

        /// <summary>
        /// 흑백 이미지를 각 색상 채널에 할당하여 칼라 이미지를 만든다
        /// 각각의 인자는 8bit 흑백 이미지여야 하며, 같은 크기(가로, 세로)여야 한다.
        /// </summary>
        /// <param name="imageToBeBlue">Blue 채널로 사용할 흑백 이미지</param>
        /// <param name="imageToBeGreen">Green 채널로 사용할 흑백 이미지</param>
        /// <param name="imageToBeRed">Red 채널로 사용할 흑백 이미지</param>
        /// <returns>합쳐진 칼라 이미지</returns>
        public JImage ColorChannelCombine(JImage imageToBeBlue, JImage imageToBeGreen, JImage imageToBeRed)
        {
            var cvtMat = base.ColorChannelCombine(imageToBeBlue.cvMat, imageToBeGreen.cvMat, imageToBeRed.cvMat);
            return new JImage(cvtMat, KDepthType.Dt_24);
        }


        // WindowLeveling
        /// <summary>
        /// 윈도우 레벨링을 실행한다.(흑백 전용)
        /// </summary>
        /// <returns>결과 이미지</returns>
        public new JImage WndLvGray()
        {
            var cvtMat = base.WndLvGray();
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// 윈도우 레벨링을 실행한다.(흑백 전용)
        /// </summary>
        /// <param name="min">레벨링할 영역의 최소값</param>
        /// <param name="max">레벨링할 영역의 최대값</param>
        /// <param name="divStep">나눌 단계</param>
        /// <returns>결과 이미지</returns>
        public new JImage WndLvGray(int min, int max, int divStep = 256)
        {
            var cvtMat = base.WndLvGray(min, max, divStep);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// 윈도우 레벨링을 실행한다.(칼라 전용)
        /// </summary>
        /// <param name="min">레벨링할 영역의 최소값</param>
        /// <param name="max">레벨링할 영역의 최대값</param>
        /// <param name="divStep">나눌 단계</param>
        /// <param name="clrMem">레벨링할 칼라 채널</param>
        /// <returns>결과 이미지</returns>
        public new JImage WndLvColor(int min, int max, int divStep, KColorChannel clrMem)
        {
            var cvtMat = base.WndLvColor(min, max, divStep, clrMem);
            return new JImage(cvtMat, this.Depth);
        }

        /// <summary>
        /// 윈도우 레벨링을 실행한다. R,G,B 채널 전부 동일한 값으로 실행된다.(칼라 전용)
        /// </summary>
        /// <param name="min">레벨링할 영역의 최소값</param>
        /// <param name="max">레벨링할 영역의 최대값</param>
        /// <param name="divStep">나눌 단계</param>
        /// <returns>결과 이미지</returns>
        public new JImage WndLvColor(int min, int max, int divStep = 256)
        {
            var cvtMat = base.WndLvColor(min, max, divStep);
            return new JImage(cvtMat, this.Depth);
        }


        // Blob
        /// <summary>
        /// 이미지내의 Blob을 찾는다. 이미지는 흑백이여야 한다.
        /// </summary>
        /// <param name="blobClr">찾아낼 Blob의 색</param>
        /// <param name="Threshold">Blob을 찾기전 이미지를 이진화 시킬 임계값</param>
        /// <param name="minArea">Blob의 최소 크기, 미만의 Area는 Blob후보에서 제외된다</param>
        /// <param name="maxArea">Blob의 최대 크기, 초과한 Area는 Blob후보에서 제외된다</param>
        /// <param name="connType">4방향, 8방향중 어떤방식으로 Blob을 지정할지 선택</param>
        /// <returns>검색한 Blob객체의 List</returns>
        public List<KBlobCV> FindBlob(KBinaryColorType blobClr, byte Threshold, int minArea = 25, int maxArea = 5000, KConnectivity connType = KConnectivity.Neighbors8)
        {
            if (this.Color == KColorType.Color)
                throw new Exception(LangResource.ER_JIMG_CantSupportColorImg);

            var dtt = new KBlobDetectorCV(this.cvMat);
            dtt.BlobColor = blobClr;
            dtt.Threshold = Threshold;
            dtt.MinArea = minArea;
            dtt.MaxArea = maxArea;
            dtt.Connectivity = connType;

            return dtt.FindBlob();
        }

        /// <summary>
        /// 인자로 전달된 Blob을 그린다. 리턴되는 이미지는 24비트 칼라이미지이다 (Contour:blue, Rectangle:Green)
        /// </summary>
        /// <param name="blobList">Blob객체들의 List</param>
        /// <param name="drawBoundingRectangle">Blob을 감싸는 사각형을 그릴지의 여부</param>
        /// <returns>Blob이 그려진 이미지(칼라)</returns>
        public JImage DrawBlob(List<KBlobCV> blobList, bool drawBoundingRectangle = false)
        {
            var dtt = new KBlobDetectorCV(this.cvMat);

            var drawMat = dtt.DrawBlobs(blobList, drawBoundingRectangle);
            return new JImage(drawMat, KDepthType.Dt_24);
        }


        // IKConvertDepth
        /// <summary>
        /// 8비트 흑백이미지로 변환한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public JImage To8BitG()
        {
            var cvtMat = base.To8BitG(this.cvMat);
            var rtImg = new JImage(cvtMat, KDepthType.Dt_8);

            return rtImg;
        }

        /// <summary>
        /// 16비트 흑백이미지로 변환한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public JImage To16BitG()
        {
            var cvtMat = base.To16BitG(this.cvMat);
            var rtImg = new JImage(cvtMat, KDepthType.Dt_16);

            return rtImg;
        }

        /// <summary>
        /// 24비트 칼라이미지로 변환한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        public JImage To24BitC()
        {
            var cvtMat = base.To24BitC(this.cvMat);
            var rtImg = new JImage(cvtMat, KDepthType.Dt_24);

            return rtImg;
        }


        // ICloneable
        /// <summary>
        /// 객체를 복사한다.
        /// </summary>
        /// <returns>복사된 객체</returns>
        public object Clone()
        {
            var rtImg = new JImage();
            rtImg.Path = this.Path;
            rtImg.cvIImg = this.cvIImg;
            rtImg.cvMat = this.cvMat;
            rtImg.cvMatLoadByDepth = this.cvMatLoadByDepth;
            rtImg.cvMatLoadByColor = this.cvMatLoadByColor;
            rtImg.data8 = this.data8;
            rtImg.data16 = this.data16;

            rtImg.Format = this.Format;
            rtImg.Depth = this.Depth;
            rtImg.Color = this.Color;
            rtImg.Width = this.Width;
            rtImg.Height = this.Height;
            rtImg.NumberOfChannels = this.NumberOfChannels;

            return rtImg;
        }
    }
}
