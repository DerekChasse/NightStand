namespace NightStand
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public abstract class TextTableWriter<T> : TableWriter<T>
    {
        private readonly TextWriter writer;
        private readonly StringBuilder builder;

        private IDictionary<Column<T>, int> columnWidthLookup;
        private IDictionary<Column<T>, int> paddedColumnWidthLookup;

        protected TextTableWriter(TextWriter writer)
        {
            this.writer = writer;
            this.builder = new StringBuilder();
        }

        public int TableMaxWidth { get; private set; }

        public override void Draw(Table<T> table, IEnumerable<T> items)
        {
            this.Draw(table, items, TableConfig.Default);
        }

        public override void Draw(Table<T> table, IEnumerable<T> items, TableConfig config)
        {
            var enumerated = items as List<T> ?? items.ToList();

            if (table.ShowIndexColumn)
            {
                table.Columns.Insert(0, new Column<T>(string.Empty, x => (enumerated.IndexOf(x) + 1).ToString()));
            }

            this.Initialize(table, enumerated, config);

            if (!string.IsNullOrEmpty(table.Title))
            {
                this.DrawTitle(table, config);
            }
            else
            {
                this.builder.Append(config.TopLeftCharacter);

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    var column = table.Columns[i];
                    this.builder.Append(config.HorizontalCharacter, this.paddedColumnWidthLookup[column]);
                    this.builder.Append(i != table.Columns.Count - 1 ? config.HorizontalTopJointCharacter : config.TopRightCharacter);
                }

                this.builder.Append(Environment.NewLine);
            }

            this.DrawHeader(table, config);

            foreach (var item in enumerated)
            {
                this.builder.Append(config.VerticalCharacter);

                foreach (var column in table.Columns)
                {
                    var content = column.ValueSelector(item) ?? config.NullCellDefaultValue;

                    this.DrawCell(content, this.columnWidthLookup[column], column.Alignment, config);

                    this.builder.Append(config.VerticalCharacter);
                }

                this.builder.Append(Environment.NewLine);
            }

            this.DrawFooter(table, config);

            this.writer.WriteLine(this.builder);
        }

        private static int ComputeColumnBaseWidth(Column<T> column, IEnumerable<T> items, string nullCellValue)
        {
            var baseWidth = column.Header.Length;

            var enumerated = items as T[] ?? items.ToArray();
            if (enumerated.Any())
            {
                baseWidth = Math.Max(enumerated.Max(item => (column.ValueSelector(item) ?? nullCellValue).Length), baseWidth);
            }

            return baseWidth;
        }

        private void DrawFooter(Table<T> table, TableConfig config)
        {
            this.builder.Append(config.BottomLeftCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];

                this.builder.Append(config.HorizontalCharacter, this.paddedColumnWidthLookup[column]);

                this.builder.Append(i != table.Columns.Count - 1 ? config.HorizontalBottomJointCharacter : config.BottomRightCharacter);
            }

            this.builder.Append(Environment.NewLine);
        }

        private void DrawHeader(Table<T> table, TableConfig config)
        {
            this.builder.Append(config.VerticalCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];

                this.DrawCell(column.Header, this.columnWidthLookup[column], column.Alignment, config);

                this.builder.Append(config.VerticalCharacter);
            }

            this.builder.Append(Environment.NewLine);

            this.builder.Append(config.VerticalLeftJointCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];

                this.builder.Append(config.HorizontalCharacter, this.paddedColumnWidthLookup[column]);

                this.builder.Append(i != table.Columns.Count - 1 ? config.IntersectionJointCharacter : config.VerticalRightJointCharacter);
            }

            this.builder.Append(Environment.NewLine);
        }

        private void DrawTitle(Table<T> table, TableConfig config)
        {
            var leftPadding = (this.TableMaxWidth - 2 - table.Title.Length) / 2;
            var rightPadding = this.TableMaxWidth - 2 - table.Title.Length - leftPadding;

            this.builder.Append(config.TopLeftCharacter);
            this.builder.Append(config.HorizontalCharacter, this.TableMaxWidth - 2);
            this.builder.Append(config.TopRightCharacter);
            this.builder.Append(Environment.NewLine);

            this.builder.Append(config.VerticalCharacter);
            this.builder.Append(' ', leftPadding);
            this.builder.Append(table.Title);
            this.builder.Append(' ', rightPadding);
            this.builder.Append(config.VerticalCharacter);
            this.builder.Append(Environment.NewLine);

            this.builder.Append(config.VerticalLeftJointCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];
                this.builder.Append(config.HorizontalCharacter, this.paddedColumnWidthLookup[column]);
                this.builder.Append(i != table.Columns.Count - 1 ? config.HorizontalTopJointCharacter : config.VerticalRightJointCharacter);
            }

            this.builder.Append(Environment.NewLine);
        }

        private void Initialize(Table<T> table, IEnumerable<T> enumerated, TableConfig config)
        {
            this.columnWidthLookup = table.Columns.ToDictionary(col => col, col => ComputeColumnBaseWidth(col, enumerated, config.NullCellDefaultValue));

            var contentLineWidth = this.columnWidthLookup.Values.Sum() + (table.Columns.Count * (config.CellLeftPadding + config.CellRightPadding)) + table.Columns.Count + 1;
            var titleLineWidth = table.Title.Length + config.CellLeftPadding + config.CellRightPadding + 2;

            if (contentLineWidth < titleLineWidth)
            {
                var lastColumn = this.columnWidthLookup.Last();

                var lastColumnWidth = lastColumn.Value + (titleLineWidth - contentLineWidth);

                this.columnWidthLookup[lastColumn.Key] = lastColumnWidth;
            }

            this.paddedColumnWidthLookup = this.columnWidthLookup.ToDictionary(kvp => kvp.Key, kvp => kvp.Value + config.CellLeftPadding + config.CellRightPadding);

            this.TableMaxWidth = Math.Max(contentLineWidth, titleLineWidth);
        }

        private void DrawCell(string content, int totalWidth, ColumnAlignment alignment, TableConfig config)
        {
            this.builder.Append(' ', config.CellLeftPadding);

            switch (alignment)
            {
                case ColumnAlignment.Center:
                    {
                        var leftPadding = (totalWidth - content.Length) / 2;
                        var rightPadding = totalWidth - content.Length - leftPadding;

                        this.builder.Append(' ', leftPadding);
                        this.builder.Append(content);
                        this.builder.Append(' ', rightPadding);

                        break;
                    }

                case ColumnAlignment.Left:
                    {
                        this.builder.Append(content.PadRight(totalWidth));
                        break;
                    }

                case ColumnAlignment.Right:
                    {
                        this.builder.Append(content.PadLeft(totalWidth));
                        break;
                    }

                default:
                    break;
            }

            this.builder.Append(' ', config.CellRightPadding);
        }
    }
}
