using ApiCentroMedico.Dto.Pacientes;
using ApiCentroMedico.Dto.Usuario;

namespace ApiCentroMedico.Services
{
    public interface IPacienteService
    {
        public Task<PacienteDto> InsertWithUser(PacienteInsertDto entity, UserDto user);
    }
}
