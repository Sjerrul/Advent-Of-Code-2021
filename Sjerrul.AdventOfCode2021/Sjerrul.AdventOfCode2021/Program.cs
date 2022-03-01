using Sjerrul.AdventOfCode2021.Core;
using Sjerrul.AdventOfCode2021.Day15;
using Sjerrul.AdventOfCode2021.Day21;
using System;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ISolve solver = new Day15Solver("Day15/Input.txt");
            await solver.Part1();
            Console.ReadKey();

            await solver.Part2();
            Console.ReadKey();
        }
    }
}
