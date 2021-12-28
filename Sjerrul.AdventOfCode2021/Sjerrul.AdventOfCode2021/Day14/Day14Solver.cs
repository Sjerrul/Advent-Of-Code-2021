using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day14
{
    public class Day14Solver : SolverBase, ISolve
    {
        public Day14Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            string template = this.Input.First();


            IDictionary<string, string> transformations = new Dictionary<string, string>();
            for (int i = 2; i < this.Input.Count(); i++)
            {
                var transformation = this.Input.ElementAt(i).Split(" -> ");
                transformations.Add(transformation[0], transformation[1]);
            }

            string result = template;
            for (int i = 0; i < 10; i++)
            {
                result = Polymerize(result, transformations);
            }

            IDictionary<char, int> counts = new Dictionary<char, int>();
            foreach (var c in result)
            {
                if (!counts.ContainsKey(c))
                {
                    counts.Add(c, 0);
                }

                counts[c]++;
            }

            Console.WriteLine($"Answer: {counts.Values.OrderByDescending(x => x).First() - counts.Values.OrderByDescending(x => x).Last()}");
        }


        public async Task Part2()
        {
            string template = this.Input.First();

            IDictionary<string, string> transformations = new Dictionary<string, string>();
            for (int i = 2; i < this.Input.Count(); i++)
            {
                var transformation = this.Input.ElementAt(i).Split(" -> ");
                transformations[transformation[0]] = transformation[1];
            }

            IDictionary<string, long> pairs = new Dictionary<string, long>();
            for (int i = 0; i < template.Length - 1; i++)
            {
                string pair = $"{template[i]}{template[i + 1]}";
                if (!pairs.ContainsKey(pair))
                {
                    pairs[pair] = 0;
                }

                pairs[pair] += 1;
            }

            for (int i = 0; i < 40; i++)
            {
                IDictionary<string, long> newPairs = new Dictionary<string, long>();
                foreach (var pair in pairs)
                {
                    string insert = transformations[pair.Key];
                    string newPair1 = $"{pair.Key[0]}{insert}";
                    string newPair2 = $"{insert}{pair.Key[1]}";

                    if (!newPairs.ContainsKey(newPair1))
                    {
                        newPairs[newPair1] = pair.Value;
                    }

                    newPairs[newPair1] += 1;

                    if (!newPairs.ContainsKey(newPair2))
                    {
                        newPairs[newPair2] = pair.Value;
                    }

                    newPairs[newPair2] += 1;
                }

                pairs.Clear();
                foreach (var p in newPairs)
                {
                    pairs[p.Key] = p.Value;
                }
            }

            IDictionary<char, long> counts = new Dictionary<char, long>();
            foreach (var p in pairs)
            {
                if (!counts.ContainsKey(p.Key.First()))
                {
                    counts.Add(p.Key.First(), p.Value);
                }
                else
                {
                    counts[p.Key.First()] += p.Value;
                }

                counts[p.Key.First()]++;


                if (!counts.ContainsKey(p.Key.Last()))
                {
                    counts.Add(p.Key.Last(), p.Value);
                }
                else
                {
                    counts[p.Key.Last()] += p.Value;
                }
            }


            //Console.WriteLine($"Answer: {counts.Values.OrderByDescending(x => x).First() - counts.Values.OrderByDescending(x => x).Last()}");
        }

        private static string Polymerize(string template, IDictionary<string, string> transformations)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < template.Length - 1; i++)
            {
                string pair = template.Substring(i, 2);
                string insert = transformations[pair];

                result.Append($"{pair.First()}{insert}");
            }
            result.Append(template.Last());
            return result.ToString();
        }

    }
}
