using DiagnosticApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DiagnosticApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
}