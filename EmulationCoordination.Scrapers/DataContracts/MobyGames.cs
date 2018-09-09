using EmulationCoordination.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Scrapers.DataContracts
{
    public class MobyGamesSearchResults
    {
        public MobyGamesSearchResult[] games { get; set; }
    }

    public class MobyGamesSearchResult
    {
        public int game_id { get; set; }
        public string title { get; set; }
    }

    public class MobyGamesGetDataResult
    {
        public string description { get; set; }
        public float moby_score { get; set; }
        public MobyGamesPlatform[] platform { get; set; }
        public MobyGamesImage sample_cover { get; set; }
        public MobyGamesImage[] sample_screenshots {get;set;}
        public string title { get; set; }
    }

    public class MobyGamesImage
    {
        public int height { get; set; }
        public string image { get; set; }
        public int width { get; set; }
    }

    public class MobyGamesPlatform
    {
        public DateTime first_release_date { get; set; }
        public EmulatorConsoles platform_id { get; set; }
    }

    public class MobyGamesConsoleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(EmulatorConsoles));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            int consoleid = (int)reader.Value;
            switch(consoleid)
            {
                case 14:
                    return EmulatorConsoles.GAMECUBE;
                case 10:
                    return EmulatorConsoles.GAME_BOY;
                case 12:
                    return EmulatorConsoles.GAME_BOY_ADVANCE;
                case 11:
                    return EmulatorConsoles.GAME_BOY_COLOR;
                case 26:
                    return EmulatorConsoles.MASTER_SYSTEM;
                case 22:
                    return EmulatorConsoles.NES;
                case 9:
                    return EmulatorConsoles.NINTENDO_64;
                case 82:
                    return EmulatorConsoles.NINTENDO_WII;
                case 7:
                    return EmulatorConsoles.PLAYSTATION_2;
                case 81:
                    return EmulatorConsoles.PLAYSTATION_3;
                case 15:
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

    public class MobyGamesReleaseDateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(DateTime));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            String date = (String)reader.Value;
            if (String.IsNullOrEmpty(date))
            {
                return DateTime.MinValue;
            }

            return DateTime.ParseExact(date, "yyyy-MM-dd",null);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
