using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day1
{
    public class Day4Solver : SolverBase, ISolve
    {
        public Day4Solver(string inputPath) : base(inputPath)
        {
        }

        public async Task Part1()
        {
            IEnumerable<int> numbers = this.Input.First().Split(',').Select(x => int.Parse(x));

            IList<BingoBoard> boards = new List<BingoBoard>();
            BingoBoard currentBoard = null;
            for (int i = 1; i < Input.Count(); i++)
            {
                string line = this.Input.ElementAt(i);
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (currentBoard != null)
                    {
                        boards.Add(currentBoard);
                    }
                    currentBoard = new BingoBoard();
                    continue;
                }

                currentBoard.AddLine(line);
            }
            boards.Add(currentBoard);


            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    board.AddCalledNumber(number);
                    bool isWinner = board.Check();

                    if (isWinner)
                    {
                        var result = board.GetChecksum() * number;
                        Console.WriteLine($"Answer: {result}");
                        return;
                    }
                }
            }
        }

        public async Task Part2()
        {
            IEnumerable<int> numbers = this.Input.First().Split(',').Select(x => int.Parse(x));

            IList<BingoBoard> boards = new List<BingoBoard>();
            BingoBoard currentBoard = null;
            for (int i = 1; i < Input.Count(); i++)
            {
                string line = this.Input.ElementAt(i);
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (currentBoard != null)
                    {
                        boards.Add(currentBoard);
                    }
                    currentBoard = new BingoBoard();
                    continue;
                }

                currentBoard.AddLine(line);
            }
            boards.Add(currentBoard);


            IList<(BingoBoard winningBoard, int calledNumber)> winningboards = new List<(BingoBoard, int)>();
            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    board.AddCalledNumber(number);
                }

                for (int i = 0; i < boards.Count; i++)
                {
                    bool isWinner = boards[i].Check();

                    if (isWinner)
                    {
                        winningboards.Add((boards[i], number));
                        boards.RemoveAt(i);
                    }
                }
            }

            var lastWinningSet = winningboards.Last();
            var result = lastWinningSet.winningBoard.GetChecksum() * lastWinningSet.calledNumber;
            Console.WriteLine($"Answer: {result}");
        }

    }
}
