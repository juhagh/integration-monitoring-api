using IntegrationMonitoringApi.Data;
using IntegrationMonitoringApi.Domain;
using IntegrationMonitoringApi.Services;
using Microsoft.EntityFrameworkCore;


namespace IntegrationMonitoringApi.Tests;

public class IntegrationEndpointServiceTests
{
    [Fact]
    public async Task AddEndpointAsync_AddsEntityToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var newEndpoint = new IntegrationEndpoint
        {
            Name = "TestEndpoint",
            Description = "Test Description"
        };

        using var context = new ApplicationDbContext(options);
        var service = new IntegrationEndpointService(context);
        
        // Act
        var result = await service.AddEndpoint(newEndpoint);
        
        // Assert
        using var verificationContext = new ApplicationDbContext(options);
        var persistedEndpoint = await verificationContext
            .IntegrationEndpoints
            .FindAsync(result.IntegrationEndpointId);
        
        Assert.NotNull(persistedEndpoint);
        Assert.Equal(1, await verificationContext.IntegrationEndpoints.CountAsync());
        Assert.Equal("TestEndpoint", persistedEndpoint.Name);
        Assert.Equal("Test Description", persistedEndpoint.Description);
    }

    [Fact]
    public async Task UpdateEndpointAsync_ExistingEntity_UpdatesValues()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var originalEndpoint = new IntegrationEndpoint
        {
            Name = "TestEndpoint",
            Description = "Test Description"
        };

        using (var context = new ApplicationDbContext(options))
        {
            context.IntegrationEndpoints.Add(originalEndpoint);
            await context.SaveChangesAsync();
        }
        var id = originalEndpoint.IntegrationEndpointId;

        var updatedEndpoint = new IntegrationEndpoint
        {
            Name = "UpdatedTestEndpoint",
            Description = "Updated Test Description"
        };

        using var serviceContext = new ApplicationDbContext(options);
        var service = new IntegrationEndpointService(serviceContext);

        // Act
        var result = await service.UpdateEndpointAsync(
            id,
            updatedEndpoint);

        // Assert
        Assert.True(result);

        using var verificationContext = new ApplicationDbContext(options);

        var persistedEndpoint = await verificationContext
            .IntegrationEndpoints
            .FindAsync(id);

        Assert.NotNull(persistedEndpoint);
        Assert.Equal(1, await verificationContext.IntegrationEndpoints.CountAsync());
        Assert.Equal("UpdatedTestEndpoint", persistedEndpoint.Name);
        Assert.Equal("Updated Test Description", persistedEndpoint.Description);
    }

    [Fact]
    public async Task DeleteEndpointAsync_ExistingEntity_RemovesEntity()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        var originalEndpoint = new IntegrationEndpoint
        {
            Name = "TestEndpoint",
            Description = "Test Description"
        };
        
        using (var context = new ApplicationDbContext(options))
        {
            context.IntegrationEndpoints.Add(originalEndpoint);
            await context.SaveChangesAsync();
        }
        
        var id = originalEndpoint.IntegrationEndpointId;
        
        using var serviceContext = new ApplicationDbContext(options);
        var service = new IntegrationEndpointService(serviceContext);
        
        // Act
        var result = await service.DeleteEndpointById(
            id);
        
        // Assert
        Assert.True(result);
        
        using var verificationContext = new ApplicationDbContext(options);
        var persistedEndpoint = await verificationContext
            .IntegrationEndpoints
            .FindAsync(id);
        
        Assert.Null(persistedEndpoint);
        Assert.Equal(0, await verificationContext.IntegrationEndpoints.CountAsync());
    }

    [Fact]
    public async Task UpdateEndpointAsync_NonExistingEntity_ReturnsFalse()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        using var serviceContext = new ApplicationDbContext(options);
        var service = new IntegrationEndpointService(serviceContext);

        var updatedEndpoint = new IntegrationEndpoint
        {
            Name = "UpdatedTestEndpoint",
            Description = "Updated Test Description"
        };
        
        // Act
        var result = await service.UpdateEndpointAsync(
            999,
            updatedEndpoint);

        // Assert
        Assert.False(result);

        using var verificationContext = new ApplicationDbContext(options);

        var persistedEndpoint = await verificationContext
            .IntegrationEndpoints
            .FindAsync(999);

        Assert.Null(persistedEndpoint);
        Assert.Equal(0, await verificationContext.IntegrationEndpoints.CountAsync());
    }
}
