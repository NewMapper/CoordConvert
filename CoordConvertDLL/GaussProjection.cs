using System;
using System.Collections.Generic;
using System.Text;

namespace CoordConvert
{
    //高斯投影正、反算 

    //3度带宽 

    //NN卯酉圈曲率半径，测量学里面用N表示

    //M为子午线弧长，测量学里用大X表示

    //fai为底点纬度，由子午弧长反算公式得到，测量学里用Bf表示

    //R为底点所对的曲率半径，测量学里用Nf表示
    public class GaussProjection
    {
        //高斯投影由经纬度(Unit:DD)计算大地平面坐标(含带号，Unit:Metres) 
        public static XYZCoordinate GaussProjCal(BLHCoordinate BLH, Datum datum,double lon)
        {
            int ProjNo, ZoneWide; ////带宽 
            double longitude0, X0, xval, yval;
            double a, e2, ee, NN, T, C, A, M, b, l, h;
            b = BLH.B;
            l = BLH.L;
            h = BLH.H;
            ZoneWide = 3; //3度带宽 
            a = datum.r_major;
            ProjNo = (int)((l - 1.5) / ZoneWide + 1);
            longitude0 = lon;
            if (Math.Abs(lon - 0) < 0.000001)
            {
                longitude0 = ProjNo * ZoneWide; //中央经线
            }
            longitude0 = longitude0 * Math.PI / 180;
            l = l * Math.PI / 180; //经度转换为弧度
            b = b * Math.PI / 180; //纬度转换为弧度
            e2 = datum.E2;
            ee = e2 * (1.0 - e2);
            NN = a / Math.Sqrt(1.0 - e2 * Math.Sin(b) * Math.Sin(b));
            T = Math.Tan(b) * Math.Tan(b);
            C = ee * Math.Cos(b) * Math.Cos(b);
            A = (l - longitude0) * Math.Cos(b);

            M = a * ((1 - e2 / 4 - 3 * e2 * e2 / 64 - 5 * e2 * e2 * e2 / 256) * b - (3 * e2 / 8 + 3 * e2 * e2 / 32 + 45 * e2 * e2 * e2 / 1024) * Math.Sin(2 * b) + (15 * e2 * e2 / 256 + 45 * e2 * e2 * e2 / 1024) * Math.Sin(4 * b) - (35 * e2 * e2 * e2 / 3072) * Math.Sin(6 * b));
            xval = NN * (A + (1 - T + C) * A * A * A / 6 + (5 - 18 * T + T * T + 72 * C - 58 * ee) * A * A * A * A * A / 120);
            yval = M + NN * Math.Tan(b) * (A * A / 2 + (5 - T + 9 * C + 4 * C * C) * A * A * A * A / 24 + (61 - 58 * T + T * T + 600 * C - 330 * ee) * A * A * A * A * A * A / 720);
            //X0 = 1000000L * ProjNo + 500000L;
            X0 =  500000L;
            xval = xval + X0;
            return new XYZCoordinate(xval, yval, h);
        }

        //高斯投影由大地平面坐标(Unit:Metres)反算经纬度(Unit:DD)
        public static BLHCoordinate GaussProjInvCal(XYZCoordinate XYZ, Datum datum, double lon)
        {
            int ProjNo, ZoneWide; ////带宽 
            double l, b, longitude0, X0, xval, yval;
            double e1, e2, f, a, ee, NN, T, C, M, D, R, u, fai;
            a = datum.r_major; //54年北京坐标系参数 
            ZoneWide = 3; //3度带宽 
            ProjNo = (int)(XYZ.X / 1000000L); //查找带号
            longitude0 = lon;
            if (Math.Abs(lon - 0) < 0.000001)
            {
                longitude0 = ProjNo * ZoneWide; //中央经线
            }
            longitude0 = longitude0 * Math.PI / 180; //中央经线
            X0 = ProjNo * 1000000L + 500000L;
            xval = XYZ.X - X0; //带内大地坐标
            yval = XYZ.Y;
            e2 = datum.E2;
            e1 = (1.0 - Math.Sqrt(1 - e2)) / (1.0 + Math.Sqrt(1 - e2));
            ee = e2 / (1 - e2);
            M = yval;
            u = M / (a * (1 - e2 / 4 - 3 * e2 * e2 / 64 - 5 * e2 * e2 * e2 / 256));
            fai = u + (3 * e1 / 2 - 27 * e1 * e1 * e1 / 32) * Math.Sin(2 * u) + (21 * e1 * e1 / 16 - 55 * e1 * e1 * e1 * e1 / 32) * Math.Sin(4 * u) + (151 * e1 * e1 * e1 / 96) * Math.Sin(6 * u) + (1097 * e1 * e1 * e1 * e1 / 512) * Math.Sin(8 * u);
            C = ee * Math.Cos(fai) * Math.Cos(fai);
            T = Math.Tan(fai) * Math.Tan(fai);
            NN = a / Math.Sqrt(1.0 - e2 * Math.Sin(fai) * Math.Sin(fai));

            R = a * (1 - e2) / Math.Sqrt((1 - e2 * Math.Sin(fai) * Math.Sin(fai)) * (1 - e2 * Math.Sin(fai) * Math.Sin(fai)) * (1 - e2 * Math.Sin(fai) * Math.Sin(fai)));
            D = xval / NN;
            //计算经度(Longitude) 纬度(Latitude)
            l = longitude0 + (D - (1 + 2 * T + C) * D * D * D / 6 + (5 - 2 * C + 28 * T - 3 * C * C + 8 * ee + 24 * T * T) * D
            * D * D * D * D / 120) / Math.Cos(fai);
            b = fai - (NN * Math.Tan(fai) / R) * (D * D / 2 - (5 + 3 * T + 10 * C - 4 * C * C - 9 * ee) * D * D * D * D / 24
            + (61 + 90 * T + 298 * C + 45 * T * T - 256 * ee - 3 * C * C) * D * D * D * D * D * D / 720);
            //转换为度 DD
            l = l * 180 / Math.PI;
            b = b * 180 / Math.PI;
            return new BLHCoordinate(b, l, XYZ.Z);
        }
    }
}
