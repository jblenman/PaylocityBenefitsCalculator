using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure;

public class BenefitsDbContext : DbContext
{
    public BenefitsDbContext(DbContextOptions<BenefitsDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Dependent> Dependents { get; set; }
}
