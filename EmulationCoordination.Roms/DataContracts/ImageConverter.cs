using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Roms.DataContracts
{
    public class ImageConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(Bitmap));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string base64String = (string)reader.Value;
            byte[] gameImageArry = Convert.FromBase64String(base64String);
            Image gameImage = Bitmap.FromStream(new MemoryStream(gameImageArry));

            return gameImage;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Image gameImage = (Image)value;
            String base64Image = null;
            using (MemoryStream mem = new MemoryStream())
            {
                gameImage.Save(mem, gameImage.RawFormat);
                byte[] imgBytes = mem.ToArray();
                base64Image = Convert.ToBase64String(imgBytes);
            }

            writer.WriteValue(base64Image);
        }
    }
}
