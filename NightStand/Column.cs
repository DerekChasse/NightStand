namespace NightStand
{
    using System;

    public class Column<T>
    {
        public Column(string header, Func<T, string> valueSelector)
            : this(header, ColumnAlignment.Right, valueSelector)
        {
        }

        public Column(string header, ColumnAlignment alignment, Func<T, string> valueSelector)
        {
            this.Header = header;
            this.Alignment = alignment;
            this.ValueSelector = valueSelector;
        }

        public string Header { get; }

        public ColumnAlignment Alignment { get; }

        public Func<T, string> ValueSelector { get; }
    }
}