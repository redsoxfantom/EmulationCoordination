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
        private bool mExitRequested = false;

        public GameWindow GameWindow
        {
            set;
            private get;
        }

        public bool ExitRequested
        {
            private set
            {
                mExitRequested = value;
            }
            get
            {
                if(mExitRequested)
                {
                    mExitRequested = false;
                    return true;
                }
                return false;
            }
        }

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
                mExitRequested = true;
            }
        }
    }
}
