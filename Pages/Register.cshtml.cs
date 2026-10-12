using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagementSystem.Pages;

public class RegisterModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public RegisterModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string Username { get; set; } = "";

    [BindProperty]
    public string Password { get; set; } = "";

    [BindProperty]
    public string ConfirmPassword { get; set; } = "";

    public string Message { get; set; } = "";

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Password != ConfirmPassword)
        {
            Message = "Passwords do not match.";
            return Page();
        }

        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == Username);

        if (existingUser != null)
        {
            Message = "Username already exists.";
            return Page();
        }

        var user = new User
        {
            Username = Username,
            Password = Password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Index");
    }
}
