using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotSet
{
    class MandelbrotSet
    {

        public const double minX = -2.1;
        public const double minY = -1.6;
        public const double maxX = 1;
        public const double maxY = 1.6;

        private float c = 0.0f;
        private int bounds_width, bounds_height;

        public MandelbrotSet(int bounds_width, int bounds_height)
        {
            this.bounds_width = bounds_width;
            this.bounds_height = bounds_height;
        }

        public UInt32[] Calculate(double xmin, double ymin, double xmax, double ymax, int loops)
        {
            UInt32[] set = new UInt32[bounds_width * bounds_height];

            // Creates the Bitmap we draw to
            // From here on out is just converted from the c++ version.
            double x, y, x1, y1, xx = 0.0;

            int looper = 0;
            double intigralX, intigralY = 0.0;
            intigralX = (xmax - xmin) / bounds_width; // Make it fill the whole window
            intigralY = (ymax - ymin) / bounds_height;
            x = xmin;

            for (int s = 0; s < bounds_width; s++)
            {
                y = ymin;
                for (int z = 0; z < bounds_height; z++)
                {
                    //set[x + y * bounds_width] = Color.Red.PackedValue;
                    x1 = 0;
                    y1 = 0;
                    looper = 0;
                    while (looper < loops && Math.Sqrt((x1 * x1) + (y1 * y1)) < 2)
                    {
                        looper++;
                        xx = (x1 * x1) - (y1 * y1) + x;
                        y1 = 2 * x1 * y1 + y;
                        x1 = xx;
                    }

                    // Get the percent of where the looper stopped
                    double perc = (looper % 100) / 100.0;
                    // Get that part of a 255 scale
                    int val = ((int)(perc * 360));
                    set[s + z * bounds_width] = HSVColor.ColorFromHue(val).PackedValue;
                    y += intigralY;
                }
                x += intigralX;
            }

            return set;
        }
    }
}
