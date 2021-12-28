using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sjerrul.AdventOfCode2021.Day12
{
    [DebuggerDisplay("{Name}")]
    public class Node
    {
        public string Name { get; set; }
        public IList<Node> Neighbours { get; set; }

        public bool IsSmall => !this.Name.Any(c => char.IsUpper(c));

        public Node()
        {
            this.Neighbours = new List<Node>();
        }
    }
}
