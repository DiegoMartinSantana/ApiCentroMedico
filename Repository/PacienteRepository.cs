
using ApiCentroMedico.Models;
using ApiCentroMedico.UnitWork;
using Microsoft.EntityFrameworkCore;

namespace ApiCentroMedico.Repository
{
    public class PacienteRepository : IRepository<Paciente>, IPacienteRepository
    {

        private CentromedicoContext _Context;
        public PacienteRepository(CentromedicoContext context)
        {
            _Context = context;
        }

        public async Task<Paciente?> GetByDni(Paciente entity) => await _Context.Pacientes.FirstOrDefaultAsync(x => x.Dni == entity.Dni);

       

        public async Task<Paciente> Insert(Paciente entity)
        {
            await _Context.Pacientes.AddAsync(entity);
            return entity;
        }
        public  void Delete(Paciente entity) => _Context.Pacientes.Remove(entity);

        public async Task<IEnumerable<Paciente>> GetAll() => await _Context.Pacientes.Select(x => x).ToListAsync();

        public async Task<Paciente?> GetById(int id)
        {
            var Paciente = await _Context.Pacientes.FindAsync(long.Parse(id.ToString()));
            return Paciente == null ? null : Paciente;
        }

        public async Task Save()
        {
            await _Context.SaveChangesAsync();
        }

        public void Update(Paciente entity)
        {
            _Context.Pacientes.Update(entity);
            _Context.Pacientes.Entry(entity).State = EntityState.Modified;

        }

    }
}
