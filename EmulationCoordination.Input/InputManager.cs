using EmulationCoordination.Input.InputMethods;
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
        public event InputReceivedEvent InputReceived;
        private GameWindow mGameWindow = null;
        public GameWindow GameWindow {
            set
            {
                mGameWindow = value;
                keyboard = new KeyboardMethod(mGameWindow);
                gamepad = new GamepadMethod();
                keyboard.InputReceived += PeripheralInputReceived;
                gamepad.InputReceived += PeripheralInputReceived;
            }
            private get
            {
                return mGameWindow;
            }
        }

        private IInputMethod keyboard;
        private IInputMethod gamepad;

        private static InputManager mInstance = null;
        
        private InputManager()
        {
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

        private void PeripheralInputReceived(InputType type)
        {
            InputReceived?.Invoke(type);
        }
    }
}
