using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagementSystem.Data;

public static class ActivityLogger
{
    public static async Task LogAsync(
        ApplicationDbContext context,
        ISession session,
        string action,
        string details)
    {
        var username = session.GetString("Username") ?? "Unknown";

        var log = new ActivityLog
        {
            Username = username,
            Action = action,
            Details = details,
            Date = DateTime.Now
        };

        context.ActivityLogs.Add(log);
        await context.SaveChangesAsync();
    }
}

