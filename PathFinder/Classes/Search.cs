using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Drawing;
namespace PathFinder
{
    public enum PathFinderNodeType
    {
        Start = 1,
        End = 2,
        Open = 4,
        Close = 8,
        Current = 16,
        Path = 32
    }
    public class Search
    {
        public static int gridSize { get; private set; }
        sbyte[,] grid = null;
        byte[] travelCost = new byte[] { 10, 14 };
        IDictionary<int, Node> closedList = new Dictionary<int, Node>();
        IDictionary<int, Node> openList = new Dictionary<int, Node>();
        public Node startingNode; 
        public Node endNode;
        //SortedSet<Node> openList = new SortedSet<Node>(Node.SortByFullThenByHeuristics());
        //LinkedList<Node> openList = new LinkedList<Node>();
        public Search(sbyte[,] _grid, int _gridSize)
        {
            startingNode = new Node(Node.startPosition, 0);
            endNode = new Node(Node.endPosition, 0);
            gridSize = _gridSize;
            grid = _grid;
        }
        public Search(sbyte[,] _grid, int _gridSize, byte[] _travelCost)
        {
            gridSize = _gridSize;
            grid = _grid;
            travelCost = _travelCost;
        }
        public void findPath()
        {
           
            Node currentNode;
            openList.Add(startingNode.GetHashCode(), startingNode);
            while (openList.Count > 0)
            {
                //lowest F value
                int minF = openList.Min(byF => byF.Value.F);
                //finds a new node to explore
                var check = openList.Where(nextNode => nextNode.Value.F == minF).OrderBy(nextNode => nextNode.Value.H).ToList();
                currentNode = check.First().Value;

                //directions to search;
                sbyte[,] direction = new sbyte[8, 2] { { 0, -1 }, { 1, 1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { -1, 1 }, { -1, 0 }, { -1, -1 } };
                if (closedList.ContainsKey(endNode.GetHashCode()))
                {
                    endNode = closedList[endNode.GetHashCode()];
                    return;
                }
                //if (currentNode.position.Equals(Node.endPosition))
                //{
                //    //make a path
                //    return;
                //}
                for (int i = 0; i < 8; i++)
                {
                    Point newNodePos = new Point(currentNode.position.X + direction[i, 0], currentNode.position.Y + direction[i, 1]);
                    Node newNode = new Node(newNodePos, currentNode, travelCost[i % 2]);
                    //outside the grid
                    if (newNodePos.X < 0 || newNodePos.Y < 0 || newNodePos.X > gridSize || newNodePos.Y > gridSize)
                    {
                        continue;
                    }
                    //A wall(not walkable)
                    if (grid[newNodePos.X, newNodePos.Y] == -1)
                    {
                        continue;
                    }
                    //fount the parent node
                    if (currentNode.parentNode!= null && newNodePos.Equals(currentNode.parentNode.position))
                    {
                        continue;
                    }
                    //node already checked and closed
                    if (closedList.ContainsKey(newNode.GetHashCode()))
                    {
                        continue;
                    }
                    
                    //Checks the open list for nodes with the same position and returns null if nothing is found
                    int tt = newNode.GetHashCode();
                    if (openList.ContainsKey(newNode.GetHashCode()))
                    {
                        //Changes an old node(if G > this.G)
                        Node t = openList[newNode.GetHashCode()];
                        if (t.G > newNode.G)
                        {
                            openList[GetHashCode()] = newNode;
                        }
                    }
                    else
                    {
                        //Adds a new node
                        openList.Add(newNode.GetHashCode(), newNode);
                    }

                }
                closedList.Add(currentNode.GetHashCode(), currentNode);
                openList.Remove(currentNode.GetHashCode());
            }



        }
    }
}
