using ApiCentroMedico.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCentroMedico.Repository
{
    public class MedicoRepository : IRepository<Medico>
    {
        //recibimos el context

        private  DiagnosticoContext _context;
        public MedicoRepository(DiagnosticoContext context)
        {
            _context = context;
        }



        public void Delete(int id) => _context.Remove(id);

        public async Task<IEnumerable<Medico>> GetAll() => await _context.Medicos.ToListAsync<Medico>();
       

        public async Task<Medico> GetById(int id) => await _context.Medicos.FindAsync(id);


        public async Task Insert(Medico entity) => await _context.Medicos.AddAsync(entity); 

        public void  Update(Medico entity)
        {
            var Existe = _context.Find<Medico>(entity);
            if (Existe != null)
            {
                _context.Medicos.Attach(entity);
                _context.Medicos.Entry(entity).State =EntityState.Modified;

            }
           
        }
        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }
    }
}
