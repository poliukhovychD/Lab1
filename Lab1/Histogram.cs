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

            for (int i = 0; i < Element.Length; i++)
            {
                chart1.Series[0].Points.AddXY(Convert.ToDouble(Element[i]), Freq[i]);
            }
        }

        private void Histogram_Load_1(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
