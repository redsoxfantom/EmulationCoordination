using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Input.InputMethods
{
    public class KeyboardMethod : IInputMethod
    {
        public event InputReceivedEvent InputReceived;

        public KeyboardMethod(GameWindow window)
        {
            window.KeyUp += Window_KeyUp;
        }

        private void Window_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Left:
                    InputReceived?.Invoke(InputType.LEFT);
                    break;
                case Key.Right:
                    InputReceived?.Invoke(InputType.RIGHT);
                    break;
                case Key.Escape:
                    InputReceived?.Invoke(InputType.EXIT);
                    break;
                case Key.Enter:
                    InputReceived?.Invoke(InputType.SELECT);
                    break;
                case Key.BackSpace:
                    InputReceived?.Invoke(InputType.BACK);
                    break;
                default:
                    break;
            }
        }
    }
}
