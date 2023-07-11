using System;

namespace RestApiAssignment4.Models.Requests
{
    public class ActorRequest
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Bio { get; set; }
    }
}
