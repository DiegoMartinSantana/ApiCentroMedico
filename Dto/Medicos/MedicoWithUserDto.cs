namespace ApiCentroMedico.Dto.Medicos
{
    public class MedicoWithUserDto
    {
    

        public int Idespecialidad { get; set; }
        public int Dni { get; set; }

        public string Apellido { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Sexo { get; set; } = null!;

        public DateOnly FechaNac { get; set; }

        public DateOnly Fechaingreso { get; set; }


        public decimal CostoConsulta { get; set; }
        public string Email { get; set; } = null!;
        public string Pass { get; set; } = null!;


    }
}
