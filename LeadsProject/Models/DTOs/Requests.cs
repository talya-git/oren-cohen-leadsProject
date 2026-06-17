namespace LeadsProject.Models.DTOs;

public record LoginRequest(string Name, string Password);

public record SetPasswordRequest(string Name, string NewPassword);

public record AssignLeadRequest(int AgentId);

public record UpdateLeadRequest(
    string? Rating,
    string? Notes,
    string? ContactName,
    string? Phone,
    string? Email,
    string? Source,
    string? Budget,
    string? Area,
    string? Rooms,
    string? PropertyType,
    string? Floor,
    string? Financing,
    string? Timeline,
    string? Intent,
    string? Amenities,
    int? AirDirections,
    string? NearBy,
    string? Objections,
    string? ReferralProject
);

public record CreateLeadRequest(
    string? ContactName,
    string? Phone,
    string? Email,
    string? Source,
    string? Budget,
    string? Area,
    string? Rooms,
    string? PropertyType,
    string? Floor,
    string? Financing,
    string? Timeline,
    string? Intent,
    string? Amenities,
    int? AirDirections,
    string? NearBy,
    string? Objections,
    string? ReferralProject,
    string? Transcript
);

public record AddPhoneCallRequest(string Agent, string Title, string Summary);
