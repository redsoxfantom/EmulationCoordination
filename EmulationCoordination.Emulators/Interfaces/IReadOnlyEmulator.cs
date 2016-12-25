using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators.Interfaces
{
    public interface IReadOnlyEmulator
    {
        String Version { get; }
        String EmulatorName { get; }
        List<EmulatorConsoles> ConsoleNames { get; }
        bool Installed { get; }
    }
}
