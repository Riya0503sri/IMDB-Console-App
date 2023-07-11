using RestApiAssignment4.Models.Entities;
using System.Collections.Generic;

namespace RestApiAssignment4.Repositories.Interfaces
{
    public interface IProducerRepository
    {
        IEnumerable<Producer> Get();
        Producer Get(int id);
        int Create(Producer producer);
        void Update(Producer producer);
        void Delete(int id);
    }
}
