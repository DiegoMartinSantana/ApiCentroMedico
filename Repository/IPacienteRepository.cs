using ApiCentroMedico.Models;

namespace ApiCentroMedico.Repository
{
    public interface IPacienteRepository
    {
        public Task<Paciente?> GetByDni(Paciente entity);
        
    }
}
