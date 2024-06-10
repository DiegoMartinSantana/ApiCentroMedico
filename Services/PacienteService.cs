using ApiCentroMedico.Dto.Pacientes;
using ApiCentroMedico.Dto.Usuario;
using ApiCentroMedico.Models;
using ApiCentroMedico.Repository;
using ApiCentroMedico.UnitWork;
using AutoMapper;

namespace ApiCentroMedico.Services
{
    public class PacienteService : ICommonService<PacienteDto, PacienteInsertDto, PacienteUpdateDto>, IPacienteService
    {

        private IPacienteRepository _PacienteRepositorySpecific;
        private IRepository<Paciente> _PacienteRepository;
        private IMapper _Mapping;
        private UnitOfWork _UnitOfWork;
        public PacienteService(IPacienteRepository RepoSpecific, IRepository<Paciente> Repo, IMapper Mapp,UnitOfWork unitOfWork)
        {
            _Mapping = Mapp;
            _PacienteRepository = Repo;
            _PacienteRepositorySpecific = RepoSpecific;
            _UnitOfWork = unitOfWork;
        }
        private bool ValidateDni(int dni)
        {
            var Dni = _PacienteRepository.GetAll().Result.Where(m => m.Dni == dni).FirstOrDefault();
            if (Dni == null)
            {
                return false;
            }
            return true;
        }
        

        public async Task<PacienteDto?> InsertWithUser(PacienteWithUserDto PacienteUser)
        {
            if(!ValidateDni(PacienteUser.Dni))
            {
                return null;
            }

            var Paciente = _Mapping.Map<Paciente>(PacienteUser);
            var User = _Mapping.Map<Usuario>(PacienteUser);

           await _UnitOfWork.PacienteRepository.Insert(Paciente);

            await _UnitOfWork.UsuarioRepository.Insert(User);

            await _UnitOfWork.Save();   //mandamos todo junto a la bd

            var PacienteInsertado = await _UnitOfWork.PacienteRepository.GetByDni(Paciente);
            if (PacienteInsertado != null)
            {
                User.IdPermiso = 1;
                User.IdPaciente = PacienteInsertado.Idpaciente;
            }
            else
            {
                return null;
            }
           _UnitOfWork.UsuarioRepository.Update(User);
            await _UnitOfWork.Save();   


            return _Mapping.Map<PacienteDto>(PacienteInsertado);

        }

        public async Task<PacienteDto?> Delete(int id)
        {
            var PacienteModel = await _PacienteRepository.GetById(id);
            if (PacienteModel == null)
            {
                return null;
            }
            
            _UnitOfWork.PacienteRepository.Delete(PacienteModel);

            var Usuarios = await _UnitOfWork.UsuarioRepository.GetAll();

            var User = Usuarios.FirstOrDefault(x => x.IdPaciente == PacienteModel.Idpaciente);
            if (User != null)
            {
            _UnitOfWork.UsuarioRepository.Delete(User);
              await  _UnitOfWork.Save();
            }
            else
            {
                return null;
            }

            return _Mapping.Map<PacienteDto>(PacienteModel);

        }

        public async Task<IEnumerable<PacienteDto>> GetAll()
        {
            var Pacientes = await _PacienteRepository.GetAll();
            var PacientesDto = Pacientes.Select(x => _Mapping.Map<PacienteDto>(x));
            return PacientesDto;

        }

        public async Task<PacienteDto?> GetById(int id)
        {
            var Model = await _PacienteRepository.GetById(id);
            return Model == null ? null : _Mapping.Map<PacienteDto>(Model);
        }

        public async Task<PacienteDto?> Insert(PacienteInsertDto entity)
        {
            if (entity == null)
            {
                return null;
            }
            var Model = _Mapping.Map<Paciente>(entity);
            await _PacienteRepository.Insert(Model);

            var PacienteDto = _Mapping.Map<PacienteDto>(Model);
            return PacienteDto;

        }

        public async Task<PacienteDto?> Update(int id, PacienteUpdateDto entity)
        {
            var Model = await _PacienteRepository.GetById(id);
            if (Model == null)
            {
                return null;
            }
            Model.Idobrasocial = entity.Idobrasocial;

            _PacienteRepository.Update(Model);
            await _PacienteRepository.Save();
            return _Mapping.Map<PacienteDto>(Model);

        }
    }
}
