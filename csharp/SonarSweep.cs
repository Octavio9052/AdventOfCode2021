using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCsharp
{
    public class SonarSweep : IProcessDayRiddle
    {
        private const string InputValues = "../../../../res/dayOne.txt";
        private readonly List<int> _windows = new();
        private string _solutionOne = string.Empty;
        private string _solutionTwo = string.Empty;

        private int[] ExtractNumbers(string path)
            => File.ReadAllLines(path).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => Convert.ToInt32(x)).ToArray();

        private int CountIncreases(int[] numbers)
        {
            var increments = 0;
            for (var i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > numbers[i - 1]) increments++;
            }
            return increments;
        }

        private void MakeWindows(int[] numbers)
        {
            if (numbers.Length >= 3)
            {
                _windows.Add(numbers.Take(3).Sum());
                MakeWindows(numbers.Skip(1).ToArray());
            }
        }

        public void ExecuteSolutions()
        {
            _solutionOne = CountIncreases(ExtractNumbers(InputValues)).ToString();
            MakeWindows(ExtractNumbers(InputValues));
            _solutionTwo = CountIncreases(_windows.ToArray()).ToString();
        }

        public void PrintSolutions()
        {
            Console.WriteLine("DAY ONE - Sonar Sweep");
            Console.WriteLine("Solution Part One: " + _solutionOne);
            Console.WriteLine("Solution Part Two: " + _solutionTwo);
        }
    }
}
