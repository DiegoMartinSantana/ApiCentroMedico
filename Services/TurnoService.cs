﻿using ApiCentroMedico.Dto.Obras_Sociales;
using ApiCentroMedico.Dto.Turnos;
using ApiCentroMedico.Models;
using ApiCentroMedico.Repository;
using AutoMapper;

namespace ApiCentroMedico.Services
{
    public class TurnoService : ICommonService<TurnoDto,TurnoInsertDto,TurnoUpdateDto>
     {
        private IRepository<Turno> _TurnoRepository;

        private IMapper _Mapping;

        public TurnoService(IRepository<Turno> repository, IMapper mapp)
        {
            _Mapping = mapp;
            _TurnoRepository = repository;
        }
        public async Task<TurnoDto> GetById(int id)
        {
            var ModelTurno = await _TurnoRepository.GetById(id);
            return ModelTurno == null ? null : _Mapping.Map<TurnoDto>(ModelTurno);
        }   

        public async Task<TurnoDto> Insert(TurnoInsertDto entity)
        {
            if (entity == null)
            {
                return null;
            }
            var TurnoModel = _Mapping.Map<Turno>(entity);
            await _TurnoRepository.Insert(TurnoModel);
            await _TurnoRepository.Save();
            return _Mapping.Map<TurnoDto>(TurnoModel);

        }

        public async Task<TurnoDto> Delete(int id)
        {
            var ModelTurno = await _TurnoRepository.GetById(id);
            if (ModelTurno == null)
            {
                return null;
            }
            _TurnoRepository.Delete(ModelTurno);

            await _TurnoRepository.Save();
            return null;

        }

        public async Task<IEnumerable<TurnoDto>> GetAll()
        {
            var Turnos = await _TurnoRepository.GetAll();
            return Turnos.Select(x => _Mapping.Map<TurnoDto>(x));

        }

        public async Task<TurnoDto> Update(int id, TurnoUpdateDto entity)
        {
            var Model = await _TurnoRepository.GetById(id);
            if (Model == null)
            {
                return null;
            }
            Model.Fechahora = entity.Fechahora;
            Model.Idmedico = entity.Idmedico;
            Model.Duracion = entity.Duracion;

            _TurnoRepository.Update(Model);
            await _TurnoRepository.Save();
            return _Mapping.Map<TurnoDto>(Model);


        }
    }
}
