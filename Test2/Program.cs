using System;
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
            Console.WriteLine("Please enter number:");

            try
            {
                string input = Console.ReadLine();
                var result = StringCalculator.Add(input);

                Console.WriteLine($"Answer: {result}");
                Console.ReadLine();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadLine();
            }
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

            var delimiters = new[] { ",", "\n" };

            // accounting for specified delimiter
            if (input.StartsWith("//"))
            {
                // add the custom delimiter to the existing list
                delimiters = delimiters.Concat(new[] { input[2].ToString() }).ToArray();
                input = input.Substring(4);
            }

            var numbersList = input
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

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