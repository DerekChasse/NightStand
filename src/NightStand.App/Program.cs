namespace NightStand.App
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Bogus;

    public class Program
    {
        public static void Main()
        {
            bool exit;

            do
            {
                Test();

                Console.WriteLine();

                Console.WriteLine("Press ENTER to run test again.");

                Console.WriteLine();

                exit = Console.ReadKey().Key == ConsoleKey.Enter;
            }
            while (exit);
        }

        private static void Test()
        {
            var people = new List<Person>();

            var table = new Table<Person>
            {
                Title = "These are people",
                ShowIndexColumn = true,
                Columns =
                {
                    new Column<Person>("Full Name", s => s.FullName),
                    new Column<Person>("Gender", s => s.Gender.ToString()),
                    new Column<Person>("Birth Date", s => s.DateOfBirth.ToShortDateString()),
                    new Column<Person>("City", s => s.Address.City),
                    new Column<Person>("State", s => s.Address.State),
                }
            };

            for (var i = 0; i < 500; i++)
            {
                people.Add(new Person());
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            table.Draw(people);

            var elapsed = stopwatch.Elapsed;

            Console.WriteLine($"Drawing took {elapsed.ToString()}");

        }
    }
}
