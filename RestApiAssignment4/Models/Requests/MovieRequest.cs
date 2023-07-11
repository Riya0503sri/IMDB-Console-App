using System.Collections.Generic;

namespace RestApiAssignment4.Models.Requests
{
    public class MovieRequest
    {
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public List<int> ActorIds { get; set; }
        public int ProducerId { get; set; }
        public List<int> GenreIds { get; set; }
        public string PosterURL { get; set; }
    }
}
