using Soenneker.Pipedrive.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Pipedrive.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides cached clients for Pipedrive API v2.
/// </summary>
public interface IPipedriveOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets a client configured with the access token in <c>Pipedrive:ApiKey</c>.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The cached Pipedrive client.</returns>
    ValueTask<PipedriveOpenApiClient> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a client configured for a specific Pipedrive OAuth access token.
    /// </summary>
    /// <param name="apiKey">The Pipedrive OAuth access token.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the configured client.</returns>
    ValueTask<PipedriveOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default);
}
