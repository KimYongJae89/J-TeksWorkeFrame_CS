using ImageManipulator.Util;
using KiyLib.DIP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageManipulator.ImageProcessingData
{
    public class IP_LUT8Bit : IPBase
    {
        
        private IpLutParams _param = new IpLutParams();
        public IP_LUT8Bit()
        {
            ProcessType = eImageProcessType.LUT_8;
        }

        public override object Clone()
        {
            IP_LUT8Bit lut = new IP_LUT8Bit();
            lut.SetParam(this._param.Clone());
            return lut;
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();
            if (_param.LUT == null)
            {
                sw.Stop();
                time.Type = ProcessType.ToString();
                time.Time = sw.ElapsedMilliseconds.ToString();
                Status.Instance().ProcessingTime.Add(time);
                return inputImage;
            }
            else
            {
                JImage ret = inputImage.Lut(_param.LUT);

                sw.Stop();
                time.Type = ProcessType.ToString();
                time.Time = sw.ElapsedMilliseconds.ToString();
                Status.Instance().ProcessingTime.Add(time);
                return ret;
            }
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            float width = Convert.ToSingle(node["Size"]["Width"].InnerText);
            float height = Convert.ToSingle(node["Size"]["Height"].InnerText);

            XmlNodeList keyNodeList = node["KeyPoints"].ChildNodes;
            List<PointF> keyList = new List<PointF>();
            foreach (XmlNode item in keyNodeList)
            {
                PointF point = new PointF();
                point.X = Convert.ToSingle(item["X"].InnerText);
                point.Y = Convert.ToSingle(item["Y"].InnerText);

                keyList.Add(point);
            }
            XmlNodeList lutValueNodeList = node["LutValue"].ChildNodes;
            int count = lutValueNodeList.Count;
            int[] Lut = new int[count];
            int index = 0;
            foreach (XmlNode item in lutValueNodeList)
            {
                string te = item.InnerText;
                int value = Convert.ToInt32(item.InnerText);
                Lut[index] = value;
                index++;
            }
            this._param.keyPt = keyList;
            this._param.Width = width;
            this._param.Height = height;
            this._param.LUT = Lut;

            


            //int count = Convert.ToInt32(node["Count"].InnerText);
            //this._param.LUT = new int[count];
            //for (int i = 0; i < count; i++)
            //{
            //    int value = Convert.ToInt32(node[i.ToString()].InnerText);
            //    this._param.LUT[i] = value;
            //}
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            try
            {
                XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
                element.AppendChild(filterelement);
                XmlElement sizeElement = xmlDocument.CreateElement("", "Size", "");
                filterelement.AppendChild(sizeElement);

                XmlHelper.SetValue(sizeElement, "Width", _param.Width.ToString());
                XmlHelper.SetValue(sizeElement, "Height", _param.Height.ToString());

                XmlElement paramElement = xmlDocument.CreateElement("", "KeyPoints", "");
                filterelement.AppendChild(paramElement);
                
                for (int i = 0; i < _param.keyPt.Count; i++)
                {
                    XmlElement pointsElement = xmlDocument.CreateElement("", "Points", "");
                    paramElement.AppendChild(pointsElement);
                    XmlHelper.SetValue(pointsElement, "X", _param.keyPt[i].X.ToString());
                    XmlHelper.SetValue(pointsElement, "Y", _param.keyPt[i].Y.ToString());
                }

                XmlElement LutElement = xmlDocument.CreateElement("", "LutValue", "");
                filterelement.AppendChild(LutElement);

                //XmlHelper.SetValue(LutElement, "X", "1234");
                for (int i = 0; i < _param.LUT.Count(); i++)
                {
                    XmlHelper.SetValue(LutElement, "Value", _param.LUT[i].ToString(), i.ToString());
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public override void SetParam(object param)
        {
            _param = (IpLutParams)param;
        }
    }

    public class IP_LUT16Bit : IPBase
    {

        private IpLutParams _param = new IpLutParams();
        public IP_LUT16Bit()
        {
            ProcessType = eImageProcessType.LUT_16;
        }

        public override object Clone()
        {
            IP_LUT16Bit lut = new IP_LUT16Bit();
            lut.SetParam(this._param.Clone());
            return lut;
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();
            if (_param.LUT == null)
            {
                sw.Stop();
                time.Type = ProcessType.ToString();
                time.Time = sw.ElapsedMilliseconds.ToString();
                Status.Instance().ProcessingTime.Add(time);
                return inputImage;
            }
            else
            {
                JImage ret = inputImage.Lut(_param.LUT);

                sw.Stop();
                time.Type = ProcessType.ToString();
                time.Time = sw.ElapsedMilliseconds.ToString();
                Status.Instance().ProcessingTime.Add(time);
                return ret;
            }
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            float width = Convert.ToSingle(node["Size"]["Width"].InnerText);
            float height = Convert.ToSingle(node["Size"]["Height"].InnerText);

            XmlNodeList keyNodeList = node["KeyPoints"].ChildNodes;
            List<PointF> keyList = new List<PointF>();
            foreach (XmlNode item in keyNodeList)
            {
                PointF point = new PointF();
                point.X = Convert.ToSingle(item["X"].InnerText);
                point.Y = Convert.ToSingle(item["Y"].InnerText);

                keyList.Add(point);
            }
            XmlNodeList lutValueNodeList = node["LutValue"].ChildNodes;
            int count = lutValueNodeList.Count;
            int[] Lut = new int[count];
            int index = 0;
            foreach (XmlNode item in lutValueNodeList)
            {
                string te = item.InnerText;
                int value = Convert.ToInt32(item.InnerText);
                Lut[index] = value;
                index++;
            }
            this._param.keyPt = keyList;
            this._param.Width = width;
            this._param.Height = height;
            this._param.LUT = Lut;




            //int count = Convert.ToInt32(node["Count"].InnerText);
            //this._param.LUT = new int[count];
            //for (int i = 0; i < count; i++)
            //{
            //    int value = Convert.ToInt32(node[i.ToString()].InnerText);
            //    this._param.LUT[i] = value;
            //}
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            try
            {
                XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
                element.AppendChild(filterelement);
                XmlElement sizeElement = xmlDocument.CreateElement("", "Size", "");
                filterelement.AppendChild(sizeElement);

                XmlHelper.SetValue(sizeElement, "Width", _param.Width.ToString());
                XmlHelper.SetValue(sizeElement, "Height", _param.Height.ToString());

                XmlElement paramElement = xmlDocument.CreateElement("", "KeyPoints", "");
                filterelement.AppendChild(paramElement);

                for (int i = 0; i < _param.keyPt.Count; i++)
                {
                    XmlElement pointsElement = xmlDocument.CreateElement("", "Points", "");
                    paramElement.AppendChild(pointsElement);
                    XmlHelper.SetValue(pointsElement, "X", _param.keyPt[i].X.ToString());
                    XmlHelper.SetValue(pointsElement, "Y", _param.keyPt[i].Y.ToString());
                }

                XmlElement LutElement = xmlDocument.CreateElement("", "LutValue", "");
                filterelement.AppendChild(LutElement);

                //XmlHelper.SetValue(LutElement, "X", "1234");
                for (int i = 0; i < _param.LUT.Count(); i++)
                {
                    XmlHelper.SetValue(LutElement, "Value", _param.LUT[i].ToString(), i.ToString());
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public override void SetParam(object param)
        {
            _param = (IpLutParams)param;
        }
    }

    public class IP_RotationCW : IPBase
    {
        public IP_RotationCW()
        {
            ProcessType = eImageProcessType.Rotate_CW;
        }

        public override object Clone()
        {
            return new IP_RotationCW();
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.Rotate(KiyLib.DIP.KRotateType.CW_90);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return null;
        }

        public override void Load(XmlNode node)
        {
           
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
        }

        public override void SetParam(object param)
        {
        }
    }

    public class IP_RotationCCW : IPBase
    {
        public IP_RotationCCW()
        {
            ProcessType = eImageProcessType.Rotate_CCW;
        }

        public override object Clone()
        {
            return new IP_RotationCCW();
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.Rotate(KiyLib.DIP.KRotateType.CCW_90);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return null;
        }

        public override void Load(XmlNode node)
        {

        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
        }

        public override void SetParam(object param)
        {
        }
    }

    public class IP_Crop : IPBase
    {
        private IpCropParams _param = new IpCropParams();
        public IP_Crop()
        {
            ProcessType = eImageProcessType.Crop;
        }

        public override object Clone()
        {
            IP_Crop crop = new IP_Crop();
            crop.SetParam(this._param.Clone());
            return crop;
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.Crop(_param.Roi);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);

            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            int left = Convert.ToInt32(node["Left"].InnerText);
            int top = Convert.ToInt32(node["Top"].InnerText);
            int width = Convert.ToInt32(node["Width"].InnerText);
            int height = Convert.ToInt32(node["Height"].InnerText);

            this._param.Roi = new System.Drawing.Rectangle(left, top, width, height);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
           // Rectangle
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
            XmlHelper.SetValue(filterelement, "Left", _param.Roi.Left.ToString());
            XmlHelper.SetValue(filterelement, "Top", _param.Roi.Top.ToString());
            XmlHelper.SetValue(filterelement, "Width", _param.Roi.Width.ToString());
            XmlHelper.SetValue(filterelement, "Height", _param.Roi.Height.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpCropParams)param;
        }
    }

    public class IP_Resize : IPBase
    {
        private IpResizeParams _param = new IpResizeParams();
        public IP_Resize()
        {
            ProcessType = eImageProcessType.Crop;
        }

        public override object Clone()
        {
            IP_Resize resize = new IP_Resize();
            resize.SetParam(this._param.Clone());
            return resize;
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.Resize(_param.WidthScale, _param.HeightScale, _param.AlgorithmType);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            KInterpolation type = (KInterpolation)Convert.ToInt32(node["Type"].InnerText);
            string widthScale = node["WidthScale"].InnerText;
            string heightScale = node["HeightScale"].InnerText;
            _param.AlgorithmType = type;
            this._param.WidthScale = Convert.ToSingle(widthScale);
            this._param.HeightScale = Convert.ToSingle(heightScale);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
            XmlHelper.SetValue(filterelement, "Type", ((int)_param.AlgorithmType).ToString());
            XmlHelper.SetValue(filterelement, "WidthScale", _param.WidthScale.ToString());
            XmlHelper.SetValue(filterelement, "HeightScale", _param.HeightScale.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpResizeParams)param;
        }
    }

    public class IP_FlipVetical : IPBase
    {
        public IP_FlipVetical()
        {
            ProcessType = eImageProcessType.Flip_Vertical;
        }

        public override object Clone()
        {
            return new IP_FlipVetical();
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.Flip(KiyLib.DIP.KFlipType.Vertical);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return null;
        }

        public override void Load(XmlNode node)
        {
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
        }

        public override void SetParam(object param)
        {
        }
    }

    public class IP_FlipHorizontal : IPBase
    {
        public IP_FlipHorizontal()
        {
            ProcessType = eImageProcessType.Flip_Horizontal;
        }

        public override object Clone()
        {
            return new IP_FlipHorizontal();
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.Flip(KiyLib.DIP.KFlipType.Horizontal);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);

            return ret;
        }

        public override object GetParam()
        {
            return null;
        }

        public override void Load(XmlNode node)
        {
            throw new NotImplementedException();
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
        }

        public override void SetParam(object param)
        {
        }
    }

    public class IP_Blur : IPBase
    {
        private IpBlurParams _param = new IpBlurParams();
        public IP_Blur()
        {
            base.ProcessType = eImageProcessType.Blur;
        }

        public override object Clone()
        {
            IP_Blur blur = new IP_Blur();
            blur.SetParam(this._param.Clone());
            return blur;
        }

        public override JImage Execute(JImage inputImage)
        {
            //JImage img = (JImage)inputImage.Clone();

            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.FT_Blur(_param.Width, _param.Height);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);

            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            string width = node["Width"].InnerText;
            string height = node["Height"].InnerText;
            this._param.Width = Convert.ToInt32(width);
            this._param.Height = Convert.ToInt32(height);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
           
            XmlHelper.SetValue(filterelement, "Width", _param.Width.ToString());
            XmlHelper.SetValue(filterelement, "Height", _param.Height.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpBlurParams)param;
        }
    }

    public class IP_Sharp1 : IPBase
    {
        public IP_Sharp1()
        {
            base.ProcessType = eImageProcessType.Sharp1;
        }

        public override object Clone()
        {
            return new IP_Sharp1();
        }

        public override JImage Execute(JImage inputImage)
        {
            //JImage img = (JImage)inputImage.Clone();

            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.FT_Sharp1();

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return null;
        }

        public override void Load(XmlNode node)
        {
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
        }

        public override void SetParam(object param)
        {
        }
    }

    public class IP_Sharp2 : IPBase
    {
        public IP_Sharp2()
        {
            base.ProcessType = eImageProcessType.Sharp2;
        }

        public override object Clone()
        {
            return new IP_Sharp2();
        }

        public override JImage Execute(JImage inputImage)
        {
            //JImage img = (JImage)inputImage.Clone();
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.FT_Sharp2();

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);

            return ret;
        }

        public override object GetParam()
        {
            return null;
        }

        public override void Load(XmlNode node)
        {

        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
        }

        public override void SetParam(object param)
        {
        }
    }

    public class IP_Sobel : IPBase
    {
        private IpSobelParams _param = new IpSobelParams();
        public IP_Sobel()
        {
            base.ProcessType = eImageProcessType.Sobel;
        }

        public override object Clone()
        {
            IP_Sobel sobel = new IP_Sobel();
            sobel.SetParam(this._param.Clone());
            return sobel;
        }

        public override JImage Execute(JImage inputImage)
        {
            //JImage img = (JImage)inputImage.Clone();
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.FT_Sobel(_param.Xorder, _param.Yorder, _param.ApertureSize);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            string xorder = node["Xorder"].InnerText;
            string yorder = node["Yorder"].InnerText;
            string aperturesize = node["ApertureSize"].InnerText;
            this._param.Xorder = Convert.ToInt32(xorder);
            this._param.Yorder = Convert.ToInt32(yorder);
            this._param.ApertureSize = Convert.ToInt32(aperturesize);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);

            XmlHelper.SetValue(filterelement, "Xorder", _param.Xorder.ToString());
            XmlHelper.SetValue(filterelement, "Yorder", _param.Yorder.ToString());
            XmlHelper.SetValue(filterelement, "ApertureSize", _param.ApertureSize.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpSobelParams)param;
        }
    }

    public class IP_Laplacian : IPBase
    {
        private IpLaplacianParams _param = new IpLaplacianParams();
        public IP_Laplacian()
        {
            base.ProcessType = eImageProcessType.Laplacian;
        }

        public override object Clone()
        {
            IP_Laplacian laplacian = new IP_Laplacian();
            laplacian.SetParam(this._param.Clone());
            return laplacian;
        }

        public override JImage Execute(JImage inputImage)
        {
            //JImage img = (JImage)inputImage.Clone();
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.FT_Laplacian(_param.ApertureSize);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            string apertureSize = node["ApertureSize"].InnerText;
            this._param.ApertureSize = Convert.ToInt32(apertureSize);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);

            XmlHelper.SetValue(filterelement, "ApertureSize", _param.ApertureSize.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpLaplacianParams)param;
        }
    }

    public class IP_Canny : IPBase
    {
        private IpCannyParams _param = new IpCannyParams();
        public IP_Canny()
        {
            base.ProcessType = eImageProcessType.Canny;
        }

        public override object Clone()
        {
            IP_Canny canny = new IP_Canny();
            canny.SetParam(this._param.Clone());
            return canny;
        }

        public override JImage Execute(JImage inputImage)
        {
            //JImage img = (JImage)inputImage.Clone();
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.FT_Canny(_param.Thresh, _param.ThreshLinking);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            string thresh = node["Thresh"].InnerText;
            string threshLinking = node["ThreshLinking"].InnerText;
            this._param.Thresh = Convert.ToInt32(thresh);
            this._param.ThreshLinking = Convert.ToInt32(threshLinking);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);

            XmlHelper.SetValue(filterelement, "Thresh", _param.Thresh.ToString());
            XmlHelper.SetValue(filterelement, "ThreshLinking", _param.ThreshLinking.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpCannyParams)param;
        }
    }

    public class IP_HorizonEdge : IPBase
    {
        public IP_HorizonEdge()
        {
            base.ProcessType = eImageProcessType.HorizonEdge;
        }

        public override object Clone()
        {
            return new IP_HorizonEdge();
        }

        public override JImage Execute(JImage inputImage)
        {
            //JImage img = (JImage)inputImage.Clone();

            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.FT_HorizonEdge();

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return null;
        }

        public override void Load(XmlNode node)
        {

        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
        }

        public override void SetParam(object param)
        {
        }
    }

    public class IP_VerticalEdge : IPBase
    {
        public IP_VerticalEdge()
        {
            base.ProcessType = eImageProcessType.VerticalEdge;
        }

        public override object Clone()
        {
            return new IP_VerticalEdge();
        }

        public override JImage Execute(JImage inputImage)
        {
            //JImage img = (JImage)inputImage.Clone();
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.FT_VerticalEdge();

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return null;
        }

        public override void Load(XmlNode node)
        {

        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
        }

        public override void SetParam(object param)
        {
        }
    }

    public class IP_Median : IPBase
    {
        private IpMedianParams _param = new IpMedianParams();
        public IP_Median()
        {
            base.ProcessType = eImageProcessType.Median;
        }

        public override object Clone()
        {
            IP_Median median = new IP_Median();
            median.SetParam(this._param.Clone());
            return median;
        }

        public override JImage Execute(JImage inputImage)
        {
            //JImage img = (JImage)inputImage.Clone();
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.FT_Median(_param.Size);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            string size = node["Size"].InnerText;
            this._param.Size = Convert.ToInt32(size);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);

            XmlHelper.SetValue(filterelement, "Size", _param.Size.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpMedianParams)param;
        }
    }

    public class IP_Dilate : IPBase
    {
        private IpDilateParams _param = new IpDilateParams();
        public IP_Dilate()
        {
            base.ProcessType = eImageProcessType.Dilate;
        }

        public override object Clone()
        {
            IP_Dilate dilate = new IP_Dilate();
            dilate.SetParam(this._param.Clone());
            return dilate;
        }

        public override JImage Execute(JImage inputImage)
        {
            //JImage img = (JImage)inputImage.Clone();

            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.FT_Dilate(_param.Iterations);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            string iterations = node["Iterations"].InnerText;
            this._param.Iterations = Convert.ToInt32(iterations);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);

            XmlHelper.SetValue(filterelement, "Iterations", _param.Iterations.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpDilateParams)param;
        }
    }

    public class IP_Erode : IPBase
    {
        private IpErodeParams _param = new IpErodeParams();
        public IP_Erode()
        {
            base.ProcessType = eImageProcessType.Erode;
        }

        public override object Clone()
        {
            IP_Erode erode = new IP_Erode();
            erode.SetParam(this._param.Clone());
            return erode;
        }

        public override JImage Execute(JImage inputImage)
        {
            //JImage img = (JImage)inputImage.Clone();
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.FT_Erode(_param.Iterations);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            string iterations = node["Iterations"].InnerText;
            this._param.Iterations = Convert.ToInt32(iterations);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);

            XmlHelper.SetValue(filterelement, "Iterations", _param.Iterations.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpErodeParams)param;
        }
    }

    public class IP_Average : IPBase
    {
        public IP_Average()
        {
            base.ProcessType = eImageProcessType.Average;
        }

        public override object Clone()
        {
            return new IP_Average();
        }

        public override JImage Execute(JImage inputImage)
        {
            //JImage img = (JImage)inputImage.Clone();

            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.FT_Average();

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return null;
        }

        public override void Load(XmlNode node)
        {
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
        }

        public override void SetParam(object param)
        {
        }
    }

    //public class IP_Basic : IPBase
    //{
    //    public IP_Basic()
    //    {
    //        base.ProcessType = eImageProcessType.Basic;
    //    }

    //    public override JImage Execute(JImage inputImage)
    //    {
    //        JImage img = (JImage)inputImage.Clone();
    //        return img.FT_Basic();
    //    }

    //    public override object GetParam()
    //    {
    //        return null;
    //    }

    //    public override void SetParam(object param)
    //    {
    //    }
    //}

    public class IP_UserFilter : IPBase
    {
        public IP_UserFilter()
        {
            base.ProcessType = eImageProcessType.UserFilter;
        }

        public override object Clone()
        {
            return new IP_UserFilter();
        }

        public override JImage Execute(JImage inputImage)
        {
            return null;
        }

        public override object GetParam()
        {
            return null;
        }

        public override void Load(XmlNode node)
        {
            throw new NotImplementedException();
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            throw new NotImplementedException();
        }

        public override void SetParam(object param)
        {
           // throw new NotImplementedException();
        }
    }

    public class IP_Leveling8 : IPBase
    {
        private IpLevelingParams _param= new IpLevelingParams();
        public IP_Leveling8()
        {
            ProcessType = eImageProcessType.Leveling_8;
            _param.Low = 0;
            _param.High = 255;
        }

        public override object Clone()
        {
            IP_Leveling8 level = new IP_Leveling8();
            level.SetParam(this._param.Clone());
            return level;
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();
            if (_param.Low ==0 && _param.High == 0)
            {
                sw.Stop();
                time.Type = ProcessType.ToString();
                time.Time = sw.ElapsedMilliseconds.ToString();
                Status.Instance().ProcessingTime.Add(time);

                return inputImage;
            }
            JImage ret;
            if (!Status.Instance().SelectedViewer.ImageManager.IsColor())
                ret = inputImage.WndLvGray(_param.Low, _param.High);
            else
                ret = inputImage.WndLvColor(_param.Low, _param.High, 256);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            string low = node["Low"].InnerText;
            string high = node["High"].InnerText;
            this._param.Low = Convert.ToInt32(low);
            this._param.High = Convert.ToInt32(high);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);

            XmlHelper.SetValue(filterelement, "Low", _param.Low.ToString());
            XmlHelper.SetValue(filterelement, "High", _param.High.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpLevelingParams)param;
        }
    }

    public class IP_Leveling16 : IPBase
    {
        private IpLevelingParams _param = new IpLevelingParams();
        public IP_Leveling16()
        {
            ProcessType = eImageProcessType.Leveling_16;
            _param.Low = 0;
            _param.High = 65535;
        }

        public override object Clone()
        {
            IP_Leveling16 level = new IP_Leveling16();
            level.SetParam(this._param.Clone());
            return level;
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();
            if (_param.Low == 0 && _param.High == 0)
            {
                sw.Stop();
                time.Type = ProcessType.ToString();
                time.Time = sw.ElapsedMilliseconds.ToString();
                Status.Instance().ProcessingTime.Add(time);

                return inputImage;
            }
            JImage ret = inputImage.WndLvGray(_param.Low, _param.High);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            string low = node["Low"].InnerText;
            string high = node["High"].InnerText;
            this._param.Low = Convert.ToInt32(low);
            this._param.High = Convert.ToInt32(high);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);

            XmlHelper.SetValue(filterelement, "Low", _param.Low.ToString());
            XmlHelper.SetValue(filterelement, "High", _param.High.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpLevelingParams)param;
        }
    }

    //public class IP_ColorLeveling : IPBase
    //{
    //    private IpColorLevelingParams _param = new IpColorLevelingParams();
    //    public IP_ColorLeveling()
    //    {
    //        ProcessType = eImageProcessType.GreyLeveling;
    //    }

    //    public override object Clone()
    //    {
    //        IP_ColorLeveling level = new IP_ColorLeveling();
    //        level.SetParam(this._param.Clone());
    //        return level;
    //    }

    //    public override JImage Execute(JImage inputImage)
    //    {
    //        Stopwatch sw = new Stopwatch();
    //        ProcessingTime time = new ProcessingTime();
    //        sw.Restart();
    //        if (_param.Low == 0 && _param.High == 0)
    //        {
    //            sw.Stop();
    //            time.Type = ProcessType.ToString();
    //            time.Time = sw.ElapsedMilliseconds.ToString();
    //            Status.Instance().ProcessingTime.Add(time);
    //            return inputImage;
    //        }
    //        JImage ret = inputImage.WndLvColor(_param.Low, _param.High, 256);

    //        sw.Stop();
    //        time.Type = ProcessType.ToString();
    //        time.Time = sw.ElapsedMilliseconds.ToString();
    //        Status.Instance().ProcessingTime.Add(time);
    //        return ret;
    //    }

    //    public override object GetParam()
    //    {
    //        return _param;
    //    }

    //    public override void Load(XmlNode node)
    //    {
    //        string low = node["Low"].InnerText;
    //        string high = node["High"].InnerText;
    //        this._param.Low = Convert.ToInt32(low);
    //        this._param.High = Convert.ToInt32(high);
    //    }

    //    public override void Save(XmlDocument xmlDocument, XmlElement element)
    //    {
    //        XmlElement filterelement = xmlDocument.CreateElement("", "ColorLeveling", "");
    //        element.AppendChild(filterelement);

    //        XmlHelper.SetValue(filterelement, "Low", _param.Low.ToString());
    //        XmlHelper.SetValue(filterelement, "High", _param.High.ToString());
    //    }

    //    public override void SetParam(object param)
    //    {
    //        _param = (IpColorLevelingParams)param;
    //    }
    //}

    public class IP_Threshold : IPBase
    {
        private IpThresholdParams _param = new IpThresholdParams();

        public IP_Threshold()
        {
            this.ProcessType = eImageProcessType.Threshold;
        }

        public override object Clone()
        {
            IP_Threshold threshold = new IP_Threshold();
            threshold.SetParam(this._param.Clone());
            return threshold;
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret;
            if (!_param.IsOust)
                ret = inputImage.Threshold(_param.Value);
            else
                ret = inputImage.ThresholdOtsu(out _param.Value);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            string value = node["Value"].InnerText;
            this._param.Value = Convert.ToInt32(value);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);

            XmlHelper.SetValue(filterelement, "Value", _param.Value.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpThresholdParams)param;
        }
    }

    public class IP_ThresholdAdaptive : IPBase
    {
        private IpThresholdAdaptiveParams _param = new IpThresholdAdaptiveParams();

        public IP_ThresholdAdaptive()
        {
            this.ProcessType = eImageProcessType.ThresholdAdaptive;
        }

        public override object Clone()
        {
            IP_ThresholdAdaptive adaptve = new IP_ThresholdAdaptive();
            adaptve.SetParam(this._param.Clone());
            return adaptve;
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.ThresholdAdaptive(_param.type, _param.BlockSize, _param.Weight);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);

            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            ThresAdaptiveType type = (ThresAdaptiveType)Convert.ToInt32(node["Type"].InnerText);
            string blockSize = node["BlockSize"].InnerText;
            string weight = node["Weight"].InnerText;
            this._param.type = type;
            this._param.BlockSize = Convert.ToInt32(blockSize);
            this._param.Weight = Convert.ToInt32(weight);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);
            XmlHelper.SetValue(filterelement, "Type", ((int)_param.type).ToString());
            XmlHelper.SetValue(filterelement, "BlockSize", _param.BlockSize.ToString());
            XmlHelper.SetValue(filterelement, "Weight", _param.Weight.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpThresholdAdaptiveParams)param;
        }
    }

    public class IP_ThresholdOtsu : IPBase
    {
        private IpThresholdOtsuParams _param = new IpThresholdOtsuParams();

        public IP_ThresholdOtsu()
        {
            this.ProcessType = eImageProcessType.Threshold;
        }

        public override object Clone()
        {
            IP_ThresholdOtsu otsu = new IP_ThresholdOtsu();
            otsu.SetParam(this._param.Clone());
            return otsu;
        }

        public override JImage Execute(JImage inputImage)
        {
            Stopwatch sw = new Stopwatch();
            ProcessingTime time = new ProcessingTime();
            sw.Restart();

            JImage ret = inputImage.ThresholdOtsu(out _param.value);

            sw.Stop();
            time.Type = ProcessType.ToString();
            time.Time = sw.ElapsedMilliseconds.ToString();
            Status.Instance().ProcessingTime.Add(time);
            return ret;
        }

        public override object GetParam()
        {
            return _param;
        }

        public override void Load(XmlNode node)
        {
            string value = node["Value"].InnerText;
            this._param.value = Convert.ToInt32(value);
        }

        public override void Save(XmlDocument xmlDocument, XmlElement element)
        {
            XmlElement filterelement = xmlDocument.CreateElement("", this.ProcessType.ToString(), "");
            element.AppendChild(filterelement);

            XmlHelper.SetValue(filterelement, "Value", _param.value.ToString());
        }

        public override void SetParam(object param)
        {
            _param = (IpThresholdOtsuParams)param;
        }
    }
}
