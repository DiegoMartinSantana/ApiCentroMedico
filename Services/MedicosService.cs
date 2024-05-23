﻿using ApiCentroMedico.Dto;
using ApiCentroMedico.MappingProfile;
using ApiCentroMedico.Models;
using ApiCentroMedico.Repository;
using AutoMapper;
using Microsoft.Identity.Client;

namespace ApiCentroMedico.Services
{
    public class MedicosService : ICommonServices<MedicoDto, MedicoInsertDto, MedicoUpdateDto>
    {

        private IRepository<Medico> _MedicoRepository;
        private IMapper _Mapping;
        public MedicosService(IRepository<Medico> repo, IMapper mapp) //ya estan inyectados
        {
            _Mapping = mapp;
            _MedicoRepository = repo;
        }

        public async Task<MedicoDto> Update(MedicoUpdateDto update)
        {
            var UpdateModel = _Mapping.Map<Medico>(update);

            _MedicoRepository.Update(UpdateModel);

            await _MedicoRepository.Save();

            return _Mapping.Map<MedicoDto>(UpdateModel);
        }

        public async Task<MedicoDto> Delete(int id)
        {
            var MedicoModel = await _MedicoRepository.GetById(id);

            if (MedicoModel == null)
            {
                return null;
            }
            var MedicoDto = _Mapping.Map<MedicoDto>(MedicoModel);   
            _MedicoRepository.Delete(id);
            await _MedicoRepository.Save();

            return MedicoDto;


        }
        public async Task<MedicoDto> Insert(MedicoInsertDto insert)
        {
            if (insert == null)
            {
                return null;
            }
            var MedicoModel = _Mapping.Map<Medico>(insert);
            var Medicodto = _Mapping.Map<MedicoDto>(MedicoModel);
            await _MedicoRepository.Insert(MedicoModel); // mapeo a la inversa ahora
            await _MedicoRepository.Save();

            return Medicodto;


        }
        public async Task<MedicoDto> GetById(int id)
        {
            var MedicoModel = await _MedicoRepository.GetById(id);
            if (MedicoModel == null)
            {
                return null;
            }
            MedicoDto Medico = _Mapping.Map<MedicoDto>(MedicoModel);

            return Medico;

        }

        //manejamos async , la interfaz usa Task

        public async Task<IEnumerable<MedicoDto>> GetAll()
        {

            var Medicos = await _MedicoRepository.GetAll(); //devuelve models no dto
                                                            // await = no hagas mas nada hasta obtener de mi repository todo
            var ListaMedicos = Medicos.Select(M => _Mapping.Map<MedicoDto>(M)); //mapeo de model a dto

            return ListaMedicos;


        }

    }
}
