using ApiCentroMedico.Dto.Turnos;
using ApiCentroMedico.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCentroMedico.Controllers
{
    [Route("ApiCentroMedico/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private ITurnoService _turnoService;

        public TurnoController([FromKeyedServices("TurnoService")] ITurnoService turnoService)
        {
            _turnoService = turnoService;
        }

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("All")]

        public async Task<IEnumerable<TurnoDto>> GetAll() => await _turnoService.GetAll();

        [Authorize(Policy = "All")]

        [HttpGet("{id}")]
        public async Task<ActionResult<TurnoDto>> GetById(int id)
        {
            var TurnoDto = await _turnoService.GetById(id);
            if(TurnoDto==null)
            {
                return NotFound();
            }
            return Ok(TurnoDto);

        }

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpPost]
        public async Task<ActionResult<TurnoDto>> Add(TurnoInsertDto TurnoAdd)
        {
            if (TurnoAdd == null)
            {
                return BadRequest();
            }
            var TurnoDto = await _turnoService.Add(TurnoAdd);
           
            return CreatedAtAction(nameof(GetById), new { id = TurnoAdd.Idturno }, TurnoAdd);


        }
        [Authorize(Policy = "All")]

        [HttpDelete("{id}")]
        public async Task<ActionResult<TurnoDto>> Cancel(int id, TurnoDto cancel)
        {
            var Turno = await _turnoService.Cancel(id);
            if(Turno==null)
            {
                return NotFound();
            }
       

            return Ok(Turno);
        }


    }
}
