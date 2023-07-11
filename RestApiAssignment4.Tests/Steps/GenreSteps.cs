using RestApiAssignment4.Tests.MockResources;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace RestApiAssignment4.Tests.Steps
{
    [Scope(Feature = "Genre")]
    [Binding]
    public class GenreSteps : BaseSteps
    {
        public GenreSteps(CustomWebApplicationFactory factory) : base(factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped(_ => GenreMock.GenreRepoMock.Object);
            });
        }))
        {
        }


        [BeforeScenario]
        public static void Mocks()
        {
            GenreMock.MockGetAll();
        }
    }
}
