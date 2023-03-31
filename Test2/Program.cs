using System;
using System.Collections.Generic;
using System.Linq;

namespace Test2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("e4 .NET Developer Exercise");
            Console.WriteLine("Tumelo Madiba - Test 2");
            Console.WriteLine(DateTime.Now.ToLongDateString());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("CALCULATOR");
            Console.WriteLine(
                "Please use either \",\" or \"\\n\" to separate your numbers. eg, \"1,2,3\" or \"1\\n2,3\"");
            Console.WriteLine(
                "Alternatively, please use this format to specify a custom delimiter //[delimiter]\\n[numbers]. eg, \"//;\\n1;2\"");
            Console.WriteLine();

            do
            {
                try
                {
                    Console.WriteLine("Please enter numbers:");
                    string input = Console.ReadLine();
                    var result = StringCalculator.Add(input);

                    Console.WriteLine($"Answer: {result}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

                Console.WriteLine("=================================");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Press ESC to exist, or any key to restart calculator.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }

    public static class StringCalculator
    {
        public static int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }

            var delimiters = new[] { ",", "\n", "\\n" };

            // accounting for specified delimiter
            if (input.StartsWith("//"))
            {
                // add the custom delimiter to the existing list
                delimiters = delimiters.Concat(new[] { input[2].ToString() }).ToArray();
                input = input.Substring(4);
            }

            var splitList = input
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var numbersList = new List<int>();

            foreach (var inp in splitList)
            {
                try
                {
                    numbersList.Add(int.Parse(inp));
                }
                catch (Exception exception)
                {
                    throw new Exception($"{inp} has a bad format.");
                }
            }

            // check for negatives
            if (numbersList.Any(i => i < 0))
            {
                throw new ArgumentException(
                    $"negatives not allowed, {string.Join(",", numbersList.Where(i => i < 0).ToArray())}");
            }

            return numbersList.Sum();
        }
    }
}