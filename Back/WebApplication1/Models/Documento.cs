using System;
using System.Collections.Generic;

namespace NaureBack.Models;

public partial class Documento
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Archivo { get; set; }

    public int IdServicio { get; set; }

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
