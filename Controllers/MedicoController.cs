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
        private IMapper _Mapper;
        private IMedicoService _MedicoServices;
        private ICommonService<MedicoDto, MedicoInsertDto, MedicoUpdateDto> _ICommonServicesMedico;

        private IValidator<MedicoWithUserDto> _MedicoInsertValidator;
        private IValidator<MedicoUpdateDto> _MedicoUpdateValidator;


        public MedicoController(IMedicoService MedicoServices, IMapper mapper, ICommonService<MedicoDto, MedicoInsertDto, MedicoUpdateDto> ICommonServices,
            IValidator<MedicoWithUserDto> validator, IValidator<UserDto> validatorUser, IValidator<MedicoUpdateDto> validatorUpdate)
        {
            _MedicoServices = MedicoServices;
            _ICommonServicesMedico = ICommonServices;
            _Mapper = mapper;

            _MedicoUpdateValidator = validatorUpdate;
            _MedicoInsertValidator = validator;
        }

        //EXAMPLE USE JWT

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("MedicosWithTurnos/{IdMedico}")]

        public async Task<ActionResult<IEnumerable<TurnoDetalleDto>>> GetTurnoDetalle(int IdMedico)
        {
            var Turnos = await _MedicoServices.GetTurnosFromMedicos(IdMedico);
            if (Turnos != null)
            {
                return Ok(Turnos);
            }
            return NotFound();

        }

        [Authorize(Policy = "All")]

        [HttpGet("MedicosByEspecialidad")]

        public async Task<IEnumerable<MedicosEspecialidadDto>> GetMedicosEspecialidad() => await _MedicoServices.GetMedicosByEspecialty();

        [Authorize(Policy = "Admin")]
/*
        [HttpPost]
        public async Task<ActionResult<MedicoDto>> Insert(MedicoWithUserDto MedicoUser) // por body solo uno. UNIFICAR EN UN DTO Y CREAR DSPS
        {
            var ValidationResultMedico = await _MedicoInsertValidator.ValidateAsync(MedicoUser);

            if (!ValidationResultMedico.IsValid)
            {
                return BadRequest(ValidationResultMedico.Errors);
            }


            var MedicoPost = await _MedicoServices.InsertWithUser(MedicoUser);
            if (MedicoPost == null)
            {
                return BadRequest("Dni existente");
            }

            return CreatedAtAction(nameof(GetMedico), new { id = MedicoPost.Idmedico }, MedicoPost); // = 201
                                                                                                 //se dirije hacia getMedico despues de crear!, new id.. = parametro de la ruta , medico = body de la respuesta
        }
*/
        [Authorize(Policy = "Admin")]

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicoDto>> Update(int id, MedicoUpdateDto medico)
        {

            var validationResult = await _MedicoUpdateValidator.ValidateAsync(medico);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            var Medico = await _ICommonServicesMedico.Update(id, medico);
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

            if (await _ICommonServicesMedico.Delete(id) == null)
            {
                return BadRequest("Medico con turnos asociados o Inexistente");
            }
            return Ok(await _ICommonServicesMedico.Delete(id));

        }

        [Authorize(Policy = "Admin")]

        [HttpGet("All")]
        public async Task<IEnumerable<MedicoDto>> GetAll()
        {
            return await _ICommonServicesMedico.GetAll();
        }

        [Authorize(Policy = "MedicoOrAdmin")]

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoDto>> GetMedico(int id)
        {
            var Medico = await _ICommonServicesMedico.GetById(id);
            if (Medico == null)
            {
                return NotFound();
            }
            return Medico;

        }
    }
}
