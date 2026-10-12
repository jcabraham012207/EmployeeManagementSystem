using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Pages;

public class ActivityLogsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public ActivityLogsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<ActivityLog> Logs { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return RedirectToPage("/Index");
        }

        Logs = await _context.ActivityLogs
            .OrderByDescending(log => log.Date)
            .ToListAsync();

        return Page();
    }
}
