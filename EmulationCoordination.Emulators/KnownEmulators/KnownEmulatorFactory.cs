using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Emulators.KnownEmulators.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators.KnownEmulators
{
    public class KnownEmulatorFactory
    {
        public static List<IKnownEmulator> GetKnownEmulatorTypes()
        {
            OperatingSystem os = Environment.OSVersion;
            switch(os.Platform)
            {
                case PlatformID.Win32NT:
                    return new List<IKnownEmulator>()
                    {
                        new Dolphin_5_0()
                    };
                default:
                    return new List<IKnownEmulator>();
            }
        }
    }
}
