# Index Column

Have the table writer append each row with an index.  

By default, the index column is hidden.

```csharp
var people = new List<Person>();

var table = new Table<Person>
{
    ShowIndexColumn = true,
    Columns =
    {
        new Column<Person>("Full Name", s => s.FullName),
        new Column<Person>("Gender", s => s.Gender.ToString()),
        new Column<Person>("Birth Date", s => s.DateOfBirth.ToShortDateString()),
        new Column<Person>("City", s => s.Address.City),
        new Column<Person>("State", s => s.Address.State),
    }
};

table.Draw(people);
```

## Output

![](./img/index_column.png)