using System;
using System.IO;
using System.Linq;

namespace AdventOfCsharp
{
    public class BinaryDiagnostic : IProcessDayRiddle
    {
        private const string InputValues = "../../../../res/dayThree.txt";
        private string _solutionOne = string.Empty;
        private string _solutionTwo = string.Empty;
        private string Oxygen = string.Empty;
        private string Co2 = string.Empty;

        private string[] ExtractNumbers(string path)
            => File.ReadAllLines(path).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

        private int CalculateGamaRate()
        {
            var lines = ExtractNumbers(InputValues);
            var total = 0;
            var GamaRate = string.Empty;

            for (int i = 0; i < lines[0].Length; i++)
            {
                var numbers = lines.Select(x => x[i]);
                GamaRate += numbers.Count(x => x.Equals('0')) > numbers.Count(x => x.Equals('1')) ? "0" : "1";
            }
            return Convert.ToInt32(GamaRate, 2);
        }

        private int CalculateEpsilonRate()
        {
            var lines = ExtractNumbers(InputValues);
            var total = 0;
            var GamaRate = string.Empty;

            for (int i = 0; i < lines[0].Length; i++)
            {
                var numbers = lines.Select(x => x[i]);
                GamaRate += numbers.Count(x => x.Equals('0')) < numbers.Count(x => x.Equals('1')) ? "0" : "1";
            }
            return Convert.ToInt32(GamaRate, 2);
        }

        private void CalculateOxygenCommonOne(string[] lines, int index)
        {
            var newLines = lines.Select(x => Convert.ToInt32(x[index].ToString())).Average() >= 0.5 ? lines.Where(x => x[index].Equals('1')) : lines.Where(x => x[index].Equals('0'));
            if (newLines.Count() == 1)
                Oxygen = newLines.Single();
            else
                CalculateOxygenCommonOne(newLines.ToArray(), index + 1);
        }

        private void CalculateCO2LeastZero(string[] lines, int index)
        {
            var newLines = lines.Select(x => Convert.ToInt32(x[index].ToString())).Average() >= 0.5 ? lines.Where(x => x[index].Equals('0')) : lines.Where(x => x[index].Equals('1'));
            if (newLines.Count() == 1)
                Co2 = newLines.Single();
            else
                CalculateCO2LeastZero(newLines.ToArray(), index + 1);
        }

        private int CalculateOxygenCommonOne()
        {
            CalculateOxygenCommonOne(ExtractNumbers(InputValues), 0);
            return Convert.ToInt32(Oxygen, 2);
        }

        private int CalculateCO2LeastZero()
        {
            CalculateCO2LeastZero(ExtractNumbers(InputValues), 0);
            return Convert.ToInt32(Co2, 2);
        }


        public void ExecuteSolutions()
        {
            _solutionOne = (CalculateGamaRate() * CalculateEpsilonRate()).ToString();
            _solutionTwo = (CalculateOxygenCommonOne() * CalculateCO2LeastZero()).ToString();
        }

        public void PrintSolutions()
        {
            Console.WriteLine("DAY THREE - Binary Diagnostic");
            Console.WriteLine("Solution Part One: " + _solutionOne);
            Console.WriteLine("Solution Part Two: " + _solutionTwo);
        }
    }
}
