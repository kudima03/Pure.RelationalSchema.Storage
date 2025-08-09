using System.Collections;
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

public sealed record StoredTableDatasetTests
{
    [Fact]
    public void InitializeCorrectly()
    {
        ITable table = new Table.Table(
            new RandomString(new UShort(10)),
            [new Column.Column(new RandomString(new UShort(10)), new IntColumnType())],
            []
        );

        IReadOnlyDictionary<IColumn, ICell> dictionary = new Dictionary<
            IColumn,
            IColumn,
            ICell
        >(
            table.Columns,
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

        IAsyncEnumerable<IRow> asyncRows = new FakeAsyncEnumerable(rows.AsEnumerable());

        IStoredTableDataSet tableDataSet = new StoredTableDataset(
            table,
            new EnumerableQuery<IRow>(rows),
            asyncRows
        );

        Assert.Equal(table, tableDataSet.TableSchema);
        Assert.Equal(rows.Provider, tableDataSet.Provider);
        Assert.Equal(asyncRows, tableDataSet);
    }

    [Fact]
    public async Task EnumeratesAsync()
    {
        ITable table = new Table.Table(
            new RandomString(new UShort(10)),
            [new Column.Column(new RandomString(new UShort(10)), new IntColumnType())],
            []
        );

        IReadOnlyDictionary<IColumn, ICell> dictionary = new Dictionary<
            IColumn,
            IColumn,
            ICell
        >(
            table.Columns,
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

        IStoredTableDataSet tableDataSet = new StoredTableDataset(
            table,
            rows.Expression,
            rows.Provider,
            new FakeAsyncEnumerable(rows.AsEnumerable())
        );

        Assert.Equal(rows, await tableDataSet.ToArrayAsync());
    }

    [Fact]
    public void EnumeratesAsTyped()
    {
        ITable table = new Table.Table(
            new RandomString(new UShort(10)),
            [new Column.Column(new RandomString(new UShort(10)), new IntColumnType())],
            []
        );

        IReadOnlyDictionary<IColumn, ICell> dictionary = new Dictionary<
            IColumn,
            IColumn,
            ICell
        >(
            table.Columns,
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

        IStoredTableDataSet tableDataSet = new StoredTableDataset(
            table,
            rows.Expression,
            rows.Provider,
            new FakeAsyncEnumerable(rows.AsEnumerable())
        );

        Assert.Equal(rows, tableDataSet);
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        ITable table = new Table.Table(
            new RandomString(new UShort(10)),
            [new Column.Column(new RandomString(new UShort(10)), new IntColumnType())],
            []
        );

        IReadOnlyDictionary<IColumn, ICell> dictionary = new Dictionary<
            IColumn,
            IColumn,
            ICell
        >(
            table.Columns,
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

        IEnumerable tableDataSet = new StoredTableDataset(
            table,
            rows.Expression,
            rows.Provider,
            new FakeAsyncEnumerable(rows.AsEnumerable())
        );

        ICollection<IRow> collection = [];

        foreach (object row in tableDataSet)
        {
            collection.Add((IRow)row);
        }

        Assert.Equal(rows, collection);
    }

    [Fact]
    public void TypeIsAlwaysIRow()
    {
        ITable table = new Table.Table(
            new RandomString(new UShort(10)),
            [new Column.Column(new RandomString(new UShort(10)), new IntColumnType())],
            []
        );

        IQueryable<int> rows = Enumerable.Empty<int>().AsQueryable();

        IStoredTableDataSet tableDataSet = new StoredTableDataset(
            table,
            rows.Expression,
            rows.Provider,
            new FakeAsyncEnumerable([])
        );

        Assert.Equal(typeof(IRow), tableDataSet.ElementType);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        ITable table = new Table.Table(
            new RandomString(new UShort(10)),
            [new Column.Column(new RandomString(new UShort(10)), new IntColumnType())],
            []
        );
        _ = Assert.Throws<NotSupportedException>(() =>
            new StoredTableDataset(
                table,
                new EnumerableQuery<IRow>([]),
                new FakeAsyncEnumerable([])
            ).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        ITable table = new Table.Table(
            new RandomString(new UShort(10)),
            [new Column.Column(new RandomString(new UShort(10)), new IntColumnType())],
            []
        );
        _ = Assert.Throws<NotSupportedException>(() =>
            new StoredTableDataset(
                table,
                new EnumerableQuery<IRow>([]),
                new FakeAsyncEnumerable([])
            ).ToString()
        );
    }
}
