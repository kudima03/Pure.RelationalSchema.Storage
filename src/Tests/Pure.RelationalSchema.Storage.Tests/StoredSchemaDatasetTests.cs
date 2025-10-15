using System.Collections;
using Pure.Collections.Generic;
using Pure.HashCodes;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Random;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.HashCodes;
using Pure.RelationalSchema.Storage.Tests.Fakes;

namespace Pure.RelationalSchema.Storage.Tests;

public sealed record StoredSchemaDatasetTests
{
    [Fact]
    public void InitializeSchemaCorrectly()
    {
        ISchema schema = new RandomSchema();
        IStoredSchemaDataSet schemaDataset = new StoredSchemaDataset(
            schema,
            new Dictionary<ITable, ITable, IStoredTableDataSet>(
                schema.Tables,
                x => x,
                x => new FakeTableDataset(x),
                x => new TableHash(x)
            )
        );

        Assert.True(
            new SchemaHash(schema).SequenceEqual(new SchemaHash(schemaDataset.Schema))
        );
    }

    [Fact]
    public void InitializeCountCorrectly()
    {
        ISchema schema = new RandomSchema();
        IStoredSchemaDataSet schemaDataset = new StoredSchemaDataset(
            schema,
            new Dictionary<ITable, ITable, IStoredTableDataSet>(
                schema.Tables,
                x => x,
                x => new FakeTableDataset(x),
                x => new TableHash(x)
            )
        );

        Assert.Equal(schema.Tables.Count(), schemaDataset.Count);
    }

    [Fact]
    public void InitializeKeysCorrectly()
    {
        ISchema schema = new RandomSchema();
        IStoredSchemaDataSet schemaDataset = new StoredSchemaDataset(
            schema,
            new Dictionary<ITable, ITable, IStoredTableDataSet>(
                schema.Tables,
                x => x,
                x => new FakeTableDataset(x),
                x => new TableHash(x)
            )
        );

        Assert.True(
            new AggregatedHash(schema.Tables.Select(x => new TableHash(x))).SequenceEqual(
                new AggregatedHash(schemaDataset.Keys.Select(x => new TableHash(x)))
            )
        );
    }

    [Fact]
    public void InitializeValuesCorrectly()
    {
        ISchema schema = new RandomSchema();
        IStoredSchemaDataSet schemaDataset = new StoredSchemaDataset(
            schema,
            new Dictionary<ITable, ITable, IStoredTableDataSet>(
                schema.Tables,
                x => x,
                x => new FakeTableDataset(x),
                x => new TableHash(x)
            )
        );

        Assert.True(
            new AggregatedHash(
                schema.Tables.Select(x => new StoredTableDataSetHash(
                    new FakeTableDataset(x)
                ))
            ).SequenceEqual(
                new AggregatedHash(
                    schemaDataset.Values.Select(x => new StoredTableDataSetHash(x))
                )
            )
        );
    }

    [Fact]
    public void IndexingOperatorWorkCorrectly()
    {
        ISchema schema = new RandomSchema();
        IStoredSchemaDataSet schemaDataset = new StoredSchemaDataset(
            schema,
            new Dictionary<ITable, ITable, IStoredTableDataSet>(
                schema.Tables,
                x => x,
                x => new FakeTableDataset(x),
                x => new TableHash(x)
            )
        );

        Assert.True(
            new StoredTableDataSetHash(
                new FakeTableDataset(schema.Tables.First())
            ).SequenceEqual(
                new StoredTableDataSetHash(schemaDataset[schema.Tables.First()])
            )
        );
    }

    [Fact]
    public void ContainsWorkCorrectly()
    {
        ISchema schema = new RandomSchema();
        IStoredSchemaDataSet schemaDataset = new StoredSchemaDataset(
            schema,
            new Dictionary<ITable, ITable, IStoredTableDataSet>(
                schema.Tables,
                x => x,
                x => new FakeTableDataset(x),
                x => new TableHash(x)
            )
        );

        Assert.True(schemaDataset.ContainsKey(schema.Tables.First()));
    }

    [Fact]
    public void TryGetValueWorkCorrectly()
    {
        ISchema schema = new RandomSchema();
        IStoredSchemaDataSet schemaDataset = new StoredSchemaDataset(
            schema,
            new Dictionary<ITable, ITable, IStoredTableDataSet>(
                schema.Tables,
                x => x,
                x => new FakeTableDataset(x),
                x => new TableHash(x)
            )
        );

        Assert.True(
            new StoredTableDataSetHash(
                new FakeTableDataset(schema.Tables.First())
            ).SequenceEqual(
                new StoredTableDataSetHash(
                    schemaDataset.TryGetValue(
                        schema.Tables.First(),
                        out IStoredTableDataSet? value
                    )
                        ? value
                        : throw new ArgumentNullException()
                )
            )
        );
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        ISchema schema = new RandomSchema();
        IEnumerable schemaDataset = new StoredSchemaDataset(
            schema,
            new Dictionary<ITable, ITable, IStoredTableDataSet>(
                schema.Tables,
                x => x,
                x => new FakeTableDataset(x),
                x => new TableHash(x)
            )
        );

        int i = 0;

        foreach (object table in schemaDataset)
        {
            i++;
        }

        Assert.Equal(schema.Tables.Count(), i);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new StoredSchemaDataset(
                new RandomSchema(),
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
                new RandomSchema(),
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
