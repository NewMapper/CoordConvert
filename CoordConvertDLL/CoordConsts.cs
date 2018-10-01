using System;
using System.Collections.Generic;
using System.Text;

namespace CoordConvert
{
    public class CoordConsts
    {
        public static Datum WGS84Datum = new Datum(6378137, 6356752.3142, 0.00669437999013);
        public static Datum bj54Datum = new Datum(6378245, 6356863.0188, 0.006693421622966);
        public static Datum xian80Datum = new Datum(6378140, 6356755.2882, 0.00669438499959);
    }
}
