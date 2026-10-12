using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagementSystem.Pages;

public class AddEmployeeModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public AddEmployeeModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string EmployeeId { get; set; } = "";

    [BindProperty]
    public string FullName { get; set; } = "";

    [BindProperty]
    public string Position { get; set; } = "";

    [BindProperty]
    public string Department { get; set; } = "";

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return RedirectToPage("/Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return RedirectToPage("/Index");
        }

        var employee = new Employee
        {
            EmployeeId = EmployeeId,
            FullName = FullName,
            Position = Position,
            Department = Department
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Employees");
    }
}
