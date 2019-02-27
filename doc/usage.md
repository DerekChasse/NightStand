# Basic Usage

Rendering a table of `Person` entities to console.

```csharp
var people = new List<Person>();

var table = new Table<Person>
{
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

![](./img/output.png)


