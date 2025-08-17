using System;
using System.Collections.Generic;

namespace NaureBack.Models;

public partial class Servicio
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();

    public virtual ICollection<Contratacion> Contratacions { get; set; } = new List<Contratacion>();

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
