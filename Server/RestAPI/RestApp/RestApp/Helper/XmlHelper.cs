using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace RestApp.Helper
{
    public class XmlHelper
    {

        public static string GetXMLFromObject(object obj)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new UnicodeEncoding(false, false);
                settings.Indent = false;
                settings.OmitXmlDeclaration = false;

                using (StringWriter textWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                    {
                        serializer.Serialize(xmlWriter, obj);
                    }
                    return textWriter.ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"FreightApp.GetXMLFromObject() exception: {e.ToString()}");
                return "";
            }

        }

        public static Object GetObjectFromXml(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
                return obj;
            }
            catch (Exception e)
            {
                Console.WriteLine($"FreightApp.GetObjectFromXml() exception: {e.ToString()}");
                return null;
            }
        }

    }

}