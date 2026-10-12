namespace EmployeeManagementSystem.Models;

public class EmployeeReport
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string Category { get; set; } = "";

    public string Details { get; set; } = "";

    public string SubmittedBy { get; set; } = "";

    public DateTime SubmittedAt { get; set; } = DateTime.Now;

    public string Status { get; set; } = "Pending";

}