using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SolarMonitor.Application.Commands;

namespace SolarMonitor.IntegrationTests;

public class PanelEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;
    private const string ApiKey = "Solar-Monitor-Super-Secret-Key-2026";
    private const string ApiKeyHeaderName = "X-API-KEY";

    public PanelEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Add(ApiKeyHeaderName, ApiKey);
    }

    [Fact]
    public async Task CreatePanel_WithEmptyBrand_ReturnsBadRequest()
    {
        var badCommand = new CreatePanelCommand
        {
            Brand = "",
            Model = ""  
        };

        var response = await _client.PostAsJsonAsync("/api/panels", badCommand);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreatePanel_WithoutApiKey_ReturnsUnauthorized()
    {
        var clientWithoutAuth = _factory.CreateClient();
        
        var command = new CreatePanelCommand
        {
            Brand = "TestBrand",
            Model = "TestModel"  
        };

        var response = await clientWithoutAuth.PostAsJsonAsync("/api/panels", command);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}