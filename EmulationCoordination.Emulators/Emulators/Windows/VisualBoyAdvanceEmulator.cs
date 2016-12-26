using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Utilities;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;

namespace EmulationCoordination.Emulators.Emulators.Windows
{
    public class VisualBoyAdvanceEmulator : BaseEmulator
    {
        private String downloadUrl = "https://sourceforge.net/projects/vba/files/VisualBoyAdvance/1.7.2/VisualBoyAdvance-1.7.2-SDL-Win32.zip/download";

        public override List<EmulatorConsoles> ConsoleNames => new List<EmulatorConsoles>() {
            EmulatorConsoles.GAME_BOY,
            EmulatorConsoles.GAME_BOY_ADVANCE,
            EmulatorConsoles.GAME_BOY_COLOR
        };

        public override string EmulatorName => "Visual Boy Advance";

        public override string Version => "1.7.2";

        protected override bool ChildSpecificDelete()
        {
            return BasicDelete();
        }
        
        protected override bool ChildSpecificInstall()
        {
            return BasicDownloadAndUnzip(downloadUrl);
        }

        protected override Command CreateCommand(IRomData rom)
        {
            String executable = Path.Combine(InstallDirectory, "VisualBoyAdvance-SDL.exe");
            Command cmd = new Command()
            {
                Executable = executable,
                Arguments = String.Format("\"{0}\"",rom.Path)
            };
            return cmd;
        }
    }
}
