namespace NightStand
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public abstract class TableWriter<T> : ITableWriter<T>
    {
        public abstract void Draw(Table<T> table, IEnumerable<T> items);

        public abstract void Draw(Table<T> table, IEnumerable<T> items, TableConfig config);

        protected virtual void PostInitialize()
        {
        }
    }
}
