using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Commands
{
    public class ExitCommand : ICommand
    {
        public string CommandName => "exit";

        public void Execute()
        {
            Environment.ExitCode = 0;
        }
    }
}
