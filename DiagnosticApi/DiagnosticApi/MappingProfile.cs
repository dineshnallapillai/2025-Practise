using DiagnosticApi.DTO;
using DiagnosticApi.DTOs;
using DiagnosticApi.Model;
using Mapster;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<CreatePatientDto, Patient>.NewConfig();
        TypeAdapterConfig<Patient, PatientDto>.NewConfig();
    }
}