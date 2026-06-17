using LeadsProject.Data;
using LeadsProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeadsProject.Controllers;

[ApiController]
[Route("api/projects")]
[Authorize]
public class ProjectsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await db.Projects.OrderBy(p => p.Name).ToListAsync());

    [HttpPost]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> Create([FromBody] Dictionary<string, string> body)
    {
        var name = body.GetValueOrDefault("name", "")?.Trim();
        if (string.IsNullOrEmpty(name)) return BadRequest("שם פרויקט חסר");

        if (await db.Projects.AnyAsync(p => p.Name == name))
            return Ok(new { message = "קיים כבר" });

        var project = new Project { Name = name };
        db.Projects.Add(project);
        await db.SaveChangesAsync();
        return Created("", project);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await db.Projects.FindAsync(id);
        if (project == null) return NotFound();
        db.Projects.Remove(project);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
