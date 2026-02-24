namespace IntegrationMonitoringApi.Controllers;

public class IntegrationEndpoint
{
    // Maybe refactor this to use GUID instead
    public int IntegrationEndpointId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}