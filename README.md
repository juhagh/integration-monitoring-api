# IntegrationMonitoringApi
This is a simple learning project built with ASP.NET Core.
The program exposes REST endpoints for managing integration endpoints.
The project currently uses in-memory data storage.

## Features
Get Endpoints
- `GET /integrationendpoints`
- `GET /integrationendpoints/{id}`
- Returns 404 when endpoint not found
- In-memory data storage (no database yet)

## How to Run

```bash
dotnet restore
dotnet run --launch-profile https
```

Once program is running, navigate to:

https://localhost:7090/integrationendpoints for all endpoints

or

https://localhost:7090/integrationendpoints/{id} for a particular endpoint based on the endpoint id.

## Next Steps

- Introduce service layer
- Add EF Core persistence
- Add POST, PUT, DELETE
- Add unit tests

