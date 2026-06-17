using System.Threading.Channels;

namespace LeadsProject.Services.Sehel;

/// <summary>
/// תור עדכונים לשכל — כשמשייכים ליד, מוסיפים לתור 
/// והשירות מטפל ברקע (בלי לעכב את ה-API).
/// </summary>
public class SehelSyncQueue
{
    private readonly Channel<SehelSyncTask> _channel = Channel.CreateUnbounded<SehelSyncTask>();

    public async Task EnqueueAsync(SehelSyncTask task)
    {
        await _channel.Writer.WriteAsync(task);
    }

    public async Task<SehelSyncTask> DequeueAsync(CancellationToken ct)
    {
        return await _channel.Reader.ReadAsync(ct);
    }
}

public record SehelSyncTask(
    string Action, // "assign" | "update_status" | "create"
    int LeadId,
    string? AgentName,
    string? Status,
    string? LeadName,
    string? Phone,
    string? Area,
    string? Budget
);
