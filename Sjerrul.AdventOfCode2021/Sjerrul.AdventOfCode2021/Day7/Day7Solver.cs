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
    public class Day7Solver : SolverBase, ISolve
    {
        public Day7Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            IEnumerable<int> positions = this.Input.First().Split(',').Select(x => int.Parse(x));

            int lowestPosition = positions.Min();
            int highestPosition = positions.Max();

            IList<(int position, int cost)> costs = new List<(int position, int cost)>();
            for (int desiredPosition = lowestPosition; desiredPosition <= highestPosition; desiredPosition++)
            {
                int cost = 0;
                foreach (var position in positions)
                {
                    cost += Math.Abs(desiredPosition - position);
                }

                costs.Add((desiredPosition, cost));
            }

            var lowestCost = costs.Min(x => x.cost);
            Console.WriteLine($"Answer: {costs.Single(x => x.cost == lowestCost)}");
        }

        public async Task Part2()
        {
            IEnumerable<int> positions = this.Input.First().Split(',').Select(x => int.Parse(x));

            int lowestPosition = positions.Min();
            int highestPosition = positions.Max();

            IList<(int position, int cost)> costs = new List<(int position, int cost)>();
            for (int desiredPosition = lowestPosition; desiredPosition <= highestPosition; desiredPosition++)
            {
                int cost = 0;
                foreach (var position in positions)
                {
                    int difference = Math.Abs(desiredPosition - position);
                    int calculatedCost = difference * (difference + 1) / 2;
                    cost += calculatedCost;
                }

                costs.Add((desiredPosition, cost));
            }

            var lowestCost = costs.Min(x => x.cost);
            Console.WriteLine($"Answer: {costs.Single(x => x.cost == lowestCost)}");
        }

    }
}
