# AI Agent Guide for Finance Management Backend

## Purpose
This file helps AI coding agents work effectively in this repository by documenting the main architecture, build commands, and project conventions.

## Solution layout
- `FinanceManager.slnx` is the main solution.
- `API/` is the ASP.NET Core Web API entry point.
- `Application/` contains business logic, feature services, DTOs, and Mapster mapping configuration.
- `Domain/` defines entities, enums, and domain models.
- `InfraStructure/` contains persistence, EF Core context, repositories, services, migrations, and extensions.

## Build and run
- Build solution: `dotnet build FinanceManager.slnx`
- Run API: `dotnet run --project API/API.csproj`
- The API uses .NET 10 (`net10.0`).

## Key runtime behavior
- `API/Program.cs` configures:
  - Serilog request logging
  - Azure AD JWT authentication via `Microsoft.Identity.Web`
  - OpenAPI and Scalar API reference in Development
  - Prometheus metrics endpoints
  - health checks at `/health/live` and `/health/ready`
  - rate limiting, correlation ID middleware, and global exception handling
  - database migrations via `ApplyMigrationsAsync()` on startup

## Dependency injection conventions
- `Application/DependencyInjections.cs` exposes `AddApplication()`.
- `InfraStructure/DependencyInjections.cs` exposes `AddInfrastructure()`.
- Services are registered with scoped lifetimes.
- Validators in `Application` are discovered via Scrutor scanning from `TransactionDto` assembly.

## Persistence and domain
- Infrastructure uses PostgreSQL with `Npgsql` and `AppDbContext`.
- Repositories follow a generic repository pattern plus feature-specific interfaces:
  - `IGenericRepository<>`
  - `ITransactionRepository`, `ITagRepository`, `IUserRepository`
- Domain entities live under `Domain/Entity/`.

## Important files and directories
- `API/Program.cs`
- `API/Controllers/`
- `API/Middlewares/`
- `Application/Features/`
- `InfraStructure/Persistence/Context/`
- `InfraStructure/Repository/`
- `InfraStructure/Services/`

## What agents should assume
- This is a backend service only; there is no frontend project in this workspace.
- The app expects a PostgreSQL connection string under `DefaultConnection`.
- Azure AD settings are required for authentication if using production authorization.
- Existing database migrations and seed data are managed in `InfraStructure/Persistence/Migrations/` and `InfraStructure/Seed/`.

## Use cases for AI assistance
- Fixing API controller logic and routing
- Implementing or extending domain services and repositories
- Adding validation, middleware, or logging features
- Updating dependency injection registration and configuration
- Addressing database schema and migration issues

## Recommended next customization
Create a dedicated `skill` for common backend tasks such as:
- adding new API endpoints
- wiring new service/repository registrations
- generating database migrations

A custom skill for endpoint creation is available at `.github/skills/add-endpoint/`.
