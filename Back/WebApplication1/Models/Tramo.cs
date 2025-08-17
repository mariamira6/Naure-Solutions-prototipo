using System;
using System.Collections.Generic;

namespace NaureBack.Models;

public partial class Tramo
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public DateOnly? FechaDesde { get; set; }

    public DateOnly? FechaHasta { get; set; }

    public decimal? Importe { get; set; }

    public bool? Pagado { get; set; }

    public int IdContratacion { get; set; }

    public virtual Contratacion IdContratacionNavigation { get; set; } = null!;
}
