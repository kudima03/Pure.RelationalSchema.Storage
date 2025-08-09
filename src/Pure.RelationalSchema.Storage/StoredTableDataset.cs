using System.Collections;
using System.Linq.Expressions;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage;

public sealed record StoredTableDataset : IStoredTableDataSet
{
    private readonly IAsyncEnumerable<IRow> _asyncEnumerable;

    public StoredTableDataset(
        ITable tableSchema,
        IQueryProvider provider,
        IAsyncEnumerable<IRow> asyncEnumerator
    )
    {
        TableSchema = tableSchema;
        Expression = Expression.Constant(this);
        Provider = provider;
        _asyncEnumerable = asyncEnumerator;
    }

    public StoredTableDataset(
        ITable tableSchema,
        Expression expression,
        IQueryProvider provider,
        IAsyncEnumerable<IRow> asyncEnumerable
    )
    {
        TableSchema = tableSchema;
        Expression = expression;
        Provider = provider;
        _asyncEnumerable = asyncEnumerable;
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
        CancellationToken cancellationToken = default
    )
    {
        return _asyncEnumerable.GetAsyncEnumerator(cancellationToken);
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
