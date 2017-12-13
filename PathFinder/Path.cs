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

        
       
        
        
        private void Path_Load(object sender, EventArgs e)
        {
            ComboBoxItems();
           // comboBox2.Items.Add
            
        }

       
        public void ComboBoxItems()
        {
            comboBox1.Items.Clear();
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
            for (int i = 30; i > 2; i--)
            {
                int height = (grid2.Size.Height)/i;
                int width = (grid2.Size.Width)/i;
                string m = String.Format("{0} X {1}  ({2})", width, height , width * height);
                if ((grid2.Size.Height-1)%i == 0 && (grid2.Size.Width-1) % i == 0)
                {
                    comboBox1.Items.Add(new ComboBoxItem(m,i,width,height));
                }

            }
            comboBox1.SelectedIndex = 0;
        }






        public Path()
        {
            InitializeComponent();
        }

        private void Path_Resize(object sender, EventArgs e)
        {
            ComboBoxItems();
            int w = grid2.Size.Width;
            int h = grid2.Size.Height;
            grid2.gridSize = 1;
            grid2.grid = new sbyte[w, h];
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid2.Invalidate();
            var a = (ComboBoxItem)comboBox1.SelectedItem;
            grid2.gridSize = a.sqSize;
            grid2.ePoint = new System.Drawing.Point(grid2.Width / (2 * grid2.gridSize), grid2.Height / (2 * grid2.gridSize));
            grid2.grid = new sbyte[a.w+1, a.h+1];
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
            //Search.;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Node.heuristicsMult = (int)numericUpDown1.Value;
        }
    }


}
