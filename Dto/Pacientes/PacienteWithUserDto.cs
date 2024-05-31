namespace ApiCentroMedico.Dto.Pacientes
{
    public class PacienteWithUserDto
    {

        public string Apellido { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public int? Idobrasocial { get; set; }

        public string Sexo { get; set; } = null!;
        public DateOnly Fechanac { get; set; }
        public string Email { get; set; } = null!;
        public string Pass { get; set; } = null!;
    }
}

