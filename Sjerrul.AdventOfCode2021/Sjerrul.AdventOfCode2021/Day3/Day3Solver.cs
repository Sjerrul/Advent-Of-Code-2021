using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day3
{
    public class Day3Solver : SolverBase, ISolve
    {
        public Day3Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            int numberOfLines = this.Input.Count();
            int[] counts = GetCounts(this.Input);

            double gamma = 0;
            double epsylon = 0;

            for (int i = 0; i < counts.Length; i++)
            {
                if (numberOfLines - counts[i] <= counts[i])
                {
                    gamma += Math.Pow(2, counts.Length - (i + 1));
                }
                else
                {
                    epsylon += Math.Pow(2, counts.Length - (i + 1));
                }
            }
            Console.WriteLine($"Answer: {gamma * epsylon}");
        }

        public async Task Part2()
        {
            IList<string> filteredOxygenLines = Input.ToList();
            int i = 0;
            while (filteredOxygenLines.Count > 1)
            {
                int[] counts = GetCounts(filteredOxygenLines);
                bool onesMoreCommon = filteredOxygenLines.Count - counts[i] <= counts[i];
                if (onesMoreCommon)
                {
                    filteredOxygenLines = filteredOxygenLines.Where(l => l[i].Equals('1')).ToList();
                }
                else
                {
                    filteredOxygenLines = filteredOxygenLines.Where(l => l[i].Equals('0')).ToList();
                }
                i++;
            }

            string oxygenValue = filteredOxygenLines.Single();
            int oxygen = Convert.ToInt32(oxygenValue, 2);


            IList<string> filteredCO2Lines = Input.ToList();
            int j = 0;
            while (filteredCO2Lines.Count > 1)
            {
                int[] counts = GetCounts(filteredCO2Lines);
                bool onesMoreCommon = filteredCO2Lines.Count - counts[j] <= counts[j];
                if (onesMoreCommon)
                {
                    filteredCO2Lines = filteredCO2Lines.Where(l => l[j].Equals('0')).ToList();
                }
                else
                {
                    filteredCO2Lines = filteredCO2Lines.Where(l => l[j].Equals('1')).ToList();
                }
                j++;
            }

            string co2Value = filteredCO2Lines.Single();
            int co2 = Convert.ToInt32(co2Value, 2);
            Console.WriteLine($"Answer: {co2 * oxygen}");

        }

        private int[] GetCounts(IEnumerable<string> lines)
        {
            int[] counts = new int[lines.First().Length];
            foreach (var line in lines)
            {
                IEnumerable<int> chars = line.Select(c => c.Equals('0') ? 0 : 1);
                for (int i = 0; i < chars.Count(); i++)
                {
                    counts[i] += chars.ElementAt(i);
                }
            }

            return counts;
        }
    }
}
