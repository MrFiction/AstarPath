using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathFinder
{
    public partial class Path : Form
    {


        private delegate void PathFinderDebugDelegate(int x, int y, PathFinderNodeType type);


        private void Path_Load(object sender, EventArgs e)
        {
            ComboBoxItems();
            numericUpDown1.Value = 1;

        }
        private void PathFinderDebug(int x, int y, PathFinderNodeType type)
        {

            grid2.DrawGrid(x, y, type);
        }
        public void RunPathFinder()
        {
            Node.startPosition = grid2.sPoint;
            Node.endPosition = grid2.ePoint;
            Search aPath = new Search(grid2.grid, grid2.gridSize);

            if (aPath != null)
                aPath.PathFinderDebug += new PathFinderDebugHandler(PathFinderDebug);
            grid2.gridSize = Search.cellSize;
            aPath.findPath();
            if (aPath.endNode.parentNode == null)
            {
                MessageBox.Show("Path Not Found");
            }
           
        }


        public void ComboBoxItems()
        {
            comboBox1.Items.Clear();
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
            for (int i = 30; i > 0; i--)
            {
                int height = (grid2.Size.Height) / i;
                int width = (grid2.Size.Width) / i;
                string m = String.Format("{0} X {1}  ({2})", width, height, width * height);
                if ((grid2.Size.Height) % i == 0 && (grid2.Size.Width) % i == 0)
                {
                    comboBox1.Items.Add(new ComboBoxItem(m, i, width, height));
                }

            }
            comboBox1.SelectedIndex = 0;
        }


        public Path()
        {
            InitializeComponent();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid2.Invalidate();
            var a = (ComboBoxItem)comboBox1.SelectedItem;
            grid2.gridSize = a.sqSize;
            grid2.ePoint = new System.Drawing.Point(grid2.Width / (2 * grid2.gridSize), grid2.Height / (2 * grid2.gridSize));
            grid2.grid = new sbyte[a.w + 1, a.h + 1];
        }
        private class ComboBoxItem
        {
            public string text { get; set; }

            public int sqSize { get; set; }
            public int w { get; set; }

            public int h { get; set; }
            public ComboBoxItem()
            {

            }

            public ComboBoxItem(string text, int sqSize, int w, int h)
            {
                this.text = text;
                this.sqSize = sqSize;
                this.w = w;
                this.h = h;
            }
        }

        private void grid2_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            grid2.drawMode = DrawModeSetup.Start;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            grid2.drawMode = DrawModeSetup.End;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            grid2.drawMode = DrawModeSetup.Block;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RunPathFinder();
            //Search.;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            grid2.ResetMatrix();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Node.heuristicsMult = (int)numericUpDown1.Value;
        }


    }


}
