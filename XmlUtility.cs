using System.Text;
using System.Xml.Serialization;

namespace System.IO
{
    public static class XmlUtility
    {
        public static string ToXml<T>(T obj)
        {
            return XmlUtility.ToXml<T>(obj, Encoding.UTF8);
        }

        public static string ToXml<T>(T obj, Encoding encoding)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream, encoding))
                {
                    serializer.Serialize(writer, obj);
                    return encoding.GetString(stream.ToArray());
                }
            }
        }

        public static T FromXml<T>(string xml)
        {
            return XmlUtility.FromXml<T>(xml, Encoding.UTF8);
        }

        public static T FromXml<T>(string xml, Encoding encoding)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(xml)))
            {
                return (T)serializer.Deserialize(memoryStream);
            }
        }
    }
}