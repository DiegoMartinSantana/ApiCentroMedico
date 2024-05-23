using System;
using System.Collections.Generic;

namespace ApiCentroMedico.Models;

public partial class Paciente
{
    public long Idpaciente { get; set; }

    public string Apellido { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int? Idobrasocial { get; set; }

    public DateOnly Fechanac { get; set; }

    public string Sexo { get; set; } = null!;

    public virtual ObrasSociale? IdobrasocialNavigation { get; set; }

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
