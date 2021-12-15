using MoreLinq.Extensions;
using Sjerrul.AdventOfCode2021.Core;
using Sjerrul.AdventOfCode2021.Day15;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day1
{
    public class Day15Solver : SolverBase, ISolve
    {
        public Day15Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            Node[][] grid = new Node[this.Input.Count()][];

            for (int row = 0; row < this.Input.Count(); row++)
            {
                string line = this.Input.ElementAt(row);
                grid[row] = line.Select((x, i) => new Node
                {
                    X = i,
                    Y = row,
                    Value = int.Parse(x.ToString())

                }).ToArray();
            }

            // A*

            // Init open/closed lists
            IList<Node> open = new List<Node>();
            IList<Node> closed = new List<Node>();

            // Add starting node to open
            (int x, int y) start = (0, 0);
            (int x, int y) end = (grid[0].GetLength(0) - 1, grid.GetLength(0) - 1);

            open.Add(grid.SelectMany(g => g).Single(n => n.X == start.x && n.Y == start.y));

            // While open is not empty

            RenderGrid(grid);
            while (open.Any())
            {
                // Get the current node with lowest F
                Node current = open.OrderBy(x => x.F).First();

                //RenderGrid(grid);
                RenderPath(current);
                await Task.Delay(10);

                // remove the currentNode from the openList
                // add the currentNode to the closedList
                open.Remove(current);
                closed.Add(current);

                // Check for goal
                if (current.X == end.x && current.Y == end.y)
                {
                    current = Solve(grid, current);

                    break;
                }

                // Get children
                IList<Node> children = Get4Children(grid, current);

                foreach (var child in children)
                {
                    if (closed.Contains(child, new NodeComparer()))
                    {
                        continue;
                    }

                    // Create the f, g, and h values
                    int g = current.G + child.Value;
                    int h = Math.Abs(end.x - child.X) + Math.Abs(end.y - child.Y);
                    //int h = 0;
                    int f = g + h;

                    child.G = g;
                    child.H = h;
                    child.F = f;

                    foreach (var openNode in open)
                    {
                        if ((openNode.X == child.X && openNode.Y == child.Y) && (child.G > openNode.G))
                        {
                            break;
                        }
                    }


                    child.Parent = current;
                    open.Add(child);
                }
            }
        }

        private Node Solve(Node[][] grid, Node current)
        {
            RenderGrid(grid);
            RenderPath(current);

            int cost = 0;
            while (current.Parent != null)
            {
                cost += current.Value;
                current = current.Parent;
            }

            Console.SetCursorPosition(0, grid.Length + 2);
            Console.WriteLine($"Cost: { cost}");
            // currentnode is goal, backtrack for path
            return current;
        }

        public class NodeComparer : IEqualityComparer<Node>
        {
            public bool Equals([AllowNull] Node x, [AllowNull] Node y)
            {
                return x.X == y.X && x.Y == y.Y;
            }

            public int GetHashCode([DisallowNull] Node obj)
            {
                return obj.X + obj.Y;
            }
        }

        public async Task Part2()
        {

        }

        private IList<Node> Get8Children(Node[][] grid, Node current)
        {
            IList<Node> children = new List<Node>();

            if (current.Y != 0 && current.X != 0)
            {
                children.Add(grid[current.Y - 1][current.X - 1]);
            }

            if (current.Y != 0 && current.X != grid[0].GetLength(0) - 1)
            {
                children.Add(grid[current.Y - 1][current.X + 1]);
            }

            if (current.Y != 0)
            {
                children.Add(grid[current.Y - 1][current.X]);
            }

            if (current.X != 0)
            {
                children.Add(grid[current.Y][current.X - 1]);
            }

            if (current.X != grid[0].GetLength(0) - 1)
            {
                children.Add(grid[current.Y][current.X + 1]);
            }

            if (current.Y != grid.GetLength(0) - 1 && current.X != 0)
            {
                children.Add(grid[current.Y + 1][current.X - 1]);
            }

            if (current.Y != grid.GetLength(0) - 1 && current.X != grid[0].GetLength(0) - 1)
            {
                children.Add(grid[current.Y + 1][current.X + 1]);
            }

            if (current.Y != grid.GetLength(0) - 1)
            {
                children.Add(grid[current.Y + 1][current.X]);
            }

            return children;
        }

        private IList<Node> Get4Children(Node[][] grid, Node current)
        {
            IList<Node> children = new List<Node>();

            if (current.Y != 0)
            {
                children.Add(grid[current.Y - 1][current.X]);
            }

            if (current.X != 0)
            {
                children.Add(grid[current.Y][current.X - 1]);
            }

            if (current.X != grid[0].GetLength(0) - 1)
            {
                children.Add(grid[current.Y][current.X + 1]);
            }

            if (current.Y != grid.GetLength(0) - 1)
            {
                children.Add(grid[current.Y + 1][current.X]);
            }

            return children;
        }

        private void RenderPath(Node node)
        {
            while (node.Parent != null)
            {
                RenderPoint(node, '.');
                node = node.Parent;
            }
        }

        private void RenderGrid(Node[][] grid)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid[y].GetLength(0); x++)
                {
                    RenderPoint(grid[y][x], grid[y][x].Value);
                }
            }
            Console.WriteLine();
        }

        private void RenderPoint(Node node, char value)
        {
            Console.SetCursorPosition(node.X, node.Y);
            Console.Write(value);
        }

        private void RenderPoint(Node node, int value)
        {
            Console.SetCursorPosition(node.X, node.Y);
            Console.Write(value);
        }
    }
}
