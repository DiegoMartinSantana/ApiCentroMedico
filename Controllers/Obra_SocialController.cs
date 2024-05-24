using ApiCentroMedico.Dto.Obras_Sociales;
using ApiCentroMedico.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ApiCentroMedico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Obra_SocialController : ControllerBase
    {

        private Obra_SocialService _ObraService;
        public Obra_SocialController(Obra_SocialService ObraService)
        {
            _ObraService = ObraService;
        }

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


        [HttpGet("All")]

        public async Task<IEnumerable<Obra_SocialDto>> GetAll() => await _ObraService.GetAll();

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
