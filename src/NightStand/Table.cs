namespace NightStand
{
    using System.Collections.Generic;

    public class Table<T>
    {
        public Table()
        {
            this.Title = string.Empty;
            this.Columns = new List<Column<T>>();
        }

        public string Title { get; set; }

        public IList<Column<T>> Columns { get; }
    }
}
