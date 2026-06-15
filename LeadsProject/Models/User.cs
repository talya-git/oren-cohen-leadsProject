namespace LeadsProject.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public string Role { get; set; } = "agent";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
