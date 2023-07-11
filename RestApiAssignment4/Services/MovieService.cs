using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Models.Requests;
using RestApiAssignment4.Models.Responses;
using RestApiAssignment4.Repositories.Interfaces;
using RestApiAssignment4.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RestApiAssignment4.Services
{
    public class MovieService:IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorService _actorService;
        private readonly IProducerService _producerService;
        private readonly IGenreService _genreService;

        public MovieService(IMovieRepository movieRepository, IActorService actorService, IProducerService producerService, IGenreService genreService)
        {
            _movieRepository = movieRepository;
            _actorService = actorService;
            _producerService = producerService;
            _genreService = genreService;
        }

        public List<MovieResponse> Get()
        {
            var movies = _movieRepository.Get();
            var response = movies.Select(x =>
            {
                var actors = _actorService.GetByMovieId(x.Id);

                var genres = _genreService.GetByMovieId(x.Id);

                return new MovieResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    YearOfRelease = x.YearOfRelease,
                    Plot = x.Plot,
                    Actors = actors,
                    Genres = genres,
                    Producer = _producerService.Get(x.ProducerId),
                    PosterURL = x.PosterURL
                };
            }).ToList();
            return response;
        }

        public MovieResponse Get(int id)
        {
            var movie = _movieRepository.Get(id);
            if (movie == null)
            {
                return null;
            }

            var actors = _actorService.GetByMovieId(id);

            var genres = _genreService.GetByMovieId(id);

            var response = new MovieResponse
            {
                Id = movie.Id,
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                Producer = _producerService.Get(movie.ProducerId),
                Actors = actors,
                Genres = genres,
                PosterURL = movie.PosterURL
            };

            return response;
        }
        public int Create(MovieRequest movieRequest)
        {
            Validate(movieRequest.Name, movieRequest.YearOfRelease, movieRequest.Plot, movieRequest.ActorIds, movieRequest.ProducerId, movieRequest.GenreIds, movieRequest.PosterURL);
            var newMovie = new Movie
            {
                Name = movieRequest.Name,
                YearOfRelease = movieRequest.YearOfRelease,
                Plot = movieRequest.Plot,
                ProducerId = movieRequest.ProducerId,
                PosterURL = movieRequest.PosterURL
            };
            return _movieRepository.Create(newMovie, movieRequest.ActorIds, movieRequest.GenreIds);

        }
        public void Update(int id, MovieRequest movieRequest)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id should be greater than 0");
            }
            Validate(movieRequest.Name, movieRequest.YearOfRelease, movieRequest.Plot, movieRequest.ActorIds, movieRequest.ProducerId, movieRequest.GenreIds, movieRequest.PosterURL);
            if (_movieRepository.Get(id) == null)
            {
                throw new ArgumentException("id is not present");
            }
            var movie = new Movie
            {
                Id = id,
                Name = movieRequest.Name,
                YearOfRelease = movieRequest.YearOfRelease,
                Plot = movieRequest.Plot,
                ProducerId = movieRequest.ProducerId,
                PosterURL = movieRequest.PosterURL
            };
            _movieRepository.Update(movie, movieRequest.ActorIds, movieRequest.GenreIds);
        }
       
        public void Delete(int id)
        {
            if (_movieRepository.Get(id) == null)
            {
                throw new ArgumentException("id is not present");
            }
            _movieRepository.Delete(id);

        }

        public void Validate(string name, int? year, string plot, List<int> actorIds, int producerId, List<int> genreIds, string coverImage)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Enter name for movie");
            }
            if (year == null || year.Equals(0))
            {
                throw new ArgumentException("Enter year for movie");
            }
            if (year > DateTime.Now.Year)
            {
                throw new ArgumentException("Enter a valid year");
            }
            if (string.IsNullOrEmpty(plot))
            {
                throw new ArgumentException("Enter plot for movie");
            }

            if (string.IsNullOrEmpty(coverImage))
            {
                throw new ArgumentException("Poster URL should not be empty");
            }

            if (actorIds == null)
            {
                throw new ArgumentException("actorIds should not be empty");
            }
            var actors = _actorService.Get();

            if (!actorIds.Any(actorId => actors.Any(a => a.Id == actorId)))
            {
                throw new ArgumentException("Enter valid Actor ID's");
            }

            var producers = _producerService.Get();
            var producer = producers.SingleOrDefault(p => p.Id == producerId);
            if (producer == null)
            {
                throw new ArgumentException("Enter valid Producer ID");
            }

            if (genreIds == null)
            {
                throw new ArgumentException("genreIds should not be empty");
            }
            var genres = _genreService.Get();

            if (!genreIds.Any(genreId => genres.Any(a => a.Id == genreId)))
            {
                throw new ArgumentException("Enter valid Genre ID's");
            }
        }
    }
}
