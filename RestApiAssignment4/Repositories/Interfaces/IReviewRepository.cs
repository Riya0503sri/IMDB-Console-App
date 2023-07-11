using RestApiAssignment4.Models.Entities;
using System.Collections.Generic;

namespace RestApiAssignment4.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        IEnumerable<Review> Get(int movieId);
        Review Get(int id, int movieId);
        int Create(Review review);
        void Update(Review review);
        void Delete(int id);
    }
}
