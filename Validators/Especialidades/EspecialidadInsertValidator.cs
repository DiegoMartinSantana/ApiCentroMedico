using ApiCentroMedico.Dto.Especialidades;
using FluentValidation;

namespace ApiCentroMedico.Validators.Especialidades
{
    public class EspecialidadInsertValidator :AbstractValidator<EspecialidadInsertDto>
    {
        public EspecialidadInsertValidator()
        {
                RuleFor(x=> x.Nombre)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("El nombre de la especialidad no puede estar vacío y debe tener un máximo de 50 caracteres");
        }

    }
}
