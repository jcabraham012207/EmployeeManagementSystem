using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagementSystem.Pages;

public class SubmitReportModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public SubmitReportModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string Title { get; set; } = "";

    [BindProperty]
    public string Category { get; set; } = "";

    [BindProperty]
    public string Details { get; set; } = "";

    public IActionResult OnGet()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
        {
            return RedirectToPage("/Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var username = HttpContext.Session.GetString("Username");

        if (string.IsNullOrEmpty(username))
        {
            return RedirectToPage("/Index");
        }

        if (string.IsNullOrWhiteSpace(Title) ||
            string.IsNullOrWhiteSpace(Category) ||
            string.IsNullOrWhiteSpace(Details))
        {
            ModelState.AddModelError(string.Empty, "Please complete all fields.");
            return Page();
        }

        var report = new EmployeeReport
        {
            Title = Title.Trim(),
            Category = Category,
            Details = Details.Trim(),
            SubmittedBy = username,
            SubmittedAt = DateTime.Now,
            Status = "Pending"
        };

        _context.EmployeeReports.Add(report);
        await _context.SaveChangesAsync();

        return RedirectToPage("/SubmittedReports");
    }

}
