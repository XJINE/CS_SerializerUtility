using System.Text;

namespace System.IO
{
    public static class JsonUtility
    {
        #region Method

        public static string Serialize<T>(T obj)
        {
            return DataContractJsonUtility.Serialize<T>(obj, Encoding.UTF8);
        }

        public static string Serialize<T>(T obj, Encoding encoding)
        {
            var memoryStream = new MemoryStream();
            var streamReader = new StreamReader(memoryStream, encoding);

            var serializer = new Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(memoryStream, obj);

            memoryStream.Position = 0;

            string json = streamReader.ReadToEnd();

            memoryStream.Dispose();
            streamReader.Dispose();

            return json;
        }

        public static T Deserialize<T>(string json)
        {
            return Deserialize<T>(json, Encoding.UTF8);
        }

        public static T Deserialize<T>(string json, Encoding encoding)
        {
            var memoryStream = new MemoryStream(encoding.GetBytes(json));
                memoryStream.Position = 0;

            var serializer = new Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
            var serializedObject = (T)serializer.ReadObject(memoryStream);

            memoryStream.Dispose();

            return serializedObject;
        }

        #endregion Method
    }
}