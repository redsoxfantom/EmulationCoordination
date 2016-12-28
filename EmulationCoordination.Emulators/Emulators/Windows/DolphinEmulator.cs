using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;
using EmulationCoordination.Utilities;

namespace EmulationCoordination.Emulators.Emulators.Windows
{
    public class DolphinEmulator : BaseEmulator
    {
        public override List<EmulatorConsoles> ConsoleNames => new List<EmulatorConsoles>()
        {
            EmulatorConsoles.GAMECUBE
        };
        public override string EmulatorName => "Dolphin";
        public override string Version => "5.0";

        protected override bool ChildSpecificDelete()
        {
            throw new NotImplementedException();
        }

        protected override bool ChildSpecificInstall()
        {
            throw new NotImplementedException();
        }

        protected override Command CreateCommand(RomData rom)
        {
            throw new NotImplementedException();
        }
    }
}
