using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotSet
{
    class QMath
    {

        public static float Map(float value, float lower, float higher, float map_lower, float map_higher)
        {
            return (value - lower) / (higher - lower) * (map_higher - map_lower) + map_lower;
        }

        public static double Map(double value, double lower, double higher, double map_lower, double map_higher)
        {
            return (value - lower) / (higher - lower) * (map_higher - map_lower) + map_lower;
        }
    }
}
