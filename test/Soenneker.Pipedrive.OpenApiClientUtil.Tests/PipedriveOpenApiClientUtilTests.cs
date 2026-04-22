using Soenneker.Pipedrive.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Pipedrive.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class PipedriveOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IPipedriveOpenApiClientUtil _openapiclientutil;

    public PipedriveOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IPipedriveOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
