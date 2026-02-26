using IntegrationMonitoringApi.Data;
using IntegrationMonitoringApi.Domain;

namespace IntegrationMonitoringApi.Services;

public class IntegrationEndpointService
{

    private readonly ApplicationDbContext _context;
    public IntegrationEndpointService(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public List<IntegrationEndpoint> GetAllEndpoints()
    {
        return _context.IntegrationEndpoints.ToList();
    }

    public IntegrationEndpoint? GetEndpointById(int id)
    {
        var endpoint = _context.IntegrationEndpoints.FirstOrDefault(e => e.IntegrationEndpointId == id);
        return endpoint;
    }

    public IntegrationEndpoint AddEndpoint(IntegrationEndpoint endpoint)
    {
        var entity = _context.IntegrationEndpoints.Add(endpoint);
        _context.SaveChanges();
        return entity.Entity;
    }

    public bool DeleteEndpointById(int id)
    {
        var endpoint = _context.IntegrationEndpoints.FirstOrDefault(e => e.IntegrationEndpointId == id);
        if (endpoint == null)
        {
            return false;
        }

        _context.IntegrationEndpoints.Remove(endpoint);
        _context.SaveChanges();
        return true;
    }
}