using ApiCentroMedico.Dto.Usuario;

namespace ApiCentroMedico.Services
{
    public interface IAuthenticationService
    {
        public Task<UserDto> AuthenticateUser(UserDto user);

    }
}
