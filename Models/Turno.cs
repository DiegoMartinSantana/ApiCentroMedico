using System;
using System.Collections.Generic;

namespace ApiCentroMedico.Models;

public partial class Turno
{
    public long Idturno { get; set; }

    public DateTime Fechahora { get; set; }

    public long Idmedico { get; set; }

    public long Idpaciente { get; set; }

    public int Duracion { get; set; }

    public virtual Medico IdmedicoNavigation { get; set; } = null!;

    public virtual Paciente IdpacienteNavigation { get; set; } = null!;
}
