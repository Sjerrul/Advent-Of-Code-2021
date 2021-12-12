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
    public class Day10Solver : SolverBase, ISolve
    {
        public Day10Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            int total = 0;
            foreach (var line in this.Input)
            {
                int points = 0;

                Stack<char> stack = new Stack<char>();
                foreach (var c in line)
                {
                    if (c == '[' || c == '(' || c == '<' || c == '{')
                    {
                        stack.Push(c);
                        continue;
                    }

                    char topOfStack = stack.Pop();
                    if (topOfStack == '[' && c != ']')
                    {
                        points += GetScore(c);
                        break;
                    }

                    if (topOfStack == '<' && c != '>')
                    {
                        points += GetScore(c);
                        break;
                    }

                    if (topOfStack == '{' && c != '}')
                    {
                        points += GetScore(c);
                        break;
                    }

                    if (topOfStack == '(' && c != ')')
                    {
                        points += GetScore(c);
                        break;
                    }

                }

                total += points;
            }

            Console.WriteLine($"Answer: {total}");
        }
        


        public async Task Part2()
        {
            int total = 0;
            IList<string> incompleteLines = new List<string>();
            foreach (var line in this.Input)
            {
                bool corrupted = false;

                Stack<char> stack = new Stack<char>();
                foreach (var c in line)
                {
                    if (c == '[' || c == '(' || c == '<' || c == '{')
                    {
                        stack.Push(c);
                        continue;
                    }

                    char topOfStack = stack.Pop();
                    if ((topOfStack == '[' && c != ']') || (topOfStack == '<' && c != '>') || (topOfStack == '{' && c != '}') || (topOfStack == '(' && c != ')'))
                    {
                        corrupted = true;
                        break;
                    }
                }

                if (!corrupted)
                {
                    incompleteLines.Add(line);
                }
            }

            IList<long> scores = new List<long>();
            foreach (var line in incompleteLines)
            {
                Stack<char> stack = new Stack<char>();
                long score = 0;
                foreach (var c in line)
                {
                    if (c == '[' || c == '(' || c == '<' || c == '{')
                    {
                        stack.Push(c);
                        continue;
                    }

                    char topOfStack = stack.Pop();
                }

                while (stack.TryPop(out char openings))
                {
                    if (openings == '[')
                    {
                        score *= 5;
                        score += 2;
                    }

                    if (openings == '<')
                    {
                        score *= 5;
                        score += 4;
                    }

                    if (openings == '{')
                    {
                        score *= 5;
                        score += 3;

                    }

                    if (openings == '(')
                    {
                        score *= 5;
                        score += 1;

                    }
                }

                scores.Add(score);
            }

            var orderedScores = scores.OrderBy(x => x);
            Console.WriteLine($"Answer: {scores[scores.Count / 2]}");
        }

        private int GetScore(char c)
        {
            switch (c)
            {
                case ')': return 3;
                case ']': return 57;
                case '}': return 1197;
                case '>': return 25137;
                default: throw new InvalidOperationException();
            }
        }
    }
}
