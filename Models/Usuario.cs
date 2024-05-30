using System;
using System.Collections.Generic;

namespace ApiCentroMedico.Models;

public partial class Usuario
{
    public long? IdPaciente { get; set; }

    public string Email { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public int IdPermiso { get; set; }

    public long? IdMedico { get; set; }

    public virtual Medico? IdMedicoNavigation { get; set; }

    public virtual Paciente? IdPacienteNavigation { get; set; }

    public virtual Permiso IdPermisoNavigation { get; set; } = null!;
}
