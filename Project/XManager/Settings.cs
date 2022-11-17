using CameraInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XManager.Camera;
using XManager.ImageProcessingData;
using XManager.Util;
using XManager.Utill;

namespace XManager
{
    public class Settings
    {
        private float _fovWidth = 1024;
        public float FovWidth
        {
            get{    return _fovWidth;   }
            set{    _fovWidth = value;  }
        }

        private float _fovHeight = 1024;
        public float FovHeight
        {
            get { return _fovHeight; }
            set { _fovHeight = value; }
        }

        private double _pitch = 1;
        public double Pitch
        {
            get { return _pitch; }
            set { _pitch = value; }
        }

        public eMeasurementType _measureMentType = eMeasurementType.Fov_Calibration;
        public eMeasurementType MeasureMentType
        {
            get { return _measureMentType; }
            set { _measureMentType = value; }
        }

        private eCameraType _cameraType = eCameraType.NONE;
        public eCameraType CameraType
        {
            get { return _cameraType; }
            set { _cameraType = value; }
        }

        public eLanguageType _language = eLanguageType.Korea;
        public eLanguageType Language
        {
            get { return _language; }
            set { _language = value; }
        }

        private int _avgCount = 1;
        public int AvgCount
        {
            get { return _avgCount; }
            set { _avgCount = value; }
        }

        private int _histogramMin = 1;
        public int HistogramMin
        {
            get { return _histogramMin; }
            set { _histogramMin = value; }
        }

        private int _histogramMax = 1;
        public int HistogramMax
        {
            get { return _histogramMax; }
            set { _histogramMax = value; }
        }
        public List<IPBase> TempImageProcessingList = new List<IPBase>();
        public void Save(bool isInitialize = false)
        {
            string currentDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XManager");

            DirectoryInfo di = new DirectoryInfo(currentDir);
            if (!di.Exists)
                di.Create();

            string filePath = Path.Combine(currentDir, "Settings.xml");

            XmlDocument xmlDocument = new XmlDocument();

            XmlElement element = XmlHelper.WriteProgramInfo(xmlDocument);
            XmlElement figureGroupElement = xmlDocument.CreateElement("", "Settings", "");
            element.AppendChild(figureGroupElement);

            //////////////////////////////
            XmlHelper.SetValue(figureGroupElement, "Language", ((int)_language).ToString());
            XmlHelper.SetValue(figureGroupElement, "FovWidth", _fovWidth.ToString());
            XmlHelper.SetValue(figureGroupElement, "FovHeight", _fovHeight.ToString());
            XmlHelper.SetValue(figureGroupElement, "Pitch", _pitch.ToString());
            XmlHelper.SetValue(figureGroupElement, "MeasurementType", ((int)_measureMentType).ToString());
            XmlHelper.SetValue(figureGroupElement, "CameraType", ((int)_cameraType).ToString());
            XmlHelper.SetValue(figureGroupElement, "AvgCount", _avgCount.ToString());
            XmlHelper.SetValue(figureGroupElement, "HistogramMin", _histogramMin.ToString());
            XmlHelper.SetValue(figureGroupElement, "HistogramMax", _histogramMax.ToString());
            //////////////////////////////


            ////
            if (!isInitialize)
            {
                if (CStatus.Instance().GetDrawBox() == null)
                {
                    xmlDocument.Save(filePath);
                    return;
                }
                if (CStatus.Instance().GetDrawBox().ImageManager.ImageProcessingList.Count <= 0)
                {
                    xmlDocument.Save(filePath);
                    return;
                }
                XmlElement imageProcessingGroupElement = xmlDocument.CreateElement("", "ImageProcessing", "");
                element.AppendChild(imageProcessingGroupElement);
                foreach (IPBase item in CStatus.Instance().GetDrawBox().ImageManager.ImageProcessingList)
                {
                    item.Save(xmlDocument, imageProcessingGroupElement);
                }
            }
            //XmlHelper.SetValue(imageProcessingGroupElement, "Test", "1234");
            ////
            xmlDocument.Save(filePath);
        }

