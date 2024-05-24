using ApiCentroMedico.Dto.Medicos;
using FluentValidation;

namespace ApiCentroMedico.Validators.Medicos
{
    public class MedicoInsertValidator : AbstractValidator<MedicoInsertDto>
    {
        public MedicoInsertValidator()
        {
            RuleFor(x => x.Nombre)
                .NotNull()
                .WithMessage("El nombre es requerido")
                .NotEmpty()
                .WithMessage("El nombre es requerido")
                .MaximumLength(50)
                .WithMessage("El nombre no puede tener más de 50 caracteres");

            RuleFor(x => x.Idespecialidad)
                .NotNull()
                .WithMessage("La especialidad es requerida")
                .NotEmpty()
                .WithMessage("La especialidad es requerida");
            RuleFor(x => x.CostoConsulta)
                .NotNull()
                .WithMessage("El costo de la consulta es requerido")
                .NotEmpty()
                .WithMessage("El costo de la consulta es requerido")
                .GreaterThan(0)
                .WithMessage("El costo de la consulta debe ser mayor a 0");
          //  RuleFor(x => x.Idmedico )
                //.Must() // FUNCION ACA PARA VALIDAR EXISTENCIA EN BD
                
        }

    }
}
