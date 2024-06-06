using ApiCentroMedico.Dto.Turnos;
using ApiCentroMedico.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCentroMedico.Controllers
{
    [Route("ApiCentroMedico/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private ICommonService<TurnoDto,TurnoInsertDto,TurnoUpdateDto> _turnoService;
        private IValidator<TurnoInsertDto> _turnoValidator;

        public TurnoController([FromKeyedServices("TurnoService")] ICommonService<TurnoDto, TurnoInsertDto, TurnoUpdateDto> turnoService , IValidator<TurnoInsertDto> validator)
        {
            _turnoService = turnoService;
            _turnoValidator = validator;
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
            var validationResult = await _turnoValidator.ValidateAsync(TurnoAdd);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var TurnoDto = await _turnoService.Insert(TurnoAdd);
           
            return CreatedAtAction(nameof(GetById), new { id = TurnoDto.Idturno }, TurnoDto);


        }
        [Authorize(Policy = "All")]

        [HttpDelete("{id}")]
        public async Task<ActionResult<TurnoDto>> Cancel(int id, TurnoDto cancel)
        {
            var Turno = await _turnoService.Delete(id);
            if(Turno==null)
            {
                return NotFound();
            }
       

            return Ok(Turno);
        }


    }
}
