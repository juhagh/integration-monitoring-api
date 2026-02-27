using IntegrationMonitoringApi.Data;
using IntegrationMonitoringApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace IntegrationMonitoringApi.Services;

public class IntegrationEndpointService
{

    private readonly ApplicationDbContext _context;
    public IntegrationEndpointService(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public async Task<List<IntegrationEndpoint>> GetAllEndpoints()
    {
        return await _context.IntegrationEndpoints
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IntegrationEndpoint?> GetEndpointById(int id)
    {
        return await _context.IntegrationEndpoints.FindAsync(id);
    }

    public async Task<IntegrationEndpoint> AddEndpoint(IntegrationEndpoint endpoint)
    {
        var entity = await _context.IntegrationEndpoints.AddAsync(endpoint);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> DeleteEndpointById(int id)
    {
        var endpoint = await _context.IntegrationEndpoints.FindAsync(id);
        if (endpoint == null)
        {
            return false;
        }
        
        _context.IntegrationEndpoints.Remove(endpoint);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateEndpointAsync(int id, IntegrationEndpoint updated)
    {
        var endpoint = await _context.IntegrationEndpoints.FindAsync(id);
        if (endpoint == null)
        {
            return false;
        }

        endpoint.Name = updated.Name;
        endpoint.Description = updated.Description;
        
        await _context.SaveChangesAsync();
        return true;
    }
}