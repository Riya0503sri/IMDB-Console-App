using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Models.Requests;
using RestApiAssignment4.Models.Responses;
using System.Collections.Generic;

namespace RestApiAssignment4.Services.Interfaces
{
    public interface IReviewService
    {
        List<ReviewResponse> Get(int movieId);
        ReviewResponse Get(int id, int movieId);
        int Create(ReviewRequest review, int movieId);
        void Update(int id, ReviewRequest review, int movieId);
        void Delete(int id, int movieId);
    }
}
