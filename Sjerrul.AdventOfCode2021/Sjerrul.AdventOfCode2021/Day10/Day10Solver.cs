using Konsole;
using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day1
{
    public class Day10Solver : SolverBase, ISolve
    {
        private IConsole lines;
        private IConsole answer;

        public Day10Solver(string inputPath) : base(inputPath)
        {
            this.lines = Window.OpenBox("Processing", 200, 20);
            this.answer = Window.OpenBox("Answer", 200, 3);
        }

        public async Task Part1()
        {
            int total = 0;
            foreach (var line in this.Input)
            {
                int points = 0;

                Stack<char> stack = new Stack<char>();
                for (int i = 0; i < line.Length; i++)
                {
                    char c = line[i];

                    RenderLine(line, i);
                
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

                RenderResult(points);
            }

            answer.WriteLine($"Answer Part 1: {total}");
        }

       
        public async Task Part2()
        {
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
            answer.WriteLine($"Answer Part 1: {scores[scores.Count / 2]}");
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

        private void RenderLine(string line, int position)
        {
            lines.CursorLeft = 0;
            lines.Write(ConsoleColor.Green, line.Substring(0, position));
            lines.Write(ConsoleColor.White, line.Substring(position, line.Length - position));
        }

        private void RenderResult(long score)
        {
            if (score == 0)
            {
                lines.WriteLine(ConsoleColor.Yellow, "   INCOMPLETED");
            }
            else
            {
                switch (score)
                {
                    case 3:
                        lines.WriteLine(ConsoleColor.Yellow, "   SYNTAX ERROR: )");
                        break;
                    case 57:
                        lines.WriteLine(ConsoleColor.Red, "   SYNTAX ERROR: ]");
                        break;
                    case 1197:
                        lines.WriteLine(ConsoleColor.Red, "   SYNTAX ERROR: }");
                        break;
                    case 25137:
                        lines.WriteLine(ConsoleColor.Red, "   SYNTAX ERROR: >");
                        break;
                    default: throw new InvalidOperationException();
                }
            }

        }

    }
}

