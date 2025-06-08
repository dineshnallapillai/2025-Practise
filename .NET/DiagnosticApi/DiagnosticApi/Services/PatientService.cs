using DiagnosticApi.Data;
using DiagnosticApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DiagnosticApi.Services;

public class PatientService : IPatientService
{
    private readonly AppDbContext _context;

    public PatientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Patient>> GetAllAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient> GetByIdAsync(int id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task<Patient> CreateAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<bool> UpdateAsync(Patient updatedPatient)
    {
        var existing = await _context.Patients.FindAsync(updatedPatient.Id);
        if (existing is null) return false;

        existing.Name = updatedPatient.Name;
        existing.Age = updatedPatient.Age;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Patients.FindAsync(id);
        if (existing is null) return false;

        _context.Patients.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}