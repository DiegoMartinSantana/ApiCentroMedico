using ApiCentroMedico.Dto.Medicos;
using ApiCentroMedico.Dto.Turnos;
using ApiCentroMedico.Models;
using ApiCentroMedico.UnitWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ApiCentroMedico.Repository
{
    public class MedicoRepository : IRepository<Medico>, IMedicoRepository
    {
        //recibimos el context

        private IUnitOfWork _unitOfWork;
        
        private CentromedicoContext _context;
        public MedicoRepository(CentromedicoContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
        }

        public async Task<IEnumerable<MedicosEspecialidadDto>> GetMedicosByEspecialty()
        {
            var Medicos_Especialidad =
                from M in _context.Medicos
                join E in _context.Especialidades
                on M.Idespecialidad equals E.Idespecialidad
                select new MedicosEspecialidadDto
                {
                    Idmedico = M.Idmedico,
                    Idespecialidad = M.Idespecialidad,
                    Especialidad = E.Nombre,
                    Apellido = M.Apellido,
                    Nombre = M.Nombre,
                    CostoConsulta = M.CostoConsulta
                };
            return await Medicos_Especialidad.ToListAsync();
        }

        public void Delete(Medico entity)
        {
            _unitOfWork.MedicoRepository.Delete(entity);
            var User= _context.Usuarios.FirstOrDefault(x =>  x.IdMedico == entity.Idmedico );
            _unitOfWork.UsuarioRepository.Delete(User);
            _unitOfWork.Save();
        }
        public async Task<IEnumerable<Medico>> GetAll() => await _context.Medicos.ToListAsync<Medico>();


        public async Task<Medico?> GetById(int id) => await _context.Medicos.FindAsync(long.Parse(id.ToString()));


        public async Task<Medico> Insert(Medico entity)
        {
            await _context.Medicos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void Update(Medico entity)
        {

            _context.Medicos.Attach(entity);
            _context.Medicos.Entry(entity).State = EntityState.Modified;

        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TurnoDetalleDto>> GetTurnosFromMedicos(int IdMedico)
        {


            long IdM = long.Parse(IdMedico.ToString());
            //devuelve los turnos asociados a un Medico
            var Turnos = from P in _context.Pacientes
                            join T in _context.Turnos
                            on P.Idpaciente equals T.Idpaciente
                            join M in _context.Medicos on T.Idmedico equals M.Idmedico
                            join E in _context.Especialidades on M.Idespecialidad equals E.Idespecialidad
                            where M.Idmedico == IdM
                            select new TurnoDetalleDto
                            {
                                FechahoraTurno = T.Fechahora,
                                Duracion = T.Duracion,
                                NombrePaciente = P.Nombre,
                                ApellidoPaciente = P.Apellido,
                                NombreMedico = M.Nombre,
                                Especialidad = E.Nombre,
                                ApellidoMedico = M.Apellido,
                                CostoConsulta = M.CostoConsulta
                            };
            return await Turnos.ToListAsync();


        }

        public async Task<Medico> InsertWithUser(Medico medico, Usuario user)
        {
           await _unitOfWork.MedicoRepository.Insert(medico);
            user.IdPermiso = _context.Permisos.FirstOrDefault(x => x.Nombre == "Medico").Idpermiso;

          await  _unitOfWork.UsuarioRepository.Insert(user);
            await _unitOfWork.Save();

            //medico ya contiene id 
            var MedicoIn = _context.Medicos.FirstOrDefault(medico);
            user.IdMedico = MedicoIn.Idmedico;
            _unitOfWork.UsuarioRepository.Update(user);
          await  _unitOfWork.Save();
                return medico;

        }

      
    }
}
