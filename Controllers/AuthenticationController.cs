using ApiCentroMedico.Dto.Usuario;
using ApiCentroMedico.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCentroMedico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _ServiceAut;
        public AuthenticationController([FromKeyedServices("AuthenticationService")] IAuthenticationService service)
        {
            _ServiceAut = service;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUser(UserDto user)
        {

            var Aut = await _ServiceAut.AuthenticateUser(user);
            if (Aut == null)
            {
                return BadRequest(new { Message = "Credenciales Invalidas" }) ;
            }

            // retorna el Token

            return Ok(Aut);


        }

    }
}
