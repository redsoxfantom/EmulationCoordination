using System;
namespace EmulationCoordination.Commands
{
    public interface ICommand
    {
        string CommandName { get; }
        void Execute();
    }
}
