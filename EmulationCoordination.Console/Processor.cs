using EmulationCoordination.Emulators;
using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Roms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination
{
    public class Processor
    {
        EmulatorManager emuMgr;
        RomManager romMgr;
        List<IReadOnlyEmulator> emulators;
        List<IReadOnlyEmulator> installedEmulators;
        List<IReadOnlyEmulator> availableEmulators;
        List<IReadOnlyEmulator> customEmulators;

        public Processor()
        {
            emuMgr = EmulatorManager.Instance;
            romMgr = RomManager.Instance;
        }

        public void Run()
        {
            String input = String.Empty;

            while (!(input == "exit"))
            {
                UpdateEmulatorInfo();
                ConsoleUtilities.PrintEmulatorInfo();

                Console.WriteLine("(Available Commands: download, delete, exit, play)");
                Console.Write("> ");
                input = Console.ReadLine();

                if(input == "download")
                {
                    HandleDownloadEmulator();
                }
                if(input == "delete")
                {
                    HandleDeleteEmulator();
                }
                if(input == "play")
                {
                    HandlePlayEmulator();
                }
                Console.WriteLine();
            }
        }

        private void HandlePlayEmulator()
        {
            var selectedEmulator = SelectEmulator(installedEmulators.Concat(customEmulators).ToList());
            if(selectedEmulator != null)
            {
                var selectedRom = SelectRom(selectedEmulator);
                if(selectedRom != null)
                {
                    emuMgr.RunEmulator(selectedEmulator, selectedRom);
                }
            }
        }

        private RomData SelectRom(IReadOnlyEmulator emulator)
        {
            List<RomData> availableRoms = new List<RomData>();
            foreach(var consoleType in emulator.ConsoleNames)
            {
                availableRoms.AddRange(romMgr.GetRoms(consoleType));
            }

            Console.WriteLine(String.Format("The following games are available for {0}:", emulator.EmulatorName));
            for(int i = 1; i <= availableRoms.Count; i++)
            {
                Console.WriteLine(String.Format("{0}) {1}",i,availableRoms[i-1].FriendlyName));
            }

            int selectedRom;
            while(true)
            {
                Console.Write("Enter a game's number ('exit' to quit) > ");
                String input = Console.ReadLine();
                if (input == "exit")
                {
                    return null;
                }
                if (int.TryParse(input, out selectedRom))
                {
                    if (selectedRom > 0 && selectedRom <= availableRoms.Count)
                    {
                        return availableRoms[selectedRom - 1];
                    }
                }
            }
        }

        private void HandleDeleteEmulator()
        {
            var selectedEmulator = SelectEmulator(installedEmulators);

            if (selectedEmulator != null)
            {
                Console.WriteLine(String.Format("Deleting {0}", selectedEmulator.EmulatorName));
                emuMgr.DeleteEmulator(selectedEmulator);
                Console.WriteLine("Complete");
            }
        }

        private void HandleDownloadEmulator()
        {
            var selectedEmulator = SelectEmulator(availableEmulators);

            if(selectedEmulator!= null)
            {
                Console.WriteLine(String.Format("Downloading {0}", selectedEmulator.EmulatorName));
                emuMgr.DownloadAndInstallEmulator(selectedEmulator);
                Console.WriteLine("Complete");
            }
        }

        private IReadOnlyEmulator SelectEmulator(List<IReadOnlyEmulator> emulatorList)
        {
            int selectedEmulator;
            for (int i = 1; i < emulatorList.Count + 1; i++)
            {
                Console.WriteLine(String.Format("{0}: {1}",i,emulatorList[i-1].EmulatorName));
            }
            while (true)
            {
                Console.Write("Enter Emulator Number ('exit' to quit) > ");
                String input = Console.ReadLine();
                if(input == "exit")
                {
                    return null;
                }
                if (int.TryParse(input, out selectedEmulator))
                {
                    if (selectedEmulator > 0 && selectedEmulator <= emulatorList.Count)
                    {
                        return emulatorList[selectedEmulator];
                    }
                }
            }
        }

        private void UpdateEmulatorInfo()
        {
            emulators = emuMgr.GetAvailableEmulators();
            installedEmulators = emulators.Where(f => f.Installed && f.EmulatorType == EmulatorType.BUILTIN).ToList();
            availableEmulators = emulators.Where(f => !f.Installed && f.EmulatorType == EmulatorType.BUILTIN).ToList();
            customEmulators = emulators.Where(f => f.EmulatorType == EmulatorType.CUSTOM).ToList();
        }
    }
}
