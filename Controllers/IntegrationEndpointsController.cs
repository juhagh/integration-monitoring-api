using IntegrationMonitoringApi.Domain;
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
            return NotFound($"Endpoint {id} not found!");    
        }
    }
    
    // POST /integrationpoints
    [HttpPost]
    public ActionResult CreateIntegrationEndpoint([FromBody] IntegrationEndpoint endpoint)
    {
        var created = _integrationEndpointService.AddEndpoint(endpoint);
        return CreatedAtAction(nameof(GetIntegrationEndpointById),
            new { id = created.IntegrationEndpointId },
            // new { id = created?.Entity.IntegrationEndpointId },
            created);
    }
    
    // DELETE /integrationendpoints/{id}
    [HttpDelete("{id:int}")]
    public ActionResult DeleteIntegrationEndpoint(int id)
    {
        var result = _integrationEndpointService.DeleteEndpointById(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}