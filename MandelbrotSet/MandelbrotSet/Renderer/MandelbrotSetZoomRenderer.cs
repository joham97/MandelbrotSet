using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MandelbrotSet
{
    class MandelbrotSetZoomRenderer : MandelbrotSetRenderer
    {
        private readonly int ITERATIONS = 300;
        private readonly double FACTOR = 0.9;

        private double NICE_CENTER_X = -0.749622012968225;
        private double NICE_CENTER_Y = -0.0350246059559247;

        private UInt32[][] allPixels;

        private int pixelCount = -1;

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            allPixels = new UInt32[ITERATIONS][];
            Zoom(NICE_CENTER_X, NICE_CENTER_Y, 2);

            base.Initialize(graphics);
        }

        public override void Update(GameTime gameTime)
        {
            pixelCount++;
            if (pixelCount >= allPixels.Length)
                pixelCount = 0;
            if (allPixels[pixelCount] == null)
            {
                for(int i = 0; i < allPixels.Length; i++)
                {
                    allPixels[i] = mandelbrotSet.Calculate(minX, minY, maxX, maxY, 300);
                    Zoom(NICE_CENTER_X, NICE_CENTER_Y, FACTOR);
                    Trace.WriteLine(i + "/" + allPixels.Length);
                }
            }
            this.pixels = allPixels[pixelCount];
        }
    }
}
