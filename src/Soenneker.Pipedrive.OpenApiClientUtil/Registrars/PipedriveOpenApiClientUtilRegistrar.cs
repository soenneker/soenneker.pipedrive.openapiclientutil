using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Pipedrive.HttpClients.Registrars;
using Soenneker.Pipedrive.OpenApiClientUtil.Abstract;

namespace Soenneker.Pipedrive.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class PipedriveOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="PipedriveOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddPipedriveOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddPipedriveOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IPipedriveOpenApiClientUtil, PipedriveOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="PipedriveOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddPipedriveOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddPipedriveOpenApiHttpClientAsSingleton()
                .TryAddScoped<IPipedriveOpenApiClientUtil, PipedriveOpenApiClientUtil>();

        return services;
    }
}
