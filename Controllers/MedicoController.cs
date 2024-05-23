using ApiCentroMedico.Dto;
using ApiCentroMedico.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiCentroMedico.Controllers
{

    [Route("ApiCentroMedico/[controller]")]
    [ApiController] //permite facil enrutamiento
    public class MedicoController : ControllerBase
    {

        private ICommonServices<MedicoDto,MedicoInsertDto,MedicoUpdateDto> _MedicoServices;

        public MedicoController([FromKeyedServices("IMedicoServices")]ICommonServices<MedicoDto,MedicoInsertDto,MedicoUpdateDto> MedicoServices)
        {
            _MedicoServices = MedicoServices;
        }


        [HttpGet]
        public async Task<IEnumerable<MedicoDto>> Get()
        {
           return await _MedicoServices.GetAll();
        }

    }
}
