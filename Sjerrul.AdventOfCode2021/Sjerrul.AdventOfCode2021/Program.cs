using Sjerrul.AdventOfCode2021.Core;
using Sjerrul.AdventOfCode2021.Day1;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ISolve solver = new Day2Solver("Day2/Input.txt");
            await solver.Part1();
            await solver.Part2();
        }
    }
}
