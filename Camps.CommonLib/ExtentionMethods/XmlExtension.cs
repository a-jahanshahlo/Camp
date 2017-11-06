using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Camps.CommonLib.ExtentionMethods
{
    public static class XmlExtension
    {
        public static string Serialize<T>(this T value)
        {
            if (value == null) return string.Empty;

            var xmlserializer = new XmlSerializer(typeof(T), new Type[]{typeof(T)});


            using (StringWriter stringWriter = new Utf8StringWriter())
            {
               
                using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true, Encoding = System.Text.Encoding.UTF8 }))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
        }
        public static T DeSerialize<T>(this T value, string path)
        {
            if (value == null) throw new NullReferenceException("The object is null");
            using (TextReader reader = new StreamReader(path, System.Text.Encoding.UTF8))
            {
                var deserializer = new XmlSerializer(typeof(T));
                object obj = deserializer.Deserialize(reader);
                return (T)obj;
            }
        }
    }
}
