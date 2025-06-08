using DiagnosticApi.DTO;
using FluentValidation;

namespace DiagnosticApi.Validators;

public class CreatePatientDtoValidator : AbstractValidator<CreatePatientDto>
{
    public CreatePatientDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(x => x.Age)
            .InclusiveBetween(0, 120);
    }
}