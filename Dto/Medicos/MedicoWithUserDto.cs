﻿namespace ApiCentroMedico.Dto.Medicos
{
    public class MedicoWithUserDto
    {
        public long Idmedico { get; set; }

        public int Idespecialidad { get; set; }

        public string Apellido { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public DateOnly FechaNac { get; set; }

        public decimal CostoConsulta { get; set; }
        public string Email { get; set; } = null!;
        public string Pass { get; set; } = null!;


    }
}
