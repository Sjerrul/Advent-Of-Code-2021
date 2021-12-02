using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day1
{
    public class Day2Solver : ISolve
    {
        private readonly string inputPath;

        public Day2Solver(string inputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
            {
                throw new ArgumentException($"'{nameof(inputPath)}' cannot be null or whitespace", nameof(inputPath));
            }

            this.inputPath = inputPath;

        }

        public async Task Part1()
        {
            var lines = await File.ReadAllLinesAsync(this.inputPath);
            IEnumerable<(string Command, int Value)> instructions = lines
                .Select(i => i.Split(' '))
                .Select(s => (s[0], int.Parse(s[1])));


            int horizontalPosition = 0;
            int depth = 0;
            foreach (var instruction in instructions)
            {
                switch (instruction.Command)
                {
                    case "forward":
                        horizontalPosition += instruction.Value;
                        break;
                    case "up":
                        depth -= instruction.Value;
                        break;
                    case "down":
                        depth += instruction.Value;
                        break;
                }
            }

            Console.WriteLine($"Answer: {horizontalPosition * depth}");
        }

        public async Task Part2()
        {
            var lines = await File.ReadAllLinesAsync(this.inputPath);
            IEnumerable<(string Command, int Value)> instructions = lines
                .Select(i => i.Split(' '))
                .Select(s => (s[0], int.Parse(s[1])));


            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;
            foreach (var instruction in instructions)
            {
                switch (instruction.Command)
                {
                    case "forward":
                        horizontalPosition += instruction.Value;
                        depth += aim * instruction.Value;
                        break;
                    case "up":
                        aim -= instruction.Value;
                        break;
                    case "down":
                        aim += instruction.Value;
                        break;
                }
            }

            Console.WriteLine($"Answer: {horizontalPosition * depth}");
        }
    }
}
