using Microsoft.AspNetCore.Mvc;

namespace IntegrationMonitoringApi.Controllers;

[ApiController]
[Route("[controller]")]
public class IntegrationEndpointsController : ControllerBase
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
    
    // GET: api/integrationendpoints
    [HttpGet]
    public ActionResult<IEnumerable<IntegrationEndpoint>> GetIntegrationEndpoints()
    {
        return _endpoints;
    }
    
    // GET /integrationendpoints/{id}
    [HttpGet("{id:int}")]
    public ActionResult<IntegrationEndpoint> GetIntegrationEndpointById(int id)
    {
        var endpoint = _endpoints.FirstOrDefault(e => e.IntegrationEndpointId == id);
        if (endpoint != null)
        {
            return Ok(endpoint);
        }
        else
        {
            return NotFound($"End point {id} not found!");    
        }
    }
}