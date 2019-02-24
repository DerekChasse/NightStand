namespace NightStand.Builder
{
    using System;

    public class ColumnBuilder<T>
    {
        private string header;
        private ColumnAlignment alignment;
        private Func<T, string> valueSelector;

        public static implicit operator Column<T>(ColumnBuilder<T> builder)
        {
            return new Column<T>(builder.header, builder.alignment, builder.valueSelector);
        }

        public ColumnBuilder<T> WithHeader(string header)
        {
            this.header = header;
            return this;
        }

        public ColumnBuilder<T> LeftAligned()
        {
            this.alignment = ColumnAlignment.Left;
            return this;
        }

        public ColumnBuilder<T> RightAligned()
        {
            this.alignment = ColumnAlignment.Right;
            return this;
        }

        public ColumnBuilder<T> WithValueSelector(Func<T, string> valueSelector)
        {
            this.valueSelector = valueSelector;
            return this;
        }

        public Column<T> Build()
        {
            return new Column<T>(this.header, this.alignment, this.valueSelector);
        }
    }
}
