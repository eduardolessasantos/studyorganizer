namespace StudyOrganizer.Api.Dtos
{
    public record SubtopicCreateDto(
        int DisciplineId,
        string Description,
        int? MasteryLevel,
        string? MaterialUrl);


    public record SubtopicUpdateDto(
        string? Description,
        int? MasteryLevel,
        string? MaterialUrl,
        string? Notes,
        int? Status,
        DateTime? StartDate,
        DateTime? EndDate,
        string? Content);
}
