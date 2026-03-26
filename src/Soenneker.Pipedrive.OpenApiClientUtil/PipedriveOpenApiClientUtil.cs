using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Pipedrive.HttpClients.Abstract;
using Soenneker.Pipedrive.OpenApiClientUtil.Abstract;
using Soenneker.Pipedrive.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Pipedrive.OpenApiClientUtil;

///<inheritdoc cref="IPipedriveOpenApiClientUtil"/>
public sealed class PipedriveOpenApiClientUtil : IPipedriveOpenApiClientUtil
{
    private readonly AsyncSingleton<PipedriveOpenApiClient> _client;

    public PipedriveOpenApiClientUtil(IPipedriveOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<PipedriveOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Pipedrive:ApiKey");
            string authHeaderValueTemplate = configuration["Pipedrive:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new PipedriveOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<PipedriveOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
