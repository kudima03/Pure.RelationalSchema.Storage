# Changelog

All notable changes to Pure.RelationalSchema.Storage are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [0.1.0-preview.8.0.0] — 2026-05-07

### Changed

- Now depends on `Pure.Primitives.Abstractions` instead of the full
  `Pure.Primitives` package, trimming the transitive dependency footprint
  for consumers.

## [0.1.0-preview.7.0.0] — 2025-12-08

### Added

- Now multi-targets `net7.0`, `net8.0`, `net9.0`, and `net10.0` (previously
  `net9.0` only).
- Package is now marked AOT-compatible (`IsAotCompatible = true`).

## [0.1.0-preview.6.0.0] — 2025-10-15

### Changed

- **Breaking:** `StoredSchemaDataset` no longer exposes a `TablesDatasets`
  property. It now implements `IReadOnlyDictionary<ITable, IStoredTableDataSet>`
  directly (`Keys`, `Values`, `Count`, indexer, `ContainsKey`, `TryGetValue`,
  enumeration), so table datasets are accessed through the type itself
  instead of through a wrapped dictionary.
- **Breaking:** `StoredSchemaDataset` constructor now also requires an
  `ISchema schema` argument, exposed through a new `Schema` property.

## [0.1.0-preview.5.0.0] — 2025-09-26

- Maintenance release: dependency and build updates.

## [0.1.0-preview.4.1.0] — 2025-08-21

- Maintenance release: dependency and build updates.

## [0.1.0-preview.4.0.0] — 2025-08-11

- Maintenance release: dependency and build updates.

## [0.1.0-preview.3.0.0] — 2025-08-11

### Removed

- **Breaking:** `StoredTableDataset` removed. Table datasets are now
  supplied as consumer-provided `IStoredTableDataSet` implementations
  rather than a built-in type.

## [0.1.0-preview.2.0.0] — 2025-08-11

### Fixed

- `StoredTableDataset`'s `Expression` no longer builds a self-referential
  constant expression, avoiding infinite recursion when the dataset is
  queried through a real LINQ provider.

## [0.1.0-preview.1.0.0] — 2025-08-10

### Fixed

- `StoredTableDataset`'s `Expression` now carries the `IQueryable<IRow>`
  element type, fixing incorrect LINQ query construction against the
  dataset.

## [0.1.0-preview.0.1.0] — 2025-08-09

### Added

- Initial release: `Cell`, `Row`, and `StoredSchemaDataset` — immutable,
  AOT-friendly storage records for a relational schema's stored data.
