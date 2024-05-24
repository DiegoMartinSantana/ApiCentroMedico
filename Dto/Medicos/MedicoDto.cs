using System.ComponentModel;

namespace ApiCentroMedico.Dto.Medicos
{
    public class MedicoDto
    {
        //DTO PARA MOSTRAR
        public int Idespecialidad { get; set; }

        public string Apellido { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public DateOnly Fechaingreso { get; set; }
        public DateOnly FechaNac { get; set; }

        public decimal CostoConsulta { get; set; }
    }
}
