using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage;

public sealed record StoredSchemaDataset : IStoredSchemaDataSet
{
    public StoredSchemaDataset(
        IReadOnlyDictionary<ITable, IStoredTableDataSet> tablesDatasets
    )
    {
        TablesDatasets = tablesDatasets;
    }

    public IReadOnlyDictionary<ITable, IStoredTableDataSet> TablesDatasets { get; }

    public override string ToString()
    {
        throw new NotSupportedException();
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }
}
