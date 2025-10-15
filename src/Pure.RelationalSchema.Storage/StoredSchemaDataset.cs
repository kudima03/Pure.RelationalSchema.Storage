using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage;

public sealed record StoredSchemaDataset : IStoredSchemaDataSet
{
    private readonly IReadOnlyDictionary<ITable, IStoredTableDataSet> _tablesDatasets;

    public StoredSchemaDataset(
        ISchema schema,
        IReadOnlyDictionary<ITable, IStoredTableDataSet> tablesDatasets
    )
    {
        _tablesDatasets = tablesDatasets;
        Schema = schema;
    }

    public ISchema Schema { get; }

    public IEnumerable<ITable> Keys => _tablesDatasets.Keys;

    public IEnumerable<IStoredTableDataSet> Values => _tablesDatasets.Values;

    public int Count => _tablesDatasets.Count;

    public IStoredTableDataSet this[ITable key] => _tablesDatasets[key];

    public bool ContainsKey(ITable key)
    {
        return _tablesDatasets.ContainsKey(key);
    }

    public bool TryGetValue(
        ITable key,
        [MaybeNullWhen(false)] out IStoredTableDataSet value
    )
    {
        return _tablesDatasets.TryGetValue(key, out value);
    }

    public IEnumerator<KeyValuePair<ITable, IStoredTableDataSet>> GetEnumerator()
    {
        return _tablesDatasets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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
