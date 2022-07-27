using Iso8601;
using System;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = {
                "P14Y2M8D",
                "P14Y2M8DT6HM02S",      // with leading zeros (02S) → works fine
                "P-14Y2M8DT-6HM02S",    // iso8601 standard itself supports negative values of time portions → works fine
                "PT20M",                // without date group → works fine
                "P14D",
                "P2M8DT6H15M02S",
                "P8DT16S",
                "P8DT0S",               // with zero-value for seconds → works fine
                "P8DTS",                // with empty-value for seconds → works fine
                "P14Y2M8DT6HM02S",
                "P2M14Y8DT6HM02S",      // months are in front of years → works fine
                "P2M8DT6HM02S14Y",      // years are in an inappropriate group → years are ignored
                "P14Y8DT6H2M6M02S"      // months are in an inappropriate group and represent minutes → minutes are summed (2+6)
            };
            foreach (var item in input)
            {
                var dt = DateTime.Parse("2020-01-01");
// adding ISO 8601 interval:
                Console.WriteLine(dt.Add(new Iso8601Duration(item)) + "\t" + item);
            }
            Console.WriteLine();
            foreach (var item in input)
            {
                var dt = DateTime.Parse("2020-01-01");
// subtracting ISO 8601 interval:
                Console.WriteLine(dt.Subtract(new Iso8601Duration(item)) + "\t" + item);
            }
            Console.WriteLine();
            for (int i = 1; i < input.Length; i++)
            {
                var prev = new Iso8601Duration(input[i - 1]);
                var cur = new Iso8601Duration(input[i]);
// comparing ISO 8601 intervals: >, <, >=, <=, ==, !=, o.Equals
                var not = (prev > cur) ? null : "not ";
                Console.WriteLine($"{input[i - 1]} is {not}more than {input[i]}");
            }
        }
    }
}
