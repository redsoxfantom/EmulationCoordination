using EmulationCoordination.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Scrapers.DataContracts.GiantBomb
{
    public class GiantBombSearchResults
    {
        public GiantBombGameData[] results { get; set; }
    }

    public class GiantBombGameResults
    {
        public GiantBombGameData results { get; set; }
    }

    public class GiantBombGameData
    {
        public string deck { get; set; }
        public long id { get; set; }
        public String name { get; set; }
        public DateTime original_release_date { get; set; }
        public GiantBombPlatform[] platforms { get; set; }
        public GiantBombImage image { get; set; }
        public GiantBombImage[] images { get; set; }
        public GiantBombCompany[] developers { get; set; }
        public GiantBombCompany[] publishers { get; set; }
        public GiantBombAdditionalDetail[] releases { get; set; }
        public GiantBombAdditionalDetail[] reviews { get; set; }
        public long score { get; set; }
        public long? maximum_players { get; set; }
        public long? minimum_players { get; set; }
    }

    public class GiantBombAdditionalDetail
    {
        public string api_detail_url { get; set; }
    }

    public class GiantBombCompany
    {
        public string name { get; set; }
    }

    public class GiantBombImage
    {
        public string icon_url { get; set; }
        public string medium_url { get; set; }
        public string screen_url { get; set; }
        public string small_url { get; set; }
        public string super_url { get; set; }
        public string thumb_url { get; set; }
        public string tiny_url { get; set; }
        public string tags { get; set; }
    }

    public class GiantBombPlatform
    {
        [JsonProperty("abbreviation")]
        public EmulatorConsoles platform { get; set; }
    }

    public class GiantBombConsoleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(EmulatorConsoles));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string abbreviation = (String)reader.Value;
            switch(abbreviation)
            {
                case "GB":
                    return EmulatorConsoles.GAME_BOY;
                case "GBA":
                    return EmulatorConsoles.GAME_BOY_ADVANCE;
                case "GBC":
                    return EmulatorConsoles.GAME_BOY_COLOR;
                case "N64":
                    return EmulatorConsoles.NINTENDO_64;
                case "SNES":
                    return EmulatorConsoles.SNES;
                default:
                    return EmulatorConsoles.UNKNOWN;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class GiantBombReleaseDateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(DateTime));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            String date = (String)reader.Value;
            return DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", null);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
