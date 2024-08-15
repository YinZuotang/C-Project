using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test2
{
    public partial class Form1 : Form
    {
        double L, B, H;//jingdu weidu gaocheng
        double LDeg, BDeg, LMin, BMin, LSec, BSec;
        string TxtB1, TxtB2, TxtB3, TxtB4, TxtB5, TxtB6;
        double a, b, f, e2, N;//tuoqiuchangbanzhou duanbanzhou bianlv bianxinlvpingfang maoyouquanbanjing
        double X, Y, Z;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {{
            a = 6378245;
            double temp1, temp2;
            temp1 = 1;
            temp2 = 298.257;
            f = temp1 / temp2;
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty) 
            { MessageBox.Show("大地经度、纬度和高程不能为空"); }   

       
            else             
       {                 
        TxtB1 = textBox1.Text;                 
        TxtB2 = textBox2.Text;                 
        TxtB3 = textBox3.Text;
        LDeg = Math.Truncate(double.Parse(TxtB1)) ;    
                LMin = Convert.ToInt16( TxtB1.Substring(TxtB1.IndexOf(".") + 1, 2));
                LSec = Convert.ToInt16(TxtB1.Substring(TxtB1.IndexOf(".") + 1, 2));

                L = LDeg + LMin / 60 + LSec / 3600; L = double.Parse(TxtB1);

                BDeg = Math.Truncate(Convert.ToDouble(TxtB2));

                BMin = Convert.ToInt16(TxtB2.Substring(TxtB2.IndexOf(".") + 1, 2));
                BSec = Convert.ToInt16(TxtB2.Substring(TxtB2.IndexOf(".") + 1, 2)); 
 
                B = BDeg + BMin / 60 + BSec / 3600;

                B = double.Parse(TxtB2);  
 
                b = a - a * f;     
                e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);                 
                N = a / Math.Sqrt(1 - e2 * Math.Pow(Math.Sin(B * Math.PI / 180), 2));    //计算N 


                H = double.Parse(TxtB3); 
 
                X=(N+H )*Math.Cos(B*Math.PI /180)*Math.Cos(L*Math.PI /180);                
                Y=(N+H )*Math.Cos(B*Math.PI /180)*Math.Sin(L*Math.PI /180);                 
                Z = (N*(1-e2)+H)*Math.Sin (B*Math .PI /180);                 
                textBox4.Text = X.ToString();                 
                textBox5.Text = Y.ToString();                 
                textBox6.Text = Z.ToString(); 
    }}
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TxtB4 = textBox4.Text;
            TxtB5 = textBox5.Text;
            TxtB6 = textBox6.Text;
            X = double.Parse(TxtB4);
            Y = double.Parse(TxtB5);
            Z = double.Parse(TxtB6);
            if (Math.Abs(X) <= 0.000001)
            {
                if (Y > 0.000001)
                {
                    L = Math.PI / 2;
                }
                if (Y < -0.000001)
                {
                    L = Math.PI * 3 / 2;
                }
                if (Math.Abs(Y) <= 0.000001)
                {
                    L = 0;
                }
                return;
            }
            L = Math.Atan(Math.Abs(Y / X));
            if (X > 0.000001)
            {
                if (Y < -0.000001)
                {
                    L = 2 * Math.PI - L;
                }
            }
            if (X < -0.000001)
            {
                if (Y > 0.000001)
                {
                    L = Math.PI - L;
                }
                if (Y < -0.000001)
                {
                    L = Math.PI + L;
                }
                if (Math.Abs(Y) <= 0.0000001)
                    L = Math.PI;
            }
            double H1;
            H = 0;

            do
            {
                H1 = H;
                B = Math.Atan(Z * (N + H1) / (Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)) * (N * (1 - e2) + H1)));
                H = Z / Math.Sin(B) - N * (1 - e2);
            }
            while (Math.Abs(H - H1) <= 0.00000001);
            L = L / Math.PI * 180;
            B = B / Math.PI * 180;
            LDeg = Math.Truncate(L);
            BDeg = Math.Truncate(B);

            double LMinF,BMinF,LSecF,BSecF;

            LMinF=(L-LDeg)*60;
            LMin = Math.Truncate(LMinF);
            LSecF = (LMinF - LMin) * 60;
            LSec = Math.Truncate(LSecF);

            BMinF = (B - BDeg) * 60;
            BMin = Math.Truncate(BMinF);
            BSecF = (BMinF - BMin) * 60;
            BSec = Math.Truncate(BSecF);

            string LStr, BStr;
            LStr = LDeg.ToString() + "." + LMin.ToString() + LSec.ToString();
            BStr = BDeg.ToString() + "." + BMin.ToString() + BSec.ToString();

            textBox1.Text = LStr;
            textBox2.Text = BStr;
            textBox3.Text = H.ToString();

        }

       }
}
