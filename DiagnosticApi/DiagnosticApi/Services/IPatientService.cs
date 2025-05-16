using DiagnosticApi.Model;

namespace DiagnosticApi.Services
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllAsync();
        Task<Patient> GetByIdAsync(int id);
        Task<Patient> CreateAsync(Patient patient);
        Task<bool> UpdateAsync(Patient updatedPatient);
        Task<bool> DeleteAsync(int id);
    }

}
