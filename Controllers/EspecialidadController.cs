using ApiCentroMedico.Dto.Especialidades;
using ApiCentroMedico.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCentroMedico.Controllers
{
    [Route("ApiCentroMedico/[controller]")]

    [ApiController]
    public class EspecialidadController : ControllerBase
    {

        private ICommonService<EspecialidadDto,EspecialidadInsertDto,EspecialidadDto> _EspecialidadServices;
        private IValidator<EspecialidadDto> _EspecialidadValidatorDto;
        private IValidator<EspecialidadInsertDto> _EspecialidadInsertValidatorDto;

        public EspecialidadController([FromKeyedServices("EspecialidadService")]ICommonService<EspecialidadDto, EspecialidadInsertDto, EspecialidadDto> especialidadServices
            ,IValidator<EspecialidadDto> especialidadValidatorDto,IValidator<EspecialidadInsertDto> especialidadInsertValidatorDto)
        {
            _EspecialidadServices = especialidadServices;
            _EspecialidadValidatorDto = especialidadValidatorDto;
            _EspecialidadInsertValidatorDto = especialidadInsertValidatorDto;
        }

        [Authorize(Policy = "Admin")]

        [HttpPut("{id}")]
        public async Task<ActionResult<EspecialidadDto>> Update( int id, EspecialidadDto especialidad)
        { 
            
            var ValidationResult = await _EspecialidadValidatorDto.ValidateAsync(especialidad); 
            if(!ValidationResult.IsValid)
            {
                return BadRequest(ValidationResult.Errors); // retorna los errors.(with message)
            }

            if (especialidad == null)
            {
                return BadRequest();
            }
            
            var UpdateService = await _EspecialidadServices.Update(id,especialidad);

            if(UpdateService == null)
            {
                return NotFound();
            }

            return Ok(UpdateService);
        }

        [Authorize(Policy = "Admin")]


        [HttpDelete("{id}")]
        public async Task<ActionResult<EspecialidadDto>> Delete (int id)
        {
            var DeleteService = await _EspecialidadServices.Delete(id);

            if(DeleteService == null)
            {
                return NotFound();
            }

            return Ok(DeleteService);

        }

        [Authorize(Policy = "Admin")]

        [HttpPost()]
        public async Task<ActionResult<EspecialidadDto>> Insert(EspecialidadInsertDto Especialidad)
        {
            var ValidationResult = await _EspecialidadInsertValidatorDto.ValidateAsync(Especialidad);
            if(!ValidationResult.IsValid)
            {
                return BadRequest(ValidationResult.Errors);
            }

            var DtoService = await _EspecialidadServices.Insert(Especialidad);
            if(DtoService == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = DtoService.Idespecialidad }, DtoService);

        }

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("All")]
        public async Task<IEnumerable<EspecialidadDto>> GetAll() => await _EspecialidadServices.GetAll();

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadDto>> GetById(int id)
        {
            var DtoService = await _EspecialidadServices.GetById(id);
            if(DtoService == null)
            {
                return NotFound();
            }
            return Ok(DtoService);

        }

    }
}
