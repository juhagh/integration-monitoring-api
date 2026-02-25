using IntegrationMonitoringApi.Controllers;

namespace IntegrationMonitoringApi.Services;

public class IntegrationEndpointService
{
    private readonly List<IntegrationEndpoint> _endpoints =
    [
        new IntegrationEndpoint()
        {
            IntegrationEndpointId = 1,
            Name = "Payment Gateway"
        },

        new IntegrationEndpoint()
        {
            IntegrationEndpointId = 2,
            Name = "Notification Gateway"
        },

        new IntegrationEndpoint()
        {
            IntegrationEndpointId = 3,
            Name = "SNMP Endpoint"
        }
    ];

    public List<IntegrationEndpoint> GetAllEndpoints()
    {
        return _endpoints;
    }

    public IntegrationEndpoint? GetEndpointById(int id)
    {
        var endpoint = _endpoints.FirstOrDefault(e => e.IntegrationEndpointId == id);
        return endpoint;
    }
    
}