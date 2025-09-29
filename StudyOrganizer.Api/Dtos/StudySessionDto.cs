namespace StudyOrganizer.Api.Dtos
{
    public class StudySessionDto
    {
        public int Id { get; set; }
        public int SubtopicId { get; set; }
        public DateTime SessionDate { get; set; }
        public int DurationMinutes { get; set; }
    }
}
