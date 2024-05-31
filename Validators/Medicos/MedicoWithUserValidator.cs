using ApiCentroMedico.Dto.Medicos;
using FluentValidation;

namespace ApiCentroMedico.Validators.Medicos
{
    public class MedicoWithUserValidator : AbstractValidator<MedicoWithUserDto>
    {
        public MedicoWithUserValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty() // valida nulls tambien
                .WithMessage("El nombre es requerido")
                .MaximumLength(50)
                .WithMessage("El nombre no puede tener más de 50 caracteres")
                .Matches("^[a-zA-Z ]*$")
                .WithMessage("El nombre solo puede contener letras");

            RuleFor(x => x.Apellido)
               .NotEmpty()
               .WithMessage("El Apellido es requerido")
               .MaximumLength(50)
               .WithMessage("El Apellido no puede tener más de 50 caracteres")
               .Matches("^[a-zA-Z ]*$")
               .WithMessage("El Apellido solo puede contener letras");
            
            RuleFor(x => x.Sexo)
                .NotEmpty()
                .WithMessage("El sexo es requerido")
                .MaximumLength(1)
                .WithMessage("El sexo no puede tener más de 1 caracter")
                .Matches("^[MF]$")
                .WithMessage("El sexo solo puede ser M o F");

            RuleFor(x => x.Idespecialidad)
                .NotEmpty()
                .WithMessage("La especialidad es requerida")
                .GreaterThan(0);

            //existente en Bd
            RuleFor(x => x.CostoConsulta)
                .NotEmpty()
                .WithMessage("El costo de la consulta es requerido")
                .GreaterThan(0)
                .WithMessage("El costo de la consulta debe ser mayor a 0")
                .LessThanOrEqualTo(1000000);

            RuleFor(x => x.FechaNac)
                .NotEmpty()
                .LessThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-18)))
                .WithMessage("El médico debe ser mayor de edad")
                .GreaterThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-100)))
                .WithMessage("El médico debe ser menor de 100 años");
                
            RuleFor(x => x.Fechaingreso)
                .NotEmpty()
                .LessThan(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("La fecha de ingreso no puede ser mayor a la fecha actual")
                .GreaterThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-100)))
                .WithMessage("La fecha de ingreso no puede ser menor a 100 años atrás")
                .LessThan(x => x.FechaNac)
                .WithMessage("La fecha de ingreso no puede ser menor a la fecha de nacimiento");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("El email es requerido")
                .MaximumLength(200)
                .WithMessage("El email no puede tener más de 50 caracteres")
                .Matches("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")
                .WithMessage("El email no es válido");

             RuleFor(x => x.Pass)
                .NotEmpty()
                .WithMessage("La contraseña es requerida")
                .MaximumLength(50)
                .WithMessage("La contraseña no puede tener más de 50 caracteres")
                .MinimumLength(8)
                .WithMessage("La contraseña debe tener al menos 8 caracteres")

              
        }

    }
}
