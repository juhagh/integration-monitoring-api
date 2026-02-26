using IntegrationMonitoringApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace IntegrationMonitoringApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<IntegrationEndpoint> IntegrationEndpoints { get; set; }
}