using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCsharp
{
    public class Dive : IProcessDayRiddle
    {
        private const string InputValues = "../../../../res/dayTwo.txt";
        private string _solutionOne = string.Empty;
        private string _solutionTwo = string.Empty;
        private List<Tuple<SubmarineNavigationAction, int>> _instructionSet = new();

        private string[] Extract(string path) =>
            File.ReadAllLines(path).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

        private int CalculateHorizontal(List<Tuple<SubmarineNavigationAction, int>> steps)
            => steps.Where(x => x.Item1.Equals(SubmarineNavigationAction.Forward)).Select(x => x.Item2).Sum();

        private int CalculateDepth(List<Tuple<SubmarineNavigationAction, int>> steps)
            => steps.Where(x => x.Item1.Equals(SubmarineNavigationAction.Down)).Select(x => x.Item2).Sum() - steps.Where(x => x.Item1.Equals(SubmarineNavigationAction.Up)).Select(x => x.Item2).Sum();

        private int CalculateStepByStep()
        {
            var depth = 0;
            var horizontal = 0;
            var aim = 0;
            foreach (var step in _instructionSet)
            {
                if (step.Item1.Equals(SubmarineNavigationAction.Forward))
                {
                    horizontal += step.Item2;
                    depth += aim * step.Item2;
                }
                if (step.Item1.Equals(SubmarineNavigationAction.Up))
                {
                    aim -= step.Item2;
                }
                if (step.Item1.Equals(SubmarineNavigationAction.Down))
                {
                    aim += step.Item2;
                }
            }

            return horizontal * depth;
        }

        private void ExtractInstructions()
        {
            var steps = Extract(InputValues);
            _instructionSet.AddRange(steps.Select(x =>
            {
                var substeps = x.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var action = Enum.Parse<SubmarineNavigationAction>(substeps[0], true);
                var amount = Convert.ToInt32(substeps[1]);
                return new Tuple<SubmarineNavigationAction, int>(action, amount);
            }));
        }

        public void ExecuteSolutions()
        {
            ExtractInstructions();
            _solutionOne = (CalculateHorizontal(_instructionSet) * CalculateDepth(_instructionSet)).ToString();
            _solutionTwo = CalculateStepByStep().ToString();
        }

        public void PrintSolutions()
        {
            Console.WriteLine("DAY TWO - Dive");
            Console.WriteLine("Solution Part One: " + _solutionOne);
            Console.WriteLine("Solution Part Two: " + _solutionTwo);
        }

        private enum SubmarineNavigationAction
        {
            Up,
            Down,
            Forward
        }
    }
}
