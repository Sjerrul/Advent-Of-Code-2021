using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day5
{
    public class Day5Solver : SolverBase, ISolve
    {
        public Day5Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            int minX = int.MaxValue;
            int minY = int.MaxValue;

            IList<(Point start, Point end)> vents = new List<(Point start, Point end)>();
            foreach (var line in this.Input)
            {
                var parts = line.Split(" -> ");

                var startCoordinates = parts[0].Split(',');
                var endCoordinates = parts[1].Split(',');
                Point start = new Point(int.Parse(startCoordinates[0]), int.Parse(startCoordinates[1]));
                Point end = new Point(int.Parse(endCoordinates[0]), int.Parse(endCoordinates[1]));

                maxX = Math.Max(Math.Max(maxX, start.X), end.X);
                maxY = Math.Max(Math.Max(maxY, start.Y), end.Y);
                minX = Math.Min(Math.Min(minX, start.X), end.X);
                minY = Math.Min(Math.Min(minY, start.Y), end.Y);


                vents.Add((start, end));
            }


            int[,] grid = new int[maxX + 1, maxY + 1];

            foreach (var ventLine in vents)
            {
                if (ventLine.start.X == ventLine.end.X)
                {
                    int max = Math.Max(ventLine.start.Y, ventLine.end.Y);
                    int min = Math.Min(ventLine.start.Y, ventLine.end.Y);

                    for (int y = min; y <= max; y++)
                    {
                        grid[ventLine.start.X, y]++;
                    }

                    continue;
                }

                if (ventLine.start.Y == ventLine.end.Y)
                {
                    int max = Math.Max(ventLine.start.X, ventLine.end.X);
                    int min = Math.Min(ventLine.start.X, ventLine.end.X);
                    for (int x = min; x <= max; x++)
                    {
                        grid[x, ventLine.start.Y]++;
                    }
                }

            }

            var explodedGrid = grid.Cast<int>();
            Console.WriteLine($"Answer: {explodedGrid.Count(x => x > 1)}");
        }


        public async Task Part2()
        {
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            int minX = int.MaxValue;
            int minY = int.MaxValue;

            IList<(Point start, Point end)> vents = new List<(Point start, Point end)>();
            foreach (var line in this.Input)
            {
                var parts = line.Split(" -> ");

                var startCoordinates = parts[0].Split(',');
                var endCoordinates = parts[1].Split(',');
                Point start = new Point(int.Parse(startCoordinates[0]), int.Parse(startCoordinates[1]));
                Point end = new Point(int.Parse(endCoordinates[0]), int.Parse(endCoordinates[1]));

                maxX = Math.Max(Math.Max(maxX, start.X), end.X);
                maxY = Math.Max(Math.Max(maxY, start.Y), end.Y);
                minX = Math.Min(Math.Min(minX, start.X), end.X);
                minY = Math.Min(Math.Min(minY, start.Y), end.Y);


                vents.Add((start, end));
            }


            int[,] grid = new int[maxX + 1, maxY + 1];
            foreach (var ventLine in vents)
            {
                if (ventLine.start.X == ventLine.end.X)
                {
                    int max = Math.Max(ventLine.start.Y, ventLine.end.Y);
                    int min = Math.Min(ventLine.start.Y, ventLine.end.Y);

                    for (int y = min; y <= max; y++)
                    {
                        grid[ventLine.start.X, y]++;
                    }

                    continue;
                }

                if (ventLine.start.Y == ventLine.end.Y)
                {
                    int max = Math.Max(ventLine.start.X, ventLine.end.X);
                    int min = Math.Min(ventLine.start.X, ventLine.end.X);
                    for (int x = min; x <= max; x++)
                    {
                        grid[x, ventLine.start.Y]++;
                    }
                    continue;
                }

                int diagX = ventLine.start.X;
                int diagY = ventLine.start.Y;
                int steps = 0;
                if (ventLine.start.X < ventLine.end.X)
                {
                    if (ventLine.start.Y < ventLine.end.Y)
                    {
                        // Right/Down
                        while (steps <= ventLine.end.X - ventLine.start.X)
                        {
                            grid[diagX, diagY]++;
                            diagX++;
                            diagY++;
                            steps++;
                        }
                    }
                    else
                    {
                        // Right/Up
                        while (steps <= ventLine.end.X - ventLine.start.X)
                        {
                            grid[diagX, diagY]++;
                            diagX++;
                            diagY--;
                            steps++;
                        }
                    }
                }
                else
                {
                    if (ventLine.start.Y < ventLine.end.Y)
                    {
                        // Left/Down
                        while (steps <= ventLine.start.X - ventLine.end.X)
                        {
                            grid[diagX, diagY]++;
                            diagX--;
                            diagY++;
                            steps++;
                        }
                    }
                    else
                    {
                        // Left/Up
                        while (steps <= ventLine.start.X - ventLine.end.X)
                        {
                            grid[diagX, diagY]++;
                            diagX--;
                            diagY--;
                            steps++;
                        }
                    }

                }
            }

            var explodedGrid = grid.Cast<int>();
            Console.WriteLine($"Answer: {explodedGrid.Count(x => x > 1)}");
        }

    }
}
