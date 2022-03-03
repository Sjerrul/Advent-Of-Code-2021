using Konsole;
using Sjerrul.AdventOfCode2021.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day16
{
    public class Day16Solver : SolverBase, ISolve
    {
        private IConsole map;
        private IConsole answers;

        public Day16Solver(string inputPath) : base(inputPath)
        {
            this.map = Window.OpenBox("Map", 110, 50);
            this.answers = Window.OpenBox("Answers", 110, 3);
        }

        public async Task Part1()
        {
            string hexString = this.Input.First();
            string binarystring = String.Join(String.Empty,
              hexString.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            );

            NewMethod(binarystring);

        }

        private void NewMethod(string binarystring)
        {
            string versionBits = binarystring.Substring(0, 3);
            int version = Convert.ToInt32(versionBits, 2);

            string typeIdBits = binarystring.Substring(3, 3);
            int typeId = Convert.ToInt32(typeIdBits, 2);

            switch (typeId)
            {
                case 4:
                    int index = 6;
                    int size = 5;

                    string bytes = string.Empty;
                    bool lastPacket = false;
                    do
                    {
                        string readBit = binarystring.Substring(index, size);
                        if (readBit.StartsWith('0'))
                        {
                            lastPacket = true;
                        }

                        bytes = $"{bytes}{readBit.Substring(1)}";

                        index += size;
                    } while (!lastPacket);

                    this.map.WriteLine($"{bytes} ({Convert.ToInt32(bytes, 2)})");

                    break;
            };
        }

        public async Task Part2()
        {

        }

    }
}
