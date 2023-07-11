using Moq;
using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RestApiAssignment4.Tests.MockResources
{
    public class ReviewMock
    {
        public static readonly Mock<IReviewRepository> ReviewRepoMock = new Mock<IReviewRepository>();
        public static readonly Mock<IMovieRepository> MovieRepoMock = new Mock<IMovieRepository>();
        private static readonly List<Review> ListOfReviews = new List<Review>
        {
            new Review
            {
                Id = 1,
                Message = "TestComment",
                MovieId = 1,
            }
        };

        private static readonly List<Movie> ListOfMovies = new List<Movie>
        {
            new Movie
            {
                Id = 1,
                Name = "TestName",
                YearOfRelease = 2000,
                Plot = "TestPlot",
                ProducerId = 1,
                PosterURL = "TestURL"
            }
        };

        public static void MockGetAll()
        {
            ReviewRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => ListOfReviews.Where(r => r.MovieId == id).ToList());
            ReviewRepoMock.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>())).Returns((int id, int movieId) => ListOfReviews.SingleOrDefault(r => r.Id == id && r.MovieId == movieId));

            ReviewRepoMock.Setup(x => x.Create(It.IsAny<Review>()));

            ReviewRepoMock.Setup(x => x.Update(It.IsAny<Review>()));

            ReviewRepoMock.Setup(x => x.Delete(It.Is<int>(id => id == 1)));

            MovieRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => ListOfMovies.SingleOrDefault(m => m.Id == id));
        }
    }
}
