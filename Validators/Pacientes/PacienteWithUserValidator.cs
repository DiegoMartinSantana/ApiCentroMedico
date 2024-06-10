using ApiCentroMedico.Dto.Pacientes;
using FluentValidation;

namespace ApiCentroMedico.Validators.Pacientes
{
    public class PacienteWithUserValidator : AbstractValidator<PacienteWithUserDto>
    {
        public PacienteWithUserValidator()
        {
            RuleFor(x=> x.Dni)
                .NotEmpty()
                .WithMessage("El DNI es requerido")
                .GreaterThan(0)
                .WithMessage("El DNI debe ser mayor a 0");

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

            RuleFor(x=> x.Idobrasocial)
                .NotEmpty()
                .WithMessage("La obra social es requerida")
                .GreaterThan(0);

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
               .WithMessage("La contraseña debe tener al menos 8 caracteres");



        }


    }
}
