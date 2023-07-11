using Microsoft.Extensions.DependencyInjection;
using RestApiAssignment4.Tests.MockResources;
using TechTalk.SpecFlow;

namespace RestApiAssignment4.Tests.Steps
{
    [Scope(Feature = "Producer")]
    [Binding]
    public class ProducerSteps : BaseSteps
    {
        public ProducerSteps(CustomWebApplicationFactory factory) : base(factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped(_ => ProducerMock.ProducerRepoMock.Object);
            });
        }))
        {
        }


        [BeforeScenario]
        public static void Mocks()
        {
            ProducerMock.MockGetAll();
        }
    }
}
