namespace NightStand
{
    using System.Collections.Generic;

    public interface ITableWriter<T>
    {
        void Draw(Table<T> table, IEnumerable<T> items);

        void Draw(Table<T> table, IEnumerable<T> items, TableConfig config);
    }
}