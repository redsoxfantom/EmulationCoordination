using EmulationCoordination.Emulators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationCoordination.Roms;
using EmulationCoordination.Utilities;
using System.IO;
using System.Diagnostics;

namespace EmulationCoordination.Emulators.Emulators
{
    public class CustomEmulator : IEmulator
    {
        public string CommandLineArguments { get; set; }
        public string PathToExecutable { get; set; }
        public string InstallDirectory
        {
            get
            {
                return Path.GetDirectoryName(PathToExecutable);
            }
        }
        public string Version { get; set; }

        public string EmulatorName { get; set; }

        public List<EmulatorConsoles> ConsoleNames { get; set; }

        public void ExecuteRom(RomData rom)
        {
            string args = CommandLineArguments
                            .Replace("$ROM_NAME", Path.GetFileName(rom.Path))
                            .Replace("$ROM_PATH", Path.GetDirectoryName(rom.Path))
                            .Replace("$FULL_ROM_PATH", rom.Path);

            Process proc = new Process();
            proc.StartInfo.FileName = PathToExecutable;
            proc.StartInfo.Arguments = args;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.WorkingDirectory = InstallDirectory;
            proc.Start();
            proc.WaitForExit();
        }

        public CustomEmulator(String PathToExecutable, String CommandLineArguments, 
            String Version, String EmulatorName, List<EmulatorConsoles> EmulatorConsoles)
        {
            this.PathToExecutable = PathToExecutable;
            this.CommandLineArguments = CommandLineArguments;
            this.Version = Version;
            this.EmulatorName = EmulatorName;
            ConsoleNames = EmulatorConsoles;
        }
    }
}
