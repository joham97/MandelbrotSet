using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotSet
{
    class MandelbrotSetRenderer
    {

        protected Texture2D canvas;
        protected Rectangle tracedSize;
        protected UInt32[] pixels;
        protected MandelbrotSet mandelbrotSet;

        protected double minX = MandelbrotSet.minX;
        protected double minY = MandelbrotSet.minY;
        protected double maxX = MandelbrotSet.maxX;
        protected double maxY = MandelbrotSet.maxY;

        protected bool ready = false;

        protected int previousScrollValue;

        public virtual void Initialize(GraphicsDeviceManager graphics)
        {
            tracedSize = graphics.GraphicsDevice.PresentationParameters.Bounds;
            canvas = new Texture2D(graphics.GraphicsDevice, tracedSize.Width, tracedSize.Height, false, SurfaceFormat.Color);

            mandelbrotSet = new MandelbrotSet(tracedSize.Width, tracedSize.Height);

            pixels = mandelbrotSet.Calculate(minX, minY, maxX, maxY, 300);
        }

        public virtual void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            if (state.ScrollWheelValue > previousScrollValue)
            {
                Zoom(state.X, state.Y, 0.5);
                pixels = mandelbrotSet.Calculate(minX, minY, maxX, maxY, 300);
            }
            else if (state.ScrollWheelValue < previousScrollValue)
            {
                Zoom(state.X, state.Y, 2);
                pixels = mandelbrotSet.Calculate(minX, minY, maxX, maxY, 300);
            }
            previousScrollValue = state.ScrollWheelValue;
        }

        protected void Zoom(int x, int y, double factor)
        {
            double centerX = QMath.Map(x, 0, tracedSize.Width, minX, maxX);
            double centerY = QMath.Map(y, 0, tracedSize.Height, minY, maxY);
            Zoom(centerX, centerY, factor);
            double offsetX = centerX - QMath.Map(x, 0, tracedSize.Width, minX, maxX);
            double offsetY = centerY - QMath.Map(y, 0, tracedSize.Height, minY, maxY);
            minX += offsetX;
            maxX += offsetX;
            minY += offsetY;
            maxY += offsetY;

        }
        protected void Zoom(double centerX, double centerY, double factor)
        {
            double width = (maxX - minX) / 2;
            double height = (maxY - minY) / 2;
            minX = centerX - width * factor;
            maxX = centerX + width * factor;
            minY = centerY - height * factor;
            maxY = centerY + height * factor;
            ready = false;
            Trace.WriteLine(centerX + " " + centerY);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            canvas.SetData<UInt32>(pixels, 0, tracedSize.Width * tracedSize.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(canvas, new Rectangle(0, 0, tracedSize.Width, tracedSize.Height), Color.White);
            spriteBatch.End();
        }

    }
}
