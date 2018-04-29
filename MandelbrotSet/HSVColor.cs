using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotSet
{
    class HSVColor
    {
        static public Color ColorFromHue(int hue)
        {
            float part = hue / 60;

            int inc = (int) ((hue % 60) / 60f * 255f);
            int dec = (int)(60 - (hue % 60) / 60f * 255f); ;

            if (part == 0)
                return new Color(255, inc, 0);
            else if (part == 1)
                return new Color(dec, 255, 0);
            else if (part == 2)
                return new Color(0, 255, inc);
            else if (part == 3)
                return new Color(0, dec, 255);
            else if (part == 4)
                return new Color(inc, 0, 255);
            else if (part == 5)
                return new Color(255, 0, dec);


            return new Color();
        }
    }
}
