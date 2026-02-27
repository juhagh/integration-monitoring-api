namespace IntegrationMonitoringApi.DTOs;

public class IntegrationEndpointDto
{
    public int IntegrationEndpointId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}