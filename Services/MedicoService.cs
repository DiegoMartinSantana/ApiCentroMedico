using ApiCentroMedico.Dto.Medicos;
using ApiCentroMedico.Dto.Turnos;
using ApiCentroMedico.Dto.Usuario;
using ApiCentroMedico.MappingProfile;
using ApiCentroMedico.Models;
using ApiCentroMedico.Repository;
using ApiCentroMedico.UnitWork;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ApiCentroMedico.Services
{
    public class MedicoService : ICommonService<MedicoDto, MedicoInsertDto, MedicoUpdateDto>, IMedicoService
    {

        private IMedicoRepository _MedicoRepositorySpecific;
        private IRepository<Medico> _MedicoRepository;

        private IUnitOfWork _unitOfWork;
        private IMapper _Mapping;
        public MedicoService(IRepository<Medico> repo, IMedicoRepository repoSpecific, IMapper mapp
            , IUnitOfWork unitOfWork) //ya estan inyectados
        {
            _Mapping = mapp;
            _MedicoRepositorySpecific = repoSpecific;
            _MedicoRepository = repo;
            _unitOfWork = unitOfWork;

        }

        private bool ValidateDni(int dni)
        {

            var Dni = _MedicoRepositorySpecific.GetByDni(dni);

            if (Dni == null)
            {
                return true;
            }
            return false;
        }


        public async Task<MedicoDto> InsertWithUser(MedicoWithUserDto MedicoUser)
        {
            if (!ValidateDni(MedicoUser.Dni))
            {
                return null;
            }

            var user = _Mapping.Map<Usuario>(MedicoUser);
            var medico = _Mapping.Map<Medico>(MedicoUser);

            await _unitOfWork.MedicoRepository.Insert(medico);

            
            user.IdPermiso = 2; // reemplaza por repository Permisos.. por si agregamos mas..

            await _unitOfWork.UsuarioRepository.Insert(user);
            
            await _unitOfWork.Save(); //Realiza ambos insert a la vez. Por si hay error en alguno. SIMIL TRANSACTION SQL

            var MedicoInsertado = await _unitOfWork.MedicoRepository.GetByDni(medico.Dni);

            user.IdMedico = MedicoInsertado.Idmedico;

            _unitOfWork.UsuarioRepository.Update(user);

            await _unitOfWork.Save();

            return _Mapping.Map<MedicoDto>(MedicoInsertado); // ya contiene id.
        }


        public async Task<IEnumerable<MedicosEspecialidadDto>> GetMedicosByEspecialty() => await _MedicoRepositorySpecific.GetMedicosByEspecialty();
        public async Task<MedicoDto> Update(int id, MedicoUpdateDto update)
        {
            var UpdateModel = await _MedicoRepository.GetById(id);
            if (UpdateModel == null)
            {
                return null;
            }

            UpdateModel.CostoConsulta = update.CostoConsulta;

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
            var Turnos = await _MedicoRepositorySpecific.GetTurnosFromMedicos(id);
            if (Turnos.Count() > 0)
            {
                return null;
            }

            _unitOfWork.MedicoRepository.Delete(MedicoModel);
            var Usuarios = await _unitOfWork.UsuarioRepository.GetAll();
            var User = Usuarios.FirstOrDefault(x => x.IdMedico == MedicoModel.Idmedico);
            if (User == null)
            {
                return null;
            }
            _unitOfWork.UsuarioRepository.Delete(User);
            await _unitOfWork.Save();
            var MedicoDto = _Mapping.Map<MedicoDto>(MedicoModel);

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
            return MedicoModel == null ? null : _Mapping.Map<MedicoDto>(MedicoModel);

        }

        //manejamos async , la interfaz usa Task

        public async Task<IEnumerable<MedicoDto>> GetAll()
        {

            var Medicos = await _MedicoRepository.GetAll(); //devuelve models no dto
                                                            // await = no hagas mas nada hasta obtener de mi repository todo
            var ListaMedicos = Medicos.Select(M => _Mapping.Map<MedicoDto>(M)); //mapeo de model a dto

            return ListaMedicos;


        }

        public async Task<IEnumerable<TurnoDetalleDto>> GetTurnosFromMedicos(int idMedico)
        {
            var Exists = await _MedicoRepository.GetById(idMedico);
            if (Exists == null)
            {
                return null;
            }

            return await _MedicoRepositorySpecific.GetTurnosFromMedicos(idMedico);

        }

    }
}
