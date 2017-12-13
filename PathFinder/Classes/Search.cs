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
        bool[,] grid = null;
        byte[] travelCost = new byte[] { 10, 14 };
        IDictionary<int, Node> closedList = new Dictionary<int, Node>();
        IDictionary<int, Node> openList = new Dictionary<int, Node>();
        //SortedSet<Node> openList = new SortedSet<Node>(Node.SortByFullThenByHeuristics());
        //LinkedList<Node> openList = new LinkedList<Node>();
        public Search(bool[,] _grid, int _gridSize)
        {
            gridSize = _gridSize;
            grid = _grid;
        }
        public Search(bool[,] _grid, int _gridSize, byte[] _travelCost)
        {
            gridSize = _gridSize;
            grid = _grid;
            travelCost = _travelCost;
        }
        public void findPath(Node startingNode, Node endNode)
        {
            Node currentNode;
            openList.Add(startingNode.GetHashCode(), startingNode);
            while (openList.Count > 0)
            {
                //lowes F value
                int minF = openList.Min(byF => byF.Value.F);
                //finds a new node to explore
                currentNode = openList.Where(nextNode => nextNode.Value.F == minF).OrderBy(nextNode => nextNode.Value.H).First().Value;
                //directions to search;
                sbyte[,] direction = new sbyte[8, 2] { { 0, -1 }, { 1, 1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { -1, 1 }, { -1, 0 }, { -1, -1 } };
                if (currentNode.position.Equals(Node.endPosition))
                {
                    //make a path
                    return;
                }
                for (int i = 0; i < 8; i++)
                {
                    Point newNodePos = new Point(currentNode.position.X + direction[i, 0], currentNode.position.Y + direction[i, 1]);

                    //outside the grid
                    if (newNodePos.X < 0 && newNodePos.Y < 0 && newNodePos.X > gridSize && newNodePos.Y > gridSize)
                    {
                        continue;
                    }
                    //A wall(not walkable)
                    if (grid[newNodePos.X, newNodePos.Y] == true)
                    {
                        continue;
                    }
                    //fount the parent node
                    if (newNodePos.Equals(currentNode.parentNode.position))
                    {
                        continue;
                    }
                    //node already checked and closed
                    if (closedList.ContainsKey(newNodePos.GetHashCode()))
                    {
                        continue;
                    }
                    Node newNode = new Node(newNodePos, currentNode, currentNode.G + travelCost[i % 2]);
                    //Checks the open list for nodes with the same position and returns null if nothing is found
                    if (openList.ContainsKey(newNode.GetHashCode()))
                    {
                        //Changes an old node(if G > this.G)
                        if (openList[GetHashCode()].G > newNode.G)
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
