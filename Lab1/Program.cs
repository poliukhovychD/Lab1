using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.AxHost;
using Lab1;

namespace Lab1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static double Xser(int[] stat)
        {
            int sum = 0;
            for (int i = 0; i < stat.Length; i++)
            {
                sum += stat[i];
            }
            //Console.WriteLine("Summa: " + sum);

            double xser = sum / stat.Length;
            return xser;
        }
        static float GetMedian(int[] stat)
        {
            Array.Sort(stat);
            float Mediana;
            if (stat.Length % 2 == 0)
            {
                Mediana = (stat.Length / 2 + (stat.Length / 2 + 1)) / 2;
                Mediana = ((stat[(int)Mediana] + stat[(int)Mediana - 1]) / 2f);
                Console.WriteLine("Mediana: " + Mediana);
                return Mediana;
            }

            else
            {
                Mediana = (stat.Length + 1) / 2;
                Console.WriteLine("Mediana: " + stat[(int)Mediana - 1]);
                return Mediana;
            }

        }
        static int GetMode(int[] stat, Dictionary<int, int> counts)
        {
            int result = int.MinValue;
            int max = int.MinValue;
            foreach (int key in counts.Keys)
            {
                if (counts[key] > max)
                {
                    max = counts[key];
                    result = key;
                }
            }
            Console.WriteLine("The mode is: " + result);
            return result;
        }
        static double GetVariance(int[] stat, double xser, Dictionary<int, int> counts)
        {
            int[] element = counts.Keys.ToArray();
            int[] freq = counts.Values.ToArray();

            double variance = 0;

            for (int i = 0; i < element.Length; i++)
            {
                variance += freq[i] * Math.Pow(element[i] - xser, 2.0);
            }

            variance = (variance / (stat.Length - 1));
            Console.WriteLine("Variance is: " + variance);
            return variance;
        }
        static double GetDeviation(float Variance)
        {
            double deviation = Math.Sqrt(Variance);
            Console.WriteLine("Deviation is: " + deviation);
            return deviation;
        }
        static void WriteFile(int[] stat, float Mediana, int Mode, double Variance, double Deviation)
        {
            StreamWriter stream = new StreamWriter(@"C:\Users\Степан Пантера\Desktop\input_10calc.txt");
            foreach (int p in stat)
            {
                stream.WriteLine(p);
            }

            stream.WriteLine("Mediana: " + Mediana);
            stream.WriteLine("The mode is: " + Mode);
            stream.WriteLine("Variance is: " + Variance);
            stream.WriteLine("Deviation is: " + Deviation);
            stream.Close();
        }

        static void Main()
        {

            int[] stat = File.ReadLines(@"C:\Users\Степан Пантера\Desktop\input_10.txt").Select(l => Convert.ToInt32(l)).ToArray();

            for (int i = 0; i < stat.Length; i++)
            {
                Console.WriteLine(stat[i] + " ");
            }

            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (int a in stat)
            {
                if (counts.ContainsKey(a))
                    counts[a] = counts[a] + 1;
                else
                    counts[a] = 1;
            }
            double xser = Xser(stat);
            float Mediana = GetMedian(stat);
            int Mode = GetMode(stat, counts);
            double Variance = GetVariance(stat, xser, counts);
            double Deviation = GetDeviation((float)Variance);
            WriteFile(stat, Mediana, Mode, Variance, Deviation);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Histogram());

        }
    }
}
