using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SolarMonitor.Application.Commands;

namespace SolarMonitor.IntegrationTests;

public class PanelEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PanelEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
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
}