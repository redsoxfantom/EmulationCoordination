using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators.Interfaces
{
    public interface IKnownEmulator
    {
        String Name { get; }
        String Arguments { get; }
        String Version { get; }
        List<EmulatorConsoles> SupportedConsoles { get; }
    }
}
