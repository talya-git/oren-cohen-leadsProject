using LeadsProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeadsProject.Controllers;

[ApiController]
[Route("api/agents")]
[Authorize]
public class AgentsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAgents() =>
        Ok(await db.Users.Where(u => u.Role == "agent").Select(u => new { u.Id, u.Name }).ToListAsync());
}
