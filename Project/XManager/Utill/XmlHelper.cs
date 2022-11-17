using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XManager.Utill
{
    public static class XmlHelper
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

        public static XmlElement WriteProgramInfo(XmlDocument xmlDocument)
        {
            XmlElement element = xmlDocument.CreateElement("", "XManager", "");
            xmlDocument.AppendChild(element);

            XmlHelper.SetValue(element, "Version", "1.0");

            return element;
        }
    }
}
