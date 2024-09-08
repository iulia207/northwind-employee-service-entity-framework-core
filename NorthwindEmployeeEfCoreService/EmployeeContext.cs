using Microsoft.EntityFrameworkCore;

namespace NorthwindEmployeeEfCoreService;

public sealed class EmployeeContext : DbContext
{
    public EmployeeContext(DbContextOptions<EmployeeContext> options)
        : base(options)
    {

    }

    public DbSet<Employee> Employees { get; set; } = default!;
}