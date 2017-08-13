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
        private string downloadUrl = "https://dl.dolphin-emu.org/builds/dolphin-master-5.0-5132-x64.7z";

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
            if(!BasicDownload(downloadUrl,"download.7z"))
            {
                return false;
            }
            return BasicUnzip("download.7z");
        }

        protected override Command CreateCommand(RomData rom)
        {
            Command cmd = new Command();
            cmd.Executable = Path.Combine(InstallDirectory, "Dolphin.exe");
            cmd.Arguments = String.Format("-c -b -e \"{0}\"",rom.Path);

            return cmd;
        }
    }
}
