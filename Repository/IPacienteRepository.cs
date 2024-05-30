using ApiCentroMedico.Models;

namespace ApiCentroMedico.Repository
{
    public interface IPacienteRepository
    {
        public  Task<Paciente> InsertWithUser(Paciente entity, Usuario user);
        
    }
}
