namespace EmployeeManagementSystem.Models;

public class ActivityLog
{
    public int Id { get; set; }

    public string Username { get; set; } = "";

    public string Action { get; set; } = "";

    public string Details { get; set; } = "";

    public DateTime Date { get; set; } = DateTime.Now;
}