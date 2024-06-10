using System.ComponentModel;

namespace ApiCentroMedico.Dto.Medicos
{
    public class MedicoDto
    {
        //DTO PARA MOSTRAR
        public long Idmedico { get; set; }
         
        public int Dni { get; set; }

        public int Idespecialidad { get; set; }

        public string Apellido { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public DateOnly FechaNac { get; set; }

        public decimal CostoConsulta { get; set; }
    }
}
