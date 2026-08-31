[![](https://img.shields.io/nuget/v/soenneker.pipedrive.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.pipedrive.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.pipedrive.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.pipedrive.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.pipedrive.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.pipedrive.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.pipedrive.openapiclientutil/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.pipedrive.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Pipedrive.OpenApiClientUtil

Provides configured Pipedrive API v2 clients and caches one generated client per OAuth access token.

## Installation

```bash
dotnet add package Soenneker.Pipedrive.OpenApiClientUtil
```

## Configuration

```json
{
  "Pipedrive": {
    "ApiKey": "your-oauth-access-token",
    "ClientBaseUrl": "https://api.pipedrive.com/api/v2/"
  }
}
```

`Pipedrive:ApiKey` is the compatibility name for the OAuth access token used by the default bearer configuration.

## Usage

```csharp
using Soenneker.Pipedrive.OpenApiClientUtil.Abstract;
using Soenneker.Pipedrive.OpenApiClientUtil.Registrars;

services.AddPipedriveOpenApiClientUtilAsSingleton();

IPipedriveOpenApiClientUtil pipedrive = serviceProvider
    .GetRequiredService<IPipedriveOpenApiClientUtil>();

var client = await pipedrive.Get(cancellationToken);
var deals = await client.Deals.GetAsync(
    cancellationToken: cancellationToken);
```

For multiple accounts, pass each account's OAuth access token explicitly:

```csharp
PipedriveOpenApiClient accountClient = await pipedrive.Get(
    accessToken,
    cancellationToken);
```

Use `AddPipedriveOpenApiClientUtilAsScoped()` when each application scope should have its own generated-client cache. The authenticated HTTP provider remains shared and is disposed by the service container at shutdown.
