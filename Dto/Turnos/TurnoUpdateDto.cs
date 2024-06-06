namespace ApiCentroMedico.Dto.Turnos
{
    public class TurnoUpdateDto
    {
        public DateTime Fechahora { get; set; }

        public long Idmedico { get; set; }

        public int Duracion { get; set; }
    }
}
