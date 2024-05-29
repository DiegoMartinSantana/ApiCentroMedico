using System;
using System.Collections.Generic;

namespace ApiCentroMedico.Models;

public partial class Permiso
{
    public int Idpermiso { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
