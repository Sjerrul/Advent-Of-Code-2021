using Sjerrul.AdventOfCode2021.Core;
using Sjerrul.AdventOfCode2021.Day1;
using System;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ISolve solver = new Day12Solver("Day12/Input.txt");
            await solver.Part1();
            Console.ReadKey();

            await solver.Part2();
            Console.ReadKey();
        }
    }
}
