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

namespace Lab1
{
    public partial class Histogram : Form
    {
        public Histogram()
        {
            InitializeComponent();
        }

        public int GetMin(int[] element)
        {
           int min = element[0];
            foreach (int el in element)
            {
                if (min > el)
                    min = el;
            }    
            return min;
        }
        public int GetMax(int[] element)
        {
            int max = element[0];
            foreach (int el in element)
            {
                if (el > max)
                    max = el;
            }
            return max;
        }
        public int GetH(int min, int max, int[]stat)
        {
            int H = 0;
            double H1 = 0;
            if (stat.Length % 2 == 0)
            {
                H1 = (max - min) / Math.Sqrt(stat.Length);
            }
            else if (stat.Length % 2 !=0)
            {
                H1 = (max - min) / (Math.Sqrt(stat.Length) - 1);
            }
            H = (int)Math.Ceiling(H1);
            return H;
        }

        //public int FindY(int xpotok, int[] freq, int[] ElemntS, int H)
        //{
        //    int y = 0;
        //    int counter = 0;
        //    for (int i = xpotok; i < xpotok+H; i++)
        //    {
        //        int indexX = Array.IndexOf(ElemntS,i);
        //        if (indexX == -1)
        //        {

        //        }
        //        else
        //        {
        //            if (counter > 0)
        //                y += freq[indexX + 1];
        //            else
        //                y += freq[indexX];
                    
        //        }
                
        //    }
        //    return y;
        //}
        public int FindY(int xpotok, int[] freq, int[] ElemntS, int H)
        {
            int y = 0;
            int counter = 0;
            for (int i = xpotok; i < xpotok + H; i++)
            {
                int indexX = Array.IndexOf(ElemntS, i);
                if (indexX == -1)
                {
                  
                } 
                else
                {
                    y += freq[indexX];
                    counter++;
                }
                    
            }
            return y;
        }
        static void Swap(ref int e1, ref int e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }
        private void Histogram_Load(object sender, EventArgs e)
        {
            var frm = new Table();
            frm.Show();

            int[] stat = File.ReadLines(@"C:\Users\Степан Пантера\Desktop\input_10.txt").Select(l => Convert.ToInt32(l)).ToArray();
            Dictionary<int, int> counts = new Dictionary<int, int>();

            foreach (int a in stat)
            {
                if (counts.ContainsKey(a))
                    counts[a] = counts[a] + 1;
                else
                    counts[a] = 1;
            }
            
            int[] Element = counts.Keys.ToArray();
            int[] Freq = counts.Values.ToArray();
            int[] ElementS = Element;

            var len = ElementS.Length;
            for (int i = 1; i < len; i++)
            {
                for (int j = 0; j < len - i; j++)
                {
                    if (ElementS[j] > ElementS[j + 1])
                    {
                        Swap(ref ElementS[j], ref ElementS[j + 1]);
                        Swap(ref Freq[j], ref Freq[j + 1]);
                    }
                }
            }

            int Min = GetMin(Element);
            int Max = GetMax(Element);
            int H = GetH(Min, Max, stat);
            int Xpotok = Min;
            int Ypotok = FindY(Xpotok, Freq, ElementS, H);

            for (int i = 0; i <= Max; i += H)
            {
                chart1.Series[0].Points.AddXY(Xpotok, Ypotok);
                Xpotok += H;
                Ypotok = FindY(Xpotok, Freq, ElementS, H);
            }

            chart1.Series[0]["PointWidth"] = "1";

            

        }

        //for (int i = 0; i < Element.Length; i++)
        //{
        //    chart1.Series[0].Points.AddXY(Convert.ToDouble(Element[i]), Freq[i]);
        //}
        //chart1.Series[0]["PointWidth"] = "1";
    }
}
