namespace NightStand
{
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static void Draw<T>(this IEnumerable<T> items, Table<T> table)
        {
            new ConsoleTableWriter<T>().Draw(table, items);
        }

        public static void DrawWith<T>(this IEnumerable<T> items, ITableWriter<T> writer, Table<T> table)
        {
            writer.Draw(table, items);
        }
    }
}