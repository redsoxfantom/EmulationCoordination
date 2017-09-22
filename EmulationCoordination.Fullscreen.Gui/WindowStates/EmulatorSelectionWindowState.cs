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
        private List<EmulatorSelection> consoles;
        private EmulatorSelection selectedConsole;
        private int selectedConsoleIndex;
        private bool leftRequested = false, rightRequested = false;

        public EmulatorSelectionWindowState()
        {
            var inputMgr = InputManager.Instance;
            inputMgr.InputReceived += InputMgr_InputReceived;

            var consolesWithRoms = RomManager.Instance.GetAllRoms().Keys.ToList();
            consoles = new List<EmulatorSelection>();
            foreach(var console in consolesWithRoms)
            {
                EmulatorSelection selection = new EmulatorSelection();
                selection.console = console;

                int consoleTexture = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, consoleTexture);
                Bitmap bmp = new Bitmap(console.ConsoleImage);
                BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                bmp.UnlockBits(data);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                selection.textureId = consoleTexture;

                consoles.Add(selection);
            }

            selectedConsole = consoles[0];
            selectedConsoleIndex = 0;
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
            GL.PushMatrix();
            GL.Translate((-3 * selectedConsoleIndex), 0, 0);
            for(int x = 0; x < consoles.Count; x++)
            {
                var console = consoles[x];
                DrawConsole(x*3, 0, console.textureId);
            }
            GL.PopMatrix();
        }

        private void DrawConsole(double x, double z, int textureId)
        {
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.PushMatrix();
            GL.Translate(new Vector3((float)x, 0, (float)z));
            GL.Begin(PrimitiveType.Quads);
                GL.Color4(Color4.White);
                GL.TexCoord2(1, 1); GL.Vertex3(-1.0f, -1.0f, 0.0f);
                GL.TexCoord2(0, 1); GL.Vertex3(1.0f, -1.0f, 0.0f);
                GL.TexCoord2(0, 0); GL.Vertex3(1.0f, 1.0f, 0.0f);
                GL.TexCoord2(1, 0); GL.Vertex3(-1.0f, 1.0f, 0.0f);
            GL.End();
            GL.PopMatrix();
        }

        public void Update()
        {
            if(leftRequested && selectedConsoleIndex < consoles.Count-1)
            {
                selectedConsoleIndex++;
                leftRequested = false;
            }
            if(rightRequested && selectedConsoleIndex > 0)
            {
                selectedConsoleIndex--;
                rightRequested = false;
            }
            selectedConsole = consoles[selectedConsoleIndex];
        }

        private class EmulatorSelection
        {
            public EmulatorConsoles console;
            public int textureId;
        }
    }
}
