using Microsoft.AspNetCore.Mvc.Testing;
using RestApiAssignment4.Tests.MockResources;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace RestApiAssignment4.Tests.Steps
{
    [Scope(Feature = "Movie")]
    [Binding]
    public class MovieSteps : BaseSteps
    {
        public MovieSteps(WebApplicationFactory<TestStartup> factory) : base(factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped(_ => MovieMock.MovieRepoMock.Object);
                services.AddScoped(_ => MovieMock.ActorRepoMock.Object);
                services.AddScoped(_ => MovieMock.GenreRepoMock.Object);
                services.AddScoped(_ => MovieMock.ProducerRepoMock.Object);
            });
        }))
        {
        }


        [BeforeScenario]
        public static void Mocks()
        {
            MovieMock.MockGetAll();
        }
    }
}
