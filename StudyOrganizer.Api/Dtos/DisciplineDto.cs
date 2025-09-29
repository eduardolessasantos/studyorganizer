namespace StudyOrganizer.Api.Dtos
{
    public class DisciplineDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ModuleId { get; set; }
        public ICollection<SubtopicDto> Subtopics { get; set; } = new List<SubtopicDto>();
    }
}
