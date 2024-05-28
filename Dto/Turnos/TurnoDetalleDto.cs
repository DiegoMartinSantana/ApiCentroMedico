namespace ApiCentroMedico.Dto.Turnos
{
    public class TurnoDetalleDto
    {

        public DateTime FechahoraTurno { get; set; }


        public int Duracion { get; set; }

        public string NombrePaciente { get; set; }

        public string ApellidoPaciente { get; set; }

        public string NombreMedico { get; set; }

        public string ApellidoMedico { get; set; }

        public string Especialidad { get; set; }

        public decimal CostoConsulta { get; set; }
    }
}
