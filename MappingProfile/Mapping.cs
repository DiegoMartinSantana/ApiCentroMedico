using ApiCentroMedico.Dto.Especialidades;
using ApiCentroMedico.Dto.Medicos;
using ApiCentroMedico.Dto.Obras_Sociales;
using ApiCentroMedico.Dto.Pacientes;
using ApiCentroMedico.Dto.Permisos;
using ApiCentroMedico.Dto.Turnos;
using ApiCentroMedico.Dto.Usuario;
using ApiCentroMedico.Models;
using AutoMapper;
using System.Runtime.CompilerServices;

namespace ApiCentroMedico.MappingProfile
{
    public class Mapping : Profile
    {
        // en el contructor del mapping creamos mapeos
        public Mapping()
        {
            #region Medicos
            //al ser iguales los nombres de las props , no necesito for member
            CreateMap<Medico, MedicoDto>();
            CreateMap<MedicoInsertDto, Medico>();
            CreateMap<MedicoUpdateDto, Medico>();
            CreateMap<MedicoWithUserDto, Medico>();
            CreateMap<Medico, MedicoWithUserDto>();
            CreateMap<MedicoWithUserDto,MedicoInsertDto>();

            #endregion

            #region Especialidades
            CreateMap<Especialidade, EspecialidadDto>();
            CreateMap<EspecialidadDto, Especialidade>();
            CreateMap<EspecialidadInsertDto, Especialidade>();
            CreateMap<EspecialidadDto, EspecialidadInsertDto>();

            #endregion

            #region ObrasSociales
            CreateMap<ObrasSociale, ObraSocialDto>();
            CreateMap<ObraSocialDto, ObrasSociale>();
            CreateMap<ObraSocialUpdateDto, ObrasSociale>();
            #endregion

            #region Pacientes
            CreateMap<Paciente, PacienteDto>();
            CreateMap<PacienteDto, Paciente>();
            CreateMap<Paciente, PacienteWithUserDto>();
            CreateMap<PacienteWithUserDto, Paciente>();
            #endregion

            #region Turnos
            CreateMap<Turno, TurnoDto>();
            CreateMap<TurnoDto, Turno>();
            CreateMap<TurnoInsertDto,Turno>();
            CreateMap<TurnoUpdateDto, Turno>();
            CreateMap<Turno, TurnoUpdateDto>();

            #endregion

            #region Users
            CreateMap<Usuario, UserDto>();
            CreateMap<UserDto, Usuario>();
            CreateMap<UserDto, MedicoWithUserDto>();
            CreateMap<MedicoWithUserDto, UserDto>();
            CreateMap<Usuario,PacienteWithUserDto>();
            CreateMap<PacienteWithUserDto, Usuario>();
            CreateMap<MedicoWithUserDto, Usuario>();
            CreateMap<Usuario, MedicoWithUserDto>();
            #endregion

            #region Permisos
            CreateMap<Permiso, PermisoDto>();
            CreateMap<PermisoDto, Permiso>();

            #endregion

        }

    }
}
