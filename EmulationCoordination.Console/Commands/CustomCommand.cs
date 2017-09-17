using EmulationCoordination.Emulators;
using EmulationCoordination.Emulators.Emulators;
using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
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
            bool pathExists = false;
            String pathToEmulator = String.Empty;

            while (!pathExists)
            {
                Console.WriteLine("Enter path to custom emulator:");
                Console.Write("> ");
                pathToEmulator = Console.ReadLine();
                if(!File.Exists(pathToEmulator))
                {
                    Console.WriteLine(String.Format("\"{0}\" does not exist", pathToEmulator));
                }
                else
                {
                    pathExists = true;
                }
            }

            Console.WriteLine("Enter arguments for custom emulator:");
            Console.Write("> ");
            String emulatorArguments = Console.ReadLine();
            
            Console.WriteLine("Enter custom emulator version [0.0.1]:");
            Console.Write("> ");
            String emulatorVersion = Console.ReadLine();
            if(String.IsNullOrEmpty(emulatorVersion))
            {
                emulatorVersion = "0.0.1";
            }

            String defaultEmulatorName = Path.GetFileName(pathToEmulator);
            Console.WriteLine(String.Format("Enter custom emulator name [{0}]:",defaultEmulatorName));
            Console.Write("> ");
            String emulatorName = Console.ReadLine();
            if (String.IsNullOrEmpty(emulatorName))
            {
                emulatorName = defaultEmulatorName;
            }

            bool inputAccepted = false;
            List<EmulatorConsoles> emulatorConsoles = new List<EmulatorConsoles>();
            while (!inputAccepted)
            {
                Console.WriteLine("Enter console types supported by this emulator (input is a list of numbers supported by spaces)");
                for (int i = 1; i < EmulatorConsoles.Values.Count() + 1; i++)
                {
                    Console.WriteLine(String.Format("{0}) {1}", i, EmulatorConsoles.Values.ToArray()[i - 1]));
                }
                Console.Write("> ");
                string emulatorConsoleInput = Console.ReadLine();

                inputAccepted = tryParseEmulatorConsoles(emulatorConsoleInput, out emulatorConsoles);
            }

            CustomEmulator emu = new CustomEmulator(pathToEmulator, emulatorArguments, emulatorVersion, emulatorName, emulatorConsoles);
            emuMgr.AddCustomEmulator(emu);
        }

        private bool tryParseEmulatorConsoles(string emulatorConsoleInput, out List<EmulatorConsoles> emulatorConsoles)
        {
            var consoleNumbers = emulatorConsoleInput.Split(' ');
            emulatorConsoles = new List<EmulatorConsoles>();
            foreach(var consoleNumber in consoleNumbers)
            {
                int num;
                if(int.TryParse(consoleNumber, out num))
                {
                    if(num > EmulatorConsoles.Values.ToList().Count + 1 || num < 1)
                    {
                        Console.WriteLine(String.Format("Input {0} was not a valie console number",num));
                        return false;
                    }
                    else
                    {
                        emulatorConsoles.Add(EmulatorConsoles.Values.ToArray()[num - 1]);
                    }
                }
                else
                {
                    Console.WriteLine(String.Format("Input {0} is not a number", consoleNumber));
                    return false;
                }
            }

            emulatorConsoles = emulatorConsoles.Distinct().ToList();
            return true;
        }
    }
}
