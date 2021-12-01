using System;

namespace AdventOfCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            IProcessDayRiddle _dayOne = new SonarSweep();
            _dayOne.ExecuteSolutions();
            _dayOne.PrintSolutions();
        }
    }
}
