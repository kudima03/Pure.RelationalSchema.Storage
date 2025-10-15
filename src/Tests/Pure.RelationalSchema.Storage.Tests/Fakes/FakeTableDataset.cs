using System.Collections;
using System.Linq.Expressions;
using Pure.Collections.Generic;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Random;
using Pure.RelationalSchema.Storage.Abstractions;
using String = Pure.Primitives.String.String;

namespace Pure.RelationalSchema.Storage.Tests.Fakes;

public sealed record FakeTableDataset : IStoredTableDataSet
{
    private readonly IQueryable<IRow> _rows;

    public FakeTableDataset()
        : this(new RandomTable()) { }

    public FakeTableDataset(ITable tableSchema)
        : this(
            tableSchema,
            Enumerable
                .Range(0, 100)
                .Select(x => new Row(
                    new Dictionary<IColumn, IColumn, ICell>(
                        tableSchema.Columns,
                        x => x,
                        x => new Cell(new String("TestValue")),
                        x => new ColumnHash(x)
                    )
                ))
        )
    { }

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
            await Task.CompletedTask;
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
