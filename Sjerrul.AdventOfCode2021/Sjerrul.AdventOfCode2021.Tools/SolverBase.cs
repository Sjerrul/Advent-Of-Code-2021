namespace Sjerrul.AdventOfCode2021.Core
{
    public abstract class SolverBase<TOutput, TInput>
    {
        public TOutput Answer { get; private set; }

        public void CalculateAnswer(TInput input)
        {
            this.Answer = SolveLogic(input);
        }

        protected abstract TOutput SolveLogic(TInput input);
    }
}
