using ApiCentroMedico.Dto.Obras_Sociales;
using ApiCentroMedico.Models;
using ApiCentroMedico.Repository;
using AutoMapper;

namespace ApiCentroMedico.Services
{
    public class ObraSocialService : ICommonService<Obra_SocialDto, Obra_SocialDto, ObraSocialUpdateDto>
    {

        private IRepository<ObrasSociale> _ObraSocRepository;
        private IMapper _Mapping;
        public ObraSocialService(IRepository<ObrasSociale> repo, IMapper mapp)
        {
            _Mapping = mapp;
            _ObraSocRepository = repo;
        }

        public async Task<Obra_SocialDto> Delete(int id)
        {
            var Model = await _ObraSocRepository.GetById(id);
            if (Model == null)
            {
                return null; 
            }
            var ObraDto = _Mapping.Map<Obra_SocialDto>(Model);
            _ObraSocRepository.Delete(Model);
           await _ObraSocRepository.Save();
            return ObraDto;
        }

        public async Task<IEnumerable<Obra_SocialDto>> GetAll()
        {
            var Model = await _ObraSocRepository.GetAll();
            return Model.Select(x => _Mapping.Map<Obra_SocialDto>(x));

        }

        public async Task<Obra_SocialDto> GetById(int id)
        {
            var Model = await _ObraSocRepository.GetById(id);
            return Model== null ?null : _Mapping.Map<Obra_SocialDto>(Model);
        }

        public async Task<Obra_SocialDto> Insert(Obra_SocialDto entity)
        {
            
            if(entity == null)
            {
                return null;
            }

            var Model = _Mapping.Map<ObrasSociale>(entity);
            await _ObraSocRepository.Insert(Model);
            await _ObraSocRepository.Save();
            return _Mapping.Map<Obra_SocialDto>(Model);
        }

        public async Task<Obra_SocialDto> Update(int id, ObraSocialUpdateDto entity)
        {
            var Model = _ObraSocRepository.GetById(id);
            if(Model == null)
            {
                return null;
            }
            var ModelUpdate = _Mapping.Map<ObrasSociale>(entity);
            _ObraSocRepository.Update(ModelUpdate);
           await _ObraSocRepository.Save();
            return _Mapping.Map<Obra_SocialDto>(ModelUpdate);



        }
    }
}
