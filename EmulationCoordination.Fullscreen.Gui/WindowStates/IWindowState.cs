using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Fullscreen.Gui.WindowStates
{
    public interface IWindowState
    {
        void Render();

        void Update();
    }
}
