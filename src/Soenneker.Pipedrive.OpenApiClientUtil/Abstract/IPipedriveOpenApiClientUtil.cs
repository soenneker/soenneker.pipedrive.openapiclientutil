using Soenneker.Pipedrive.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Pipedrive.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IPipedriveOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<PipedriveOpenApiClient> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a client configured for a specific Pipedrive API key.
    /// </summary>
    /// <param name="apiKey">The Pipedrive API key.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the configured client.</returns>
    ValueTask<PipedriveOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default);
}
