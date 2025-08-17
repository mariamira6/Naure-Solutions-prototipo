using System;
using System.Collections.Generic;

namespace NaureBack.Models;

public partial class Contratacion
{
    public int Id { get; set; }

    public DateOnly? FechaDesde { get; set; }

    public DateOnly? FechaHasta { get; set; }

    public int IdServicio { get; set; }

    public int IdCliente { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Usuario IdClienteNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;

    public virtual ICollection<Tramo> Tramos { get; set; } = new List<Tramo>();
}
