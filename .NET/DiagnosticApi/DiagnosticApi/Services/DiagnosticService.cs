using DiagnosticApi.Data;
using DiagnosticApi.Model;

namespace DiagnosticApi.Services;

public class DiagnosticService : IDiagnosticService
{
    private readonly AppDbContext _dbContext;

    public DiagnosticService(AppDbContext db)
    {
        _dbContext = db;
    }

    public List<Diagnostic> GetAll() => _dbContext.Diagnostics.ToList();

    public Diagnostic? GetByCode(string code) => _dbContext.Diagnostics.Find(code);

    public void Add(Diagnostic diagnostic)
    {
        _dbContext.Diagnostics.Add(diagnostic);
        _dbContext.SaveChanges();

    }
}