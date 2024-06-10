namespace ApiCentroMedico.Dto.Pacientes
{
    public class PacienteDto
    {

        public long Idpaciente { get; set; }
        public int Dni { get; set; }

        public string Apellido { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public int? Idobrasocial { get; set; }

        public string Sexo { get; set; } = null!;
        public DateOnly Fechanac { get; set; }


    }
}
