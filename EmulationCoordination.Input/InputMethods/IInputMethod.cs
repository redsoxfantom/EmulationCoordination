using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Input.InputMethods
{
    public interface IInputMethod
    {
        event InputReceivedEvent InputReceived;
    }
}
