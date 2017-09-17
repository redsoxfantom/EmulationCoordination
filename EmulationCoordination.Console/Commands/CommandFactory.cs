using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Commands
{
    public class CommandFactory
    {
        private List<ICommand> commands;

        public CommandFactory()
        {
            commands = new List<ICommand>()
            {
                new DeleteCommand(),
                new PlayCommand(),
                new DownloadCommand(),
                new ExitCommand()
            };
        }

        public IEnumerable<string> GetCommandNames()
        {
            foreach(var command in commands)
            {
                yield return command.CommandName;
            }
        }

        public void ExecuteCommand(string command)
        {
            var cmd = commands.Where(f => f.CommandName == command);
            if(cmd.Count() == 1)
            {
                cmd.First().Execute();
            }
        }
    }
}
