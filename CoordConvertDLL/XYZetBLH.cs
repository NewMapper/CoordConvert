using System;
using System.Collections.Generic;
using System.Text;

namespace CoordConvert
{
    public class XYZetBLH
    {
        private const double Pi = 3.1415926;
        public static XYZCoordinate BLHToXYZ(BLHCoordinate BLH, Datum datum)
        {
            double a = datum.r_major;
            double b = datum.r_minor;
            double b1 = BLH.B * Pi / 180;
            double l1 = BLH.L * Pi / 180;
            double h1 = BLH.H;
            double eq = datum.E2;
            double N = a / Math.Sqrt(1 - eq * Math.Sin(b1) * Math.Sin(b1));
            double Xq = (N + h1) * Math.Cos(b1) * Math.Cos(l1);
            double Yq = (N + h1) * Math.Cos(b1) * Math.Sin(l1);
            double Zq = ((1 - eq) * N + h1) * Math.Sin(b1);
            return new XYZCoordinate(Xq, Yq, Zq);
        }

        public static BLHCoordinate XYZToBLH(XYZCoordinate XYZ, Datum datum)
        {
            double f, f1, f2;
            double p,zw, nnq;
            double b, l, h;

            double a = datum.r_major;
            double eq = datum.E2;
            f = Pi * 50 / 180;
            double x, y, z;
            x = XYZ.X;
            y = XYZ.Y;
            z = XYZ.Z;
            p = z / Math.Sqrt(x * x + y * y);
            do
            {
                zw = a / Math.Sqrt(1 - eq * Math.Sin(f) * Math.Sin(f));
                nnq = 1 - eq * zw / (Math.Sqrt(x * x + y * y) / Math.Cos(f));
                f1 = Math.Atan(p / nnq);
                f2 = f;
                f = f1;
            } while (!(Math.Abs(f2 - f1) < 10E-10));
            b = f * 180 / Pi;
            l = Math.Atan(y / x) * 180 / Pi;
            if (l < 0)
                l += 180;
            h = Math.Sqrt(x * x + y * y) / Math.Cos(f1) - a / Math.Sqrt(1 - eq * Math.Sin(f1) * Math.Sin(f1));
            return new BLHCoordinate(b, l, h);
        }

    }
}
