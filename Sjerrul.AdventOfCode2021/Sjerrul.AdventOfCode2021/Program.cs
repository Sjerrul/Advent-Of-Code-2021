using Sjerrul.AdventOfCode2021.Day1;
using System;

namespace Sjerrul.AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputParser = new Day1Parser();
            inputParser.ReadFile("Day1/Input.txt");
            inputParser.ParseFile();

            var solver = new Day1Solver();
            solver.CalculateAnswer(inputParser.ParsedFileContent);

            Console.WriteLine(solver.Answer);
        }
    }
}
