using EmulationCoordination.Emulators.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators.Emulators
{
    public abstract class BaseEmulator : IEmulator
    {
        public virtual List<EmulatorConsoles> ConsoleNames => new List<EmulatorConsoles>();

        public virtual string EmulatorName => String.Empty;

        public bool Installed { get; set; }

        public virtual string Version => String.Empty;

        public override bool Equals(object obj)
        {
            IReadOnlyEmulator other = obj as IReadOnlyEmulator;
            if(other != null)
            {
                return (other.EmulatorName == EmulatorName &&
                        other.Version == Version);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return EmulatorName.GetHashCode() ^ Version.GetHashCode();
        }

        public abstract bool Delete();

        public abstract bool DownloadAndInstall();

        public abstract void ExecuteRom(string PathToRom);
    }
}
