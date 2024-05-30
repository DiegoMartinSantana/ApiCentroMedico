using ApiCentroMedico.Models;
using ApiCentroMedico.Repository;
using Microsoft.Identity.Client;
using System.Net.Sockets;

namespace ApiCentroMedico.UnitWork
{
    public interface IUnitOfWork
    {

        public  IRepository<Usuario> UsuarioRepository { get; }
        public PacienteRepository PacienteRepository { get; }
        public MedicoRepository MedicoRepository { get; }
        public Task Save();
    }
}
