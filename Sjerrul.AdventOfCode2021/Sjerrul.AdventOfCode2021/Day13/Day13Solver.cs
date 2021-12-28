using Konsole;
using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day13
{
    public class Day13Solver : SolverBase, ISolve
    {
        private IConsole map;
        private IConsole answer;
        private IConsole instruction;

        public Day13Solver(string inputPath) : base(inputPath)
        {
            this.map = Window.OpenBox("Map", 110, 50);

            this.instruction = Window.OpenBox("Instruction", 110, 3);
            this.answer = Window.OpenBox("Answer", 110, 3);
        }

        public async Task Part1()
        {
            IList<(int x, int y)> coordinates = new List<(int, int)>();
            IList<(string axis, int position)> instructions = new List<(string, int)>();

            int maxX = int.MinValue;
            int maxY = int.MinValue;
            int minX = int.MaxValue;
            int minY = int.MaxValue;

            bool coordinatesDone = false;
            foreach (var line in this.Input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    coordinatesDone = true;
                    continue;
                }

                if (!coordinatesDone)
                {
                    var parts = line.Split(",");

                    int x = int.Parse(parts[0]);
                    int y = int.Parse(parts[1]);
                    coordinates.Add((x, y));

                    maxX = Math.Max(maxX, x);
                    maxY = Math.Max(maxY, y);
                    minX = Math.Min(minX, x);
                    minY = Math.Min(minY, y);


                    continue;
                }

                var i = line.Substring(11);

                var instructionParts = i.Split("=");
                instructions.Add((instructionParts[0], int.Parse(instructionParts[1])));
            }


            char[][] grid = new char[maxY + 1][];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                grid[i] = new char[maxX + 1];
            }

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid[0].GetLength(0); x++)
                {
                    grid[y][x] = '.';
                }
            }

            foreach (var c in coordinates)
            {
                grid[c.y][c.x] = '#';
            }

            var newGrid = grid;
            newGrid = Fold(newGrid, instructions.First());

            int count = 0;
            for (int y = 0; y < newGrid.GetLength(0); y++)
            {
                for (int x = 0; x < newGrid[0].GetLength(0); x++)
                {
                    if (newGrid[y][x] == '#')
                    {
                        count++;
                    }
                }
            }

            this.answer.WriteLine($"Number of dots: {count}");
        }

        private void RenderGrid(char[][] grid)
        {
            this.map.Clear();
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                StringBuilder line = new StringBuilder();
                for (int x = 0; x < grid[0].GetLength(0); x++)
                {
                    line.Append($"{grid[y][x]}");
                }
                this.map.WriteLine(line.ToString());
            }
        }

        private char[][] Fold(char[][] grid, (string axis, int position) instruction)
        {
            if (instruction.axis == "y")
            {
                int width = grid[0].GetLength(0);
                int newHeight = grid.GetLength(0) / 2;

                char[][] fold = new char[newHeight][];
                for (int i = 0; i < fold.GetLength(0); i++)
                {
                    fold[i] = new char[width];
                }


                for (int y = 0; y < newHeight; y++)
                {
                    for (int x = 0; x < grid[0].GetLength(0); x++)
                    {
                        fold[y][x] = grid[y][x];
                    }
                }



                for (int y = 0; y < newHeight; y++)
                {
                    for (int x = 0; x < grid[0].GetLength(0); x++)
                    {
                        if (grid[grid.GetLength(0) - 1 - y][x] != '.')
                        {
                            fold[y][x] = grid[grid.GetLength(0) - 1 - y][x];
                        }
                    }
                }


                return fold;
            }

            if (instruction.axis == "x")
            {
                int height = grid.GetLength(0);
                int newWidth = grid[0].GetLength(0) / 2;

                char[][] fold = new char[height][];
                for (int i = 0; i < fold.GetLength(0); i++)
                {
                    fold[i] = new char[newWidth];
                }


                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < newWidth; x++)
                    {
                        fold[y][x] = grid[y][x];
                    }
                }



                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < newWidth; x++)
                    {
                        if (grid[y][grid[0].GetLength(0) - 1 - x] != '.')
                        {
                            fold[y][x] = grid[y][grid[0].GetLength(0) - 1 - x];
                        }
                    }
                }


                return fold;
            }

            return null;
        }

        public async Task Part2()
        {
            IList<(int x, int y)> coordinates = new List<(int, int)>();
            IList<(string axis, int position)> instructions = new List<(string, int)>();

            int maxX = int.MinValue;
            int maxY = int.MinValue;
            int minX = int.MaxValue;
            int minY = int.MaxValue;

            bool coordinatesDone = false;
            foreach (var line in this.Input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    coordinatesDone = true;
                    continue;
                }

                if (!coordinatesDone)
                {
                    var parts = line.Split(",");

                    int x = int.Parse(parts[0]);
                    int y = int.Parse(parts[1]);
                    coordinates.Add((x, y));

                    maxX = Math.Max(maxX, x);
                    maxY = Math.Max(maxY, y);
                    minX = Math.Min(minX, x);
                    minY = Math.Min(minY, y);


                    continue;
                }

                var i = line.Substring(11);

                var instructionParts = i.Split("=");
                instructions.Add((instructionParts[0], int.Parse(instructionParts[1])));
            }


            char[][] grid = new char[maxY + 1][];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                grid[i] = new char[maxX + 1];
            }

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid[0].GetLength(0); x++)
                {
                    grid[y][x] = '.';
                }
            }

            foreach (var c in coordinates)
            {
                grid[c.y][c.x] = (char)9632;
            }

            RenderGrid(grid);

            var newGrid = grid;
            foreach (var i in instructions)
            {
                this.instruction.WriteLine($"Fold along {i.axis}-axis at {i.axis} = {i.position}");
                newGrid = Fold(newGrid, i);
                RenderGrid(newGrid);
            }
        }
    }
}
