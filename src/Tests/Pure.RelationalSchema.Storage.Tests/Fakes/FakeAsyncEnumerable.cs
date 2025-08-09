using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage.Tests.Fakes;

public sealed record FakeAsyncEnumerable : IAsyncEnumerable<IRow>
{
    private readonly IEnumerable<IRow> _rows;

    public FakeAsyncEnumerable(IEnumerable<IRow> rows)
    {
        _rows = rows;
    }

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
}
