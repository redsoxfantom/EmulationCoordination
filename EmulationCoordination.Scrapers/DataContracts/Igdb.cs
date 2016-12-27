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
        public string storyline { get; set; }
        public double rating { get; set; }
        public long[] developers { get; set; }
        public long[] publishers { get; set; }
        public IgdbReleaseDate[] release_dates { get; set; }
        public IgdbScreenshotData[] screenshots { get; set; }
        public IgdbGameMode[] game_modes { get; set; }
    }
    public class IgdbGameMode
    {
        public string NumPlayers { get; set; }
    }
    public class IgdbReleaseDate
    {
        public EmulatorConsoles platform { get; set; }
        public DateTime date { get; set; }
    }
    public class IgdbScreenshotData
    {
        public string url { get; set; }
        public string cloudinary_id { get; set; }
        public long width { get; set; }
        public long height { get; set; }
    }
    public class IgdbGameModeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(IgdbGameMode));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch((long)reader.Value)
            {
                case 1:
                    return new IgdbGameMode() { NumPlayers = "1" };
                case 5:
                    return new IgdbGameMode() { NumPlayers = "4+" };
                case 4:
                    return new IgdbGameMode() { NumPlayers = "2" };
                case 3:
                    return new IgdbGameMode() { NumPlayers = "2" };
                case 2:
                    return new IgdbGameMode() { NumPlayers = "4" };
                default:
                    throw new Exception("Could not convert " + reader.Value);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Not intended to be used to write out json
            throw new NotImplementedException();
        }
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
