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

            this.DrawTop(table, config);

            this.DrawHeader(table, config);

            foreach (var item in items)
            {
                this.DrawContentRow(table, item, config);
            }

            this.DrawBottom(table, config);
        }

        protected virtual void PostInitialize()
        {
        }

        private void DrawHeader(Table<T> table, TableConfig config)
        {
            this.writer.Write(config.VerticalCharacter);

            foreach (var column in table.Columns)
            {
                this.WriteColumnCell(column, column.Header, config);
                this.writer.Write(config.VerticalCharacter);
            }

            this.writer.Write(Environment.NewLine);

            this.DrawHorizontalDivider(table, config);
        }

        private void Initialize(Table<T> table, IEnumerable<T> items, TableConfig config)
        {
            this.columnBaseWidthDictionary = table.Columns.ToDictionary(c => c, sel => this.ComputeColumnBaseWidth(sel, items));
            this.columnPaddedWidthDictionary = this.columnBaseWidthDictionary.ToDictionary(kvp => kvp.Key, kvp => kvp.Value + config.CellLeftPadding + config.CellRightPadding);
            this.TableTotalWidth = this.columnPaddedWidthDictionary.Values.Sum() + table.Columns.Count + 1;
        }

        private void DrawTop(Table<T> table, TableConfig config)
        {
            this.writer.Write(config.TopLeftCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];
                this.WriteColumnCell(column, config.HorizontalCharacter);

                if (i + 1 != table.Columns.Count)
                {
                    this.writer.Write(config.HorizontalTopJointCharacter);
                }
            }

            this.writer.Write(config.TopRightCharacter);
            this.writer.Write(Environment.NewLine);
        }

        private void DrawHorizontalDivider(Table<T> table, TableConfig config)
        {
            this.writer.Write(config.VerticalLeftJointCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];
                this.WriteColumnCell(column, config.HorizontalCharacter);

                if (i + 1 != table.Columns.Count)
                {
                    this.writer.Write(config.IntersectionJointCharacter);
                }
            }

            this.writer.Write(config.VerticalRightJointCharacter);
            this.writer.Write(Environment.NewLine);
        }

        private void DrawBottom(Table<T> table, TableConfig config)
        {
            this.writer.Write(config.BottomLeftCharacter);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];
                this.WriteColumnCell(column, config.HorizontalCharacter);

                if (i + 1 != table.Columns.Count)
                {
                    this.writer.Write(config.HorizontalBottomJointCharacter);
                }
            }

            this.writer.Write(config.BottomRightCharacter);
            this.writer.Write(Environment.NewLine);
        }

        private void WriteColumnCell(Column<T> column, char character)
        {
            this.writer.Write(new string(character, this.columnPaddedWidthDictionary[column]));
        }

        private void WriteColumnCell(Column<T> column, string cellValue, TableConfig config)
        {
            this.writer.Write(new string(' ', config.CellLeftPadding));

            var paddedCellValue = cellValue.PadLeft(this.columnBaseWidthDictionary[column]);

            this.writer.Write(paddedCellValue);

            this.writer.Write(new string(' ', config.CellRightPadding));
        }

        private void DrawContentRow(Table<T> table, T item, TableConfig config)
        {
            this.writer.Write(config.VerticalCharacter);

            foreach (var column in table.Columns)
            {
                var cellValue = column.ValueSelector(item);
                this.WriteColumnCell(column, cellValue, config);
                this.writer.Write(config.VerticalCharacter);
            }

            this.writer.Write(Environment.NewLine);
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
