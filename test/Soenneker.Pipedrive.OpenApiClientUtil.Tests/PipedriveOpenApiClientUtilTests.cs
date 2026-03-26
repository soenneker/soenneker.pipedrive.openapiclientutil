using Soenneker.Pipedrive.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Pipedrive.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class PipedriveOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly IPipedriveOpenApiClientUtil _openapiclientutil;

    public PipedriveOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IPipedriveOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
