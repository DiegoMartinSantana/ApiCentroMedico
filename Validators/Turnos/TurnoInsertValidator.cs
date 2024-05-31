using ApiCentroMedico.Dto.Turnos;
using FluentValidation;
using Microsoft.Identity.Client;

namespace ApiCentroMedico.Validators.Turnos
{
    public class TurnoInsertValidator :AbstractValidator<TurnoInsertDto> 
    {

        public TurnoInsertValidator()
        {
            RuleFor(x => x.Fechahora)
                .NotEmpty()
                .WithMessage("La fecha y hora es requerida")
                .GreaterThan(System.DateTime.Now)
                .WithMessage("La fecha y hora debe ser mayor a la actual")
                .LessThan(System.DateTime.Now.AddMonths(3))
                .WithMessage("No se pueden asignar turnos mas alla de los proximos 3 meses");

            RuleFor(x => x.Idmedico)
                .NotEmpty()
                .WithMessage("El médico es requerido")
                .GreaterThan(0);
             

            RuleFor(x => x.Idpaciente)
                .NotEmpty()
                .WithMessage("El paciente es requerido")
                .GreaterThan(0);

            RuleFor(x => x.Duracion)
                .NotEmpty()
                .WithMessage("La duración es requerida")
                .GreaterThan(0)
                .LessThanOrEqualTo(60)
                .WithMessage("La duración debe ser mayor a 0 y menor o igual a 60 minutos");
        }
    }
}
