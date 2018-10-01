using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoordConvert;

namespace CoordConvertApp
{
    public partial class CalFourPara : Form
    {
        Datum WGS84Datum = new Datum(6378137, 6356752.3142, 0.00669437999013);

        public CalFourPara()
        {
            InitializeComponent();
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            try
            {
                List<BLHCoordinate> pSourceBLH = new List<BLHCoordinate>();
                List<XYZCoordinate> pSource = new List<XYZCoordinate>();
                List<XYZCoordinate> pTo = new List<XYZCoordinate>();
                List<XYZCoordinate> pToNew = new List<XYZCoordinate>();

                if (txtB1.Text != "" && txtL1.Text != "")
                {
                    pSourceBLH.Add(new BLHCoordinate(double.Parse(txtB1.Text), double.Parse(txtL1.Text), 0));
                }
                if (txtB2.Text != "" && txtL2.Text != "")
                {
                    pSourceBLH.Add(new BLHCoordinate(double.Parse(txtB2.Text), double.Parse(txtL2.Text), 0));
                }
                if (txtB3.Text != "" && txtL3.Text != "")
                {
                    pSourceBLH.Add(new BLHCoordinate(double.Parse(txtB3.Text), double.Parse(txtL3.Text), 0));
                }
                if (txtB4.Text != "" && txtL4.Text != "")
                {
                    pSourceBLH.Add(new BLHCoordinate(double.Parse(txtB4.Text), double.Parse(txtL4.Text), 0));
                }
                if (txtB5.Text != "" && txtL5.Text != "")
                {
                    pSourceBLH.Add(new BLHCoordinate(double.Parse(txtB5.Text), double.Parse(txtL5.Text), 0));
                }
                if (txtB6.Text != "" && txtL6.Text != "")
                {
                    pSourceBLH.Add(new BLHCoordinate(double.Parse(txtB6.Text), double.Parse(txtL6.Text), 0));
                }

                if (txtX1.Text != "" && txtY1.Text != "")
                {
                    pTo.Add(new XYZCoordinate(double.Parse(txtX1.Text), double.Parse(txtY1.Text), 0));
                }
                if (txtX2.Text != "" && txtY2.Text != "")
                {
                    pTo.Add(new XYZCoordinate(double.Parse(txtX2.Text), double.Parse(txtY2.Text), 0));
                }
                if (txtX3.Text != "" && txtY3.Text != "")
                {
                    pTo.Add(new XYZCoordinate(double.Parse(txtX3.Text), double.Parse(txtY3.Text), 0));
                }
                if (txtX4.Text != "" && txtY4.Text != "")
                {
                    pTo.Add(new XYZCoordinate(double.Parse(txtX4.Text), double.Parse(txtY4.Text), 0));
                }
                if (txtX5.Text != "" && txtY5.Text != "")
                {
                    pTo.Add(new XYZCoordinate(double.Parse(txtX5.Text), double.Parse(txtY5.Text), 0));
                }
                if (txtX6.Text != "" && txtY6.Text != "")
                {
                    pTo.Add(new XYZCoordinate(double.Parse(txtX6.Text), double.Parse(txtY6.Text), 0));
                }

                int ptNum = 0;
                if (txtPtNum.Text != "")
                {
                    ptNum = int.Parse(txtPtNum.Text);
                }

                double Lon = 0;
                if (pSourceBLH.Count > 1)
                {
                    Lon = (int)pSourceBLH[0].L;
                }
                else
                {
                    MessageBox.Show("请输入控制点坐标!");
                    return;
                }

                for (int i = 0; i < ptNum; i++)
                {
                    XYZCoordinate wgsxy = GaussProjection.GaussProjCal(pSourceBLH[i], WGS84Datum, Lon);
                    pSource.Add(wgsxy);
                    pToNew.Add(pTo[i]);
                }
                double rms;
                FourPara forPara = Para4.Calc4Para(pSource, pTo,out rms);
                txtLon.Text = Lon + "";
                txtDeltaX.Text = forPara.deltaX + "";
                txtDeltaY.Text = forPara.deltaY + "";
                txtAx.Text = forPara.Ax + "";
                txtS.Text = forPara.scale + "";
            }
            catch
            {
                MessageBox.Show("错误，请检查!");
            }
        }
    }
}
