using System;
using System.Collections.Generic;
using System.Text;

namespace CoordConvert
{
    public class Datum
    {
        private double _a;
        private double _b;
        private double _e2;

        /// <summary>
        /// Initializes a new Point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Datum()
        {
            _a = 0;
            _b = 0;
            _e2 = 0;
        }

        /// <summary>
        /// Initializes a new Point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Datum(double a, double b, double e2)
        {
            _a = a;
            _b = b;
            _e2 = e2;
        }

        /// <summary>
        /// Gets or sets the X coordinate of the point
        /// </summary>
        public double r_major
        {
            get
            {
                return _a;
            }
            set
            {
                _a = value;
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the point
        /// </summary>
        public double r_minor
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
        public double E2
        {
            get
            {
                return _e2;
            }
            set
            {
                _e2 = value;
            }
        }
    }
}
