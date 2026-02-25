using IntegrationMonitoringApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationMonitoringApi.Controllers;

[ApiController]
[Route("[controller]")]
public class IntegrationEndpointsController : ControllerBase
{

    private readonly IntegrationEndpointService _integrationEndpointService;
    public IntegrationEndpointsController(IntegrationEndpointService integrationEndpointService)
    {
        _integrationEndpointService = integrationEndpointService;
    }
    
    
    // GET: api/integrationendpoints
    [HttpGet]
    public ActionResult<IEnumerable<IntegrationEndpoint>> GetIntegrationEndpoints()
    {
        var _endpoints = _integrationEndpointService.GetAllEndpoints();
        return Ok(_endpoints);
    }
    
    // GET /integrationendpoints/{id}
    [HttpGet("{id:int}")]
    public ActionResult<IntegrationEndpoint> GetIntegrationEndpointById(int id)
    {
        var _endpoint = _integrationEndpointService.GetEndpointById(id);
        if (_endpoint != null)
        {
            return Ok(_endpoint);
        }
        else
        {
            return NotFound($"End point {id} not found!");    
        }
    }
}