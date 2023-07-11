using RestApiAssignment4.Models.Entities;
using System.Collections.Generic;

namespace RestApiAssignment4.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> Get();
        Movie Get(int id);
        int Create(Movie movie, List<int> actorIds, List<int> genreIds);
        void Update(Movie movie, List<int> actorIds, List<int> genreIds);
        void Delete(int id);
    }
}
