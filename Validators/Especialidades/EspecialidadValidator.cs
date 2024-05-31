using ApiCentroMedico.Dto.Especialidades;
using FluentValidation;

namespace ApiCentroMedico.Validators.Especialidades
{
    public class EspecialidadValidator : AbstractValidator<EspecialidadDto>
    {
        public EspecialidadValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().MaximumLength(50).WithMessage("El nombre de la especialidad no puede estar vacío y debe tener un máximo de 50 caracteres");

        }
    }
}
