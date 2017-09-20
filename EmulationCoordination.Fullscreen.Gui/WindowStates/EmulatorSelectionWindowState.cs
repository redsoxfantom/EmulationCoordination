﻿using EmulationCoordination.Roms;
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
using QuickFont;
using QuickFont.Configuration;

namespace EmulationCoordination.Fullscreen.Gui.WindowStates
{
    public class EmulatorSelectionWindowState : IWindowState
    {
        private List<EmulatorSelection> consoles;
        private EmulatorSelection selectedConsole;
        private QFont font;
        private QFontDrawing fontDrawing;
        
        public EmulatorSelectionWindowState()
        {
            font = new QFont("Fonts/times.ttf", 72, new QFontBuilderConfiguration());
            fontDrawing = new QFontDrawing();

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

            selectedConsole = consoles[consoles.Count / 2];
        }

        public void Render()
        {
            fontDrawing.DrawingPrimitives.Clear();
            fontDrawing.Print(font, "TEST", new Vector3(0.0f, 1.25f, 5.0f), QFontAlignment.Left);

            DrawConsole(0, 1, selectedConsole.textureId);
            DrawConsole(0.5f, 0.5f, selectedConsole.textureId);
            DrawConsole(1, 0, selectedConsole.textureId);
            DrawConsole(-0.5f, 0.5f, selectedConsole.textureId);
            DrawConsole(-1, 0, selectedConsole.textureId);

            //fontDrawing.RefreshBuffers();
            //fontDrawing.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, 800 / (float)600, 1.0f, 64.0f);
            //fontDrawing.Draw();
        }

        private void DrawConsole(float x, float z, int textureId)
        {
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.PushMatrix();
            GL.Translate(x, 0, z);
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

        }

        private class EmulatorSelection
        {
            public EmulatorConsoles console;
            public int textureId;
        }
    }
}
