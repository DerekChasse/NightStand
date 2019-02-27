namespace NightStand
{
    using System.Collections.Generic;

    public static class TableExtensions
    {
        public static void Draw<T>(this Table<T> table, IEnumerable<T> items)
        {
            new ConsoleTableWriter<T>().Draw(table, items);
        }

        public static void DrawWith<T>(this Table<T> table, ITableWriter<T> writer, IEnumerable<T> items)
        {
            writer.Draw(table, items);
        }
    }
}
