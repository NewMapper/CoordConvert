using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoordConvertApp
{
    class CoordConvert
    {
        public static double GetX_dc(double dx, double dy)
        {
            double A1 = 6103729.01171875;
            double B1 = -158449219.125;
            double C1 = -709804016;
            double D1 = 18920915837.022213;
            double fx;

            fx = A1 * dx * dy + B1 * dx + C1 * dy + D1;

            return fx / 1000;
        }

        public static double GetY_dc(double dx, double dy)
        {
            double A2 = 7662548.3125;
            double B2 = -305916495;
            double C2 = -780249200;
            double D2 = 31456748837.828285;
            double fy;

            fy = A2 * dx * dy + B2 * dx + C2 * dy + D2;

            return fy / 1000;
        }
    }
}
