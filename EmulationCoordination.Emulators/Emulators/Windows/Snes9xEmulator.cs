using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;
using EmulationCoordination.Utilities;

namespace EmulationCoordination.Emulators.Emulators.Windows
{
    public class Snes9xEmulator : BaseEmulator
    {
        public override List<EmulatorConsoles> ConsoleNames => new List<EmulatorConsoles>()
        {
            EmulatorConsoles.SNES
        };

        public override string EmulatorName => "SNES9X";

        public override string Version => "1.53";

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
