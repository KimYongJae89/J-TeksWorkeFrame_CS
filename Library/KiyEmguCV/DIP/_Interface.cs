using Emgu.CV;
using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace KiyEmguCV.DIP
{
    /// <summary>
    /// 이미지처리에 사용되는 기본적인 기능을 정의한 인터페이스
    /// </summary>
    public interface IKImage
    {
        /// <summary>
        /// 이미지의 포맷 기본값: None (지원포맷: RAW, TIFF, BMP, JPEG, PNG, DCM)
        /// </summary>
        KImageFormat Format { get; }

        /// <summary>
        /// 이미지의 Depth타입을 나타낸다,
        /// 8bit: 흑백Only, 16bit: 흑백Only, 24bit: 칼라Only
        /// </summary>
        KDepthType Depth { get; }

        /// <summary>
        /// 이미지의 색상타입 흑백또는 칼라를 나타낸다.
        /// </summary>
        KColorType Color { get; }

        /// <summary>
        /// 이미지의 가로길이
        /// </summary>
        int Width { get; }

        /// <summary>
        /// 이미지의 세로길이
        /// </summary>
        int Height { get; }

        /// <summary>
        /// 이미지의 색상채널의 수 (흑백:1, 칼라:3 고정)
        /// </summary>
        int NumberOfChannels { get; }

        /// <summary>
        /// 이미지를 불러온다.
        /// </summary>
        /// <param name="path">이미지의 경로</param>
        void Load(string path);

        /// <summary>
        /// 이미지를 저장한다
        /// </summary>
        /// <param name="path">이미지를 저장할 경로. 확장자도 포함해서 지정해야 한다.</param>
        void Save(string path);

        /// <summary>
        /// 흑백 이미지일때 픽셀의 밝기값을 가져온다.
        /// </summary>
        /// <param name="x">픽셀의 x좌표</param>
        /// <param name="y">픽셀의 y좌표</param>
        /// <returns>픽셀의 밝기값</returns>
        int GetPixelGray(int x, int y);

        /// <summary>
        /// 칼라 이미지일때 픽셀의 밝기값을 가져온다.
        /// </summary>
        /// <param name="x">픽셀의 x좌표</param>
        /// <param name="y">픽셀의 y좌표</param>
        /// <returns>픽셀의 밝기값</returns>
        KColor GetPixelColor(int x, int y);

        /// <summary>
        /// 흑백 이미지일때 픽셀의 밝기값을 지정한다.
        /// </summary>
        /// <param name="x">픽셀의 x좌표</param>
        /// <param name="y">픽셀의 y좌표</param>
        /// <param name="GrayValue">픽셀의 밝기값</param>
        void SetPixelGray(int x, int y, int GrayValue);

        /// <summary>
        /// 칼라 이미지일때 픽셀의 밝기값을 지정한다.
        /// </summary>
        /// <param name="x">픽셀의 x좌표</param>
        /// <param name="y">픽셀의 y좌표</param>
        /// <param name="clr">픽셀의 밝기값</param>
        void SetPixelColor(int x, int y, KColor clr);
    }


    /// <summary>
    /// 히스토그램 관련 기능을 정의한 인터페이스
    /// </summary>
    public interface IKHistogram
    {
        /// <summary>
        /// 흑빅 이미지일때 히스토그램 배열을 가져온다.
        /// </summary>
        /// <param name="dstHistoArr">결과를 저장할 배열</param>
        void GetHistoGray(out int[] dstHistoArr);

        /// <summary>
        /// 칼라 이미지일때 히스토그램 배열을 가져온다.(BGR 순서)
        /// </summary>
        /// <param name="dstHistoArrB">결과를 저장할 배열(Blue)</param>
        /// <param name="dstHistoArrG">결과를 저장할 배열(Green)</param>
        /// <param name="dstHistoArrR">결과를 저장할 배열(Red)</param>
        void GetHistoColor(out int[] dstHistoArrB, out int[] dstHistoArrG, out int[] dstHistoArrR);

        /// <summary>
        /// 히스토그램 배열에서 최저값의 개수와 index를 구한다. (흑백 전용)
        /// </summary>
        /// <param name="index">최저값의 index</param>
        /// <param name="count">최저값의 개수</param>
        void GetHistoMinGray(out int index, out int count);

        /// <summary>
        /// 히스토그램 배열에서 최대값의 개수와 index를 구한다. (흑백 전용)
        /// </summary>
        /// <param name="index">최대값의 index</param>
        /// <param name="count">최대값의 개수</param>
        void GetHistoMaxGray(out int index, out int count);

        /// <summary>
        /// 히스토그램의 평균을 구한다.
        /// </summary>
        /// <returns>결과값</returns>
        double GetHistoAvgGray();

        /// <summary>
        /// 히스토그램 배열에서 최저값의 개수와 index를 구한다. (칼라 전용)
        /// </summary>
        /// <param name="indexB">최저값의 index (Blue)</param>
        /// <param name="countB">최저값의 개수 (Blue)</param>
        /// <param name="indexG">최저값의 index (Green)</param>
        /// <param name="countG">최저값의 개수 (Green)</param>
        /// <param name="indexR">최저값의 index (Red)</param>
        /// <param name="countR">최저값의 개수 (Red)</param>
        void GetHistoMinColor(out int indexB, out int countB,
                              out int indexG, out int countG,
                              out int indexR, out int countR);

        /// <summary>
        /// 히스토그램 배열에서 최대값의 개수와 index를 구한다. (칼라 전용)
        /// </summary>
        /// <param name="indexB">최대값의 index (Blue)</param>
        /// <param name="countB">최대값의 개수 (Blue)</param>
        /// <param name="indexG">최대값의 index (Green)</param>
        /// <param name="countG">최대값의 개수 (Green)</param>
        /// <param name="indexR">최대값의 index (Red)</param>
        /// <param name="countR">최대값의 개수 (Red)</param>
        void GetHistoMaxColor(out int indexB, out int countB,
                              out int indexG, out int countG,
                              out int indexR, out int countR);

        /// <summary>
        /// 히스토그램의 평균을 구한다.
        /// </summary>
        /// <param name="avgB">결과값 (Blue)</param>
        /// <param name="avgG">결과값 (Green)</param>
        /// <param name="avgR">결과값 (Red)</param>
        void GetHistoAvgColor(out double avgB, out double avgG, out double avgR);

        /// <summary>
        /// 히스토그램에서 FirstData와 LastData의 Index를 구한다(ISee! 뷰어에서 드래그 윈도우 레벨링에 사용)
        /// </summary>
        /// <param name="indexOfFirstData">검색된 FirstData의 Index</param>
        /// <param name="indexOfLastData">검색된 LastData의 Index</param>
        void GetHistoFirstNonZeroAtBothSideGray(out int indexOfFirstData, out int indexOfLastData);
    }


    /// <summary>
    /// Line Profile 관련기능을 정의한 인터페이스
    /// </summary>
    public interface IKLineProfile
    {
        /// <summary>
        /// 라인 프로파일을 실행할 객체를 가져옵니다.
        /// </summary>
        /// <param name="start">프로파일 시작점</param>
        /// <param name="end">프로파일 끝점</param>
        /// <returns>라인 프로파일 객체</returns>
        KLineProfileBaseCV GetLineProfileInfo(Point start, Point end);

        /// <summary>
        /// 라인 프로파일을 실행할 객체를 가져옵니다.
        /// </summary>
        /// <param name="startX">>프로파일 시작점 X좌표</param>
        /// <param name="startY">>프로파일 시작점 Y좌표</param>
        /// <param name="endX">프로파일 끝점 X좌표</param>
        /// <param name="endY">프로파일 끝점 Y좌표</param>
        /// <returns>라인 프로파일 객체</returns>
        KLineProfileBaseCV GetLineProfileInfo(int startX, int startY, int endX, int endY);
    }


    /// <summary>
    /// 필터, 윈도우레벨링등 기본적은 영상처리 기능을 정의한 인터페이스
    /// </summary>
    public interface IKProcessing
    {
        /// <summary>
        /// 이진화 시킨다. 흑백 이미지에서만 사용가능하다.
        /// </summary>
        /// <param name="val">임계값</param>
        /// <returns>처리된 Mat객체</returns>
        Mat Threshold(int val);

        /// <summary>
        /// Adaptive Threshold를 실행한다. 흑백 이미지에서만 사용가능하다.
        /// </summary>
        /// <param name="algoType">threshold 방식</param>
        /// <param name="blockSize">thresholding을 적용할 영역 사이즈, 
        /// 짝수 입력시 홀수로 변환돼서 적용됩니다.
        /// (ex)blockSize = 14; -> blockSize = 13로 내부에서 변경돼 적용</param>
        /// <param name="c">평균이나 가중평균에서 차감할 값</param>
        /// <returns>처리된 Mat객체</returns>
        Mat ThresholdAdaptive(ThresAdaptiveType algoType, int blockSize, int c);

        /// <summary>
        /// Otsu Threshold를  실행한다. 흑백 이미지에서만 사용가능하다.
        /// </summary>
        /// <param name="usedThresVal">Otsu threshold에 사용된 임계값</param>
        /// <returns>처리된 Mat객체</returns>
        Mat ThresholdOtsu(out int usedThresVal);

        /// <summary>
        /// 색상 반전
        /// </summary>
        /// <returns>처리된 Mat객체</returns>
        Mat Inverse();

        /// <summary>
        /// LUT(Lookup table)에 맞게 픽셀 데이터를 매핑한다.
        /// </summary>
        /// <param name="lutData">LUT 배열</param>
        /// <returns>처리된 Mat객체</returns>
        Mat Lut(int[] lutData);


        // Filter
        /// <summary>
        /// Blur 필터를 적용한다.
        /// </summary>
        /// <param name="width">필터에 적용할 width 값</param>
        /// <param name="height">필터에 적용할 height 값</param>
        /// <returns>결과 이미지</returns>
        Mat FT_Blur(int width = 3, int height = 3);

        /// <summary>
        /// Sharp1 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        Mat FT_Sharp1();

        /// <summary>
        /// Sharp2 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        Mat FT_Sharp2();

        /// <summary>
        /// Sobel 필터를 적용한다.
        /// </summary>
        /// <param name="xorder">필터에 적용할 xorder 값</param>
        /// <param name="yorder">필터에 적용할 yorder 값</param>
        /// <param name="apertureSize">필터에 적용할 apertureSize 값</param>
        /// <returns>결과 이미지</returns>
        Mat FT_Sobel(int xorder = 1, int yorder = 1, int apertureSize = 3);

        /// <summary>
        /// Laplacian 필터를 적용한다,
        /// </summary>
        /// <param name="apertureSize">필터에 적용할 apertureSize 값</param>
        /// <returns>결과 이미지</returns>
        Mat FT_Laplacian(int apertureSize = 3);

        /// <summary>
        /// Canny 필터를 적용한다.
        /// </summary>
        /// <param name="thresh">필터에 적용할 thresh 값</param>
        /// <param name="threshLinking">필터에 적용할 threshLinking 값</param>
        /// <returns>결과 이미지</returns>
        Mat FT_Canny(double thresh = 30, double threshLinking = 200);

        /// <summary>
        /// HorizonEdge 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        Mat FT_HorizonEdge();

        /// <summary>
        /// VerticalEdge 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        Mat FT_VerticalEdge();

        /// <summary>
        /// Median 필터를 적용한다.
        /// </summary>
        /// <param name="size">필터에 적용할 size 값</param>
        /// <returns>결과 이미지</returns>
        Mat FT_Median(int size = 3);

        /// <summary>
        /// Dilate 필터를 적용한다.
        /// </summary>
        /// <param name="iterations">필터에 적용할 iterations 값</param>
        /// <returns>결과 이미지</returns>
        Mat FT_Dilate(int iterations = 1);

        /// <summary>
        /// Erode 필터를 적용한다.
        /// </summary>
        /// <param name="iterations">필터에 적용할 iterations 값</param>
        /// <returns>결과 이미지</returns>
        Mat FT_Erode(int iterations = 1);

        /// <summary>
        /// Average 필터를 적용한다.
        /// </summary>
        /// <returns>결과 이미지</returns>
        Mat FT_Average();

        /// <summary>
        /// Convolution 필터를 적용한다.
        /// </summary>
        /// <param name="kernel">적용할 Convolution 배열(홀수, 각 배열차원의 Length는 동일)</param>
        /// <returns>처리된 Mat객체</returns>
        Mat Filter(float[,] kernel);


        // WindowLeveling 
        /// <summary>
        /// 윈도우 레벨링 함수(흑백), min, max범위를 자동으로 정하여 256단계로 나눈다.
        /// min값은 히스토그램 배열에서 0이 아닌 첫번째 값을 기준으로 하며,
        /// max값은 0이 아닌 마지막 값을 기준으로 한다.
        /// </summary>
        /// <returns></returns>
        Mat WndLvGray();

        /// <summary>
        /// 윈도우 레벨링 함수(흑백), min, max범위의 픽셀값을 divStep만큼의 단계로 나눈다.
        /// (ex)WndLvGray(0, 40000, 1024); 0 ~ 40000 사이의 픽셀들을 1024 단계로 나눈다.
        /// </summary>
        /// <param name="min">레벨링할 픽셀의 최소 값</param>
        /// <param name="max">레벨링할 픽셀의 최대 값</param>
        /// <param name="divStep">나눌 단계의 수</param>
        /// <returns>처리된 Mat객체</returns>
        Mat WndLvGray(int min, int max, int divStep = 256);

        /// <summary>
        /// 윈도우 레벨링 함수(칼라), min, max범위의 픽셀값을 divStep만큼의 단계로 나눈다.
        /// </summary>
        /// <param name="min">레벨링할 픽셀의 최소 값</param>
        /// <param name="max">레벨링할 픽셀의 최대 값</param>
        /// <param name="divStep">나눌 단계의 수</param>
        /// <param name="clrMem">레벨링할 칼라 채널</param>
        /// <returns>처리된 Mat객체</returns>
        Mat WndLvColor(int min, int max, int divStep, KColorChannel clrMem);

        /// <summary>
        /// 윈도우 레벨링을 실행한다. R,G,B 채널 전부 동일한 값으로 실행된다.(칼라 전용)
        /// </summary>
        /// <param name="min">레벨링할 영역의 최소값</param>
        /// <param name="max">레벨링할 영역의 최대값</param>
        /// <param name="divStep">나눌 단계</param>
        /// <returns>결과 이미지</returns>
        Mat WndLvColor(int min, int max, int divStep = 256);


        // Color Channel
        /// <summary>
        /// 칼라 이미지의 각 색상채널을 분리한다 (BGR순서)
        /// 추출된 채널은 흑백 이미지로 변환된다
        /// 칼라 이미지 에서만 사용 가능하며, 흑백 이미지에서 예외를 발생시킨다
        /// </summary>
        /// <returns>분리된 색상 채널 (BGR순서)</returns>
        Mat[] ColorChannelSeparate();

        /// <summary>
        /// 칼라 이미지의 색상채널을 추출한다
        /// 추출된 채널은 흑백 이미지로 변환된다
        /// 칼라 이미지 에서만 사용 가능하며, 흑백 이미지에서 예외를 발생시킨다
        /// </summary>
        /// <param name="channelToExtract">추출할 색상채널</param>
        /// <returns>추출된 색상 채널</returns>
        Mat ColorChannelSeparate(KColorChannel channelToExtract);

        /// <summary>
        /// 흑백 이미지를 각 색상 채널에 할당하여 칼라 이미지를 만든다
        /// 각각의 인자는 8bit 흑백 이미지여야 하며, 같은 크기(가로, 세로)여야 한다.
        /// </summary>
        /// <param name="imageToBeBlue">Blue 채널로 사용할 흑백 이미지</param>
        /// <param name="imageToBeGreen">Green 채널로 사용할 흑백 이미지</param>
        /// <param name="imageToBeRed">Red 채널로 사용할 흑백 이미지</param>
        /// <returns>합쳐진 칼라 이미지</returns>
        Mat ColorChannelCombine(Mat imageToBeBlue, Mat imageToBeGreen, Mat imageToBeRed);
    }


    /// <summary>
    /// Geometry 영상처리를 위한 인터페이스
    /// </summary>
    public interface IKGeometry
    {
        /// <summary>
        /// 이미지를 Resize한다.
        /// </summary>
        /// <param name="scale">Resize할 가로, 세로 비율</param>
        /// <param name="interType">Resize방식 기본은 Linear(선형)</param>
        /// <returns>처리된 Mat객체</returns>
        Mat Resize(double scale, KInterpolation interType = KInterpolation.Linear);

        /// <summary>
        /// 이미지를 Resize한다.
        /// </summary>
        /// <param name="widthScale">Resize할 가로 비율</param>
        /// <param name="heightScale">Resize할 세로 비율></param>
        /// <param name="interType">Resize방식 기본은 Linear(선형)</param>
        /// <returns>처리된 Mat객체</returns>
        Mat Resize(double widthScale, double heightScale, KInterpolation interType = KInterpolation.Linear);

        /// <summary>
        /// 이미지의 특정영역을 잘라낸다.
        /// </summary>
        /// <param name="ROI">잘라낼 ROI</param>
        /// <returns>처리된 Mat객체</returns>
        Mat Crop(Rectangle ROI);

        /// <summary>
        /// 이미지를 반전시킨다.
        /// </summary>
        /// <param name="flipType">이미지를 반전시킬 방향(상하, 좌우)</param>
        /// <returns>처리된 Mat객체</returns>
        Mat Flip(KFlipType flipType);

        /// <summary>
        /// 이미지를 회전시킨다.
        /// </summary>
        /// <param name="rotateType">이미지를 회전시킬 방향(90도 단위)</param>
        /// <returns>처리된 Mat객체</returns>
        Mat Rotate(KRotateType rotateType);
    }


    /// <summary>
    /// ROI단위로 영상처리를 하기위한 인터페이스
    /// GetROIInfo(..) 메서드를 사용하여 반환된 KRoiCV객체를 이용하여 영상처리를 실행한다
    /// </summary>
    public interface IKROI
    {
        /// <summary>
        /// 지정한 region에 대한 KRoiCV객체를 리턴한다.
        /// </summary>
        /// <param name="region">ROI 영역</param>
        /// <returns>지정한 ROI정보를 담고있는 객체</returns>
        KRoiCV GetROIInfo(Rectangle region);
    }


    /// <summary>
    /// 고속푸리에 변환(FFT)를 위한 인터페이스. 현재 프로그램에서는 사용하지 않고있다.
    /// </summary>
    public interface IKFourierTransform
    {
        /// <summary>
        /// 현재 이미지에 대한 FourierTransformCV 객체를 리턴한다
        /// </summary>
        /// <returns>현재 이미지의 FFT연산을 수행할수 있는 객체</returns>
        FourierTransformCV GetFFTInfo();
    }


    /// <summary>
    /// Bitmap객체로 변환하기 위한 인터페이스
    /// </summary>
    public interface IKConvertibleToBitmap
    {
        /// <summary>
        /// C# Bitmap 클래스로 변환한다
        /// 원본이 16비트 흑백 이미지일 경우 흑백 8비트로 변환해서 반환한다
        /// </summary>
        /// <returns>변환된 Bitmap 객체</returns>
        Bitmap ToBitmap();
    }


    /// <summary>
    /// 이미지의 Depth(bit)변환을 위해 사용하는 인터페이스
    /// </summary>
    public interface IKConvertDepth
    {
        /// <summary>
        /// 8bit 흑백 이미지 Mat객체로 변환한다
        /// </summary>
        /// <param name="src">변환할 Mat객체</param>
        /// <returns></returns>
        Mat To8BitG(Mat src);

        /// <summary>
        /// 16bit 흑백 이미지 Mat객체로 변환한다
        /// </summary>
        /// <param name="src"></param>
        /// <returns>변환할 Mat객체</returns>
        Mat To16BitG(Mat src);

        /// <summary>
        /// 24bit 칼라 이미지 Mat객체로 변환한다
        /// </summary>
        /// <param name="src"></param>
        /// <returns>변환할 Mat객체</returns>
        Mat To24BitC(Mat src);
    }
}
