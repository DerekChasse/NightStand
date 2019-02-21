namespace NightStand
{
    using System;

    public class Column<T>
    {
        public Column(string header, Func<T, string> valueSelector)
        {
            this.Header = header;
            this.ValueSelector = valueSelector;
        }

        public string Header { get; }

        public Func<T, string> ValueSelector { get; }
    }
}