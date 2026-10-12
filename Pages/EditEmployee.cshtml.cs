using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagementSystem.Pages;

public class EditEmployeeModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditEmployeeModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public int Id { get; set; }

    [BindProperty]
    public string EmployeeId { get; set; } = "";

    [BindProperty]
    public string FullName { get; set; } = "";

    [BindProperty]
    public string Position { get; set; } = "";

    [BindProperty]
    public string Department { get; set; } = "";

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
        {
            return RedirectToPage("/Employees");
        }

        Id = employee.Id;
        EmployeeId = employee.EmployeeId;
        FullName = employee.FullName;
        Position = employee.Position;
        Department = employee.Department;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var employee = await _context.Employees.FindAsync(Id);

        if (employee == null)
        {
            return RedirectToPage("/Employees");
        }

        employee.EmployeeId = EmployeeId;
        employee.FullName = FullName;
        employee.Position = Position;
        employee.Department = Department;

        await _context.SaveChangesAsync();

        return RedirectToPage("/Employees");
    }
}