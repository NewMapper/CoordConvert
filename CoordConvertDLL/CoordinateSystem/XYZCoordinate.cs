using System;
using System.Collections.Generic;
using System.Text;

namespace CoordConvert
{
    public class XYZCoordinate
    {
        private double _X;
        private double _Y;
        private double _Z;

        /// <summary>
        /// Initializes a new Point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public XYZCoordinate()
        {
            _X = 0;
            _Y = 0;
            _Z = 0;
        }

        /// <summary>
        /// Initializes a new Point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public XYZCoordinate(double x, double y, double z)
        {
            _X = x;
            _Y = y;
            _Z = z;
        }

        /// <summary>
        /// Gets or sets the X coordinate of the point
        /// </summary>
        public double X
        {
            get
            {
                return _X;
            }
            set
            {
                _X = value;
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the point
        /// </summary>
        public double Y
        {
            get
            {
                return _Y;
            }
            set
            {
                _Y = value;
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the point
        /// </summary>
        public double Z
        {
            get
            {
                return _Z;
            }
            set
            {
                _Z = value;
            }
        }
    }
}
