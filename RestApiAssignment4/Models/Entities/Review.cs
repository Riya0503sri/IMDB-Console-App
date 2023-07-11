namespace RestApiAssignment4.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Message { get; set; }
    }
}
