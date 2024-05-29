using ApiCentroMedico.Dto.Usuario;
using ApiCentroMedico.Models;
using ApiCentroMedico.Services;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiCentroMedico.Controllers
{
    [Route("ApiCentroMedico/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _configuration;

        private IAuthenticationService _ServiceAut;
        public AuthenticationController([FromKeyedServices("AuthenticationService")] IAuthenticationService service, IConfiguration config)
        {
            _configuration = config;
            _ServiceAut = service;
        }


        [HttpPost]
        public async Task<IActionResult> AuthenticateUser(UserAuthenticateDto UserAut)
        {

            var Aut = await _ServiceAut.AuthenticateUser(UserAut);
            if (Aut == null)
            {
                return BadRequest(new { Message = "Credenciales Invalidas" });
            }

          
            var JwtToken = GenerateTokenJWT(Aut);


            return Ok(new { token = JwtToken }); //retorna el objeto anonimo con el token


        }

        private  string GenerateTokenJWT(UserDto user)
        {
            //obtengo nivel de permiso del ingresante 

            string Permiso =  _ServiceAut.GetPermissions(user).Result.Nombre; //obtengo el nombre del permiso
            

            var claims = new[]
            {
               new Claim(ClaimTypes.Email, user.Email),
               new Claim("Type" , Permiso) // genero claim para dicho permiso
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var Credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: Credentials
                );
            string tokenReturn = new JwtSecurityTokenHandler().WriteToken(Token);
            return tokenReturn;
        }

    }
}
