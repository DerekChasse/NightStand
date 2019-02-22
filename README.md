[![Build Status](https://derhasse.visualstudio.com/NightStand/_apis/build/status/Build%20and%20Package%20Master?branchName=master)](https://derhasse.visualstudio.com/NightStand/_build/latest?definitionId=42&branchName=master)
# NightStand
Tabular data pretty-printer in the console.

## Quick Start

Usage is simple and straight forward.

First import NightStand.

```csharp
using NightStand;
```

Then define your table.

As an example lets imagine that we want to print some information derived from `CultureInfo` objects. The following is a simple example of how to do so.

```csharp
Table<CultureInfo> cultureTable = new Table<CultureInfo>
{
    Columns =
    {
        new Column<CultureInfo>(header: "Name", valueSelector: ci => ci.Name),
        new Column<CultureInfo>(header: "Display Name", valueSelector: ci => ci.DisplayName)
    }
};
```

Once defined that table can be passed to a `TableWriter` instance such as the `ConsoleTableWriter`.

```csharp
// Some sample data
IEnumerable<CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.AllCultures).Take(10);

ConsoleTableWriter<CultureInfo> consoleWriter = new ConsoleTableWriter<CultureInfo>();

consoleWriter.Draw(table: cultureTable, items: cultures);
```


Sample output
```
┌────────┬────────────────────────────────────────┐
│ Name   │ Display Name                           │
├────────┼────────────────────────────────────────┤
│        │ Invariant Language (Invariant Country) │
│ aa     │ Qafar                                  │
│ aa-DJ  │ Qafar (Yabuuti)                        │
│ aa-ER  │ Qafar (Eretria)                        │
│ aa-ET  │ Qafar (Otobbia)                        │
│ af     │ Afrikaans                              │
│ af-NA  │ Afrikaans (Namibië)                    │
│ af-ZA  │ Afrikaans (South Africa)               │
│ agq    │ Aghem                                  │
│ agq-CM │ Aghem (Kàmàlû?)                        │
└────────┴────────────────────────────────────────┘
```
