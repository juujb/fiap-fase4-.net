using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;

namespace FIAP.IRRIGACAO.API.Tests.Integration
{
    public class ControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ControllerIntegrationTests()
        {
            var clientHandler = new HttpClientHandler();
            _client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri("http://localhost:8080")
            };
        }

        [Fact]
        public async Task GetAccountLogin_ShouldReturn200OK()
        {
            // Act
            var response = await _client.GetAsync("/Account/Login");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetFaucet_ShouldReturn200OK()
        {
            // Act
            var response = await _client.GetAsync("/Faucet");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
