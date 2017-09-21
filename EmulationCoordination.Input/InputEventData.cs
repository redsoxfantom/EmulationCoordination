using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Input
{

    public delegate void InputReceivedEvent(InputType type);

    public enum InputType
    {
        LEFT,
        RIGHT,
        EXIT
    }
}
