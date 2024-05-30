using ApiCentroMedico.Models;
using ApiCentroMedico.Repository;

namespace ApiCentroMedico.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private PacienteRepository _pacienteRepository;
        private MedicoRepository _medicoRepository;
        private IRepository<Usuario> _usuarioRepository;

        private CentromedicoContext _context;
        public UnitOfWork(CentromedicoContext context)
        {
            _context = context;

        }
        public MedicoRepository MedicoRepository
        {
            get
            {
                return _medicoRepository == null ? new MedicoRepository(_context) : _medicoRepository;
            }
        }
        public IRepository<Usuario> UsuarioRepository
        {
            get
            {
                return _usuarioRepository == null ? new Repository<Usuario>(_context) : _usuarioRepository;
            }
        }

        public PacienteRepository PacienteRepository
        {
            get
            {
                return _pacienteRepository == null ? new PacienteRepository(_context) : _pacienteRepository;    
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
