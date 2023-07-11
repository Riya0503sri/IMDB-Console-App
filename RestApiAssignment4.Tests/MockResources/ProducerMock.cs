using Moq;
using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Repositories.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RestApiAssignment4.Tests.MockResources
{
    public class ProducerMock
    {
        public static readonly Mock<IProducerRepository> ProducerRepoMock = new Mock<IProducerRepository>();

        public static readonly List<Producer> ListOfProducers = new List<Producer>
        {
            new Producer
            {
                Id = 1,
                Name = "TestName",
                Gender = "M",
                Dob = new DateTime(2002, 02, 02),
                Bio = "TestBio"
            }
        };

        public static void MockGetAll()
        {
            ProducerRepoMock.Setup(x => x.Get()).Returns(ListOfProducers);
            ProducerRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => ListOfProducers.SingleOrDefault(a => a.Id == id));

            ProducerRepoMock.Setup(x => x.Create(It.IsAny<Producer>()));

            ProducerRepoMock.Setup(x => x.Update(It.IsAny<Producer>()));


            ProducerRepoMock.Setup(x => x.Delete(It.Is<int>(id => id == 1)));
        }
    }
}
