using ApiCentroMedico.Dto.Especialidades;
using ApiCentroMedico.Dto.Medicos;
using ApiCentroMedico.Dto.Obras_Sociales;
using ApiCentroMedico.Dto.Pacientes;
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
            #endregion

            #region Especialidades
            CreateMap<Especialidade, EspecialidadDto>();
            CreateMap<EspecialidadDto, Especialidade>();
            #endregion

            #region ObrasSociales
           CreateMap<ObrasSociale, Obra_SocialDto>();
            CreateMap<Obra_SocialDto, ObrasSociale>();
            CreateMap<ObraSocialUpdateDto, ObrasSociale>();
            #endregion

            #region Pacientes
            CreateMap<Paciente, PacienteDto>();
            CreateMap<PacienteDto, Paciente>();
            CreateMap<PacienteInsertDto, Paciente>();
            #endregion

            #region Turnos
            CreateMap<Turno, TurnoDto>();
            CreateMap<TurnoDto, Turno>();
            CreateMap<TurnoInsertDto,Turno>();
            #endregion

            #region Users
            CreateMap<Usuario, UserDto>();
            CreateMap<UserDto, Usuario>();
            #endregion

        }

    }
}
