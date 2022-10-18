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
    public partial class Table : Form
    {
        public Table()
        {
            InitializeComponent();
        }

        private void Table_Load(object sender, EventArgs e)
        {
            int[] stat = File.ReadLines(@"C:\Users\Степан Пантера\Desktop\input_10.txt")
                .Select(l => Convert.ToInt32(l)).ToArray();

            Dictionary<int, int> counts = new Dictionary<int, int>();

            foreach (int a in stat)
            {
                if (counts.ContainsKey(a))
                    counts[a] = counts[a] + 1;
                else
                    counts[a] = 1;
            }

            int[] d = counts.Keys.ToArray();
            int[] r = counts.Values.ToArray();
            int tmp = r[0];


            for (int i = 0; i < d.Length; i++)
            {
                dataGridView1.Rows.Add(d[i], r[i], tmp);
                if (i != d.Length - 1)
                    tmp += r[i + 1];
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
