using EmulationCoordination.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Scrapers.DataContracts.Igdb
{
    public class IgdbData
    {
        public string id { get; set; }
        public string name { get; set; }
        public IgdbReleaseDate[] release_dates { get; set; }
    }
    public class IgdbReleaseDate
    {
        public EmulatorConsoles platform { get; set; }
        public DateTime date { get; set; }
    }

    public class IgdbReleaseDateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(DateTime));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long timeMillis = (long)reader.Value;
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(timeMillis);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Not intended to be used to write out json
            throw new NotImplementedException();
        }
    }

    public class IgdbPlatformConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(EmulatorConsoles));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch ((long)reader.Value)
            {
                case 33:
                    return EmulatorConsoles.GAME_BOY;
                case 22:
                    return EmulatorConsoles.GAME_BOY_COLOR;
                case 24:
                    return EmulatorConsoles.GAME_BOY_ADVANCE;
                case 4:
                    return EmulatorConsoles.NINTENDO_64;
                default:
                    return EmulatorConsoles.UNKNOWN;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Not intended to be used to write out json
            throw new NotImplementedException();
        }
    }
}
