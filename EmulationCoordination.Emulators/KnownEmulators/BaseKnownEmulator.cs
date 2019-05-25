using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators.KnownEmulators
{
    public abstract class BaseKnownEmulator : IKnownEmulator
    {
        public abstract string Name { get; }
        public abstract string Arguments { get; }
        public abstract string Version { get; }
        public abstract List<EmulatorConsoles> SupportedConsoles { get; }
        public override string ToString()
        {
            return String.Format("{0} - {1}",Name,Version);
        }
    }
}
