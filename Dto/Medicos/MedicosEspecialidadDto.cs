namespace ApiCentroMedico.Dto.Medicos
{
    public class MedicosEspecialidadDto
    {

        public long Idmedico { get; set; }
        
        public int Idespecialidad { get; set; }
        public string Especialidad { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public decimal CostoConsulta { get; set; }

    }

}
