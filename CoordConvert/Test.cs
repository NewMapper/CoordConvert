using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CoordConvert;

namespace CoordConvertApp
{
    class Test
    {
        public static void ConvertTest()
        {
            List<XYZCoordinate> pSource = new List<XYZCoordinate>();
            List<XYZCoordinate> pTo = new List<XYZCoordinate>();

            //FourPara forPara = new FourPara(-439902.865, -3266656.406, 0.0013362875487730103, 0.99999205271109981);

            StreamReader sr = new StreamReader(@"E:\Projects\张家口\张家口控制点-宣化.txt");
            //StreamWriter sw = new StreamWriter(@"E:\Projects\杭州项目\点云数据\data-20120517-123249_1_left.xyzt");
            string line;
            double b, l, h, x1 = 0, y1 = 0, x2 = 0, y2 = 0, weekSec;
            int i = 1;
            while ((line = sr.ReadLine()) != null)
            {
                string[] strs = line.Split(' ');
                //b = double.Parse(strs[0]) + double.Parse(strs[1]) / 60 + double.Parse(strs[2]) / 3600;
                //l = double.Parse(strs[3]) + double.Parse(strs[4]) / 60 + double.Parse(strs[5]) / 3600;
                x1 = double.Parse(strs[0]);
                y1 = double.Parse(strs[1]);

                x2 = double.Parse(strs[2]);
                y2 = double.Parse(strs[3]);

                //l = double.Parse(strs[4]) + double.Parse(strs[5]) / 60 + double.Parse(strs[6]) / 3600;
                //b = double.Parse(strs[7]) + double.Parse(strs[8]) / 60 + double.Parse(strs[9]) / 3600;

                //int intb = (int)b;
                //double millib = b - intb;
                //int minb = (int)(millib * 100);
                //double secb = (millib * 100 - minb) * 100;
                //b = intb + minb / 60.0 + secb / 3600;

                //int intl = (int)l;
                //double millil = l - intl;
                //int minl = (int)(millil * 100);
                //double secl = (millil * 100 - minl) * 100;
                //l = intl + minl / 60.0 + secl / 3600;

                //BLHCoordinate wgsblh = GaussProjection.GaussProjInvCal(new XYZCoordinate(x, y, h), CoordConsts.WGS84Datum, 105);

                //XYZCoordinate wgsxyh = GaussProjection.GaussProjCal(new BLHCoordinate(b,l,0), CoordConsts.WGS84Datum, 108);
                pSource.Add(new XYZCoordinate(y1, x1, 0));

                pTo.Add(new XYZCoordinate(y2, x2, 0));
            }
            sr.Close();
            //sw.Close();
            double rms;
            FourPara forPara = Para4.Calc4Para(pSource, pTo, out rms);


            //SevenPara bigPara7 = Para7.CalBigAngle7Paras(pSource,pTo);
            //SevenPara sevenPara = Para7.Calc7Para(pSource, pTo);
            //SevenPara sevenPara = new SevenPara(-3297.3976, 12883.8090, 17375.9167, 0.4908300492, 0.1026210199, 0.053060665555, -1286.6821354);
            //XYZCoordinate wgs84xyz = new XYZCoordinate(103.51925547390123, 30.588686329132248, 486.1406);
            //XYZCoordinate bj54xyz = Para7.Para7Convert(wgs84xyz, sevenPara);

            //XYZCoordinate wgsxyh = GaussProjection.GaussProjCal(new BLHCoordinate(30.5857555556, 103.5258305556, 16.580), CoordConsts.WGS84Datum, 105);
            //FourPara forPara = new FourPara(-439902.86518674204, -3266656.40625, 0.0013362875487730103, 0.99999205271109981);
            //XYZCoordinate localCor = Para4.Para4Convert(wgsxyh, forPara);
        }
    }
}
