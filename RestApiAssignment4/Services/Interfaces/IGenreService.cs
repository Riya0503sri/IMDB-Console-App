using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Models.Requests;
using RestApiAssignment4.Models.Responses;
using System.Collections.Generic;

namespace RestApiAssignment4.Services.Interfaces
{
    public interface IGenreService
    {
        List<GenreResponse> Get();
        GenreResponse Get(int id);
        int Create(GenreRequest genreRequest);
        void Update(int id, GenreRequest genreRequest);
        void Delete(int id);
        List<GenreResponse> GetByMovieId(int id);
    }
}
