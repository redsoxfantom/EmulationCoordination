using EmulationCoordination.Emulators;
using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Roms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Commands
{
    public class PlayCommand : ICommand
    {
        private EmulatorManager emuMgr;
        private RomManager romMgr;

        public PlayCommand()
        {
            romMgr = RomManager.Instance;
            emuMgr = EmulatorManager.Instance;
        }

        public string CommandName => "play";

        public void Execute()
        {
            var emulators = emuMgr.GetAvailableEmulators().Where(f => f.Installed == true).ToList();

            var selectedEmulator = ConsoleUtilities.SelectEmulator(emulators);
            if (selectedEmulator != null)
            {
                var selectedRom = SelectRom(selectedEmulator);
                if (selectedRom != null)
                {
                    emuMgr.RunEmulator(selectedEmulator, selectedRom);
                }
            }
        }

        private RomData SelectRom(IReadOnlyEmulator emulator)
        {
            List<RomData> availableRoms = new List<RomData>();
            foreach (var consoleType in emulator.ConsoleNames)
            {
                availableRoms.AddRange(romMgr.GetRoms(consoleType));
            }

            Console.WriteLine(String.Format("The following games are available for {0}:", emulator.EmulatorName));
            for (int i = 1; i <= availableRoms.Count; i++)
            {
                var rom = availableRoms[i - 1];
                Console.WriteLine(String.Format("{0}) {1} (Time Played: {2})", i, rom.FriendlyName, rom.PrettyPrintPlayTime()));
            }

            int selectedRom;
            while (true)
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
    }
}
