# TableWriter

## ITableWriter

The `ITableWriter` interface is the primary point of extensibility allowing users to provide their own custom implementation.

## ConsoleTableWriter

By default, users are able to render their tables to the console by using the provided `ConsoleTableWriter`.

### Customization
The user has two items of customization when defining a `ConsoleTableWriter`.  

#### ResizeConsole
By default, drawing the table will attempt to resize the Console's buffer width to accommodate the rendered table.  If this is not desired, users can disable this feature by setting this property to `false`.

#### ConsoleResizePadding
When the console buffer is resized, the value of this property is added to the buffer.  This is done to preemptively avoid future attempts to resize the console.