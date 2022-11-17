using JTeksSplineGraph.Controls;
using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XManager.ImageProcessingData
{
    public class ProcessingTime
    {
        public string Type { get; set; }
        public string Time { get; set; }
    }

    public abstract class IParamBase : ICloneable
    {
        public abstract object Clone();
    }
    public class IpThresholdOtsuParams : IParamBase
    {
        public int value;

        public override object Clone()
        {
            IpThresholdOtsuParams otsu = new IpThresholdOtsuParams();
            otsu.value = this.value;
            return otsu;
        }
    }

    public class IpThresholdAdaptiveParams : IParamBase
    {
        public ThresAdaptiveType type;
        public int BlockSize;
        public int Weight; // 가중치

        public IpThresholdAdaptiveParams()
        {
            type = ThresAdaptiveType.Gaussian;
            this.BlockSize = 3;
            this.Weight = 1;
        }

        public override object Clone()
        {
            IpThresholdAdaptiveParams adaptive = new IpThresholdAdaptiveParams();
            adaptive.type = this.type;
            adaptive.BlockSize = this.BlockSize;
            adaptive.Weight = this.Weight;
            return adaptive;
        }
    }

    public class IpThresholdParams : IParamBase
    {
        public int Value;

        public IpThresholdParams()
        {
            this.Value = 60;
        }
        public override object Clone()
        {
            IpThresholdParams threshold = new IpThresholdParams();
            threshold.Value = this.Value;
            return threshold;
        }
    }

    public class IpColorLevelingParams : IParamBase
    {
        public int Low;
        public int High;

        public IpColorLevelingParams()
        {
            if (CStatus.Instance().NULLCheckDrawBox())
            {
                Low = 0;
                High = 255;
                return;
            }
            KDepthType bitType = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Depth;
            if (bitType == KDepthType.Dt_8)
            {
                Low = 0;
                High = 255;
            }
            else if (bitType == KDepthType.Dt_8)
            {
                Low = 0;
                High = 65535;
            }
            else
            {
                Low = 0;
                High = 255;
            }
        }

        public override object Clone()
        {
            IpColorLevelingParams level = new IpColorLevelingParams();
            level.Low = this.Low;
            level.High = this.High;
            return level;
        }
    }

    public class IpGreyLevelingParams : IParamBase
    {
        public int Low;
        public int High;

        public IpGreyLevelingParams()
        {
            if (CStatus.Instance().NULLCheckDrawBox())
            {
                Low = 0;
                High = 255;
                return;
            }
            KDepthType bitType = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Depth;
            if (bitType == KDepthType.Dt_8)
            {
                Low = 0;
                High = 255;
            }
            else if (bitType == KDepthType.Dt_8)
            {
                Low = 0;
                High = 65535;
            }
            else
            {
                Low = 0;
                High = 255;
            }
        }

        public override object Clone()
        {
            IpGreyLevelingParams level = new IpGreyLevelingParams();
            level.Low = this.Low;
            level.High = this.High;
            return level;
        }
    }

    public class IpLutParams : IParamBase
    {
        public List<PointF> keyPt;
        public float Width { get; set; }
        public float Height { get; set; }
        public int[] LUT { get; set; }

        public IpLutParams()
        {
            this.keyPt = new List<PointF>();
            ImageCurve curve = new ImageCurve();
            curve.ImageBit = CStatus.Instance().GetDrawBox().ImageManager.GetBit();
            tCurveDataInfo info = curve.GetInformation();
            this.keyPt = info.keyPt;
            this.Width = info.Width;
            this.Height = info.Height;
            this.LUT = info.LUT;
        }

        public override object Clone()
        {
            IpLutParams lut = new IpLutParams();
            lut.keyPt.Clear();
            lut.keyPt.AddRange(this.keyPt);
            lut.Width = this.Width;
            lut.Height = this.Height;
            lut.LUT = this.LUT.ToArray();

            return lut;
        }
    }

    public class IpCropParams : IParamBase
    {
        private Rectangle _roi;
        public Rectangle Roi { get { return _roi; } set { _roi = value; } }

        public IpCropParams()
        {
            this.Roi = new Rectangle();
        }
        public override object Clone()
        {
            IpCropParams crop = new IpCropParams();
            crop.Roi = new Rectangle(this.Roi.X, this.Roi.Y, this.Roi.Width, this.Roi.Height);

            return crop;
        }
    }

    public class IpBlurParams : IParamBase
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public IpBlurParams()
        {
            this.Width = 3;
            this.Height = 3;
        }

        public override object Clone()
        {
            IpBlurParams blur = new IpBlurParams();
            blur.Width = this.Width;
            blur.Height = this.Height;
            return blur;
        }
    }

    public class IpResizeParams : IParamBase
    {
        public double WidthScale { get; set; }
        public double HeightScale { get; set; }
        public KInterpolation AlgorithmType { get; set; }

        public IpResizeParams()
        {
            this.WidthScale = 1;
            this.HeightScale = 1;
            AlgorithmType = KInterpolation.Linear;
        }

        public override object Clone()
        {
            IpResizeParams resize = new IpResizeParams();
            resize.WidthScale = this.WidthScale;
            resize.HeightScale = this.HeightScale;
            resize.AlgorithmType = this.AlgorithmType;

            return resize;
        }
    }

    public class IpSobelParams : IParamBase
    {
        public int Xorder { get; set; }
        public int Yorder { get; set; }
        public int ApertureSize { get; set; }

        public IpSobelParams()
        {
            this.Xorder = 1;
            this.Yorder = 1;
            this.ApertureSize = 3;
        }

        public override object Clone()
        {
            IpSobelParams sobel = new IpSobelParams();
            sobel.Xorder = this.Xorder;
            sobel.Yorder = this.Yorder;
            sobel.ApertureSize = this.ApertureSize;
            return sobel;
        }
    }

    public class IpLaplacianParams : IParamBase
    {
        public int ApertureSize { get; set; }

        public IpLaplacianParams()
        {
            this.ApertureSize = 3;
        }

        public override object Clone()
        {
            IpLaplacianParams laplacian = new IpLaplacianParams();
            laplacian.ApertureSize = this.ApertureSize;
            return ApertureSize;
        }
    }

    public class IpCannyParams : IParamBase
    {
        public int Thresh { get; set; }
        public int ThreshLinking { get; set; }

        public IpCannyParams()
        {
            this.Thresh = 30;
            this.ThreshLinking = 200;
        }

        public override object Clone()
        {
            IpCannyParams canny = new IpCannyParams();
            canny.Thresh = this.Thresh;
            canny.ThreshLinking = this.ThreshLinking;

            return canny;
        }
    }

    public class IpMedianParams : IParamBase
    {
        public int Size { get; set; }

        public IpMedianParams()
        {
            this.Size = 3;
        }

        public override object Clone()
        {
            IpMedianParams median = new IpMedianParams();
            median.Size = this.Size;
            return median;
        }
    }

    public class IpDilateParams : IParamBase
    {
        public int Iterations { get; set; }

        public IpDilateParams()
        {
            this.Iterations = 1;
        }

        public override object Clone()
        {
            IpDilateParams dilate = new IpDilateParams();
            dilate.Iterations = this.Iterations;
            return dilate;
        }
    }

    public class IpErodeParams : IParamBase
    {
        public int Iterations { get; set; }

        public IpErodeParams()
        {
            this.Iterations = 1;
        }

        public override object Clone()
        {
            IpErodeParams erode = new IpErodeParams();
            erode.Iterations = this.Iterations;
            return erode;
        }
    }

    public class IpUserFilterParams : IParamBase
    {
        private object _kernel;
        public object Kernel { get { return _kernel; } set { _kernel = value; } }

        public override object Clone()
        {
            IpUserFilterParams filter = new IpUserFilterParams();
            return filter;
        }
    }

}
