using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using EmulationCoordination.Input;

namespace EmulationCoordination.Fullscreen.Gui.WindowStates
{
    public class Carousel : IDisposable
    {
        private List<Tuple<CarouselItem, int>> ItemTextureMapping;
        private TextRenderer textRenderer;
        private int selectedItemIndex;
        private TextRenderingOptions opt = new TextRenderingOptions();

        public Carousel(List<CarouselItem> Items, TextRenderer textRenderer)
        {
            ItemTextureMapping = new List<Tuple<CarouselItem, int>>();
            foreach(var item in Items)
            {
                int itemTextureId = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, itemTextureId);
                Bitmap bmp = new Bitmap(item.ItemImage);
                BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                bmp.UnlockBits(data);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                ItemTextureMapping.Add(new Tuple<CarouselItem, int>(item, itemTextureId));
            }

            selectedItemIndex = 0;
            this.textRenderer = textRenderer;
            opt.Alignment = new TextAlignment(AlignmentHorizontal.ALIGN_CENTER);
            Update();
        }

        public void Render()
        {
            GL.PushMatrix();
            GL.Translate((-3 * selectedItemIndex), 0, 0);
            for (int x = 0; x < ItemTextureMapping.Count; x++)
            {
                var item = ItemTextureMapping[x].Item2;
                DrawItemImage(x * 3, 0, item);
            }
            GL.PopMatrix();
        }

        public void ChangeSelectedItem(InputType inputType)
        {
            bool selectedItemUpdated = false;
            if (inputType == InputType.LEFT && selectedItemIndex < ItemTextureMapping.Count - 1)
            {
                selectedItemIndex++;
                selectedItemUpdated = true;
            }
            if (inputType == InputType.RIGHT && selectedItemIndex > 0)
            {
                selectedItemIndex--;
                selectedItemUpdated = true;
            }
            if (selectedItemUpdated)
            {
                Update();
            }
        }

        public object GetSelectedItem()
        {
            var selectedItem = ItemTextureMapping[selectedItemIndex].Item1.Tag;
            return selectedItem;
        }

        private void Update()
        {
            textRenderer.Clear(Color.Blue);
            var itemStrings = ItemTextureMapping[selectedItemIndex].Item1.ItemStrings;
            for (int idx = 0; idx < itemStrings.Length; idx++)
            {
                opt.Location = new PointF(0, 50 * idx);
                textRenderer.DrawString(itemStrings[idx], opt);
            }
        }

        private void DrawItemImage(double x, double z, int textureId)
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                foreach(var item in ItemTextureMapping)
                {
                    //GL.DeleteTexture(item.Item2);
                }

                if (disposing)
                {
                    ItemTextureMapping = null;
                    opt = null;
                }
                
                disposedValue = true;
            }
        }
        
         ~Carousel()
        {
           Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class CarouselItem
    {
        public Image ItemImage { get; set; }
        public object Tag { get; set; }
        public string[] ItemStrings { get; set; }
    }
}
