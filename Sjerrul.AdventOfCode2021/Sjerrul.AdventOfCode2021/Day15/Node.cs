using System;
using System.Collections.Generic;
using System.Text;

namespace Sjerrul.AdventOfCode2021.Day15
{
    public class Node
    {
        public Node Parent { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }

        public int Value { get; set; }
    }
}
