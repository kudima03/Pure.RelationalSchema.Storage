using System.Collections;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage.Tests.Fakes;

public sealed record FakeTableDataset : IStoredTableDataSet
{
    private readonly IEnumerable<IRow> _rows;

    public FakeTableDataset(ITable tableSchema, IEnumerable<IRow> rows)
    {
        _rows = rows;
        TableSchema = tableSchema;
    }

    public ITable TableSchema { get; }

    public async IAsyncEnumerator<IRow> GetAsyncEnumerator(
        CancellationToken cancellationToken = default
    )
    {
        foreach (IRow row in _rows)
        {
            yield return row;
            await Task.Delay(1, cancellationToken);
        }
    }

    public IEnumerator<IRow> GetEnumerator()
    {
        return _rows.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
