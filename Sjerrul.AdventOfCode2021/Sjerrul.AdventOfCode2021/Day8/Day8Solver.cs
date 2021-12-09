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
    public class Day8Solver : SolverBase, ISolve
    {
        public Day8Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            var matchingOutputs = this.Input.Select(line => line.Split(" | "))
                .Select(parts => parts[1])
                .Select(outputs => outputs.Split(' '))
                .Select(x => x.Where(o => o.Length == 2 || o.Length == 3 || o.Length == 4 || o.Length == 7))
                .SelectMany(x => x)
                .Count();

            Console.WriteLine($"Answer: {matchingOutputs}");

        }

        public async Task Part2()
        {
            int sum = 0;
            foreach (var line in this.Input)
            {
                sum += FindNumber(line);
            }

            Console.WriteLine($"Answer: {sum}");
        }


        public int FindNumber(string line)
        {
            var parts = line.Split(" | ");
            IList<string> inputs = parts[0].Split(" ").ToList();
            IList<string> outputs = parts[1].Split(" ").ToList();


            IDictionary<string, string> decoded = new Dictionary<string, string>();

            decoded.Add("1", inputs.Single(x => x.Length == 2));
            decoded.Add("4", inputs.Single(x => x.Length == 4));
            decoded.Add("7", inputs.Single(x => x.Length == 3));
            decoded.Add("8", inputs.Single(x => x.Length == 7));

            // Missing 2, 3, 5, 6, 9, 0
            IList<string> inputsOfLengthFive = inputs.Where(x => x.Length == 5).ToList(); // 2, 3, 5
            IList<string> inputsOfLengthSix = inputs.Where(x => x.Length == 6).ToList(); // 6, 9, 0


            foreach (var input in inputsOfLengthSix)
            {
                // 6 has only one overlapping with 1
                var overlappingSegments = GetOVerlappingSegments(input, decoded["1"]);
                if (overlappingSegments.Count == 1)
                {
                    decoded.Add("6", input);
                    inputsOfLengthSix.Remove(input);
                    break;
                }
            }

            // Missing 2, 3, 5, 9, 0
            foreach (var input in inputsOfLengthSix)
            {
                // 4 has four overlapping with 9
                var overlappingSegments = GetOVerlappingSegments(input, decoded["4"]);
                if (overlappingSegments.Count == 4)
                {
                    decoded.Add("9", input);
                    inputsOfLengthSix.Remove(input);
                    break;
                }
            }

            // Missing 2, 3, 5, 0
            decoded.Add("0", inputsOfLengthSix.Single());

            // Missing 2, 3, 5
            foreach (var input in inputsOfLengthFive)
            {
                // 5 has five overlapping with 6
                var overlappingSegments = GetOVerlappingSegments(input, decoded["6"]);
                if (overlappingSegments.Count == 5)
                {
                    decoded.Add("5", input);
                    inputsOfLengthFive.Remove(input);
                    break;
                }
            }

            // Missing 2, 3
            foreach (var input in inputsOfLengthFive)
            {
                // 3 has two overlapping with 1
                var overlappingSegments = GetOVerlappingSegments(input, decoded["1"]);
                if (overlappingSegments.Count == 2)
                {
                    decoded.Add("3", input);
                    inputsOfLengthFive.Remove(input);
                    break;
                }
            }

            // Missing 2
            decoded.Add("2", inputsOfLengthFive.Single());


            // All values known    
            string number = string.Empty;
            foreach (var output in outputs)
            {
                string sortedOutput = String.Concat(output.OrderBy(c => c));

                var decodedNumber = decoded.Single(x => String.Concat(x.Value.OrderBy(c => c)).Equals(sortedOutput));
                number = $"{number}{decodedNumber.Key}";
            }


            return int.Parse(number);
        }


        public IList<char> GetOVerlappingSegments(string input1, string input2)
        {
            char[] chars1 = input1.ToCharArray();
            char[] chars2 = input2.ToCharArray();


            IList<char> overlapping = new List<char>();
            for (int i = 0; i < chars1.Length; i++)
            {
                if (chars2.Contains(chars1[i]))
                {
                    overlapping.Add(chars1[i]);
                }
            }
            return overlapping;

        }
    }
}
