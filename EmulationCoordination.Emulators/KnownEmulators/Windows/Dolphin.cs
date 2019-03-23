using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators.KnownEmulators.Windows
{
    public class Dolphin_5_0 : IKnownEmulator
    {
        public string Name => "Dolphin";

        public string Arguments => "--exec=\"$FULL_ROM_PATH\" --batch";

        public string Version => "5.0";

        public List<EmulatorConsoles> SupportedConsoles => new List<EmulatorConsoles>() {
            EmulatorConsoles.GAMECUBE,
            EmulatorConsoles.NINTENDO_WII
        };
    }
}
