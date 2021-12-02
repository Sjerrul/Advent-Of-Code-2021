using System.Collections.Generic;
using System.IO;

namespace Sjerrul.AdventOfCode2021.Core
{
    public abstract class SolverBase
    {
        protected IEnumerable<string> Input { get; private set; }

        public SolverBase(string inputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
            {
                throw new System.ArgumentException($"'{nameof(inputPath)}' cannot be null or whitespace", nameof(inputPath));
            }

            this.Input = File.ReadAllLines(inputPath);

        }
    }
}
