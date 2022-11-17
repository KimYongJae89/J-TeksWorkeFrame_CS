using KiyEmguCV.DIP;
using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XManager.Controls;
using XManager.FigureData;
using XManager.ImageProcessingData;
using XManager.Util;

namespace XManager
{
    //public enum eProfileDrawType
    //{
    //    None, OneDerivative, TwoDerivative,
    //}

    public enum eImageEditting
    {
        None,
        CW90,
        CW180,
        CW270,
        CCW90,
        CCW180,
        CCW270,
        FlipX,
        FlipY,
        Resize,
        Crop,
    }

    public class ColorParam
    {
        public int Grey;
        public int R;
        public int G;
        public int B;
    }

    public class ColorHistogram
    {
        public int[] R;
        public int[] G;
        public int[] B;
    }

    public class ImageEditParam
    {
        public eImageEditting type = eImageEditting.None;
        public float Left { get; set; }
        public float Top { get; set; }
        public float Right { get; set; }
        public float Bottom { get; set; }
    }

    public class LutParams
    {
        public int LutWidth { get; set; }
        public int LutHeight { get; set; }
        public List<PointF> RealPoints = new List<PointF>(); // 1Pixel 당 1 Vaule 일 경우의 좌표
    }

    public class GreyHistogramParams
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Avg { get; set; }
        public float[] HistogramValue { get; set; }
    }

    public class ColorHistogramParams
    {
        public int R_Min { get; set; }
        public int R_Max { get; set; }
        public int R_Avg { get; set; }
        public int G_Min { get; set; }
        public int G_Max { get; set; }
        public int G_Avg { get; set; }
        public int B_Min { get; set; }
        public int B_Max { get; set; }
        public int B_Avg { get; set; }
        public float[] R_HistogramValue { get; set; }
        public float[] G_HistogramValue { get; set; }
        public float[] B_HistogramValue { get; set; }
    }

    public class SettingParam
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public SettingParam(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    public class Level
    {
        private int _min_Grey_8 = 0;
        public int Min_Grey_8
        {
            get { return _min_Grey_8; }
            set { _min_Grey_8 = value; }
        }

        private int _max_Grey_8 = 255;
        public int Max_Grey_8
        {
            get { return _max_Grey_8; }
            set { _max_Grey_8 = value; }
        }

        private int _min_R_8 = 0;
        public int Min_R_8
        {
            get { return _min_R_8; }
            set { _min_R_8 = value; }
        }

        private int _max_R_8 = 255;
        public int Max_R_8
        {
            get { return _max_R_8; }
            set { _max_R_8 = value; }
        }

        private int _min_G_8 = 0;
        public int Min_G_8
        {
            get { return _min_G_8; }
            set { _min_G_8 = value; }
        }

        private int _max_G_8 = 255;
        public int Max_G_8
        {
            get { return _max_G_8; }
            set { _max_G_8 = value; }
        }

        private int _min_B_8 = 0;
        public int Min_B_8
        {
            get { return _min_B_8; }
            set { _min_B_8 = value; }
        }

        private int _max_B_8 = 255;
        public int Max_B_8
        {
            get { return _max_B_8; }
            set { _max_B_8 = value; }
        }

        private int _min_16 = 0;
        public int Min_16
        {
            get { return _min_16; }
            set { _min_16 = value; }
        }

        private int _max_16 = 65535;
        public int Max_16
        {
            get { return _max_16; }
            set { _max_16 = value; }
        }
    }

    public class EmguImageWrapper
    {
        private JImage _orgImage { get; set; }
        public JImage CalcImage { get; set; }  // ImageProcessingList가 실행된 이미지
        public JImage DisPlayImage { get; set; } // 실제 출력 이미지
        public LutParams LutParams = new LutParams();
        public List<ImageEditParam> _ImageEditParams = new List<ImageEditParam>();
        public List<IPBase> ImageProcessingList = new List<IPBase>();
        public string ImageFilePath;
        public int[] _lut = null;
        public int[] LUT
        {
            get { return _lut; }
            set { _lut = value; }
        }

        public Level Leveling = new Level();

        public event Action DisplayUpadate;

        public int GetBufferSize()
        {
            int size = 0;
            int bit = GetBit();

            if(bit == 24)
                size = (int)Math.Pow(2, 8);
            else
                size =(int)Math.Pow(2, GetBit());

            return size;
        }

        public int GetBit()
        {
            int bit = 0;
            switch (_orgImage.Depth)
            {
                case KDepthType.Dt_8:
                    bit = 8;
                    break;
                case KDepthType.Dt_16:
                    bit = 16;
                    break;
                case KDepthType.Dt_24:
                    bit = 24;
                    break;
             case KDepthType.None:
                default:
                    break;
            }
            return bit;
        }

        public bool IsColor()
        {
            bool isColor = false;
            switch (_orgImage.Color)
            {
                case KColorType.Gray:
                    isColor = false;
                    break;
                case KColorType.Color:
                    isColor = true;
                    break;
                default:
                    break;
            }
            return isColor;
        }

        public JImage LoadImage(string filePath)
        {
            ImageFilePath = filePath;
            JImage img = new JImage(filePath);
            _orgImage = (JImage)img.Clone();
            DisPlayImage = _orgImage;
            ImageProcessingList.Clear();

            return _orgImage;
        }

        public void SetOrgImage(JImage image)
        {
            _orgImage = (JImage)image.Clone();
            DisPlayImage = _orgImage;
        }

        public JImage GetOrgImage()
        {
            return _orgImage;
        }
        
        public ColorParam GetPixelValue(PointF point)
        {
            ColorParam ret = new ColorParam();
            if (point.X > DisPlayImage.Width - 1)
                point.X = DisPlayImage.Width - 1;
            if (point.X < 0)
                point.X = 0;
            if (point.Y > DisPlayImage.Height - 1)
                point.Y = DisPlayImage.Height - 1;
            if (point.Y < 0)
                point.Y = 0;

            if (DisPlayImage.Color == KColorType.Gray)
            {
                ret.Grey = DisPlayImage.GetPixelGray((int)point.X, (int)point.Y);
            }
            else
            {
                KColor color = DisPlayImage.GetPixelColor((int)point.X, (int)point.Y);
                ret.R = color.R;
                ret.G = color.G;
                ret.B = color.B;
            }

            return ret;
        }

        public float[] GetGreyHistogram()
        {
            int[] greyHistogram;
            List<IPBase> te = ImageProcessingList;
            DisPlayImage.GetHistoGray(out greyHistogram);
            float[] ret = Array.ConvertAll(greyHistogram, new Converter<int, float>(ConvertIntToFloat));
            return ret;
        }

        public float[] GetGreyHistogram(JImage image)
        {
            int[] greyHistogram;

            image.GetHistoGray(out greyHistogram);
            float[] ret = Array.ConvertAll(greyHistogram, new Converter<int, float>(ConvertIntToFloat));
            return ret;
        }


        private float ConvertIntToFloat(int input)
        {
            return (float)input;
        }

        public ColorHistogram GetColorHistogram()
        {
            ColorHistogram param = new ColorHistogram();
            DisPlayImage.GetHistoColor(out param.B, out param.G, out param.R);
            return param;
        }

        public ColorHistogram GetColorHistogram(JImage image)
        {
            ColorHistogram param = new ColorHistogram();
            image.GetHistoColor(out param.B, out param.G, out param.R);
            return param;
        }

        public GreyHistogramParams GetGreyRoiHistogramParam(RectangleF rect)
        {
            GreyHistogramParams param = new GreyHistogramParams();
            Rectangle roi = new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);

            KRoiCV roiInfo = DisPlayImage.GetROIInfo(roi);
            int[] dst;
            roiInfo.GetHistoGray(out dst);
            int minIndex = 0;
            int minValue = 0;
            roiInfo.GetHistoMinGray(out minIndex, out minValue);
            int maxIndex = 0;
            int maxValue = 0;
            roiInfo.GetHistoMaxGray(out maxIndex, out maxValue);

            double avg = roiInfo.GetHistoAvgGray();

            param.Min = minValue;
            param.Max = maxValue;
            param.Avg = (int)avg;

            param.HistogramValue = Array.ConvertAll(dst, new Converter<int, float>(ConvertIntToFloat));
            return param;
        }

        public ColorHistogramParams GetColorRoiHistogramParam(RectangleF rect)
        {

            Rectangle roi = new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);

            KRoiCV roiInfo = DisPlayImage.GetROIInfo(roi);

            int rMinIndex, gMinIndex, bMinIndex, rMaxIndex, gMaxIndex, bMaxIndex;
            int rMinValue, gMinValue, bMinValue;
            int rMaxValue, gMaxValue, bMaxValue;
            int[] rDst, gDst, bDst;
            double rAvg, gAvg, bAvg;

            roiInfo.GetHistoMinColor(out bMinIndex, out bMinValue, out gMinIndex, out gMinValue, out rMinIndex, out rMinValue);
            roiInfo.GetHistoMaxColor(out bMaxIndex, out bMaxValue, out gMaxIndex, out gMaxValue, out rMaxIndex, out rMaxValue);
            roiInfo.GetHistoColor(out bDst, out gDst, out rDst);
            roiInfo.GetHistoAvgColor(out bAvg, out gAvg, out rAvg);

            ColorHistogramParams param = new ColorHistogramParams();
            param.R_Min = rMinValue; param.R_Max = rMaxValue; param.R_Avg = (int)rAvg;
            param.G_Min = gMinValue; param.G_Max = gMaxValue; param.G_Avg = (int)gAvg;
            param.B_Min = bMinValue; param.B_Max = bMaxValue; param.B_Avg = (int)bAvg;
            param.R_HistogramValue = Array.ConvertAll(rDst, new Converter<int, float>(ConvertIntToFloat));
            param.G_HistogramValue = Array.ConvertAll(gDst, new Converter<int, float>(ConvertIntToFloat));
            param.B_HistogramValue = Array.ConvertAll(bDst, new Converter<int, float>(ConvertIntToFloat));

            return param;
        }

        public JImage CalcDisplayImage()
        {
            //Console.WriteLine("들어옴");
            if (_orgImage == null)
                return null;
            JImage destImage = (JImage)_orgImage.Clone();
            foreach (IPBase item in ImageProcessingList)
            {
                if(item.ProcessType == eImageProcessType.LUT)
                {
                    IpLutParams param = (IpLutParams)item.GetParam();
                    if (param.LUT == null)
                        continue;
                }
                destImage = item.Execute(destImage);
            }
            CalcImage = destImage;
            DisPlayImage = destImage;
           
            return DisPlayImage;
        }

        public List<HistogramParams> GetSelectedHistogramParam()
        {
            if (DisPlayImage == null)
                return null;

            List<HistogramParams> paramList = new List<HistogramParams>();
            if (!IsColor())
            {
                HistogramParams param = new HistogramParams();
                param.HistogramValue = GetGreyHistogram();
                param.GraphColor = Color.Gray;
                param.Width = DisPlayImage.Width;
                param.Height = DisPlayImage.Height;
                paramList.Add(param);
            }
            else
            {
                ColorHistogram histograms = GetColorHistogram();

                HistogramParams redParam = new HistogramParams();
                redParam.HistogramValue = Array.ConvertAll(histograms.R, new Converter<int, float>(ConvertIntToFloat));
                redParam.GraphColor = Color.Red;
                redParam.Width = DisPlayImage.Width;
                redParam.Height = DisPlayImage.Height;
                ////
                HistogramParams greenParam = new HistogramParams();
                greenParam.HistogramValue = Array.ConvertAll(histograms.G, new Converter<int, float>(ConvertIntToFloat));
                greenParam.GraphColor = Color.Green;
                greenParam.Width = DisPlayImage.Width;
                greenParam.Height = DisPlayImage.Height;
                ////
                HistogramParams blueParam = new HistogramParams();
                blueParam.HistogramValue = Array.ConvertAll(histograms.B, new Converter<int, float>(ConvertIntToFloat));
                blueParam.GraphColor = Color.Blue;
                blueParam.Width = DisPlayImage.Width;
                blueParam.Height = DisPlayImage.Height;

                paramList.Add(redParam);
                paramList.Add(greenParam);
                paramList.Add(blueParam);
            }
            return paramList;
        }

        public List<HistogramParams> GetSelectedHistogramParam(JImage image)
        {
            if (image == null)
                return null;

            List<HistogramParams> paramList = new List<HistogramParams>();
            if (!IsColor())
            {
                HistogramParams param = new HistogramParams();
                param.HistogramValue = GetGreyHistogram(image);
                param.GraphColor = Color.Gray;
                param.Width = DisPlayImage.Width;
                param.Height = DisPlayImage.Height;
                paramList.Add(param);
            }
            else
            {
                ColorHistogram histograms = GetColorHistogram(image);

                HistogramParams redParam = new HistogramParams();
                redParam.HistogramValue = Array.ConvertAll(histograms.R, new Converter<int, float>(ConvertIntToFloat));
                redParam.GraphColor = Color.Red;
                redParam.Width = DisPlayImage.Width;
                redParam.Height = DisPlayImage.Height;
                ////
                HistogramParams greenParam = new HistogramParams();
                greenParam.HistogramValue = Array.ConvertAll(histograms.G, new Converter<int, float>(ConvertIntToFloat));
                greenParam.GraphColor = Color.Green;
                greenParam.Width = DisPlayImage.Width;
                greenParam.Height = DisPlayImage.Height;
                ////
                HistogramParams blueParam = new HistogramParams();
                blueParam.HistogramValue = Array.ConvertAll(histograms.B, new Converter<int, float>(ConvertIntToFloat));
                blueParam.GraphColor = Color.Blue;
                blueParam.Width = DisPlayImage.Width;
                blueParam.Height = DisPlayImage.Height;

                paramList.Add(redParam);
                paramList.Add(greenParam);
                paramList.Add(blueParam);
            }
            return paramList;
        }

        public List<HistogramParams> GetRoiHistogramParamList()
        {
            if (DisPlayImage == null)
                return null;
            RectangleFigure selectedRoi = CStatus.Instance().GetDrawBox().TrackerManager.GetSelectedRoi();

            if (selectedRoi == null)
            {
                return null;
            }

            List<HistogramParams> paramList = new List<HistogramParams>();
            if (!IsColor())
            {
                HistogramParams param = new HistogramParams();

                RectangleF orgRect = ((tRectangleResult)selectedRoi.GetResult()).resultRectangle;

                param.HistogramValue = CStatus.Instance().GetDrawBox().ImageManager.GetGreyRoiHistogramParam(orgRect).HistogramValue;
                param.GraphColor = Color.Gray;
                param.Mark1Value = 0;
                param.Mark2Value = param.HistogramValue.Count();

                param.Width = DisPlayImage.Width;
                param.Height = DisPlayImage.Height;

                paramList.Add(param);
            }
            else
            {
                int histogramMax = GetHistogramLength(CStatus.Instance().GetDrawBox().ImageManager.GetBit(), true) - 1;

                RectangleF orgRect = ((tRectangleResult)selectedRoi.GetResult()).resultRectangle;

                ColorHistogramParams roiParam = CStatus.Instance().GetDrawBox().ImageManager.GetColorRoiHistogramParam(orgRect);

                HistogramParams rParam = new HistogramParams();
                rParam.HistogramValue = roiParam.R_HistogramValue;
                rParam.GraphColor = Color.Red;
                rParam.Width = DisPlayImage.Width;
                rParam.Height = DisPlayImage.Height;

                HistogramParams gParam = new HistogramParams();
                gParam.HistogramValue = roiParam.G_HistogramValue;
                gParam.GraphColor = Color.Green;
                gParam.Width = DisPlayImage.Width;
                gParam.Height = DisPlayImage.Height;

                HistogramParams bParam = new HistogramParams();
                bParam.HistogramValue = roiParam.B_HistogramValue;
                bParam.GraphColor = Color.Blue;
                bParam.Width = DisPlayImage.Width;
                bParam.Height = DisPlayImage.Height;

                rParam.Mark1Value = 0;
                rParam.Mark2Value = rParam.HistogramValue.Count();

                gParam.Mark1Value = 0;
                gParam.Mark2Value = gParam.HistogramValue.Count();

                bParam.Mark1Value = 0;
                bParam.Mark2Value = bParam.HistogramValue.Count();

                paramList.Add(rParam);
                paramList.Add(gParam);
                paramList.Add(bParam);
            }
            return paramList;
        }
        //작업중

        public List<HistogramParams> GetProfileHistogramParamList()
        {
            if (DisPlayImage == null)
                return null;

            List<HistogramParams> paramList = new List<HistogramParams>();
            ProfileFigure selectedProfile = CStatus.Instance().GetDrawBox().TrackerManager.GetSelectedProfile();

            if (selectedProfile == null)
                return null;

            if (!IsColor())
            {
                HistogramParams param = new HistogramParams();

                float pictureBoxWidth = CStatus.Instance().GetDrawBox().GetPictureBox().Width;
                float pictureBoxHeight = CStatus.Instance().GetDrawBox().GetPictureBox().Height;
                float displayImageWidth = (float)CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width;
                float displayImageHeight = (float)CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height;



                tProfileResult result = (tProfileResult)selectedProfile.GetResult();
                PointF startPoint = CStatus.Instance().GetDrawBox().ImageManager.GetOrgPoint(result.StartPoint);
                PointF endPoint = CStatus.Instance().GetDrawBox().ImageManager.GetOrgPoint(result.EndPoint);

                param.Mark1Value = result.Mark1;
                param.Mark2Value = result.Mark2;
                param.Width = DisPlayImage.Width;
                param.Height = DisPlayImage.Height;

                var profileInfo = DisPlayImage.GetLineProfileInfo((int)startPoint.X, (int)startPoint.Y, (int)endPoint.X, (int)endPoint.Y);
                KLineProfileGrayCV greyProfileInfo = profileInfo as KLineProfileGrayCV;

                int width = CStatus.Instance().ProfileWidth;
                if (width > 1)
                {
                    List<Point> rectPts = greyProfileInfo.GetProjectionRectPoints(width);
                }
                if (CStatus.Instance().DerivativeType == eDerivativeType.None)
                {
                    List<Tuple<Point, float>> greyInfo = greyProfileInfo.GetPixelDatasByProjection(width);

                    float[] histogram = new float[greyInfo.Count];
                    for (int i = 0; i < greyInfo.Count; i++)
                        histogram[i] = greyInfo[i].Item2;

                    param.HistogramValue = histogram;

                    param.Points = new List<PointF>(greyProfileInfo.PointsInLine.Count());
                    for (int i = 0; i < greyProfileInfo.PointsInLine.Count; i++)
                    {
                        PointF calcPoint = new PointF();
                        calcPoint.X = greyProfileInfo.PointsInLine[i].X * pictureBoxWidth / displayImageWidth;
                        calcPoint.Y = greyProfileInfo.PointsInLine[i].X * pictureBoxHeight / displayImageHeight;
                        param.Points.Add(calcPoint);
                    }
                }
                else if (CStatus.Instance().DerivativeType == eDerivativeType.OneDerivative)
                {
                    List<Tuple<Point, float>> derivativeInfo = greyProfileInfo.Calc1stDerivative(width);
                    float[] histogram = new float[derivativeInfo.Count];
                    for (int i = 0; i < derivativeInfo.Count; i++)
                        histogram[i] = derivativeInfo[i].Item2;

                    param.HistogramValue = histogram;
                    param.Points = new List<PointF>(greyProfileInfo.PointsInLine.Count() - 1);
                    for (int i = 1; i < greyProfileInfo.PointsInLine.Count; i++)
                    {
                        param.Points.Add(greyProfileInfo.PointsInLine[i]);
                    }

                }
                else if (CStatus.Instance().DerivativeType == eDerivativeType.TwoDerivative)
                {
                    List<Tuple<Point, float>> derivativeInfo = greyProfileInfo.Calc2stDerivative(width);
                    float[] histogram = new float[derivativeInfo.Count];
                    for (int i = 0; i < derivativeInfo.Count; i++)
                        histogram[i] = derivativeInfo[i].Item2;

                    param.HistogramValue = histogram;

                    param.Points = new List<PointF>(greyProfileInfo.PointsInLine.Count() - 2);
                    for (int i = 2; i < greyProfileInfo.PointsInLine.Count; i++)
                    {
                        param.Points.Add(greyProfileInfo.PointsInLine[i]);
                    }

                }
                param.GraphColor = Color.Gray;
                paramList.Add(param);
            }
            else
            {
                tProfileResult result = (tProfileResult)selectedProfile.GetResult();
                PointF startPoint = CStatus.Instance().GetDrawBox().ImageManager.GetOrgPoint(result.StartPoint);
                PointF endPoint = CStatus.Instance().GetDrawBox().ImageManager.GetOrgPoint(result.EndPoint);


                var profileInfo = DisPlayImage.GetLineProfileInfo((int)startPoint.X, (int)startPoint.Y, (int)endPoint.X, (int)endPoint.Y);
                KLineProfileColorCV colorProfileInfo = profileInfo as KLineProfileColorCV;

                int width = CStatus.Instance().ProfileWidth;
                if (CStatus.Instance().DerivativeType == eDerivativeType.None)
                {
                    List<Tuple<Point, KColorF>> colorInfo = colorProfileInfo.GetPixelDatasByProjection(width);

                    float[] rHistogram = new float[colorInfo.Count];
                    float[] gHistogram = new float[colorInfo.Count];
                    float[] bHistogram = new float[colorInfo.Count];
                    for (int i = 0; i < colorInfo.Count; i++)
                    {
                        rHistogram[i] = colorInfo[i].Item2.R;
                        gHistogram[i] = colorInfo[i].Item2.G;
                        bHistogram[i] = colorInfo[i].Item2.B;
                    }
                    HistogramParams rParam = new HistogramParams();
                    rParam.HistogramValue = rHistogram;
                    rParam.GraphColor = Color.Red;
                    rParam.Width = DisPlayImage.Width;
                    rParam.Height = DisPlayImage.Height;

                    HistogramParams gParam = new HistogramParams();
                    gParam.HistogramValue = gHistogram;
                    gParam.GraphColor = Color.Green;
                    gParam.Width = DisPlayImage.Width;
                    gParam.Height = DisPlayImage.Height;

                    HistogramParams bParam = new HistogramParams();
                    bParam.HistogramValue = bHistogram;
                    bParam.GraphColor = Color.Blue;
                    bParam.Width = DisPlayImage.Width;
                    bParam.Height = DisPlayImage.Height;

                    rParam.Points = new List<PointF>(profileInfo.PointsInLine.Count());
                    gParam.Points = new List<PointF>(profileInfo.PointsInLine.Count());
                    bParam.Points = new List<PointF>(profileInfo.PointsInLine.Count());
                    for (int i = 0; i < profileInfo.PointsInLine.Count; i++)
                    {
                        rParam.Points.Add(profileInfo.PointsInLine[i]);
                        gParam.Points.Add(profileInfo.PointsInLine[i]);
                        bParam.Points.Add(profileInfo.PointsInLine[i]);
                    }

                    rParam.Mark1Value = result.Mark1;
                    rParam.Mark2Value = result.Mark2;
                    gParam.Mark1Value = result.Mark1;
                    gParam.Mark2Value = result.Mark2;
                    bParam.Mark1Value = result.Mark1;
                    bParam.Mark2Value = result.Mark2;

                    paramList.Add(rParam);
                    paramList.Add(gParam);
                    paramList.Add(bParam);
                }
                else if (CStatus.Instance().DerivativeType == eDerivativeType.OneDerivative)
                {
                    List<Tuple<Point, KColorF>> derivativeInfo = colorProfileInfo.Calc1stDerivative(width);
                    float[] rHistogram = new float[derivativeInfo.Count];
                    float[] gHistogram = new float[derivativeInfo.Count];
                    float[] bHistogram = new float[derivativeInfo.Count];
                    for (int i = 0; i < derivativeInfo.Count; i++)
                    {
                        rHistogram[i] = derivativeInfo[i].Item2.R;
                        gHistogram[i] = derivativeInfo[i].Item2.G;
                        bHistogram[i] = derivativeInfo[i].Item2.B;
                    }
                    HistogramParams rParam = new HistogramParams();
                    rParam.HistogramValue = rHistogram;
                    rParam.GraphColor = Color.Red;
                    rParam.Width = DisPlayImage.Width;
                    rParam.Height = DisPlayImage.Height;

                    HistogramParams gParam = new HistogramParams();
                    gParam.HistogramValue = gHistogram;
                    gParam.GraphColor = Color.Green;
                    gParam.Width = DisPlayImage.Width;
                    gParam.Height = DisPlayImage.Height;

                    HistogramParams bParam = new HistogramParams();
                    bParam.HistogramValue = bHistogram;
                    bParam.GraphColor = Color.Blue;
                    bParam.Width = DisPlayImage.Width;
                    bParam.Height = DisPlayImage.Height;

                    rParam.Points = new List<PointF>(profileInfo.PointsInLine.Count());
                    gParam.Points = new List<PointF>(profileInfo.PointsInLine.Count());
                    bParam.Points = new List<PointF>(profileInfo.PointsInLine.Count());
                    for (int i = 0; i < profileInfo.PointsInLine.Count; i++)
                    {
                        rParam.Points.Add(profileInfo.PointsInLine[i]);
                        gParam.Points.Add(profileInfo.PointsInLine[i]);
                        bParam.Points.Add(profileInfo.PointsInLine[i]);
                    }

                    rParam.Mark1Value = result.Mark1;
                    rParam.Mark2Value = result.Mark2;
                    gParam.Mark1Value = result.Mark1;
                    gParam.Mark2Value = result.Mark2;
                    bParam.Mark1Value = result.Mark1;
                    bParam.Mark2Value = result.Mark2;

                    paramList.Add(rParam);
                    paramList.Add(gParam);
                    paramList.Add(bParam);
                }
                else if (CStatus.Instance().DerivativeType == eDerivativeType.TwoDerivative)
                {
                    List<Tuple<Point, KColorF>> derivativeInfo = colorProfileInfo.Calc2stDerivative(width);
                    float[] rHistogram = new float[derivativeInfo.Count];
                    float[] gHistogram = new float[derivativeInfo.Count];
                    float[] bHistogram = new float[derivativeInfo.Count];
                    for (int i = 0; i < derivativeInfo.Count; i++)
                    {
                        rHistogram[i] = derivativeInfo[i].Item2.R;
                        gHistogram[i] = derivativeInfo[i].Item2.G;
                        bHistogram[i] = derivativeInfo[i].Item2.B;
                    }
                    HistogramParams rParam = new HistogramParams();
                    rParam.HistogramValue = rHistogram;
                    rParam.GraphColor = Color.Red;
                    rParam.Width = DisPlayImage.Width;
                    rParam.Height = DisPlayImage.Height;

                    HistogramParams gParam = new HistogramParams();
                    gParam.HistogramValue = gHistogram;
                    gParam.GraphColor = Color.Green;
                    gParam.Width = DisPlayImage.Width;
                    gParam.Height = DisPlayImage.Height;

                    HistogramParams bParam = new HistogramParams();
                    bParam.HistogramValue = bHistogram;
                    bParam.GraphColor = Color.Blue;
                    bParam.Width = DisPlayImage.Width;
                    bParam.Height = DisPlayImage.Height;

                    rParam.Points = new List<PointF>(profileInfo.PointsInLine.Count());
                    gParam.Points = new List<PointF>(profileInfo.PointsInLine.Count());
                    bParam.Points = new List<PointF>(profileInfo.PointsInLine.Count());
                    for (int i = 0; i < profileInfo.PointsInLine.Count; i++)
                    {
                        rParam.Points.Add(profileInfo.PointsInLine[i]);
                        gParam.Points.Add(profileInfo.PointsInLine[i]);
                        bParam.Points.Add(profileInfo.PointsInLine[i]);
                    }

                    rParam.Mark1Value = result.Mark1;
                    rParam.Mark2Value = result.Mark2;
                    gParam.Mark1Value = result.Mark1;
                    gParam.Mark2Value = result.Mark2;
                    bParam.Mark1Value = result.Mark1;
                    bParam.Mark2Value = result.Mark2;

                    paramList.Add(rParam);
                    paramList.Add(gParam);
                    paramList.Add(bParam);
                }
            }
            return paramList;
        }

        public GreyHistogramParams GetGreyProfileHistogramParam(Point startPoint, Point endPoint)
        {
            if (DisPlayImage == null)
                return null;
            GreyHistogramParams param = new GreyHistogramParams();

            var profileInfo = DisPlayImage.GetLineProfileInfo((int)startPoint.X, (int)startPoint.Y, (int)endPoint.X, (int)endPoint.Y);
            KLineProfileGrayCV greyProfileInfo = profileInfo as KLineProfileGrayCV;

            Tuple<int, Point, int> min = greyProfileInfo.GetMinValue();
            Tuple<int, Point, int> max = greyProfileInfo.GetMaxValue();

            List<Tuple<Point, int>> greyInfo = greyProfileInfo.GetPixelDatas();

            int[] histogram = new int[greyInfo.Count];

            for (int i = 0; i < greyInfo.Count; i++)
                histogram[i] = greyInfo[i].Item2;
            param.HistogramValue = Array.ConvertAll(histogram, new Converter<int, float>(ConvertIntToFloat));

            param.Max = max.Item3;
            param.Min = min.Item3;

            return param;
        }

        public ColorHistogramParams GetColorProfileHistogramParam(Point startPoint, Point endPoint)
        {
            if (DisPlayImage == null)
                return null;
            ColorHistogramParams param = new ColorHistogramParams();
            try
            {
                var profileInfo = DisPlayImage.GetLineProfileInfo((int)startPoint.X, (int)startPoint.Y, (int)endPoint.X, (int)endPoint.Y);
                KLineProfileColorCV colorProfileInfo = profileInfo as KLineProfileColorCV;

                param.R_Min = colorProfileInfo.GetMinValue(KColorChannel.R).Item3;
                param.R_Max = colorProfileInfo.GetMaxValue(KColorChannel.R).Item3;

                param.G_Min = colorProfileInfo.GetMinValue(KColorChannel.G).Item3;
                param.G_Max = colorProfileInfo.GetMaxValue(KColorChannel.G).Item3;

                param.B_Min = colorProfileInfo.GetMinValue(KColorChannel.B).Item3;
                param.B_Max = colorProfileInfo.GetMaxValue(KColorChannel.B).Item3;

                List<Tuple<Point, KColor>> colorInfo = colorProfileInfo.GetPixelDatas();

                int[] Rhistogram = new int[colorInfo.Count];
                int[] Ghistogram = new int[colorInfo.Count];
                int[] Bhistogram = new int[colorInfo.Count];
                for (int i = 0; i < colorInfo.Count; i++)
                {
                    Rhistogram[i] = colorInfo[i].Item2.R;
                    Ghistogram[i] = colorInfo[i].Item2.G;
                    Bhistogram[i] = colorInfo[i].Item2.B;
                }

                param.R_HistogramValue = Array.ConvertAll(Rhistogram, new Converter<int, float>(ConvertIntToFloat));
                param.G_HistogramValue = Array.ConvertAll(Ghistogram, new Converter<int, float>(ConvertIntToFloat));
                param.B_HistogramValue = Array.ConvertAll(Bhistogram, new Converter<int, float>(ConvertIntToFloat));
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
            return param;
        }

        /// <summary>
        /// 실제 이미지 사이즈에 맞춰 Roi 출력
        /// </summary>
        /// <returns></returns>
        public RectangleF GetOrgRectangle(RectangleF rect)
        {
            if (rect.X < 0)
            {
                rect.Width = rect.Width - Math.Abs(rect.X);
                rect.X = 0;
            }
            if (rect.Y < 0)
            {
                rect.Height = rect.Height - Math.Abs(rect.Y);
                rect.Y = 0;
            }

            int orgWidth = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width;
            int orgHeight = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height;
            int nowWidth = CStatus.Instance().GetDrawBox().GetPictureBox().Width;
            int nowHeight = CStatus.Instance().GetDrawBox().GetPictureBox().Height;

            float left = rect.Left * orgWidth / (float)nowWidth;
            float top = rect.Top * orgHeight / (float)nowHeight;
            float right = rect.Right * orgWidth / (float)nowWidth;
            float bottom = rect.Bottom * orgHeight / (float)nowHeight;

            //Rectangle orgRect = new Rectangle((int)left, (int)top, (int)Math.Abs(right - left), (int)Math.Abs(bottom - top));
            RectangleF orgRect = new RectangleF(left, top, Math.Abs(right - left), Math.Abs(bottom - top));
            return orgRect;
        }

        public PointF GetOrgPoint(PointF point)
        {
            int orgWidth = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width;
            int orgHeight = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height;
            int nowWidth = CStatus.Instance().GetDrawBox().GetPictureBox().Width;
            int nowHeight = CStatus.Instance().GetDrawBox().GetPictureBox().Height;
            float newX = point.X * orgWidth / (float)nowWidth;
            float newY = point.Y * orgHeight / (float)nowHeight;

            if (newX < 0)
                newX = 0;
            if (newX >= CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width)
                newX = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Width - 1;
            if (newY < 0)
                newY = 0;
            if (newY >= CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height)
                newY = CStatus.Instance().GetDrawBox().ImageManager.DisPlayImage.Height - 1;

            return new PointF(newX, newY);
        }

        private int GetHistogramLength(int bit, bool isColor)
        {
            int ret = 0;
            if (isColor)
            {
                ret = (int)Math.Pow(2, 8);
            }
            else
            {
                if (bit == 24)
                    ret = (int)Math.Pow(2, 8);
                else
                    ret = (int)Math.Pow(2, bit);
            }
            return ret;
        }

        public int DistancePointToPoint(PointF point1, PointF point2)
        {
            return KLine.GetPointListOfLine(new Point((int)point1.X, (int)point1.Y), new Point((int)point2.X, (int)point2.Y)).Count;
        }
    }
}
