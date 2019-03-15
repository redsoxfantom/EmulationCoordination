using System;
using System.Linq;
using EmulationCoordination.Emulators;
using EmulationCoordination.Emulators.Interfaces;
using System.Collections.Generic;
using EmulationCoordination.Roms;
using EmulationCoordination.Utilities;

namespace EmulationCoordination
{
    public class ConsoleUtilities
    {
        private ConsoleUtilities() { }

        public static void PrintEmulatorInfo()
		{
			var emuMgr = EmulatorManager.Instance;
			var emulators = emuMgr.GetAvailableEmulators();
			var installedEmulators = emulators.Where(f => f.Installed && f.EmulatorType == EmulatorType.BUILTIN).ToList();
			var availableEmulators = emulators.Where(f => !f.Installed && f.EmulatorType == EmulatorType.BUILTIN).ToList();
			var customEmulators = emulators.Where(f => f.EmulatorType == EmulatorType.CUSTOM).ToList();

			Console.WriteLine("The Following Emulators Have Been Installed:");
			for (int i = 1; i <= installedEmulators.Count; i++)
			{
				var emulator = installedEmulators[i - 1];
				Console.WriteLine(String.Format("{0}) {1} ({2}) ({3})", i, emulator.EmulatorName, emulator.Version, string.Join(",", emulator.ConsoleNames)));
			}
			Console.WriteLine();

			Console.WriteLine("The Following Emulators Are Available For Download:");
			for (int i = 1; i <= availableEmulators.Count; i++)
			{
				var emulator = availableEmulators[i - 1];
				Console.WriteLine(String.Format("{0}) {1} ({2}) ({3})", i, emulator.EmulatorName, emulator.Version, string.Join(",", emulator.ConsoleNames)));
			}
			Console.WriteLine();

			Console.WriteLine("The Following Custom Emulators Have Been Defined:");
			for (int i = 1; i <= customEmulators.Count; i++)
			{
				var emulator = customEmulators[i - 1];
				Console.WriteLine(String.Format("{0}) {1} ({2}) ({3})", i, emulator.EmulatorName, emulator.Version, string.Join(",", emulator.ConsoleNames)));
			}
			Console.WriteLine();
		}

        public static void RunWithArguments(Options args)
        {
            var emuMgr = EmulatorManager.Instance;
            var romMgr = RomManager.Instance;

            var emulators = emuMgr.GetAvailableEmulators();
            emulators = emulators.Where(e => { return e.EmulatorName == args.Emulator; }).ToList();
            if (emulators.Count < 1)
            {
                throw new EmulationCoordinationException(String.Format("Emulator {0} not defined", args.Emulator));
            }

            IReadOnlyEmulator emulator = null;
            if (String.IsNullOrEmpty(args.Version))
            {
                emulator = emulators.First();
            }
            else
            {
                emulators = emulators.Where(e => { return e.Version == args.Version; }).ToList();
                if (emulators.Count < 1)
                {
                    throw new EmulationCoordinationException(string.Format("{0}.{1} is not a defined emulator", args.Emulator, args.Version));
                }
                emulator = emulators.First();
            }

            List<RomData> roms = new List<RomData>();
            foreach (var console in emulator.ConsoleNames)
            {
                roms.AddRange(romMgr.GetRoms(console));
            }
            roms = roms.Where(r => { return r.FriendlyName == args.Rom; }).ToList();
            if(roms.Count < 1)
            {
                throw new EmulationCoordinationException(string.Format("Rom {0} not available for emulator {1}", args.Rom, args.Emulator));
            }
            RomData rom = roms.First();

            emuMgr.RunEmulator(emulator, rom);
        }

        public static IReadOnlyEmulator SelectEmulator(List<IReadOnlyEmulator> emulatorList)
        {
            int selectedEmulator;
            for (int i = 1; i < emulatorList.Count + 1; i++)
            {
                Console.WriteLine(String.Format("{0}: {1}", i, emulatorList[i - 1].EmulatorName));
            }
            while (true)
            {
                Console.Write("Enter Emulator Number ('exit' to quit) > ");
                String input = Console.ReadLine();
                if (input == "exit")
                {
                    return null;
                }
                if (int.TryParse(input, out selectedEmulator))
                {
                    if (selectedEmulator > 0 && selectedEmulator <= emulatorList.Count)
                    {
                        return emulatorList[selectedEmulator - 1];
                    }
                }
            }
        }
    }
}
