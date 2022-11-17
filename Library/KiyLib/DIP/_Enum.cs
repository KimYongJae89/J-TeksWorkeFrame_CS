using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.DIP
{
    [Flags]
    public enum KFlipType
    {
        None = 1,       // No flipping
        Horizontal = 2, // Flip horizontally
        Vertical = 4    // Flip vertically
    }

    public enum KRotateType
    {
        //None = 0,
        CW_90 = 0,
        CW_180 = 1,
        CW_270 = 2,

        CCW_90 = 3,
        CCW_180 = 4,
        CCW_270 = 5,

        //CCW_90 = 2,
        //CCW_180 = 1,
        //CCW_270 = 0
    }

    public enum KDepthType
    {
        None = 0,
        Dt_8 = 8,       // byte
        Dt_16 = 16,     // ushort   16비트는 흑백Tiff만 사용
        Dt_24 = 24,     // int
        //Dt_32 = 32    // Alpha채널을 지원하지 않기 때문에 사용하지 않는다
    }

    public enum KColorType
    {
        Gray, Color
    }

    public enum KBinaryColorType
    {
        Black, White
    }

    public enum KColorChannel
    {
        B, G, R
    }

    public enum KConnectivity
    {
        Neighbors4 = 4, Neighbors8 = 8
    }


    public enum KImageFormat
    {
        None = 0, RAW, TIFF, BMP, JPEG, PNG, DCM
    }


    public enum KRectCorner
    {
        NONE, LT, RT, LB, RB
    }

    public enum KArith
    {
        Add, Sub, Mult, Div
    }

    public enum Filter
    {
        None, Sharpen, MeanRemoval, Smooth, GaussianBlur, EmbossLaplacian, EdgeDetectQuick
    }

    public enum KInterpolation
    {
        //
        // 요약:
        //     Nearest-neighbor interpolation
        Nearest = 0,
        //
        // 요약:
        //     Bilinear interpolation
        Linear = 1,
        //
        // 요약:
        //     Resampling using pixel area relation. It is the preferred method for image decimation
        //     that gives moire-free results. In case of zooming it is similar to CV_INTER_NN
        //     method
        Cubic = 2,
        //
        // 요약:
        //     Bicubic interpolation
        Area = 3,
        //
        // 요약:
        //     Lanczos interpolation over 8x8 neighborhood
        Lanczos4 = 4,
        //
        // 요약:
        //     Bit exact bilinear interpolation
        LinearExact = 5
    }

    //DFT에 사용
    public enum KDxtType
    {
        Forward, Inverse
    }

    public enum KFilter
    {
        Blur, Sharp1, Sharp2, Sobel, SobelX, SobelY,
        Laplacian, Canny, Median, Dilate, Erode, Average, Basic
    }


    public enum LineProFileAlgo
    {
        Bresenham,  //Bresenham's line algorithm
        DDA         //DigitalDifferentialAnalyzer - 매트록스에서 사용
    }

    // Threshold
    public enum ThresAdaptiveType
    {
        Mean = 0, Gaussian = 1
    }
}
