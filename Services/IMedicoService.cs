using ApiCentroMedico.Dto.Medicos;
using ApiCentroMedico.Dto.Pacientes;
using ApiCentroMedico.Dto.Turnos;
using ApiCentroMedico.Dto.Usuario;

namespace ApiCentroMedico.Services
{
    public interface IMedicoService 
    {
        public Task<IEnumerable<TurnoDetalleDto>> GetTurnosFromMedicos(int idMedico);
        public Task<IEnumerable<MedicosEspecialidadDto>> GetMedicosByEspecialty();

 //       public Task<MedicoDto> InsertWithUser (MedicoWithUserDto MedicoUser);
    }
}
