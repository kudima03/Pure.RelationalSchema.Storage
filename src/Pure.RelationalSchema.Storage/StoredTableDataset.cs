using System.Collections;
using System.Linq.Expressions;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage;

public sealed record StoredTableDataset : IStoredTableDataSet
{
    private readonly IAsyncEnumerator<IRow> _asyncEnumerator;

    public StoredTableDataset(ITable tableSchema, IQueryProvider provider, IAsyncEnumerator<IRow> asyncEnumerator)
        : this(tableSchema, Expression.Constant(null, typeof(IQueryable<IRow>)), provider, asyncEnumerator) { }

    public StoredTableDataset(ITable tableSchema, Expression expression, IQueryProvider provider, IAsyncEnumerator<IRow> asyncEnumerator)
    {
        TableSchema = tableSchema;
        Expression = expression;
        Provider = provider;
        _asyncEnumerator = asyncEnumerator;
    }

    public ITable TableSchema { get; }

    public Type ElementType => typeof(IRow);

    public Expression Expression { get; }

    public IQueryProvider Provider { get; }

    public IEnumerator<IRow> GetEnumerator()
    {
        return Provider.Execute<IEnumerable<IRow>>(Expression).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IAsyncEnumerator<IRow> GetAsyncEnumerator(
        CancellationToken cancellationToken = default)
    {
        return _asyncEnumerator;
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }
}
