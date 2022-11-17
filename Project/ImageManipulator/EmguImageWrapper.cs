using ImageManipulator.Controls;
using ImageManipulator.Data;
using ImageManipulator.ImageProcessingData;
using ImageManipulator.Util;
using JTeksSplineGraph.Geometry;
using KiyEmguCV.DIP;
using KiyLib.DIP;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulator
{
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
    /// <summary>
    /// EmguCV 관리 클래스
    /// </summary>
    public class EmguImageWrapper
    {
        private JImage _orgImage { get; set; }
        public JImage CalcImage { get; set; }  // ImageProcessingList가 실행된 이미지
        public JImage DisPlayImage { get; set; } // 실제 출력 이미지
        public List<IPBase> ImageProcessingList = new List<IPBase>();
        public string ImageFilePath;
        /// <summary>
        /// orgImage의 비트수를 가져온다.
        /// </summary>
        /// <returns>결과값</returns>
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
        /// <summary>
        /// orgImage의 칼라 여부
        /// </summary>
        /// <returns>결과값</returns>
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
        /// <summary>
        /// 이미지를 연 후 OrgImage에 넣는다.
        /// </summary>
        /// <param name="filePath">파일경로</param>
        /// <returns>이미지 객체</returns>
        public JImage LoadImage(string filePath)
        {
            ImageFilePath = filePath;
            JImage img = new JImage(filePath);
            _orgImage = (JImage)img.Clone();
            DisPlayImage = _orgImage;
            ImageProcessingList.Clear();

            return _orgImage;
        }
        /// <summary>
        /// OrgImage를 설정한다.
        /// </summary>
        /// <param name="image">Image</param>
        public void SetOrgImage(JImage image)
        {
            _orgImage = (JImage)image.Clone();
            DisPlayImage = _orgImage;
        }
        /// <summary>
        /// OrgImage를 가져온다.
        /// </summary>
        /// <returns>OrgImage</returns>
        public JImage GetOrgImage()
        {
            return _orgImage;
        }
        /// <summary>
        /// Point의 값을 가져온다.
        /// </summary>
        /// <param name="point">Point</param>
        /// <returns>ColorParam</returns>
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
        /// <summary>
        /// Display Image가 흑백일 경우 히스토그램을 가져온다.
        /// </summary>
        /// <returns>흑백히스토그램</returns>
        public float[] GetGreyHistogram()
        {
            int[] greyHistogram;

            DisPlayImage.GetHistoGray(out greyHistogram);
            float[] ret = Array.ConvertAll(greyHistogram, new Converter<int, float>(ConvertIntToFloat));
            return ret;
        }
        /// <summary>
        /// int를 Float으로 변환
        /// </summary>
        /// <param name="input">값</param>
        /// <returns></returns>
        private float ConvertIntToFloat(int input)
        {
            return (float)input;
        }
        /// <summary>
        /// Display Image가 칼라일 경우 히스트로그램을 가져온다.
        /// </summary>
        /// <returns>칼라히스토그램</returns>
        public ColorHistogram GetColorHistogram()
        {
            ColorHistogram param = new ColorHistogram();
            DisPlayImage.GetHistoColor(out param.B, out param.G, out param.R);
            return param;
        }
        /// <summary>
        /// 흑백 이미지를 경우 RectangleF 범위의 히스토그램을 가져온다.
        /// </summary>
        /// <param name="rect">RectangleF</param>
        /// <returns>히스토그램 Param</returns>
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
        /// <summary>
        /// 칼라 이미지를 경우 RectangleF 범위의 히스토그램을 가져온다.
        /// </summary>
        /// <param name="rect">RectangleF</param>
        /// <returns>히스토그램 Param</returns>
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
        /// <summary>
        /// Display할 이미지를 계산한다.
        /// </summary>
        /// <returns>Diplay Image</returns>
        public JImage CalcDisplayImage()
        {
            //Console.WriteLine("들어옴");
            if (_orgImage == null)
                return null;
            JImage destImage = (JImage)_orgImage.Clone();
            int count = 0;
            Status.Instance().ProcessingTime.Clear();

            foreach (IPBase item in ImageProcessingList)
            {
                if (!Status.Instance().CheckEanbleProcessing(item.ProcessType))
                {
                    count++;
                    continue;
                }
                destImage = item.Execute(destImage);

                count++;
            }
            CalcImage = destImage;
            DisPlayImage = destImage;

            Status.Instance().ProcessingTimeFormUpdate();

            return DisPlayImage;
        }
        /// <summary>
        /// OrgImage에 IPBase 리스트를 실행한다.
        /// </summary>
        /// <param name="excuteList">IPBase 리스트</param>
        /// <returns>리스트를 실행한 결과 이미지</returns>
        public JImage ExcuteImage(List<IPBase> excuteList)
        {
            if (_orgImage == null)
                return null;
            JImage destImage = (JImage)_orgImage.Clone();

            foreach (IPBase item in ImageProcessingList)
            {
                destImage = item.Execute(destImage);
            }

            foreach (IPBase excute in excuteList)
            {
                destImage = excute.Execute(destImage);
            }
            return destImage;
        }
        /// <summary>
        /// 선택된 Viewer의 히스토그램 가져오기
        /// </summary>
        /// <returns>히스토그램 Param 리스트</returns>
        public List<HistogramParams> GetSelectedViewerHistogramParam()
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
        /// <summary>
        /// 선택된 Roi의 히스토그램 가져오기
        /// </summary>
        /// <returns>히스토그램 Param 리스트</returns>
        public List<HistogramParams> GetRoiHistogramParamList()
        {
            if (DisPlayImage == null)
                return null;
            RectangleFigure selectedRoi = Status.Instance().SelectedViewer.GetDrawBox().GetSelectedRoi();

            if (selectedRoi == null)
                return null;

            List<HistogramParams> paramList = new List<HistogramParams>();
            if (!IsColor())
            {
                HistogramParams param = new HistogramParams();

                //RectangleF orgRect = GetOrgRectangle(((tRectangleResult)selectedRoi.GetResult()).resultRectangle);
                RectangleF orgRect = ((tRectangleResult)selectedRoi.GetResult()).resultRectangle;

                param.HistogramValue = Status.Instance().SelectedViewer.ImageManager.GetGreyRoiHistogramParam(orgRect).HistogramValue;
                param.GraphColor = Color.Gray;
                param.Mark1Value = 0;
                param.Mark2Value = param.HistogramValue.Count();
                param.Width = DisPlayImage.Width;
                param.Height = DisPlayImage.Height;
                paramList.Add(param);
            }
            else
            {
                int histogramMax = GetHistogramLength(Status.Instance().SelectedViewer.GetBit(), true) - 1;

                //RectangleF orgRect = GetOrgRectangle(((tRectangleResult)selectedRoi.GetResult()).resultRectangle);
                RectangleF orgRect = ((tRectangleResult)selectedRoi.GetResult()).resultRectangle;

                ColorHistogramParams roiParam =  Status.Instance().SelectedViewer.ImageManager.GetColorRoiHistogramParam(orgRect);

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
        /// <summary>
        /// 선택된 Profile의 히스토그램 가져오기
        /// </summary>
        /// <returns>히스토그램 Param 리스트</returns>
        public List<HistogramParams> GetProfileHistogramParamList()
        {
            if (DisPlayImage == null)
                return null;

            List<HistogramParams> paramList = new List<HistogramParams>();
            ProfileFigure selectedProfile = Status.Instance().SelectedViewer.GetDrawBox().GetSelectedProfile();

            if (selectedProfile == null)
                return null;
          
            if (!IsColor())
            {
                HistogramParams param = new HistogramParams();

                float pictureBoxWidth = Status.Instance().SelectedViewer.GetDrawBox().PictureBoxWidth();
                float pictureBoxHeight = Status.Instance().SelectedViewer.GetDrawBox().PictureBoxHeight();
                float displayImageWidth = (float)Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width;
                float displayImageHeight = (float)Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height;

               

                tProfileResult result = (tProfileResult)selectedProfile.GetResult();
                PointF startPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(result.StartPoint);
                PointF endPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(result.EndPoint);

                param.Mark1Value = result.Mark1;
                param.Mark2Value = result.Mark2;
                param.Width = DisPlayImage.Width;
                param.Height = DisPlayImage.Height;
                     
                var profileInfo = DisPlayImage.GetLineProfileInfo((int)startPoint.X, (int)startPoint.Y, (int)endPoint.X, (int)endPoint.Y);
                KLineProfileGrayCV greyProfileInfo = profileInfo as KLineProfileGrayCV;
              
                int width = Status.Instance().ProfileWidth;
                if(width >1)
                {
                    List<Point> rectPts = greyProfileInfo.GetProjectionRectPoints(width);
                }

                if (Status.Instance().DerivativeType == eDerivativeType.None)
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
                else if (Status.Instance().DerivativeType == eDerivativeType.OneDerivative)
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
                else if (Status.Instance().DerivativeType == eDerivativeType.TwoDerivative)
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
                //ProfileFigure selectedProfile = Status.Instance().SelectedViewer.GetDrawBox().GetSelectedProfile();
            
                tProfileResult result = (tProfileResult)selectedProfile.GetResult();
                PointF startPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(result.StartPoint);
                PointF endPoint = Status.Instance().SelectedViewer.ImageManager.GetOrgPoint(result.EndPoint);

                
                var profileInfo = DisPlayImage.GetLineProfileInfo((int)startPoint.X, (int)startPoint.Y, (int)endPoint.X, (int)endPoint.Y);
                KLineProfileColorCV colorProfileInfo = profileInfo as KLineProfileColorCV;

                int width = Status.Instance().ProfileWidth;
                if (Status.Instance().DerivativeType == eDerivativeType.None)
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
                else if(Status.Instance().DerivativeType == eDerivativeType.OneDerivative)
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
                else if(Status.Instance().DerivativeType == eDerivativeType.TwoDerivative)
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
        /// <summary>
        /// 흑백 이미지를 경우 startPoint부터 endPoint까지의 Profile 히스토그램을 가져온다.
        /// </summary>
        /// <param name="startPoint">startPoint</param>
        /// <param name="endPoint">endPoint</param>
        /// <returns>흑백히스토그램Param</returns>
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
        /// <summary>
        /// 칼라 이미지를 경우 startPoint부터 endPoint까지의 Profile 히스토그램을 가져온다.
        /// </summary>
        /// <param name="startPoint">startPoint</param>
        /// <param name="endPoint">endPoint</param>
        /// <returns>칼라히스토그램Param</returns>
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
        ///  실제 이미지 사이즈에 맞춰 RectangleF 출력
        /// </summary>
        /// <param name="rect">RectangleF</param>
        /// <returns>결과값</returns>
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

            int orgWidth = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width;
            int orgHeight = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height;
            int nowWidth = Status.Instance().SelectedViewer.GetDrawBox().GetPictureBoxSize().Width;
            int nowHeight = Status.Instance().SelectedViewer.GetDrawBox().GetPictureBoxSize().Height;

            float left = rect.Left * orgWidth / (float)nowWidth;
            float top = rect.Top * orgHeight / (float)nowHeight;
            float right = rect.Right * orgWidth / (float)nowWidth;
            float bottom = rect.Bottom * orgHeight / (float)nowHeight;

            RectangleF orgRect = new RectangleF(left, top, Math.Abs(right - left), Math.Abs(bottom - top));
            return orgRect;
        }
        /// <summary>
        /// 실제 이미지 사이즈에 맞춰 Point 출력
        /// </summary>
        /// <param name="point">PointF</param>
        /// <returns>결과값</returns>
        public PointF GetOrgPoint(PointF point)
        {
            int orgWidth = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width;
            int orgHeight = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height;
            int nowWidth = Status.Instance().SelectedViewer.GetDrawBox().GetPictureBoxSize().Width;
            int nowHeight = Status.Instance().SelectedViewer.GetDrawBox().GetPictureBoxSize().Height;
            float newX = point.X * orgWidth / (float)nowWidth;
            float newY = point.Y * orgHeight / (float)nowHeight;

            if (newX < 0)
                newX = 0;
            if (newX >= Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width)
                newX = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width - 1;
            if (newY < 0)
                newY = 0;
            if (newY >= Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height)
                newY = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height - 1;

            return new PointF(newX, newY);
        }
        /// <summary>
        /// 히스토그램 길이를 구한다.
        /// </summary>
        /// <param name="bit">Bit</param>
        /// <param name="isColor">칼라 여부</param>
        /// <returns>히스토그램 길이</returns>
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
        /// <summary>
        /// Point1과 Point2 사이의 길이를 구한다.
        /// </summary>
        /// <param name="point1">Point1과</param>
        /// <param name="point2">Point2</param>
        /// <returns>결과값</returns>
        public int DistancePointToPoint(PointF point1, PointF point2)
        {
            return KLine.GetPointListOfLine(new Point((int)point1.X, (int)point1.Y), new Point((int)point2.X, (int)point2.Y)).Count - 1;
        }
        /// <summary>
        /// Display Image의 히스토그램의 최대값을 구한다.
        /// </summary>
        /// <returns>결과값</returns>
        public int GetHistogramMax()
        {
            if (this.DisPlayImage.Color == KColorType.Color)
                return 255;
            else
            {
                if (this.DisPlayImage.Depth == KDepthType.Dt_16)
                    return 65535;
                else if (this.DisPlayImage.Depth == KDepthType.Dt_8)
                    return 255;
                else
                    return 255;
            }
        }
        /// <summary>
        /// Filter 창의 히스토그램을 RectangleF에 맞춰 업데이트 시킨다.(Local Histogram)
        /// </summary>
        /// <param name="rect">RectangleF</param>
        public void LocalHistogram(RectangleF rect)
        {
            if(Status.Instance().FilterForm == null || IsColor())
                return;

            RectangleF orgRect = GetOrgRectangle(rect);
            Status.Instance().FilterForm.LocalHistogram(orgRect);
        }
    }
}
