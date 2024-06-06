
using ApiCentroMedico.Models;
using ApiCentroMedico.UnitWork;
using Microsoft.EntityFrameworkCore;

namespace ApiCentroMedico.Repository
{
    public class PacienteRepository : IRepository<Paciente>, IPacienteRepository
    {

        private CentromedicoContext _Context;
        private IUnitOfWork _UnitOfWork;
        public PacienteRepository(CentromedicoContext context)
        {

            _Context = context;
            _UnitOfWork = new UnitOfWork(context);
        }

        public async Task<Paciente> InsertWithUser(Paciente entity, Usuario user)
        {

            await Insert(entity);
            user.IdPermiso = _Context.Permisos.FirstOrDefault(x => x.Nombre == "Paciente").Idpermiso;

            await _UnitOfWork.UsuarioRepository.Insert(user);
            await _UnitOfWork.Save();
            return entity;
        }

        public async Task<Paciente> Insert(Paciente entity)
        {
            await _Context.Pacientes.AddAsync(entity);
            await Save();
            return entity;
        }
        public void Delete(Paciente entity)
        {
            _UnitOfWork.PacienteRepository.Delete(entity);
            var User = _Context.Usuarios.FirstOrDefault(x => x.IdPaciente == entity.Idpaciente);
            _UnitOfWork.UsuarioRepository.Delete(User);
            _UnitOfWork.Save();
        }

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
