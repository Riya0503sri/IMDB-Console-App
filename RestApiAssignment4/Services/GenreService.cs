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
    public class GenreService:IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository geneRepository)
        {
            _genreRepository = geneRepository;
        }
        public List<GenreResponse> GetByMovieId(int id)
        {
            var genres = _genreRepository.GetByMovieId(id);
            return genres.Select(x => new GenreResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
        public List<GenreResponse> Get()
        {
            var genres = _genreRepository.Get();
            return genres.Select(x => new GenreResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public GenreResponse Get(int id)
        {
            var genre = _genreRepository.Get(id);
            if (genre == null)
            {
                return null;
            }
            return new GenreResponse
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public int Create(GenreRequest genreRequest)
        {
            Validate(genreRequest.Name);
            var newGenre = new Genre
            {
                Name = genreRequest.Name,
            };
            return _genreRepository.Create(newGenre);
            /*
            var genreResponse= _genreRepository.Create(newGenre);
            if (genreResponse == null)
            {
                return null;
            }
            else
            {
                return new GenreResponse
                {
                    Id = genreResponse.Id,
                    Name = genreResponse.Name,
                };
            }
            */

        }

        public void Update(int id, GenreRequest genreRequest)
        {

            if (id <= 0)
            {
                throw new ArgumentException("Id should be greater than 0");
            }
            Validate(genreRequest.Name);
            if(_genreRepository.Get(id)==null)
            {
                throw new ArgumentException("id not present");
            }
            var genre = new Genre
            {
                Id = id,
                Name = genreRequest.Name,
            };
            _genreRepository.Update(genre);
        }

        public void Delete(int id)
        {
            if (_genreRepository.Get(id) == null)
            {
                throw new ArgumentException("id not present");
            }
            _genreRepository.Delete(id);
        }

        public static void Validate(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Enter Genre name");
            }

            if (name.Any(char.IsDigit))
            {
                throw new ArgumentException("Genre name should not have numbers");
            }
        }
    }
}
