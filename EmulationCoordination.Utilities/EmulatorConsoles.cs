using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Utilities
{
    public class EmulatorConsoles
    {
        public static readonly EmulatorConsoles GAME_BOY = new EmulatorConsoles("Game Boy");
        public static readonly EmulatorConsoles GAME_BOY_COLOR = new EmulatorConsoles("Game Boy Color");
        public static readonly EmulatorConsoles GAME_BOY_ADVANCE = new EmulatorConsoles("Game Boy Advance");
        public static readonly EmulatorConsoles NINTENDO_64 = new EmulatorConsoles("Nintendo 64");

        public static IEnumerable<EmulatorConsoles> Values
        {
            get
            {
                yield return GAME_BOY;
                yield return GAME_BOY_COLOR;
                yield return GAME_BOY_ADVANCE;
                yield return NINTENDO_64;
            }
        }

        public String FriendlyName { get; }

        private EmulatorConsoles(String friendlyName)
        {
            FriendlyName = friendlyName;
        }
    }
}
