using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotSet
{
    class MandelbrotSetRenderer
    {

        private Texture2D canvas;
        private Rectangle tracedSize;
        private UInt32[] pixels;
        private MandelbrotSet mandelbrotSet;

        public void Initialize(GraphicsDeviceManager graphics)
        {
            tracedSize = graphics.GraphicsDevice.PresentationParameters.Bounds;
            canvas = new Texture2D(graphics.GraphicsDevice, tracedSize.Width, tracedSize.Height, false, SurfaceFormat.Color);
            pixels = new UInt32[tracedSize.Width * tracedSize.Height];

            mandelbrotSet = new MandelbrotSet();
            mandelbrotSet.Calculate();
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            canvas.SetData<UInt32>(pixels, 0, tracedSize.Width * tracedSize.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(canvas, new Rectangle(0, 0, tracedSize.Width, tracedSize.Height), Color.White);
            spriteBatch.End();

        }

    }
}
