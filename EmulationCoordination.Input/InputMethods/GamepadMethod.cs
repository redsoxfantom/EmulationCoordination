using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmulationCoordination.Input.InputMethods
{
    public class GamepadMethod : IInputMethod
    {
        public event InputReceivedEvent InputReceived;

        private GamePadState oldGamePadState;

        private GameWindow window;

        public GamepadMethod(GameWindow window)
        {
            oldGamePadState = GamePad.GetState(0);
            this.window = window;
            Task.Factory.StartNew(() => {
                while (true)
                {
                    Thread.Sleep(16);
                    PollGamepadState();
                }
            });
        }

        private void PollGamepadState()
        {
            if (window.Focused)
            {
                var newGamePadState = GamePad.GetState(0);
                if (!oldGamePadState.Equals(newGamePadState))
                {
                    if ((newGamePadState.Buttons.Back != oldGamePadState.Buttons.Back) &&
                        newGamePadState.Buttons.Back.Equals(ButtonState.Released))
                    {
                        InputReceived?.Invoke(InputType.EXIT);
                    }

                    if ((newGamePadState.Buttons.B != oldGamePadState.Buttons.B) &&
                        newGamePadState.Buttons.B.Equals(ButtonState.Released))
                    {
                        InputReceived?.Invoke(InputType.BACK);
                    }

                    if ((newGamePadState.Buttons.A != oldGamePadState.Buttons.A) &&
                        newGamePadState.Buttons.A.Equals(ButtonState.Released))
                    {
                        InputReceived?.Invoke(InputType.SELECT);
                    }

                    if ((newGamePadState.DPad.Left != oldGamePadState.DPad.Left) &&
                        newGamePadState.DPad.Left.Equals(ButtonState.Released))
                    {
                        InputReceived?.Invoke(InputType.LEFT);
                    }

                    if ((newGamePadState.DPad.Right != oldGamePadState.DPad.Right) &&
                        newGamePadState.DPad.Right.Equals(ButtonState.Released))
                    {
                        InputReceived?.Invoke(InputType.RIGHT);
                    }
                }

                oldGamePadState = newGamePadState;
            }
        }
    }
}
