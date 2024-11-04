using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace ApiTests;

public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private HttpClient? _httpClient;
    private WebApplicationFactory<Program> _factory = new(); // Added to run tests locally within process.

    protected HttpClient HttpClient
    {
        get
        {
            if (_httpClient == default)
            {
                _httpClient = _factory.CreateClient(); // No need to point to separate host. Tests pass.
            }

            return _httpClient;
        }
    }

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}
