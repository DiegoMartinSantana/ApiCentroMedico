﻿using ApiCentroMedico.Dto.Especialidades;
using ApiCentroMedico.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCentroMedico.Controllers
{
    [Route("ApiCentroMedico/[controller]")]

    [ApiController]
    public class EspecialidadController : ControllerBase
    {

        private ICommonService<EspecialidadDto,EspecialidadDto,EspecialidadDto> _EspecialidadServices;

        public EspecialidadController([FromKeyedServices("EspecialidadService")]ICommonService<EspecialidadDto, EspecialidadDto, EspecialidadDto> especialidadServices)
        {
            _EspecialidadServices = especialidadServices;
        }

        [Authorize(Policy = "Admin")]

        [HttpPut("{id}")]
        public async Task<ActionResult<EspecialidadDto>> Update( int id, EspecialidadDto especialidad)
        {
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

        [HttpPost]
        public async Task<ActionResult<EspecialidadDto>> Insert(EspecialidadDto especialidad)
        {
            var DtoService = await _EspecialidadServices.Insert(especialidad);
            if(DtoService == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = especialidad.Idespecialidad }, especialidad);

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
