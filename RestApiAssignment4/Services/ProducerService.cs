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
    public class ProducerService:IProducerService
    {
        private readonly IProducerRepository _producerRepository;

        public ProducerService(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }
        public List<ProducerResponse> Get()
        {
            var producers = _producerRepository.Get();
            return producers.Select(x => new ProducerResponse
            {
                Id = x.Id,
                Name = x.Name,
                Gender = x.Gender,
                Dob = x.Dob,
                Bio = x.Bio
            }).ToList();
        }

        public ProducerResponse Get(int id)
        {
            var producer = _producerRepository.Get(id);
            if (producer == null)
            {
                return null;
            }
            return new ProducerResponse
            {
                Id = producer.Id,
                Name = producer.Name,
                Gender = producer.Gender,
                Dob = producer.Dob,
                Bio = producer.Bio
            };
        }

        public int Create(ProducerRequest producerRequest)
        {
            Validate(producerRequest.Name, producerRequest.Dob, producerRequest.Gender, producerRequest.Bio);
            var newProducer = new Producer
            {
                Name = producerRequest.Name,
                Gender = producerRequest.Gender,
                Dob = producerRequest.Dob,
                Bio = producerRequest.Bio
            };
            return _producerRepository.Create(newProducer);
            

        }

        public void Update(int id, ProducerRequest producerRequest)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id should be greater than 0");
            }
            Validate(producerRequest.Name, producerRequest.Dob, producerRequest.Gender, producerRequest.Bio);
            if (_producerRepository.Get(id) == null)
            {
                throw new ArgumentException("id is not present");
            }
            var producer = new Producer
            {
                Id = id,
                Name = producerRequest.Name,
                Gender = producerRequest.Gender,
                Dob = producerRequest.Dob,
                Bio = producerRequest.Bio
            };
            _producerRepository.Update(producer);
        }

        public void Delete(int id)
        {
            if (_producerRepository.Get(id) == null)
            {
                throw new ArgumentException("id is not present");
            }
            _producerRepository.Delete(id);
        }

        public static void Validate(string name, DateTime dob, string gender, string bio)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Enter producer name");
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

            if (!(gender.ToLower().Equals("m") || gender.ToLower().Equals("f")))
            {
                throw new ArgumentException("Enter valid Gender");
            }
        }
    }
}
