using System;
using System.Collections.Generic;

namespace CoordConvert
{
    public class FourPara
    {
        public double deltaX;
        public double deltaY;
        public double scale;
        public double Ax;

        /// <summary>
        /// Initializes a new Point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public FourPara()
        {
            deltaX = 0;
            deltaY = 0;
            scale = 0;
            Ax = 0;
        }

        /// <summary>
        /// Initializes a new Point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public FourPara(double dx, double dy, double Ex, double m)
        {
            deltaX = dx;
            deltaY = dy;
            scale = m;
            Ax = Ex;
        }
    }
}
