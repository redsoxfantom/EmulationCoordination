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
        private string vcppredist = "https://www.microsoft.com/en-us/download/confirmation.aspx?id=48145&6B49FDFB-8E5B-4B07-BC31-15695C5A2143=1";
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
            string targetDir = Path.Combine(InstallDirectory, "download.zip");
            if (BasicDownload(vcppredist))
            {
                string vcc_renamed_file = Path.Combine(InstallDirectory, "vcc_redist.exe");
                File.Move(targetDir, vcc_renamed_file);
                Process vccInstallProc = new Process();
                vccInstallProc.StartInfo.FileName = vcc_renamed_file;
                vccInstallProc.Start();
                vccInstallProc.WaitForExit();

                return true;
            }
            else
            {
                return false;
            }
        }

        protected override Command CreateCommand(RomData rom)
        {
            return null;
        }
    }
}
