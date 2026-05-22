# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

All `dotnet` commands must be run from the `./src` directory.

```bash
dotnet restore
dotnet build --no-restore -warnaserror /p:RunAnalyzers=true
dotnet format --verify-no-changes             # check code style (CI enforces this)
dotnet format                                  # auto-fix code style
dotnet test --no-build --verbosity normal      # run xUnit tests
dotnet pack --configuration Release -p:PackageVersion=<version> --output .
```

## Architecture

This is a **concrete implementation NuGet library** — three `sealed record` types that implement the interfaces defined in `Pure.RelationalSchema.Storage.Abstractions`:

- `Cell` — implements `ICell`; wraps a single `IString` value (from `Pure.Primitives.Abstractions`)
- `Row` — implements `IRow`; holds an `IReadOnlyDictionary<IColumn, ICell>` mapping schema columns to cells
- `StoredSchemaDataset` — implements `IStoredSchemaDataSet`; an `IReadOnlyDictionary<ITable, IStoredTableDataSet>` bound to an `ISchema`

`ToString()` and `GetHashCode()` throw `NotSupportedException` on all three types — hash code computation is the responsibility of companion packages.

**Multi-targeting:** net7.0, net8.0, net9.0, net10.0. `IsAotCompatible = true` — no reflection-based APIs may be introduced.

**No `EnablePackageValidation`** is configured on this library.

**Publishing:** triggered by pushing a semver tag (e.g. `1.2.3`). The tag becomes the `PackageVersion`. The package is published to both GitHub Packages and NuGet.org.

## Code Style

Enforced via `.editorconfig` and `dotnet format --verify-no-changes` in CI:

- No `var` — always use explicit types
- No expression-bodied methods or constructors — use block bodies
- Properties and indexers use expression bodies
- File-scoped namespaces (`namespace Foo;`)
- Private fields: `_camelCase`; public instance fields are disallowed (enforced as error)
- Max line length: 90 characters
- `using` directives outside namespace, `System.*` sorted first

## Commit Messages

Do not mention Claude or AI assistance in commit messages.
