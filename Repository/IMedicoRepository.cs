﻿using ApiCentroMedico.Dto.Turnos;
using ApiCentroMedico.Models;

namespace ApiCentroMedico.Repository
{
    public interface IMedicoRepository
    {
        public Task<IEnumerable<TurnoDetalleDto>> GetTurnosFromMedicos(int IdMedico);
        public Task<Medico> InsertWithUser(Medico medico, Usuario user);
    }
}
