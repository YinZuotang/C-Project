using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        float u = float.Parse("0.3986005e+15");
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.DefaultExt = "txt";
            OpenFileDialog1.Filter = "文本文件（*.txt）|*.txt";
            OpenFileDialog1.FileName = "";
            OpenFileDialog1.Title = "浏览";
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    TextBox1.Text = OpenFileDialog1.FileName;
                    if (File.Exists(OpenFileDialog1.FileName))
                    {
                        string[] gdata = File.ReadAllLines(OpenFileDialog1.FileName,
                            Encoding.Default);
                        a0.Text = gdata[1];
                        a1.Text = gdata[2];
                        a2.Text = gdata[3];
                        te.Text = gdata[4];
                        iode.Text = gdata[5];
                        aRoot.Text = gdata[6];
                        e0.Text = gdata[7];
                        i0.Text = gdata[8];
                        w0.Text = gdata[9];
                        ascendingNode.Text = gdata[10];
                        M.Text = gdata[11];
                        meanMotion.Text = gdata[12];
                        RateascendingN.Text = gdata[13];
                        Ratei.Text = gdata[14];
                        cus.Text = gdata[15];
                        cuc.Text = gdata[16];
                        cis.Text = gdata[17];
                        cic.Text = gdata[18];
                        crs.Text = gdata[19];
                        crc.Text = gdata[20];
                        gpd.Text = gdata[21];
                        tgd.Text = gdata[22];
                        iodc.Text = gdata[23];
                        psatellite.Text = gdata[24];
                        hsatellite.Text = gdata[25];
                    }
                }
                catch
                {
                    MessageBox.Show("读取文本文件出现错误！");
                    Console.WriteLine("the process failed:{0}", e.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double n = Math.Pow(u, 0.5) / Math.Pow(float.Parse(aRoot.Text), 3);
                n = n + double.Parse(meanMotion.Text);
                double tk = 0;
                double Mk = double.Parse(M.Text) + n * tk;
                double Ek = Mk;
                double Ekt = 0;
                while (Math.Abs(Ek - Ekt) > 0.000000000001)
                {
                    Ekt = Ek;
                    Ek = Mk + double.Parse(e0.Text) * Math.Sin(Ekt);
                }
                double e00 = double.Parse(e0.Text);
                double Vk =
                    Math.Atan(Math.Pow(1 - Math.Pow(e00, 2), 0.5) * Math.Sin(Ek) / (Math.Cos(Ek) - e00));
                double Qk = Vk + double.Parse(w0.Text);
                double Cu = double.Parse(cuc.Text) *
          Math.Cos(2 * Qk) + double.Parse(cus.Text) * Math.Sin(2 * Qk);
                double Cr = double.Parse(crc.Text) *
          Math.Cos(2 * Qk) + double.Parse(crs.Text) * Math.Sin(2 * Qk);
                double Ci = double.Parse(cic.Text) *
          Math.Cos(2 * Qk) + double.Parse(cis.Text) * Math.Sin(2 * Qk);
                double Uk = Qk + Cu;
                double Rk = Math.Pow(double.Parse(aRoot.Text), 2) * (1 - e00 * Math.Cos(Ek)) + Cr;
                double Ik = double.Parse(i0.Text) + double.Parse(Ratei.Text) * tk + Ci;
                double OrbitX = Rk * Math.Cos(Uk);
                double OrbitY = Rk * Math.Sin(Uk);
                double we = double.Parse("7.29211567E-5");
                double W = double.Parse(ascendingNode.Text) + (double.Parse(RateascendingN.Text) - we) * tk - we * double.Parse(te.Text);
                double EarthX = OrbitX * Math.Cos(W) - OrbitY * Math.Cos(Ik) * Math.Sin(W);
                double EarthY = OrbitX * Math.Sin(W) - OrbitY * Math.Cos(Ik) * Math.Cos(W);
                double EarthZ = OrbitY * Math.Sin(Ik);
                xcoord.Text = EarthX.ToString();
                ycoord.Text = EarthY.ToString();
                zcoord.Text = EarthZ.ToString();

                //

            }
            catch
            {
                Console.WriteLine("此操作失败：{0}", e.ToString());
            }
        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void xcoord_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        

        
    }
}
