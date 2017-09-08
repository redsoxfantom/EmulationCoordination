﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Utilities;
using EmulationCoordination.Roms;
using System.IO;

namespace EmulationCoordination.Emulators.Emulators.Windows
{
    public class Mupen64PlusEmulator : BaseBuiltinEmulator
    {
        private String downloadUrl = "https://github.com/mupen64plus/mupen64plus-core/releases/download/2.5/mupen64plus-bundle-win32-2.5.zip";

        public override string EmulatorName => "Mupen64Plus";

        public override string Version => "2.5.0";

        public override List<EmulatorConsoles> ConsoleNames => new List<EmulatorConsoles>
        {
            EmulatorConsoles.NINTENDO_64
        };

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
            String executable = Path.Combine(InstallDirectory, "mupen64plus-ui-console.exe");

            Command cmd = new Command()
            {
                Executable = executable,
                Arguments = String.Format("--fullscreen \"{0}\"", rom.Path)
            };
            return cmd;
        }
    }
}
