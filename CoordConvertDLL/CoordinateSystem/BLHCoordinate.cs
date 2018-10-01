using System;
using System.Collections.Generic;
using System.Text;

namespace CoordConvert
{
    public class BLHCoordinate
    {
        private double _b;
        private double _l;
        private double _h;

        /// <summary>
        /// Initializes a new Point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public BLHCoordinate()
        {
            _b = 0;
            _l = 0;
            _h = 0;
        }

        /// <summary>
        /// Initializes a new Point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public BLHCoordinate(double b, double l, double h)
        {
            _b = b;
            _l = l;
            _h = h;
        }

        /// <summary>
        /// Gets or sets the X coordinate of the point
        /// </summary>
        public double B
        {
            get
            {
                return _b;
            }
            set
            {
                _b = value;
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the point
        /// </summary>
        public double L
        {
            get
            {
                return _l;
            }
            set
            {
                _l = value;
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the point
        /// </summary>
        public double H
        {
            get
            {
                return _h;
            }
            set
            {
                _h = value;
            }
        }
    }
}
