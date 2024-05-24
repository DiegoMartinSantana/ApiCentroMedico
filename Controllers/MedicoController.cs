using ApiCentroMedico.Dto.Medicos;
using ApiCentroMedico.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ApiCentroMedico.Controllers
{

    [Route("ApiCentroMedico/[controller]")]
    [ApiController] //permite facil enrutamiento
    public class MedicoController : ControllerBase
    {

        private ICommonServices<MedicoDto, MedicoInsertDto, MedicoUpdateDto> _MedicoServices;
        private IValidator<MedicoInsertDto> _MedicoInsertValidator;
        public MedicoController([FromKeyedServices("IMedicoServices")] ICommonServices<MedicoDto, MedicoInsertDto, MedicoUpdateDto> MedicoServices,
            IValidator<MedicoInsertDto> validator )
        {
            _MedicoInsertValidator = validator;
            _MedicoServices = MedicoServices;
        }

        [HttpPost]
        public async Task<ActionResult<MedicoDto>> Insert(MedicoInsertDto medico)
        {
            var ValidationResult = await _MedicoInsertValidator.ValidateAsync(medico); 
            
            if(!ValidationResult.IsValid)
            {
                return BadRequest(ValidationResult.Errors);
            }
            if (medico == null)
            {
                return BadRequest();
            }
             var Medico = await _MedicoServices.Insert(medico);

            return CreatedAtAction(nameof(GetMedico), new { id = medico.Idmedico }, medico); // = 201
      //se dirije hacia getMedico despues de crear!, new id.. = parametro de la ruta , medico = body de la respuesta
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicoDto>> Update(int id,MedicoUpdateDto medico)
        {
            if (medico == null)
            {
                return BadRequest();
            }
            var Medico= await _MedicoServices.Update(id,medico);
            if (medico == null)
            {
                return NotFound();
            }
            return Ok(Medico);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicoDto>> Delete(int id) //retorna un ActionResult con tipo medico dto
        {
           
            //validacion si no tiene turnos asociados ACA

           if( await _MedicoServices.Delete(id) == null)
            {
                return NotFound();
            }
           return Ok(await _MedicoServices.Delete(id));

        }
        [HttpGet("All")]
        public async Task<IEnumerable<MedicoDto>> GetAll()
        {
            return await _MedicoServices.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<MedicoDto> GetMedico(int id)
        {
            return await _MedicoServices.GetById(id);

        }
    }
}
