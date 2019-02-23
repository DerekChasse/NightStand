namespace NightStand
{
    using System.Collections.Generic;

    public interface ITable<T>
    {
        IList<Column<T>> Columns { get; }
    }
}