using RestApiAssignment4.Tests.MockResources;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace RestApiAssignment4.Tests.Steps
{
    [Scope(Feature = "Actor")]
    [Binding]
    public class ActorSteps : BaseSteps
    {
        public ActorSteps(CustomWebApplicationFactory factory) : base(factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped(_ => ActorMock.ActorRepoMock.Object);
            });
        }))
        {
        }


        [BeforeScenario]
        public static void Mocks()
        {
            ActorMock.MockGetAll();
        }
    }
}
