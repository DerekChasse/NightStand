namespace NightStand
{
    using System;

    /// <summary>
    /// Describes a column within a table.
    /// </summary>
    /// <typeparam name="T">The type of item being rendered.</typeparam>
    public class Column<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Column{T}"/> class.
        /// The column has <see cref="ColumnAlignment.Left"/> by default.
        /// </summary>
        /// <param name="header">Value to be rendered at the top of the column.</param>
        /// <param name="valueSelector">Method which evaluates a string value for instances of <typeparamref name="T"/>.</param>
        public Column(string header, Func<T, string> valueSelector)
            : this(header, ColumnAlignment.Left, valueSelector)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column{T}"/> class.
        /// </summary>
        /// <param name="header">Value to be rendered at the top of the column.</param>
        /// <param name="alignment">Value indicating how the data should be aligned within the column.</param>
        /// <param name="valueSelector">Method which evaluates a string value for instances of <typeparamref name="T"/>.</param>
        public Column(string header, ColumnAlignment alignment, Func<T, string> valueSelector)
        {
            this.Header = header;
            this.Alignment = alignment;
            this.ValueSelector = valueSelector;
        }

        /// <summary>
        /// Gets the string value rendered at the top of the column.
        /// </summary>
        public string Header { get; }

        /// <summary>
        /// Gets the alignment value for data within the column.
        /// </summary>
        public ColumnAlignment Alignment { get; }

        /// <summary>
        /// Gets the method which evaluates the columns display value for instances of <typeparamref name="T"/>.
        /// </summary>
        public Func<T, string> ValueSelector { get; }
    }
}