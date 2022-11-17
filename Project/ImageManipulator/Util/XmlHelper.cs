using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageManipulator.Util
{
    public class XmlHelper
    {
        public static void SetValue(XmlElement xmlElement, string keyName, string value)
        {
            XmlElement subElement = xmlElement.OwnerDocument.CreateElement("", keyName, "");
            subElement.InnerText = value;
            xmlElement.AppendChild(subElement);
        }

        public static void SetValue(XmlElement xmlElement, string keyName, string value, string index)
        {
            XmlElement subElement = xmlElement.OwnerDocument.CreateElement("", keyName, "");
            subElement.InnerText = value;
            xmlElement.AppendChild(subElement);
            subElement.SetAttribute("Index", index);
        }

        public static string GetValue(XmlElement xmlElement, string keyName, string defaultValue)
        {
            XmlElement subElement = xmlElement[keyName];
            if (subElement == null)
                return defaultValue;

            return subElement.InnerText;
        }

        public static void SetPointValue(XmlDocument xmlDocument, XmlElement element, string KeyName, string pointX, string pointY)
        {
            XmlElement realPointElement = xmlDocument.CreateElement("", KeyName, "");
            element.AppendChild(realPointElement);

            SetValue(realPointElement, "PointX", pointX);
            SetValue(realPointElement, "PointY", pointY);

        }
    }
}
