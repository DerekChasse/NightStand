namespace NightStand.App
{
    using System;
    using System.Collections.Generic;
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

                exit = Console.ReadKey().Key == ConsoleKey.Enter;
            }
            while (exit);
        }

        private static void Test()
        {
            var samples = new List<Person>();

            var table = new Table<Person>
            {
                Columns =
                {
                    new Column<Person>("Full Name", s => s.FullName),
                    new Column<Person>("Gender", s => s.Gender.ToString()),
                    new Column<Person>("Birth Date", s => s.DateOfBirth.ToShortDateString()),
                    new Column<Person>("City", s => s.Address.City),
                    new Column<Person>("State", s => s.Address.State),
                }
            };

            for (var i = 0; i < 10; i++)
            {
                samples.Add(new Person());
            }

            new ConsoleTableWriter<Person>().Draw(table, samples);
        }
    }
}
