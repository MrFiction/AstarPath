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
    public delegate void PathFinderDebugHandler(int x, int y, PathFinderNodeType type);
    public class Search
    {
        public event PathFinderDebugHandler PathFinderDebug;
        public static int cellSize { get; private set; }
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
            cellSize = _gridSize;
            grid = _grid;
        }
        public Search(sbyte[,] _grid, int _gridSize, byte[] _travelCost)
        {
            cellSize = _gridSize;
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
                    displayPath(closedList[endNode.GetHashCode()]);
                    return;
                }
                for (int i = 0; i < 8; i++)
                {
                    Point newNodePos = new Point(currentNode.position.X + direction[i, 0], currentNode.position.Y + direction[i, 1]);
                    Node newNode = new Node(newNodePos, currentNode, travelCost[i % 2]);
                    //outside the grid
                    if (newNodePos.X < 0 || newNodePos.Y < 0 || newNodePos.X > grid.GetUpperBound(0) || newNodePos.Y > grid.GetUpperBound(1))
                    {
                        continue;
                    }
                    //A wall(not walkable)
                    if (grid[newNodePos.X, newNodePos.Y] == -1)
                    {
                        continue;
                    }
                    //fount the parent node
                    if (currentNode.parentNode != null && newNodePos.Equals(currentNode.parentNode.position))
                    {
                        continue;
                    }
                    //node already checked and closed
                    if (closedList.ContainsKey(newNode.GetHashCode()))
                    {
                        continue;
                    }

                    //Checks the open list for nodes with the same position and returns null if nothing is found

                    if (openList.ContainsKey(newNode.GetHashCode()))
                    {
                        //Changes an old node(if G > this.G)
                        Node t = openList[newNode.GetHashCode()];
                        if (t.G > newNode.G)
                        {

                            openList[newNode.GetHashCode()] = newNode;
                        }
                    }
                    else
                    {
                        //Adds a new node
                        openList.Add(newNode.GetHashCode(), newNode);
                    }
                    if (!newNode.position.Equals(endNode.position))
                    {
                        PathFinderDebug(newNode.position.X, newNode.position.Y, PathFinderNodeType.Open);
                    }


                }

                closedList.Add(currentNode.GetHashCode(), currentNode);
                openList.Remove(currentNode.GetHashCode());

                if (!(currentNode.position.Equals(startingNode.position) || currentNode.position.Equals(endNode.position)))
                {
                    PathFinderDebug(currentNode.position.X, currentNode.position.Y, PathFinderNodeType.Close);
                }

            }

        }
        public void displayPath(Node endNode)
        {
            Node searchNode = endNode;
            while (searchNode.parentNode.parentNode != null)
            {
                PathFinderDebug(searchNode.parentNode.position.X, searchNode.parentNode.position.Y, PathFinderNodeType.Path);
                searchNode = searchNode.parentNode;
            }
        }
    }
}
