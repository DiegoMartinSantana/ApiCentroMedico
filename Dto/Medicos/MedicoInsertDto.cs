namespace ApiCentroMedico.Dto.Medicos
{
    public class MedicoInsertDto
    {

        public int Idespecialidad { get; set; }

        public string Apellido { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Sexo { get; set; } = null!;

        public DateOnly Fechanac { get; set; }

        public DateOnly Fechaingreso { get; set; }

        public decimal CostoConsulta { get; set; }

    }
}
