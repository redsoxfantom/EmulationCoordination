using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators.KnownEmulators.Windows
{
    public class Dolphin_5_0 : BaseKnownEmulator
    {
        public override string Name => "Dolphin";

        public override string Arguments => "--exec=\"$FULL_ROM_PATH\" --batch";

        public override string Version => "5.0";

        public override List<EmulatorConsoles> SupportedConsoles => new List<EmulatorConsoles>() {
            EmulatorConsoles.GAMECUBE,
            EmulatorConsoles.NINTENDO_WII
        };
    }
}
