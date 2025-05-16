using DiagnosticApi.Data;
using DiagnosticApi.Model;
using DiagnosticApi.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;


namespace DiagnosticApi.Tests;

public class PatientServiceTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // fresh DB per test
        .Options;

        var context = new AppDbContext(options);

        // Seed if needed
        context.Patients.AddRange(
            new Patient { Name = "Alice", Age = 30 },
            new Patient { Name = "Bob", Age = 40 }
        );
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllPatients()
    {
        using var context = GetDbContext();
        var service = new PatientService(context);

        var result = await service.GetAllAsync();

        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectPatient()
    {
        using var context = GetDbContext();
        var service = new PatientService(context);

        var patient = await service.GetByIdAsync(1);

        patient.Should().NotBeNull();
        patient!.Name.Should().Be("Alice");
    }

    [Fact]
    public async Task CreateAsync_AddsPatientSuccessfully()
    {
        using var context = GetDbContext();
        var service = new PatientService(context);

        var newPatient = new Patient { Name = "Charlie", Age = 25 };

        var result = await service.CreateAsync(newPatient);

        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be("Charlie");

        (await context.Patients.CountAsync()).Should().Be(3);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        using var context = GetDbContext();
        var service = new PatientService(context);

        var result = await service.GetByIdAsync(999);

        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingPatient()
    {
        using var context = GetDbContext();
        var service = new PatientService(context);

        var patientToUpdate = new Patient { Id = 1, Name = "Updated", Age = 35 };

        var result = await service.UpdateAsync(patientToUpdate);

        result.Should().BeTrue();
        var updated = await context.Patients.FindAsync(1);
        updated!.Name.Should().Be("Updated");
        updated.Age.Should().Be(35);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenPatientDoesNotExist()
    {
        using var context = GetDbContext();
        var service = new PatientService(context);

        var nonExistent = new Patient { Id = 999, Name = "Ghost", Age = 50 };

        var result = await service.UpdateAsync(nonExistent);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_DeletesPatient()
    {
        using var context = GetDbContext();
        var service = new PatientService(context);

        var result = await service.DeleteAsync(1);

        result.Should().BeTrue();
        (await context.Patients.FindAsync(1)).Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenNotFound()
    {
        using var context = GetDbContext();
        var service = new PatientService(context);

        var result = await service.DeleteAsync(999);

        result.Should().BeFalse();
    }

}
