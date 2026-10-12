using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<ActivityLog> ActivityLogs { get; set; }

    public DbSet<EmployeeReport> EmployeeReports { get; set; }

}