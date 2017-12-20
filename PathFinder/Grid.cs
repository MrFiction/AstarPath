using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public enum DrawModeSetup
{
    None = 0,
    Start = 1,
    End = 2,
    Block = 3
}
namespace PathFinder
{
    public partial class Grid : UserControl
    {
       
        public int gridSize { get; set; }
        public sbyte[,] grid { get; set; }
        public Point sPoint { get; set; }
        public Point ePoint { get; set; }
        public DrawModeSetup drawMode { get; set; }
        public Grid()
        {
            gridSize = 20;
            grid = new sbyte[450, 300];
            sPoint = Point.Empty;
            ePoint = Point.Empty;
            drawMode = DrawModeSetup.None;
            InitializeComponent();
            ResetMatrix();
            
        }
        public void ResetMatrix()
        {
            for (int y = 0; y < grid.GetUpperBound(1); y++)
                for (int x = 0; x < grid.GetUpperBound(0); x++)
                    grid[x, y] = 0;

            sPoint = Point.Empty;
            ePoint = Point.Empty;
            this.Invalidate();
        }



        public void DrawGrid(int x , int y, PathFinderNodeType type)
        {
            Graphics g = Graphics.FromHwnd(this.Handle);
            Rectangle inR = new Rectangle(x * gridSize, y * gridSize, gridSize, gridSize);
            
            Color c = Color.Empty;
            switch (type)
            {
                case PathFinderNodeType.Close:
                    c = Color.DarkSlateBlue;
                    break;
                case PathFinderNodeType.Current:
                    c = Color.Red;
                    break;
                case PathFinderNodeType.End:
                    c = Color.Red;
                    break;
                case PathFinderNodeType.Open:
                    c = Color.Gray;
                    break;
                case PathFinderNodeType.Path:
                    c = Color.Blue;
                    break;
                case PathFinderNodeType.Start:
                    c = Color.Green;
                    break;
            }

            using (Brush brush = new SolidBrush(c))
            {
                g.FillRectangle(brush, inR);
            }
            c = Color.Black;
            using (Pen pen = new Pen(c))
            {
                g.DrawLine(pen, x * gridSize, y * gridSize, x * gridSize + gridSize, y * gridSize);
                g.DrawLine(pen, x * gridSize + gridSize, y * gridSize, x * gridSize + gridSize, y * gridSize + gridSize);
                g.DrawLine(pen, x * gridSize, y * gridSize, x * gridSize, y * gridSize + gridSize);
                g.DrawLine(pen, x * gridSize + gridSize, y * gridSize, x * gridSize + gridSize, y * gridSize + gridSize);   

            }
            g.Dispose();


        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            try
            {
                for (int y = e.ClipRectangle.Y; y < e.ClipRectangle.Bottom; y += gridSize)
                {
                    for (int x = e.ClipRectangle.X; x < e.ClipRectangle.Right; x += gridSize)
                    {

                        int sx = x / gridSize;
                        int sy = y / gridSize;
                        Color color = Color.Empty;
                        switch (grid[sx, sy])
                        {
                            case -1:
                                color = Color.Black;
                                break;
                            case 0:
                                color = Color.White;
                                break;
                            case 1:
                                color = Color.Green;
                                break;
                            case 2:
                                color = Color.Red;
                                break;
                            case 4:
                                color = Color.Gray;
                                break;
                            case 8:
                                color = Color.DarkSlateBlue;
                                break;
                            case 16:
                                color = Color.Red;
                                break;
                            case 32:
                                color = Color.Blue;
                                break;

                        }
                        using (SolidBrush brush = new SolidBrush(color))
                        {
                            g.FillRectangle(brush, x, y, gridSize, gridSize);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw new Exception("Blogai");
            }


            Color c = Color.Black;
            using (Pen pen = new Pen(c))
            {

                for (int y = (e.ClipRectangle.Y / gridSize) * gridSize; y < e.ClipRectangle.Bottom + gridSize; y += gridSize)
                {
                    g.DrawLine(pen, e.ClipRectangle.X, y, e.ClipRectangle.Right, y);
                }
                for (int x = (e.ClipRectangle.X / gridSize) * gridSize; x <= e.ClipRectangle.Right + gridSize; x += gridSize)
                {
                    g.DrawLine(pen, x, e.ClipRectangle.Y, x, e.ClipRectangle.Bottom);
                }

            }
            base.OnPaint(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None || drawMode == DrawModeSetup.None)
                return;

            int x = e.X / gridSize;
            int y = e.Y / gridSize;

            switch (drawMode)
            {
                case DrawModeSetup.Start:
                    grid[sPoint.X, sPoint.Y] = 0;
                    this.Invalidate(new System.Drawing.Rectangle(sPoint.X * gridSize, sPoint.Y * gridSize, gridSize, gridSize));
                    sPoint = new Point(x, y);
                    grid[x, y] = 1;
                    break;
                case DrawModeSetup.End:
                    grid[ePoint.X, ePoint.Y] = 0;
                    this.Invalidate(new System.Drawing.Rectangle(ePoint.X * gridSize, ePoint.Y * gridSize, gridSize, gridSize));
                    ePoint = new Point(x, y);
                    grid[x, y] = 2;
                    break;
                case DrawModeSetup.Block:
                    grid[x, y] = -1;
                    break;
              
            }
            //e.Button = MouseButtons.None;
            this.Invalidate(new System.Drawing.Rectangle(x * gridSize, y * gridSize, gridSize, gridSize));
            base.OnMouseMove(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.OnMouseMove(e);
            base.OnMouseDown(e);
        }

        //private void Grid_Paint(object sender, PaintEventArgs e)
        //{
        //    grid[sPoint.X, sPoint.Y] = 1;
        //    grid[ePoint.X, ePoint.Y] = 2;
        //    Graphics g = e.Graphics;
        //    try
        //    {
        //        for (int y = e.ClipRectangle.Y ; y < e.ClipRectangle.Bottom; y += gridSize)
        //        {
        //            for (int x =e.ClipRectangle.X ; x < e.ClipRectangle.Right; x += gridSize)
        //            {

        //                int sx = x / gridSize;
        //                int sy = y / gridSize;
        //                Color color = Color.Empty;
        //                switch (grid[sx,sy])
        //                {
        //                    case -1:
        //                        color = Color.Black;
        //                        break;
        //                    case 1:
        //                        color = Color.Green;
        //                        break;
        //                    case 2:
        //                        color = Color.Red;
        //                        break;
        //                    case 4:
        //                        color = Color.Gray;
        //                        break;
        //                    case 8:
        //                        color = Color.DarkSlateBlue;
        //                        break;
        //                    case 16:
        //                        color = Color.Red;
        //                        break;
        //                    case 32:
        //                        color = Color.Blue;
        //                        break;

        //                }
        //                using (SolidBrush brush = new SolidBrush(color))
        //                {
        //                    g.FillRectangle(brush, x,y, gridSize, gridSize);
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw new Exception("Blogai");
        //    }


        //    Color c = Color.Black;
        //    using (Pen pen = new Pen(c))
        //    {

        //        for (int y = (e.ClipRectangle.Y/gridSize)*gridSize ; y < e.ClipRectangle.Bottom + gridSize;  y += gridSize)
        //        {
        //            g.DrawLine(pen, e.ClipRectangle.X, y, e.ClipRectangle.Right, y);
        //        }
        //        for (int x = (e.ClipRectangle.X / gridSize) * gridSize; x <= e.ClipRectangle.Right + gridSize; x += gridSize)
        //        {
        //            g.DrawLine(pen, x, e.ClipRectangle.Y, x, e.ClipRectangle.Bottom);
        //        }

        //    }

        //}

        private void Grid_Load(object sender, EventArgs e)
        {
            

        }

       
    }
}
