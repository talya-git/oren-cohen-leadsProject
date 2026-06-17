using System.Security.Claims;
using LeadsProject.Data;
using LeadsProject.Models;
using LeadsProject.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeadsProject.Controllers;

[ApiController]
[Route("api/leads")]
[Authorize]
public class LeadsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> GetAll() =>
        Ok(await db.Leads.Include(l => l.AssignedTo).OrderByDescending(l => l.CreatedAt).ToListAsync());

    [HttpGet("my")]
    [Authorize(Roles = "agent")]
    public async Task<IActionResult> GetMy()
    {
        var agentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await db.Leads.Where(l => l.AssignedToId == agentId).OrderByDescending(l => l.CreatedAt).ToListAsync());
    }

    [HttpPost]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> Create(CreateLeadRequest req)
    {
        var lead = new Lead
        {
            ContactName = req.ContactName, Phone = req.Phone, Email = req.Email,
            Source = req.Source, Budget = req.Budget,
            Area = req.Area, Rooms = req.Rooms, PropertyType = req.PropertyType,
            Floor = req.Floor, Financing = req.Financing, Timeline = req.Timeline,
            Intent = req.Intent, Amenities = req.Amenities, AirDirections = req.AirDirections,
            NearBy = req.NearBy, Transcript = req.Transcript
        };
        db.Leads.Add(lead);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = lead.Id }, lead);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> Update(int id, UpdateLeadRequest req)
    {
        var lead = await db.Leads.FindAsync(id);
        if (lead == null) return NotFound();

        if (req.Rating != null) lead.Rating = req.Rating;
        if (req.Notes != null) lead.Notes = req.Notes;
        if (req.ContactName != null) lead.ContactName = req.ContactName;
        if (req.Phone != null) lead.Phone = req.Phone;
        if (req.Email != null) lead.Email = req.Email;
        if (req.Source != null) lead.Source = req.Source;
        if (req.Budget != null) lead.Budget = req.Budget;
        if (req.Area != null) lead.Area = req.Area;
        if (req.Rooms != null) lead.Rooms = req.Rooms;
        if (req.PropertyType != null) lead.PropertyType = req.PropertyType;
        if (req.Floor != null) lead.Floor = req.Floor;
        if (req.Financing != null) lead.Financing = req.Financing;
        if (req.Timeline != null) lead.Timeline = req.Timeline;
        if (req.Intent != null) lead.Intent = req.Intent;
        if (req.Amenities != null) lead.Amenities = req.Amenities;
        if (req.AirDirections != null) lead.AirDirections = req.AirDirections;
        if (req.NearBy != null) lead.NearBy = req.NearBy;
        if (req.Objections != null) lead.Objections = req.Objections;
        if (req.ReferralProject != null) lead.ReferralProject = req.ReferralProject;
        lead.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return Ok(lead);
    }

    [HttpPost("{id}/assign")]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> Assign(int id, AssignLeadRequest req)
    {
        var lead = await db.Leads.FindAsync(id);
        if (lead == null) return NotFound();

        lead.AssignedToId = req.AgentId;
        lead.Status = "assigned";
        lead.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return Ok(lead);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> Delete(int id)
    {
        var lead = await db.Leads.FindAsync(id);
        if (lead == null) return NotFound();
        db.Leads.Remove(lead);
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id}/phone-call")]
    public async Task<IActionResult> AddPhoneCall(int id, AddPhoneCallRequest req)
    {
        var lead = await db.Leads.FindAsync(id);
        if (lead == null) return NotFound();

        var calls = string.IsNullOrEmpty(lead.PhoneCalls)
            ? new List<object>()
            : System.Text.Json.JsonSerializer.Deserialize<List<object>>(lead.PhoneCalls) ?? new List<object>();

        var newCall = new { date = DateTime.UtcNow.ToString("o"), agent = req.Agent, title = req.Title, summary = req.Summary };
        calls.Add(newCall);

        lead.PhoneCalls = System.Text.Json.JsonSerializer.Serialize(calls);
        lead.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return Ok(lead);
    }
}
