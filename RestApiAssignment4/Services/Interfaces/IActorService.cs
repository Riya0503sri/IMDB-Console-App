using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Models.Requests;
using RestApiAssignment4.Models.Responses;
using System.Collections.Generic;

namespace RestApiAssignment4.Services.Interfaces
{
    public interface IActorService
    {
        List<ActorResponse> Get();
        ActorResponse Get(int id);
        int Create(ActorRequest actorRequest);
        void Update(int id, ActorRequest actorRequest);
        void Delete(int id);
        List<ActorResponse> GetByMovieId(int id);
    }
}
