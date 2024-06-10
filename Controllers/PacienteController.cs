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
        private IPacienteService _PacienteServices;
        private ICommonService<PacienteDto, PacienteInsertDto, PacienteUpdateDto> _ICommonServicesPaciente;

        private IValidator<PacienteWithUserDto> _pacienteUserValidator;
        private IValidator<PacienteUpdateDto> _pacienteUpdateValidator;

        public PacienteController(IPacienteService PacienteServices,ICommonService<PacienteDto,PacienteInsertDto,PacienteUpdateDto> commonService ,IMapper map,IValidator<PacienteWithUserDto> pacienteValidator,
            IValidator<PacienteUpdateDto> pacienteUpdateValidator)
        {
            _pacienteUpdateValidator = pacienteUpdateValidator; 
            _pacienteUserValidator  = pacienteValidator;
            _Mapper = map;
            _PacienteServices = PacienteServices;
            _ICommonServicesPaciente = commonService;
        }

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("All")]

        public async Task<IEnumerable<PacienteDto>> GetAll() => await _ICommonServicesPaciente.GetAll();

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDto>> GetById(int id)
        {
            var Paciente = await _ICommonServicesPaciente.GetById(id);
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


            var PacienteDto = await _PacienteServices.InsertWithUser(PacienteUser);

            if (PacienteDto == null)
            {
                return BadRequest("Dni existente");
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
            var PacienteDto = await _ICommonServicesPaciente.Update(id, Paciente);
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
            var Paciente = await _ICommonServicesPaciente.Delete(id);
            
            if (Paciente == null)
            {
                return NotFound();
            }
            return Ok(Paciente);
        }




    }
}
