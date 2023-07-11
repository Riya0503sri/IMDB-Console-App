using Moq;
using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RestApiAssignment4.Tests.MockResources
{
    public class GenreMock
    {
        public static readonly Mock<IGenreRepository> GenreRepoMock = new Mock<IGenreRepository>();

        public static readonly List<Genre> ListOfGenres = new List<Genre>
        {
            new Genre
            {
                Id = 1,
                Name = "TestName"
            }
        };

        public static void MockGetAll()
        {
            GenreRepoMock.Setup(x => x.Get()).Returns(ListOfGenres);
            GenreRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => ListOfGenres.SingleOrDefault(a => a.Id == id));

            GenreRepoMock.Setup(x => x.Create(It.IsAny<Genre>()));

            GenreRepoMock.Setup(x => x.Update(It.IsAny<Genre>()));


            GenreRepoMock.Setup(x => x.Delete(It.Is<int>(id => id == 1)));
        }
    }
}
