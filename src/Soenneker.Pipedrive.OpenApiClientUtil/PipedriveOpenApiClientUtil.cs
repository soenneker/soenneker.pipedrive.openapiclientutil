using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Dictionaries.Singletons;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Pipedrive.HttpClients.Abstract;
using Soenneker.Pipedrive.OpenApiClientUtil.Abstract;
using Soenneker.Pipedrive.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;

namespace Soenneker.Pipedrive.OpenApiClientUtil;

///<inheritdoc cref="IPipedriveOpenApiClientUtil"/>
public sealed class PipedriveOpenApiClientUtil : IPipedriveOpenApiClientUtil
{
    private readonly SingletonDictionary<PipedriveOpenApiClient> _clients;
    private readonly IConfiguration _configuration;
    private readonly IPipedriveOpenApiHttpClient _httpClientUtil;

    public PipedriveOpenApiClientUtil(IPipedriveOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _httpClientUtil = httpClientUtil;
        _configuration = configuration;
        _clients = new SingletonDictionary<PipedriveOpenApiClient>(CreateClient);
    }

    private async ValueTask<PipedriveOpenApiClient> CreateClient(string apiKey, CancellationToken token)
    {
        HttpClient httpClient = await _httpClientUtil.Get(apiKey, token).NoSync();

        string authHeaderName = _configuration["Pipedrive:AuthHeaderName"] ?? "Authorization";
        string authHeaderValueTemplate = _configuration["Pipedrive:AuthHeaderValueTemplate"] ?? "Bearer {token}";
        string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

        var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
            httpClient: httpClient);

        return new PipedriveOpenApiClient(requestAdapter);
    }

    public ValueTask<PipedriveOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        var apiKey = _configuration.GetValueStrict<string>("Pipedrive:ApiKey");

        return Get(apiKey, cancellationToken);
    }

    public ValueTask<PipedriveOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);

        return _clients.Get(apiKey, cancellationToken);
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _clients.Dispose();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _clients.DisposeAsync();
    }
}
