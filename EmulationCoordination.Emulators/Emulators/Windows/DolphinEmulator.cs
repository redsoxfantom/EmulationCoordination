using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;
using EmulationCoordination.Utilities;
using System.IO;
using System.Diagnostics;

namespace EmulationCoordination.Emulators.Emulators.Windows
{
    public class DolphinEmulator : BaseEmulator
    {
        private string downloadUrl = "http://dl-mirror.dolphin-emu.org/5.0/dolphin-x64-5.0.exe";

        public override List<EmulatorConsoles> ConsoleNames => new List<EmulatorConsoles>()
        {
            EmulatorConsoles.GAMECUBE
        };
        public override string EmulatorName => "Dolphin";
        public override string Version => "5.0";

        protected override bool ChildSpecificDelete()
        {
            return BasicDelete();
        }

        protected override bool ChildSpecificInstall()
        {
            return BasicDownload(downloadUrl,"dolphin.exe");
        }

        protected override Command CreateCommand(RomData rom)
        {
            return null;
        }
    }
}
