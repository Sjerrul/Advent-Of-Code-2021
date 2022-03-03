using System;
using System.Collections.Generic;
using System.Linq;

namespace Sjerrul.AdventOfCode2021.Day4
{
    public class BingoBoard
    {
        private int currentLine;
        private IList<int> calledNumbers = new List<int>();
        public int[][] Board { get; } = new int[5][];

        public void AddLine(string line)
        {
            IEnumerable<int> numbers = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));

            Board[currentLine] = numbers.ToArray();

            currentLine++;
        }

        public void AddCalledNumber(int number)
        {
            this.calledNumbers.Add(number);
        }

        public bool Check()
        {
            // Check horizontal
            foreach (var line in this.Board)
            {
                bool fullLine = true;
                foreach (var number in line)
                {
                    if (!this.calledNumbers.Contains(number))
                    {
                        fullLine = false;
                        break;
                    }
                }

                if (fullLine)
                {
                    return true;
                }
            }

            //Check vertical
            for (int i = 0; i < Board[0].Length; i++)
            {
                bool fullLine = true;
                for (int j = 0; j < Board[0].Length; j++)
                {

                    if (!this.calledNumbers.Contains(this.Board[j][i]))
                    {
                        fullLine = false;
                        break;
                    }
                }

                if (fullLine)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetChecksum()
        {
            return this.Board.SelectMany(a => a).Where(n => !this.calledNumbers.Contains(n)).Sum();
        }
    }
}
