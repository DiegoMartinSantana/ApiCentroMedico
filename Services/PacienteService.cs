using ApiCentroMedico.Dto.Pacientes;
using ApiCentroMedico.Models;
using ApiCentroMedico.Repository;
using AutoMapper;

namespace ApiCentroMedico.Services
{
    public class PacienteService : ICommonService<PacienteDto, PacienteInsertDto, PacienteUpdateDto>
    {

        private IRepository<Paciente> _PacienteRepository;
        private IMapper _Mapping;
        public PacienteService(IRepository<Paciente> PacienteRepository, IMapper Mapp)
        {
            _Mapping = Mapp;
            _PacienteRepository = PacienteRepository;
        }
        public async Task<PacienteDto> Delete(int id)
        {
            var PacienteModel = await _PacienteRepository.GetById(id);
            if(PacienteModel == null)
            {
                return null;
            }
            _PacienteRepository.Delete(PacienteModel);
            await _PacienteRepository.Save();
            var PacienteDto = _Mapping.Map<PacienteDto>(PacienteModel);
            return PacienteDto;

        }

        public async Task<IEnumerable<PacienteDto>> GetAll()
        {
            var Pacientes = await _PacienteRepository.GetAll();
            var PacientesDto = Pacientes.Select(x => _Mapping.Map<PacienteDto>(x)); 
            return PacientesDto;

        }

        public async Task<PacienteDto> GetById(int id)
        {
           var Model = await _PacienteRepository.GetById(id);
            return Model == null? null : _Mapping.Map<PacienteDto>(Model);
        }

        public async Task<PacienteDto> Insert(PacienteInsertDto entity)
        {
            if (entity == null)
            {
                return null;
            }
            var Model = _Mapping.Map<Paciente>(entity);
            await _PacienteRepository.Insert(Model);
            await _PacienteRepository.Save();
            var PacienteDto = _Mapping.Map<PacienteDto>(Model);
            return PacienteDto;

        }

        public async Task<PacienteDto> Update(int id, PacienteUpdateDto entity)
        {
            var Model = await _PacienteRepository.GetById(id);
            if(Model == null)
            {
                return null;
            }
            _PacienteRepository.Update(Model);
            await _PacienteRepository.Save();
            return _Mapping.Map<PacienteDto>(Model);

        }
    }
}
