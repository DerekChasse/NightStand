namespace NightStand
{
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/> which aide table drawing.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Draws a collection of items in a table to the console.
        /// </summary>
        /// <typeparam name="T">The type of item to draw.</typeparam>
        /// <param name="items">The collection of items to draw.</param>
        /// <param name="table">The configured table in which to draw the items.</param>
        public static void Draw<T>(this IEnumerable<T> items, Table<T> table)
        {
            new ConsoleTableWriter<T>().Draw(table, items);
        }

        /// <summary>
        /// Draws a collection of items in a table using the specified <see cref="ITableWriter{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of item to draw.</typeparam>
        /// <param name="items">The collection of items to draw.</param>
        /// <param name="writer">The <see cref="ITableWriter{T}"/> instance responsible for drawing the table.</param>
        /// <param name="table">The configued table in which to draw the items.</param>
        public static void DrawWith<T>(this IEnumerable<T> items, ITableWriter<T> writer, Table<T> table)
        {
            writer.Draw(table, items);
        }
    }
}