using ImageManipulator.ImageProcessingData;
using ImageManipulator.Util;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ImageManipulator.Data
{
    public class XmlReaderBuilder
    {
        /// <summary>
        /// XmlReaderBuilder를 상속받은 클래스를 타입에 맞게 생성한다.
        /// </summary>
        /// <param name="filePath">Xml파일경로</param>
        /// <param name="type">XmlType</param>
        /// <returns>생성된 객체</returns>
        public static XmlReaderManager Create(string filePath, eXmlType type)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlElement xmlElement = xmlDocument.DocumentElement;
            float fileVersion = Convert.ToSingle(XmlHelper.GetValue(xmlElement, "Version", ""));

            XmlReaderManager xmlReader = null;
            if (fileVersion == 1.0)
            {
                switch (type)
                {
                    case eXmlType.Roi:
                        xmlReader = new RoiReader();
                        break;
                    case eXmlType.Settings:
                        xmlReader = new SettingsReader();
                        break;
                    case eXmlType.FilterList:
                        xmlReader = new FilterListReader();
                        break;
                    default:
                        break;
                }
            }
            return xmlReader;
        }
    }

    public abstract class XmlReaderManager
    {
        public abstract List<T> Load<T>(string filePath);
    }

    public class RoiReader : XmlReaderManager
    {
        public override List<T> Load<T>(string filePath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlElement xmlElement = xmlDocument.DocumentElement;

            List<Figure> figureGroup = new List<Figure>();
            LoadFigureGroup(figureGroup, xmlElement["RoiList"]);

            return (List<T>)(object)figureGroup;
        }

        private void LoadFigureGroup(List<Figure> figureGroup, XmlElement packageGroupElement)
        {
            if (packageGroupElement == null)
                return;
            int width = 0;
            int height = 0;

            foreach (XmlElement element in packageGroupElement)
            {
               // if(element.Name )
                if (element.Name == "Width")
                    width = Convert.ToInt32(element.InnerText);
                if (element.Name == "Height")
                    height = Convert.ToInt32(element.InnerText);
                if (element.Name == "Roi")
                {
                    string id = XmlHelper.GetValue(element, "Id", "");
                    float left = Convert.ToSingle(XmlHelper.GetValue(element, "Left", ""));
                    float top = Convert.ToSingle(XmlHelper.GetValue(element, "Top", ""));
                    float right = Convert.ToSingle(XmlHelper.GetValue(element, "Right", ""));
                    float bottom = Convert.ToSingle(XmlHelper.GetValue(element, "Bottom", ""));
                    Figure figure = new RectangleFigure(new PointF(left, top), new PointF(right, bottom));
                    figure.Id = id;
                    figureGroup.Add(figure);
                }
            }

            if (width == 0 || height == 0)
            {
                System.Windows.Forms.MessageBox.Show("The file format is different.");
                figureGroup.Clear();
            }
            else if (width != Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width 
                        || height != Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height)
            {
                System.Windows.Forms.MessageBox.Show("Image size is different.");
                figureGroup.Clear();
            }
        }
    }

    public class SettingsReader : XmlReaderManager
    {
        public override List<T> Load<T>(string filePath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlElement xmlElement = xmlDocument.DocumentElement;

            LoadFov(xmlElement["Settings"]["Fov"]);
            LoadRecentFiles(xmlElement["Settings"]["RecentFiles"]);
            return (List<T>)null;
        }

        private void LoadFov(XmlElement packageGroupElement)
        {
            foreach (XmlElement element in packageGroupElement)
            {
                if (element.Name == "FovWidth")
                    Status.Instance().FovWidth = Convert.ToInt32(element.InnerText);
                if (element.Name == "FovHeight")
                    Status.Instance().FovHeight = Convert.ToInt32(element.InnerText);
            }
        }

        private void LoadRecentFiles(XmlElement packageGroupElement)
        {
            
            foreach (XmlElement element in packageGroupElement)
            {
                if (element.Name == "Path")
                    Status.Instance().AddRecentFile(element.InnerText);
                if (element.Name == "Language")
                    Status.Instance().Language = (eLanguageType)Convert.ToInt32(element.InnerText);
                if(element.Name == "MeasurementType")
                    Status.Instance().MeasureMentType = (eMeasurementType)Convert.ToInt32(element.InnerText);
                if (element.Name == "Pitch")
                    Status.Instance().Pitch = Convert.ToDouble(element.InnerText);
                if(element.Name == "UnitType")
                    Status.Instance().MeasurementUnit = (eMeasurementUnit)Convert.ToInt32(element.InnerText);
            }
        }
    }

    public class FilterListReader : XmlReaderManager
    {
        public override List<T> Load<T>(string filePath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlElement xmlElement = xmlDocument.DocumentElement;

            int bit = Convert.ToInt32(XmlHelper.GetValue(xmlElement, "Bit", ""));
            bool isColor = Convert.ToBoolean(XmlHelper.GetValue(xmlElement, "Color", ""));

            if(Status.Instance().SelectedViewer.ImageManager.GetBit() != bit 
                 || Status.Instance().SelectedViewer.ImageManager.IsColor() != isColor)
            {
                MessageBox.Show(LangResource.LoadFilterListMessage);
                return (List<T>)null;
            }

            XmlNodeList nodes = xmlElement["FilterList"].ChildNodes;
            LoadImageProcessing(nodes);
            return (List<T>)null;
        }

        private void LoadImageProcessing(XmlNodeList nodes)
        {
            //Status.Instance().SelectedViewer.ImageManager.ImageProcessingList

            foreach (XmlNode item in nodes)
            {
                string name = item.Name;
                eImageProcessType type = (eImageProcessType)Enum.Parse(typeof(eImageProcessType), item.Name);
                switch (type)
                {
                    case eImageProcessType.LUT_8:
                        IP_LUT8Bit lut8 = new IP_LUT8Bit();
                        lut8.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(lut8);
                        break;
                    case eImageProcessType.LUT_16:
                        IP_LUT16Bit lut16 = new IP_LUT16Bit();
                        lut16.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(lut16);
                        break;
                    case eImageProcessType.Rotate_CW:
                        IP_RotationCW cw = new IP_RotationCW();
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(cw);
                        break;
                    case eImageProcessType.Rotate_CCW:
                        IP_RotationCCW ccw = new IP_RotationCCW();
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(ccw);
                        break;
                    case eImageProcessType.Crop:
                        IP_Crop crop = new IP_Crop();
                        crop.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(crop);
                        break;
                    case eImageProcessType.Resize:
                        IP_Resize resize = new IP_Resize();
                        resize.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(resize);
                        break;
                    case eImageProcessType.Flip_Vertical:
                        IP_FlipVetical flipVetical = new IP_FlipVetical();
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(flipVetical);
                        break;
                    case eImageProcessType.Flip_Horizontal:
                        IP_FlipHorizontal flipHorizontal = new IP_FlipHorizontal();
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(flipHorizontal);
                        break;
                    case eImageProcessType.Blur:
                        IP_Blur blur = new IP_Blur();
                        blur.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(blur);
                        break;
                    case eImageProcessType.Sharp1:
                        IP_Sharp1 sharp1 = new IP_Sharp1();
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(sharp1);
                        break;
                    case eImageProcessType.Sharp2:
                        IP_Sharp2 sharp2 = new IP_Sharp2();
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(sharp2);
                        break;
                    case eImageProcessType.Sobel:
                        IP_Sobel sobel = new IP_Sobel();
                        sobel.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(sobel);
                        break;
                    case eImageProcessType.Laplacian:
                        IP_Laplacian laplacian = new IP_Laplacian();
                        laplacian.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(laplacian);
                        break;
                    case eImageProcessType.Canny:
                        IP_Canny canny = new IP_Canny();
                        canny.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(canny);
                        break;
                    case eImageProcessType.HorizonEdge:
                        IP_HorizonEdge horizonEdge = new IP_HorizonEdge();
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(horizonEdge);
                        break;
                    case eImageProcessType.VerticalEdge:
                        IP_VerticalEdge verticalEdge = new IP_VerticalEdge();
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(verticalEdge);
                        break;
                    case eImageProcessType.Median:
                        IP_Median median = new IP_Median();
                        median.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(median);
                        break;
                    case eImageProcessType.Dilate:
                        IP_Dilate dilate = new IP_Dilate();
                        dilate.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(dilate);
                        break;
                    case eImageProcessType.Erode:
                        IP_Erode erode = new IP_Erode();
                        erode.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(erode);
                        break;
                    case eImageProcessType.Average:
                        IP_Average average = new IP_Average();
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(average);
                        break;
                    case eImageProcessType.Leveling_8:
                        IP_Leveling8 Leveling8 = new IP_Leveling8();
                        Leveling8.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(Leveling8);
                        break;
                    case eImageProcessType.Leveling_16:
                        IP_Leveling16 Leveling16 = new IP_Leveling16();
                        Leveling16.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(Leveling16);
                        break;
                    //case "ColorLeveling":
                    //    IP_ColorLeveling colorLeveling = new IP_ColorLeveling();
                    //    colorLeveling.Load(item);
                    //    Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(colorLeveling);
                    //    break;
                    case eImageProcessType.Threshold:
                        IP_Threshold threshold = new IP_Threshold();
                        threshold.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(threshold);
                        break;
                    case eImageProcessType.ThresholdAdaptive:
                        IP_ThresholdAdaptive thresholdAdaptive = new IP_ThresholdAdaptive();
                        thresholdAdaptive.Load(item);
                        Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(thresholdAdaptive);
                        break;
                    //case "ThresholdOtsu":
                    //    IP_ThresholdOtsu thresholdOtsu = new IP_ThresholdOtsu();
                    //    thresholdOtsu.Load(item);
                    //    Status.Instance().SelectedViewer.ImageManager.ImageProcessingList.Add(thresholdOtsu);
                    //    break;

                    default:
                        break;
                }
            }
        }
    }
}
