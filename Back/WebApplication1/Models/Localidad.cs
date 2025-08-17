using System;
using System.Collections.Generic;

namespace NaureBack.Models;

public partial class Localidad
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Geometria { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
