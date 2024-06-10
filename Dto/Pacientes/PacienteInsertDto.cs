namespace ApiCentroMedico.Dto.Pacientes
{
    public class PacienteInsertDto
    {
        public int Dni { get; set; }

        public string Apellido { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public int? Idobrasocial { get; set; }

        public DateOnly Fechanac { get; set; }

        public string Sexo { get; set; } = null!;
    }
}
