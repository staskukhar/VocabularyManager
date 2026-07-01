# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build
dotnet build VocabularyManager.sln

# Run tests
dotnet test
dotnet test --filter "FullyQualifiedName~SomeTestClass"   # single test class

# Run locally (requires PostgreSQL + Keycloak running)
dotnet run --project src/VocabularyManager.Api
dotnet run --project src/VocabularyManager.BlazorApp

# Full stack via Docker
docker-compose up --build

# EF Core migrations
dotnet ef migrations add <Name> --project src/VocabularyManager.Infrastructure --startup-project src/VocabularyManager.Api
dotnet ef database update --project src/VocabularyManager.Infrastructure --startup-project src/VocabularyManager.Api
```

## Architecture

Clean Architecture with 5 projects and a strict dependency rule:

```
Core  <--  UseCases  <--  Infrastructure  <--  Api
                                          <--  BlazorApp
```

- **Core** — Domain entities (`Vocabulary`, `Word`, `Meaning`) and Ardalis `Specification` classes. No framework dependencies.
- **UseCases** — Application services (`VocabularyStorageManager`, `WordStorageManager`, `MeaningStorageManager`, `AnkiExportService`), interfaces, DTOs, FluentValidation validators, and Oxford Dictionary parsers (currently misplaced — should be in `Infrastructure`; tracked in `ARCHITECTURE_IMPROVEMENTS.md`).
- **Infrastructure** — EF Core (`VocabularyContext`), `GenericRepository<T>` (extends Ardalis `RepositoryBase<T>`), entity configurations, `DashboardMetricsProvider`, and `DependencyInjection.cs`.
- **Api** — ASP.NET Core Web API controllers, FluentValidation action filter, exception handlers, JWT/Keycloak middleware, Swagger.
- **BlazorApp** — Blazor WASM SPA. Uses `HttpService` (wraps `HttpClient`) and `HttpPathBuilder` to call the API. Keycloak OIDC auth via `Microsoft.AspNetCore.Components.WebAssembly.Authentication`. i18n with `.resx` files (English + Ukrainian).

**API request flow:**
```
HTTP Request → Controller → [ValidationFilter] → StorageManager → Repository (Ardalis spec) → EF Core → PostgreSQL
```

**BlazorApp request flow:**
```
User action → Razor Page/Component → HttpService → HttpPathBuilder → API
```

## Key Conventions

**C# style (from Cursor rules):**
- No `var` — always use explicit types
- Self-descriptive names; only abbreviate in lambda expressions
- Small, focused functions
- Prefer direct calls over reflection

**EF Core:** Use Fluent API for all entity configuration — no data annotations on domain entities.

**Authorization:** Two Keycloak realm roles — `Administrator` (full CRUD + Vocabulary Craft tool) and `User` (read-only). Roles are mapped to `ClaimTypes.Role` during JWT validation in `Program.cs`.

**Repositories:** Always use Ardalis `Specification` classes (in `Core`) when querying — do not write raw LINQ in services.

## Known Architecture Issues

See `ARCHITECTURE_IMPROVEMENTS.md` for the full backlog. Active violations to be aware of:

- `BlazorApp` references `UseCases` (should use its own response models; this inflates the WASM bundle with `AngleSharp` and `FluentValidation`)
- Oxford parsers are in `UseCases/Services/Parsers/` but belong in `Infrastructure`
- `DbContext` registration and `MigrateAsync()` live in `Api/Program.cs` but should move to `Infrastructure.DependencyInjection`
- Controllers bind raw domain entities instead of dedicated request DTOs

## Infrastructure Setup

- **Database:** PostgreSQL 16 — configure connection string in `appsettings.Development.json`
- **Auth:** Keycloak 26.0 — realm `vocabulary-manager`, client `vocabulary-app`. Import realm from `keycloak/realm-import.json` for local setup
- **Docker:** `.env` / `.env.example` at root configures ports and credentials for all 5 services (`db`, `keycloak-db-init`, `keycloak`, `api`, `client`)
