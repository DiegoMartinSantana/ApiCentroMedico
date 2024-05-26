
using ApiCentroMedico.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCentroMedico.Repository
{
    public class EspecialidadRepository : IRepository<Especialidade>
    {
        private CentromedicoContext _Context;
        public EspecialidadRepository (CentromedicoContext context)
        {
            _Context = context;
        }

        public void Delete(Especialidade entity) => _Context.Remove(entity);


        public async Task<IEnumerable<Especialidade>> GetAll() => await _Context.Especialidades.ToListAsync<Especialidade>();


        public async Task<Especialidade> GetById(int id) => await _Context.Especialidades.FindAsync(id);
        

        public async Task Insert(Especialidade entity) => await _Context.Especialidades.AddAsync(entity);

        public async Task Save()
        {
           await _Context.SaveChangesAsync();
        }

        public void Update(Especialidade entity)
        {
           //service no llama hasta validar existencia
           _Context.Especialidades.Attach(entity);
            _Context.Especialidades.Entry(entity).State = EntityState.Modified;
        }
    }
}
