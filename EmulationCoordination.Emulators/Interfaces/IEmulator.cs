using EmulationCoordination.Roms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators.Interfaces
{
    public interface IEmulator : IReadOnlyEmulator
    {
        void ExecuteRom(RomData rom);
    }
}
