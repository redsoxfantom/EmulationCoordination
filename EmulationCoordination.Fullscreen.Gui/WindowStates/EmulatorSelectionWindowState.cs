using EmulationCoordination.Roms;
using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Drawing.Imaging;
using System.Drawing;
using EmulationCoordination.Input;

namespace EmulationCoordination.Fullscreen.Gui.WindowStates
{
    public class EmulatorSelectionWindowState : IWindowState
    {
        private bool leftRequested = false, rightRequested = false;
        private Carousel carousel;
        private TextRenderer textRenderer;

        public EmulatorSelectionWindowState(TextRenderer textRenderer)
        {
            var inputMgr = InputManager.Instance;
            inputMgr.InputReceived += InputMgr_InputReceived;
            this.textRenderer = textRenderer;
        }

        private void InputMgr_InputReceived(InputType type)
        {
            switch(type)
            {
                case InputType.LEFT:
                    leftRequested = true;
                    break;
                case InputType.RIGHT:
                    rightRequested = true;
                    break;
            }
        }

        public void Render()
        {
            carousel.Render();
        }

        public void Update()
        {
            if(leftRequested)
            {
                leftRequested = false;
                carousel.ChangeSelectedItem(InputType.LEFT);
            }
            if(rightRequested)
            {
                rightRequested = false;
                carousel.ChangeSelectedItem(InputType.RIGHT);
            }
        }

        public void Initialize()
        {
            var consolesWithRoms = RomManager.Instance.GetAllRoms();
            List<CarouselItem> carouselItems = new List<CarouselItem>();
            foreach (var console in consolesWithRoms.Keys)
            {
                CarouselItem consoleItem = new CarouselItem();
                int numRoms = consolesWithRoms[console].Count;
                consoleItem.ItemImage = console.ConsoleImage;
                consoleItem.Tag = console;
                consoleItem.ItemStrings = new string[2];
                consoleItem.ItemStrings[0] = console.FriendlyName;
                string numRomsStrings;
                if (numRoms > 1)
                {
                    numRomsStrings = String.Format("{0} Games", numRoms);
                }
                else
                {
                    numRomsStrings = String.Format("1 Game");
                }
                consoleItem.ItemStrings[1] = numRomsStrings;
                carouselItems.Add(consoleItem);
            }

            carousel = new Carousel(carouselItems, textRenderer);
        }
    }
}
