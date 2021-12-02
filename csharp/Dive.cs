using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCsharp
{
    public class Dive : IProcessDayRiddle
    {
        private enum SubmarineNavigationAction
        {
            Up,
            Down,
            Forward
        }

        private const string InputValues = "../../../../res/dayTwo.txt";
        private string _solutionOne = string.Empty;
        private string _solutionTwo = string.Empty;
        private readonly List<Tuple<SubmarineNavigationAction, int>> _instructionSet = new();

        private IEnumerable<string> Extract(string path) => File.ReadAllLines(path).Where(x => !string.IsNullOrWhiteSpace(x));

        private int CalculateHorizontal() => _instructionSet.Where(x => x.Item1.Equals(SubmarineNavigationAction.Forward)).Select(x => x.Item2).Sum();

        private int CalculateDepth() => _instructionSet.Where(x => x.Item1.Equals(SubmarineNavigationAction.Down)).Select(x => x.Item2).Sum()
                                        - _instructionSet.Where(x => x.Item1.Equals(SubmarineNavigationAction.Up)).Select(x => x.Item2).Sum();

        private int CalculateStepByStep()
        {
            var depth = 0;
            var horizontal = 0;
            var aim = 0;
            foreach (var (action, amount) in _instructionSet)
            {
                if (action.Equals(SubmarineNavigationAction.Forward))
                {
                    horizontal += amount;
                    depth += aim * amount;
                }
                if (action.Equals(SubmarineNavigationAction.Up))
                {
                    aim -= amount;
                }
                if (action.Equals(SubmarineNavigationAction.Down))
                {
                    aim += amount;
                }
            }

            return horizontal * depth;
        }

        private void ExtractInstructions(string path)
        {
            var steps = Extract(path);
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
            ExtractInstructions(InputValues);
            _solutionOne = (CalculateHorizontal() * CalculateDepth()).ToString();
            _solutionTwo = CalculateStepByStep().ToString();
        }

        public void PrintSolutions()
        {
            Console.WriteLine("DAY TWO - Dive");
            Console.WriteLine("Solution Part One: " + _solutionOne);
            Console.WriteLine("Solution Part Two: " + _solutionTwo);
        }
    }
}
