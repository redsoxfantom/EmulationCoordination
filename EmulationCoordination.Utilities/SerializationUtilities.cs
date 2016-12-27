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
        public static T DeserializeString<T>(String stringToDeserialize, DataFormat stringFormat)
        {
            if(stringFormat == DataFormat.XML)
            {
                return DeserializeXmlString<T>(stringToDeserialize);
            }
            else
            {
                return DeserializeJsonString<T>(stringToDeserialize);
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

        private static T DeserializeJsonString<T>(string stringToDeserialize)
        {
            return JsonConvert.DeserializeObject<T>(stringToDeserialize);
        }
    }

    public enum DataFormat
    {
        JSON,
        XML
    }
}
