using LeadsProject.Data;
using LeadsProject.Models.DTOs;
using LeadsProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeadsProject.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AppDbContext db, JwtService jwt) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Name == req.Name);
        if (user == null) return Unauthorized("משתמש לא נמצא");
        if (user.PasswordHash == null) return BadRequest("first-login");
        if (!BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash)) return Unauthorized("סיסמה שגויה");

        return Ok(new { token = jwt.GenerateToken(user), user = new { user.Id, user.Name, user.Role } });
    }

    [HttpPost("set-password")]
    public async Task<IActionResult> SetPassword(SetPasswordRequest req)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Name == req.Name);
        if (user == null) return NotFound("משתמש לא נמצא");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.NewPassword);
        await db.SaveChangesAsync();

        return Ok(new { token = jwt.GenerateToken(user), user = new { user.Id, user.Name, user.Role } });
    }

    [HttpPost("check-user")]
    public async Task<IActionResult> CheckUser([FromBody] Dictionary<string, string> body)
    {
        var name = body.GetValueOrDefault("name", "");
        var user = await db.Users.FirstOrDefaultAsync(u => u.Name == name);
        if (user == null) return NotFound("משתמש לא נמצא");
        return Ok(new { hasPassword = user.PasswordHash != null, role = user.Role });
    }
}
