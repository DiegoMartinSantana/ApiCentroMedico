using ApiCentroMedico.Dto.Pacientes;
using ApiCentroMedico.Dto.Usuario;
using ApiCentroMedico.Models;
using ApiCentroMedico.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCentroMedico.Controllers
{
    [Authorize]

    [Route("ApiCentroMedico/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private IMapper _Mapper;
        private PacienteService _PacienteServices;

        private IValidator<PacienteWithUserDto> _pacienteUserValidator;
        private IValidator<PacienteUpdateDto> _pacienteUpdateValidator;

        public PacienteController([FromKeyedServices("PacienteService")] PacienteService PacienteServices, IMapper map,IValidator<PacienteWithUserDto> pacienteValidator,
            IValidator<PacienteUpdateDto> pacienteUpdateValidator)
        {
            _pacienteUpdateValidator = pacienteUpdateValidator; 
            _pacienteUserValidator  = pacienteValidator;
            _Mapper = map;
            _PacienteServices = PacienteServices;
        }

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("All")]

        public async Task<IEnumerable<PacienteDto>> GetAll() => await _PacienteServices.GetAll();

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDto>> GetById(int id)
        {
            var Paciente = await _PacienteServices.GetById(id);
            if (Paciente == null)
            {
                return NotFound();
            }
            return Ok(Paciente);
        }


        [Authorize(Policy = "All")]

        [HttpPost]

        public async Task<ActionResult<PacienteDto>> Insert(PacienteWithUserDto PacienteUser)
        {
            var validationResult = await _pacienteUserValidator.ValidateAsync(PacienteUser);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var Paciente = _Mapper.Map<PacienteInsertDto>(PacienteUser);
            var User = _Mapper.Map<UserDto>(PacienteUser);

            

            var PacienteDto = await _PacienteServices.InsertWithUser(Paciente, User);
            if (PacienteDto == null)
            {
                return BadRequest();
            }
            //obtener aca el id post insert.. validar
            return CreatedAtAction(nameof(GetById), new { id = PacienteDto.Idpaciente }, PacienteDto);
        }

        [Authorize(Policy = "MedicoOrAdmin")]


        [HttpPut("{id}")]

        public async Task<ActionResult<PacienteDto>> Update(int id, PacienteUpdateDto Paciente)
        {

            var validationResult = await _pacienteUpdateValidator.ValidateAsync(Paciente);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if (Paciente == null)
            {
                return BadRequest();
            }
            var PacienteDto = await _PacienteServices.Update(id, Paciente);
            if (PacienteDto == null)
            {
                return NotFound();
            }
            return Ok(PacienteDto);

        }



        [Authorize(Policy = "Admin")]

        [HttpDelete("{id}")]

        public async Task<ActionResult<PacienteDto>> Delete(int id)
        {
            var Paciente = await _PacienteServices.Delete(id);
            if (Paciente == null)
            {
                return NotFound();
            }
            return Ok(Paciente);
        }




    }
}
