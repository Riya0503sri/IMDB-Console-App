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
    public class ActorService:IActorService
    {
        private readonly IActorRepository _actorRepository;
        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }
        public List<ActorResponse> GetByMovieId(int id)
        {
            var actors = _actorRepository.GetByMovieId(id);
            return actors.Select(x => new ActorResponse
            {
                Id = x.Id,
                Name = x.Name,
                Gender = x.Gender,
                Dob = x.Dob,
                Bio = x.Bio
            }).ToList();
        }
        public List<ActorResponse> Get()
        {
            var actors = _actorRepository.Get();
            return actors.Select(x => new ActorResponse
            {
                Id = x.Id,
                Name = x.Name,
                Gender = x.Gender,
                Dob = x.Dob,
                Bio = x.Bio
            }).ToList();
        }

        public ActorResponse Get(int id)
        {
            var actor = _actorRepository.Get(id);
            if (actor == null)
            {
                return null;
            }
            return new ActorResponse
            {
                Id = actor.Id,
                Name = actor.Name,
                Gender = actor.Gender,
                Dob = actor.Dob,
                Bio = actor.Bio
            };
        }

        public int Create(ActorRequest actorRequest)
        {
            Validate(actorRequest.Name, actorRequest.Dob, actorRequest.Gender, actorRequest.Bio);
           
            var newActor = new Actor
            {
                Name = actorRequest.Name,
                Gender = actorRequest.Gender,
                Dob = actorRequest.Dob,
                Bio = actorRequest.Bio
            };
            return _actorRepository.Create(newActor);
            

        }

        public void Update(int id, ActorRequest actorRequest)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id should be greater than 0");
            }
            Validate(actorRequest.Name, actorRequest.Dob, actorRequest.Gender, actorRequest.Bio);
            if (_actorRepository.Get(id) == null)
            {
                throw new ArgumentException("id not present");
            }
            var actor = new Actor
            {
                Id = id,
                Name = actorRequest.Name,
                Gender = actorRequest.Gender,
                Dob = actorRequest.Dob,
                Bio = actorRequest.Bio
            };
            _actorRepository.Update(actor);
        }

        public void Delete(int id)
        {
            if (_actorRepository.Get(id) == null)
            {
                throw new ArgumentException("id not present");
            }
            _actorRepository.Delete(id);
        }

        public static void Validate(string name, DateTime dob, string gender, string bio)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Enter actor name");
            }

            if (dob > DateTime.Now)
            {
                throw new ArgumentException("Enter a valid year");
            }
            if (string.IsNullOrEmpty(gender))
            {
                throw new ArgumentException("Enter Gender");
            }
            if (string.IsNullOrEmpty(bio))
            {
                throw new ArgumentException("Please Enter bio");
            }
            if (dob > DateTime.Now)
            {
                throw new ArgumentException("Enter valid year");
            }

            if (!(gender.ToLower().Equals("m") || gender.ToLower().Equals("f")))
            {
                throw new ArgumentException("Enter valid Gender");
            }

        }
    }
}
