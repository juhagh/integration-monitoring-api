# IntegrationMonitoringApi

Backend-focused ASP.NET Core Web API for managing integration endpoints.

This project demonstrates practical backend development in C#/.NET using a layered structure, DTO-based API contracts, async CRUD operations, EF Core persistence, SQLite, startup seeding, and service-layer unit tests.

## Overview

The API manages integration endpoints through a simple domain so the emphasis stays on backend engineering fundamentals rather than business complexity.

The project is designed to demonstrate:
- clean separation between controllers, DTOs, services, and persistence
- REST-style endpoint design and response handling
- async request handling with EF Core
- correct use of scoped `DbContext`
- validation at the API boundary
- service-layer testing with EF Core InMemory

## Tech stack

- C#
- .NET
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- xUnit
- EF Core InMemory provider
- OpenAPI

## Architecture

Request flow:

`HTTP request -> Controller -> Service layer -> DbContext -> SQLite database`

### Design decisions

- **Controllers** handle HTTP concerns and map between DTOs and domain models
- **DTOs** define the API contract and help prevent over-posting
- **Service layer** keeps application logic out of controllers
- **EF Core + SQLite** provide persistence and simple local setup
- **Scoped DbContext** aligns with the request lifecycle
- **Startup seeding** makes the API usable immediately after first run
- **OpenAPI support** is enabled in development

## Project structure

```text
IntegrationMonitoringApi.sln
├── src/
│   └── IntegrationMonitoringApi/
│       ├── Controllers/
│       ├── DTOs/
│       ├── Data/
│       ├── Domain/
│       ├── Migrations/
│       ├── Services/
│       └── Program.cs
└── IntegrationMonitoringApi.Tests/
````

## Features

### Endpoints

#### Get all endpoints

`GET /integrationendpoints`

Returns all integration endpoints.

#### Get endpoint by id

`GET /integrationendpoints/{id}`

Returns:

* `200 OK` when found
* `404 Not Found` when the resource does not exist

#### Create endpoint

`POST /integrationendpoints`

Returns:

* `201 Created` when the resource is created successfully
* `400 Bad Request` when validation fails

#### Update endpoint

`PUT /integrationendpoints/{id}`

Returns:

* `204 No Content` when updated successfully
* `404 Not Found` when the resource does not exist

#### Delete endpoint

`DELETE /integrationendpoints/{id}`

Returns:

* `204 No Content` when deleted successfully
* `404 Not Found` when the resource does not exist

## Validation

Validation is applied at the DTO boundary using Data Annotations and `[ApiController]` behaviour.

This keeps invalid requests from reaching deeper layers and ensures the API returns automatic `400 Bad Request` responses for invalid input.

## Persistence

The application uses EF Core with SQLite.

`ApplicationDbContext` exposes the `IntegrationEndpoints` table, and the database is configured in `Program.cs`.

At startup, the application seeds sample endpoint data if the database is empty.

## Testing

The test project covers service-layer behaviour using EF Core's InMemory provider.

Current tests include:

* adding an endpoint
* updating an existing endpoint
* deleting an existing endpoint
* returning `false` when updating a non-existing endpoint

This allows the service layer to be tested independently from the full API and SQLite database.

## Running the project

### Prerequisites

* .NET SDK

### Restore dependencies

```bash
dotnet restore
```

### Apply migrations

```bash
dotnet ef database update --project src/IntegrationMonitoringApi
```

### Run the API

```bash
dotnet run --project src/IntegrationMonitoringApi --launch-profile https
```

## Example requests

### Create

```http
POST /integrationendpoints
Content-Type: application/json

{
  "name": "Billing Gateway",
  "description": "Endpoint for billing integration"
}
```

### Update

```http
PUT /integrationendpoints/1
Content-Type: application/json

{
  "name": "Billing Gateway",
  "description": "Updated description"
}
```

### Delete

```http
DELETE /integrationendpoints/1
```

## What this project demonstrates

This project demonstrates practical backend fundamentals in .NET, including:

* layered API design
* DTO boundaries and over-posting prevention
* dependency injection with scoped services
* async EF Core operations
* `AsNoTracking` for read scenarios
* `FindAsync` for key-based lookup
* idempotent `PUT` basics
* startup seeding
* service-layer unit testing
* clean solution structure

## Next improvements

Areas I would improve next:

* optimistic concurrency with `RowVersion`
* centralized exception handling middleware
* structured logging
* integration tests for the API layer
* pagination and filtering for list endpoints
* stronger environment-based configuration
* GUID-based identifiers as an alternative to integer IDs

## Purpose

This repository is part of my transition into backend development, with a focus on C#/.NET, APIs, persistence, testing, and backend design fundamentals.
