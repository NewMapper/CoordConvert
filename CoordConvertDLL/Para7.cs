using System;
using System.Collections.Generic;
using System.Text;

namespace CoordConvert
{
    public class Para7
    {
        /// <summary>
        /// 根据3个或者3个以上的点的两套坐标系的坐标计算7参数(最小二乘法) 适用于小角度转换 bursa模型
        /// </summary>
        /// <param name="aPtSource">已知点的源坐标系的坐标</param>
        /// <param name="aPtTo">已知点的新坐标系的坐标</param>
        /// <param name="sep">输出: 7参数</param>
        public static SevenPara Calc7Para(List<XYZCoordinate> aPtSource, List<XYZCoordinate> aPtTo, out double rms)
        {
            #region 给A B 矩阵赋值
            double[,] arrA = new double[aPtSource.Count * 3, 7]; // 如果是4个已知点， 12 * 7矩阵  A*X=B中的矩阵A
            for (int i = 0; i < arrA.GetLength(0); i++)
            {
                if (i % 3 == 0)
                {
                    arrA[i, 0] = 1;
                    arrA[i, 1] = 0;
                    arrA[i, 2] = 0;
                    arrA[i, 3] = aPtSource[i / 3].X;
                    arrA[i, 4] = 0;
                    arrA[i, 5] = -aPtSource[i / 3].Z;
                    arrA[i, 6] = aPtSource[i / 3].Y;
                }
                else if (i % 3 == 1)
                {
                    arrA[i, 0] = 0;
                    arrA[i, 1] = 1;
                    arrA[i, 2] = 0;
                    arrA[i, 3] = aPtSource[i / 3].Y;
                    arrA[i, 4] = aPtSource[i / 3].Z;
                    arrA[i, 5] = 0;
                    arrA[i, 6] = -aPtSource[i / 3].X;
                }
                else if (i % 3 == 2)
                {
                    arrA[i, 0] = 0;
                    arrA[i, 1] = 0;
                    arrA[i, 2] = 1;
                    arrA[i, 3] = aPtSource[i / 3].Z;
                    arrA[i, 4] = -aPtSource[i / 3].Y;
                    arrA[i, 5] = aPtSource[i / 3].X;
                    arrA[i, 6] = 0;
                }
            }
            double[,] arrB = new double[aPtSource.Count * 3, 1]; // A * X = B 中的矩阵B, 如果有4个点，就是 12*1矩阵
            for (int i = 0; i <= arrB.GetLength(0) - 1; i++)
            {
                if (i % 3 == 0)
                {
                    arrB[i, 0] = aPtTo[i / 3].X;
                }
                else if (i % 3 == 1)
                {
                    arrB[i, 0] = aPtTo[i / 3].Y;
                }
                else if (i % 3 == 2)
                {
                    arrB[i, 0] = aPtTo[i / 3].Z;
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
            SevenPara sep = new SevenPara();
            sep.deltaX = mtrResult[0, 0];
            sep.deltaY = mtrResult[1, 0];
            sep.deltaZ = mtrResult[2, 0];
            sep.scale = mtrResult[3, 0];
            sep.Ax = mtrResult[4, 0] / sep.scale;
            sep.Ay = mtrResult[5, 0] / sep.scale;
            sep.Az = mtrResult[6, 0] / sep.scale;

            //计算均方根误差rms
            List<XYZCoordinate> pTo = new List<XYZCoordinate>();
            for (int i = 0; i < aPtSource.Count; i++)
            {
                pTo.Add(Para7Convert(aPtSource[i], sep));
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

            return sep;
        }

        /// <summary>
        /// 12参数最小二乘解大角度7参数
        /// </summary>
        /// <param name="cameraManager"></param>
        /// <param name="XcAndXwPairs"></param>
        /// <param name="X_ins"></param>
        /// <returns></returns>
        public static SevenPara CalBigAngle7Paras(List<XYZCoordinate> aPtSource, List<XYZCoordinate> aPtTo)
        {
            Matrix A = new Matrix(3 * aPtSource.Count, 12);
            Matrix B = new Matrix(3 * aPtSource.Count, 1);

            for (int i = 0; i < 3 * aPtSource.Count; i++)
            {
                if (i % 3 == 0)
                {
                    A[i, 0] = aPtSource[i / 3].X;
                    A[i, 1] = aPtSource[i / 3].Y;
                    A[i, 2] = aPtSource[i / 3].Z;
                    A[i, 3] = 0;
                    A[i, 4] = 0;
                    A[i, 5] = 0;
                    A[i, 6] = 0;
                    A[i, 7] = 0;
                    A[i, 8] = 0;
                    A[i, 9] = 1;
                    A[i, 10] = 0;
                    A[i, 11] = 0;
                }
                else if (i % 3 == 1)
                {
                    A[i, 0] = 0;
                    A[i, 1] = 0;
                    A[i, 2] = 0;
                    A[i, 3] = aPtSource[i / 3].X;
                    A[i, 4] = aPtSource[i / 3].Y;
                    A[i, 5] = aPtSource[i / 3].Z;
                    A[i, 6] = 0;
                    A[i, 7] = 0;
                    A[i, 8] = 0;
                    A[i, 9] = 0;
                    A[i, 10] = 1;
                    A[i, 11] = 0;
                }
                else if (i % 3 == 2)
                {
                    A[i, 0] = 0;
                    A[i, 1] = 0;
                    A[i, 2] = 0;
                    A[i, 3] = 0;
                    A[i, 4] = 0;
                    A[i, 5] = 0;
                    A[i, 6] = aPtSource[i / 3].X;
                    A[i, 7] = aPtSource[i / 3].Y;
                    A[i, 8] = aPtSource[i / 3].Z;
                    A[i, 9] = 0;
                    A[i, 10] = 0;
                    A[i, 11] = 1;
                }
            }

            for (int i = 0; i < 3 * aPtTo.Count; i++)
            {
                if (i % 3 == 0)
                {
                    B[i, 0] = aPtTo[i / 3].X;
                }
                else if (i % 3 == 1)
                {
                    B[i, 0] = aPtTo[i / 3].Y;
                }
                else if (i % 3 == 2)
                {
                    B[i, 0] = aPtTo[i / 3].Z;
                }
            }

            Matrix R12 = Matrix.Inverse(Matrix.Transpose(A) * A) * (Matrix.Transpose(A) * B);

            //根据R_w_ins的矩阵元素可解出各个姿态角
            double s = Math.Sqrt(Math.Pow(R12[0, 0], 2) + Math.Pow(R12[3, 0], 2) + Math.Pow(R12[6, 0], 2));
            double cosRoll = Math.Sqrt(Math.Pow(R12[0, 0], 2) + Math.Pow(R12[3, 0], 2)) / s;
            double sinRoll = -R12[6, 0] / s;
            double roll = (sinRoll >= 0) ? (Math.Acos(cosRoll)) : (-Math.Acos(cosRoll));
            double sinPitch = R12[7, 0] / Math.Cos(roll) / s;
            double cosPitch = R12[8, 0] / Math.Cos(roll) / s;
            double pitch = (sinPitch >= 0) ? (Math.Acos(cosPitch)) : (-Math.Acos(cosPitch));
            double sinYaw = -R12[3, 0] / Math.Cos(roll) / s;
            double cosYaw = R12[0, 0] / Math.Cos(roll) / s;
            double yaw = (sinYaw >= 0) ? (Math.Acos(cosYaw)) : (-Math.Acos(cosYaw));
            yaw = (yaw >= 0) ? yaw : (-yaw);
            

            roll = roll * 180 / Math.PI;
            pitch = pitch * 180 / Math.PI;
            yaw = yaw * 180 / Math.PI;

            SevenPara sevPara = new SevenPara();
            sevPara.deltaX = R12[9, 0];
            sevPara.deltaY = R12[10, 0];
            sevPara.deltaZ = R12[11, 0];
            sevPara.Ax = pitch;
            sevPara.Ay = roll;
            sevPara.Az = yaw;
            sevPara.scale = s;

            return sevPara;
        }

        /// <summary>
        /// //罗德里格公式计算七参数
        ///坐标转换公式：X2=s*R*X1+T
        ///输入参数
        /// X1 坐标系1下标定点坐标 3*N
        /// X2 坐标系2下标定点坐标 3*N
        ///输出
        /// R 旋转矩阵
        /// T 平移向量
        /// s 缩放因子
        ///罗德里格矩阵形式
        /// R = inv(I-S)*(I+S)
        /// I为单位矩阵，S为反对称矩阵
        /// S=[ 0  -c   b;
        ///     c   0  -a;
        ///    -b   a   0]    
        /// </summary>
        /// <param name="ptSource"></param>
        /// <param name="ptTo"></param>
        /// <returns></returns>
        public static List<Matrix> CalRT(List<Matrix> ptSource, List<Matrix> ptTo)
        {
            int N = ptSource.Count;

            Matrix ptSource_center = new Matrix(3, 1);
            Matrix ptTo_center = new Matrix(3, 1);

            //计算控制点质心坐标
            for (int i = 0; i < N; i++)
            {
                ptSource_center += ptSource[i];

                ptTo_center += ptTo[i];
            }

            ptSource_center /= N;

            ptTo_center /= N;

            for (int i = 0; i < N; i++)
            {
                ptSource[i] = ptSource[i] - ptSource_center;

                ptTo[i] = ptTo[i] - ptTo_center;
            }

            //根据线段长度计算 s 比例因子
            double s1 = 0;
            double s2 = 0;
            for (int i = 0; i < N; i++)
            {
                s1 = s1 + Matrix.norm(ptSource[i]);
                s2 = s2 + Matrix.norm(ptTo[i]);
            }
            double s = s2 / s1;

            Matrix A = new Matrix(3 * N, 3);
            Matrix L = new Matrix(3 * N, 1);
            for (int i = 0; i < N; i++)
            {
                double a1 = 1;
                double a2 = 0;
                double a3 = 0;
                double b1 = 0;
                double b2 = 1;
                double b3 = 0;
                double c1 = 0;
                double c2 = 0;
                double c3 = 1;

                // 将坐标转为重力坐标
                // 计算u v w
                double x2 = ptTo[i][0, 0];
                double y2 = ptTo[i][1, 0];
                double z2 = ptTo[i][2, 0];
                double x1 = ptSource[i][0, 0];
                double y1 = ptSource[i][1, 0];
                double z1 = ptSource[i][2, 0];
                double u = a3 * x2 + b3 * y2 + c3 * z2 + s * z1;
                double v = a2 * x2 + b2 * y2 + c2 * z2 + s * y1;
                double w = a1 * x2 + b1 * y2 + c1 * z2 + s * x1;
                Matrix one_A = new Matrix(new double[3, 3] { { 0, u, -v }, { -u, 0, w }, { v, -w, 0 } });
                Matrix one_L = new Matrix(new double[3, 1] { { a1 * x2 + b1 * y2 + c1 * z2 - s * x1 }, { a2 * x2 + b2 * y2 + c2 * z2 - s * y1 }, { a3 * x2 + b3 * y2 + c3 * z2 - s * z1 } });
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        A[3 * i + j, k] = one_A[j, k];
                    }
                    L[3 * i + j, 0] = one_L[j, 0];
                }
            }

            Matrix X = Matrix.Inverse(Matrix.Transpose(A) * A) * Matrix.Transpose(A) * L;

            double a = X[0, 0];
            double b = X[1, 0];
            double c = X[2, 0];
            Matrix S = new Matrix(new double[3, 3] { { 0, -c, b }, { c, 0, -a }, { -b, a, 0 } });
            Matrix I = new Matrix(new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix R = Matrix.Inverse(I - S) * (I + S);
            Matrix T = ptTo_center - s * R * ptSource_center;

            List<Matrix> RT = new List<Matrix>();
            RT.Add(R);
            RT.Add(T);

            return RT;
        }

        /// <summary>
        /// 利用7参数求新坐标系的坐标
        /// </summary>
        /// <param name="aPtSource">点的源坐标系的坐标</param>
        /// <param name="sep">已经知道的7参数</param>
        /// <param name="aPtTo">输出: 点的新坐标系的坐标</param>
        public static XYZCoordinate Para7Convert(XYZCoordinate aPtSource, SevenPara sep)
        {
            double k = sep.scale;
            double a2 = k * sep.Ax;
            double a3 = k * sep.Ay;
            double a4 = k * sep.Az;

            XYZCoordinate ToCoordinate = new XYZCoordinate();
            ToCoordinate.X = sep.deltaX + k * aPtSource.X - a3 * aPtSource.Z + a4 * aPtSource.Y;
            ToCoordinate.Y = sep.deltaY + k * aPtSource.Y + a2 * aPtSource.Z - a4 * aPtSource.X;
            ToCoordinate.Z = sep.deltaZ + k * aPtSource.Z - a2 * aPtSource.Y + a3 * aPtSource.X;

            return ToCoordinate;
        }
    }
}
