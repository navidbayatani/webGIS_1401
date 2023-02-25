using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string x1=textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string y1=textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string x2=textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string y2=textBox4.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string x1 = textBox1.Text;
            double res = Convert.ToDouble(x1);

            string y1 = textBox2.Text;
            double res2 = Convert.ToDouble(y1);

            string x2 = textBox3.Text;
            double res3 = Convert.ToDouble(x2);

            string y2 = textBox4.Text;
            double res4 = Convert.ToDouble(y2);

            double delta_x = res3 - res;
            double delta_y = res4 - res2;

            double javab = Math.Atan2(delta_x,delta_y) * 180/Math.PI;
            if (delta_x< 0)
            {
                javab = javab + 360;
            }

            label6.Text=Convert.ToString(javab);

            




        }
    }
}
