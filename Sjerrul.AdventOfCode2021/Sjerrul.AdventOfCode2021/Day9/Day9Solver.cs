using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day1
{
    public class Day9Solver : SolverBase, ISolve
    {
        public Day9Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            var matchingOutputs = this.Input.Select(line => line.Split(" | ")).Select(parts => parts[1]).Select(outputs => outputs.Split(' ')).Where(o => o.Length == 2 || o.Length == 3 || o.Length == 4 || o.Length == 9);


            Console.WriteLine($"Answer: {matchingOutputs.Count()}");
        }

        public async Task Part2()
        {
          //  this.Input.Select(line => line.Split(" | ")).Select(parts => (parts[0], parts[1]))
            Console.WriteLine($"Answer: ");
        }

    }
}
