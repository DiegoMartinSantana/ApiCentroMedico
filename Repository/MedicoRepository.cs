using ApiCentroMedico.Dto.Medicos;
using ApiCentroMedico.Dto.Pacientes;
using ApiCentroMedico.Dto.Turnos;
using ApiCentroMedico.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCentroMedico.Repository
{
    public class MedicoRepository : IRepository<Medico>, IMedicoRepository
    {
        //recibimos el context

        private CentromedicoContext _context;
        public MedicoRepository(CentromedicoContext context)
        {
            _context = context;
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

        public void Delete(Medico entity) => _context.Remove(entity);

        public async Task<IEnumerable<Medico>> GetAll() => await _context.Medicos.ToListAsync<Medico>();


        public async Task<Medico> GetById(int id) => await _context.Medicos.FindAsync(long.Parse(id.ToString()));


        public async Task Insert(Medico entity) => await _context.Medicos.AddAsync(entity);

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
    }
}
