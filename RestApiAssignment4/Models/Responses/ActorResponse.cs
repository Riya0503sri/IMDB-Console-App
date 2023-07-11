using System;

namespace RestApiAssignment4.Models.Responses
{
    public class ActorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Bio { get; set; }
    }
}
