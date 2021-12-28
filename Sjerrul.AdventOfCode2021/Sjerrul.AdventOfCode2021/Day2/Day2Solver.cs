using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day2
{
    public class Day2Solver : SolverBase, ISolve
    {
        public Day2Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            IEnumerable<(string command, int value)> instructions = this.Input
                .Select(i => i.Split(' '))
                .Select(s => (s[0], int.Parse(s[1])));

            int horizontalPosition = 0;
            int depth = 0;
            foreach (var (command, value) in instructions)
            {
                switch (command)
                {
                    case "forward":
                        horizontalPosition += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                    case "down":
                        depth += value;
                        break;
                }
            }

            Console.WriteLine($"Answer: {horizontalPosition * depth}");
        }

        public async Task Part2()
        {
            IEnumerable<(string command, int value)> instructions = this.Input
                .Select(i => i.Split(' '))
                .Select(s => (s[0], int.Parse(s[1])));

            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;
            foreach (var (command, value) in instructions)
            {
                switch (command)
                {
                    case "forward":
                        horizontalPosition += value;
                        depth += aim * value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                    case "down":
                        aim += value;
                        break;
                }
            }

            Console.WriteLine($"Answer: {horizontalPosition * depth}");
        }
    }
}
