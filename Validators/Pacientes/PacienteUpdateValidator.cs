using ApiCentroMedico.Dto.Pacientes;
using FluentValidation;

namespace ApiCentroMedico.Validators.Pacientes
{
    public class PacienteUpdateValidator : AbstractValidator<PacienteUpdateDto>
    {
        public PacienteUpdateValidator()
        {
            RuleFor(x => x.Idobrasocial)
                .NotEmpty()
                .WithMessage("La obra social es requerida")
                .GreaterThan(0);
        }
    }
}
