using ApiCentroMedico.Dto.Permisos;
using ApiCentroMedico.Dto.Usuario;
using ApiCentroMedico.Models;

namespace ApiCentroMedico.Services
{
    public interface IAuthenticationService
    {
        public Task<UserDto?> AuthenticateUser(UserAuthenticateDto user);
        public Task<PermisoDto?>  GetPermissions(UserDto user);

    }
}
