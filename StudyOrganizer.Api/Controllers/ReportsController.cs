using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyOrganizer.Data;

namespace StudyOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly StudyContext _ctx;
        public ReportsController(StudyContext ctx) => _ctx = ctx;

        // GET api/Reports/progress?disciplineId=1
        [HttpGet("progress")]
        public async Task<IActionResult> GetProgress([FromQuery] int? disciplineId)
        {
            var query = _ctx.Subtopics.AsNoTracking();
            if (disciplineId.HasValue)
                query = query.Where(s => s.DisciplineId == disciplineId.Value);

            var total = await query.CountAsync();
            if (total == 0) return Ok(new { Total = 0, Done = 0, ProgressPercent = 0 });

            var done = await query.CountAsync(s => s.Status == Core.Entities.StudyStatus.Done);
            var percent = (int)((done / (double)total) * 100);

            return Ok(new { Total = total, Done = done, ProgressPercent = percent });
        }

        // GET api/Reports/study-time?disciplineId=1
        [HttpGet("study-time")]
        public async Task<IActionResult> GetStudyTime([FromQuery] int? disciplineId)
        {
            var query = _ctx.StudySessions.AsNoTracking().Include(s => s.Subtopic);
            if (disciplineId.HasValue)
                query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Core.Entities.StudySession, Core.Entities.Subtopic?>)query.Where(s => s.Subtopic!.DisciplineId == disciplineId.Value);

            var totalMinutes = await query.SumAsync(s => s.DurationMinutes);

            return Ok(new { TotalMinutes = totalMinutes, TotalHours = totalMinutes / 60.0 });
        }
    }
}
