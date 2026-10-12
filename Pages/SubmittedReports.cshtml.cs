
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Pages;

public class SubmittedReportsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public SubmittedReportsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<EmployeeReport> Reports { get; set; } = new();

    public async Task OnGetAsync()
    {
        Reports = await _context.EmployeeReports
            .OrderByDescending(report => report.SubmittedAt)
            .ToListAsync();
    }

}