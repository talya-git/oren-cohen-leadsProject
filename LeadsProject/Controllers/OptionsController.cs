using LeadsProject.Data;
using LeadsProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeadsProject.Controllers;

[ApiController]
[Route("api/options")]
[Authorize]
public class OptionsController(AppDbContext db) : ControllerBase
{
    [HttpGet("amenities")]
    public async Task<IActionResult> GetAmenities() =>
        Ok(await db.AmenityOptions.OrderBy(a => a.Name).ToListAsync());

    [HttpPost("amenities")]
    public async Task<IActionResult> AddAmenity([FromBody] Dictionary<string, string> body)
    {
        var name = body.GetValueOrDefault("name", "")?.Trim();
        if (string.IsNullOrEmpty(name)) return BadRequest();
        if (await db.AmenityOptions.AnyAsync(a => a.Name == name)) return Ok(new { message = "exists" });
        var item = new AmenityOption { Name = name };
        db.AmenityOptions.Add(item);
        await db.SaveChangesAsync();
        return Created("", item);
    }

    [HttpGet("nearby")]
    public async Task<IActionResult> GetNearBy() =>
        Ok(await db.NearByOptions.OrderBy(n => n.Name).ToListAsync());

    [HttpPost("nearby")]
    public async Task<IActionResult> AddNearBy([FromBody] Dictionary<string, string> body)
    {
        var name = body.GetValueOrDefault("name", "")?.Trim();
        if (string.IsNullOrEmpty(name)) return BadRequest();
        if (await db.NearByOptions.AnyAsync(n => n.Name == name)) return Ok(new { message = "exists" });
        var item = new NearByOption { Name = name };
        db.NearByOptions.Add(item);
        await db.SaveChangesAsync();
        return Created("", item);
    }
}
