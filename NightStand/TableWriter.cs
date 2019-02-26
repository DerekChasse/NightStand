namespace NightStand
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public abstract class TableWriter<T>
    {
        private readonly TextWriter writer;

        private Dictionary<Column<T>, int> columnWidthMap;
        private string horizontalLine = string.Empty;
        private string contentLine = string.Empty;

        protected TableWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        protected int TableTotalWidth { get; private set; }

        public void Draw(Table<T> table, IEnumerable<T> items)
        {
            this.Draw(table, items, TableConfig.Default);
        }

        public void Draw(Table<T> table, IEnumerable<T> items, TableConfig config)
        {
            var enumerated = items as T[] ?? items.ToArray();

            this.Initialize(table, enumerated, config);

            this.PostInitialize();

            this.DrawTableTop(config);

            this.DrawTableHeader(table, config);

            foreach (var item in enumerated)
            {
                this.DrawContentRow(table, item);
            }

            this.DrawTableBottom(config);
        }

        protected virtual void PostInitialize()
        {
        }

        private static int ComputeColumnBaseWidth(Column<T> column, IEnumerable<T> items)
        {
            var baseWidth = column.Header.Length;

            var enumerated = items as T[] ?? items.ToArray();
            if (enumerated.Any())
            {
                baseWidth = Math.Max(enumerated.Max(item => column.ValueSelector(item).Length), baseWidth);
            }

            return baseWidth;
        }

        private static string PadCellValue(string content, ColumnAlignment alignment, int totalWidth)
        {
            return alignment == ColumnAlignment.Left ? content.PadRight(totalWidth) : content.PadLeft(totalWidth);
        }

        private void Initialize(Table<T> table, IEnumerable<T> items, TableConfig config)
        {
            this.columnWidthMap = table.Columns.ToDictionary(c => c, sel => ComputeColumnBaseWidth(sel, items));
            var columnPaddedWidthDictionary = this.columnWidthMap.ToDictionary(kvp => kvp.Key, kvp => kvp.Value + config.CellLeftPadding + config.CellRightPadding);
            this.TableTotalWidth = columnPaddedWidthDictionary.Values.Sum() + table.Columns.Count + 1;

            var horizontalLineBuilder = new StringBuilder();
            horizontalLineBuilder.Append("{0}");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];
                horizontalLineBuilder.Append(config.HorizontalCharacter, columnPaddedWidthDictionary[column]);

                if (i + 1 != table.Columns.Count)
                {
                    horizontalLineBuilder.Append("{1}");
                }
            }

            horizontalLineBuilder.Append("{2}");
            this.horizontalLine = horizontalLineBuilder.ToString();

            var contentLineBuilder = new StringBuilder();

            contentLineBuilder.Append(config.VerticalCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                contentLineBuilder.Append(' ', config.CellLeftPadding);
                contentLineBuilder.Append('{').Append(i).Append('}');
                contentLineBuilder.Append(' ', config.CellRightPadding);
                contentLineBuilder.Append(config.VerticalCharacter);
            }

            this.contentLine = contentLineBuilder.ToString();
        }

        private void DrawTableTop(TableConfig config)
        {
            this.writer.WriteLine(this.horizontalLine, config.TopLeftCharacter, config.HorizontalTopJointCharacter, config.TopRightCharacter);
        }

        private void DrawTableHeader(Table<T> table, TableConfig config)
        {
            var values = table.Columns.Select(c => PadCellValue(c.Header, c.Alignment, this.columnWidthMap[c])).ToArray();

            this.writer.WriteLine(this.contentLine, values);

            this.DrawHorizontalDivider(config);
        }

        private void DrawHorizontalDivider(TableConfig config)
        {
            this.writer.WriteLine(this.horizontalLine, config.VerticalLeftJointCharacter, config.IntersectionJointCharacter, config.VerticalRightJointCharacter);
        }

        private void DrawTableBottom(TableConfig config)
        {
            this.writer.WriteLine(this.horizontalLine, config.BottomLeftCharacter, config.HorizontalBottomJointCharacter, config.BottomRightCharacter);
        }

        private void DrawContentRow(Table<T> table, T item)
        {
            var values = table.Columns.Select(c => PadCellValue(c.ValueSelector(item), c.Alignment, this.columnWidthMap[c])).ToArray();

            this.writer.WriteLine(this.contentLine, values);
        }
    }
}
