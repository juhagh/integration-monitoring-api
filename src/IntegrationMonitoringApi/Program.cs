using IntegrationMonitoringApi.Data;
using IntegrationMonitoringApi.Domain;
using IntegrationMonitoringApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add IntegrationEndpointService
builder.Services.AddScoped<IntegrationEndpointService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=integrationmonitoring.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed database with sample endpoint data
using (var scope = app.Services.CreateScope())
{
    var seedManager = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!seedManager.IntegrationEndpoints.Any())
    {
        List<IntegrationEndpoint> seedData =
        [
            new IntegrationEndpoint()
            {
                Name = "Payment Gateway"
            },

            new IntegrationEndpoint()
            {
                Name = "Notification Gateway",
                Description = "Notification Gateway Endpoint"
            },

            new IntegrationEndpoint()
            {
                Name = "SNMP Endpoint"
            }
        ];
        seedManager.IntegrationEndpoints.AddRange(seedData);
        seedManager.SaveChanges();
    }
}

app.Run();
