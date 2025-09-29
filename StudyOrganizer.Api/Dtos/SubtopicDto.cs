using StudyOrganizer.Core.Entities;

namespace StudyOrganizer.Api.Dtos
{
    public class SubtopicDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int DisciplineId { get; set; }
        public StudyStatus Status { get; set; } = StudyStatus.NotStarted;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Notes { get; set; }
        public string? MaterialUrl { get; set; }
        public int? MasteryLevel { get; set; }
    }
}
