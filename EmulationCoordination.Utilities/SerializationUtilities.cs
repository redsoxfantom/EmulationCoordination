using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace EmulationCoordination.Utilities
{
    public class SerializationUtilities
    {
        public static T DeserializeString<T>(String stringToDeserialize, DataFormat stringFormat, params object[] converters)
        {
            if(stringFormat == DataFormat.XML)
            {
                return DeserializeXmlString<T>(stringToDeserialize);
            }
            else
            {
                List<JsonConverter> jsonConverters = new List<JsonConverter>();
                foreach(var converter in converters)
                {
                    jsonConverters.Add((JsonConverter)converter);
                }
                return DeserializeJsonString<T>(stringToDeserialize,jsonConverters.ToArray());
            }
        }

        private static T DeserializeXmlString<T>(string stringToDeserialize)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(stringToDeserialize))
            {
                using (XmlReader xmlReader = XmlReader.Create(reader))
                {
                    return (T)ser.Deserialize(xmlReader);
                }
            }
        }

        private static T DeserializeJsonString<T>(string stringToDeserialize, params JsonConverter[] converters)
        {
            return JsonConvert.DeserializeObject<T>(stringToDeserialize,converters);
        }
    }

    public enum DataFormat
    {
        JSON,
        XML
    }
}
