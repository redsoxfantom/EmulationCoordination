using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Fullscreen.Gui
{
    public class TextRenderingOptions
    {
        public Font Font { get; set; }
        public PointF Location { get; set; }
        public Brush Color { get; set; }
        public TextAlignment Alignment { get; set; }

        public TextRenderingOptions()
        {
            Font = new Font(FontFamily.GenericMonospace, 24);
            Location = PointF.Empty;
            Color = Brushes.White;
            Alignment = new TextAlignment();
        }
    }

    public class TextAlignment
    {
        public AlignmentHorizontal HorizontalAlignment { get; set; }
        public AlignmentVertical VerticalAlignment { get; set; }
        public TextAlignment(AlignmentHorizontal HorizontalAlignment = AlignmentHorizontal.ALIGN_NONE,
                             AlignmentVertical VerticalAlignment = AlignmentVertical.ALIGN_NONE)
        {
            this.HorizontalAlignment = HorizontalAlignment;
            this.VerticalAlignment = VerticalAlignment;
        }
    }

    public enum AlignmentHorizontal
    {
        ALIGN_RIGHT,
        ALIGN_LEFT,
        ALIGN_CENTER,
        ALIGN_NONE
    }

    public enum AlignmentVertical
    {
        ALIGN_TOP,
        ALIGN_BOTTOM,
        ALIGN_CENTER,
        ALIGN_NONE
    }

    public class TextRenderer
    {
        private Bitmap bmp;
        private Graphics gfx;
        private int texture;
        private Rectangle dirty_region;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="width">The width of the backing store in pixels.</param>
        /// <param name="height">The height of the backing store in pixels.</param>
        public TextRenderer(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width");
            if (height <= 0)
                throw new ArgumentOutOfRangeException("height ");
            if (GraphicsContext.CurrentContext == null)
                throw new InvalidOperationException("No GraphicsContext is current on the calling thread.");

            bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            gfx = Graphics.FromImage(bmp);
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0,
                PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
        }

        /// <summary>
        /// Clears the backing store to the specified color.
        /// </summary>
        /// <param name="color">A <see cref="System.Drawing.Color"/>.</param>
        public void Clear(Color color)
        {
            gfx.Clear(color);
            dirty_region = new Rectangle(0, 0, bmp.Width, bmp.Height);
        }

        /// <summary>
        /// Draws the specified string to the backing store.
        /// </summary>
        /// <param name="text">The <see cref="System.String"/> to draw.</param>
        /// <param name="font">The <see cref="System.Drawing.Font"/> that will be used.</param>
        /// <param name="brush">The <see cref="System.Drawing.Brush"/> that will be used.</param>
        /// <param name="point">The location of the text on the backing store, in 2d pixel coordinates.
        /// The origin (0, 0) lies at the top-left corner of the backing store.</param>
        public void DrawString(string text, TextRenderingOptions options = null)
        {
            if (options == null)
            {
                options = new TextRenderingOptions();
            }

            SizeF size = gfx.MeasureString(text, options.Font);
            PointF newLocation = DetermineAlignment(options.Alignment, options.Location, size);
            gfx.DrawString(text, options.Font, options.Color, newLocation);
            dirty_region = Rectangle.Round(RectangleF.Union(dirty_region, new RectangleF(newLocation, size)));
            dirty_region = Rectangle.Intersect(dirty_region, new Rectangle(0, 0, bmp.Width, bmp.Height));
        }

        /// <summary>
        /// Updates the alignment of the text based on the options passed in
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        private PointF DetermineAlignment(TextAlignment alignment, PointF location, SizeF textSize)
        {
            float halfTextWidth = textSize.Width / 2.0f;
            float halfTextHeight = textSize.Height / 2.0f;
            PointF newLocation = new PointF(location.X, location.Y);

            switch (alignment.HorizontalAlignment)
            {
                case AlignmentHorizontal.ALIGN_CENTER:
                    newLocation.X = (bmp.Width / 2.0f) - halfTextWidth;
                    break;
                case AlignmentHorizontal.ALIGN_LEFT:
                    newLocation.X = 0;
                    break;
                case AlignmentHorizontal.ALIGN_RIGHT:
                    newLocation.X = bmp.Width - textSize.Width;
                    break;
            }
            switch (alignment.VerticalAlignment)
            {
                case AlignmentVertical.ALIGN_CENTER:
                    newLocation.Y = (bmp.Height / 2.0f) - halfTextHeight;
                    break;
                case AlignmentVertical.ALIGN_TOP:
                    newLocation.Y = 0;
                    break;
                case AlignmentVertical.ALIGN_BOTTOM:
                    newLocation.Y = bmp.Height - textSize.Height;
                    break;
            }

            return newLocation;
        }

        public void Render()
        {
            GL.PushMatrix();
            
            GL.BindTexture(TextureTarget.Texture2D, Texture);
            GL.Begin(PrimitiveType.Quads);
            
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex3(-2.5f, -2.5f, 0f);
            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex3(2.5f, -2.5f, 0f);
            GL.TexCoord2(0.0f, 0.0f);
            GL.Vertex3(2.5f, 2.5f, 0f);
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex3(-2.5f, 2.5f, 0f);

            GL.End();
            GL.PopMatrix();
        }

        /// <summary>
        /// Gets a <see cref="System.Int32"/> that represents an OpenGL 2d texture handle.
        /// The texture contains a copy of the backing store. Bind this texture to TextureTarget.Texture2d
        /// in order to render the drawn text on screen.
        /// </summary>
        public int Texture
        {
            get
            {
                UploadBitmap();
                return texture;
            }
        }

        // Uploads the dirty regions of the backing store to the OpenGL texture.
        void UploadBitmap()
        {
            if (dirty_region != RectangleF.Empty)
            {
                System.Drawing.Imaging.BitmapData data = bmp.LockBits(dirty_region,
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.BindTexture(TextureTarget.Texture2D, texture);
                GL.TexSubImage2D(TextureTarget.Texture2D, 0,
                    dirty_region.X, dirty_region.Y, dirty_region.Width, dirty_region.Height,
                    PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

                bmp.UnlockBits(data);

                dirty_region = Rectangle.Empty;
            }
        }
    }
}
