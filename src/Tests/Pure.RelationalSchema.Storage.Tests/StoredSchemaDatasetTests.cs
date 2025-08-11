using Pure.Collections.Generic;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.Tests.Fakes;

namespace Pure.RelationalSchema.Storage.Tests;

public sealed record StoredSchemaDatasetTests
{
    [Fact]
    public void InitializeCorrectly()
    {
        IReadOnlyCollection<ITable> tables =
        [
            new Table.Table(
                new RandomString(new UShort(10)),
                [
                    new Column.Column(
                        new RandomString(new UShort(10)),
                        new IntColumnType()
                    ),
                ],
                []
            ),
        ];

        IReadOnlyDictionary<IColumn, ICell> dictionary = new Dictionary<
            IColumn,
            IColumn,
            ICell
        >(
            tables.First().Columns,
            x => x,
            _ => new Cell(new RandomString(new UShort(10))),
            x => new ColumnHash(x)
        );

        IQueryable<IRow> rows = new IRow[]
        {
            new Row(dictionary),
            new Row(dictionary),
            new Row(dictionary),
            new Row(dictionary),
            new Row(dictionary),
            new Row(dictionary),
        }.AsQueryable();

        IReadOnlyDictionary<ITable, IStoredTableDataSet> data = new Dictionary<
            ITable,
            ITable,
            IStoredTableDataSet
        >(
            tables,
            x => x,
            x => new FakeTableDataset(tables.First(), rows),
            x => new TableHash(x)
        );

        Assert.Equal(data, new StoredSchemaDataset(data).TablesDatasets);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new StoredSchemaDataset(
                new Dictionary<ITable, ITable, IStoredTableDataSet>(
                    [],
                    x => x,
                    x => new FakeTableDataset(x, []),
                    x => new TableHash(x)
                )
            ).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new StoredSchemaDataset(
                new Dictionary<ITable, ITable, IStoredTableDataSet>(
                    [],
                    x => x,
                    x => new FakeTableDataset(x, []),
                    x => new TableHash(x)
                )
            ).ToString()
        );
    }
}
