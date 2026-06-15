namespace LeadsProject.Models;

public class Lead
{
    public int Id { get; set; }
    public string? ContactName { get; set; }
    public string? Phone { get; set; }
    public string? Budget { get; set; }
    public string? Area { get; set; }
    public string? Rooms { get; set; }
    public string? PropertyType { get; set; }  // דירה/פנטהאוז/דירת גן/בית פרטי/וילה/דופלקס/סטודיו
    public string? Floor { get; set; }
    public string? Financing { get; set; }
    public string? Timeline { get; set; }
    public string? Intent { get; set; }
    public string? Amenities { get; set; }     // JSON array: ["מרפסת","מחסן","חניה","ממד","מעלית","גישה לנכים","נוף"]
    public int? AirDirections { get; set; }    // 1/2/3/4
    public string? NearBy { get; set; }        // JSON array: ["בית כנסת","סופרים"]
    public string Rating { get; set; } = "none";
    public string Status { get; set; } = "new";
    public int? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }
    public string? Transcript { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
