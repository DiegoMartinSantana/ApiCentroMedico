using ApiCentroMedico.Dto.Medicos;
using FluentValidation;
using Microsoft.AspNetCore.Rewrite;

namespace ApiCentroMedico.Validators.Medicos
{
    public class MedicoUpdateValidator :AbstractValidator<MedicoUpdateDto>
    {
        public MedicoUpdateValidator()
        {
            RuleFor(x=> x.CostoConsulta)
                .NotEmpty()
                .WithMessage("El costo de la consulta es requerido")
                .GreaterThan(0)
                .WithMessage("El costo de la consulta debe ser mayor a 0")
                .LessThanOrEqualTo(1000000);
        }
    }
}
