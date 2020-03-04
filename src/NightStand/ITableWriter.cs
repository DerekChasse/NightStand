namespace NightStand
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides a machanism for drawing tables.
    /// </summary>
    /// <typeparam name="T">The type being drawn by the table.</typeparam>
    public interface ITableWriter<T>
    {
        /// <summary>
        /// Draws a table with the default <see cref="TableConfig"/>.
        /// </summary>
        /// <param name="table">The table to draw.</param>
        /// <param name="items">Values to draw within the table.</param>
        void Draw(Table<T> table, IEnumerable<T> items);

        /// <summary>
        /// Draws a table with the default <see cref="TableConfig"/>.
        /// </summary>
        /// <param name="table">The table to draw.</param>
        /// <param name="items">Values to draw within the table.</param>
        /// <param name="config">Configuration object for the table.</param>
        void Draw(Table<T> table, IEnumerable<T> items, TableConfig config);
    }
}