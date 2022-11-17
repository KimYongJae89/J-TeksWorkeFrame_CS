using ImageManipulator.ImageProcessingData;
using ImageManipulator.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageManipulator.Data
{
    public enum eXmlType
    {
        Roi, Settings, FilterList
    }

    public class XmlWriterBuilder
    {
        /// <summary>
        /// XmlWriterBuilder 상속받은 클래스를 타입에 맞게 생성한다.
        /// </summary>
        /// <param name="filePath">Xml파일경로</param>
        /// <param name="type">XmlType</param>
        /// <returns>생성된 객체</returns>
        public static XmlWriterManager Create(string filePath, eXmlType type)
        {
            XmlWriterManager writer = null;

            switch (type)
            {
                case eXmlType.Roi:
                    writer = new RoiWriter();
                    break;
                case eXmlType.Settings:
                    writer = new SettingsWriter();
                    break;
                case eXmlType.FilterList:
                    writer = new FilterListWriter();
                    break;
                default:
                    break;
            }
            return writer;
        }
    }
    public abstract class XmlWriterManager
    {
        protected XmlDocument _xmlDocument;
        public abstract void Write<T>(string filePath, List<T> group);
        public XmlElement WriteProgramInfo()
        {
            _xmlDocument = new XmlDocument();
            XmlElement element = _xmlDocument.CreateElement("", "ImageManipulator", "");
            _xmlDocument.AppendChild(element);

            XmlHelper.SetValue(element, "Version", "1.0");

            return element;
        }
    }

    public class RoiWriter : XmlWriterManager
    {
        public override void Write<T>(string filePath, List<T> group)
        {
            XmlElement element = base.WriteProgramInfo();

            XmlElement figureGroupElement = _xmlDocument.CreateElement("", "RoiList", "");
            element.AppendChild(figureGroupElement);
            int width = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width;
            int height = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height;
            XmlHelper.SetValue(figureGroupElement, "Width", width.ToString());
            XmlHelper.SetValue(figureGroupElement, "Height", height.ToString());

            WriteFigureGroup(_xmlDocument, figureGroupElement, group);

            _xmlDocument.Save(filePath);
        }

        private void WriteFigureGroup<T>(XmlDocument xmlDocument, XmlElement groupElement, List<T> figureGroup)
        {
            foreach (T obj in figureGroup)
            {
                Figure figure = (Figure)(object)obj;

                if (figure.Type == eFigureType.Rectangle)
                {
                    XmlElement figureElement = xmlDocument.CreateElement("", "Roi", "");
                    groupElement.AppendChild(figureElement);

                    RectangleF rect = ((tRectangleResult)figure.GetResult()).resultRectangle;

                    int orgWidth = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Width;
                    int orgHeight = Status.Instance().SelectedViewer.ImageManager.DisPlayImage.Height;
                    int nowWidth = Status.Instance().SelectedViewer.GetDrawBox().GetPictureBoxSize().Width;
                    int nowHeight = Status.Instance().SelectedViewer.GetDrawBox().GetPictureBoxSize().Height;

                    string id = figure.Id;
                    float left = rect.Left * orgWidth / (float)nowWidth;
                    float top = rect.Top * orgHeight / (float)nowHeight;
                    float right = rect.Right * orgWidth / (float)nowWidth;
                    float bottom = rect.Bottom * orgHeight / (float)nowHeight;

                    XmlHelper.SetValue(figureElement, "Id", id);
                    XmlHelper.SetValue(figureElement, "Left", left.ToString());
                    XmlHelper.SetValue(figureElement, "Top", top.ToString());
                    XmlHelper.SetValue(figureElement, "Right", right.ToString());
                    XmlHelper.SetValue(figureElement, "Bottom", bottom.ToString());
                }
            }
        }
    }

    public class SettingsWriter : XmlWriterManager
    {
        public override void Write<T>(string filePath, List<T> group)
        {

            XmlElement element = base.WriteProgramInfo();

            XmlElement figureGroupElement = _xmlDocument.CreateElement("", "Settings", "");
            element.AppendChild(figureGroupElement);

            WriteFovInfo(_xmlDocument, figureGroupElement);
            WriteSettings(_xmlDocument, figureGroupElement);

            _xmlDocument.Save(filePath);
        }

        private void WriteFovInfo(XmlDocument xmlDocument, XmlElement groupElement)
        {
            XmlElement figureElement = xmlDocument.CreateElement("", "Fov", "");
            groupElement.AppendChild(figureElement);

            XmlHelper.SetValue(figureElement, "FovWidth", Status.Instance().FovWidth.ToString());
            XmlHelper.SetValue(figureElement, "FovHeight", Status.Instance().FovHeight.ToString());
        }

        private void WriteSettings(XmlDocument xmlDocument, XmlElement groupElement)
        {
            XmlElement figureElement = xmlDocument.CreateElement("", "RecentFiles", "");
            groupElement.AppendChild(figureElement);

            foreach (string filePath in Status.Instance().GetRecentFiles())
            {
                XmlHelper.SetValue(figureElement, "Path", filePath);
            }
            XmlHelper.SetValue(figureElement, "Language", ((int)Status.Instance().Language).ToString());
            XmlHelper.SetValue(figureElement, "MeasurementType", ((int)Status.Instance().MeasureMentType).ToString());
            XmlHelper.SetValue(figureElement, "Pitch", Status.Instance().Pitch.ToString());
            XmlHelper.SetValue(figureElement, "UnitType", ((int)Status.Instance().MeasurementUnit).ToString());
        }
    }

    public class FilterListWriter : XmlWriterManager
    {
        public override void Write<T>(string filePath, List<T> group)
        {
            XmlElement element = base.WriteProgramInfo();

            XmlHelper.SetValue(element, "Bit", Status.Instance().SelectedViewer.ImageManager.GetBit().ToString());
            XmlHelper.SetValue(element, "Color", Status.Instance().SelectedViewer.ImageManager.IsColor().ToString());

            XmlElement filterListElement = _xmlDocument.CreateElement("", "FilterList", "");
            element.AppendChild(filterListElement);

            foreach (IPBase item in Status.Instance().SelectedViewer.ImageManager.ImageProcessingList)
            {
                item.Save(_xmlDocument, filterListElement);
            }

            _xmlDocument.Save(filePath);
        }
    }
}
