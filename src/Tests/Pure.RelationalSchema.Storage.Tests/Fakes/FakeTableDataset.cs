using System.Collections;
using System.Linq.Expressions;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage.Tests.Fakes;

public sealed record FakeTableDataset : IStoredTableDataSet
{
    private readonly IQueryable<IRow> _rows;

    public FakeTableDataset(ITable tableSchema, IEnumerable<IRow> rows)
    {
        _rows = rows.AsQueryable();
        TableSchema = tableSchema;
    }

    public ITable TableSchema { get; }

    public Type ElementType => _rows.ElementType;

    public Expression Expression => _rows.Expression;

    public IQueryProvider Provider => _rows.Provider;

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
