using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyOrganizer.Api.Dtos;
using StudyOrganizer.Core.Entities;
using StudyOrganizer.Data;

namespace StudyOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModulesController : ControllerBase
{
    private readonly StudyContext _ctx;
    public ModulesController(StudyContext ctx) => _ctx = ctx;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _ctx.Modules
            .Include(m => m.Disciplines)
            .AsNoTracking()
            .ToListAsync();

        var dtoList = list.Select(m => new ModuleDto
        {
            Id = m.Id,
            Name = m.Name,
            Disciplines = m.Disciplines.Select(d => new DisciplineDto
            {
                Id = d.Id,
                Name = d.Name,
                ModuleId = d.ModuleId
            }).ToList()
        }).ToList();

        return Ok(dtoList);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var m = await _ctx.Modules
            .Include(m => m.Disciplines)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (m is null) return NotFound();

        var dto = new ModuleDto
        {
            Id = m.Id,
            Name = m.Name,
            Disciplines = m.Disciplines.Select(d => new DisciplineDto
            {
                Id = d.Id,
                Name = d.Name,
                ModuleId = d.ModuleId
            }).ToList()
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Module module)
    {
        _ctx.Modules.Add(module);
        await _ctx.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = module.Id }, module);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Module dto)
    {
        var m = await _ctx.Modules.FindAsync(id);
        if (m is null) return NotFound();
        m.Name = dto.Name;
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var m = await _ctx.Modules.FindAsync(id);
        if (m is null) return NotFound();
        _ctx.Modules.Remove(m);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}