        public void Load()
        {
            string currentDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XManager");
            string filePath = Path.Combine(currentDir, "Settings.xml");

            FileInfo info = new FileInfo(filePath);
            if (!info.Exists)
                Save(true);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlElement xmlElement = xmlDocument.DocumentElement;
            float fileVersion = Convert.ToSingle(XmlHelper.GetValue(xmlElement, "Version", ""));

            if (fileVersion != 1.0)
                return;
            if (xmlElement["Settings"] == null)
                return;

            //
            this._language = (eLanguageType)Convert.ToInt32(XmlHelper.GetValue(xmlElement["Settings"], "Language", "0"));
            this._fovWidth = Convert.ToSingle(XmlHelper.GetValue(xmlElement["Settings"], "FovWidth", "1024"));
            this._fovHeight = Convert.ToSingle(XmlHelper.GetValue(xmlElement["Settings"], "FovHeight", "1024"));
            this._pitch = Convert.ToSingle(XmlHelper.GetValue(xmlElement["Settings"], "Pitch", "1"));
            this._measureMentType = (eMeasurementType)Convert.ToInt32(XmlHelper.GetValue(xmlElement["Settings"], "MeasurementType", "0"));
            this._cameraType = (eCameraType)Convert.ToInt32(XmlHelper.GetValue(xmlElement["Settings"], "CameraType", "0"));
            this._avgCount = Convert.ToInt32(XmlHelper.GetValue(xmlElement["Settings"], "AvgCount", "1"));
            this._avgCount = Convert.ToInt32(XmlHelper.GetValue(xmlElement["Settings"], "Bit", "16"));
            this._histogramMin = Convert.ToInt32(XmlHelper.GetValue(xmlElement["Settings"], "HistogramMin", "0"));
            this._histogramMax = Convert.ToInt32(XmlHelper.GetValue(xmlElement["Settings"], "HistogramMax", "65535"));

            if (xmlElement["ImageProcessing"] == null)
                return;
            XmlNodeList nodes = xmlElement["ImageProcessing"].ChildNodes;
            LoadImageProcessing(nodes);
        }
        private void LoadImageProcessing(XmlNodeList nodes)
        {
            TempImageProcessingList.Clear();
            foreach (XmlNode item in nodes)
            {
                string name = item.Name;

                switch (name)
                {
                    case "Lut":
                        IP_LUT lut = new IP_LUT();
                        lut.Load(item);
                        TempImageProcessingList.Add(lut);
                        break;
                    case "CW":
                        IP_RotationCW cw = new IP_RotationCW();
                        TempImageProcessingList.Add(cw);
                        break;
                    case "CCW":
                        IP_RotationCCW ccw = new IP_RotationCCW();
                        TempImageProcessingList.Add(ccw);
                        break;
                    case "Crop":
                        IP_Crop crop = new IP_Crop();
                        crop.Load(item);
                        TempImageProcessingList.Add(crop);
                        break;
                    case "Resize":
                        IP_Resize resize = new IP_Resize();
                        resize.Load(item);
                        TempImageProcessingList.Add(resize);
                        break;
                    case "FlipVetical":
                        IP_FlipVetical flipVetical = new IP_FlipVetical();
                        TempImageProcessingList.Add(flipVetical);
                        break;
                    case "FlipHorizontal":
                        IP_FlipHorizontal flipHorizontal = new IP_FlipHorizontal();
                        TempImageProcessingList.Add(flipHorizontal);
                        break;
                    case "Blur":
                        IP_Blur blur = new IP_Blur();
                        blur.Load(item);
                        TempImageProcessingList.Add(blur);
                        break;
                    case "Sharp1":
                        IP_Sharp1 sharp1 = new IP_Sharp1();
                        TempImageProcessingList.Add(sharp1);
                        break;
                    case "Sharp2":
                        IP_Sharp2 sharp2 = new IP_Sharp2();
                        TempImageProcessingList.Add(sharp2);
                        break;
                    case "Sobel":
                        IP_Sobel sobel = new IP_Sobel();
                        sobel.Load(item);
                        TempImageProcessingList.Add(sobel);
                        break;
                    case "Laplacian":
                        IP_Laplacian laplacian = new IP_Laplacian();
                        laplacian.Load(item);
                        TempImageProcessingList.Add(laplacian);
                        break;
                    case "Canny":
                        IP_Canny canny = new IP_Canny();
                        canny.Load(item);
                        TempImageProcessingList.Add(canny);
                        break;
                    case "HorizonEdge":
                        IP_HorizonEdge horizonEdge = new IP_HorizonEdge();
                        TempImageProcessingList.Add(horizonEdge);
                        break;
                    case "VerticalEdge":
                        IP_VerticalEdge verticalEdge = new IP_VerticalEdge();
                        TempImageProcessingList.Add(verticalEdge);
                        break;
                    case "Median":
                        IP_Median median = new IP_Median();
                        median.Load(item);
                        TempImageProcessingList.Add(median);
                        break;
                    case "Dilate":
                        IP_Dilate dilate = new IP_Dilate();
                        dilate.Load(item);
                        TempImageProcessingList.Add(dilate);
                        break;
                    case "Erode":
                        IP_Erode erode = new IP_Erode();
                        erode.Load(item);
                        TempImageProcessingList.Add(erode);
                        break;
                    case "Average":
                        IP_Average average = new IP_Average();
                        TempImageProcessingList.Add(average);
                        break;
                    case "GreyLeveling":
                        IP_GreyLeveling greyLeveling = new IP_GreyLeveling();
                        greyLeveling.Load(item);
                        TempImageProcessingList.Add(greyLeveling);
                        break;
                    case "ColorLeveling":
                        IP_ColorLeveling colorLeveling = new IP_ColorLeveling();
                        colorLeveling.Load(item);
                        TempImageProcessingList.Add(colorLeveling);
                        break;
                    case "Threshold":
                        IP_Threshold threshold = new IP_Threshold();
                        threshold.Load(item);
                        TempImageProcessingList.Add(threshold);
                        break;
                    case "ThresholdAdaptive":
                        IP_ThresholdAdaptive thresholdAdaptive = new IP_ThresholdAdaptive();
                        thresholdAdaptive.Load(item);
                        TempImageProcessingList.Add(thresholdAdaptive);
                        break;
                    case "ThresholdOtsu":
                        IP_ThresholdOtsu thresholdOtsu = new IP_ThresholdOtsu();
                        thresholdOtsu.Load(item);
                        TempImageProcessingList.Add(thresholdOtsu);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
