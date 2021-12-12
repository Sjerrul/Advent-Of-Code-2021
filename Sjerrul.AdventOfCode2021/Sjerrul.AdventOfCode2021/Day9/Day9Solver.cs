using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day1
{
    public class Day9Solver : SolverBase, ISolve
    {
        public Day9Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            int[][] grid = new int[this.Input.Count() + 2][];

            for (int i = 0; i < this.Input.Count(); i++)
            {
                string line = this.Input.ElementAt(i);
                line = $"9{line}9";

                if (i == 0)
                {
                    grid[i] = new String('9', line.Length).Select(x => int.Parse(x.ToString())).ToArray();
                }

                grid[i + 1] = line.Select(x => int.Parse(x.ToString())).ToArray();

                if (i == this.Input.Count() - 1)
                {
                    grid[i + 2] = new String('9', line.Length).Select(x => int.Parse(x.ToString())).ToArray();
                }
            }

            int riskLevelSum = 0;
            for (int y = 1; y < grid.GetLength(0) - 1; y++)
            {
                for (int x = 1; x < grid[y].GetLength(0) - 1; x++)
                {
                    int currentValue = grid[y][x];
                    if (currentValue < grid[y - 1][x] && currentValue < grid[y + 1][x] && currentValue < grid[y][x - 1] && currentValue < grid[y][x + 1])
                    {
                        riskLevelSum += currentValue + 1;
                        continue;
                    }
                }
            }

            Console.WriteLine($"Answer: {riskLevelSum}");
        }


        public async Task Part2()
        {
            int[][] grid = new int[this.Input.Count() + 2][];

            for (int i = 0; i < this.Input.Count(); i++)
            {
                string line = this.Input.ElementAt(i);
                line = $"9{line}9";

                if (i == 0)
                {
                    grid[i] = new String('9', line.Length).Select(x => int.Parse(x.ToString())).ToArray();
                }

                grid[i + 1] = line.Select(x => int.Parse(x.ToString())).ToArray();

                if (i == this.Input.Count() - 1)
                {
                    grid[i + 2] = new String('9', line.Length).Select(x => int.Parse(x.ToString())).ToArray();
                }
            }

            IList<(int, int)> lowestPpoints = new List<(int, int)>();
            for (int y = 1; y < grid.GetLength(0) - 1; y++)
            {
                for (int x = 1; x < grid[y].GetLength(0) - 1; x++)
                {
                    int currentValue = grid[y][x];
                    if (currentValue < grid[y - 1][x] && currentValue < grid[y + 1][x] && currentValue < grid[y][x - 1] && currentValue < grid[y][x + 1])
                    {
                        lowestPpoints.Add((x, y));
                        continue;
                    }
                }
            }
            IList<int> basinSizes = new List<int>();
            foreach (var start in lowestPpoints)
            {
                var result = Fill(grid, start.Item1, start.Item2);
                foreach (var point in result)
                {
                    RenderPoint(point.Item1, point.Item2, '0');
                    await Task.Delay(1);
                }
                basinSizes.Add(result.Count);
            }


            //IList<int> basinSizes = new List<int>();
            //IEnumerable<(int, int)> visited = new List<(int, int)>();
            //for (int y = 1; y < grid.GetLength(0) - 1; y++)
            //{
            //    for (int x = 1; x < grid[y].GetLength(0) - 1; x++)
            //    {
            //        if (visited.Contains((x, y)))
            //        {
            //            continue;
            //        }

            //        if (grid[y][x] == 9)
            //        {
            //            continue;
            //        };

            //        var result = Fill(grid, x, y);
            //        visited = visited.Concat(result.Distinct());

            //        foreach (var point in visited)
            //        {
            //            RenderPoint(point.Item1, point.Item2, 'X');
            //            await Task.Delay(1);
            //        }
            //        basinSizes.Add(result.Count);
            //    }
            //}

            var biggest = basinSizes.OrderByDescending(x => x).Take(3);
            Console.WriteLine($"Answer: {biggest.ElementAt(0) * biggest.ElementAt(1) * biggest.ElementAt(2)}");
        }

        public IList<(int, int)> Fill(int[][] array, int x, int y)
        {
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((x, y));

            IList<(int, int)> list = new List<(int, int)>();
            while (queue.Any())
            {
                (int, int) point = queue.Dequeue();
                if (!list.Contains(point))
                {
                    list.Add(point);

                    EnqueueIfMatches(array, queue, point.Item1 - 1, point.Item2, list);
                    EnqueueIfMatches(array, queue, point.Item1 + 1, point.Item2, list);
                    EnqueueIfMatches(array, queue, point.Item1, point.Item2 - 1, list);
                    EnqueueIfMatches(array, queue, point.Item1, point.Item2 + 1, list);
                }
               
            }

            return list;
        }

        private void EnqueueIfMatches(int[][] array, Queue<(int, int)> queue, int x, int y, IList<(int, int)> visited)
        {
            if (array[y][x] != 9 && !visited.Contains((x, y)))
                queue.Enqueue((x, y));
        }

        private void RenderPoint(int x, int y, char c)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }
    }
}
