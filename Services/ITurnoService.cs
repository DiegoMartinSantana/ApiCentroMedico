using ApiCentroMedico.Dto.Turnos;

namespace ApiCentroMedico.Services
{
    public interface ITurnoService 
    {

        public Task<IEnumerable<TurnoDto>> GetAll();

        public Task<TurnoDto> Add(TurnoInsertDto entity);

        public Task<TurnoDto> Cancel(int id);

        public Task<TurnoDto> GetById(int id);

    }
}
