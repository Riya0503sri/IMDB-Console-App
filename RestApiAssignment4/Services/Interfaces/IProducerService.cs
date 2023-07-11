using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Models.Requests;
using RestApiAssignment4.Models.Responses;
using System.Collections.Generic;

namespace RestApiAssignment4.Services.Interfaces
{
    public interface IProducerService
    {
        List<ProducerResponse> Get();
        ProducerResponse Get(int id);
        int Create(ProducerRequest producerRequest);
        void Update(int id, ProducerRequest producerRequest);
        void Delete(int id);
    }
}
