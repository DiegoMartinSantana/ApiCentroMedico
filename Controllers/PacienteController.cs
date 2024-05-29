using ApiCentroMedico.Dto.Pacientes;
using ApiCentroMedico.Services;
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

        private ICommonService<PacienteDto, PacienteInsertDto, PacienteUpdateDto> _PacienteServices;
        public PacienteController([FromKeyedServices("PacienteService")] ICommonService<PacienteDto, PacienteInsertDto, PacienteUpdateDto> PacienteServices)
        {
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


        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpPost]

        public async Task<ActionResult<PacienteDto>> Insert(PacienteInsertDto Paciente)
        {
            var PacienteDto = await _PacienteServices.Insert(Paciente);
            if (PacienteDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = Paciente.Idpaciente }, PacienteDto);
        }

        [Authorize(Policy = "MedicoOrAdmin")]


        [HttpPut("{id}")]

        public async Task<ActionResult<PacienteDto>> Update(int id, PacienteUpdateDto Paciente)
        {
            if(Paciente == null)
            {
                return BadRequest();
            }
            var PacienteDto = await _PacienteServices.Update(id, Paciente);
            if(PacienteDto == null)
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
