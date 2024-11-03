using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Net.Http;
using Xunit;

namespace ApiTests;

public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private HttpClient? _httpClient;
    private WebApplicationFactory<Program> _factory = new();

    protected HttpClient HttpClient
    {
        get
        {
            if (_httpClient == default)
            {
                _httpClient = _factory.CreateClient();
            }

            return _httpClient;
        }
    }

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}

