using EmulationCoordination.Emulators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators.Emulators
{
    public static class EmulatorRetriever
    {
        public static Dictionary<IReadOnlyEmulator, IEmulator> GetWindowsEmulators()
        {
            List<IEmulator> windowsEmulators = new List<IEmulator>()
            {
                new Windows.VisualBoyAdvanceEmulator()
            };
            return ConvertList(windowsEmulators);
        }

        private static Dictionary<IReadOnlyEmulator, IEmulator> ConvertList(List<IEmulator> emulators)
        {
            var returnDict = new Dictionary<IReadOnlyEmulator, IEmulator>();
            foreach (var emulator in emulators)
            {
                returnDict.Add(emulator, emulator);
            }
            return returnDict;
        }
    }
}
