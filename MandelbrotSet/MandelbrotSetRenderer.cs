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

        private Texture2D canvas;
        private Rectangle tracedSize;
        private UInt32[] pixels;
        private MandelbrotSet mandelbrotSet;
        
        private double minX = MandelbrotSet.minX;
        private double minY = MandelbrotSet.minY;
        private double maxX = MandelbrotSet.maxX;
        private double maxY = MandelbrotSet.maxY;

        private bool ready = false;

        private int previousScrollValue;

        public void Initialize(GraphicsDeviceManager graphics)
        {
            tracedSize = graphics.GraphicsDevice.PresentationParameters.Bounds;
            canvas = new Texture2D(graphics.GraphicsDevice, tracedSize.Width, tracedSize.Height, false, SurfaceFormat.Color);
            pixels = new UInt32[tracedSize.Width * tracedSize.Height];

            mandelbrotSet = new MandelbrotSet(tracedSize.Width, tracedSize.Height);
        }

        public void Update(GameTime gameTime)
        {
            Trace.WriteLine("FPS: " + (1f / (gameTime.ElapsedGameTime.Milliseconds / 1000f)));

            MouseState state = Mouse.GetState();
            if (state.ScrollWheelValue > previousScrollValue)
            {
                ZoomIn(state);
            }
            else if (state.ScrollWheelValue < previousScrollValue)
            { 
                ZoomOut(state);
            }
            previousScrollValue = state.ScrollWheelValue;

            if (!ready)
            {
                pixels = mandelbrotSet.Calculate(minX, minY, maxX, maxY, 200);
                ready = true;
            }
        }

        private void ZoomIn(MouseState state)
        {
            int mouseX = state.X;
            int mouseY = state.Y;
            double width = (maxX - minX) / 2;
            double height = (maxY - minY) / 2;
            double centerX = QMath.Map(mouseX, 0, tracedSize.Width, minX, maxX);
            double centerY = QMath.Map(mouseY, 0, tracedSize.Height, minY, maxY);
            minX = centerX - width / 2;
            maxX = centerX + width / 2;
            minY = centerY - height / 2;
            maxY = centerY + height / 2;
            ready = false;
        }

        private void ZoomOut(MouseState state)
        {
            int mouseX = state.X;
            int mouseY = state.Y;
            double width = (maxX - minX) / 2;
            double height = (maxY - minY) / 2;
            double centerX = QMath.Map(mouseX, 0, tracedSize.Width, minX, maxX);
            double centerY = QMath.Map(mouseY, 0, tracedSize.Height, minY, maxY);
            minX = centerX - width * 2;
            maxX = centerX + width * 2;
            minY = centerY - height * 2;
            maxY = centerY + height * 2;
            ready = false;
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
