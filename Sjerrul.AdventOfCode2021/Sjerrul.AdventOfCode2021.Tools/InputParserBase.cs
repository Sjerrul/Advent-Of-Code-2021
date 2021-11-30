using System;
using System.IO;

namespace Sjerrul.AdventOfCode2021.Core
{
    public abstract class InputParserBase<T>
    {
        public bool IsFileRead { get; private set; }
        public bool IsFileParsed { get; private set; }
        public T ParsedFileContent { get; private set; }

        protected string rawFileContent;

        public void ReadFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace", nameof(path));
            }

            this.rawFileContent = File.ReadAllText(path);
            this.IsFileRead = true;
        }

        public void ParseFile()
        {
            this.ParsedFileContent = ParseLogic();
            this.IsFileParsed = true;
        }

        protected abstract T ParseLogic();
    }
}
