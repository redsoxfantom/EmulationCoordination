using EmulationCoordination.Emulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Commands
{
    public class DeleteCommand : ICommand
    {
        EmulatorManager mgr;

        public DeleteCommand()
        {
            mgr = EmulatorManager.Instance;
        }

        public string CommandName => "delete";

        public void Execute()
        {
            var installedEmulators = mgr.GetAvailableEmulators().Where(f => f.Installed && f.EmulatorType == EmulatorType.BUILTIN 
                || f.EmulatorType == EmulatorType.CUSTOM).ToList();
            var selectedEmulator = ConsoleUtilities.SelectEmulator(installedEmulators);

            if (selectedEmulator != null)
            {
                Console.WriteLine(String.Format("Deleting {0}", selectedEmulator.EmulatorName));
                mgr.DeleteEmulator(selectedEmulator);
                Console.WriteLine("Complete");
            }
        }
    }
}
