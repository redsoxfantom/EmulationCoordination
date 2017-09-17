using EmulationCoordination.Commands;
using EmulationCoordination.Emulators;
using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Roms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination
{
    public class Processor
    {
        CommandFactory factory;

        public Processor()
        {
            factory = new CommandFactory();
        }

        public void Run()
        {
            String input = String.Empty;

            while (!(input == "exit"))
            {
                ConsoleUtilities.PrintEmulatorInfo();

                Console.WriteLine(String.Format("(Available Commands: {0})",String.Join(", ",factory.GetCommandNames())));
                Console.Write("> ");
                input = Console.ReadLine();
                
                factory.ExecuteCommand(input);

                Console.WriteLine();
            }
        }
    }
}
