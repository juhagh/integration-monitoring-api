# IntegrationMonitoringApi

A layered ASP.NET Core Web API demonstrating clean architecture principles, async request handling, EF Core persistence, and API contract separation using DTOs.

The API manages integration endpoints and is intentionally built with a simple domain to focus on engineering practices rather than domain complexity.

---

## Architecture Overview

Request flow:

HTTP Request
→ Controller (DTO boundary + validation)
→ Service layer (application logic)
→ DbContext (EF Core, Unit of Work)
→ SQLite database

Key architectural decisions:

* **DTO boundary** to decouple API contract from persistence model
* **DataAnnotations validation** with automatic 400 responses via `[ApiController]`
* **Async end-to-end** using `ToListAsync`, `SaveChangesAsync`, etc.
* **Scoped DbContext** per request for safe change tracking
* **Startup seeding** for initial data population
* **EF Core migrations** for database schema versioning

---

## Features

### Read

* `GET /integrationendpoints`
* `GET /integrationendpoints/{id}`
* Returns 404 when endpoint not found

### Create

* `POST /integrationendpoints`
* Returns 201 Created with Location header
* Input validation enforced (Name required)

### Delete

* `DELETE /integrationendpoints/{id}`
* Returns 204 NoContent on success
* Returns 404 if not found

---

## Persistence

* EF Core with SQLite
* Database created via migrations
* Seed data applied at application startup

To apply migrations:

```bash
dotnet ef database update
```

---

## How to Run

```bash
dotnet restore
dotnet run --launch-profile https
```

Navigate to:

```
https://localhost:7090/integrationendpoints
```

---

## Engineering Focus

This project intentionally demonstrates:

* Separation of concerns
* Proper REST semantics (201, 204, 404)
* Prevention of over-posting via DTOs
* Validation at the API boundary
* Async scalability patterns
* Incremental architectural improvement via Git history

---

## Next Steps

* Add PUT endpoint
* Implement optimistic concurrency (RowVersion)
* Introduce unit tests
* Add logging and error handling middleware
