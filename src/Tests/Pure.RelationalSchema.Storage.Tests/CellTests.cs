using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Materialized.Bool;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage.Tests;

public sealed record CellTests
{
    [Fact]
    public void InitializeCorrectly()
    {
        IString cellValue = new RandomString(new UShort(10));

        ICell cell = new Cell(cellValue);

        Assert.True(
            new MaterializedBool(new EqualCondition(cellValue, cell.Value)).Value
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new Cell(new RandomString(new UShort(10))).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new Cell(new RandomString(new UShort(10))).ToString()
        );
    }
}
