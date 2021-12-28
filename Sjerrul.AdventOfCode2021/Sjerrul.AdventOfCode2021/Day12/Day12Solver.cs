using Konsole;
using Sjerrul.AdventOfCode2021.Core;
using Sjerrul.AdventOfCode2021.Day12;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day12
{
    public class Day12Solver : SolverBase, ISolve
    {
        private readonly IConsole nodesWindow;
        private readonly IConsole linesWindow;
        private readonly IConsole answerWindow;
        private readonly IConsole instructionsWindow;

        private int pathCount;

        public Day12Solver(string inputPath) : base(inputPath)
        {
            Console.CursorVisible = false;

            this.nodesWindow = Window.OpenBox("Nodes", 200, 3);
            this.linesWindow = Window.OpenBox("Processing", 0, 6, 100, 20);
            this.instructionsWindow = Window.OpenBox("Instructions", 105, 6, 95, 20);
            this.answerWindow = Window.OpenBox("Answer", 200, 3);
        }

        public async Task Part1()
        {
            Node startNode = null;
            IList<Node> createdNodes = new List<Node>();

            foreach (var line in this.Input)
            {
                var caves = line.Split('-');

                Node begin = createdNodes.SingleOrDefault(x => x.Name == caves[0]);
                Node end = createdNodes.SingleOrDefault(x => x.Name == caves[1]);

                if (begin == null)
                {
                    begin = new Node
                    {
                        Name = caves[0]
                    };

                    createdNodes.Add(begin);
                }

                if (end == null)
                {
                    end = new Node
                    {
                        Name = caves[1]
                    };

                    createdNodes.Add(end);
                }

                end.Neighbours.Add(begin);
                begin.Neighbours.Add(end);

                if (begin.Name == "start" && startNode == null)
                {
                    startNode = begin;
                }
            }

            pathCount = 1;

            RenderNodes(this.nodesWindow, createdNodes);
            RenderInstructions(this.instructionsWindow, this.Input);
            Traverse("end", new List<Node>
            {
                startNode
            }, startNode);
        }

        public async Task Part2()
        {
            Node startNode = null;
            IList<Node> createdNodes = new List<Node>();

            foreach (var line in this.Input)
            {
                var caves = line.Split('-');

                Node begin = createdNodes.SingleOrDefault(x => x.Name == caves[0]);
                Node end = createdNodes.SingleOrDefault(x => x.Name == caves[1]);

                if (begin == null)
                {
                    begin = new Node
                    {
                        Name = caves[0]
                    };

                    createdNodes.Add(begin);
                }

                if (end == null)
                {
                    end = new Node
                    {
                        Name = caves[1]
                    };

                    createdNodes.Add(end);
                }

                end.Neighbours.Add(begin);
                begin.Neighbours.Add(end);

                if (begin.Name == "start" && startNode == null)
                {
                    startNode = begin;
                }
            }

            pathCount = 1;

            RenderNodes(this.nodesWindow, createdNodes);
            RenderInstructions(this.instructionsWindow, this.Input);
            TraverseWithSmallCavesLimit("end", new List<Node>
            {
                startNode
            }, startNode);
        }

        private Node Find(string toFind, IList<Node> visited, Node node)
        {
            if (node == null)
            {
                return null;
            }

            visited.Add(node);
            if (node.Name == toFind)
            {
                return node;
            }

            foreach (var neighbour in node.Neighbours)
            {
                if (visited.Select(x => x.Name).Contains(neighbour.Name))
                {
                    continue;
                }

                Node found = Find(toFind, visited, neighbour);
                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }

        private void Traverse(string toFind, IList<Node> visited, Node node)
        {
            if (node.Name == toFind)
            {
                RenderNodes(this.linesWindow, visited);
                RenderPathsFound(this.answerWindow, this.pathCount);
                pathCount++;
                return;
            }

            foreach (var neighbour in node.Neighbours)
            {
                if (neighbour.IsSmall && visited.Contains(neighbour))
                {
                    continue;
                }

                visited.Add(neighbour);
                Traverse(toFind, visited, neighbour);
                visited.RemoveAt(visited.Count() - 1);
            }
        }

        private void TraverseWithSmallCavesLimit(string toFind, IList<Node> visited, Node node)
        {
            if (node.Name == toFind)
            {
                RenderNodes(this.linesWindow, visited);
                RenderPathsFound(this.answerWindow, this.pathCount);
                pathCount++;
                return;
            }

            foreach (var neighbour in node.Neighbours)
            {
                if (neighbour.Name == "start")
                {
                    continue;
                }


                if (neighbour.IsSmall && visited.Contains(neighbour))
                {
                    if (IsSmallCaveLimitReached(visited))
                    {
                        continue;
                    }
                }

                visited.Add(neighbour);
                TraverseWithSmallCavesLimit(toFind, visited, neighbour);

                visited.RemoveAt(visited.Count() - 1);
            }
        }

        private bool IsSmallCaveLimitReached(IList<Node> visited)
        {
            var groupedSmallCaves = visited.GroupBy(x => x)
                            .Where(g => g.Count() > 1)
                            .Select(x => x.Key)
                            .Where(n => n.IsSmall)
                            .Count();

            return groupedSmallCaves == 1;
        }

        private void RenderNodes(IConsole window, IList<Node> nodes)
        {
            window.WriteLine(string.Join(' ', nodes.Select(x => x.Name)));
        }

        private void RenderPathsFound(IConsole window, int pathsFound)
        {
            window.WriteLine($"{pathsFound}");
        }

        private void RenderInstructions(IConsole window, IEnumerable<string> input)
        {
            foreach (var instuction in input)
            {
                window.WriteLine(instuction);
            }
        }
    }
}
