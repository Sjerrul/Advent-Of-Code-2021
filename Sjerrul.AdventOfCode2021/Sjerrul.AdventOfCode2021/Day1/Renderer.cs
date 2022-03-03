using Konsole;
using System.Threading;

namespace Sjerrul.AdventOfCode2021.Day1
{
    public class Renderer
    {
        private readonly bool visualize;

        private int lastIncreasing;
        private int left;
        private int depth;

        private readonly IConsole screenWindow;
        private readonly IConsole answerWindow;

        public Renderer(bool visualize)
        {
            this.screenWindow = Window.OpenBox("Processing", 100, 50);
            this.answerWindow = Window.OpenBox("Answer", 100, 3);

            this.visualize = visualize;
        }

        public void Tick(int increasing)
        {
            this.answerWindow.WriteLine($"{increasing}");

            if (!visualize)
            {
                return;
            }

            this.screenWindow.CursorLeft = left++;
            this.screenWindow.CursorTop = increasing != lastIncreasing ? depth++ : depth--;

            if (depth > this.screenWindow.WindowHeight)
            {
                this.screenWindow.Clear();
                depth = 10;
                left = 0;
            }

            this.screenWindow.Write("#");
            Thread.Sleep(1);

            lastIncreasing = increasing;

        }
    }
}
