using System.ComponentModel.DataAnnotations;

namespace IntegrationMonitoringApi.DTOs;

public class CreateIntegrationEndpointDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = default!;
    [StringLength(500)]
    public string? Description { get; set; }
}