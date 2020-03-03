namespace NightStand
{
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods for <see cref="Table{T}"/> which aide table drawing.
    /// </summary>
    public static class TableExtensions
    {
        /// <summary>
        /// Draws a collection of items in a table to the console.
        /// </summary>
        /// <typeparam name="T">The type of item to draw.</typeparam>
        /// <param name="table">The configured table in which to draw the items.</param>
        /// <param name="items">The collection of items to draw.</param>
        public static void Draw<T>(this Table<T> table, IEnumerable<T> items)
        {
            new ConsoleTableWriter<T>().Draw(table, items);
        }

        /// <summary>
        /// Draws a collection of items in a table using the specified <see cref="ITableWriter{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of item to draw.</typeparam>
        /// <param name="table">The configured table in which to draw the items.</param>
        /// <param name="writer">The <see cref="ITableWriter{T}"/> instance responsible for drawing the table.</param>
        /// <param name="items">The collection of items to draw.</param>
        public static void DrawWith<T>(this Table<T> table, ITableWriter<T> writer, IEnumerable<T> items)
        {
            writer.Draw(table, items);
        }
    }
}
