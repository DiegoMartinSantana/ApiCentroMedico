using ApiCentroMedico.Dto.Obras_Sociales;
using ApiCentroMedico.Models;
using ApiCentroMedico.Repository;
using AutoMapper;

namespace ApiCentroMedico.Services
{
    public class ObraSocialService : ICommonService<ObraSocialDto, ObraSocialInsertDto, ObraSocialUpdateDto>
    {

        private IRepository<ObrasSociale> _ObraSocRepository;
        private IMapper _Mapping;
        public ObraSocialService(IRepository<ObrasSociale> repo, IMapper mapp)
        {
            _Mapping = mapp;
            _ObraSocRepository = repo;
        }

        public async Task<ObraSocialDto> Delete(int id)
        {
            var Model = await _ObraSocRepository.GetById(id);
            if (Model == null)
            {
                return null; 
            }
            var ObraDto = _Mapping.Map<ObraSocialDto>(Model);
            _ObraSocRepository.Delete(Model);
           await _ObraSocRepository.Save();
            return ObraDto;
        }

        public async Task<IEnumerable<ObraSocialDto>> GetAll()
        {
            var Model = await _ObraSocRepository.GetAll();
            return Model.Select(x => _Mapping.Map<ObraSocialDto>(x));

        }

        public async Task<ObraSocialDto> GetById(int id)
        {
            var Model = await _ObraSocRepository.GetById(id);
            return Model== null ?null : _Mapping.Map<ObraSocialDto>(Model);
        }

        public async Task<ObraSocialDto> Insert(ObraSocialInsertDto entity)
        {
            
            if(entity == null)
            {
                return null;
            }

            var Model = _Mapping.Map<ObrasSociale>(entity);
            await _ObraSocRepository.Insert(Model);
            await _ObraSocRepository.Save();
            return _Mapping.Map<ObraSocialDto>(Model);
        }

        public async Task<ObraSocialDto> Update(int id, ObraSocialUpdateDto entity)
        {
            var Model = _ObraSocRepository.GetById(id);
            if(Model == null)
            {
                return null;
            }
            var ModelUpdate = _Mapping.Map<ObrasSociale>(entity);
            _ObraSocRepository.Update(ModelUpdate);
           await _ObraSocRepository.Save();
            return _Mapping.Map<ObraSocialDto>(ModelUpdate);



        }
    }
}
