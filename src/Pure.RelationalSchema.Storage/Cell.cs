using Pure.Primitives.Abstractions.String;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage;

public sealed record Cell : ICell
{
    public Cell(IString value)
    {
        Value = value;
    }

    public IString Value { get; }

    public override string ToString()
    {
        throw new NotSupportedException();
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }
}
