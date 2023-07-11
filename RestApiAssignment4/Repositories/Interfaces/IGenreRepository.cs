using RestApiAssignment4.Models.Entities;
using System.Collections.Generic;

namespace RestApiAssignment4.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> Get();
        Genre Get(int id);
        int Create(Genre genre);
        void Update(Genre genre);
        void Delete(int id);
        List<Genre> GetByMovieId(int id);
    }
}
