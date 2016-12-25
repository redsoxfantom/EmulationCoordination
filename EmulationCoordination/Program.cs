using EmulationCoordination.Emulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination
{
    class Program
    {
        static void Main(string[] args)
        {
            EmulatorManager mgr = EmulatorManager.Instance;
            var emulators = mgr.GetAvailableEmulators();
            var installedEmulators = emulators.Where(f => f.Installed).ToList();
            var availableEmulators = emulators.Where(f => !f.Installed).ToList();

            Console.WriteLine("The Following Emulators Have Been Installed:");
            for (int i = 1; i <= installedEmulators.Count; i++)
            {
                var emulator = installedEmulators[i - 1];
                Console.WriteLine(String.Format("{0}) {1} ({2})", i, emulator.EmulatorName, emulator.Version));
            }
            Console.WriteLine();

            Console.WriteLine("The Following Emulators Are Available For Download:");
            for(int i = 1; i <= availableEmulators.Count; i++)
            {
                var emulator = availableEmulators[i - 1];
                Console.WriteLine(String.Format("{0}) {1} ({2})", i, emulator.EmulatorName, emulator.Version));
            }
            Console.WriteLine();
            
        }
    }
}
