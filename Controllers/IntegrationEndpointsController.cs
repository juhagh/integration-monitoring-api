using IntegrationMonitoringApi.Domain;
using IntegrationMonitoringApi.DTOs;
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
    public async Task<ActionResult<IEnumerable<IntegrationEndpointDto>>> GetIntegrationEndpoints()
    {
        var endpoints = await _integrationEndpointService.GetAllEndpoints();
        var result = endpoints.Select(e =>
            new IntegrationEndpointDto
            {
                IntegrationEndpointId = e.IntegrationEndpointId,
                Name = e.Name,
                Description = e.Description
            }).ToList();
        return Ok(result);
    }
    
    // GET /integrationendpoints/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<IntegrationEndpointDto>> GetIntegrationEndpointById(int id)
    {
        var endpoint = await _integrationEndpointService.GetEndpointById(id);
        if (endpoint != null)
        {
            var result = new IntegrationEndpointDto
            {
                IntegrationEndpointId = endpoint.IntegrationEndpointId,
                Name = endpoint.Name,
                Description = endpoint.Description
            };
            return Ok(result);
        }
        else
        {
            return NotFound($"Endpoint {id} not found!");    
        }
    }
    
    // POST /integrationpoints
    [HttpPost]
    public async Task<ActionResult<IntegrationEndpointDto>> CreateIntegrationEndpoint([FromBody] CreateIntegrationEndpointDto dto)
    {
        IntegrationEndpoint endpoint = new IntegrationEndpoint()
        {
            Name = dto.Name,
            Description = dto.Description
        };
        
        var created = await _integrationEndpointService.AddEndpoint(endpoint);

        IntegrationEndpointDto result = new IntegrationEndpointDto()
        {
            IntegrationEndpointId = created.IntegrationEndpointId,
            Name = created.Name,
            Description = created.Description
        }; 
        
        return CreatedAtAction(
            nameof(GetIntegrationEndpointById),
            new { id = result.IntegrationEndpointId },
            result);
    }
    
    // DELETE /integrationendpoints/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteIntegrationEndpoint(int id)
    {
        var result = await _integrationEndpointService.DeleteEndpointById(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}