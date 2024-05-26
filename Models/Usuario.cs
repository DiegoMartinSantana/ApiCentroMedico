using System;
using System.Collections.Generic;

namespace ApiCentroMedico.Models;

public partial class Usuario
{
    public long IdPaciente { get; set; }

    public string Email { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;
}
