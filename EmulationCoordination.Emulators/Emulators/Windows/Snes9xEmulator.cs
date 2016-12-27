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
        private string downloadUrl = "http://www.emuparadise.me/emulators/files/user/SNES9x%20v1.53-1240.rar";

        public override List<EmulatorConsoles> ConsoleNames => new List<EmulatorConsoles>()
        {
            EmulatorConsoles.SNES
        };

        public override string EmulatorName => "SNES9X";

        public override string Version => "1.53";

        protected override bool ChildSpecificDelete()
        {
            return BasicDelete();
        }

        protected override bool ChildSpecificInstall()
        {
            return BasicDownloadAndUnzip(downloadUrl);
        }

        protected override Command CreateCommand(RomData rom)
        {
            throw new NotImplementedException();
        }
    }
}
