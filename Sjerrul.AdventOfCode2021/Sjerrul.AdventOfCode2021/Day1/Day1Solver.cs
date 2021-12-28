using Sjerrul.AdventOfCode2021.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day1
{
    public class Day1Solver : SolverBase, ISolve
    {
        private Renderer renderer;

        public Day1Solver(string inputPath) : base(inputPath)
        {
            this.renderer = new Renderer(true);
        }

        public async Task Part1()
        {
            IEnumerable<int> depths = this.Input.Select(x => int.Parse(x));

            int increasing = GetIncreasing(depths);
        }

        public async Task Part2()
        {
            IEnumerable<int> depths = this.Input.Select(x => int.Parse(x));

            IList<int> slidingDepths = new List<int>();
            for (int i = 0; i < depths.Count() - 2; i++)
            {
                int sum = depths.ElementAt(i) + depths.ElementAt(i + 1) + depths.ElementAt(i + 2);
                slidingDepths.Add(sum);
            }

            int increasing = GetIncreasing(slidingDepths);
        }

        private int GetIncreasing(IEnumerable<int> depths)
        {
            int increasing = 0;
            for (int i = 1; i < depths.Count(); i++)
            {
                if (depths.ElementAt(i) > depths.ElementAt(i - 1))
                {
                    increasing++;
                }

                this.renderer.Tick(increasing);
            }

            return increasing;
        }
    }
}
