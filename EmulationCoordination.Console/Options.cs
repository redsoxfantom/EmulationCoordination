using CommandLine;
using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination
{
    public class Options
    {
        [Option('e',"emulator", Required = false)]
        public string Emulator { get; set; }
        [Option('v', "version", Required = false)]
        public string Version { get; set; }
        [Option('r', "rom", Required = false)]
        public string Rom { get; set; }
        public bool OptionsDefined { get; set; }

        public void ValidateArguments()
        {
            if(String.IsNullOrEmpty(Emulator) && String.IsNullOrEmpty(Version) && String.IsNullOrEmpty(Rom))
            {
                OptionsDefined = false;
            }
            else
            {
                OptionsDefined = true;
                if(String.IsNullOrEmpty(Emulator) || String.IsNullOrEmpty(Rom))
                {
                    throw new EmulationCoordinationException("Emulator and Rom must be defined");
                }
            }
        }
    }
}
