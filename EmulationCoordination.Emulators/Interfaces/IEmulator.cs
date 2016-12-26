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
        bool DownloadAndInstall();
        bool Delete();
        void ExecuteRom(IRomData rom);
        new bool Installed { get; set; }
        new String InstallDirectory { get; set; }
    }
}
