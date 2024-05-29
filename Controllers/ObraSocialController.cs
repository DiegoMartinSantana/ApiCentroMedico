using ApiCentroMedico.Dto.Obras_Sociales;
using ApiCentroMedico.Services;
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

        private ICommonService<Obra_SocialDto, Obra_SocialDto, ObraSocialUpdateDto> _ObraService;
        public ObraSocialController([FromKeyedServices("ObraSocialService")] ICommonService<Obra_SocialDto,Obra_SocialDto,ObraSocialUpdateDto> ObraService)
        {
            _ObraService = ObraService;
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]

        public async Task<ActionResult<Obra_SocialDto>> Update(int id, ObraSocialUpdateDto UpdateDto)
        {
            if (UpdateDto == null)
            {
                return BadRequest();
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
        public async Task<ActionResult<Obra_SocialDto>> Insert(Obra_SocialDto Obra)
        {
            var ObraDto = await _ObraService.Insert(Obra);
            if (ObraDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = Obra.Idobrasocial }, ObraDto);

        }

        [Authorize(Policy = "Admin")]

        [HttpDelete("{id}")]
        public async Task<ActionResult<Obra_SocialDto>> Delete(int id)
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

        public async Task<IEnumerable<Obra_SocialDto>> GetAll() => await _ObraService.GetAll();

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("{id}")]
        public async Task<ActionResult<Obra_SocialDto>> GetById(int id)
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
