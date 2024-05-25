namespace ApiCentroMedico.Dto.Pacientes
{
    public class PacienteUpdateDto
    {
        public string Apellido { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public int? Idobrasocial { get; set; }
    }
}
