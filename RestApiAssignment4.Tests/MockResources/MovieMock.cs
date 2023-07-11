using Moq;
using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Repositories.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RestApiAssignment4.Tests.MockResources
{
    public class MovieMock
    {
        public static readonly Mock<IMovieRepository> MovieRepoMock = new Mock<IMovieRepository>();
        public static readonly Mock<IProducerRepository> ProducerRepoMock = new Mock<IProducerRepository>();
        public static readonly Mock<IActorRepository> ActorRepoMock = new Mock<IActorRepository>();
        public static readonly Mock<IGenreRepository> GenreRepoMock = new Mock<IGenreRepository>();
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
        private static readonly List<Actor> ListOfActors = new List<Actor>
        {
            new Actor
            {
                Id = 1,
                Name = "TestName",
                Bio = "TestBio",
                Dob = new DateTime(2002,02,02),
                Gender = "M"
            }
        };
        private static readonly List<Producer> ListOfProducers = new List<Producer>
        {
            new Producer
            {
                Id = 1,
                Name = "TestName",
                Gender = "M",
                Dob = new DateTime(2002, 02, 02),
                Bio = "TestBio"
            }
        };
        private static readonly List<Genre> ListOfGenres = new List<Genre>
        {
            new Genre
            {
                Id = 1,
                Name = "TestName"
            }
        };
        private static readonly List<int> ActorIds = new List<int> { 1 };
        private static readonly List<int> GenreIds = new List<int> { 1 };

        public static void MockGetAll()
        {
            ProducerRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => ListOfProducers.SingleOrDefault(p => p.Id == id));
            ProducerRepoMock.Setup(x => x.Get()).Returns(ListOfProducers);

            ActorRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => ListOfActors.SingleOrDefault(a => a.Id == id));
            ActorRepoMock.Setup(x => x.Get()).Returns(ListOfActors);
            ActorRepoMock.Setup(x => x.GetByMovieId(It.IsAny<int>())).Returns(ListOfActors.Where(a => ActorIds.Contains(a.Id)).ToList());

            GenreRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => ListOfGenres.SingleOrDefault(g => g.Id == id));
            GenreRepoMock.Setup(x => x.Get()).Returns(ListOfGenres);
            GenreRepoMock.Setup(x => x.GetByMovieId(It.IsAny<int>())).Returns(ListOfGenres.Where(g => GenreIds.Contains(g.Id)).ToList());

            MovieRepoMock.Setup(x => x.Get()).Returns(ListOfMovies);
            MovieRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => ListOfMovies.SingleOrDefault(m => m.Id == id));

            MovieRepoMock.Setup(x => x.Create(It.IsAny<Movie>(), It.IsAny<List<int>>(), It.IsAny<List<int>>()));

            MovieRepoMock.Setup(x => x.Update(It.IsAny<Movie>(), It.IsAny<List<int>>(), It.IsAny<List<int>>()));

            MovieRepoMock.Setup(x => x.Delete(It.Is<int>(id => id == 1)));
        }
    }
}
