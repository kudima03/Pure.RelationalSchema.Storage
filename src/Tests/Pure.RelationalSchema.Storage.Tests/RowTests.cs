using Pure.Collections.Generic;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage.Tests;

public sealed record RowTests
{
    [Fact]
    public void InitializeCorrectly()
    {
        IEnumerable<(IColumn, ICell)> values =
        [
            (
                new Column.Column(new RandomString(new UShort(10)), new IntColumnType()),
                new Cell(new RandomString(new UShort(10)))
            ),
        ];

        IReadOnlyDictionary<IColumn, ICell> dictionary = new Dictionary<
            (IColumn, ICell),
            IColumn,
            ICell
        >(values, x => x.Item1, x => x.Item2, x => new ColumnHash(x));

        IRow row = new Row(dictionary);

        Assert.Equal(dictionary, row.Cells);
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
