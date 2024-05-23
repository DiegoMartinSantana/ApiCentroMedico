using ApiCentroMedico.Models;

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



        public Task<Medico> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Medico>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Medico> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Medico> Insert(Medico entity)
        {
            throw new NotImplementedException();
        }

        public Task<Medico> Update(Medico entity)
        {
            throw new NotImplementedException();
        }
    }
}
