namespace NightStand
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    public abstract class TableWriter<T>
    {
        private readonly TextWriter writer;

        private int[] columnBaseWidthDictionary;
        private int[] columnPaddedWidthDictionary;

        private int tableTotalWidth;
        private string contentFormatString;

        public TableWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        protected int TableTotalWidth { get => this.tableTotalWidth; }

        public void Draw(Table<T> table, IEnumerable<T> items)
        {
            this.Draw(table, items, TableConfig.Default);
        }

        public void Draw(Table<T> table, IEnumerable<T> items, TableConfig config)
        {
            this.Initialize(table, items, config);

            this.DrawHeaderTop(table, config);

            this.DrawRow(table.Columns.Select(col => col.Header).ToArray());

            this.DrawHeaderBottom(table, config);

            foreach (var item in items)
            {
                this.DrawRow(table.Columns.Select(col => col.ValueSelector(item)).ToArray());
            }

            this.DrawFooter(table, config);
        }

        public virtual void PostInitialize()
        {
        }

        private void Initialize(Table<T> table, IEnumerable<T> items, TableConfig config)
        {
            this.columnBaseWidthDictionary = table.Columns.Select(
                col => this.ComputeColumnBaseWidth(col, items))
                .ToArray();

            this.columnPaddedWidthDictionary = this.columnPaddedWidthDictionary.Select(
                    width => width + config.CellLeftPadding + config.CellRightPadding)
                    .ToArray();

            this.tableTotalWidth = this.columnPaddedWidthDictionary.Sum() + table.Columns.Count + 1;

            this.contentFormatString = this.BuildContentFormatString(table, config);
        }

        private void DrawHeaderTop(Table<T> table, TableConfig config)
        {
            this.writer.Write(config.TopLeftCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                this.writer.Write(new string(config.HorizontalCharacter, this.columnPaddedWidthDictionary[i]));

                if (i + 1 != table.Columns.Count)
                {
                    this.writer.Write(config.HorizontalTopJointCharacter);
                }
            }

            this.writer.Write(config.TopRightCharacter);
            this.writer.Write(Environment.NewLine);
        }

        private void DrawHeaderBottom(Table<T> table, TableConfig config)
        {
            this.writer.Write(config.VerticalLeftJointCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                this.writer.Write(new string(config.HorizontalCharacter, this.columnPaddedWidthDictionary[i]));

                if (i + 1 != table.Columns.Count)
                {
                    this.writer.Write(config.IntersectionJointCharacter);
                }
            }

            this.writer.Write(config.VerticalRightJointCharacter);
            this.writer.Write(Environment.NewLine);
        }

        private void DrawFooter(Table<T> table, TableConfig config)
        {
            this.writer.Write(config.BottomLeftCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                this.writer.Write(new string(config.HorizontalCharacter, this.columnPaddedWidthDictionary[i]));

                if (i + 1 != table.Columns.Count)
                {
                    this.writer.Write(config.HorizontalBottomJointCharacter);
                }
            }

            this.writer.Write(config.BottomRightCharacter);
            this.writer.Write(Environment.NewLine);
        }

        private void DrawRow(params string[] args)
        {
            this.writer.Write(string.Format(CultureInfo.InvariantCulture, this.contentFormatString, args));
        }

        private string BuildContentFormatString(Table<T> table, TableConfig config)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                sb.Append(config.VerticalCharacter);
                sb.Append(new string(' ', config.CellLeftPadding));
                sb.Append("{");
                sb.Append(i);
                sb.Append(',');
                sb.Append((table.Columns[i].Width + config.CellRightPadding) * -1);
                sb.Append('}');
            }

            sb.Append(config.VerticalCharacter);

            return sb.ToString();
        }

        private int ComputeColumnBaseWidth(Column<T> column, IEnumerable<T> items)
        {
            var baseWidth = column.Header.Length;

            if (items.Any())
            {
                baseWidth = Math.Max(items.Max(item => column.ValueSelector(item).Length), baseWidth);
            }

            return baseWidth;
        }
    }
}
