using ApiCentroMedico.Dto.Especialidades;
using ApiCentroMedico.Models;
using ApiCentroMedico.Repository;
using AutoMapper;

namespace ApiCentroMedico.Services
{
    public class EspecialidadService : ICommonService<EspecialidadDto, EspecialidadInsertDto, EspecialidadDto>

    {
        private IRepository<Especialidade> _EspecialidadRepository;
        private IMapper _Mapper;
        public EspecialidadService(IRepository<Especialidade> repository, IMapper mapper)
        {
            _EspecialidadRepository = repository;
            _Mapper = mapper;
        }

        public async Task<EspecialidadDto> Delete(int id)
        {
            var EspecialidadModel = await _EspecialidadRepository.GetById(id);

            if (EspecialidadModel == null)
            {
                return null;
            }

            _EspecialidadRepository.Delete(EspecialidadModel);

            await _EspecialidadRepository.Save();

            var Convert = _Mapper.Map<EspecialidadDto>(EspecialidadModel);
            return Convert;

        }

        public async Task<IEnumerable<EspecialidadDto>> GetAll()
        {
            var Especialidades = await _EspecialidadRepository.GetAll();
            var Convert = Especialidades.Select(e => _Mapper.Map<EspecialidadDto>(e));
            return Convert;

        }




        public async Task<EspecialidadDto> GetById(int id)
        {

            var Model = await _EspecialidadRepository.GetById(id);


            return Model == null ? null : _Mapper.Map<EspecialidadDto>(Model);

        }

        public async Task<EspecialidadDto> Insert(EspecialidadInsertDto entity)
        {
            if (entity == null)
            {
                return null;
            }
            var Model = _Mapper.Map<Especialidade>(entity);
            var Insertado = _EspecialidadRepository.Insert(Model);
            await _EspecialidadRepository.Save();

            return _Mapper.Map<EspecialidadDto>(Insertado);
        }

        public async Task<EspecialidadDto> Update(int id, EspecialidadDto entity)
        {
            var ModelUpdate = await _EspecialidadRepository.GetById(id);
            if (ModelUpdate == null)
            {
                return null;
            }
            ModelUpdate.Nombre = entity.Nombre;

            _EspecialidadRepository.Update(ModelUpdate);

            await _EspecialidadRepository.Save();
            return _Mapper.Map<EspecialidadDto>(ModelUpdate);
        }
    }
}
