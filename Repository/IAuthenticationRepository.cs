using ApiCentroMedico.Models;

namespace ApiCentroMedico.Repository
{
    public interface IAuthenticationRepository
    {

        public Task<Usuario> AuthenticateUser(string email, string pass);

    }
}
