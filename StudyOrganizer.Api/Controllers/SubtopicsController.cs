using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyOrganizer.Api.Dtos;
using StudyOrganizer.Core.Entities;
using StudyOrganizer.Data;

namespace StudyOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubtopicsController : ControllerBase
{
    private readonly StudyContext _ctx;
    public SubtopicsController(StudyContext ctx) => _ctx = ctx;

    // DTO para a resposta
    public class SubtopicResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int DisciplineId { get; set; }
        public StudyStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Notes { get; set; }
        public string? MaterialUrl { get; set; }
        public int? MasteryLevel { get; set; }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? disciplineId)
    {
        var query = _ctx.Subtopics.AsNoTracking().AsQueryable();
        if (disciplineId.HasValue)
            query = query.Where(s => s.DisciplineId == disciplineId.Value);

        var list = await query
            .OrderBy(s => s.Id)
            .ToListAsync();

        var dtoList = list.Select(s => new SubtopicResponseDto
        {
            Id = s.Id,
            Description = s.Description,
            DisciplineId = s.DisciplineId,
            Status = s.Status,
            StartDate = s.StartDate,
            EndDate = s.EndDate,
            Notes = s.Notes,
            MaterialUrl = s.MaterialUrl,
            MasteryLevel = s.MasteryLevel
        }).ToList();

        return Ok(dtoList);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var s = await _ctx.Subtopics.FindAsync(id);
        if (s is null) return NotFound();

        var dto = new SubtopicResponseDto
        {
            Id = s.Id,
            Description = s.Description,
            DisciplineId = s.DisciplineId,
            Status = s.Status,
            StartDate = s.StartDate,
            EndDate = s.EndDate,
            Notes = s.Notes,
            MaterialUrl = s.MaterialUrl,
            MasteryLevel = s.MasteryLevel
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SubtopicCreateDto dto)
    {
        var sub = new Subtopic
        {
            DisciplineId = dto.DisciplineId,
            Description = dto.Description,
            MasteryLevel = dto.MasteryLevel,
            MaterialUrl = dto.MaterialUrl,
            Status = StudyStatus.NotStarted
        };
        _ctx.Subtopics.Add(sub);
        await _ctx.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = sub.Id }, sub);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, SubtopicUpdateDto dto)
    {
        var sub = await _ctx.Subtopics.FirstOrDefaultAsync(x => x.Id == id);
        if (sub is null) return NotFound();
        if (dto.Description != null)
            sub.Description = dto.Description;
        if (dto.MasteryLevel.HasValue)
            sub.MasteryLevel = dto.MasteryLevel;
        if (dto.MaterialUrl != null)
            sub.MaterialUrl = dto.MaterialUrl;
        if (dto.Notes != null)
            sub.Notes = dto.Notes;
        if (dto.Status.HasValue)
            sub.Status = (StudyStatus)dto.Status.Value;
        if (dto.StartDate.HasValue)
            sub.StartDate = dto.StartDate.Value;
        if (dto.EndDate.HasValue)
            sub.EndDate = dto.EndDate.Value;
        if (dto.Content != null)
            sub.Content = dto.Content;
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sub = await _ctx.Subtopics.FirstOrDefaultAsync(x => x.Id == id);
        if (sub is null) return NotFound();
        _ctx.Subtopics.Remove(sub);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}