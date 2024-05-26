using System;
using System.Collections.Generic;

namespace ApiCentroMedico.Models;

public partial class Admin
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Pass { get; set; } = null!;
}
