using EmulationCoordination.Emulators;
using EmulationCoordination.Input;
using EmulationCoordination.Roms;
using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Fullscreen.Gui.WindowStates
{
    public class RomSelectionWindowState : IWindowState
    {
        private bool leftRequested = false, rightRequested = false, selectRequested = false, backRequested = false;
        private TextRenderer textRenderer;
        private Carousel carousel;
        private EmulatorConsoles emulator;

        public event WindowStateChangedEvent WindowStateChanged;

        public RomSelectionWindowState(TextRenderer textRenderer, EmulatorConsoles emulator)
        {
            this.textRenderer = textRenderer;
            this.emulator = emulator;
            var inputMgr = InputManager.Instance;
            inputMgr.InputReceived += InputMgr_InputReceived;
        }

        private void InputMgr_InputReceived(InputType type)
        {
            switch (type)
            {
                case InputType.LEFT:
                    leftRequested = true;
                    break;
                case InputType.RIGHT:
                    rightRequested = true;
                    break;
                case InputType.SELECT:
                    selectRequested = true;
                    break;
                case InputType.BACK:
                    backRequested = true;
                    break;
            }
        }

        public void Initialize()
        {
            var romMgr = RomManager.Instance;
            var roms = romMgr.GetRoms(emulator);

            List<CarouselItem> carouselItems = new List<CarouselItem>();
            foreach(var rom in roms)
            {
                CarouselItem carouselItem = new CarouselItem();
                carouselItem.ItemImage = rom.BoxArt;
                carouselItem.Tag = rom;
                carouselItem.ItemStrings = new string[2];
                carouselItem.ItemStrings[0] = rom.FriendlyName;
                carouselItem.ItemStrings[1] = String.Format("Time Played: {0}",rom.PrettyPrintPlayTime());
                carouselItems.Add(carouselItem);
            }

            carousel = new Carousel(carouselItems, textRenderer);
        }

        public void Render()
        {
            carousel.Render();
        }

        public void Update()
        {
            if (leftRequested)
            {
                leftRequested = false;
                carousel.ChangeSelectedItem(InputType.LEFT);
            }
            if (rightRequested)
            {
                rightRequested = false;
                carousel.ChangeSelectedItem(InputType.RIGHT);
            }
            if (selectRequested)
            {
                selectRequested = false;
                RomData romData = (RomData)carousel.GetSelectedItem();
                var emuMgr = EmulatorManager.Instance;
            }
            if(backRequested)
            {
                backRequested = false;
                EmulatorSelectionWindowState windowState = new EmulatorSelectionWindowState(textRenderer);
                WindowStateChanged?.Invoke(new WindowStateChangedEventArgs() { NewWindowState = windowState });
            }
        }
    }
}
