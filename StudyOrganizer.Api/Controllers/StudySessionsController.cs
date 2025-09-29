using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyOrganizer.Api.Dtos;
using StudyOrganizer.Core.Entities;
using StudyOrganizer.Data;

namespace StudyOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudySessionsController : ControllerBase
{
    private readonly StudyContext _ctx;
    public StudySessionsController(StudyContext ctx) => _ctx = ctx;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? subtopicId)
    {
        var query = _ctx.StudySessions.AsNoTracking();
        if (subtopicId.HasValue)
            query = query.Where(s => s.SubtopicId == subtopicId.Value);

        var list = await query.OrderByDescending(s => s.SessionDate).ToListAsync();

        var dtoList = list.Select(s => new StudySessionDto
        {
            Id = s.Id,
            SubtopicId = s.SubtopicId,
            SessionDate = s.SessionDate,
            DurationMinutes = s.DurationMinutes
        }).ToList();

        return Ok(dtoList);
    }

    [HttpPost]
    public async Task<IActionResult> Create(StudySession session)
    {
        session.SessionDate = session.SessionDate == default ? DateTime.UtcNow : session.SessionDate;
        _ctx.StudySessions.Add(session);
        await _ctx.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { subtopicId = session.SubtopicId }, session);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var s = await _ctx.StudySessions.FindAsync(id);
        if (s is null) return NotFound();
        _ctx.StudySessions.Remove(s);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}