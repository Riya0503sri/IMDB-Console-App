using RestApiAssignment4.Models.Entities;
using System.Collections.Generic;

namespace RestApiAssignment4.Repositories.Interfaces
{
    public interface IActorRepository
    {
        IEnumerable<Actor> Get();
        Actor Get(int id);
        int Create(Actor actor);
        void Update(Actor actor);
        void Delete(int id);
        List<Actor> GetByMovieId(int id);
    }
}
