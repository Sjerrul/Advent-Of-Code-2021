using Konsole;
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
        private IConsole map;
        private IConsole answers;

        public Day15Solver(string inputPath) : base(inputPath)
        {
            this.map = Window.OpenBox("Map", 110, 80);
            this.answers = Window.OpenBox("Answers", 110, 3);
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

            // DEfine start and end nodes
            (int x, int y) start = (0, 0);
            (int x, int y) end = (grid[0].GetLength(0) - 1, grid.GetLength(0) - 1);

            // Add starting node to open
            open.Add(grid.SelectMany(g => g).Single(n => n.X == start.x && n.Y == start.y));

            RenderGrid(this.Input);

            // While open is not empty
            while (open.Any())
            {
                // Get the current node with lowest F
                Node current = open.OrderBy(x => x.F).First();
                RenderCurrent(current);

                // remove the currentNode from the openList
                // add the currentNode to the closedList
                open.Remove(current);
                closed.Add(current);

                // Check for goal
                if (current.X == end.x && current.Y == end.y)
                {
                    // Current node is the goal node. Backtrack through parants to find the path
                    RenderGrid(this.Input);
                    RenderPath(current);

                    int cost = Backtrack(grid, current);
                    answers.WriteLine($"Answer Part 1: {cost}");
                    break;
                }

                // Get children of current node
                IList<Node> children = Get4Children(grid, current);
                foreach (var child in children)
                {
                    if (closed.Contains(child, new NodeComparer()))
                    {
                        // Node already visited, do nothing
                        continue;
                    }

                    if (open.Contains(child, new NodeComparer()))
                    {
                        foreach (var openNode in open)
                        {
                            if ((openNode.X == child.X && openNode.Y == child.Y) && child.G < openNode.G)
                            {
                                // this node has a lower G cost than the current node, set its parent to current
                                openNode.Parent = current;
                            }
                        }
                    }
                    else
                    {
                        child.G = current.G + child.Value; // Cost of current to child
                       
                        // Cost function, this is a low estimate using pythagros, other options are manhattan-distance or 0 (for Dijkstra)
                        child.H = (int)Math.Sqrt(Math.Pow(end.x - child.X, 2) + Math.Pow(end.y - child.Y, 2));
                        //child.H = Math.Abs(end.x - child.X) + Math.Abs(end.y - child.Y); // Estimation of current to end
                        //child.H = 0; //Dijkstra

                        child.Parent = current;
                        open.Add(child);
                    }

                }
            }
        }

        public async Task Part2()
        {

        }

        private int Backtrack(Node[][] grid, Node current)
        {
            int cost = 0;
            while (current.Parent != null)
            {
                cost += current.Value;
                current = current.Parent;
            }

            return cost;
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

        private void RenderGrid(IEnumerable<string> input)
        {
            map.Clear();
            foreach (var line in input)
            {
                map.WriteLine(line);
            }
        }

        private void RenderPath(Node node)
        {
            while (node.Parent != null)
            {
                map.CursorLeft = node.X;
                map.CursorTop = node.Y;
                map.Write(ConsoleColor.Green, $"{node.Value}");

                node = node.Parent;
            }
        }

        private void RenderCurrent(Node node)
        {
            map.CursorLeft = node.X;
            map.CursorTop = node.Y;
            map.Write(ConsoleColor.Yellow, $"{node.Value}");
        }
    }
}
