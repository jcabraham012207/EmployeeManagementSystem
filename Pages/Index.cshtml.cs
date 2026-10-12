using EmployeeManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string Username { get; set; } = "";

    [BindProperty]
    public string Password { get; set; } = "";

    public string Message { get; set; } = "";

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Built-in admin account
        if (Username == "admin" && Password == "admin123")
        {
            HttpContext.Session.SetString("Username", Username);
            HttpContext.Session.SetString("Role", "Admin");

            return RedirectToPage("/Dashboard");
        }

        // Check registered accounts
        var user = await _context.Users
            .FirstOrDefaultAsync(u =>
                u.Username == Username &&
                u.Password == Password);

        if (user != null)
        {
            HttpContext.Session.SetString("Username", user.Username);

            string role = string.Equals(
                user.Role,
                "Admin",
                StringComparison.OrdinalIgnoreCase)
                ? "Admin"
                : "User";

            HttpContext.Session.SetString("Role", role);

            return RedirectToPage("/Dashboard");
        }

        Message = "Invalid username or password.";
        return Page();
    }
}