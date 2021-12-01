using System;
using System.Collections.Generic;

namespace AdventOfCsharp
{
    static class Program
    {
        private static readonly Dictionary<int, IProcessDayRiddle> RiddleSolvers = new()
        {
            { 1, new SonarSweep() }
        };

        private const string Welcome =
            @"                          
                          ADVENT OF CODE 2021
                          ";

        private const string InvalidDay = "Invalid day riddle selected; starts at 1, ends at ";
        private const string SelectDayMessage = "Please select a day from 1 to ";

        static void Main(string[] args)
        {
            Console.WriteLine(Welcome);
            Console.WriteLine(SelectDayMessage + RiddleSolvers.Count);
            var day = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(day) || !int.TryParse(day, out var dayNumber) || int.Parse(day) <= 0 || int.Parse(day) > RiddleSolvers.Count)
            {
                Console.WriteLine(InvalidDay + RiddleSolvers.Count);
                Console.WriteLine(InvalidDay + RiddleSolvers.Count);
                Main(args);
            }
            else
            {
                var daySolver = RiddleSolvers[int.Parse(day)];
                daySolver.ExecuteSolutions();
                daySolver.PrintSolutions();
            }
        }

    }
}
