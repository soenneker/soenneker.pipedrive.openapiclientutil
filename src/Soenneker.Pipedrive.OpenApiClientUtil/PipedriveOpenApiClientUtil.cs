using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Dictionaries.Singletons;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Pipedrive.HttpClients.Abstract;
using Soenneker.Pipedrive.OpenApiClientUtil.Abstract;
using Soenneker.Pipedrive.OpenApiClient;

namespace Soenneker.Pipedrive.OpenApiClientUtil;

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

        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient)
        {
            BaseUrl = httpClient.BaseAddress!.ToString().TrimEnd('/')
        };

        return new PipedriveOpenApiClient(requestAdapter);
    }

    public ValueTask<PipedriveOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        var apiKey = _configuration.GetValueStrict<string>("Pipedrive:ApiKey");

        return Get(apiKey, cancellationToken);
    }

    public ValueTask<PipedriveOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default)
    {
        return _clients.Get(apiKey, cancellationToken);
    }

    public void Dispose()
    {
        _clients.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _clients.DisposeAsync();
    }
}
