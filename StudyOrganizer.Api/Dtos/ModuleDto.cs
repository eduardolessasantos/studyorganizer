namespace StudyOrganizer.Api.Dtos
{
    public class ModuleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<DisciplineDto> Disciplines { get; set; } = new List<DisciplineDto>();
    }
}
