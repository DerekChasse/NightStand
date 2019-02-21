namespace NightStand
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public abstract class TableWriter<T>
    {
        private readonly TextWriter writer;
        private Dictionary<Column<T>, int> columnBaseWidthDictionary;
        private Dictionary<Column<T>, int> columnPaddedWidthDictionary;

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
            this.Initialize(table, items, config);

            this.PostInitialize();

            this.DrawTableTop(table, config);

            this.DrawTableHeader(table, config);

            foreach (var item in items)
            {
                this.DrawContentRow(table, item, config);
            }

            this.DrawTableBottom(table, config);
        }

        protected virtual void PostInitialize()
        {
        }

        private void Initialize(Table<T> table, IEnumerable<T> items, TableConfig config)
        {
            this.columnBaseWidthDictionary = table.Columns.ToDictionary(c => c, sel => this.ComputeColumnBaseWidth(sel, items));
            this.columnPaddedWidthDictionary = this.columnBaseWidthDictionary.ToDictionary(kvp => kvp.Key, kvp => kvp.Value + config.CellLeftPadding + config.CellRightPadding);
            this.TableTotalWidth = this.columnPaddedWidthDictionary.Values.Sum() + table.Columns.Count + 1;
        }

        private void DrawTableTop(Table<T> table, TableConfig config)
        {
            this.DrawTableBorderPortion(config.TopLeftCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                this.DrawTableBorderPortion(config.HorizontalCharacter, this.columnPaddedWidthDictionary[table.Columns[i]]);

                if (i + 1 != table.Columns.Count)
                {
                    this.DrawTableBorderPortion(config.HorizontalTopJointCharacter);
                }
            }

            this.DrawTableBorderPortion(config.TopRightCharacter);
            this.DrawNewLine();
        }

        private void DrawTableHeader(Table<T> table, TableConfig config)
        {
            this.DrawTableBorderPortion(config.VerticalCharacter);

            foreach (var column in table.Columns)
            {
                var paddedCellValue = this.PadCellValue(column.Header, column.Alignment, this.columnBaseWidthDictionary[column]);

                this.DrawTableCellPadding(config.CellLeftPadding);
                this.DrawTableCellContent(paddedCellValue);
                this.DrawTableCellPadding(config.CellRightPadding);

                this.DrawTableBorderPortion(config.VerticalCharacter);
            }

            this.DrawNewLine();

            this.DrawHorizontalDivider(table, config);
        }

        private void DrawHorizontalDivider(Table<T> table, TableConfig config)
        {
            this.DrawTableBorderPortion(config.VerticalLeftJointCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                this.DrawTableBorderPortion(config.HorizontalCharacter, this.columnPaddedWidthDictionary[table.Columns[i]]);

                if (i + 1 != table.Columns.Count)
                {
                    this.DrawTableBorderPortion(config.IntersectionJointCharacter);
                }
            }

            this.DrawTableBorderPortion(config.VerticalRightJointCharacter);
            this.DrawNewLine();
        }

        private void DrawTableBottom(Table<T> table, TableConfig config)
        {
            this.DrawTableBorderPortion(config.BottomLeftCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                this.DrawTableBorderPortion(config.HorizontalCharacter, this.columnPaddedWidthDictionary[table.Columns[i]]);

                if (i + 1 != table.Columns.Count)
                {
                    this.DrawTableBorderPortion(config.HorizontalBottomJointCharacter);
                }
            }

            this.DrawTableBorderPortion(config.BottomRightCharacter);
            this.DrawNewLine();
        }

        private void DrawContentRow(Table<T> table, T item, TableConfig config)
        {
            this.DrawTableBorderPortion(config.VerticalCharacter);

            foreach (var column in table.Columns)
            {
                var paddedCellValue = this.PadCellValue(column.ValueSelector(item), column.Alignment, this.columnBaseWidthDictionary[column]);

                this.DrawTableCellPadding(config.CellLeftPadding);
                this.DrawTableCellContent(paddedCellValue);
                this.DrawTableCellPadding(config.CellRightPadding);

                this.DrawTableBorderPortion(config.VerticalCharacter);
            }

            this.DrawNewLine();
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

        private string PadCellValue(string content, ColumnAlignment alignment, int totalWidth)
        {
            return alignment == ColumnAlignment.Left ? content.PadRight(totalWidth) : content.PadLeft(totalWidth);
        }

        private void DrawNewLine()
        {
            this.writer.Write(Environment.NewLine);
        }

        private void DrawTableBorderPortion(char character)
        {
            this.writer.Write(character);
        }

        private void DrawTableBorderPortion(char character, int instances)
        {
            this.writer.Write(new string(character, instances));
        }

        private void DrawTableCellPadding(int instances)
        {
            this.writer.Write(new string(' ', instances));
        }

        private void DrawTableCellContent(string content)
        {
            this.writer.Write(content);
        }
    }
}
