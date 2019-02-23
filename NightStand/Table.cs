namespace NightStand
{
    using System.Collections.Generic;

    public class Table<T> : ITable<T>
    {
        public Table()
        {
            this.Columns = new List<Column<T>>();
        }

        public IList<Column<T>> Columns { get; }
    }
}
