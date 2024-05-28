using ApiCentroMedico.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCentroMedico.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {

        private CentromedicoContext _Context;

        public AuthenticationRepository( CentromedicoContext context)
        {
                _Context = context;
        }

        public async Task<Usuario?> AuthenticateUser(string email, string pass) // puede devolver null si no existe
        {
            return  await _Context.Usuarios.SingleOrDefaultAsync( x => x.Email == email && x.Pass == pass);

        }
    }
}
