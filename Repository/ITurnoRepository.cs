using ApiCentroMedico.Models;
using Microsoft.Identity.Client;

namespace ApiCentroMedico.Repository
{
    public interface ITurnoRepository
    {

        public Task Save();
        public void Delete(Turno entity);

        public Task Add(Turno entity);

        public Task <IEnumerable<Turno>> GetAll();

        public Task<Turno> GetById(int id);



    }
}
