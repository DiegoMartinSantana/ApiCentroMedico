using ApiCentroMedico.Dto.Medicos;
using ApiCentroMedico.Dto.Turnos;
using ApiCentroMedico.Models;

namespace ApiCentroMedico.Repository
{
    public interface IMedicoRepository
    {
        public Task<IEnumerable<TurnoDetalleDto>> GetTurnosFromMedicos(int IdMedico);
      //  public Task<Medico> InsertWithUser(Medico medico, Usuario user);
        public Task<IEnumerable<MedicosEspecialidadDto>> GetMedicosByEspecialty();
        public Task<Medico> GetByDni(int? dni);
    }
}
