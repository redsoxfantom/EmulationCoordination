using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;
using EmulationCoordination.Utilities;
using System.IO;

namespace EmulationCoordination.Emulators.Emulators.Windows
{
    public class FusionEmulator : BaseEmulator
    {
        public override List<EmulatorConsoles> ConsoleNames => new List<EmulatorConsoles>(){EmulatorConsoles.MASTER_SYSTEM};
        public override string EmulatorName => "Fusion";
        public override string Version => "3.64";

        private string downloadUrl = "http://www.emulator-zone.com/download.php/emulators/genesis/fusion/Fusion364.zip";

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
            String executable = Path.Combine(InstallDirectory, "Fusion.exe");
            Command cmd = new Command()
            {
                Executable = executable,
                Arguments = String.Format("\"{0}\"", rom.Path)
            };
            return cmd;
        }
    }
}
