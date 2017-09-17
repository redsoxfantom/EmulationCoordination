using EmulationCoordination.Emulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Commands
{
    public class DownloadCommand : ICommand
    {
        EmulatorManager emuMgr;

        public DownloadCommand()
        {
            emuMgr = EmulatorManager.Instance;
        }

        public string CommandName => "download";

        public void Execute()
        {
            var availableEmulators = emuMgr.GetAvailableEmulators().Where(f => f.Installed == false).ToList();

            var selectedEmulator = ConsoleUtilities.SelectEmulator(availableEmulators);

            if (selectedEmulator != null)
            {
                Console.WriteLine(String.Format("Downloading {0}", selectedEmulator.EmulatorName));
                emuMgr.DownloadAndInstallEmulator(selectedEmulator);
                Console.WriteLine("Complete");
            }
        }
    }
}
