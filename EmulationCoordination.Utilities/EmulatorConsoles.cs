using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Utilities
{
    public class EmulatorConsoles
    {
        public static readonly EmulatorConsoles GAME_BOY = new EmulatorConsoles("Game Boy", new List<string>() { ".gb" });
        public static readonly EmulatorConsoles GAME_BOY_COLOR = new EmulatorConsoles("Game Boy Color", new List<string>() { ".gbc" });
        public static readonly EmulatorConsoles GAME_BOY_ADVANCE = new EmulatorConsoles("Game Boy Advance", new List<string>() { ".gba" });
        public static readonly EmulatorConsoles NINTENDO_64 = new EmulatorConsoles("Nintendo 64", new List<string>() { ".n64" });
        public static readonly EmulatorConsoles SNES = new EmulatorConsoles("SNES", new List<string>() { ".sfc" });
        public static readonly EmulatorConsoles GAMECUBE = new EmulatorConsoles("GameCube", new List<string>() { ".iso" });
        public static readonly EmulatorConsoles MASTER_SYSTEM = new EmulatorConsoles("Sega Master System", new List<string>() { ".sms" });
        public static readonly EmulatorConsoles UNKNOWN = new EmulatorConsoles("Unknown Console Type");

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

        public String FriendlyName { get; }
        public List<String> FileExtensions { get; }
        
        public EmulatorConsoles(String friendlyName, List<String> acceptableExtensions = null)
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
        }
    }
}
