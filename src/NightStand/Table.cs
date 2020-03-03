namespace NightStand
{
    using System.Collections.Generic;

    /// <summary>
    /// Describes how a collection of items are to be rendered in a tabular format.
    /// </summary>
    /// <typeparam name="T">The type of item to be rendered.</typeparam>
    public class Table<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Table{T}"/> class.
        /// </summary>
        public Table()
        {
            this.Title = string.Empty;
            this.Columns = new List<Column<T>>();
        }

        /// <summary>
        /// Gets or sets the title of the table.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the index column should be rendered.
        /// </summary>
        public bool ShowIndexColumn { get; set; }

        /// <summary>
        /// Gets a collection of <see cref="Column{T}"/> which will be rendered as part of the table.
        /// </summary>
        public IList<Column<T>> Columns { get; }
    }
}
