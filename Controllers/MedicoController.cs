using ApiCentroMedico.Dto.Medicos;
using ApiCentroMedico.Dto.Turnos;
using ApiCentroMedico.Dto.Usuario;
using ApiCentroMedico.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiCentroMedico.Controllers
{


    [Route("ApiCentroMedico/[controller]")]
    [ApiController] //permite facil enrutamiento
    public class MedicoController : ControllerBase
    {

        private MedicoService _MedicoServices;
        private IValidator<MedicoInsertDto> _MedicoInsertValidator;
        private IMapper _Mapper;
        public MedicoController([FromKeyedServices("MedicoService")] MedicoService MedicoServices,
            IValidator<MedicoInsertDto> validator , IMapper mapper)
        {
            _Mapper = mapper;
            _MedicoInsertValidator = validator;
            _MedicoServices = MedicoServices;
        }

        //EXAMPLE USE JWT

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("MedicosWithTurnos/{IdMedico}")]

        public async Task<IEnumerable<TurnoDetalleDto>> GetTurnoDetalle(int IdMedico) => await _MedicoServices.GetTurnosFromMedicos(IdMedico);

        [Authorize(Policy = "All")]

        [HttpGet("MedicosByEspecialidad")]

        public async Task<IEnumerable<MedicosEspecialidadDto>> GetMedicosEspecialidad() => await _MedicoServices.GetMedicosByEspecialty();

        [Authorize(Policy = "Admin")]

        [HttpPost]
        public async Task<ActionResult<MedicoDto>> Insert(MedicoWithUserDto MedicoUser) // por body solo uno. UNIFICAR EN UN DTO Y CREAR DSPS
        {
            if (MedicoUser == null)
            {
                return BadRequest();
            }
            var medico = _Mapper.Map<MedicoInsertDto>(MedicoUser);
            var user = _Mapper.Map<UserDto>(MedicoUser);
            user.IdPermiso = 2;
         
            var ValidationResult = await _MedicoInsertValidator.ValidateAsync(medico);

            if (!ValidationResult.IsValid)
            {
                return BadRequest(ValidationResult.Errors);
            }

            var MedicoPost = await _MedicoServices.InsertWithUser(medico,user);


            return CreatedAtAction(nameof(GetMedico), new { id = MedicoPost.Idmedico }, medico); // = 201
                                                                                             //se dirije hacia getMedico despues de crear!, new id.. = parametro de la ruta , medico = body de la respuesta
        }

        [Authorize(Policy = "Admin")]

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicoDto>> Update(int id, MedicoUpdateDto medico)
        {
            if (medico == null)
            {
                return BadRequest();
            }
            var Medico = await _MedicoServices.Update(id, medico);
            if (medico == null)
            {
                return NotFound();
            }
            return Ok(Medico);
        }

        [Authorize(Policy = "Admin")]

        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicoDto>> Delete(int id) //retorna un ActionResult con tipo medico dto
        {

            //validacion si no tiene turnos asociados ACA

            if (await _MedicoServices.Delete(id) == null)
            {
                return NotFound();
            }
            return Ok(await _MedicoServices.Delete(id));

        }

        [Authorize(Policy = "Admin")]

        [HttpGet("All")]
        public async Task<IEnumerable<MedicoDto>> GetAll()
        {
            return await _MedicoServices.GetAll();
        }
     
        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("{id}")]
        public async Task<MedicoDto> GetMedico(int id)
        {
            return await _MedicoServices.GetById(id);

        }
    }
}
