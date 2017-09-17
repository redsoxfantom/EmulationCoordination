using EmulationCoordination.Emulators;
using EmulationCoordination.Emulators.Emulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Commands
{
    public class CustomCommand : ICommand
    {
        EmulatorManager emuMgr;

        public CustomCommand()
        {
            emuMgr = EmulatorManager.Instance;
        }

        public string CommandName => "createcustom";

        public void Execute()
        {
            Console.WriteLine("Enter path to custom emulator:");
            Console.Write("> ");
            String pathToEmulator = Console.ReadLine();
            
        }
    }
}
