# Copilot Plan Mode Prompt — Task Management API

Paste everything below into VS Code Copilot Chat (Plan/Agent mode).

---

## Prompt

I'm building an ASP.NET Core Web API called **TaskManagementAPI**. This is an API-only project — no frontend for now. I'm coming from Java, so I understand OOP, interfaces, and layered architecture, but I'm new to .NET/C# idioms (LINQ, dependency injection, EF Core).

Before writing any code, **create a step-by-step implementation plan** that I can review and approve. Don't generate the whole project at once — break it into phases, and after each phase, pause so I can test and confirm before moving to the next.

### Tech stack
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core + SQL Server (SQL Server Express / LocalDB)
- Swagger / OpenAPI for documentation
- xUnit for testing

### Target folder structure

```
TaskManagementAPI/
├── Controllers/
├── Models/
├── Data/
├── DTOs/
├── Services/
├── Repositories/
├── Interfaces/
├── Migrations/
└── Tests/
```

### Architecture
Strict layered flow, one responsibility per layer:

```
Client → Controller → Service → Repository → DbContext → Database
```

- Controllers: handle HTTP only, no business logic
- Services: business logic (e.g. `CompleteTask()`, `SearchTasks()`)
- Repositories: data access only, implemented against interfaces (`ITaskRepository`, `ICategoryRepository`)
- DTOs: never expose EF entities directly through the API

### Domain model

**TaskItem**
- Id
- Title (required, max length 100)
- Description
- DueDate
- Completed (bool)
- Priority (enum: Low/Medium/High)
- CategoryId (FK)

**Category**
- Id
- Name

Relationship: one Category → many TaskItems (navigation property both directions).

### Planned phases (please structure the plan around these, adjusting as needed)

1. **Scaffold the project** — create the ASP.NET Core Web API (.NET 8) with Swagger enabled, no auth yet, matching the folder structure above. Confirm it runs and Swagger UI loads.
2. **Models** — create `TaskItem` and `Category` with the fields above and the FK relationship.
3. **EF Core setup** — install EF Core + SQL Server + Tools packages, create `ApplicationDbContext` with `DbSet<TaskItem>` and `DbSet<Category>`, configure connection string, run initial migration and update the database.
4. **Repository layer** — `IRepository`/`ITaskRepository`/`ICategoryRepository` interfaces + implementations wrapping `ApplicationDbContext`.
5. **Service layer** — `ITaskService`/`ICategoryService` + implementations that call repositories and contain business logic (e.g. `CompleteTask`, `SearchTasks`).
6. **DTOs** — request/response DTOs for Task and Category (don't leak internal-only fields); set up mapping between entities and DTOs.
7. **Controllers — Tasks CRUD** — `GET /api/tasks`, `GET /api/tasks/{id}`, `POST /api/tasks`, `PUT /api/tasks/{id}`, `DELETE /api/tasks/{id}`, using services and DTOs only.
8. **Controllers — Categories CRUD** — `GET/POST/DELETE /api/categories`, with tasks referencing categories via foreign key.
9. **Validation** — data annotations (`[Required]`, `[StringLength]`, etc.) on DTOs, return proper `400 Bad Request` with validation error details.
10. **Search & filtering** — `GET /api/tasks?search=homework` using LINQ (`Where`, `Contains`).
11. **Sorting/filtering by query params** — `?priority=High`, `?completed=true`, `?dueDate=today`, using `OrderBy`.
12. **Swagger polish** — XML doc comments / annotations so Swagger UI clearly documents all endpoints, request/response shapes, and status codes.
13. **Unit tests** — xUnit tests in `Tests/` covering `CreateTask`, `DeleteTask`, `SearchTasks`, `CompleteTask`, and a few controller/service-level tests (aim for ~10–15 tests), using mocked repositories/services where appropriate.

### How I want you to work
- Explain any C#/.NET-specific concept that differs from Java as you introduce it (e.g., DI container, LINQ, `async`/`await`, EF Core migrations) — brief inline explanations, not full tutorials.
- Use dependency injection throughout (`builder.Services.AddScoped<...>()`), don't manually `new` up services/repositories in controllers.
- Prefer explicit, readable LINQ over clever one-liners.
- After each phase, tell me exactly what to run (`dotnet run`, `dotnet ef migrations add ...`, `dotnet test`, etc.) and what I should see if it worked.
- Flag any decision that needs my input (e.g., naming, connection string location, whether to use AutoMapper vs manual mapping) instead of assuming silently.

Start by proposing the phase breakdown and asking me any clarifying questions before scaffolding the project.
