namespace NightStand
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Column<T>
    {
        public Column(string header, Func<T, string> valueSelector)
        {
            this.Header = header;
            this.ValueSelector = valueSelector;
        }

        public int Width { get; private set; }

        public string Header { get; }

        public Func<T, string> ValueSelector { get; }
    }
}