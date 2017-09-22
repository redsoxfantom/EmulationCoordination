using EmulationCoordination.Fullscreen.Gui.WindowStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Fullscreen.Gui
{
    public class WindowManager
    {
        private IWindowState activeWindowState;

        public WindowManager(TextRenderer textRenderer)
        {
            activeWindowState = new EmulatorSelectionWindowState(textRenderer);
        }

        public void Update()
        {
            activeWindowState.Update();
        }

        public void Render()
        {
            activeWindowState.Render();
        }

        public void Initialize()
        {
            activeWindowState.Initialize();
        }
    }
}
