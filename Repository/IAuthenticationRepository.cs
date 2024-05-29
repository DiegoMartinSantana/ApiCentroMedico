using ApiCentroMedico.Dto.Permisos;
using ApiCentroMedico.Models;

namespace ApiCentroMedico.Repository
{
    public interface IAuthenticationRepository
    {

        public Task<Usuario?> AuthenticateUser(string email, string pass);
        public Task<Permiso?> GetPermissions(Usuario user);

    }
}
