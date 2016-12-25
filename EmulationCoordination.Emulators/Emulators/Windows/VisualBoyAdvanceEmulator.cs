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

        public override bool Delete()
        {
            if (!Installed)
            {
                return false;
            }
            
            Directory.Delete(InstallDirectory, true);

            Installed = false;

            return true;
        }

        public override bool DownloadAndInstall()
        {
            if (Installed)
            {
                return true;
            }
            
            Directory.CreateDirectory(InstallDirectory);
            String targetFile = Path.Combine(InstallDirectory, "download.zip");
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(downloadUrl, targetFile);
            }
            using (ZipFile file = ZipFile.Read(targetFile))
            {
                file.ExtractAll(InstallDirectory);
            }

            Installed = true;

            return true;
        }

        public override void ExecuteRom(string PathToRom)
        {
            throw new NotImplementedException();
        }
    }
}
