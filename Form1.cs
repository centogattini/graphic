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


namespace Graphic
{
    public partial class Form1 : Form
    {
        int a = 5;
        // Если переменная имеет значение "true" то используется встроенный график, иначе используется график по точкам из файла
        Boolean graphicType = false;
        List<double> X2;
        List<double> Y2;
        public Form1()
        {
            
            InitializeComponent();
            
            initFunc();
            update();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            a = trackBar1.Value;
            update();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        private void update()
        {
            chart1.Series[0].Points.Clear();
            if (graphicType == false)
            {
                updateChart1();
            }
            else
            {
                updateChart2();
            }
        }
        private void updateChart2()
        {
            trackBar1.Visible = false;
            label1.Visible = false;
            for (int i = 0; i < X2.Count; i++)
            {
                chart1.Series[0].Points.AddXY(X2[i], Y2[i]);
            }
        }
        private void updateChart1()
        {
            label1.Visible = true;
            trackBar1.Visible = true;
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            // Реализововать будем функцию sqrt(sin(pi*sqrt(x)))
            Func<double, double> func = x => Math.Sin(x/(a+10)) + Math.Cos(x);
            // Инициализруем точки оси Ох
            double[] X = new double[1000];
            for (int i = 0; i < 1000; i++)
            {
                X[i] = (i) / 10;
            }
            //Инициализируем Y-координаты точек применяя к ним фунукцию func и нанесём точки на график
            double[] Y = new double[1000];
            for (int i = 0; i < 1000; i++)
            {
                Y[i] = func(X[i]);
                chart1.Series[0].Points.AddXY(X[i], Y[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graphicType = true;
            update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graphicType = false;
            update();
        }
        private void initFunc()
        {
            X2 = new List<double>();
            Y2 = new List<double>();
            using (StreamReader sr = new StreamReader("C:/Users/phoen/source/repos/Graphic/Graphic/func.txt"))
            {
                X2 = stringToArrayD(sr.ReadLine()).ToList();
                X2.Sort();
                Y2 = stringToArrayD(sr.ReadLine()).ToList();
            }
        }
        static double[] stringToArrayD(string text)
        {
            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            double[] array = new double[words.Length];
            for (int i = 0; i < array.Length; i++)
                array[i] = Double.Parse(words[i]);
            return array;
        }


    }
}
