namespace ApiCentroMedico.Dto.Obras_Sociales
{
    public class ObraSocialDto // UTILIZO MISMO PARA INSERT
    {
        public int Idobrasocial { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal Cobertura { get; set; }
    }
}
