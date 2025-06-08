using DiagnosticApi.DTO;
using DiagnosticApi.DTOs;
using DiagnosticApi.Model;
using DiagnosticApi.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace DiagnosticApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _service;

    public PatientsController(IPatientService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Patient>>> GetAll()
    {
        var patients = await _service.GetAllAsync();
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> Get(int id)
    {
        var patient = await _service.GetByIdAsync(id);
        if (patient == null) return NotFound();
        return Ok(patient);
    }

    [HttpPost]
    public Task<IActionResult> Create(CreatePatientDto dto)
    {
        var patient = dto.Adapt<Patient>();
        _service.CreateAsync(patient);

        var result = patient.Adapt<PatientDto>();
        return Task.FromResult<IActionResult>(CreatedAtAction(nameof(Get), new { id = result.Id }, result));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreatePatientDto dto)
    {
        var updated = await _service.UpdateAsync(dto.Adapt<Patient>());
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}