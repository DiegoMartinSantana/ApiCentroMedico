using ApiCentroMedico.Dto;
using ApiCentroMedico.Models;
using AutoMapper;

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

        }

    }
}
