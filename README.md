# Pure.RelationalSchema.Storage

Concrete implementations of stored relational schema data types for the **Pure** ecosystem.

[![.NET build & test](https://github.com/kudima03/Pure.RelationalSchema.Storage/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.RelationalSchema.Storage/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.RelationalSchema.Storage/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.RelationalSchema.Storage/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.RelationalSchema.Storage)](https://www.nuget.org/packages/Pure.RelationalSchema.Storage)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`Pure.RelationalSchema.Storage` provides concrete `sealed record` implementations of the storage abstractions defined in [`Pure.RelationalSchema.Storage.Abstractions`](https://github.com/kudima03/Pure.RelationalSchema.Storage.Abstractions/tree/0.1.0-preview.4.1.0). It gives you ready-to-use, immutable data containers for representing the stored state of a relational schema — cells, rows, and full schema datasets.

## Types

| Type | Interface | Description |
|------|-----------|-------------|
| `Cell` | `ICell` | A single stored value, backed by an `IString` |
| `Row` | `IRow` | A mapping of `IColumn` → `ICell` representing one data row |
| `StoredSchemaDataset` | `IStoredSchemaDataSet` | A read-only dictionary of `ITable` → `IStoredTableDataSet`, paired with its `ISchema` |

All three types are `sealed record`s. `ToString()` and `GetHashCode()` throw `NotSupportedException` by design — use [`Pure.RelationalSchema.Storage.HashCodes`](https://github.com/kudima03/Pure.RelationalSchema.Storage.HashCodes) for hash code computation.

## Design Principles

- **Immutable** — All properties are init-only; mutation is not possible after construction.
- **AOT-compatible** — `IsAotCompatible = true`; no reflection-based APIs are used.
- **Thin** — No logic beyond structural storage; behaviour such as hashing and querying is delegated to companion packages.

## Dependencies

- [`Pure.RelationalSchema.Storage.Abstractions`](https://github.com/kudima03/Pure.RelationalSchema.Storage.Abstractions/tree/0.1.0-preview.4.1.0) — storage interfaces (`ICell`, `IRow`, `IStoredSchemaDataSet`, `IStoredTableDataSet`)
- [`Pure.Primitives.Abstractions`](https://github.com/kudima03/Pure.Primitives.Abstractions/tree/4.3.0) — base primitive interfaces for the Pure ecosystem (`IString`, `INumber<T>`, `IDate`, etc.)

## Target Frameworks

- .NET 7
- .NET 8
- .NET 9
- .NET 10

## Installation

```bash
dotnet add package Pure.RelationalSchema.Storage
```

## Usage

```csharp
IString value = ...;   // from Pure.Primitives.Abstractions
IColumn column = ...;  // from Pure.RelationalSchema.Abstractions

Cell cell = new Cell(value);
Row row = new Row(new Dictionary<IColumn, ICell> { [column] = cell });
StoredSchemaDataset dataset = new StoredSchemaDataset(
    schema,
    new Dictionary<ITable, IStoredTableDataSet> { [table] = tableDataset });
```
