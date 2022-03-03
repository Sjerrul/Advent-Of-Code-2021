using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day11
{
    public class Day11Solver : SolverBase, ISolve
    {
        public Day11Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            int[][] grid = new int[this.Input.Count()][];

            for (int i = 0; i < this.Input.Count(); i++)
            {
                string line = this.Input.ElementAt(i);
                grid[i] = line.Select(x => int.Parse(x.ToString())).ToArray();
            }

            int flashes = 0;

            for (int i = 0; i < 100; i++)
            {
                flashes += Step(grid);
                await Task.Delay(60);
            }


            Console.WriteLine($"Answer: {flashes}");
        }

        public async Task Part2()
        {
            int[][] grid = new int[this.Input.Count()][];

            for (int i = 0; i < this.Input.Count(); i++)
            {
                string line = this.Input.ElementAt(i);
                grid[i] = line.Select(x => int.Parse(x.ToString())).ToArray();
            }

            int step = 0;
            do
            {
                Step(grid);
                await Task.Delay(10);
                step++;
            } while (!Synchronized(grid));

            Console.WriteLine();
            Console.WriteLine($"Answer: {step}");
        }

        private bool Synchronized(int[][] grid)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid[y].GetLength(0); x++)
                {
                    if (grid[y][x] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int Step(int[][] grid)
        {
            int flashes = 0;
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid[y].GetLength(0); x++)
                {
                    grid[y][x] += 1;
                }
            }


            bool hasFlash = false;
            do
            {
                hasFlash = false;
                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    for (int x = 0; x < grid[y].GetLength(0); x++)
                    {
                        if (grid[y][x] > 9)
                        {
                            if (y != 0 && x != 0)
                            {
                                grid[y - 1][x - 1] += 1;
                            }

                            if (y != 0 && x != grid[0].GetLength(0) - 1)
                            {
                                grid[y - 1][x + 1] += 1;
                            }

                            if (y != 0)
                            {
                                grid[y - 1][x] += 1;
                            }

                            if (x != 0)
                            {
                                grid[y][x - 1] += 1;
                            }

                            if (x != grid[0].GetLength(0) - 1)
                            {
                                grid[y][x + 1] += 1;
                            }

                            if (y != grid.GetLength(0) - 1 && x != 0)
                            {
                                grid[y + 1][x - 1] += 1;
                            }

                            if (y != grid.GetLength(0) - 1 && x != grid[0].GetLength(0) - 1)
                            {
                                grid[y + 1][x + 1] += 1;
                            }

                            if (y != grid.GetLength(0) - 1)
                            {
                                grid[y + 1][x] += 1;
                            }

                            hasFlash = true;
                            flashes++;
                            grid[y][x] = int.MinValue;
                        }
                    }
                }
            }
            while (hasFlash);

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid[y].GetLength(0); x++)
                {
                    if (grid[x][y] < 0)
                    {
                        grid[x][y] = 0;
                    }
                }
            }

            RenderGrid(grid);

            return flashes;
        }




        private void RenderGrid(int[][] grid)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid[y].GetLength(0); x++)
                {
                    RenderPoint(x, y, grid[y][x]);
                }
            }
            Console.WriteLine();
        }


        private void RenderPoint(int x, int y, int i)
        {
            Console.SetCursorPosition(x, y);

            if (i == 9)
            {
                Console.Write('0');
            }
            else if (i == 0)
            {
                Console.Write('.');
            }
            else
            {
                Console.Write(' ');
            }
        }
    }
}
