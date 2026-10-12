using EmployeeManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EmployeeManagementSystem.Pages;

public class ReportsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public ReportsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public int TotalEmployees { get; set; }

    public List<string> Departments { get; set; } = new();

    public async Task OnGetAsync()
    {
        TotalEmployees = await _context.Employees.CountAsync();

        Departments = await _context.Employees
            .Select(e => e.Department)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IActionResult> OnGetExportCsvAsync()
    {
        var employees = await _context.Employees
            .OrderBy(e => e.EmployeeId)
            .ToListAsync();

        var csv = new StringBuilder();

        csv.AppendLine("Employee ID,Full Name,Position,Department");

        foreach (var employee in employees)
        {
            csv.AppendLine(string.Join(",",
                CsvEscape(employee.EmployeeId),
                CsvEscape(employee.FullName),
                CsvEscape(employee.Position),
                CsvEscape(employee.Department)));
        }

        var bytes = Encoding.UTF8.GetPreamble()
            .Concat(Encoding.UTF8.GetBytes(csv.ToString()))
            .ToArray();

        return File(bytes, "text/csv", "EmployeeReport.csv");
    }

    private static string CsvEscape(string? value)
    {
        value ??= "";
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }

}