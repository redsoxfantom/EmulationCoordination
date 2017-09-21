using EmulationCoordination.Input;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Fullscreen.Gui
{
    public class MainWindow : GameWindow
    {
        private WindowManager winMgr;
        private InputManager inputManager;

        public MainWindow()
            :base(800,600,GraphicsMode.Default,"EmulationCoordination.Fullscreen.GUI",GameWindowFlags.Default,
                 DisplayDevice.Default,4,0,GraphicsContextFlags.ForwardCompatible)
        {
            inputManager = InputManager.Instance;
            inputManager.GameWindow = this;
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            Matrix4 modelview = Matrix4.LookAt(new Vector3(0,0,-40), new Vector3(0,0,0), Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
        }

        protected override void OnLoad(EventArgs e)
        {
            winMgr = new WindowManager();
            GL.ClearColor(Color4.Blue);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            winMgr.Update();
            if(inputManager.ExitRequested)
            {
                Close();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            winMgr.Render();
            
            SwapBuffers();
        }
    }
}
