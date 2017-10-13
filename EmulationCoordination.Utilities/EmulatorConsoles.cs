using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Utilities
{
    public class EmulatorConsoles
    {
        public static readonly EmulatorConsoles GAME_BOY = 
            new EmulatorConsoles("Game Boy", ConsoleImages.GameBoy, new List<string>() { ".gb" });
        public static readonly EmulatorConsoles GAME_BOY_COLOR = 
            new EmulatorConsoles("Game Boy Color", ConsoleImages.GameBoyColor, new List<string>() { ".gbc" });
        public static readonly EmulatorConsoles GAME_BOY_ADVANCE = 
            new EmulatorConsoles("Game Boy Advance", ConsoleImages.GameBoyAdvance, new List<string>() { ".gba" });
        public static readonly EmulatorConsoles NINTENDO_64 = 
            new EmulatorConsoles("Nintendo 64", ConsoleImages.N64, new List<string>() { ".n64", ".z64" });
        public static readonly EmulatorConsoles SNES = 
            new EmulatorConsoles("SNES", ConsoleImages.SNES, new List<string>() { ".sfc" });
        public static readonly EmulatorConsoles GAMECUBE = 
            new EmulatorConsoles("GameCube", ConsoleImages.Gamecube, new List<string>() { ".iso" });
        public static readonly EmulatorConsoles MASTER_SYSTEM = 
            new EmulatorConsoles("Sega Master System", ConsoleImages.SegaMasterSystem, new List<string>() { ".sms" });
        public static readonly EmulatorConsoles PLAYSTATION_2 = 
            new EmulatorConsoles("Playstation 2", ConsoleImages.PS2, new List<string>() { ".iso" });
        public static readonly EmulatorConsoles NINTENDO_WII =
            new EmulatorConsoles("Nintendo Wii", ConsoleImages.UnknownConsole, new List<string>() { ".iso" });
        public static readonly EmulatorConsoles UNKNOWN = 
            new EmulatorConsoles("Unknown Console Type", ConsoleImages.UnknownConsole);

        public static IEnumerable<EmulatorConsoles> Values
        {
            get
            {
                yield return GAME_BOY;
                yield return GAME_BOY_COLOR;
                yield return GAME_BOY_ADVANCE;
                yield return NINTENDO_64;
                yield return SNES;
                yield return GAMECUBE;
                yield return MASTER_SYSTEM;
                yield return PLAYSTATION_2;
                yield return NINTENDO_WII;
            }
        }

        public static bool operator ==(EmulatorConsoles a, EmulatorConsoles b)
        {
            bool aIsNull = object.ReferenceEquals(a, null);
            bool bIsNull = object.ReferenceEquals(b, null);

            if((aIsNull && !bIsNull) || (!aIsNull && bIsNull))
            {
                return false;
            }
            if(aIsNull && bIsNull)
            {
                return true;
            }

            return a.FriendlyName == b.FriendlyName;
        }

        public static bool operator !=(EmulatorConsoles a, EmulatorConsoles b)
        {
            bool aIsNull = object.ReferenceEquals(a, null);
            bool bIsNull = object.ReferenceEquals(b, null);

            if ((aIsNull && !bIsNull) || (!aIsNull && bIsNull))
            {
                return true;
            }
            if (aIsNull && bIsNull)
            {
                return false;
            }

            return a.FriendlyName != b.FriendlyName;
        }

        public override bool Equals(object obj)
        {
            EmulatorConsoles other = obj as EmulatorConsoles;
            if(other != null)
            {
                return other.FriendlyName == FriendlyName;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return FriendlyName.GetHashCode();
        }

        public override string ToString()
        {
            return FriendlyName;
        }

        public String FriendlyName { get; }
        public List<String> FileExtensions { get; }
        
        public Image ConsoleImage { get; }

        public EmulatorConsoles(String friendlyName, Image consoleImage, List<String> acceptableExtensions = null)
        {
            if(acceptableExtensions == null)
            {
                FileExtensions = new List<string>();
            }
            else
            {
                FileExtensions = acceptableExtensions;
            }
            FriendlyName = friendlyName;
            if (consoleImage == null)
            {
                ConsoleImage = ConsoleImages.UnknownConsole;
            }
            else
            {
                ConsoleImage = consoleImage;
            }
        }
    }

    public class ConsoleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(EmulatorConsoles));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            String consoleFriendlyName = (string)reader.Value;
            return EmulatorConsoles.Values.Where(f => f.FriendlyName == consoleFriendlyName).First();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((EmulatorConsoles)value).FriendlyName);
        }
    }
}
