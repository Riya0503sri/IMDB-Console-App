using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Models.Requests;
using RestApiAssignment4.Models.Responses;
using RestApiAssignment4.Repositories.Interfaces;
using RestApiAssignment4.Services.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RestApiAssignment4.Services
{
    public class ReviewService:IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieService _movieService;
        public ReviewService(IReviewRepository reviewRepository, IMovieService movieService)
        {
            _reviewRepository = reviewRepository;
            _movieService = movieService;
        }

        public List<ReviewResponse> Get(int movieId)
        {
            Validate(movieId);
            var reviews = _reviewRepository.Get(movieId);
            return reviews.Select(x => new ReviewResponse
            {
                Id = x.Id,
                Message = x.Message
            }).ToList();
        }

        public ReviewResponse Get(int id, int movieId)
        {
            Validate(movieId);
            var review = _reviewRepository.Get(id, movieId);
            if (review == null)
            {
                throw new ArgumentException("Id is not present");
            }
            return new ReviewResponse
            {
                Id = review.Id,
                Message = review.Message
            };
        }

        public int Create(ReviewRequest reviewRequest, int movieId)
        {
            Validate(movieId, reviewRequest.Message);
            var newReview = new Review
            {
                MovieId = movieId,
                Message = reviewRequest.Message
            };
            return _reviewRepository.Create(newReview);
            

        }

        public void Update(int id, ReviewRequest reviewRequest, int movieId)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id should be greater than 0");
            }
            Validate(movieId, reviewRequest.Message);
            var review = _reviewRepository.Get(id, movieId);
            if (review == null)
            {
                throw new ArgumentException("Given review Id is not present corresponding to movie Id");
            }
            review = new Review
            {
                Id = id,
                MovieId = movieId,
                Message = reviewRequest.Message
            };
            _reviewRepository.Update(review);
        }

        public void Delete(int id, int movieId)
        {
            var review = _reviewRepository.Get(id, movieId);
            if (review == null)
            {
                throw new ArgumentException("Given review Id is not present corresponding to movie Id");
            }
            _reviewRepository.Delete(id);
        }

        public void Validate(int movieId, string message)
        {
            Validate(movieId);
            if (movieId <= 0)
            {
                throw new ArgumentException("Id should be greater than 0");
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Enter comment");
            }
        }

        public void Validate(int movieId)
        {
            var result = _movieService.Get(movieId);
            if (result == null)
            {
                throw new ArgumentException("Invalid Movie Id");
            }
        }
    }
}
