using CommandLine;
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
        Options arguments;

        public Processor(string[] args)
        {
            factory = new CommandFactory();
            ParseArguments(args);
        }

        private void ParseArguments(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    arguments = o;
                    arguments.ValidateArguments();
                });
        }

        public void Run()
        {
            if(arguments.OptionsDefined)
            {
                ConsoleUtilities.RunWithArguments(arguments);
            }
            else
            {

                RunWithUserInput();
            }
        }

        private void RunWithUserInput()
        {
            String input = String.Empty;

            while (!(input == "exit"))
            {
                ConsoleUtilities.PrintEmulatorInfo();

                Console.WriteLine(String.Format("(Available Commands: {0})", String.Join(", ", factory.GetCommandNames())));
                Console.Write("> ");
                input = Console.ReadLine();

                factory.ExecuteCommand(input);

                Console.WriteLine();
            }
        }
    }
}
