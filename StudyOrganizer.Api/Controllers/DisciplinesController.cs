using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyOrganizer.Api.Dtos; // Importe o namespace dos DTOs
using StudyOrganizer.Core.Entities;
using StudyOrganizer.Data;

namespace StudyOrganizer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly StudyContext _ctx;
        public DisciplinesController(StudyContext ctx) => _ctx = ctx;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? moduleId)
        {
            var query = _ctx.Disciplines
            .Include(d => d.Subtopics)
            .AsNoTracking();

            if (moduleId.HasValue)
                query = query.Where(d => d.ModuleId == moduleId.Value);

            var list = await query
                .Select(d => new DisciplineDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    ModuleId = d.ModuleId,
                    Subtopics = d.Subtopics.Select(s => new SubtopicDto
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
                    }).ToList()
                })
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var d = await _ctx.Disciplines
                .Include(d => d.Subtopics)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (d is null) return NotFound();

            // Mapeamento da entidade para o DTO
            var dto = new DisciplineDto
            {
                Id = d.Id,
                Name = d.Name,
                ModuleId = d.ModuleId,
                Subtopics = d.Subtopics.Select(s => new SubtopicDto
                {
                    Id = s.Id,
                    Description = s.Description,
                    DisciplineId = s.DisciplineId
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Discipline discipline)
        {
            _ctx.Disciplines.Add(discipline);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = discipline.Id }, discipline);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Discipline dto)
        {
            var d = await _ctx.Disciplines.FindAsync(id);
            if (d is null) return NotFound();

            d.Name = dto.Name;
            d.ModuleId = dto.ModuleId;
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var d = await _ctx.Disciplines.FindAsync(id);
            if (d is null) return NotFound();

            _ctx.Disciplines.Remove(d);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}