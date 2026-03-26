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
    ValueTask<PipedriveOpenApiClient> Get(CancellationToken cancellationToken = default);
}
