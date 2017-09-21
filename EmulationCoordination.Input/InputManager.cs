using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmulationCoordination.Input
{
    public class InputManager
    {
        public GameWindow GameWindow { set; private get;}

        public bool ExitRequested { private set; get; }
        public bool LeftRequested { private set; get; }
        public bool RightRequested { private set; get; }

        private static InputManager mInstance = null;
        
        private InputManager()
        {
            GameWindow = null;
            Task.Factory.StartNew(() => {
                while(true)
                {
                    UpdateInputState();
                    Thread.Sleep(16);
                }
            });
        }

        public static InputManager Instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new InputManager();
                }
                return mInstance;
            }
        }

        private void UpdateInputState()
        {
            if(GameWindow == null || !GameWindow.Focused)
            {
                return;
            }

            var keyboard = Keyboard.GetState();
            if(keyboard[Key.Escape])
            {
                ExitRequested = true;
            }
            else
            {
                ExitRequested = false;
            }

            if(keyboard[Key.Left])
            {
                LeftRequested = true;
            }
            else
            {
                LeftRequested = false;
            }

            if (keyboard[Key.Right])
            {
                RightRequested = true;
            }
            else
            {
                RightRequested = false;
            }
        }
    }
}
