using System.ComponentModel;

namespace ApiCentroMedico.Dto
{
    public class MedicoDto
    {
        //DTO PARA MOSTRAR
        public int Idespecialidad { get;  }

        public string Apellido { get; }

        public string Nombre { get;  } 

        public DateOnly Fechaingreso { get;  }
        public DateOnly FechaNac { get; }

        public decimal CostoConsulta { get;  }
    }
}
