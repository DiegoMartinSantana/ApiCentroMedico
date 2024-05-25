namespace ApiCentroMedico.Dto.Turnos
{
    public class TurnoInsertDto
    {
        public long Idturno { get; set; }

        public DateTime Fechahora { get; set; }

        public long Idmedico { get; set; }

        public long Idpaciente { get; set; }

        public int Duracion { get; set; }

    }
}
