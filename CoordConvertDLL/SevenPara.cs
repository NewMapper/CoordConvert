using System;
using System.Collections.Generic;
using System.Text;

namespace CoordConvert
{
    public class SevenPara
    {
        public double deltaX;
        public double deltaY;
        public double deltaZ;
        public double scale;
        public double Ax;
        public double Ay;
        public double Az;

        /// <summary>
        /// Initializes a new Point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public SevenPara()
        {
            deltaX = 0;
            deltaY = 0;
            deltaZ = 0;
            scale = 0;
            Ax = 0;
            Ay = 0;
            Az = 0;
        }

        /// <summary>
        /// Initializes a new Point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public SevenPara(double dx, double dy, double dz, double Ex, double Ey, double Ez, double m)
        {
            deltaX = dx;
            deltaY = dy;
            deltaZ = dz;
            scale = m;
            Ax = Ex;
            Ay = Ey;
            Az = Ez;
        }
    }
}
