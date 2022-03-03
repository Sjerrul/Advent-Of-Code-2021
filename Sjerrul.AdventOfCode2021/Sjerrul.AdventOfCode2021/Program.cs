using Sjerrul.AdventOfCode2021.Core;
using Sjerrul.AdventOfCode2021.Day15;
using Sjerrul.AdventOfCode2021.Day21;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sjerrul.AdventOfCode2021.Day1;
using Sjerrul.AdventOfCode2021.Day10;
using Sjerrul.AdventOfCode2021.Day11;
using Sjerrul.AdventOfCode2021.Day12;
using Sjerrul.AdventOfCode2021.Day13;
using Sjerrul.AdventOfCode2021.Day14;
using Sjerrul.AdventOfCode2021.Day16;
using Sjerrul.AdventOfCode2021.Day2;
using Sjerrul.AdventOfCode2021.Day3;
using Sjerrul.AdventOfCode2021.Day4;
using Sjerrul.AdventOfCode2021.Day5;
using Sjerrul.AdventOfCode2021.Day6;
using Sjerrul.AdventOfCode2021.Day7;
using Sjerrul.AdventOfCode2021.Day8;
using Sjerrul.AdventOfCode2021.Day9;

namespace Sjerrul.AdventOfCode2021
{
    class Program
    {
        private static IDictionary<int, Expression<Func<ISolve>>> solvers = new Dictionary<int, Expression<Func<ISolve>>>
        {
            {1, () => new Day1Solver("Day1/Input.txt")},
            {2, () => new Day2Solver("Day2/Input.txt")},
            {3, () => new Day3Solver("Day3/Input.txt")},
            {4, () => new Day4Solver("Day4/Input.txt")},
            {5, () => new Day5Solver("Day5/Input.txt")},
            {6, () => new Day6Solver("Day6/Input.txt")},
            {7, () => new Day7Solver("Day7/Input.txt")},
            {8, () => new Day8Solver("Day8/Input.txt")},
            {9, () => new Day9Solver("Day9/Input.txt")},
            {10, () => new Day10Solver("Day10/Input.txt")},
            {11, () => new Day11Solver("Day11/Input.txt")},
            {12, () => new Day12Solver("Day12/Input.txt")},
            {13, () => new Day13Solver("Day13/Input.txt")},
            {14, () => new Day14Solver("Day14/Input.txt")},
            {15, () => new Day15Solver("Day15/Input.txt")},
            {16, () => new Day16Solver("Day16/Input.txt")},
            {21, () => new Day21Solver("Day21/Input.txt")}
        };
        
        static async Task Main()
        {
            ISolve solver = PromptForSolver();
            
            await solver.Part1();
            Console.ReadKey();

            await solver.Part2();
            Console.ReadKey();
        }

        static ISolve PromptForSolver()
        {
            while (true)
            {
                Console.WriteLine("Select AOC day (1-24) to load, or no number for latest");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    var latestSolver = solvers.OrderByDescending(x => x.Key).First().Value.Compile();
                    return latestSolver();
                }

                if (!int.TryParse(input, out int day))
                {
                    Console.WriteLine($"You input '{input}' was not a valid number, try again");
                    continue;
                }

                if (!solvers.Keys.Contains(day))
                {
                    Console.WriteLine($"Day '{day}' is not available, try again");
                    continue;
                }

                var solver = solvers[day].Compile();
                return solver();
            }
        }
    }
}
