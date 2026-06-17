using Microsoft.Playwright;

namespace LeadsProject.Services.Sehel;

/// <summary>
/// שירות סנכרון לשכל באמצעות Playwright.
/// מתחבר לממשק שכל ומעדכן שיוך סוכן/סטטוס ליד.
/// </summary>
public class SehelSyncService
{
    private readonly string _loginUrl;
    private readonly string _username;
    private readonly string _password;
    private IBrowser? _browser;
    private IPage? _page;
    private bool _isLoggedIn;

    public SehelSyncService(string loginUrl, string username, string password)
    {
        _loginUrl = loginUrl;
        _username = username;
        _password = password;
    }

    /// <summary>
    /// אתחול הדפדפן והתחברות לשכל.
    /// </summary>
    public async Task InitAsync()
    {
        var playwright = await Playwright.CreateAsync();
        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true // שנה ל-false לדיבוג
        });
        _page = await _browser.NewPageAsync();
        await LoginAsync();
    }

    /// <summary>
    /// התחברות לשכל.
    /// </summary>
    private async Task LoginAsync()
    {
        if (_page == null) return;

        await _page.GotoAsync(_loginUrl);

        // TODO: עדכן את הselectors לפי ה-UI של שכל
        // await _page.FillAsync("input[name='username']", _username);
        // await _page.FillAsync("input[name='password']", _password);
        // await _page.ClickAsync("button[type='submit']");
        // await _page.WaitForURLAsync("**/dashboard**");

        _isLoggedIn = true;
    }

    /// <summary>
    /// שיוך ליד לסוכן בשכל.
    /// </summary>
    /// <param name="leadId">מזהה הליד בשכל</param>
    /// <param name="agentName">שם הסוכן לשיוך</param>
    public async Task<bool> AssignLeadToAgentAsync(string leadId, string agentName)
    {
        if (!_isLoggedIn || _page == null) return false;

        try
        {
            // TODO: עדכן את הניווט לפי ה-UI של שכל
            // 1. ניווט לדף הליד
            // await _page.GotoAsync($"https://crm.sehel.co.il/leads/{leadId}");
            
            // 2. לחיצה על כפתור "שייך סוכן"
            // await _page.ClickAsync("button.assign-agent");
            
            // 3. בחירת הסוכן מהרשימה
            // await _page.ClickAsync($"text={agentName}");
            
            // 4. אישור
            // await _page.ClickAsync("button.confirm");

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SehelSync] Error assigning lead {leadId}: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// עדכון סטטוס ליד בשכל.
    /// </summary>
    /// <param name="leadId">מזהה הליד בשכל</param>
    /// <param name="status">סטטוס חדש</param>
    public async Task<bool> UpdateLeadStatusAsync(string leadId, string status)
    {
        if (!_isLoggedIn || _page == null) return false;

        try
        {
            // TODO: עדכן את הניווט לפי ה-UI של שכל
            // await _page.GotoAsync($"https://crm.sehel.co.il/leads/{leadId}");
            // await _page.SelectOptionAsync("select.status", status);
            // await _page.ClickAsync("button.save");

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SehelSync] Error updating lead {leadId}: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// יצירת ליד חדש בשכל.
    /// </summary>
    public async Task<string?> CreateLeadAsync(string name, string phone, string? area, string? budget)
    {
        if (!_isLoggedIn || _page == null) return null;

        try
        {
            // TODO: עדכן את הניווט לפי ה-UI של שכל
            // await _page.GotoAsync("https://crm.sehel.co.il/leads/new");
            // await _page.FillAsync("input[name='name']", name);
            // await _page.FillAsync("input[name='phone']", phone);
            // if (area != null) await _page.FillAsync("input[name='area']", area);
            // if (budget != null) await _page.FillAsync("input[name='budget']", budget);
            // await _page.ClickAsync("button.save");
            // var url = _page.Url; // extract lead ID from URL

            return null; // TODO: return the new lead ID
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SehelSync] Error creating lead: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// סגירת הדפדפן.
    /// </summary>
    public async Task DisposeAsync()
    {
        if (_browser != null)
            await _browser.DisposeAsync();
    }
}
