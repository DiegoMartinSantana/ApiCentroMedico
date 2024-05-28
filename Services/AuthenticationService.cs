using ApiCentroMedico.Dto.Usuario;
using ApiCentroMedico.Repository;
using AutoMapper;

namespace ApiCentroMedico.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private IAuthenticationRepository _repository;
        private IMapper _Mapping;

        public AuthenticationService( IAuthenticationRepository repo, IMapper mapp)
        {
            _Mapping = mapp;
            _repository = repo;

        }
        public async Task<UserDto?> AuthenticateUser(UserDto user)
        {
           var Model = await _repository.AuthenticateUser(user.Email, user.Pass);

            return Model == null ? null :_Mapping.Map<UserDto>(Model);

        }
    }
}
