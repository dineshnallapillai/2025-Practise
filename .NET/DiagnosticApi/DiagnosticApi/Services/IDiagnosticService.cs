using DiagnosticApi.Model;

namespace DiagnosticApi.Services
{
    public interface IDiagnosticService
    {
        List<Diagnostic> GetAll();
        Diagnostic? GetByCode(string code);
        void Add(Diagnostic diagnostic);
    }
}
