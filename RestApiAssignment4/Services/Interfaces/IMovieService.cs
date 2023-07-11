using RestApiAssignment4.Models.Requests;
using RestApiAssignment4.Models.Responses;
using System.Collections.Generic;

namespace RestApiAssignment4.Services.Interfaces
{
    public interface IMovieService
    {
        List<MovieResponse> Get();
        MovieResponse Get(int id);
        int Create(MovieRequest movie);
        void Update(int id, MovieRequest movie);
        
        void Delete(int id);
    }
}
