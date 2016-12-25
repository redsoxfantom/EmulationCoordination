using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators.Interfaces
{
    public sealed class EmulatorConsoles
    {
        public static readonly EmulatorConsoles GAME_BOY = new EmulatorConsoles("Game Boy");
        public static readonly EmulatorConsoles GAME_BOY_COLOR = new EmulatorConsoles("Game Boy Color");
        public static readonly EmulatorConsoles GAME_BOY_ADVANCE = new EmulatorConsoles("Game Boy Advance");

        public String FriendlyName { get; }

        private EmulatorConsoles(String friendlyName)
        {
            FriendlyName = friendlyName;
        }
    }
}
