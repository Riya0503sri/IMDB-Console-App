using System.Collections.Generic;
using System;
using Moq;
using RestApiAssignment4.Repositories.Interfaces;
using RestApiAssignment4.Models.Entities;
using System.Linq;

namespace RestApiAssignment4.Tests.MockResources
{
    public class ActorMock
    {
        public static readonly Mock<IActorRepository> ActorRepoMock = new Mock<IActorRepository>();

        public static readonly List<Actor> ListOfActors = new List<Actor>
        {
            new Actor
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
            ActorRepoMock.Setup(x => x.Get()).Returns(ListOfActors);
            ActorRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => ListOfActors.SingleOrDefault(a => a.Id == id));
            ActorRepoMock.Setup(x => x.Create(It.IsAny<Actor>()));

            ActorRepoMock.Setup(x => x.Update(It.IsAny<Actor>()));

            ActorRepoMock.Setup(x => x.Delete(It.Is<int>(id => id == 1)));
        }
    }
}
