using System;
using System.Linq;
using EmulationCoordination.Emulators;

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
    }
}
