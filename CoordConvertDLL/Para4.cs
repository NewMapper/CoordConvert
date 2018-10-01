using System;
using System.Collections.Generic;
using System.Text;

namespace CoordConvert
{
    public class Para4
    {
        /// <summary>
        /// 根据2个或者2个以上的点的两套坐标系的坐标计算4参数(最小二乘法) 
        /// </summary>
        /// <param name="aPtSource">已知点的源坐标系的坐标</param>
        /// <param name="aPtTo">已知点的新坐标系的坐标</param>
        /// <param name="sep">输出: 4参数</param>
        public static FourPara Calc4Para(List<XYZCoordinate> aPtSource, List<XYZCoordinate> aPtTo,out double rms)
        {
            #region 给A B 矩阵赋值
            double[,] arrA = new double[aPtSource.Count * 2, 4]; // 如果是4个已知点， 8 * 4矩阵  A*X=B中的矩阵A
            for (int i = 0; i <= arrA.GetLength(0) - 1; i++)
            {
                if (i % 2 == 0)
                {
                    arrA[i, 0] = aPtSource[i / 2].X;
                    arrA[i, 1] = aPtSource[i / 2].Y;
                    arrA[i, 2] = 1;
                    arrA[i, 3] = 0;
                }
                else if (i % 2 == 1)
                {
                    arrA[i, 0] = aPtSource[i / 2].Y;
                    arrA[i, 1] = -aPtSource[i / 2].X;
                    arrA[i, 2] = 0;
                    arrA[i, 3] = 1;
                }
            }
            double[,] arrB = new double[aPtSource.Count * 2, 1]; // A * X = B 中的矩阵B, 如果有4个点，就是 8*1矩阵
            for (int i = 0; i <= arrB.GetLength(0) - 1; i++)
            {
                if (i % 2 == 0)
                {
                    arrB[i, 0] = aPtTo[i / 2].X;
                }
                else if (i % 2 == 1)
                {
                    arrB[i, 0] = aPtTo[i / 2].Y;
                }
            }
            #endregion
            Matrix mtrA = new Matrix(arrA); // A矩阵
            Matrix mtrAT = Matrix.Transpose(mtrA); // A的转置
            Matrix mtrB = new Matrix(arrB); // B矩阵
            Matrix mtrATmulA = Matrix.Multiply(mtrAT, mtrA); // A的转置×A
            // 求(A的转置×A)的逆矩阵
            mtrATmulA = Matrix.Inverse(mtrATmulA);
            // A的转置 × B
            Matrix mtrATmulB = Matrix.Multiply(mtrAT, mtrB); // A的转置 * B
            // 结果
            Matrix mtrResult = Matrix.Multiply(mtrATmulA, mtrATmulB);

            FourPara forPara = new FourPara();
            forPara.deltaX = mtrResult[2, 0];
            forPara.deltaY = mtrResult[3, 0];
            forPara.Ax = Math.Atan(mtrResult[1, 0] / mtrResult[0, 0]);
            forPara.scale = mtrResult[1, 0] / Math.Sin(forPara.Ax);

            //计算均方根误差rms
            List<XYZCoordinate> pTo = new List<XYZCoordinate>();
            for (int i = 0; i < aPtSource.Count; i++)
            {
                pTo.Add(Para4Convert(aPtSource[i], forPara));
            }
            rms = 0;
            for (int i = 0; i < aPtTo.Count; i++)
            {
                double deltaX = aPtTo[i].X - pTo[i].X;
                double deltaY = aPtTo[i].Y - pTo[i].Y;

                double oneSigma = Math.Sqrt((Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)) / 2);

                rms += oneSigma;
            }

            rms = rms / aPtTo.Count;

            return forPara;
        }

        /// <summary>
        /// 利用4参数求新坐标系的坐标
        /// </summary>
        /// <param name="aPtSource">点的源坐标系的坐标</param>
        /// <param name="sep">已经知道的4参数</param>
        /// <param name="aPtTo">输出: 点的新坐标系的坐标</param>
        public static XYZCoordinate Para4Convert(XYZCoordinate aPtSource, FourPara forPara)
        {
            double k = forPara.scale;
            double a1 = k * Math.Cos(forPara.Ax);
            double a2 = k * Math.Sin(forPara.Ax);

            XYZCoordinate ToCoordinate = new XYZCoordinate();
            ToCoordinate.X = forPara.deltaX + a1 * aPtSource.X + a2 * aPtSource.Y;
            ToCoordinate.Y = forPara.deltaY + a1 * aPtSource.Y - a2 * aPtSource.X;
            ToCoordinate.Z = aPtSource.Z;

            return ToCoordinate;
        }
    }
}
