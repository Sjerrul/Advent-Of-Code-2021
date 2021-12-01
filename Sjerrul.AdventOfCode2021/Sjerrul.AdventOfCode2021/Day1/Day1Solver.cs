using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day1
{
    public class Day1Solver : ISolve
    {
        private readonly string inputPath;

        public Day1Solver(string inputPath)
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
            IEnumerable<int> depths = lines.Select(x => int.Parse(x));

            int increasing = 0;
            int decreasing = 0;
            for (int i = 1; i < depths.Count(); i++)
            {
                if (depths.ElementAt(i) > depths.ElementAt(i - 1))
                {
                    increasing++;
                    continue;
                }

                decreasing++;
            }

            Console.WriteLine($"Increasing depths: {increasing}");
        }

        public async Task Part2()
        {
            var lines = await File.ReadAllLinesAsync(this.inputPath);
            IEnumerable<int> depths = lines.Select(x => int.Parse(x));

            IList<int> slidingDepths = new List<int>();
            int increasing = 0;
            int decreasing = 0;
            for (int i = 0; i < depths.Count() - 2; i++)
            {
                int sum = depths.ElementAt(i) + depths.ElementAt(i + 1) + depths.ElementAt(i + 2);
                slidingDepths.Add(sum);
            }

            for (int i = 1; i < slidingDepths.Count(); i++)
            {
                if (slidingDepths.ElementAt(i) > slidingDepths.ElementAt(i - 1))
                {
                    increasing++;
                    continue;
                }

                decreasing++;
            }


            Console.WriteLine($"Increasing depths: {increasing}");
        }
    }
}
