---
name: add-endpoint
displayName: "Add Endpoint"
description: "Use when adding a new backend API endpoint, including controller route, request/response models, service logic, repository operations, and validation."
user-invocable: true
---

# Add Endpoint Skill

Use this skill when adding a new REST API endpoint to the finance management backend.

## What it helps with

- identifying the right layer for the change:
  - `API/Controllers/`
  - `Application/Features/`
  - `InfraStructure/Repository/`
  - `Domain/Entity/`
- designing request/response DTOs and validation
- wiring dependency injection and service registration
- implementing repository operations and DbContext changes
- keeping the new endpoint aligned with existing conventions

## Workflow

1. Confirm the endpoint intent:
   - route path and HTTP method
   - feature area (transactions, transactions, tags, auth, etc.)
   - request body, query parameters, and response payload
   - authorization requirement
2. Place the controller action in the correct API controller or create a new controller.
3. Add request/response models or DTOs in `Application/Features/<Feature>/`.
4. Add validation using existing `FluentValidation` conventions.
5. Add Mapster mapping if the endpoint requires DTO-to-entity translation.
6. Implement business logic in `Application/Features/<Feature>/Services`.
7. Add or update repository methods in `InfraStructure/Repository/`.
8. Update domain models only when the data shape changes.
9. Add tests or example HTTP requests if the repository already has them.

## Repo conventions

- `API/Program.cs` configures authentication, rate limiting, correlation ID middleware, global exception handling, metrics, and health checks.
- `Application/DependencyInjections.cs` registers application services.
- `InfraStructure/DependencyInjections.cs` registers repositories, `AppDbContext`, and infrastructure services.
- Validators are discovered via Scrutor scanning from `TransactionDto`.
- New endpoint logic should use `FluentValidation` and be wired through the service layer rather than placed directly in controllers.

## Use when

- adding new controllers or routes
- expanding business logic for transactions, transactions, tags, or auth
- adding data access logic and repository support
- extending the API surface with new request/response payloads
