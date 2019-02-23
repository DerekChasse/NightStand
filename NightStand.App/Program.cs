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
            var rand = new Random();

            List<Person> samples = new List<Person>();

            Table<Person> table = new Table<Person>
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

            for (int i = 0; i < 10; i++)
            {
                samples.Add(new Person());
            }

            samples.Draw(table);
        }
    }
}
