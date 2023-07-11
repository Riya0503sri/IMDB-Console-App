using Microsoft.AspNetCore.Mvc.Testing;
using RestApiAssignment4.Tests.MockResources;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RestApiAssignment4.Tests.Steps
{
    [Scope(Feature = "Review")]
    [Binding]
    public class ReviewSteps : BaseSteps
    {
        public ReviewSteps(WebApplicationFactory<TestStartup> factory) : base(factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped(_ => ReviewMock.ReviewRepoMock.Object);
                services.AddScoped(_ => ReviewMock.MovieRepoMock.Object);
            });
        }))
        {
        }


        [BeforeScenario]
        public static void Mocks()
        {
            ReviewMock.MockGetAll();
            
        }
    }
}
