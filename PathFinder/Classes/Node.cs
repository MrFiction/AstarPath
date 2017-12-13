using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace PathFinder
{
    public class Node
    {
        public static int heuristicsMult { get; set; }
        public static Point startPosition { get; private set; }
        public static Point endPosition { get; private set; }
        public Point position { get; private set;}
        public Node parentNode { get; private set; }
        public int G { get; private set; }
        public int H { get; private set; }
        public int F { get; private set; }
        
        public Node(Point position, Node parentNode, int g)
        {
            this.position = position;
            this.parentNode = parentNode;
            G = parentNode.G + g;
            H = HeuristicValue();
            F = g + this.H;
        }

        private int HeuristicValue()
        {
            int d = 10;
            int d2 = 14;
            int dx = Math.Abs(position.X - endPosition.X);
            int dy = Math.Abs(position.Y - endPosition.Y);
            return d * (dx + dy) + (d2 - 2 * d2) * Math.Min(dx, dy) * heuristicsMult;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int result = 33;
                result = 37 * result + position.X;
                result = 37 * result + position.Y;
                return result;
            }
            
        }
        private class SortByFThenByH : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                if (a == null || b == null)
                {
                    throw new ArgumentNullException("Null for node comparison");
                }
                try
                {
                    Node first = (Node)a;
                    Node second = (Node)b;
                    if (first.F > second.F)
                    {
                        return 1;
                    }
                    if (first.F < second.F)
                    {
                        return -1;
                    }
                    if (first.H < second.H)
                    {
                        return -1;
                    }
                    return 1;

                }
                catch (InvalidCastException)
                {

                    throw new InvalidCastException("Not a node");
                }
                catch
                {
                    throw new Exception("Something with node comparison");
                }
            }
        }
        public static IComparer<Node> SortByFullThenByHeuristics()
        {
            return (IComparer<Node>) new SortByFThenByH();
        }
    }
}
