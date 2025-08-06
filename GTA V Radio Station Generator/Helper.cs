using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace GTA_V_Radio_Station_Generator
{
    internal class Helper
    {


        public static List<T> DeserializeParams<T>(string FileName)
        {
            XDocument doc = new XDocument(XDocument.Load(FileName));
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            XmlReader reader = doc.CreateReader();
            List<T> result = (List<T>)serializer.Deserialize(reader);
            reader.Close();
            return result;
        }
        public static void SerializeParams<T>(List<T> paramList, string FileName)
        {
            XDocument doc = new XDocument();
            XmlSerializer serializer = new XmlSerializer(paramList.GetType());
            XmlWriter writer = doc.CreateWriter();
            serializer.Serialize(writer, paramList);
            writer.Close();
            File.WriteAllText(FileName, doc.ToString());
        }

    }
}
