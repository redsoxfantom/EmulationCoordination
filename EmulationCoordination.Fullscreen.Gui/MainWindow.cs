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
        private bool exitRequested = false;
        private TextRenderer textRenderer;

        public MainWindow()
            :base(800,600,GraphicsMode.Default,"EmulationCoordination.Fullscreen.GUI",GameWindowFlags.Fullscreen, DisplayDevice.Default,4,0,GraphicsContextFlags.ForwardCompatible)
        {
            inputManager = InputManager.Instance;
            inputManager.GameWindow = this;
            inputManager.InputReceived += InputManager_InputReceived;

            textRenderer = new TextRenderer(800, 600);
        }

        private void InputManager_InputReceived(InputType type)
        {
            if(type == InputType.EXIT)
            {
                exitRequested = true;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-2.5, 2.5, -2.5, 2.5, 0.1, 100.0);

            Matrix4 modelview = Matrix4.LookAt(new Vector3(0,0,-9), new Vector3(0,0,0), Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.LoadMatrix(ref modelview);
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color4.Red);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);

            textRenderer.Clear(System.Drawing.Color.Blue);
            winMgr = new WindowManager(textRenderer);
            winMgr.Initialize();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            winMgr.Update();
            if(exitRequested)
            {
                Close();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            winMgr.Render();
            textRenderer.Render();
            
            SwapBuffers();
        }
    }
}
