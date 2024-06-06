using ApiCentroMedico.Dto.Obras_Sociales;
using ApiCentroMedico.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ApiCentroMedico.Controllers
{
    [Authorize]
    [Route("ApiCentroMedico/[controller]")]
    [ApiController]
    public class ObraSocialController : ControllerBase
    {

        private ICommonService<ObraSocialDto, ObraSocialInsertDto, ObraSocialUpdateDto> _ObraService;
        private IValidator<ObraSocialUpdateDto> _ObraUpdateValidator;
        private IValidator<ObraSocialInsertDto> _ObraInsertValidator;
        public ObraSocialController([FromKeyedServices("ObraSocialService")] ICommonService<ObraSocialDto,ObraSocialInsertDto,ObraSocialUpdateDto> ObraService
            ,IValidator<ObraSocialUpdateDto> validatorUpdate,IValidator<ObraSocialInsertDto> validatorInsert)
        {
            _ObraInsertValidator = validatorInsert;
            _ObraUpdateValidator = validatorUpdate;
            _ObraService = ObraService;
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]

        public async Task<ActionResult<ObraSocialDto>> Update(int id, ObraSocialUpdateDto UpdateDto)
        {
            var ValidationResult = await _ObraUpdateValidator.ValidateAsync(UpdateDto);
            if(!ValidationResult.IsValid)
            {
                return BadRequest(ValidationResult.Errors);
            }   

            var Obradto = await _ObraService.Update(id, UpdateDto);
            if (Obradto == null)
            {
                return NotFound();
            }
            return Ok(Obradto);

        }
        [Authorize(Policy = "Admin")]

        [HttpPost]
        public async Task<ActionResult<ObraSocialDto>> Insert(ObraSocialInsertDto Obra)
        {

            var validationResult = await _ObraInsertValidator.ValidateAsync(Obra);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var ObraDto = await _ObraService.Insert(Obra);
            if (ObraDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = ObraDto.Idobrasocial }, ObraDto);

        }

        [Authorize(Policy = "Admin")]

        [HttpDelete("{id}")]
        public async Task<ActionResult<ObraSocialDto>> Delete(int id)
        {
            var ObraDto = await _ObraService.Delete(id);
            if (ObraDto == null)
            {
                return NotFound();
            }
            return Ok(ObraDto);

        }

        [Authorize(Policy = "All")]

        [HttpGet("All")]

        public async Task<IEnumerable<ObraSocialDto>> GetAll() => await _ObraService.GetAll();

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("{id}")]
        public async Task<ActionResult<ObraSocialDto>> GetById(int id)
        {
            var ObraDto = await _ObraService.GetById(id);
            if (ObraDto == null)
            {
                return NotFound();
            }
            return Ok(ObraDto);

        }

    }
}
