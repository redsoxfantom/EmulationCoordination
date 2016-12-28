﻿using EmulationCoordination.Emulators;
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
                PrintEmulatorInfo();

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
            int selectedEmulator = SelectEmulator(installedEmulators);
            if(selectedEmulator != -1)
            {
                var emulator = installedEmulators[selectedEmulator];
                var selectedRom = SelectRom(emulator);
                if(selectedRom != null)
                {
                    emuMgr.RunEmulator(emulator, selectedRom);
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
            int selectedEmulator = SelectEmulator(installedEmulators);

            if (selectedEmulator != -1)
            {
                var emulator = installedEmulators[selectedEmulator];
                Console.WriteLine(String.Format("Deleting {0}", emulator.EmulatorName));
                emuMgr.DeleteEmulator(emulator);
                Console.WriteLine("Complete");
            }
        }

        private void HandleDownloadEmulator()
        {
            int selectedEmulator = SelectEmulator(availableEmulators);

            if(selectedEmulator!= -1)
            {
                var emulator = availableEmulators[selectedEmulator];
                Console.WriteLine(String.Format("Downloading {0}", emulator.EmulatorName));
                emuMgr.DownloadAndInstallEmulator(emulator);
                Console.WriteLine("Complete");
            }
        }

        private int SelectEmulator(List<IReadOnlyEmulator> emulatorList)
        {
            int selectedEmulator;
            while (true)
            {
                Console.Write("Enter Emulator Number ('exit' to quit) > ");
                String input = Console.ReadLine();
                if(input == "exit")
                {
                    return -1;
                }
                if (int.TryParse(input, out selectedEmulator))
                {
                    if (selectedEmulator > 0 && selectedEmulator <= emulatorList.Count)
                    {
                        return selectedEmulator-1;
                    }
                }
            }
        }

        private void UpdateEmulatorInfo()
        {
            emulators = emuMgr.GetAvailableEmulators();
            installedEmulators = emulators.Where(f => f.Installed).ToList();
            availableEmulators = emulators.Where(f => !f.Installed).ToList();
        }

        private void PrintEmulatorInfo()
        {
            Console.WriteLine("The Following Emulators Have Been Installed:");
            for (int i = 1; i <= installedEmulators.Count; i++)
            {
                var emulator = installedEmulators[i - 1];
                Console.WriteLine(String.Format("{0}) {1} ({2})", i, emulator.EmulatorName, emulator.Version));
            }
            Console.WriteLine();

            Console.WriteLine("The Following Emulators Are Available For Download:");
            for (int i = 1; i <= availableEmulators.Count; i++)
            {
                var emulator = availableEmulators[i - 1];
                Console.WriteLine(String.Format("{0}) {1} ({2})", i, emulator.EmulatorName, emulator.Version));
            }
            Console.WriteLine();
        }
    }
}