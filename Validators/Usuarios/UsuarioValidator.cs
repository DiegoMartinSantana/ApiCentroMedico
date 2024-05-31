using ApiCentroMedico.Dto.Usuario;
using FluentValidation;

namespace ApiCentroMedico.Validators.Usuarios
{
    public class UsuarioValidator : AbstractValidator<UserDto>
    {

        public UsuarioValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().WithMessage("El email no puede estar vacio y debe ser un email valido")
                .MaximumLength(200).WithMessage("El email no puede tener mas de 200 caracteres")
            .MinimumLength(15).WithMessage("El email no puede tener menos de 15 caracteres");
            RuleFor(x => x.Pass).NotNull().NotEmpty().MinimumLength(8).WithMessage("La contraseña no puede estar vacia y debe tener al menos 8 caracteres")
                .MaximumLength(50).WithMessage("La contraseña no puede tener mas de 50 caracteres");
        }
    }
}
