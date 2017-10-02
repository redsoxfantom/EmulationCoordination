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

        void Initialize();

        event WindowStateChangedEvent WindowStateChanged;
    }

    public delegate void WindowStateChangedEvent(WindowStateChangedEventArgs args);

    public class WindowStateChangedEventArgs
    {
        public IWindowState NewWindowState { get; set; }
    }
}
