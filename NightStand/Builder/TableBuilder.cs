namespace NightStand.Builder
{
    public class TableBuilder<T>
    {
        private readonly Table<T> table;

        public TableBuilder()
        {
            this.table = new Table<T>();
        }

        public static implicit operator Table<T>(TableBuilder<T> builder)
        {
            return builder.table;
        }

        public TableBuilder<T> AddColumn(Column<T> column)
        {
            this.table.Columns.Add(column);
            return this;
        }
    }
}
