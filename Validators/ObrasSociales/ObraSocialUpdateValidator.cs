using ApiCentroMedico.Dto.Obras_Sociales;
using FluentValidation;
namespace ApiCentroMedico.Validators.ObrasSociales
{
    public class ObraSocialUpdateValidator : AbstractValidator<ObraSocialUpdateDto>
    {
        public ObraSocialUpdateValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre de la obra social no puede estar vacio");
            RuleFor(x => x.Cobertura).NotEmpty().WithMessage("La cobertura de la obra social no puede estar vacia").GreaterThan(0).WithMessage("La cobertura de la obra social debe ser mayor a 0")
                   .LessThan(100).WithMessage("La cobertura de la obra social no puede ser mayor a 100");
        }
      
    }
}
