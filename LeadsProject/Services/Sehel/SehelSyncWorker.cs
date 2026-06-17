namespace LeadsProject.Services.Sehel;

/// <summary>
/// Background service שרץ כל הזמן ומעבד עדכונים לשכל מהתור.
/// </summary>
public class SehelSyncWorker : BackgroundService
{
    private readonly SehelSyncQueue _queue;
    private readonly SehelSyncService _sehel;
    private readonly ILogger<SehelSyncWorker> _logger;
    private readonly bool _enabled;

    public SehelSyncWorker(SehelSyncQueue queue, IConfiguration config, ILogger<SehelSyncWorker> logger)
    {
        _queue = queue;
        _logger = logger;

        var loginUrl = config["Sehel:LoginUrl"] ?? "";
        var username = config["Sehel:Username"] ?? "";
        var password = config["Sehel:Password"] ?? "";

        _enabled = !string.IsNullOrEmpty(loginUrl) && !string.IsNullOrEmpty(username);
        _sehel = new SehelSyncService(loginUrl, username, password);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_enabled)
        {
            _logger.LogInformation("[SehelSync] Disabled — no credentials configured.");
            return;
        }

        _logger.LogInformation("[SehelSync] Starting...");

        try
        {
            await _sehel.InitAsync();
            _logger.LogInformation("[SehelSync] Connected to Sehel.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[SehelSync] Failed to connect to Sehel.");
            return;
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var task = await _queue.DequeueAsync(stoppingToken);

                switch (task.Action)
                {
                    case "assign":
                        _logger.LogInformation($"[SehelSync] Assigning lead {task.LeadId} to {task.AgentName}");
                        await _sehel.AssignLeadToAgentAsync(task.LeadId.ToString(), task.AgentName ?? "");
                        break;

                    case "update_status":
                        _logger.LogInformation($"[SehelSync] Updating lead {task.LeadId} status to {task.Status}");
                        await _sehel.UpdateLeadStatusAsync(task.LeadId.ToString(), task.Status ?? "");
                        break;

                    case "create":
                        _logger.LogInformation($"[SehelSync] Creating lead {task.LeadName}");
                        await _sehel.CreateLeadAsync(task.LeadName ?? "", task.Phone ?? "", task.Area, task.Budget);
                        break;
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[SehelSync] Error processing task.");
                await Task.Delay(5000, stoppingToken); // wait before retry
            }
        }

        await _sehel.DisposeAsync();
    }
}
