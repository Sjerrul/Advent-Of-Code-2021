using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day6
{
    public class Day6Solver : SolverBase, ISolve
    {
        public Day6Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            IList<Fish> fish = Input.First().Split(',').Select(x => new Fish(int.Parse(x))).ToList();

            int days = 1;
            while (days <= 80)
            {
                int numberOfCurrentFish = fish.Count;
                for (int i = 0; i < numberOfCurrentFish; i++)
                {
                    if (fish[i].Age == 0)
                    {
                        fish[i].Age = 6;
                        fish.Add(new Fish(8));
                    }
                    else
                    {
                        fish[i].Age--;
                    }
                }

                days++;
            }

            Console.WriteLine($"Answer: {fish.Count}");
        }

        public async Task Part2()
        {
            long[] fish = new long[10];

            IEnumerable<int> timers = Input.First().Split(',').Select(x => int.Parse(x)).ToList();
            foreach (var timer in timers)
            {
                fish[timer]++;
            }

            int days = 1;
            while (days <= 256)
            {
                for (int a = 0; a < fish.Length; a++)
                {
                    if (a == 0)
                    {
                        fish[9] = fish[0];
                        fish[7] += fish[0];
                        fish[0] = 0;
                        continue;
                    }

                    fish[a - 1] = fish[a];
                    fish[a] = 0;
                }

                days++;
            }

            Console.WriteLine($"Answer: {fish.Sum(x => x)}");
        }

    }
}
