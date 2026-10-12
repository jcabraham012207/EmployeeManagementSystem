using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagementSystem.Pages
{
    public class DashboardModel : PageModel
    {
        public string Username { get; set; } = "";
        public string Role { get; set; } = "";

        public IActionResult OnGet()
        {
            Username = HttpContext.Session.GetString("Username") ?? "";
            Role = HttpContext.Session.GetString("Role") ?? "";

            if (string.IsNullOrEmpty(Username))
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}