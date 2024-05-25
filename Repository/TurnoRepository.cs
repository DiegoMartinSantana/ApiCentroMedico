using ApiCentroMedico.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCentroMedico.Repository
{
    public class TurnoRepository : ITurnoRepository
    {
        private DiagnosticoContext _Context;

        public TurnoRepository(DiagnosticoContext context)
        {
            _Context = context;
        }
        public async Task Add(Turno entity) => await _Context.Turnos.AddAsync(entity);
            

        public void Delete(Turno entity) => _Context.Turnos.Remove(entity);

        public async Task<IEnumerable<Turno>> GetAll() => await _Context.Turnos.Select(x => x).ToListAsync();

        public async Task<Turno> GetById(int id) => await _Context.Turnos.FindAsync(id);
        public async Task Save()
        {
          await   _Context.SaveChangesAsync();
        }
    }
}
